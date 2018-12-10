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
        //Variables glovales de marcador
        GMarkerGoogle vgMarker;
        GMapOverlay vgMarkerOverlay;

        //Tabla global de almacenamiento de datos 
        DataTable vgDataTable;

        //Variable glovales de posicionamiento
        int vgFilaSeleccionada = 0;
        double vgLatitudInicial = 40.6473035625225;
        double vgLongitudInicial = -104.677734375;

        // Variables globales de enrutamiento
        bool vgTrazarRuta = false;
        int vgContadorIndicadoresDeRuta = 0;
        PointLatLng vgPuntoA;
        PointLatLng vgPuntoB;

        public Mapa() 
        {  
            InitializeComponent();

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
            gMapControl1.MaxZoom = 5;
            gMapControl1.Zoom = 4;
            gMapControl1.AutoScroll = true;
            //Marcador
            vgMarkerOverlay = new GMapOverlay("Marcadores");
            GMapOverlay MarkerOverlay = new GMapOverlay("Marcador");
            vgMarker = new GMarkerGoogle(new PointLatLng(vgLatitudInicial, vgLongitudInicial),GMarkerGoogleType.red);
            MarkerOverlay.Markers.Add(vgMarker);
            // Se agrega el overrlay al mapa
            gMapControl1.Overlays.Add(MarkerOverlay);
            gMapControl1.Overlays.Add(vgMarkerOverlay);
            InicializarDatos();
        }

        private void SeleccionarRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {

            //Se obtiene el index de la fila que usuario seleciono el dataGridView
            gMapControl1.Overlays[1].Markers[vgFilaSeleccionada].ToolTipMode = MarkerTooltipMode.OnMouseOver;
            vgFilaSeleccionada = e.RowIndex;
            txtDescripcion.Text = dataGridView2.Rows[vgFilaSeleccionada].Cells[0].Value.ToString();
            txtLatitud.Text = dataGridView2.Rows[vgFilaSeleccionada].Cells[1].Value.ToString();
            txtLongitud.Text = dataGridView2.Rows[vgFilaSeleccionada].Cells[2].Value.ToString();

            gMapControl1.Overlays[1].Markers[vgFilaSeleccionada].ToolTipMode = MarkerTooltipMode.Always;
            //Se posiciona mapa en el marcador
            gMapControl1.Position = gMapControl1.Overlays[1].Markers[vgFilaSeleccionada].Position;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Se inserta un registro en el DataTable
            vgDataTable.Rows.Add(txtDescripcion.Text,txtLatitud.Text,txtLongitud.Text);

            //Creamos el marcador para moverlo al lugar en el mapa donde le usuario dio doble click.
            vgMarker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(txtLatitud.Text), Convert.ToDouble(txtLongitud.Text)), GMarkerGoogleType.green);
            vgMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            vgMarker.ToolTipText = String.Format("Ubicacion: \nLatitud: {0} \nLongitud: {1}", txtLatitud.Text, txtLongitud.Text);
            gMapControl1.Overlays[1].Markers.Add(vgMarker);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Se elimina un registro del  DataTable
            vgDataTable.Rows.RemoveAt(vgFilaSeleccionada);
        }

        private void btnLlegar_Click(object sender, EventArgs e)
        {
            this.vgTrazarRuta = true;
            btnLlegar.Enabled = false;
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
                        GDirections Directions;
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
            //Creamos el marcador para moverlo al lugar en el mapa donde le usuario dio doble click.
            //vgMarker.Position = new PointLatLng(latitud, longitud);
            //vgMarker.ToolTipText = String.Format("Ubicacion: \nLatitud: {0} \nLongitud: {1}", latitud, longitud);
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
        private void InicializarDatos()
        {
            SerializadorJson Serializador = SerializadorJson.GetInstancia();
            object[] objetosResultante = (object[])Serializador.GetDatosDelArchivoJson("Lugares");
            for (int i = 0; i < objetosResultante.Length;i++)
            {
                Dictionary<string, object> datos = (Dictionary<string, object>)objetosResultante[i];
                vgMarker = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(datos["Latitud"]), Convert.ToDouble(datos["Longitud"])), GMarkerGoogleType.green);
                vgMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                vgMarker.ToolTipText = String.Format("Nombre lugar: {0} \nUbicacion: \nLatitud: {1} \nLongitud: {2}", datos["Nombre"], datos["Latitud"], datos["Longitud"]);
                gMapControl1.Overlays[1].Markers.Add(vgMarker);
                vgDataTable.Rows.Add(datos["Nombre"], datos["Latitud"],datos["Longitud"]);
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
