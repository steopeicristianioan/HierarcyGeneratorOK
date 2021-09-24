using System;
using System.Collections.Generic;
using System.Text;

namespace hieracyGenerator
{
    class TreeNode<T> : IComparable<TreeNode<T>> where T : IComparable<T>
    {
        private T data;
        public T Data { get => this.data; set => this.data = value; }
        private TreeNode<T> left;
        public TreeNode<T> Left { get => this.left; set => this.left = value; }
        private TreeNode<T> right;
        public TreeNode<T> Right { get => this.right; set => this.right = value; }

        public TreeNode(T data)
        {
            this.data = data;
            this.left = this.right = null;
        }
        public TreeNode()
        {
            this.left = this.right = null;
        }
        public int CompareTo(TreeNode<T> other)
        {
            return 1;
        }
        public override string ToString()
        {
            return data.ToString();
        }
    }
}
