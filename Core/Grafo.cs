using System;

namespace Core
{
    public class Grafo<T>
    {
        private bool Dirigido = false;
        private Arista<T>[,] MatrizArcos;
        private TablaHash<String, Vertice<T>>  ArregloDVertices;

        public Grafo()
        {
            ArregloDVertices = new TablaHash<String, Vertice<T>>(40);
            MatrizArcos = new Arista<T>[TablaHash<String, Vertice<T>>.ObtenerPrimo(40), TablaHash<String, Vertice<T>>.ObtenerPrimo(40)];
        }
        public Grafo(bool pDirigido)
        {
            this.Dirigido = pDirigido;
            ArregloDVertices = new TablaHash<String, Vertice<T>>(40);
            MatrizArcos = new Arista<T>[TablaHash<String, Vertice<T>>.ObtenerPrimo(40), TablaHash<String, Vertice<T>>.ObtenerPrimo(40)];
        }
        public  bool InsertaVertice(string pNombre, T pInformacion)
        {
            return  ArregloDVertices.Insertar(pNombre, new Vertice<T>(pNombre,pInformacion));
        }
        public bool InsertarArco(string pVertice1, string pVertice2, double pPeso)
        {
            if (!(ExisteArco(pVertice1, pVertice2)))
            {
                CrearRelacion(pVertice1, pVertice2, pPeso);
                return true;
            }
            return false;
        }
        public bool GetDirigido()
        {
            return this.Dirigido;
        }
        public void SetDirigido(bool pDirigido)
        {
            this.Dirigido = pDirigido;
        }

