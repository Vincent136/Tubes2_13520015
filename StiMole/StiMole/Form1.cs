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
            dialog.InitialDirectory = "c:\\";
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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (openedFile)
            {
                if (radioButton1.Checked)
                {
                    if (checkBox1.Checked)
                    {
                        string Target = textBox1.Text;
                        List<string> pathOut = new List<string>();
                        Graph graph = new Graph("graph");
                        Task<Tree> root = BFSAll(Path, Target,pathOut,true , graph);
                        Tree resetTree = await root;
                        resetTree.resetCounter();
                    } else
                    {
                        string Target = textBox1.Text;
                        List<string> pathOut = new List<string>();
                        Graph graph = new Graph("graph");
                        Task<Tree> root = BFSNOTALL(Path, Target, pathOut, true, graph);
                        Tree resetTree = await root;
                        linkLabel1.Text = pathOut[0];
                        resetTree.resetCounter();
                    }
                } else
                {
                    if (checkBox1.Checked)
                    {
                        string Target = textBox1.Text;
                        List<string> pathOut = new List<string>();
                        Graph graph = new Graph("graph");
                        Task<Tree> root = DFSall(Path, Target, pathOut, null, true, graph);
                        Tree resetTree = await root;
                        resetTree.resetCounter();
                    }
                    else
                    {
                        string Target = textBox1.Text;
                        List<string> pathOut = new List<string>();
                        Graph graph = new Graph("graph");
                        List<bool> found = new List<bool>();
                        found.Add(false);
                        Task<Tree> root = DFSNOTALL(Path, Target, pathOut, null, true, found, graph);
                        Tree resetTree = await root;
                        linkLabel1.Text = pathOut[0];
                        resetTree.resetCounter();
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel label = sender as LinkLabel;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = label.Text;
            dialog.ShowDialog();
        }
    }
}
