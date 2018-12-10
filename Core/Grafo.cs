using System;

namespace Core
{
    public class Grafo<T>
    {
        private bool Dirigido = false;
        private Arista<T>[,] MatrizArcos = new Arista<T>[8, 8];
        private TablaHash<String, Vertice<T>> ArregloDVertices;

        public Grafo()
        {
            ArregloDVertices = new TablaHash<String, Vertice<T>>();
            this.InicializarArregloDVertices();
        }
        public Grafo(bool pDirigido)
        {
            this.Dirigido = pDirigido;
            ArregloDVertices = new TablaHash<String, Vertice<T>>();
            this.InicializarArregloDVertices();
        }
        private void InicializarArregloDVertices()
        {
            int indice = 0;
            for (int valorAscii = 65; valorAscii < 73; valorAscii++)
            {
                ArregloDVertices.Insertar(Convert.ToChar(valorAscii).ToString(), new Vertice<T>(Convert.ToChar(valorAscii).ToString()));
                indice++;
            }
        }
        public bool GetDirigido()
        {
            return this.Dirigido;
        }
        public void SetDirigido(bool pDirigido)
        {
            this.Dirigido = pDirigido;
        }
        public bool InsertarArco(string pVertice1, string pVertice2, int pPeso)
        {
            if (!(ExisteArco(pVertice1, pVertice2)))
            {
                CrearRelacion(pVertice1, pVertice2, pPeso);
                return true;
            }
            return false;
        }
        private void CrearRelacion(string pVerticeA, string pVerticeB, int pPeso = 0)
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
                        result += MatrizArcos[index, i].GetVertB().Nombre;
                        result += "\n";
                    }
                }
            }
            Console.WriteLine(result);
            return result;
        }
        public Lista<Vertice<T>> GetListaSucesores(string pVertice)
        {
            Lista<Vertice<T>> ListaVerticesSucesores = new Lista<Vertice<T>>();
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        ListaVerticesSucesores.InsertarAlFinal(MatrizArcos[index, i].GetVertB());
                    }
                }
            }
            return ListaVerticesSucesores;
        }
        public Lista<Vertice<T>> GetListaPredecesores(string pVertice)
        {
            Lista<Vertice<T>> ListaVerticesPredecesores = new Lista<Vertice<T>>();
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        ListaVerticesPredecesores.InsertarAlFinal(MatrizArcos[i, index].GetVertA());
                    }
                }
            }
            return ListaVerticesPredecesores;
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
    }
}
