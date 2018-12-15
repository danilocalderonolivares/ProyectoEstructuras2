namespace Core
{//Pojo
    public class NodoL<T>
    {
        private T Info;
        private NodoL<T> Sig;
        private NodoL<T> Anterior;
        public NodoL()
        {
            this.Info = default(T);
            this.Sig = null;
            this.Anterior = null;
        }
        public NodoL(T info)
        {
            this.Info = info;
            this.Sig = null;
            this.Anterior = null;
        }
        public NodoL(T info, NodoL<T> sig)
        {
            this.Info = info;
            this.Sig = sig;
        }
        public NodoL(T info, NodoL<T> sig, NodoL<T> anterior)
        {
            this.Info = info;
            this.Sig = sig;
            this.Anterior = anterior;
        }
        public virtual T GetInfo()
        {
            return this.Info;
        }
        public NodoL<T> GetSig()
        {
            return this.Sig;
        }
        public NodoL<T> GetAnt()
        {
            return (this.Anterior);
        }
        public void SetInfo(T nuevo)
        {
            this.Info = nuevo;
        }
        public void SetSig(NodoL<T> nuevo)
        {
            this.Sig = nuevo;
        }
        public void SetAnt(NodoL<T> anterior)
        {
            this.Anterior = anterior;
        }

    }
}
