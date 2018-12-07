using System;

namespace Core
{
    public class Arista<T>
    {
        private Vertice<T> VerticeA;
        private Vertice<T> VerticeB;
        private double Peso;
        private string Etiqueta;
        private bool EsVisit;
        private T Informacion { get; set; }
        public T Info
        {
            get
            {
                return Informacion;
            }
            set
            {
                if (value != null)
                {
                    Informacion = value;
                }
            }
        }
        public Arista(Vertice<T> pVerticeA, Vertice<T> pVerticeB, double pPeso)
        {
            this.VerticeA = pVerticeA;
            this.VerticeB = pVerticeB;
            this.Peso = pPeso;
            this.EsVisit = false;
        }
        public Arista(Vertice<T> pVerticeA, Vertice<T> pVerticeB, double pPeso,string pEtiqueta)
        {
            this.VerticeA = pVerticeA;
            this.VerticeB = pVerticeB;
            this.Peso = pPeso;
            this.Etiqueta = pEtiqueta;
            this.EsVisit = false;
        }
        public Vertice<T> GetVertA()
        {
            return VerticeA;
        }
        public void SetVertA(Vertice<T> pVerticeA)
        {
            this.VerticeA = pVerticeA;
        }
        public Vertice<T> GetVertB()
        {
            return VerticeB;
        }
        public void SetVertB(Vertice<T> pVerticeB)
        {
            this.VerticeB = pVerticeB;
        }
        public double GetPeso()
        {
            return Peso;
        }
        public string GetEtiqueta()
        {
            return this.Etiqueta;
        }
        public void SetPeso(int pPeso)
        {
            this.Peso = pPeso;
        }
        public void SetEtiqueta(string pEtiqueta)
        {
            this.Etiqueta = pEtiqueta;
        }
        public bool GetVisit()
        {
            return EsVisit;
        }
        public void SetVisit(bool pEsVisit)
        {
            this.EsVisit = pEsVisit;
        }
        public override String ToString()
        {
            return "(" + this.VerticeA.Nombre + " ----- " + (this.Peso == 0 ? "" : "" + Peso) +" "+(this.Etiqueta =="" || this.Etiqueta == null?"":this.Etiqueta) + " ----- " + this.VerticeB.Nombre + ")";
        }
        public  String ToString(bool pDirigido)
        {
            if (pDirigido)
            {
                return "(" + this.VerticeA.Nombre + " ----- "+ (this.Peso == 0 ? "" : "" + Peso) + " " + (this.Etiqueta == "" || this.Etiqueta == null ? "" : this.Etiqueta) + " ----> "  + this.VerticeB.Nombre + ")";
            }
            return "(" + this.VerticeA.Nombre + " ----- " + (this.Peso == 0 ? "" : "" + Peso) + " " + (this.Etiqueta == "" || this.Etiqueta == null ? "" : this.Etiqueta) + " ----- " + this.VerticeB.Nombre +")";
        }
    }
}
