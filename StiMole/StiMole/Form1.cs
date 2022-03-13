using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Msagl.Drawing;

namespace StiMole
{
    public partial class Form1 : Form
    {
        string Path = "";
        bool openedFile = false;
        public Form1()
        {
            InitializeComponent();
            this.Text = String.Empty;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Path = dialog.FileName;
                label3.Text = Path;
            }
            openedFile = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label10.Text = "You choose : BFS";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label10.Text = "You choose : DFS";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openedFile)
            {
                //ini terakhir kali nopal edit buat nyelesain DFS, kalau mau nyoba, tinggal set targetnya sama pilih search all ato ngga
                string Target = "BruteForce.java";
                bool searchAll = false;
                List<string> pathOut = new List<string>();
                Tree root = DFS.Search(Path, Target, pathOut, searchAll);      
                root.resetCounter();
                drawTree(root);
            }
        }

        private void drawTree(Tree root)
        {
            Graph graph = new Graph("graph");
            if (root.children != null)
            {
                foreach (Tree child in root.children)
                {
                    drawTree(child, graph);
                    if (child.warna == Warna.Merah)
                    {
                        graph.AddEdge(root.id.ToString() + "\n" + root.FileName, child.id.ToString() + "\n" + child.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    } else if (child.warna == Warna.Hitam)
                    {
                        graph.AddEdge(root.id.ToString() + "\n" + root.FileName, child.id.ToString() + "\n" + child.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    } else if (child.warna == Warna.Biru)
                    {
                        graph.AddEdge(root.id.ToString() + "\n" + root.FileName, child.id.ToString() + "\n" + child.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    }
                }
            }
            gViewer1.Graph = graph;
        }

        private void drawTree(Tree root, Graph graph)
        {
            if (root.children != null)
            {
                foreach (Tree child in root.children)
                { 
                    drawTree(child, graph);
                    if (child.warna == Warna.Merah)
                    {
                        graph.AddEdge(root.id.ToString() + "\n" + root.FileName, child.id.ToString() + "\n" + child.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }
                    else if (child.warna == Warna.Hitam)
                    {
                        graph.AddEdge(root.id.ToString() + "\n" + root.FileName, child.id.ToString() + "\n" + child.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    }
                    else if (child.warna == Warna.Biru)
                    {
                        graph.AddEdge(root.id.ToString() + "\n" + root.FileName, child.id.ToString() + "\n" + child.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    }
                }
            }  
        }
    }
}
