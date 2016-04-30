using System;
using System.Text;

namespace BinaryTree
{
    public class KDTreeNode<T> : Node<T> where T : IComparable
    {
        private int Dimension { get; set; }

        public new T[] Value { get; set; }

        public T this[int index]
        {
            get { return Value[index]; }
            set { Value[index] = value; }
        }

        public KDTreeNode<T> Parent { get; set; }

        public KDTreeNode<T> Left
        {
            get
            {
                if (Children == null)
                    return null;
                else
                    return (KDTreeNode<T>)Children[0];
            }
            set
            {
                if (Children == null)
                    Children = new NodeList<T>(2);

                Children[0] = value;
            }
        }

        public KDTreeNode<T> Right
        {
            get
            {
                if (Children == null)
                    return null;
                else
                    return (KDTreeNode<T>)Children[1];
            }
            set
            {
                if (Children == null)
                    Children = new NodeList<T>(2);

                Children[1] = value;
            }
        }

        public KDTreeNode(int dimension = 1) : base()
        {
            Dimension = dimension;
            Value = new T[dimension];
        }

        public KDTreeNode(T[] data, KDTreeNode<T> left = null, KDTreeNode<T> right = null)
        {
            Value = data;
            Dimension = Value.Length;

            Children = new NodeList<T>(2);

            if (left != null)
                Children[0] = left;
            if (right != null)
                Children[1] = right;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder("( ");
            for (int i = 0; i < Dimension;i++)
                s.Append(this[i]).Append(", ");
            s.Append(" )");
            return s.ToString();
        }
    }
}
