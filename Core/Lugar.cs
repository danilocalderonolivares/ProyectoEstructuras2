namespace Core
{
    public class Lugar
    {
        public string Nombre { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public Lugar()
        {
            this.Nombre = "";
            this.Latitud = 0;
            this.Longitud = 0;
        }
        public Lugar(string pNombre, double pLatitud, double pLongitud)
        {
            this.Nombre = pNombre;
            this.Latitud = pLatitud;
            this.Longitud = pLongitud;
        }
    }
}
