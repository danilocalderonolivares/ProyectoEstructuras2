namespace Core
{
    public class Vertice<T>
    {
        public T Info { get; set; }
        public string Nombre { get; set; }
        public bool EsVisit { get; set; }
        public Vertice(string pNombre)
        {
            this.Nombre = pNombre;
            this.EsVisit = false;
        }
        public Vertice(T pInfo)
        {
            this.Info = pInfo;
            this.EsVisit = false;
        }
        public Vertice(string pNombre, T pInfo)
        {
            this.Nombre = pNombre;
            this.Info = pInfo;
            this.EsVisit = false;
        }
        public Vertice(bool pEsvisitado, string pNombre, T pInfo)
        {
            this.Nombre = pNombre;
            this.Info = pInfo;
            this.EsVisit = pEsvisitado;
        }
    }
}