        private void CrearRelacion(string pVerticeA, string pVerticeB, double pPeso = 0)
        {
            int indexA, indexB;
            indexA = ArregloDVertices.GetIndex(pVerticeA);
            indexB = ArregloDVertices.GetIndex(pVerticeB);
            if (indexA >= 0 && indexB >= 0)
            {
                MatrizArcos[indexA, indexB] = new Arista<T>(ArregloDVertices.BuscaHashPorClave(pVerticeA), ArregloDVertices.BuscaHashPorClave(pVerticeB), pPeso);
                if (!Dirigido)
                {
                    MatrizArcos[indexB, indexA] = new Arista<T>(ArregloDVertices.BuscaHashPorClave(pVerticeB), ArregloDVertices.BuscaHashPorClave(pVerticeA), pPeso);
                }

            }
        }
        private bool ExisteArco(string pVertice1, string pVertice2)
        {
            bool siExiste = false;
            int index1, index2;
            index1 = ArregloDVertices.GetIndex(pVertice1);
            index2 = ArregloDVertices.GetIndex(pVertice2);
            if (index1 >= 0 && index2 >= 0)
            {
                if (MatrizArcos[index1, index2] != null)
                {
                    siExiste = true;
                }
            }
            return siExiste;
        }
        private Arista<T> GetArco(string pVertice1, string pVertice2)
        {
            int index1, index2;
            index1 = ArregloDVertices.GetIndex(pVertice1);
            index2 = ArregloDVertices.GetIndex(pVertice2);
            if (index1 >= 0 && index2 >= 0)
            {
                if (MatrizArcos[index1, index2] != null)
                {
                    return MatrizArcos[index1, index2];
                }
            }
            return null;
        }
        private bool ExisteVertice(string pVertice1, string pVertice2)
        {
            bool siExiste = false;
            int index1, index2;
            index1 = ArregloDVertices.GetIndex(pVertice1);
            index2 = ArregloDVertices.GetIndex(pVertice2);
            if (index1 >= 0 && index2 >= 0)
            {
                    siExiste = true;
            }
            return siExiste;
        }
        private Vertice<T>  GetVerticePorNombre(string pNombreVertice)
        {
            int index1 = ArregloDVertices.GetIndex(pNombreVertice);
            if (index1 >= 0)
            {
                return ArregloDVertices.GetForIndex(index1);
            }
            return null;
        }
        public Lista<T> GetUbicacion(string pNombreVertice)
        {
            return null;
        }
        public Lista<T> GetCaminoMasCerca(string pNombreVerticeA,string pNombreVerticeB)
        {
            return null;
        }
        public Lista<T> GetCaminoMasLargo(string pNombreVerticeA, string pNombreVerticeB)
        {
            return null;
        }
        public Lista<T> GetUbicacionesAdyacentes(string pNombreVertice)
        {
            return null;
        }
        public override string ToString()
        {
            return MostrarArcos();
        }
        private string MostrarArcos()
        {
            string result = "";
            for (int i = 0; i < MatrizArcos.GetLength(0); i++)
            {
                for (int j = 0; j < MatrizArcos.GetLength(1); j++)
                {
                    if (MatrizArcos[i, j] != null)
                    {
                        result += MatrizArcos[i, j].ToString(this.Dirigido);
                    }
                }
                result += "\n";
            }
            Console.WriteLine(result);
            return result;
        }
        public string MostrarPredecesores(string pVertice)
        {
            string result = "";
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                for (int i = 0; i < MatrizArcos.GetLength(0); i++)
                {
                    if (MatrizArcos[i, index] != null)
                    {
                        result += MatrizArcos[i, index].GetVertA().Nombre;
                        result += "\n";
                    }
                }
            }
            Console.WriteLine(result);
            return result;
        }
        public string MostrarSucesores(string pVertice)
        {
            string result = "";
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        result += MatrizArcos[index,i].GetVertB().Nombre;
                        result += "\n";
                    }
                }
            }
            Console.WriteLine(result);
            return result;
        }
        public Lista<Vertice<T>> GetListaSucesores(string pVertice)
        {
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                Lista<Vertice<T>> ListaVerticesSucesores = new Lista<Vertice<T>>();
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        ListaVerticesSucesores.InsertarAlFinal( MatrizArcos[index, i].GetVertB());
                    }
                }
                return ListaVerticesSucesores;
            }
            return null;
        }
        public Lista<Vertice<T>> GetListaPredecesores(string pVertice)
        {
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                Lista<Vertice<T>> ListaVerticesPredecesores = new Lista<Vertice<T>>();
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        ListaVerticesPredecesores.InsertarAlFinal(MatrizArcos[i,index].GetVertA());
                    }
                }
                return ListaVerticesPredecesores;
            }
            return null;
        }
        public Vertice<T> GetVeticePorNombre(string pNombre)
        {
            Vertice<T> vertice = ArregloDVertices.BuscaHashPorClave(pNombre);
            if (vertice == null)
            {
                return null;
            }
            return vertice;
        }
        public string MostrarMatrizDAdyacencias()
        {
            string result = "", fila = "";
            for (int i = 0; i < MatrizArcos.GetLength(0); i++)
            {
                result += ArregloDVertices.GetForIndex(i).Nombre + "|";
                fila += "\t" + ArregloDVertices.GetForIndex(i).Nombre;
                for (int j = 0; j < MatrizArcos.GetLength(1); j++)
                {
                    if (MatrizArcos[i, j] == null)
                    {
                        result += "\tNULL";
                    }
                    else
                    {
                        result += "\t" + MatrizArcos[i, j].GetPeso();
                    }
                }
                result += "\n";
            }
            fila = fila + "\n" + result;
            result = fila;
            Console.WriteLine(result);
            return result;
        }
        private bool HayPesosNegativosEnArcos()
        {
            for (int i = 0; i < MatrizArcos.GetLength(0); i++)
            {
                for (int j = 0; j < MatrizArcos.GetLength(1); j++)
                {
                    if (MatrizArcos[i, j]!= null )
                    {
                        if (MatrizArcos[i, j].GetPeso() < 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void LimpiaVisitasVertices()
        {
            foreach (Informacion<string, Vertice<T>> Informacion in this.ArregloDVertices.GetInformacionEntrada())
            {
                if (Informacion.GetInformacion() != null)
                {
                    Informacion.GetInformacion().EsVisit = false;
                }
            }
        }
        public Cola<Vertice<T>> RutaMinimaDijkstra(string pNombreVerticeA, string pNombreVerticeB)
        {
            Cola<Vertice<T>> ColaCaminoCorto = new Cola<Vertice<T>>();
            if (this.HayPesosNegativosEnArcos())
            {
                return null;
            }
            Arista<T>[,] MatrizAristas = new Arista<T>[MatrizArcos.GetLength(0),MatrizArcos.GetLength(1)];
            this.LimpiaVisitasVertices();
            int[] costos = new int[ArregloDVertices.GetTamanio()];

            Lista<Vertice<T>> ListaSucesores = this.GetListaSucesores(pNombreVerticeA);
            Vertice<T> verticeActual = null, verticeInicio = this.GetVerticePorNombre(pNombreVerticeA);
            verticeInicio.EsVisit = true;
            ColaCaminoCorto.EnColar(verticeInicio);
            Arista<T> arco = null;
            int peso = 0, costo = 0; ;
            Iterador<Vertice<T>> iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
            bool noSalir = true;
            while (noSalir)
            {
                for (Vertice<T> verticeAdyac = iterador.Next(); verticeAdyac != null; verticeAdyac = iterador.Next())
                {
                    arco = GetArco(verticeInicio.Nombre, verticeAdyac.Nombre);
                    if (arco != null)
                    {
                        if ((peso == 0 || peso > arco.GetPeso() + costo) && !arco.GetVertB().EsVisit || arco.GetVertB().Nombre.Equals(pNombreVerticeB))
                        {
                            peso = (int)arco.GetPeso() + costo;
                            verticeActual = arco.GetVertB();
                            //vIni.EsVisit = true;
                            if (arco.GetVertB().Nombre.Equals(pNombreVerticeB))
                            {
                                noSalir = false;
                            }
                        }
                    }
                }
                costo = peso;
                peso = 0;
                verticeInicio = verticeActual;
                verticeInicio.EsVisit = true;
                ColaCaminoCorto.EnColar(verticeInicio);
                ListaSucesores = this.GetListaSucesores(verticeInicio.Nombre);
                iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
            }
            //ColaCaminoCorto.EnColar(this.GetVerticePorNombre(pNombreVerticeB));
            return ColaCaminoCorto;
        }

    }
}
