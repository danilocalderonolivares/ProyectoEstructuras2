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
            GrafoMatriz<String> g = new GrafoMatriz<string>();
            Vertice<String> v = g.GetVeticePorNombre("F");
            Console.WriteLine(v.ToString());
            Console.ReadLine();
        }
    }
}
