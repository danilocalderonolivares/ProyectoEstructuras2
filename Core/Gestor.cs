using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Gestor
    {
        private static Gestor Instancia;
        private Grafo<Lugar> Grafo;
        private Gestor()
        {
            Grafo = new Grafo<Lugar>(false);
        }
        public static Gestor GetInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new Gestor();
            }
            return Instancia;
        }
        public bool InsertarVertice(string pNombre,double pLongitud,double pLatitud)
        {
            return Grafo.InsertaVertice(pNombre,new Lugar(pNombre,pLatitud,pLongitud));
        }
        public bool InsertarArco(string pNombreVerticeA , string pNombreVerticeB, double pPeso)
        {
            return Grafo.InsertarArco(pNombreVerticeA, pNombreVerticeB, pPeso);
        }
    }
}
