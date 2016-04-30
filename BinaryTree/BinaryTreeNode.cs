using System;
using System.Text;

namespace BinaryTree
{
    public class BinaryTreeNode<T> : Node<T> where T : IComparable
    {
        public BinaryTreeNode<T> Parent { get; set; }

        public BinaryTreeNode<T> Left {
            get
            {
                if (Children == null)
                    return null;
                else
                    return (BinaryTreeNode<T>)Children[0];
            }
            set
            {
                if (Children == null)
                    Children = new NodeList<T>(2);

                Children[0] = value;
            }
        }

        public BinaryTreeNode<T> Right {
            get
            {
                if (Children == null)
                    return null;
                else
                    return (BinaryTreeNode<T>)Children[1];
            }
            set
            {
                if (Children == null)
                    Children = new NodeList<T>(2);

                Children[1] = value;
            }
        }
        
        public BinaryTreeNode() : base() { }

        public BinaryTreeNode(T data) : base(data, null) { }

        public BinaryTreeNode(T data, BinaryTreeNode<T> left = null, BinaryTreeNode<T> right = null)
        {
            Value = data;

            //create a list of children of maximum of two children
            Children = new NodeList<T>(2);

            if (left != null)
                Children[0] = left;
            if(right != null)
                Children[1] = right;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder("( ").Append(Value).Append(" )");
            return s.ToString();
        }
    }
}
