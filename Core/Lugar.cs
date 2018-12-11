namespace Core
{
    public class Lugar
    {
        private string Nombre;
        private double Longitud;
        private double Latitud;
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

        public string GetNombre()
        {
            return this.Nombre;
        }
        public double GetLatitud()
        {
            return this.Latitud;
        }
        public double GetLongitud()
        {
            return this.Longitud;
        }
        public void SetNombre(string pNombre)
        {
            this.Nombre = pNombre;
        }
        public void SetLatitud(double pLatitud)
        {
            this.Latitud = pLatitud;
        }
        public void SetLongitud(double pLongitud)
        {
            this.Longitud = pLongitud;
        }
    }
}
