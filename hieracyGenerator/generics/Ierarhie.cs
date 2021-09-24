using System;
using System.Collections.Generic;
using System.Text;

namespace hieracyGenerator
{
    class Ierarhie<T> where T : IComparable<T>
    {

        private TreeNode<T> root;
        public TreeNode<T> Root { get => this.root; }
        private int rows = 0;
        public int Rows { get => this.rows; set => this.rows = value; }

        public Ierarhie(T data)
        {
            root = new TreeNode<T>(data);  
        }
        public Ierarhie()
        {
            root = new TreeNode<T>();
        }

        public TreeNode<T> find(TreeNode<T> root, T data)
        {
            if (root == null)
            {
                return null;
            }
            else if (root.Data.Equals(data))
            {
                return root;
            }
            var left = find(root.Left, data);
            if (left!=null)
                return left;
            return find(root.Right, data);
        }
        public bool add( T manager, T subordonat)
        {
            TreeNode<T> manNode = find(root, manager);
            if (manNode == null)
            {
                Console.WriteLine("Nu exista managerul");
                return false;
            }
            if (manNode.Left != null && manNode.Right != null)
            {

                Console.WriteLine("Deja avem oameni pe pozitiile respective");
            }
            if (manNode.Left == null)
            {
                manNode.Left = new TreeNode<T>(subordonat);
            }
            else
            {
                manNode.Right = new TreeNode<T>(subordonat);
            }
            Console.WriteLine("Am adaugat "+  subordonat);
            return true;
        }
        public void print()
        {
            Coada<TreeNode<T>> coada = new Coada<TreeNode<T>>();
            coada.push(root);
            while (!coada.isEmpty())
            {
                TreeNode<T> node = coada.pop();
                Console.WriteLine(node);
                if (node.Left != null)
                {
                    coada.push(node.Left);
                }
                if (node.Right != null)
                {
                    coada.push(node.Right);
                }
            }






            

        }
        public int height(TreeNode<T> root)
        {
            if (root == null)
                return -1;
            return Math.Max(height(root.Left), height(root.Right)) + 1;
        }
    }

}
