namespace Core
{
    public class Vertice<T>
    {
        public int X = 300;
        public int Y = 60;
        public int Radio = 50;
        private T Info;
        public string Nombre { get; set; }
        private bool EsVisit;
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
        public Vertice(string pNombre,T pInfo)
        {
            this.Nombre = pNombre;
            this.Info = pInfo;
            this.EsVisit = false;
        }
        public T GetInfo()
        {
            return Info;
        }
        public bool GetVisit()
        {
            return EsVisit;
        }
        public void SetInfo(T pInfo)
        {
            this.Info = pInfo;
        }
        public void SetVisit(bool pEsVisit)
        {
            this.EsVisit = pEsVisit;
        }
        public bool Equals(Vertice<T> pVertice)
        {
            return (this.Info.Equals(pVertice.GetInfo()));
        }
        public override string ToString()
        {
            return "(" + this.Nombre + ")";
        }
    }
}
