using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Comparable : IComparer
    {
        private  static Comparable Instancia;
        private Comparable()
        {
        }
        public static  Comparable GetInstancia()
        {
            if (Instancia == null)
            {
                return Instancia = new Comparable();
            }
            else
            {
                return Instancia;
            }
        }
        public int Compare(object x, object y)
        {

            if (x.GetType() == typeof(int) && y.GetType() == typeof(int))
            {
                int c1 = (int)x;
                int c2 = (int)y;
                if (c1 > c2)
                {
                    return 1;
                }
                if (c1 < c2)
                {
                    return -1;
                }
            }
            if (x.GetType() == typeof(Double) && y.GetType() == typeof(Double))
            {
                Double c1 = (Double)x;
                Double c2 = (Double)y;
                if (c1 > c2)
                {
                    return 1;
                }
                if (c1 < c2)
                {
                    return -1;
                }
            }
            if (x.GetType() == typeof(String) && y.GetType() == typeof(String))
            {
                String c1 = (String)x;
                String c2 = (String)y;
                if (c1.Length > c2.Length)
                {
                    return 1;
                }
                if (c1.Length < c2.Length)
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}
