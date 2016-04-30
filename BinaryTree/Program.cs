using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinaryTree
{
    class Program : Form
    {
        BinaryTree<int> bTree = new BinaryTree<int>();

        public Program()
        {
            InitComponent();
        }

        public void InitComponent()
        {
            int[] num = new int[22];
            Random r = new Random();

            for (int x = 0;x < num.Length;x++)
                num[x] = r.Next(-100, 100);

            bTree.AddNodes(num);
            bTree.AVL();

            Size = Screen.PrimaryScreen.WorkingArea.Size;
            Location = new Point { X = 100, Y = 500 };
            Paint += new PaintEventHandler(Paint_Something);
        }

        private void Paint_Something(object sender, PaintEventArgs e)
        {
            Pen pen;
            Color color = Color.Black;
            pen = new Pen(color, 1.0f);
            SolidBrush brush = new SolidBrush(color);

            Graphics g = CreateGraphics();

            //KDTree<int> kdTree = new KDTree<int>(3);
            
            //kdTree.AddNode(new int[] { 6, 2, 7 });
            //kdTree.AddNode(new int[] { 4, 1, 6 });
            //kdTree.AddNode(new int[] { 7, 0, 9 });
            //kdTree.AddNode(new int[] { 10, 2, 3 });
            //kdTree.AddNode(new int[] { 3, 3, 0 });
            //kdTree.AddNode(new int[] { 6, 8, 9 });
            //kdTree.AddNode(new int[] { 0, 6, 1 });

            //kdTree.AddNode(new int[] { 3, 7, 1 });
            //kdTree.AddNode(new int[] { 8, 1, 1 });
            //kdTree.AddNode(new int[] { 6, 6, 2 });
            //kdTree.AddNode(new int[] { 2, 9, 4 });
            //kdTree.AddNode(new int[] { 8, 7, 1 });
            //kdTree.AddNode(new int[] { 2, 8, 8 });
            //kdTree.AddNode(new int[] { 5, 7, 1 });
            //kdTree.AddNode(new int[] { 1, 6, 3 });

            //kdTree.AddNode(new int[] { 51, 75 });
            //kdTree.AddNode(new int[] { 25, 40 });
            //kdTree.AddNode(new int[] { 10, 30 });
            //kdTree.AddNode(new int[] { 35, 90 });
            //kdTree.AddNode(new int[] { 1, 10 });
            //kdTree.AddNode(new int[] { 50, 50 });
            //kdTree.AddNode(new int[] { 70, 70 });
            //kdTree.AddNode(new int[] { 55, 1 });
            //kdTree.AddNode(new int[] { 60, 80 });
            //kdTree.PreOrderTraversal(kdTree.Root);
            //Console.WriteLine(s);

            //kdTree.InOrderTraversal(kdTree.Root);
            //Console.WriteLine(s);

            //kdTree.PostOrderTraversal(kdTree.Root);
            //Console.WriteLine(s);

            //KDTreeNode<int> node = kdTree.FindMinimum(kdTree.Root, 0, 0);
            //KDTreeNode<int> node2 = kdTree.FindMinimum(kdTree.Root, 1, 0);
            //KDTreeNode<int> node3 = kdTree.FindMinimum(kdTree.Root, 2, 0);

            //Console.WriteLine(node == null ? "Node is null" : node.ToString());
            //Console.WriteLine(node2 == null ? "Node is null" : node2.ToString());
            //Console.WriteLine(node3 == null ? "Node is null" : node3.ToString());

            //kdTree.DrawTree(g, pen, brush, Screen.PrimaryScreen.WorkingArea.Width / 5, 50);

            bTree.DrawTree(g, pen, brush, Screen.PrimaryScreen.WorkingArea.Width / 2, 50);

            //KDTreeNode<int> aNode = kdTree.Find(new int[] { 0, 6, 1 });
            //kdTree.DeleteNode(aNode, aNode.Value, 0);
            //Console.WriteLine(s);

            //kdTree.PreOrderTraversal(kdTree.Root);
            //Console.WriteLine(s);

            //kdTree.DrawTree(g, pen, brush, Screen.PrimaryScreen.WorkingArea.Width * 3 / 5, 50);
        }

        static void Main(string[] args)
        {
            Application.Run(new Program());
            
        //   Console.Read();
        }
    }
}
