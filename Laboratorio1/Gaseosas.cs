using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1
{
    public class Gaseosas
    {
        public string Nombre { get; set; }
        public string Sabor { get; set; }
        public string Volumen { get; set; }
        

        public static Comparison<Gaseosas> ComparebyName = delegate (Gaseosas m1, Gaseosas m2)
        {
            return m1.Nombre.CompareTo(m2.Nombre);
        };
    }
}
