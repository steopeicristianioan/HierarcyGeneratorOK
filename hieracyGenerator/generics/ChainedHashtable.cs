using System;
using System.Collections.Generic;
using System.Text;

namespace hieracyGenerator
{
    class ChainedHashtable<K, V> where K : IComparable<K> where V : IComparable<V>
    {
        private Lista<Stored<K, V>>[] hashtable;
        private int size;
        public ChainedHashtable(int size)
        {
            this.size = size;
            this.hashtable = new Lista<Stored<K, V>>[size];
            for (int i = 0; i < size; i++)
                hashtable[i] = new Lista<Stored<K, V>>();
        }

        private int hashKey(K key)
        {
            return key.ToString().Length % hashtable.Length;
        }
        public void put(K key, V value)
        {
            int hashedKey = hashKey(key);
            Stored<K, V> data = new Stored<K, V>(key, value);
            hashtable[hashedKey].addLast(data);
        }
        public int findPosition(K key, bool show)
        {
            int hashedKey = hashKey(key);
            Node<Stored<K, V>> head = hashtable[hashedKey].First;
            int p = 0;
            while (head != null)
            {
                if (head.Data.Key.Equals(key))
                {
                    if (show)
                        Console.Write(hashedKey + ",");
                    return p;
                }
                p++;
                head = head.Next;
            }
            return -1;
        }
        public V get(K key)
        {
            int hashedKey = hashKey(key);
            int position = findPosition(key, false);
            if (position == -1)
                return default(V);
            else
            {
                int p = 0;
                Node<Stored<K, V>> head = hashtable[hashedKey].First;
                while (head != null && p != position)
                {
                    p++;
                    head = head.Next;
                }
                return head.Data.Value;
            }
        }
        public void remove(K key, V value)
        {
            int hashedKey = hashKey(key);
            int p = findPosition(key, false);
            if (p < hashtable[hashedKey].Length && p != -1)
            {
                Node<Stored<K, V>> head = hashtable[hashedKey].First;
                int i = 0, flag = 0;
                for (i = 0; i < p; i++)
                    head = head.Next;
                while (head != null && head.Data.Key.Equals(key))
                {
                    if (head.Data.Value.Equals(value))
                    {
                        flag = 1;
                        hashtable[hashedKey].remove(i);
                        break;
                    }
                    i++;
                    head = head.Next;
                }
                if (flag == 0) Console.WriteLine("the value doesn't exist");
            }
            else Console.WriteLine("null key");
        }
        public void print()
        {
            for (int i = 0; i < hashtable.Length; i++)
            {
                Console.Write(i + ": ");
                if (hashtable[i].isEmpty())
                {
                    hashtable[i].print();
                    Console.WriteLine("");
                }
                else
                {
                    Node<Stored<K, V>> head = hashtable[i].First;
                    while (head != null)
                    {
                        Console.WriteLine(head.Data.Value);
                        head = head.Next;
                    }
                }
            }
        }
        public Lista<K> allKeys()
        {
            Lista<K> keys = new Lista<K>();
            for (int i = 0; i < size; i++)
            {
                Node<Stored<K, V>> head = hashtable[i].First;
                while (head != null)
                {
                    keys.addLast(head.Data.Key);
                    head = head.Next;
                }
            }
            return keys;
        }
        public Lista<V> allValues()
        {
            Lista<V> values = new Lista<V>();
            for (int i = 0; i < size; i++)
            {
                Node<Stored<K, V>> head = hashtable[i].First;
                while (head != null)
                {
                    values.addLast(head.Data.Value);
                    head = head.Next;
                }
            }
            return values;
        }
        public Lista<V> getAllValues(K key)
        {
            int hashedKey = hashKey(key);
            Lista<V> values = new Lista<V>();
            Node<Stored<K, V>> head = hashtable[hashedKey].First;
            while (head != null)
            {
                if (head.Data.Key.Equals(key))
                    values.addLast(head.Data.Value);
                head = head.Next;
            }
            return values;
        }


    }
}
