using System;
namespace Core
{
    public class Lista<T>
    {
        private NodoL<T> Cabeza;
        private int Tamanio = 0;
        public Lista()
        {
            this.Cabeza = null;
  
        }
        public Lista(T pPato)
        {
            this.Cabeza = new NodoL<T>(pPato);
            this.Cabeza.SetSig(null);
        }
        public NodoL<T> GetCabeza()
        {
            return Cabeza;
        }
        public bool InsertarAlInicio(T pDato)
        {
            bool result = false;
            NodoL<T> newNode = new NodoL<T>(pDato);
            if (this.EsVacia())
            {
                this.Cabeza = newNode;
                result = true;
            }
            else
            {
                newNode.SetSig(this.Cabeza);
                this.Cabeza = newNode;
                result = true;
            }
            int longt = this.GetTamanio();
            longt = longt + 1;
            this.Tamanio = longt;
            return result;
        }
        public bool InsertarAlInicio(NodoL<T> pNodo)
        {
            bool result = false;
            if (this.EsVacia())
            {
                pNodo.SetSig(null);
                this.Cabeza = pNodo;
                result = true;
            }
            else
            {
                pNodo.SetSig(this.Cabeza);
                this.Cabeza = pNodo;
                result = true;
            }
            int longt = this.GetTamanio();
            longt = longt + 1;
            this.Tamanio = longt;
            return result;
        }
        public bool InsertarAlFinal(T pDato)
        {
            bool result = false;
            NodoL<T> newNode = new NodoL<T>(pDato), aux;
            newNode.SetSig(null);
            if (this.EsVacia())
            {
                this.Cabeza = newNode;
                result = true;
            }
            else
            {
                int rta = Comparable.GetInstancia().Compare(this.Cabeza.GetInfo(), 0);
                if (rta == 0 && this.Cabeza.GetSig() == null && this.Tamanio == 0)
                {
                    this.Cabeza = newNode;
                    result = true;
                }
                else
                {
                    aux = this.Cabeza;
                    bool bandera = true;
                    while (bandera)
                    {
                        if (aux.GetSig() == null)
                        {
                            aux.SetSig(newNode);
                            result = true;
                            bandera = false;
                        }
                        aux = aux.GetSig();
                    }
                }
            }

            int longt = this.GetTamanio();
            longt = longt + 1;
            this.Tamanio = longt;

            return result;
        }
        public bool InsertarOrdenado(T pDato)
        {
            bool result = false;
            NodoL<T> newNode = new NodoL<T>(pDato, null);
            if (this.EsVacia())
            {
                this.Cabeza = newNode;
                result = true;
            }
            else
            {
                NodoL<T> temp = Cabeza;
                int rta = Comparable.GetInstancia().Compare(Cabeza.GetInfo(), pDato);
                if (rta == 1)
                {
                    newNode.SetSig(Cabeza);
                    this.Cabeza = newNode;
                    result = true;
                }
                else
                {
                    while ((temp.GetSig() != null) && (Comparable.GetInstancia().Compare(temp.GetSig().GetInfo(), pDato) < 0))
                    {
                        temp = temp.GetSig();
                    }
                    newNode.SetSig(temp.GetSig());
                    temp.SetSig(newNode);
                    result = true;
                }
            }
            int longt = this.GetTamanio();
            longt = longt + 1;
            this.Tamanio = longt;
            return result;
        }
        public NodoL<T> Buscar(int pDato)
        {
            NodoL<T> nodo;
            nodo = this.Cabeza;
            while (nodo != null)
            {
                if (Convert.ToInt32(nodo.GetInfo()) == pDato)
                {
                    return (nodo);
                }
                else
                {
                    nodo = nodo.GetSig();
                }

            }
            return (null);
        }
        public T Eliminar(T pDato)
        {
            NodoL<T> temp = this.Cabeza, anterior = this.Cabeza;
            int longit = this.GetTamanio();
            int rta;
            for (int i = 0; i < this.GetTamanio(); i++)
            {
                rta = Comparable.GetInstancia().Compare(Cabeza.GetInfo(), pDato);
                if (rta == 0 && i == 0)
                {
                    this.Cabeza = temp.GetSig();
                    longit = longit - 1;
                    this.Tamanio = longit;
                }
                rta = Comparable.GetInstancia().Compare(temp.GetInfo(), pDato);
                if (rta == 0 && i > 0)
                {
                    anterior.SetSig(temp.GetSig());
                    longit = longit - 1;
                    this.Tamanio = longit;
                }
                anterior = temp;
                temp = temp.GetSig();
            }
            return default(T);
        }
        public T Eliminar(int i)
        {
            try
            {
                NodoL<T> x;
                if (i == 0)
                {
                    x = this.Cabeza.GetSig();
                    this.Cabeza.SetSig(x.GetSig());
                    this.Cabeza.GetSig().SetAnt(this.Cabeza);
                    x.SetSig(null);
                    x.SetAnt(null);
                    this.Tamanio--;
                    return (x.GetInfo());
                }
                x = this.GetPos(i - 1);
                if (x == null)
                    return (default(T));
                NodoL<T> y = x.GetSig();
                x.SetSig(y.GetSig());
                y.GetSig().SetAnt(x);
                y.SetSig(null);
                y.SetAnt(null);
                this.Tamanio--;
                return (y.GetInfo());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (default(T));
        }
        public void Vaciar()
        {
            this.Cabeza.SetSig(Cabeza);
            this.Tamanio = 0;
        }
        public T Get(int i)
        {
            try
            {
                NodoL<T> x = this.GetPos(i);
                if (x == null)
                {
                    return default(T);
                }
                return (x.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return default(T);
        }
        public void Set(int i, T dato)
        {
            try
            {
                NodoL<T> t = this.GetPos(i);
                if (t == null)
                {
                    return;
                }
                t.SetInfo(dato);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int GetTamanio()
        {
            return (this.Tamanio);
        }

        public bool EsVacia()
        {
            return (Cabeza == null || this.Tamanio == 0);

        } 


        public bool Esta(T info)
        {
            return (this.GetIndice(info) != -1);
        }

        public  NodoL<T> ElimiAlFinal()
        {

            NodoL<T> aux, eliminado = null;
            int longt = this.GetTamanio();
            if (longt > 0)
            {
                if (longt == 1)
                {
                    this.Cabeza = null;
                }
                else
                {
                    aux = this.Cabeza;
                    bool bandera = true;
                    while (bandera)
                    {
                        if (aux.GetSig().GetSig() == null)
                        {
                            eliminado = aux.GetSig();
                            aux.SetSig(null);
                            bandera = false;
                            break;
                        }
                        aux = aux.GetSig();
                    }
                }
                longt = longt - 1;
                this.Tamanio = longt;
            }
            return eliminado;
        }
        public bool EnlazarLista(Lista<T> lista)
        {
            return Enlazar(this.GetCabeza(), lista);
        }
        private bool Enlazar(NodoL<T> cabeza, Lista<T> lista)
        {
            bool result = false;
            if (cabeza.GetSig() is null)
            {
                cabeza.SetSig(lista.GetCabeza());
                result = true;
            }
            else
            {
                result = Enlazar(cabeza.GetSig(), lista);
            }
            return result;
        }
        public Object[] AVector()
        {
            if (this.EsVacia())
            {
                return null;
            }
            Object[] vector = new Object[this.GetTamanio()];
            NodoL<T> actual = this.Cabeza;
            for (int i = 0; i < this.GetTamanio(); i++)
            {
                vector[i] = actual;
                actual = actual.GetSig();
            }
            return vector;
        }
        public override String ToString()
        {
            string valores = "";
            int cont = 0;
            NodoL<T> aux;
            aux = this.Cabeza;
            valores = "Longitud : " + this.GetTamanio() + "\n";
            while (this.GetTamanio() > cont)
            {
                valores = valores + "\n           Dato : " + aux.GetInfo() + ", Nodo: " + aux.GetHashCode() + " \n";
                aux = aux.GetSig();
                cont++;
            }
            return valores;
        }
        private NodoL<T> GetPos(int i)
        {
            if (i < 0 || i >= this.GetTamanio())
            {
                Console.WriteLine("Error indice no valido en una Lista Circular!");
                return null;
            }
            else
            {
                NodoL<T> x = this.Cabeza;
                for (int j = 0; j < i && x.GetSig() != null; j++)
                {
                    x = x.GetSig();
                }
                return x;
            }
        }
        public int GetIndice(T dato)
        {
            int i = -1;
            if (Cabeza.GetSig() is null)
            {
                return -1;
            }
            for (NodoL<T> x = this.Cabeza.GetSig(); x != this.Cabeza && x != null; x = x.GetSig())
            {

                if (x.GetInfo().Equals(dato))
                {
                    return (i);
                }
                i++;
            }
            return (-1);
        }
    }

}
