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
        private Gestor Gestor;
        private double vgLatitudInicial = 40.6473035625225;
        private double vgLongitudInicial = -104.677734375;
        private bool vgTrazarRuta = false;
        private bool vgVerAdyacentes = false;
        private bool vgVerArco = false; 
        private int vgContadorIndicadoresDeRuta = 0;
        private GMapMarker vgMarcadorA;
        private GMapMarker vgMarcadorB;
        private GMapOverlay vgCapaMarcadores = new GMapOverlay("Marcadores");
        public Mapa() 
        {  
            InitializeComponent();
            this.Gestor = Gestor.GetInstancia();
        }
        private void Mapa_Load(object sender, EventArgs e)
        {
           
            //Inicializacion de propiedades por defecto del control gMap
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            this.gMapControl1.DragButton = MouseButtons.Left;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.MapProvider = GoogleMapProvider.Instance;
            this.gMapControl1.Position = new PointLatLng(this.vgLatitudInicial, this.vgLongitudInicial);
            this.gMapControl1.MinZoom = 4;
            this.gMapControl1.MaxZoom = 7;
            this.gMapControl1.Zoom = 4;
            this.gMapControl1.AutoScroll = true;
            // Se agregan los overrlays al mapa
            GMapOverlay MarkerOverlay = new GMapOverlay("Aristas");
            gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Vertices");
            this.gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Marcadores");
            this.gMapControl1.Overlays.Add(MarkerOverlay);
            MarkerOverlay = new GMapOverlay("Ruta");
            this.gMapControl1.Overlays.Add(MarkerOverlay);
            InicializarDatos();
        }
        private void InicializarDatos()
        {
            SerializadorJson Serializador = SerializadorJson.GetInstancia();
            object[] objetosResultante = (object[])Serializador.GetDatosDelArchivoJson("Lugares");

            for (int i = 0; i < objetosResultante.Length; i++)
            {
                Dictionary<string, object> datos = (Dictionary<string, object>)objetosResultante[i];
                GMarkerGoogle Marcador = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(datos["Latitud"]), Convert.ToDouble(datos["Longitud"])), GMarkerGoogleType.green);
                Marcador.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                Marcador.ToolTip = new GMapRoundedToolTip(Marcador);
                Brush ColorFondoInformacion = new SolidBrush(Color.Blue);
                Font letra = new Font("Arial", 12, FontStyle.Bold);
                ColorFondoInformacion.GetType();
                Marcador.ToolTip.Stroke.Color = Color.Red;
                Marcador.ToolTip.TextPadding = new Size(5,10);
                Marcador.ToolTip.Font = letra;
                Marcador.ToolTip.Fill = ColorFondoInformacion;
                ColorFondoInformacion = new SolidBrush(Color.White);
                Marcador.ToolTip.Foreground = ColorFondoInformacion;
                Marcador.ToolTipText = String.Format(" \n" + datos["Nombre"].ToString());

                Marcador.Tag = datos["Nombre"];
                this.gMapControl1.Overlays[1].Markers.Add(Marcador);
                this.Gestor.InsertarVertice(datos["Nombre"].ToString(), Convert.ToDouble(datos["Longitud"]), Convert.ToDouble(datos["Latitud"]));
            }
            MarcartRutas();
        }
        private void MarcartRutas()
        {
            GMapRoute RutaObtenida;
            
            for (int i = 0, j = 1; j < gMapControl1.Overlays[1].Markers.Count;i++)
            {
                RutaObtenida = new GMapRoute (new List<PointLatLng> { this.gMapControl1.Overlays[1].Markers[i].Position, this.gMapControl1.Overlays[1].Markers[j].Position }, "Ruta");
                RutaObtenida.Stroke.Width = 2;
                RutaObtenida.Stroke.Color = Color.SeaGreen;
                gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[j].Tag.ToString(), RutaObtenida.Distance * 1000);
                if (i + 2 < gMapControl1.Overlays[1].Markers.Count)
                {
                    RutaObtenida = new GMapRoute(new List<PointLatLng> { this.gMapControl1.Overlays[1].Markers[i].Position, this.gMapControl1.Overlays[1].Markers[i + 2].Position }, "Ruta");
                    RutaObtenida.Stroke.Width = 2;
                    RutaObtenida.Stroke.Color = Color.SeaGreen;
                    this.gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                    this.Gestor.InsertarArco(this.gMapControl1.Overlays[1].Markers[i].Tag.ToString(), this.gMapControl1.Overlays[1].Markers[i + 2].Tag.ToString(), RutaObtenida.Distance * 1000);
                }
                if (i + 5 < gMapControl1.Overlays[1].Markers.Count)
                {
                    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 5].Position }, "Ruta");
                    RutaObtenida.Stroke.Width = 2;
                    RutaObtenida.Stroke.Color = Color.SeaGreen;
                    gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 5].Tag.ToString(), RutaObtenida.Distance * 1000);
                }
                //if (i + 10 < gMapControl1.Overlays[1].Markers.Count)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 10].Position }, "Ruta");
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 10].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                //if (i - 7 > 0)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i - 7].Position }, "Ruta");
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i - 7].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                //if (i + 5 < gMapControl1.Overlays[1].Markers.Count)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 5].Position }, "Ruta" + i + "" + j);
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 5].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                //if (i + 6 < gMapControl1.Overlays[1].Markers.Count)
                //{
                //    RutaObtenida = new GMapRoute(new List<PointLatLng> { gMapControl1.Overlays[1].Markers[i].Position, gMapControl1.Overlays[1].Markers[i + 6].Position }, "Ruta" + i + "" + j);
                //    RutaObtenida.Stroke.Width = 2;
                //    RutaObtenida.Stroke.Color = Color.SeaGreen;
                //    gMapControl1.Overlays[0].Routes.Add(RutaObtenida);
                //    this.Gestor.InsertarArco(gMapControl1.Overlays[1].Markers[i].Tag.ToString(), gMapControl1.Overlays[1].Markers[i + 6].Tag.ToString(), RutaObtenida.Distance * 1000);
                //}
                j++;
            }
        }
        private void gMapControl1_OnMarkerClick(GMapMarker pMarcador, MouseEventArgs e)
        {
            if (this.vgTrazarRuta && !this.vgVerAdyacentes && !this.vgVerArco)
            {
                this.TrasarRutMasCaminoCorto(pMarcador);
            }
            if (this.vgVerAdyacentes && !this.vgVerArco && !this.vgTrazarRuta)
            {
                this.TrasarRutaVerticesAyacentes(pMarcador);
            }
            if (this.vgVerArco && !this.vgVerAdyacentes && !this.vgTrazarRuta)
            {
                this.TrasarRutaArco(pMarcador);
            }
        }
        private void TrasarRutaArco(GMapMarker pMarcador)
        {
            GMarkerGoogle Marcador;
            string result = "\n";
            Brush ColorFondoInformacion = new SolidBrush(Color.Red);
            Font f = new Font("Arial", 7, FontStyle.Bold);
            switch (this.vgContadorIndicadoresDeRuta)
            {
                case 0:
                    this.vgContadorIndicadoresDeRuta++;
                    this.vgMarcadorA = pMarcador;
                    Marcador = new GMarkerGoogle(this.vgMarcadorA.Position, GMarkerGoogleType.blue);
                    Marcador.Tag = pMarcador.Tag;
                    Marcador.ToolTipMode = MarkerTooltipMode.Always;
                    Marcador.ToolTip = new GMapRoundedToolTip(Marcador);
                    ColorFondoInformacion.GetType();
                    Marcador.ToolTip.Stroke.Width = 2;
                    Marcador.ToolTip.Stroke.Color = Color.Black;
                    Marcador.ToolTip.TextPadding = new Size(5, 5);
                    Marcador.ToolTip.Font = f;
                    Marcador.ToolTip.Fill = ColorFondoInformacion;
                    ColorFondoInformacion = new SolidBrush(Color.White);
                    Marcador.ToolTip.Foreground = ColorFondoInformacion;
                    Marcador.ToolTipText = String.Format("\nPunto A");
                    this.vgCapaMarcadores.Markers.Add(Marcador);
                    this.gMapControl1.Overlays[2] = this.vgCapaMarcadores;
                    break;
                case 1:
                    this.vgContadorIndicadoresDeRuta++;
                    this.vgMarcadorB = pMarcador;
                    Arista<Lugar> arco = this.Gestor.GetArco(this.vgMarcadorA.Tag.ToString(), this.vgMarcadorB.Tag.ToString());
                    if (arco != null)
                    {
                        List<Lugar> listaLugares = new List<Lugar> { arco.GetVertA().Info, arco.GetVertB().Info };
                        MarcarRuta(listaLugares);
                        Marcador = new GMarkerGoogle(this.vgMarcadorB.Position, GMarkerGoogleType.blue);
                        Marcador.Tag = pMarcador.Tag;
                        Marcador.ToolTipMode = MarkerTooltipMode.Always;
                        Marcador.ToolTip = new GMapRoundedToolTip(Marcador);
                        ColorFondoInformacion.GetType();
                        Marcador.ToolTip.Stroke.Color = Color.Black;
                        Marcador.ToolTip.TextPadding = new Size(5, 5);
                        Marcador.ToolTip.Font = f;
                        Marcador.ToolTip.Fill = ColorFondoInformacion;
                        ColorFondoInformacion = new SolidBrush(Color.White);
                        Marcador.ToolTip.Foreground = ColorFondoInformacion;
                        Marcador.ToolTipText = String.Format("\nPunto B");
                        this.vgCapaMarcadores.Markers.Add(Marcador);
                        this.gMapControl1.Overlays[2] = this.vgCapaMarcadores;
                        result += "Punto A: " + arco.GetVertA().Info.GetNombre() + "\n";
                        result += "Punto B: " + arco.GetVertB().Info.GetNombre() + "\n";
                        result += "Peso: " + arco.GetPeso() + " Metros";
                        this.textDatosVerticeBuscado.Text = result;
                    }
                    else
                    {

                        if (arco == null)
                        {
                            result = "el punto de inicio y final no son un arco";
                        }
                        MessageBox.Show("Ocurrio un error " + result);
                        this.gMapControl1.Overlays[2].Clear();
                    }
                    this.vgContadorIndicadoresDeRuta = 0;
                    this.vgVerArco = false;
                    this.btnVerArco.Enabled = true;
                    this.btnVerArco.BackColor = Color.MediumSeaGreen;
                    break;
            }
            RefrecarMapa();
        }
        private void TrasarRutaVerticesAyacentes(GMapMarker pMarcador)
        {
            List<Lugar> listaLugaresAyacentes = this.Gestor.ObtenerVerticesAyacentes(pMarcador.Tag.ToString());
            string result = "\n";
            if (listaLugaresAyacentes != null)
            {
                result += "Punto A : " + pMarcador.Tag.ToString()+"\n";
                result += String.Format("{0}\t\t{1}\n", "Puntos B: ", "Peso:");
                GMapRoute RutaObtenida;
                this.gMapControl1.Overlays[0].Clear();
                this.gMapControl1.Overlays[2].Clear();
                this.gMapControl1.Overlays[3].Clear();
                List<PointLatLng> listaDePuntosLongLati = new List<PointLatLng>();
                Brush ToolTipBackColor = new SolidBrush(Color.Red);
                Font f = new Font("Arial", 7, FontStyle.Bold);
                GMarkerGoogle Marcador = new GMarkerGoogle(pMarcador.Position, GMarkerGoogleType.blue);
                Marcador.Tag = pMarcador.Tag;
                this.vgCapaMarcadores.Markers.Add(Marcador);
                this.gMapControl1.Overlays[3] = vgCapaMarcadores;
                for (int i = 0; i < listaLugaresAyacentes.Count; i++)
                {
                    RutaObtenida = new GMapRoute(new List<PointLatLng> { pMarcador.Position, new PointLatLng { Lat = listaLugaresAyacentes[i].GetLatitud(), Lng = listaLugaresAyacentes[i].GetLongitud() } }, "Camino");
                    RutaObtenida.Stroke.Width = 2;
                    RutaObtenida.Stroke.Color = Color.Red;
                    this.gMapControl1.Overlays[3].Routes.Add(RutaObtenida);
                    result += String.Format("{0}      \t{1}",listaLugaresAyacentes[i].GetNombre(), RutaObtenida.Distance * 1000 + " M") + "\n";
                }
                RefrecarMapa();
            }
            else
            {
                result += "No se encontraron vertices ayacentes";
            }
            this.textDatosVerticeBuscado.Text = result;
            this.vgVerAdyacentes = false;
            this.btnVerVerticesAdyacentes.Enabled = true;
            this.btnVerVerticesAdyacentes.BackColor = Color.MediumSeaGreen;
        }
        private void TrasarRutMasCaminoCorto(GMapMarker pMarcador)
        {
            GMarkerGoogle Marcador;
            Brush ColorFondoInformacion = new SolidBrush(Color.Red);
            Font f = new Font("Arial", 7, FontStyle.Bold);
            string result = "";
            switch (this.vgContadorIndicadoresDeRuta)
            {
                case 0:
                    this.vgContadorIndicadoresDeRuta++;
                    this.vgMarcadorA = pMarcador;

                    Marcador = new GMarkerGoogle(this.vgMarcadorA.Position, GMarkerGoogleType.blue);
                    Marcador.Tag = pMarcador.Tag;
                    Marcador.ToolTipMode = MarkerTooltipMode.Always;
                    Marcador.ToolTip = new GMapRoundedToolTip(Marcador);
                    ColorFondoInformacion.GetType();
                    Marcador.ToolTip.Stroke.Width = 2;
                    Marcador.ToolTip.Stroke.Color = Color.Black;
                    Marcador.ToolTip.TextPadding = new Size(5, 5);
                    Marcador.ToolTip.Font = f;
                    Marcador.ToolTip.Fill = ColorFondoInformacion;
                    ColorFondoInformacion = new SolidBrush(Color.White);
                    Marcador.ToolTip.Foreground = ColorFondoInformacion;
                    Marcador.ToolTipText = String.Format("\nPunto A");
                    this.vgCapaMarcadores.Markers.Add(Marcador);
                    this.gMapControl1.Overlays[2] = this.vgCapaMarcadores;
                    break;
                case 1:
                    this.vgContadorIndicadoresDeRuta++;
                    this.vgMarcadorB = pMarcador;
                    List<Lugar> listaCaminoMinimo = this.Gestor.GetRutaMinimaDijkstra(this.vgMarcadorA.Tag.ToString(), this.vgMarcadorB.Tag.ToString());
                    if (listaCaminoMinimo != null)
                    {
                        result =MarcarRuta(listaCaminoMinimo);
                        Marcador = new GMarkerGoogle(this.vgMarcadorB.Position, GMarkerGoogleType.blue);
                        Marcador.Tag = pMarcador.Tag;
                        Marcador.ToolTipMode = MarkerTooltipMode.Always;
                        Marcador.ToolTip = new GMapRoundedToolTip(Marcador);
                        ColorFondoInformacion.GetType();
                        Marcador.ToolTip.Stroke.Color = Color.Black;
                        Marcador.ToolTip.TextPadding = new Size(5, 5);
                        Marcador.ToolTip.Font = f;
                        Marcador.ToolTip.Fill = ColorFondoInformacion;
                        ColorFondoInformacion = new SolidBrush(Color.White);
                        Marcador.ToolTip.Foreground = ColorFondoInformacion;
                        Marcador.ToolTipText = String.Format("\nPunto B");
                        this.vgCapaMarcadores.Markers.Add(Marcador);
                        this.gMapControl1.Overlays[2] = this.vgCapaMarcadores;
                        this.textDatosVerticeBuscado.Text = result;
                    }
                    else
                    {
                        if (this.vgMarcadorA.Tag.ToString().Equals(this.vgMarcadorB.Tag.ToString()))
                        {
                            result = "el punto de inicio y final es el mismo";
                        }
                        MessageBox.Show("Ocurrio un error " + result);
                        this.gMapControl1.Overlays[2].Clear();
                    }
                    this.vgContadorIndicadoresDeRuta = 0;
                    this.vgTrazarRuta = false;
                    this.btnLlegar.Enabled = true;
                    this.btnLlegar.BackColor = Color.MediumSeaGreen;
                    break;
            }
            RefrecarMapa();
        }
        private string  MarcarRuta(List<Lugar> pListaDatos)
        {
            string result = "", costo = "\n";
            if (pListaDatos != null && pListaDatos.Count > 0)
            {
                result += "Vertices:\n";
                List<PointLatLng> listaDePuntosLongLati = new List<PointLatLng>();
                for (int i = 0; i < pListaDatos.Count; i++)
                {
                    listaDePuntosLongLati.Add(new PointLatLng(pListaDatos[i].GetLatitud(), pListaDatos[i].GetLongitud()));
                    result +=pListaDatos[i].GetNombre() + "\n";
                }
                GMapRoute RutaObtenida = new GMapRoute(listaDePuntosLongLati, "Camino");
                costo += "Costo: "+ RutaObtenida.Distance*1000+" Metros\n";
                costo += result;
                result = costo;
                RutaObtenida.Stroke.Width = 2;
                this.gMapControl1.Overlays[0].Clear();
                this.gMapControl1.Overlays[2].Clear();
                this.gMapControl1.Overlays[3].Clear();
                RutaObtenida.Stroke.Color = Color.Red;
                this.gMapControl1.Overlays[3].Routes.Add(RutaObtenida);
            }

            return result;
        }



        private void btnBucarVerice_Click(object sender, EventArgs e)
        {
            string txt = this.txtNombreVertice.Text.ToUpper();
            this.BuscarVertice(txt);
            this.txtNombreVertice.Focus();
        }
        private void BuscarVertice( string pNombreVertice)
        {
            Lugar lugarEncontrado = this.Gestor.BuscarVerticePorNombre(pNombreVertice);
            if (lugarEncontrado == null)
            {
                this.textDatosVerticeBuscado.Text = "\n\nNo se encontro un vertice con ese nombre";
                if (this.btnIrVerticer.Visible)
                {
                    this.btnIrVerticer.Visible = false;
                }
                this.gMapControl1.Zoom = 4;
                return;
            }
            if (!this.btnIrVerticer.Visible)
            {
                this.btnIrVerticer.Visible = true;
            }
            this.textDatosVerticeBuscado.Text = String.Format("\n\n Nombre: {0}\n Longitud: {1}\n Latitud: {2}\n", lugarEncontrado.GetNombre(), lugarEncontrado.GetLongitud(), lugarEncontrado.GetLatitud());
            this.vgLatitudInicial = lugarEncontrado.GetLatitud();
            this.vgLongitudInicial = lugarEncontrado.GetLongitud();
        }

        private void btnIrVerticer_Click(object sender, EventArgs e)
        {
            this.gMapControl1.Position = new PointLatLng(this.vgLatitudInicial, this.vgLongitudInicial);
            this.gMapControl1.Zoom = 7;
        }

        private void btnCerraApp_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Desea salir de la aplicación?","Salir ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if ( result == DialogResult.OK)
            {
                this.Close();
            }
        }
        private void RefrecarMapa()
        {
            this.gMapControl1.Zoom = 3;
            this.gMapControl1.Zoom = 4;
        }
        private void btnLlegar_Click(object sender, EventArgs e)
        {

            if (!this.vgVerAdyacentes && !this.vgVerArco)
            {
                this.RefrecarMapa();
                this.gMapControl1.Overlays[2].Clear();
                this.gMapControl1.Overlays[3].Clear();
                this.vgTrazarRuta = true;
                this.btnLlegar.Enabled = false;
                this.btnLlegar.BackColor = Color.DarkGreen;
                MarcartRutas();
            }
            else
            {
                MessageBox.Show("Ya esta seleccionada una de la funcionalidade de aplicacion");
            }

        }
        private void btnVerVerticesAdyacentes_Click(object sender, EventArgs e)
        {
            if (!this.vgVerArco && !this.vgTrazarRuta)
            {
                this.gMapControl1.Overlays[2].Clear();
                this.gMapControl1.Overlays[3].Clear();
                this.vgVerAdyacentes = true;
                this.btnVerVerticesAdyacentes.Enabled = false;
                this.btnVerVerticesAdyacentes.BackColor = Color.DarkGreen;
                this.textDatosVerticeBuscado.Text = "";
                this.MarcartRutas();
            }
            else
            {
                MessageBox.Show("Ya esta seleccionada una de la funcionalidade de aplicacion");
            }
        }

        private void btnVerArco_Click(object sender, EventArgs e)
        {

            if (!this.vgVerAdyacentes && !this.vgTrazarRuta)
            {
                this.gMapControl1.Overlays[2].Clear();
                this.gMapControl1.Overlays[3].Clear();
                this.vgVerArco = true;
                this.btnVerArco.Enabled = false;
                this.btnVerArco.BackColor = Color.DarkGreen;
                this.textDatosVerticeBuscado.Text = "";
                this.MarcartRutas();
            }
            else
            {
                MessageBox.Show("Ya esta seleccionada una de la funcionalidad de aplicacion");
            }
           
        }
    }
}
