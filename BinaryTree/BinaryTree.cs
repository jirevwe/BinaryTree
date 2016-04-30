using System;
using System.Collections.Generic;
using System.Drawing;

namespace BinaryTree
{
    public class BinaryTree<E> where E : IComparable
    {
        public BinaryTreeNode<E> Root { get; set; }

        public BinaryTree()
        {
            Root = null;
        }

        public virtual void Clear()
        {
            Root = null;
        }

        public bool AddNodes(E[] nodesToAdd)
        {
            bool complete = false;

            foreach (var node in nodesToAdd)
                complete = AddNode(node);

            return complete;
        }

        public bool AddNode(E dataToAdd)
        {
            BinaryTreeNode<E> tempNode = new BinaryTreeNode<E>(dataToAdd);
            BinaryTreeNode<E> Current = Root, parent = null;

            while (Current != null) {
                if (Current.Value.CompareTo(dataToAdd) == 0)
                {
                    return false;
                }
                else if (Current.Value.CompareTo(dataToAdd) > 0)
                {
                    parent = Current;
                    Current = Current.Left;
                }
                else if (Current.Value.CompareTo(dataToAdd) < 0)
                {
                    parent = Current;
                    Current = Current.Right;
                }
            }

            if (parent == null)
                Root = tempNode;
            else {
                if (parent.Value.CompareTo(dataToAdd) > 0)
                {
                    parent.Left = tempNode;
                    parent.Left.Parent = parent;
                }
                else if (parent.Value.CompareTo(dataToAdd) < 0)
                {
                    parent.Right = tempNode;
                    parent.Right.Parent = parent;
                }
                return true;
            }

            return false;
        }

        public bool Contains(E data)
        {
            return Find(Root, data) != null;
        }

        public BinaryTreeNode<E> Find(BinaryTreeNode<E> thisNode, E dataToFind)
        {
            if (thisNode == null)
                return null;
            if (thisNode.Value.Equals(dataToFind))
                return thisNode;
            if (thisNode.Value.CompareTo(dataToFind) > 0)
                return Find(thisNode.Left, dataToFind);
            else if (thisNode.Value.CompareTo(dataToFind) < 0)
                return Find(thisNode.Right, dataToFind);
            return null;
        }

        public List<E> TraversalList = new List<E>();

        public bool DeleteNode(E data)
        {
            BinaryTreeNode<E> node = Find(Root, data);
            if (node == null)
                return false;
            else
            {
                if(node.Right == null)
                {
                    if (node.Parent == null)
                    {
                        Root = node.Left;
                        Root.Parent = null;
                    }
                    else
                    {
                        if (node.Parent.Value.CompareTo(node.Value) > 0)
                        {
                            node.Parent.Left = node.Left;
                            node.Parent.Left = node.Parent;
                        }
                        else if (node.Parent.Value.CompareTo(node.Value) < 0)
                        {
                            node.Parent.Right = node.Right;
                            node.Parent.Right = node.Parent;
                        }
                    }
                }
                else if (node.Right.Left == null)
                {
                    node.Right.Left = node.Left;
                    if (node.Parent == null)
                    {
                        Root = node.Right;
                        Root.Parent = null;
                    }
                    else
                    {
                        if (node.Parent.Value.CompareTo(node.Value) > 0)
                        {
                            node.Parent.Left = node.Left;
                            node.Parent.Left = node.Parent;
                        }
                        else if (node.Parent.Value.CompareTo(node.Value) < 0)
                        {
                            node.Parent.Right = node.Right;
                            node.Parent.Right = node.Parent;
                        }
                    }
                }
                else
                {
                    BinaryTreeNode<E> leftmost = node.Right.Left, lmParent = node.Right;
                    while (leftmost.Left != null)
                    {
                        lmParent = leftmost;
                        leftmost = leftmost.Left;
                    }

                    lmParent.Left = leftmost.Right;

                    leftmost.Left = node.Left;
                    if (leftmost.Left != null)
                        leftmost.Left.Parent = leftmost;

                    leftmost.Right = node.Right;
                    if (leftmost.Right != null)
                        leftmost.Right.Parent = leftmost;

                    if (node.Parent == null)
                    {
                        Root = leftmost;
                        Root.Parent = null;
                    }
                    else
                    {
                        if (node.Parent.Value.CompareTo(node.Value) > 0)
                        {
                            node.Parent.Left = leftmost;
                            leftmost.Parent = node.Parent;
                        }
                        else if (node.Parent.Value.CompareTo(node.Value) < 0)
                        {
                            node.Parent.Right = leftmost;
                            leftmost.Parent = node.Parent;
                        }
                    }
                }
            }
            return true;
        }

