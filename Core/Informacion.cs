using System;

namespace Core
{
    public class Informacion<Clave, T> : Object
    {
        private T Information;
        private Clave clave;
        private int Estado = 0;

        public Informacion()
        {
            this.clave = default(Clave);
            this.Information = default(T);
            this.Estado = 0;
        }
        public Informacion(Clave clave, T objeto)
        {
            this.clave = clave;
            this.Information = objeto;
            this.Estado = 2;
        }
        public Informacion(Clave clave)
        {
            this.Information = default(T);
            this.clave = clave;
            this.Estado = 1;
        }
        public T GetInformacion()
        {
            return this.Information;
        }
        public int GetEstado()
        {
            return this.Estado;
        }
        public Clave GetClave()
        {
            return this.clave;
        }

        public bool SetClave(Clave pClave)
        {
            if (pClave != null)
            {
                this.clave = pClave;
                return true;
            }
            return false;
        }
        public bool SetObjeto(T obje)
        {
            if (obje != null)
            {
                this.Information = obje;
                return true;
            }
            return false;
        }

        public bool SetEstado(int estado)
        {
            if (estado >= 0 && estado <= 2)
            {
                this.Estado = estado;
                return true;
            }
            throw new Exception("El estado del componente debe estar entre 0 y 2 donde 0 es nulo, 1 es borrado y 2 en uso");
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
        public override String ToString()
        {
            if (this.Estado == 2)
            {
                return ("Clave: " + this.clave.ToString() + " del Objeto: " + this.Information.ToString() + "\n");
            }
            return "";
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                throw new Exception("El objeto insertado no puede estar nulo");
            }
            Informacion<Clave, T> x = (Informacion<Clave, T>)obj;
            return (this.clave.Equals(x.GetClave()));
        }
    }
}
