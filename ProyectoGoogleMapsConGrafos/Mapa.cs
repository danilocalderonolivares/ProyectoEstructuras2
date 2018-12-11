using System;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using Core;

namespace ProyectoGoogleMapsConGrafos
{
    public partial class Mapa : Form
    {
        //Variable gloval Gestor
        private Gestor Gestor;
        //Tabla global de almacenamiento de datos 
        private DataTable vgDataTable;
        //Variable glovales de posicionamiento
        private  int vgFilaSeleccionada = 0;
        private double vgLatitudInicial = 40.6473035625225;
        private double vgLongitudInicial = -104.677734375;
        // Variables globales de enrutamiento
        bool vgTrazarRuta = false;
        int vgContadorIndicadoresDeRuta = 0;
        PointLatLng vgPuntoA;
        PointLatLng vgPuntoB;
        GMapMarker MarkerA;
        GMapMarker MarkerB;
        public Mapa() 
        {  
            InitializeComponent();
            this.Gestor = Gestor.GetInstancia();
        }
        private void Mapa_Load(object sender, EventArgs e)
        {
            //Se crean las columnas del DataTable
            vgDataTable = new DataTable();
            vgDataTable.Columns.Add(new DataColumn("Nombre",typeof( string )));
            vgDataTable.Columns.Add(new DataColumn("Latitud", typeof(double)));
            vgDataTable.Columns.Add(new DataColumn("Longitud", typeof(double)));
            //Se agregan registros(Rows) al DataTable
            //Se agrega el DataTable al DataGridView
            dataGridView2.DataSource = vgDataTable;
            //Inicializacion de propiedades por defecto del control gMap
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(this.vgLatitudInicial, this.vgLongitudInicial);
            gMapControl1.MinZoom = 4;
            gMapControl1.MaxZoom = 7;
            gMapControl1.Zoom = 4;
            gMapControl1.AutoScroll = true;
            //Marcador
            GMarkerGoogle Marker;
            GMapOverlay MarkerOverlay = new GMapOverlay("Marcador");
            Marker = new GMarkerGoogle(new PointLatLng(vgLatitudInicial, vgLongitudInicial),GMarkerGoogleType.red);
            MarkerOverlay.Markers.Add(Marker);
            // Se agregan los overrlays al mapa
            gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Vertices");
            gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Aristas");
            gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Ruta");
            gMapControl1.Overlays.Add(MarkerOverlay);
            InicializarDatos();
        }
        private void InicializarDatos()
        {
            SerializadorJson Serializador = SerializadorJson.GetInstancia();
            object[] objetosResultante = (object[])Serializador.GetDatosDelArchivoJson("Lugares");

            for (int i = 0; i < objetosResultante.Length; i++)
            {
                Dictionary<string, object> datos = (Dictionary<string, object>)objetosResultante[i];
                GMarkerGoogle Marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(datos["Latitud"]), Convert.ToDouble(datos["Longitud"])), GMarkerGoogleType.green);
                Marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                Marker.ToolTipText = String.Format("Nombre lugar: {0} \nUbicacion: \nLatitud: {1} \nLongitud: {2}", datos["Nombre"], datos["Latitud"], datos["Longitud"]);
                Marker.Tag = datos["Nombre"];
                gMapControl1.Overlays[1].Markers.Add(Marker);
                vgDataTable.Rows.Add(datos["Nombre"], datos["Latitud"], datos["Longitud"]);
                this.Gestor.InsertarVertice(datos["Nombre"].ToString(), Convert.ToDouble(datos["Longitud"]), Convert.ToDouble(datos["Latitud"]));
            }
            MarcartRutas();
        }
        private void MarcartRutas()
        {
            GMapRoute RutaObtenida;
            //label2.Text = (RutaObtenida.Distance * 1000).ToString();
            for (int i = 0,j = 0; i < gMapControl1.Overlays[1].Markers.Count;)
            {
                if (j == 0 && i+1 < gMapControl1.Overlays[1].Markers.Count)
                {
                    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i+1].Position }, "Ruta" + i + "" + j);
                    j++;
                    RutaObtenida.Stroke.Width = 2;
                    RutaObtenida.Stroke.Color = Color.SeaGreen;
                    gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 1].Tag.ToString(), RutaObtenida.Distance * 1000);
                    if (i + 2 < gMapControl1.Overlays[1].Markers.Count)
                    {
                        RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 2].Position }, "Ruta" + i + "" + j);
                        RutaObtenida.Stroke.Width = 2;
                        RutaObtenida.Stroke.Color = Color.SeaGreen;
                        gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                        this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 2].Tag.ToString(), RutaObtenida.Distance * 1000);
                    }
                }
                else 
                {
                    
                    if (i + 5 < gMapControl1.Overlays[1].Markers.Count)
                    {
                        RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 5].Position }, "Ruta" + i + "" + j);
                        RutaObtenida.Stroke.Width = 2;
                        RutaObtenida.Stroke.Color = Color.SeaGreen;
                        gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                        this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 5].Tag.ToString(), RutaObtenida.Distance * 1000);
                    }
                    j = 0;
                    i++;

                }
            }
           
        }
        private void SeleccionarRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {

            //Se obtiene el index de la fila que usuario seleciono el dataGridView
            gMapControl1.Overlays[1].Markers[vgFilaSeleccionada].ToolTipMode = MarkerTooltipMode.OnMouseOver;
            vgFilaSeleccionada = e.RowIndex;
            txtDescripcion.Text = dataGridView2.Rows[vgFilaSeleccionada].Cells[0].Value.ToString();
            txtLatitud.Text = dataGridView2.Rows[vgFilaSeleccionada].Cells[1].Value.ToString();
            txtLongitud.Text = dataGridView2.Rows[vgFilaSeleccionada].Cells[2].Value.ToString();

            //Se posiciona mapa en el marcador
            gMapControl1.Position = gMapControl1.Overlays[1].Markers[vgFilaSeleccionada].Position;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Se inserta un registro en el DataTable
            vgDataTable.Rows.Add(txtDescripcion.Text,txtLatitud.Text,txtLongitud.Text);

            //Creamos el marcador para moverlo al lugar en el mapa donde le usuario dio doble click.
            GMarkerGoogle Marker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(txtLatitud.Text), Convert.ToDouble(txtLongitud.Text)), GMarkerGoogleType.green);
            Marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            Marker.ToolTipText = String.Format("Ubicacion: \nLatitud: {0} \nLongitud: {1}", txtLatitud.Text, txtLongitud.Text);
            gMapControl1.Overlays[1].Markers.Add(Marker);
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Se elimina un registro del  DataTable
            vgDataTable.Rows.RemoveAt(vgFilaSeleccionada);
        }
        private void btnLlegar_Click(object sender, EventArgs e)
        {
            gMapControl1.Overlays[2].Clear();
            gMapControl1.Overlays[3].Clear();
            this.vgTrazarRuta = true;
            btnLlegar.Enabled = false;
            MarcartRutas();
        }
        public void CrearDireccionTrazarRuta(double pLatitud, double pLongitud)
        {
            if (this.vgTrazarRuta)
            {
                switch (vgContadorIndicadoresDeRuta)
                {
                    case 0:
                        this.vgContadorIndicadoresDeRuta++;
                        this.vgPuntoA = new PointLatLng(pLatitud,pLongitud);
                        break;
                    case 1:
                        this.vgContadorIndicadoresDeRuta++;
                        this.vgPuntoB = new PointLatLng(pLatitud, pLongitud);
                        GMapProviders.GoogleMap.ApiKey = "AIzaSyAKaUlHFCwxvkDF2Itv3twhKgW3-gYmzUA";
                        //GDirections Directions;
                        //DirectionsStatusCode RutasDireccion = GMapProviders.GoogleMap.GetDirections(out Directions, this.vgPuntoA, this.vgPuntoB, false, false, false, false, false);
                        GMapRoute RutaObtenida = new GMapRoute(new List<PointLatLng> { vgPuntoA, vgPuntoB }, "Ruta ubicación");
                        RutaObtenida.Stroke.Width = 2;
                        RutaObtenida.Stroke.Color = Color.SeaGreen;

                        GMapOverlay CapaRutas = new GMapOverlay("Capa Ruta");
                        CapaRutas.Routes.Add(RutaObtenida);
                        gMapControl1.Overlays[1] = CapaRutas;


                        label2.Text = (RutaObtenida.Distance * 1000).ToString();

                        gMapControl1.Zoom = gMapControl1.Zoom + 1;
                        gMapControl1.Zoom = gMapControl1.Zoom - 1;
                        this.vgContadorIndicadoresDeRuta = 0;
                        this.vgTrazarRuta = false;
                        btnLlegar.Enabled = true;

                        break;
                }
            }
        }
        private void gMapControl1_OnMarkerDoubleClick(GMapMarker item, MouseEventArgs e)
        {
            //Se obtienen la longitud y la latitud  donde el usuario dio doble click
            double latitud = item.Position.Lat;
            double longitud = item.Position.Lng;
            //Se escribe el los txt de latiud y longitus los datos corespondientes.
            txtLatitud.Text = latitud.ToString();
            txtLongitud.Text = longitud.ToString();

            this.CrearDireccionTrazarRuta(latitud, longitud);
        }
        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Se obtienen la longitud y la latitud  donde el usuario dio doble click
            double latitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double longitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            //Se escribe el los txt de latiud y longitus los datos corespondientes.
            txtLatitud.Text = latitud.ToString();
            txtLongitud.Text = longitud.ToString();
            //Creamos el marcador para moverlo al lugar en el mapa donde le usuario dio doble click.
            gMapControl1.Overlays[0].Markers[0].Position = new PointLatLng(latitud, longitud);
            gMapControl1.Overlays[0].Markers[0].ToolTipText = String.Format("Ubicacion: \nLatitud: {0} \nLongitud: {1}", latitud, longitud);
            this.CrearDireccionTrazarRuta(latitud, longitud);

        }
        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (this.vgTrazarRuta)
            {
                switch (vgContadorIndicadoresDeRuta)
                {
                    case 0:
                        this.vgContadorIndicadoresDeRuta++;
                        this.MarkerA = item;
                        break;
                    case 1:
                        this.vgContadorIndicadoresDeRuta++;
                        this.MarkerB = item;
                        List<Lugar> listaCaminoMinimo = this.Gestor.GetRutaMinimaDijkstra(MarkerA.Tag.ToString(), MarkerB.Tag.ToString());
                        MarcarRuta(listaCaminoMinimo);
                        this.vgContadorIndicadoresDeRuta = 0;
                        this.vgTrazarRuta = false;
                        btnLlegar.Enabled = true;
                        break;
                }
            }
        }
        private void MarcarRuta(List<Lugar> listaDatos)
        {
            List<PointLatLng> listaDePuntosLongLati = new List<PointLatLng>();
            for (int i = 0; i< listaDatos.Count; i++)
            {
                listaDePuntosLongLati.Add(new PointLatLng(listaDatos[i].GetLatitud(),listaDatos[i].GetLongitud()));
            }
            GMapRoute  RutaObtenida = new GMapRoute(listaDePuntosLongLati,"Camino");
            RutaObtenida.Stroke.Width = 2;
            gMapControl1.Overlays[2].Clear();
            RutaObtenida.Stroke.Color = Color.Red;
            gMapControl1.Overlays[3].Routes.Add(RutaObtenida);
           
        }
    }
}