        public void DrawTree(Graphics g, Pen pen, SolidBrush brush, BinaryTreeNode<E> node, int xPos, int yPos, int xPad, int depth)
        {
            if (node == null)
                return;
            if (node.Left != null && node.Left.Value != null)
            {
                DrawTree(g, pen, brush, node.Left, (xPos - xPad), yPos + 100, xPad - 50, depth + 1);
                if (node.Value != null)
                    g.DrawLine(pen, new Point(xPos + 5, yPos + 5), new Point((xPos - xPad) + 5, (yPos + 100) + 5));
            }
            if (node.Value != null)
            {
                g.DrawString(node.ToString(), new Font(FontFamily.GenericSansSerif, 7), brush, xPos, yPos - 20);
                g.FillEllipse(brush, xPos, yPos, 10, 10);
            }
            if (node.Right != null && node.Right.Value != null)
            {
                DrawTree(g, pen, brush, node.Right, (xPos + xPad), yPos + 100, xPad - 30, depth + 1);
                g.DrawLine(pen, new Point(xPos + 5, yPos + 5), new Point((xPos + xPad) + 5, (yPos + 100) + 5));
            }
        }

        public void DrawTree(Graphics g, Pen pen, SolidBrush brush, int xPos, int yPos)
        {
            DrawTree(g, pen, brush, Root, xPos, yPos, 200, 1);
        }

        public void AVL()
        {
            InOrderTraversal(Root);
            Root = null;
            CreateBalancedTree(TraversalList);
        }

        public void CreateBalancedTree(List<E> items)
        {
            if (items.Count == 0)
                return;

            if (items.Count == 1)
            {
                AddNode(items[0]);
                return;
            }

            int middle = items.Count % 2 == 0 ? items.Count / 2 - 1 : items.Count / 2;

            AddNode(GetMedian(items));

            //LEFT
            List<E> lowerList = new List<E>();
            for(int i = 0;i < middle + 1;i++)
                lowerList.Add(items[i]);

            CreateBalancedTree(lowerList);

            //RIGHT
            List<E> upperList = new List<E>();
            for (int i = middle + 1; i < items.Count; i++)
                upperList.Add(items[i]);

            CreateBalancedTree(upperList);
        }

        public E GetMedian(List<E> items)
        {
            int middle = items.Count % 2 == 0 ? items.Count / 2 : items.Count / 2;
            return items[middle];
        }

        public void PreOrderTraversal(BinaryTreeNode<E> node)
        { 
            if(node == Root)
                TraversalList.Clear();
            if (node.Value != null)
            {
                TraversalList.Add(node.Value);
                //Console.Write("\n" + String.Format("Value : {0:00}, Parent : {1}, Left Child : {2}, Right Child : {3}",
                //                    node.Value,
                //                    node.Parent != null ? node.Parent.Value.ToString() : "No Parent",
                //                    node.Left != null ? node.Left.Value.ToString() : "No Left Child",
                //                    node.Right != null ? node.Right.Value.ToString() : "No Right Child")
                //                    );
            }
            if (node.Left != null)
                PreOrderTraversal(node.Left);
            if (node.Right != null)
                PreOrderTraversal(node.Right);
        }

        public void InOrderTraversal(BinaryTreeNode<E> node)
        {
            if(node == Root)
                TraversalList.Clear();
            if (node.Left != null)
                InOrderTraversal(node.Left);
            if (node.Value != null)
            {
                TraversalList.Add(node.Value);
                //Console.Write("\n" + String.Format("Value : {0:00}, Parent : {1}, Left Child : {2}, Right Child : {3}",
                //                    node.Value,
                //                    node.Parent != null ? node.Parent.Value.ToString() : "No Parent",
                //                    node.Left != null ? node.Left.Value.ToString() : "No Left Child",
                //                    node.Right != null ? node.Right.Value.ToString() : "No Right Child")
                //                    ); 
            } 
            if (node.Right != null)
                    InOrderTraversal(node.Right);
        }

        public void PostOrderTraversal(BinaryTreeNode<E> node)
        {
            if(node == Root)
                TraversalList.Clear();
            if (node.Left != null)
                PostOrderTraversal(node.Left);
            if (node.Right != null)
                PostOrderTraversal(node.Right);
            if (node.Value != null)
            {
                TraversalList.Add(node.Value);
                //Console.Write("\n" + String.Format("Value : {0:00}, Parent : {1}, Left Child : {2}, Right Child : {3}",
                //                    node.Value,
                //                    node.Parent != null ? node.Parent.Value.ToString() : "No Parent",
                //                    node.Left != null ? node.Left.Value.ToString() : "No Left Child",
                //                    node.Right != null ? node.Right.Value.ToString() : "No Right Child")
                //                    );
            }
        }
    }
}
