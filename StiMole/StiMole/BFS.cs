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
        Tree BFSAll(string path,string Target, List<string> pathOut)
        {   
            Queue<Tree> queue = new Queue<Tree>();
            Tree root = MTree.MakeTree(path);
            queue.Enqueue(root);

            while(queue.Count > 0)
            {   

                Tree Current = queue.Dequeue();

                foreach(Tree child in Current.children)
                {
                    queue.Enqueue(child);
                }

                if(Current.FileName == Target)
                {
                    Current.Found();
                    pathOut.Add(Current.FileName); //Temp
                    DFS.Color(Current);
                }
                else
                {
                    Current.NotFound();
                }
            }


            return root;
        }

        
      
        private async void BFSNOTALL(string path,string Target, List<string> pathOut,bool isFolder, Graph graph)
        {   
            Tree root = new Tree(path);
            if(isFolder){ 
                Queue<Tree> queue = new Queue<Tree>();
                queue.Enqueue(root);

                while(queue.Count > 0)
                {   
                    Tree Current = queue.Dequeue();

                    if(Current.FileName == Target)
                    {
                        Current.Found();
                        pathOut.Add(Current.Path); //Temp
                        ColorParent(Current, graph);
                        await Task.Delay(100);
                        ;
                        return;
                    }
                    else
                    {
                        Current.NotFound();
                        if (Current.parent != null)
                        {
                            Color(Current, graph);
                            await Task.Delay(100);
                        }
                    }

                    string[] Folders = ReadDirectory.FoldersInDirectory(Current.Path);
                    string[] Files = ReadDirectory.FilesInDirectory(Current.Path);

                    if (Files != null)
                    {
                        foreach (string file in Files)
                        {
                            Tree child = new Tree(file);
                            Current.AddChild(child);
                            Color(child, graph);
                            await Task.Delay(100);
                            queue.Enqueue(child);
                        }
                    }
                    if (Folders != null)
                    {
                        foreach (string folder in Folders)
                        {
                            Tree child = new Tree(folder);  
                            Current.AddChild(child);
                            Color(child, graph);
                            await Task.Delay(100);
                            queue.Enqueue(child);
                        }
                    }

                }
            }
        }

        private void Color (Tree Current, Graph graph)
        {
            foreach (Edge e in graph.Edges)
            {
                if (e.Source == Current.parent.id.ToString() + "\n" + Current.parent.FileName && e.Target == Current.id.ToString() + "\n" + Current.FileName)
                {
                    graph.RemoveEdge(e);
                    break;
                }
            }

            if (Current.warna == Warna.Hitam)
            {
                graph.AddEdge(Current.parent.id.ToString() + "\n" + Current.parent.FileName, Current.id.ToString() + "\n" + Current.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                gViewer1.Graph = graph;
            } else if (Current.warna == Warna.Merah)
            {
                graph.AddEdge(Current.parent.id.ToString() + "\n" + Current.parent.FileName, Current.id.ToString() + "\n" + Current.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                gViewer1.Graph = graph;
            } else if (Current.warna == Warna.Biru)
            {
                graph.AddEdge(Current.parent.id.ToString() + "\n" + Current.parent.FileName, Current.id.ToString() + "\n" + Current.FileName).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                gViewer1.Graph = graph;
            }
        }

        private void ColorParent (Tree Current, Graph graph)
        {
            Current.Found();
            if (Current.parent != null)
            {
                Color(Current, graph);
            }
            if (Current.parent != null)
            {
                ColorParent(Current.parent, graph);
            }
        }
    }
}
