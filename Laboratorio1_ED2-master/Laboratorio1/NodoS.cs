using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1
{
    public class Nodo
    {
        public List<Nodo> Hijos { get; private set; }
        public List<String> Valores { get; private set; }
        public Nodo Padre { get; set; }


        public Nodo(string valor)
        {
            Valores = new List<string>();
            Valores.Add(valor);
            Hijos = new List<Nodo>();
        }

        public int Encontrado(string valor)
        {
            for (int i = 0; i < Valores.Count; i++)
            {
                if (Valores[i].CompareTo(valor) == 0)
                {
                    return 1;
                }
            }
            return -1;
        }


        public int CompareTo(object obj, int x)
        {
            return this.Hijos[x].Valores[0].CompareTo(((Nodo)obj).Valores[0]);
        }


        public void InsertarHijo(Nodo hijo)
        {
            for (int x = 0; x < Hijos.Count; x++)
            {
                if (this.CompareTo(hijo, x) > 0)
                {
                    Hijos.Insert(x, hijo);
                    return;
                }
            }

            Hijos.Add(hijo);
            hijo.Padre = this;
        }
    }
}
