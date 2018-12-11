using System;
using System.Collections.Generic;

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
        public Lista<Lugar> GetCaminoMasRapido(string pNombreVerticeA, string pNombreVerticeB)
        {

            return null;
        }
        public List<Lugar> GetRutaMinimaDijkstra(string pNombreVerticeA, string pNombreVerticeB)
        {
            Lista<Vertice<Lugar>>  ListaCaminoMinimo = Grafo.RutaMinimaDijkstra(pNombreVerticeA, pNombreVerticeB);
            if (ListaCaminoMinimo != null)
            {
                List<Lugar> ListaLugares = new List<Lugar>();
                Iterador<Vertice<Lugar>> iterador = new Iterador<Vertice<Lugar>>(ListaCaminoMinimo.GetCabeza());
                for (Vertice<Lugar> verticeAdyac = iterador.Next(); verticeAdyac != null; verticeAdyac = iterador.Next())
                {
                    ListaLugares.Add(verticeAdyac.Info);
                }

                return ListaLugares;
            }
            return null;
        }
        public Lugar BuscarVerticePorNombre(string pNombreVertice)
        {
            Vertice<Lugar> verticeEncontrado = Grafo.GetVeticePorNombre(pNombreVertice);
            if (verticeEncontrado == null)
            {
                return null;
            }
            return verticeEncontrado.Info;
        }
    }
}
