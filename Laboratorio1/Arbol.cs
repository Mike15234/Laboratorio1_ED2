using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1
{
    public class Arbol
    {
        public Nodo Raiz { get; set; }
        public Arbol()
        {
            Raiz = null;
        }
        public void Insertar(string value)
        {
            if (Raiz == null)
            {
                Raiz = new Nodo(value);
                return;
            }
            Nodo actual = Raiz;
            Nodo padre = null;
            while (actual != null)
            {
                if (actual.Valores.Count == 3)
                {
                    if (padre == null)
                    {
                        string k = actual.Pop(1);
                        Nodo nuevaRaiz = new Nodo(k);
                        Nodo[] newNodos = actual.Split();
                        nuevaRaiz.InsertarHijo(newNodos[0]);
                        nuevaRaiz.InsertarHijo(newNodos[1]);
                        Raiz = nuevaRaiz;
                        actual = nuevaRaiz;
                    }
                    else
                    {
                        string k = actual.Pop(1);
                        if (k != null)
                        {
                            padre.Push(k);
                        }
                        Nodo[] nNodos = actual.Split();
                        int pos1 = padre.EncontrarPosicionHijo(nNodos[1].Valores[0]);
                        padre.InsertarHijo(nNodos[1]);

                        int posActual = padre.EncontrarPosicionHijo(value);
                        actual = padre.GetHijo(posActual);

                    }
                }
                padre = actual;
                actual = actual.Traverse(value);
                if (actual == null)
                {
                    padre.Push(value);
                }
            }
        }
        public Nodo Find(string k)
        {
            Nodo curr = Raiz;

            while (curr != null)
            {
                if (curr.HasKey(k) >= 0)
                {
                    return curr;
                }
                else
                {
                    int p = curr.EncontrarPosicionHijo(k);
                    curr = curr.GetHijo(p);
                }
            }

            return null;
        }
        public void Eliminar(string k)
        {
            Nodo curr = Raiz;
            Nodo Padre = null;
            while (curr != null)
            {
               
                if (curr.Valores.Count == 1)
                {
                    if (curr != Raiz)
                    { 
                        string cK = curr.Valores[0];
                        int HijoPos = Padre.EncontrarPosicionHijo(cK);

                        bool? takeRight = null;
                        Nodo sibling = null;

                        if (HijoPos > -1)
                        {
                            if (HijoPos < 3)
                            {
                                sibling = Padre.GetHijo(HijoPos + 1);
                                if (sibling.Valores.Count > 1)
                                {
                                    takeRight = true;
                                }
                            }

                            if (takeRight == null)
                            {
                                if (HijoPos > 0)
                                {
                                    sibling = Padre.GetHijo(HijoPos - 1);
                                    if (sibling.Valores.Count > 1)
                                    {
                                        takeRight = false;
                                    }
                                }
                            }

                            if (takeRight != null)
                            {
                                string pK = "";
                                string sK = "";

                                if (takeRight.Value)
                                {
                                    pK = Padre.Pop(HijoPos);
                                    sK = sibling.Pop(0);

                                    if (sibling.Hijos.Count > 0)
                                    {
                                        Nodo Hijo = sibling.EliminarHijo(0);
                                        curr.InsertarHijo(Hijo);
                                    }
                                }
                                else
                                {
                                    pK = Padre.Pop(HijoPos);
                                    sK = sibling.Pop(sibling.Valores.Count - 1);

                                    if (sibling.Hijos.Count > 0)
                                    {
                                        Nodo Hijo = sibling.EliminarHijo(sibling.Hijos.Count - 1);
                                        curr.InsertarHijo(Hijo);
                                    }
                                }

                                Padre.Push(sK);
                                curr.Push(pK);
                            }
                            else
                            {
                                string pK = null;
                                if (Padre.Hijos.Count >= 2)
                                {
                                    if (HijoPos == 0)
                                    {
                                        pK = Padre.Pop(0);
                                    }
                                    else if (HijoPos == Padre.Hijos.Count)
                                    {
                                        pK = Padre.Pop(Padre.Valores.Count - 1);
                                    }
                                    else
                                    {
                                        pK = Padre.Pop(1);
                                    }

                                    if (pK != null)
                                    {
                                        curr.Push(pK);
                                        Nodo sib = null;
                                        if (HijoPos != Padre.Hijos.Count)
                                        {
                                            sib = Padre.EliminarHijo(HijoPos + 1);
                                        }
                                        else
                                        {
                                            sib = Padre.EliminarHijo(Padre.Hijos.Count - 1);
                                        }

                                        curr.Fuse(sib);
                                    }
                                }
                                else
                                {
                                    curr.Fuse(Padre, sibling);
                                    Raiz = curr;
                                    Padre = null;
                                }
                            }
                        }
                    }
                }

                int rmPos = -1;
                if ((rmPos = curr.HasKey(k)) >= 0)
                {
                    
                    if (curr.Hijos.Count == 0)
                    {
                        if (curr.Valores.Count == 0)
                        {
                            Padre.Hijos.Remove(curr);
                        }
                        else
                        {
                            curr.Pop(rmPos);
                        }
                    }
                    else
                    {
                        Nodo successor = Min(curr.Hijos[rmPos]);
                        string sK = successor.Valores[0];
                        if (successor.Valores.Count > 1)
                        {
                            successor.Pop(0);
                        }
                        else
                        {
                            if (successor.Hijos.Count == 0)
                            {
                                Nodo p = successor.Padre;
                                p.EliminarHijo(successor);
                            }
                            else
                            {
                                
                            }
                        }
                    }

                    curr = null;
                }
                else
                {
                    
                    int p = curr.EncontrarPosicionHijo(k);
                    Padre = curr;
                    curr = curr.GetHijo(p);
                }
            }

        }
        public Nodo Min(Nodo n = null)
        {
            if (n == null)
            {
                n = Raiz;
            }

            Nodo curr = n;
            if (curr != null)
            {
                while (curr.Hijos.Count > 0)
                {
                    curr = curr.Hijos[0];
                }
            }

            return curr;
        }
        public string[] Inorder(Nodo n = null)
        {
            if (n == null)
            {
                n = Raiz;
            }
            int a = 0;
            List<string> items = new List<string>();
            Tuple<Nodo, int> curr = new Tuple<Nodo, int>(n, a);
            Stack<Tuple<Nodo, int>> stack = new Stack<Tuple<Nodo, int>>();
            while (stack.Count > 0 || curr.Item1 != null)
            {
                if (curr.Item1 != null)
                {
                    stack.Push(curr);
                    Nodo leftChild = curr.Item1.GetHijo(curr.Item2);//move to leftmost unvisited child
                    curr = new Tuple<Nodo, int>(leftChild, a);
                }
                else//Case 2
                {
                    curr = stack.Pop();
                    Nodo currNode = curr.Item1;

                    //because for every node, it can possibly have more Hijos than key
                    //if the current index corresponds to a key, we want to add the key into the list.
                    //else we just want to traverse it's Hijos.
                    if (curr.Item2 < currNode.Valores.Count)
                    {
                        items.Add(currNode.Valores[0]);
                        curr = new Tuple<Nodo, int>(currNode, curr.Item2 + 1);
                    }
                    else
                    {
                        Nodo rightChild = currNode.GetHijo(curr.Item2 + 1);//get the rightmost child, may be null

                        //if right most child is null, we will visit 'Case 2' again in the next loop,
                        //and the Padre will be popped off the stack
                        curr = new Tuple<Nodo, int>(rightChild, curr.Item2 + 1);
                    }
                }
            }
            return items.ToArray();
        }

    }
}

    

