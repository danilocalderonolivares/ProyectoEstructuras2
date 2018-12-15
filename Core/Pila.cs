using System;
namespace Core
{
    public class Pila<T>
    {
        private NodoL<T> Tope;
        private int Tamanio;
        public Pila()
        {
            this.Tope = null;
            this.Tamanio = 0;
        }
        public void Apilar(T info)
        {
            if (this.EsVacia())
            {
                this.Tope = new NodoL<T>(info, null);
            }
            else
            {
                this.Tope = new NodoL<T>(info, this.Tope);
            }
            this.Tamanio++;

        }
        public NodoL<T> ElimiAlFinal()
        {

            NodoL<T> aux,eliminado = null;
            int longt = this.GetTamanio();
            if (longt > 0)
            {
                if (longt == 1)
                {
                    this.Tope = null;
                }
                else
                {
                    aux = this.Tope;
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
        public void Apilar(NodoL<T> pNodo)
        {
            if (this.EsVacia())
            {
                this.Tope = pNodo;
            }
            else
            {
                pNodo.SetSig( this.Tope);
                this.Tope = pNodo;
            }
            int longt = this.GetTamanio();
            longt = longt + 1;
            this.Tamanio = longt;

        }
        public T Eliminar(T pDato)
        {
            NodoL<T> temp = this.Tope, anterior = this.Tope;
            int longit = this.GetTamanio();
            int rta;
            for (int i = 0; i < this.GetTamanio(); i++)
            {
                rta = Comparable.GetInstancia().Compare(Tope.GetInfo(), pDato);
                if (rta == 0 && i == 0)
                {
                    this.Tope = temp.GetSig();
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
        public NodoL<T> Buscar(int pDato)
        {
            NodoL<T> nodo;
            nodo = this.Tope;
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
        public T Desapilar()
        {
            if (this.EsVacia())
            {
                return (default(T));
            }
            NodoL<T> x = this.Tope;
            this.Tope = Tope.GetSig();
            this.Tamanio--;
            if (Tamanio == 0)
            {
                this.Tope = null;
            }
            return (x.GetInfo());
        }
        public void Vaciar()
        {
            this.Tope = null;
            this.Tamanio = 0;
        }
        public T GetTope()
        {
            return (this.Tope.GetInfo());
        }
        public NodoL<T> GetNodoTope()
        {
            return (this.Tope);
        }
        public int GetTamanio()
        {
            return (this.Tamanio);
        }
        public bool EsVacia()
        {
            return (this.Tope == null || this.Tamanio == 0);
        }
        public override String ToString()
        {

            string valores = "";
            int cont = 0;
            NodoL<T> aux;
            aux = this.Tope;
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
                NodoL<T> x = this.Tope.GetSig();
                for (; i-- > 0; x = x.GetSig())
                {
                    return x;
                }
            }
            return null;
        }
    }
}
