using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryTree
{
    public class KDTree<E> where E : IComparable
    {
        public KDTreeNode<E> Root { get; set; }

        private int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;

        public int Dimension { get; set; }

        public KDTree(int dimension)
        {
            Root = null;
            Dimension = dimension;
        }

        public void Clear()
        {
            Root = null;
        }

        public bool AddNode(E[] dataToAdd)
        {
            KDTreeNode<E> tempNode = new KDTreeNode<E>(dataToAdd);
            KDTreeNode<E> Current = Root, parent = null;
            int depth = -1;

            if (dataToAdd.Length != Dimension)
                return false;

            while (Current != null)
            {
                if (depth == Dimension - 1)
                    depth = 0;
                else
                    ++depth;

                if (Current.Value[depth].CompareTo(dataToAdd[depth]) == 0)
                {
                    return false;
                }
                else if (Current.Value[depth].CompareTo(dataToAdd[depth]) > 0)
                {
                    parent = Current;
                    Current = Current.Left;
                }
                else if (Current.Value[depth].CompareTo(dataToAdd[depth]) < 0)
                {
                    parent = Current;
                    Current = Current.Right;
                }
            }

            if (parent == null)
            {
                Root = tempNode;
                Root.Parent = null;
            }
            else {
                if (parent.Value[depth].CompareTo(dataToAdd[depth]) > 0)
                {
                    parent.Left = tempNode;
                    parent.Left.Parent = parent;
                }
                else if (parent.Value[depth].CompareTo(dataToAdd[depth]) < 0)
                {
                    parent.Right = tempNode;
                    parent.Right.Parent = parent;
                }
                depth = 0;
                return true;
            }

            return false;
        }

        public KDTreeNode<E> FindMinimum(KDTreeNode<E> t, int axisToSearch, int cuttingDimension)
        {
            int cd = (cuttingDimension + 1) % Dimension;

            if (axisToSearch == cuttingDimension)
            {
                if (t.Left == null)
                    return t;
                else
                    return FindMinimum(t.Left, axisToSearch, cd);
            }
            else {
                if (t.Left != null && t.Right != null)
                {
                    return FindMinimum(t.Right, axisToSearch, cd).Value[axisToSearch].CompareTo(FindMinimum(t.Left, axisToSearch, cd).Value[axisToSearch]) > 0 ? FindMinimum(t.Left, axisToSearch, cd) : FindMinimum(t.Right, axisToSearch, cd);
                }
                else if (t.Left != null && t.Right == null)
                    return FindMinimum(t, axisToSearch, cd).Value[axisToSearch].CompareTo(FindMinimum(t.Left, axisToSearch, cd).Value[axisToSearch]) > 0 ? FindMinimum(t, axisToSearch, cd) : FindMinimum(t.Left, axisToSearch, cd);
                else if (t.Left == null && t.Right != null)
                    return FindMinimum(t, axisToSearch, cd).Value[axisToSearch].CompareTo(FindMinimum(t.Right, axisToSearch, cd).Value[axisToSearch]) < 0 ? FindMinimum(t, axisToSearch, cd) : FindMinimum(t.Right, axisToSearch, cd);
                else
                    return t;
            }
        }

        public void DrawTree(Graphics g, Pen pen, SolidBrush brush, KDTreeNode<E> node, int xPos, int yPos, int xPad, int depth)
        {
            double maxNumPerDepth = Math.Pow(2, depth);

            if (node == null)
                return;
            if (node.Left != null && node.Left.Value != null)
            {
                DrawTree(g, pen, brush, node.Left, (xPos - xPad), yPos + 100, xPad - 15, depth + 1);
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
                DrawTree(g, pen, brush, node.Right, (xPos + xPad), yPos + 100, xPad - 15, depth + 1);
                g.DrawLine(pen, new Point(xPos + 5, yPos + 5), new Point((xPos + xPad) + 5, (yPos + 100) + 5));
            }
        }

        public void DrawTree(Graphics g, Pen pen, SolidBrush brush, int xPos, int yPos)
        {
            DrawTree(g, pen, brush, Root, xPos, yPos, 100, 1);
        }

        public KDTreeNode<E> DeleteNode(KDTreeNode<E> t, E[] point, int cuttingDimension)
        {
            if (t == null)
                return null;

            int cd = (cuttingDimension + 1) % Dimension;

            if (ArrayEquals(point, t.Value))
            {
                List<E[]> nodes = GetChildren(t);
                nodes.RemoveAt(0);
                if (t.Parent != null)
                {
                    if (t == t.Parent.Left)
                        t.Parent.Left = null;
                    else if (t == t.Parent.Right)
                        t.Parent.Right = null;
                }
                foreach(var x in nodes)
                {
                    AddNode(x);
                }
                t = null;
            }
            else if (point[cuttingDimension].CompareTo(t.Value[cuttingDimension]) < 0)
            {
                t.Left = DeleteNode(t.Left, t.Value, cd);
            }
            else if (point[cuttingDimension].CompareTo(t.Value[cuttingDimension]) > 0)
            {
                t.Right = DeleteNode(t.Right, t.Value, cd);
            }
            return t;
        }

        private void Nullify(KDTreeNode<E> node)
        {
            node = null;
        }

        public bool Contains(E[] data)
        {
            return Find(data) != null;
        }

        public KDTreeNode<E> Find(E[] dataToFind)
        {
            int depth = -1;

            KDTreeNode<E> Current = Root;

            if (dataToFind.Length != Dimension)
                return null;

            while (Current != null)
            {
                if (depth == Dimension - 1)
                    depth = 0;
                else
                    ++depth;

                if (ArrayEquals(Current.Value, dataToFind))
                    return Current;
                else if (Current.Value[depth].CompareTo(dataToFind[depth]) > 0)
                    Current = Current.Left;
                else if (Current.Value[depth].CompareTo(dataToFind[depth]) < 0)
                    Current = Current.Right;
            }

            return null;
        }

        public int GetDepth(E[] dataToFind)
        {
            int depth = -1;

            KDTreeNode<E> Current = Root;

            if (dataToFind.Length != Dimension)
                return -1;

            while (Current != null)
            {
                if (depth == Dimension - 1)
                    depth = 0;
                else
                    ++depth;

                if (ArrayEquals(Current.Value, dataToFind))
                    return depth;
                else if (Current.Value[depth].CompareTo(dataToFind[depth]) > 0)
                    Current = Current.Left;
                else if (Current.Value[depth].CompareTo(dataToFind[depth]) < 0)
                    Current = Current.Right;
            }

            return -1;
        }

        public bool ArrayEquals(E[] array1, E[] array2)
        {
            if (array1.Length != array2.Length)
                return false;
            for (int i = 0; i < array1.Length; i++)
                if (array1[i].CompareTo(array2[i]) != 0)
                    return false;
            return true;
        }

        public List<E[]> GetChildren(KDTreeNode<E> node, List<E[]> list = null)
        {
            if (list == null)
                list = new List<E[]>();
            if (node == null)
                return list;
            if (node.Value != null)
                list.Add(node.Value);
            if (node.Left != null)
                GetChildren(node.Left, list);
            if (node.Right != null)
                GetChildren(node.Right, list);
            return list;
        }

        public void PreOrderTraversal(KDTreeNode<E> node)
        {
            if (node == null)
                return;
            if (node.Value != null)
                Console.Write("\n" + string.Format("Value : {0:00},  Parent : {1}, Left Child : {2}, Right Child : {3}",
                                    node.ToString(),
                                    node.Parent != null ? node.Parent.ToString() : "No Parent",
                                    node.Left != null ? node.Left.ToString() : "No Left Child",
                                    node.Right != null ? node.Right.ToString() : "No Right Child")
                                    );
            if (node.Left != null)
                PreOrderTraversal(node.Left);
            if (node.Right != null)
                PreOrderTraversal(node.Right);
        }

        public void InOrderTraversal(KDTreeNode<E> node)
        {
            if (node == null)
                return;
            if (node.Left != null)
                InOrderTraversal(node.Left);
            if (node.Value != null)
                Console.Write("\n" + string.Format("Value : {0:00},  Parent : {1}, Left Child : {2}, Right Child : {3}",
                                    node.ToString(),
                                    node.Parent != null ? node.Parent.ToString() : "No Parent",
                                    node.Left != null ? node.Left.ToString() : "No Left Child",
                                    node.Right != null ? node.Right.ToString() : "No Right Child")
                                    );
            if (node.Right != null)
                InOrderTraversal(node.Right);
        }

        public void PostOrderTraversal(KDTreeNode<E> node)
        {
            if (node == null)
                return;
            if (node.Left != null)
                PostOrderTraversal(node.Left);
            if (node.Right != null)
                PostOrderTraversal(node.Right);
            if (node.Value != null)
                Console.Write("\n" + string.Format("Value : {0:00},  Parent : {1}, Left Child : {2}, Right Child : {3}",
                                    node.ToString(),
                                    node.Parent != null ? node.Parent.ToString() : "No Parent",
                                    node.Left != null ? node.Left.ToString() : "No Left Child",
                                    node.Right != null ? node.Right.ToString() : "No Right Child")
                                    );
        }
    }
}
