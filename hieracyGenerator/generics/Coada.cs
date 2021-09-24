using System;
using System.Collections.Generic;
using System.Text;

namespace hieracyGenerator
{
    class Coada<T> where T : IComparable<T>
    {
        private Lista<T> lista;
        public Coada()
        {
            this.lista = new Lista<T>();
        }
        public Coada(T data)
        {
            this.lista = new Lista<T>();
            lista.create(data);
        }

        public void push(T data)
        {
            this.lista.addLast(data);
        }
        public T pop()
        {
            T res = default(T);
            if (!this.lista.isEmpty())
            {
                res = lista.First.Data;
                this.lista.removeFirst();
            }
            return res;
        }
        public T top()
        {
            return this.lista.First.Data;
        }
        public void print()
        {
            this.lista.print();
        }
        public bool isEmpty()
        {
            return (this.lista.Length == 0 || this.lista == null);
        }
    }
}
