using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class Cola<T>
    {
        private NodoL<T> Inicio;
        private int Tamanio;
        public Cola()
        {
            this.Inicio = new NodoL<T>();
            this.Inicio.SetSig(null);
            Inicio.SetAnt(Inicio);
            this.Tamanio = 0;
        }
        public T Eliminar(T pDato)
        {
            NodoL<T> temp = this.Inicio, anterior = this.Inicio;
            int longit = this.GetTamanio();
            int rta;
            for (int i = 0; i < this.GetTamanio(); i++)
            {
                rta = Comparable.GetInstancia().Compare(Inicio.GetInfo(), pDato);
                if (rta == 0 && i == 0)
                {
                    this.Inicio = temp.GetSig();
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
        private NodoL<T> GetPos(int i)
        {
            if (i < 0 || i >= this.GetTamanio())
            {
                Console.WriteLine("Error indice no valido en una Lista Circular!");
                return null;
            }
            else
            {
                NodoL<T> x = this.Inicio.GetSig();
                for (; i-- > 0; x = x.GetSig())
                {
                    return x;
                }
            }
            return null;
        }
        public bool EnColar(T pDato)
        {
            bool result = false;
            NodoL<T> newNode = new NodoL<T>(pDato,null), aux;
            if (this.EsVacia() || this.Tamanio == 0)
            {
                this.Inicio = newNode;
                result = true;
            }
            else
            {
                aux = this.Inicio;
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
            int longt = this.GetTamanio();
            longt = longt + 1;
            this.Tamanio = longt ;
            return result;
        }
        public bool EnColar(NodoL<T> pNodo)
        {
            bool result = false;
            NodoL<T> aux;
            pNodo.SetSig(null);
            if (this.EsVacia() || this.Tamanio == 0)
            {
                this.Inicio = pNodo;
                result = true;
            }
            else
            {
                aux = this.Inicio;
                bool bandera = true;
                while (bandera)
                {
                    if (aux.GetSig() == null)
                    {
                        aux.SetSig(pNodo);
                        result = true;
                        bandera = false;
                    }
                    aux = aux.GetSig();
                }
            }
            int longt = this.GetTamanio();
            longt = longt + 1;
            this.Tamanio = longt;
            return result;
        }
        public  NodoL<T> Buscar(int pDato)
        {
            NodoL<T> nodo;
            nodo = this.GetInicio();
            while (nodo != null)
            {
                if (Convert.ToInt32( nodo.GetInfo() ) == pDato)
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

        public T DeColar()
        {
            if (this.EsVacia())
                return default(T);
            NodoL<T> x = this.Inicio.GetSig();
            this.Inicio.SetSig(x.GetSig());
            x.GetSig().SetAnt(Inicio);
            x.SetSig(null);
            x.SetAnt(null);
            this.Tamanio--;
            return (x.GetInfo());
        }
        public void Vaciar()
        {
            this.Inicio.SetSig(this.Inicio);
            this.Inicio.SetAnt(this.Inicio);
            this.Tamanio = 0;
        }
        public NodoL<T> ElimiAlFinal()
        {

            NodoL<T> aux, eliminado = null;
            int longt = this.GetTamanio();
            if (longt > 0)
            {
                if (longt == 1)
                {
                    this.Inicio = null;
                }
                else
                {
                    aux = this.Inicio;
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
        public  NodoL<T> GetInicio()
        {
            return this.Inicio;
        }
        public T GetInfoInicio()
        {
            return this.Inicio.GetSig().GetInfo();
        }
        public void AumentarTamanio()
        {
            this.Tamanio++;
        }
        public void SetInicio(NodoL<T> ini)
        {
            this.Inicio = ini;
        }
        public int GetTamanio()
        {
            return (this.Tamanio);
        }
        public bool EsVacia()
        {
            return (this.GetTamanio() == 0);
        }

        public override String ToString()
        {
            string valores = "";
            int cont = 0;
            NodoL<T> aux;
            aux = this.Inicio;
            valores = "Longitud : " + this.GetTamanio() + "\n";
            while (this.GetTamanio() > cont)
            {
                valores = valores + "\n           Dato : " + aux.GetInfo() + ", Nodo: " + aux.GetHashCode() + " \n";
                aux = aux.GetSig();
                cont++;
            }
            return valores;
        }

    }
}
