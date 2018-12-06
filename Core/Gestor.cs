using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Gestor
    {
        private static Gestor Instancia;
        private Grafo<String> Grafo;
        private Gestor()
        {
            Grafo = new Grafo<string>(false);
        }
        public static Gestor GetInstancia()
        {
            if (Instancia == null)
            {
                Instancia = new Gestor();
            }
            return Instancia;
        }
       
    }
}
