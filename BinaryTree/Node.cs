using System;
using System.Collections.ObjectModel;

namespace BinaryTree
{
    public class Node<T> where T : IComparable
    {
        private T data;
        private NodeList<T> childern = null;

        public Node() { }
        public Node(T data) : this(data, null) { }
        public Node(T data, NodeList<T> childern)
        {
            this.data = data;
            this.childern = childern;
        }

        /// <summary>
        /// get the object value of the node
        /// </summary>
        public T Value
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// get the children of this node
        /// </summary>
        protected NodeList<T> Children
        {
            get { return childern; }
            set { childern = value;}
        }
    }

    public class NodeList<E> : Collection<Node<E>> where E : IComparable
    {
        public NodeList() : base() { }

        public NodeList(int initialSize)
        {
            // Add the specified number of items
            for (int i = 0; i < initialSize; i++)
                Items.Add(default(Node<E>));
        }

        /// <summary>
        /// find a node in the list of childern with this value
        /// </summary>
        /// <param name="value">the value to be found</param>
        /// <returns>the node with the specified value or null</returns>
        public Node<E> FindByValue(E value)
        {
            foreach (Node<E> node in Items)
                if (node.Value.Equals(value))
                    return node;

            return null;
        }
    }
}
