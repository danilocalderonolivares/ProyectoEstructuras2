using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Grafo<String> g = new Grafo<string>(true);
            Vertice<String> v = g.GetVeticePorNombre("F");
            g.InsertarArco("A","B",3);
            g.InsertarArco("B", "F", 3);
            g.InsertarArco("C", "F", 3);
            g.InsertarArco("A", "F", 3);
            g.InsertarArco("F", "G", 3);
            g.InsertarArco("F", "H", 3);
            g.MostrarPredecesores("F");
            g.MostrarSucesores("A");
            Console.WriteLine(v.Nombre);
            Console.ReadLine();
        }
    }
}
