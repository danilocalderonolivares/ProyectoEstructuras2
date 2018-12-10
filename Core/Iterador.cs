using System;
namespace Core
{
    public class Iterador<T>
    {
        private NodoL<T> Nodo;
        public Iterador(NodoL<T> posicion)
        {
            this.Nodo = posicion;
        }
        public bool HasNext()
        {
            return (Nodo != null);
        }
        public T Next()
        {
            if (!this.HasNext())
            {
                Console.WriteLine("No hay mas elementos");
                return default(T);
            }
            NodoL<T> nodoActual = Nodo;
            Nodo = Nodo.GetSig();
            return (nodoActual.GetInfo());
        }
    }
}
