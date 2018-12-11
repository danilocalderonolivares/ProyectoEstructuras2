using System;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
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
        //Variable glovales de posicionamiento
        private double vgLatitudInicial = 40.6473035625225;
        private double vgLongitudInicial = -104.677734375;
        // Variables globales de enrutamiento
        private bool vgTrazarRuta = false;
        private int vgContadorIndicadoresDeRuta = 0;
        private GMapMarker MarkerA;
        private GMapMarker MarkerB;
        private GMapOverlay MarkerOverlayRuta = new GMapOverlay("Marcadores");
        public Mapa() 
        {  
            InitializeComponent();
            this.Gestor = Gestor.GetInstancia();
        }
        private void Mapa_Load(object sender, EventArgs e)
        {
           
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
            // Se agregan los overrlays al mapa
            GMapOverlay MarkerOverlay = new GMapOverlay("Aristas");
            gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Vertices");
            gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Marcadores");
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
                Marker.ToolTip = new GMapRoundedToolTip(Marker);
                Brush ToolTipBackColor = new SolidBrush(Color.Blue);
                Font f = new Font("Arial", 12, FontStyle.Bold);
                ToolTipBackColor.GetType();
                Marker.ToolTip.Stroke.Color = Color.Red;
                Marker.ToolTip.TextPadding = new Size(5,10);
                Marker.ToolTip.Font = f;
                Marker.ToolTip.Fill = ToolTipBackColor;
                ToolTipBackColor = new SolidBrush(Color.White);
                Marker.ToolTip.Foreground = ToolTipBackColor;
                Marker.ToolTipText = String.Format(" \n" + datos["Nombre"].ToString());

                Marker.Tag = datos["Nombre"];
                gMapControl1.Overlays[1].Markers.Add(Marker);
                this.Gestor.InsertarVertice(datos["Nombre"].ToString(), Convert.ToDouble(datos["Longitud"]), Convert.ToDouble(datos["Latitud"]));
            }
            MarcartRutas();
        }
        private void MarcartRutas()
        {
            GMapRoute RutaObtenida;
            
            for (int i = 0, j = 1; j < gMapControl1.Overlays[1].Markers.Count;i++)
            {
                RutaObtenida = new GMapRoute (new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[j].Position }, "Ruta");
                RutaObtenida.Stroke.Width = 2;
                RutaObtenida.Stroke.Color = Color.SeaGreen;
                gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[j].Tag.ToString(), RutaObtenida.Distance * 1000);
                if (i + 2 < gMapControl1.Overlays[1].Markers.Count)
                {
                    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 2].Position }, "Ruta");
                    RutaObtenida.Stroke.Width = 2;
                    RutaObtenida.Stroke.Color = Color.SeaGreen;
                    gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 2].Tag.ToString(), RutaObtenida.Distance * 1000);
                }
                //if (i + 5 < gMapControl1.Overlays[1].Markers.Count)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 5].Position }, "Ruta");
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 5].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                //if (i + 10 < gMapControl1.Overlays[1].Markers.Count)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 10].Position }, "Ruta");
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 10].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                //if (i - 7 > 0)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i - 7].Position }, "Ruta");
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i - 7].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                //if (i + 5 < gMapControl1.Overlays[1].Markers.Count)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 5].Position }, "Ruta" + i + "" + j);
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 5].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                //if (i + 6 < gMapControl1.Overlays[1].Markers.Count)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 6].Position }, "Ruta" + i + "" + j);
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[2].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 6].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                j++;
            }
        }



        private void btnLlegar_Click(object sender, EventArgs e)
        {
            gMapControl1.Zoom = 7;
            gMapControl1.Overlays[2].Clear();
            gMapControl1.Overlays[3].Clear();
            this.vgTrazarRuta = true;
            btnLlegar.Enabled = false;
            MarcartRutas();
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (this.vgTrazarRuta)
            {
                GMarkerGoogle Marker;
                Brush ToolTipBackColor = new SolidBrush(Color.Red);
                Font f = new Font("Arial", 7, FontStyle.Bold);
                switch (vgContadorIndicadoresDeRuta)
                {
                    case 0:
                        this.vgContadorIndicadoresDeRuta++;
                        this.MarkerA = item;

                        Marker = new GMarkerGoogle(this.MarkerA.Position, GMarkerGoogleType.blue);
                        Marker.ToolTipMode = MarkerTooltipMode.Always;
                        Marker.ToolTip = new GMapRoundedToolTip (Marker);
                        ToolTipBackColor.GetType();
                        Marker.ToolTip.Stroke.Width = 2;
                        Marker.ToolTip.Stroke.Color = Color.Black;
                        Marker.ToolTip.TextPadding = new Size(5, 5);
                        Marker.ToolTip.Font = f;
                        Marker.ToolTip.Fill = ToolTipBackColor;
                        ToolTipBackColor = new SolidBrush(Color.White);
                        Marker.ToolTip.Foreground = ToolTipBackColor;
                        Marker.ToolTipText = String.Format("\nPunto A");
                        MarkerOverlayRuta.Markers.Add(Marker);
                        gMapControl1.Overlays[2] = MarkerOverlayRuta;
                        break;
                    case 1:
                        this.vgContadorIndicadoresDeRuta++;
                        this.MarkerB = item;
                        List<Lugar> listaCaminoMinimo = this.Gestor.GetRutaMinimaDijkstra(MarkerA.Tag.ToString(), MarkerB.Tag.ToString());
                        if (listaCaminoMinimo != null)
                        {
                            MarcarRuta(listaCaminoMinimo);
                            Marker = new GMarkerGoogle(this.MarkerB.Position, GMarkerGoogleType.blue);
                            Marker.ToolTipMode = MarkerTooltipMode.Always;
                            Marker.ToolTip = new GMapRoundedToolTip(Marker);
                            ToolTipBackColor.GetType();
                            Marker.ToolTip.Stroke.Color = Color.Black;
                            Marker.ToolTip.TextPadding = new Size(5,5);
                            Marker.ToolTip.Font = f;
                            Marker.ToolTip.Fill = ToolTipBackColor;
                            ToolTipBackColor = new SolidBrush(Color.White);
                            Marker.ToolTip.Foreground = ToolTipBackColor;
                            Marker.ToolTipText = String.Format("\nPunto B");
                            MarkerOverlayRuta.Markers.Add(Marker);
                            gMapControl1.Overlays[2] = MarkerOverlayRuta;
                        }
                        else
                        {
                            string result = "";
                            if (this.MarkerA.Tag.ToString().Equals(this.MarkerB.Tag.ToString()))
                            {
                                result = "el punto de inicio y final es el mismo";
                            }
                            MessageBox.Show("Ocurrio un error " + result);
                        }
                        this.vgContadorIndicadoresDeRuta = 0;
                        this.vgTrazarRuta = false;
                        btnLlegar.Enabled = true;
                        break;
                }
                gMapControl1.Zoom = 7;
                gMapControl1.Zoom = 4;
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
            gMapControl1.Overlays[0].Clear();
            RutaObtenida.Stroke.Color = Color.Red;
            gMapControl1.Overlays[3].Routes.Add(RutaObtenida);
           
        }



        private void btnBucarVerice_Click(object sender, EventArgs e)
        {
            string txt = this.txtNombreVertice.Text.ToUpper();
            BuscarVertice(txt);
            this.txtNombreVertice.Focus();
        }
        private void BuscarVertice( string pNombreVertice)
        {
            Lugar lugarEncontrado = this.Gestor.BuscarVerticePorNombre(pNombreVertice);
            if (lugarEncontrado == null)
            {
                this.textDatosVerticeBuscado.Text = "\n\nNo se encontro un vertice con ese nombre";
                if (btnIrVerticer.Visible)
                {
                    btnIrVerticer.Visible = false;
                }
                gMapControl1.Zoom = 4;
                return;
            }
            if (!btnIrVerticer.Visible)
            {
                btnIrVerticer.Visible = true;
            }
            this.textDatosVerticeBuscado.Text = String.Format("\n\n Nombre: {0}\n Longitud: {1}\n Latitud: {2}\n", lugarEncontrado.GetNombre(), lugarEncontrado.GetLongitud(), lugarEncontrado.GetLatitud());
            this.vgLatitudInicial = lugarEncontrado.GetLatitud();
            this.vgLongitudInicial = lugarEncontrado.GetLongitud();
        }

        private void btnIrVerticer_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(this.vgLatitudInicial, this.vgLongitudInicial);
            gMapControl1.Zoom = 7;
        }

        private void btnCerraApp_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Desea salir de la aplicación?","Salir ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if ( result == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
