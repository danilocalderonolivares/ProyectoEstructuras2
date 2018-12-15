using System;
namespace Core
{
    public class TablaHash<Clave, T>
    {
        private int NumeroDeDatos;
        private int Tamanio;
        private Informacion<Clave, T>[] Vector;

        public TablaHash()
        {
            this.NumeroDeDatos = 0;
            this.Tamanio = ObtenerPrimo(40);
            this.Vector = new Informacion<Clave, T>[this.Tamanio];
            this.InicializarListas();
        }
        public TablaHash(int tamanio)
        {
            this.NumeroDeDatos = 0;
            this.Tamanio = ObtenerPrimo(tamanio);
            this.Vector = new Informacion<Clave, T>[this.Tamanio];
            this.InicializarListas();
        }
        private int Hashing1(int clave)
        {
            return (clave % this.Tamanio);
        }
        private int Hashing2(int clave)
        {
            return 1 + (clave % (this.Tamanio - 1));
        }
        private int DobleHashing(Clave Clave, int index)
        {
            int hash = Clave.GetHashCode();
            hash = Math.Abs(hash);
            return (Hashing1(hash) + ((index * Hashing2(hash)) % Tamanio));
        }
        public bool Insertar(Clave clave, T objeto)
        {

            bool result = false, estaLlenaLaTablas = true;
            int posicion, i;
            for (i = 0; i < Tamanio; i++)
            {
                posicion = DobleHashing(clave, i);

                if (posicion > Tamanio - 1)
                {
                    posicion -= Tamanio;
                }
                if (Vector[posicion] == null || Vector[posicion].GetEstado() == 0)
                {
                    Vector[posicion].SetClave(clave);
                    Vector[posicion].SetObjeto(objeto);
                    Vector[posicion].SetEstado(2);
                    this.NumeroDeDatos = NumeroDeDatos + 1;
                    estaLlenaLaTablas = false;
                    result = true;
                    break;
                }
                else
                {
                    Console.WriteLine("\nColision en la posición : " + posicion);
                }
            }
            if (estaLlenaLaTablas)
            {
                throw new IndexOutOfRangeException("La Tabla esta llena");
            }
            return result;
        }
        public bool ActualizarDatoPorClave(Clave pClave, T pDato)
        {
            int index = this.GetIndex(pClave);
            if (index >=0)
            {
                Vector[index].SetObjeto(pDato);
                return true;
            }
            return false;
        }
        public bool ActualizarDatoPorIndex(int pIndex, T pDato)
        {
            if (pIndex >=0)
            {
                Vector[pIndex].SetObjeto(pDato);
                return true;
            }
            return false;
        }
        public int GetIndex(Clave clave)
        {
            int posicion, i;
            for (i = 0; i < Tamanio; i++)
            {
                posicion = DobleHashing(clave, i);

                if (posicion > Tamanio - 1)
                {
                    posicion -= Tamanio;
                }
                if (Vector[posicion].GetClave().Equals(clave) && Vector[posicion].GetEstado() == 2)
                {

                    return posicion;
                }
            }
            return -1;
        }
        public Clave GetClave(int pIndex)
        {
            if (pIndex > -1 && pIndex < this.Tamanio)
            {
                return Vector[pIndex].GetClave();
            }
            return default(Clave);
        }
        public T GetForIndex(int pIndex)
        {
            if (pIndex > - 1 && pIndex < this.Tamanio)
            {
                return Vector[pIndex].GetInformacion();
            }
            return default(T);
        }
        public Informacion<Clave, T>[] GetInformacionEntrada()
        {
            return Vector;
        }
        public T BuscaHashPorClave(Clave pClave)
        {
            bool seEncontroAlguno = false;
            int posicion, i;
            for (i = 0; i < Tamanio; i++)
            {
                posicion = DobleHashing(pClave, i);

                if (posicion > Tamanio - 1)
                {
                    posicion -= Tamanio;
                }
                if (Vector[posicion].GetClave() != null)
                {
                    if (pClave.GetType() == typeof(string) && Vector[posicion].GetClave().GetType() == typeof(string) && Vector[posicion].GetEstado() == 2)
                    {
                        if (pClave.ToString().Equals(Vector[posicion].GetClave().ToString()))
                        {
                            return Vector[posicion].GetInformacion();
                        }
                    }
                    else if (Vector[posicion].GetClave().Equals(pClave) && Vector[posicion].GetEstado() == 2)
                    {
                        return Vector[posicion].GetInformacion();
                    }
                }
            }
            if (!seEncontroAlguno)
            {
                // "No se encontraron resultados";
                this.ToString();
            }
            return default(T);
        }
        private void InicializarListas()
        {
            for (int i = 0; i < this.Vector.Length; i++)
            {
                if (this.Vector[i] == null)
                {
                    this.Vector[i] = new Informacion<Clave, T>();
                }
            }
        }
        public void ReHashing()
        {
            int tamanio = ObtenerPrimo(this.Tamanio * 2);
            this.Tamanio = tamanio;
            Informacion<Clave, T>[] newVector = new Informacion<Clave, T>[tamanio];
            Vector.CopyTo(newVector, 0);
            Vector = newVector;
            InicializarListas();
        }
        public static int ObtenerPrimo(int numero)
        {
            if (!EsPrimo(numero))
            {
                while (!EsPrimo(numero))
                {
                    numero += 1;
                }
            }
            return numero;
        }
        private static bool EsPrimo(int numero)
        {
            int a = 0;
            for (int i = 1; i < (numero + 1); i++)
            {
                if (numero % i == 0)
                {
                    a++;
                }
            }
            if (a != 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int GetTamanio()
        {
            return this.Tamanio;
        }
        public override String ToString()
        {
            String msg = "";
            for (int i = 0; i < this.Vector.Length; i++)
            {
                if (this.Vector[i] != null)
                {
                    msg += "Funcion Hash  " + i + " de la tabla Hash  ==> " + this.Vector[i].ToString() + "\n";

                }
            }
            return msg;
        }
        public bool EsVacia()
        {
            return (this.NumeroDeDatos == 0);
        }
    }
}
