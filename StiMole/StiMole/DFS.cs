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
    partial class Form1 : Form
    {
        //Path: starting path, Target: file name, pathOut: all the path that leads to the file, searchAll: set to true to search all instances
        private async Task<Tree> Search(string Path, string Target, List<string> pathOut, bool searchAll, Graph graph,int interval)
        {
            if(searchAll)
            {
                return await DFSall(Path, Target, pathOut, null, true, graph,interval);
            }
            else
	{

	}
            {
                List<bool> found = new List<bool>();
                found.Add(false);
                return await DFSNOTALL(Path, Target, pathOut, null, true, found, graph,interval);
            }
        }
        private async Task<Tree> DFSall(string Path, string Target, List<string> pathOut, Tree parent, bool isFolder,Graph graph,int interval)
        {
            if (isFolder)
            {
                string[] Folders = ReadDirectory.FoldersInDirectory(Path);
                string[] Files = ReadDirectory.FilesInDirectory(Path);

                Tree root = new Tree(Path, parent, Warna.Merah);
                if (root.parent != null)
                {
                    Color(root, graph);
                    await Task.Delay(interval);
                }

                if (Files != null)
                {
                    foreach (string file in Files)
                    {
                        Task<Tree> tchild = DFSall(file, Target, pathOut, root, false, graph,interval);
                        Tree child = await tchild;
                        root.AddChild(child);
                        Color(child, graph);
                        await Task.Delay(interval);
                    }
                }
                if (Folders != null)
                {
                    foreach (string folder in Folders)
                    {
                        Task<Tree> tchild = DFSall(folder, Target, pathOut, root, true, graph,interval);
                        Tree child = await tchild;
                        root.AddChild(child);
                        Color(child, graph);
                        await Task.Delay(interval);
                    }
                }
                return root;
            }
            else
            {
                Tree root = new Tree(Path, parent, Warna.Merah);
                if(root.FileName == Target)
                {
                    ColorParent(root, graph);
                    await Task.Delay(interval);
                    pathOut.Add(Path);
                }
                return root;
            }
        }

        private async Task<Tree> DFSNOTALL(string Path, string Target, List<string> pathOut, Tree parent, bool isFolder, List<bool> found, Graph graph,int interval)
        {

            if (isFolder)
            {
                string[] Folders = ReadDirectory.FoldersInDirectory(Path);
                string[] Files = ReadDirectory.FilesInDirectory(Path);

                Tree root = new Tree(Path, parent, Warna.Merah);
                if (root.parent != null)
                {
                    Color(root, graph);
                    await Task.Delay(interval);
                }

                if (Files != null)
                {
                    foreach (string file in Files)
                    {
                        if (!found[0])
                        {
                            Task<Tree> tchild = DFSNOTALL(file, Target, pathOut, root, false, found, graph,interval);
                            Tree child = await tchild;
                            root.AddChild(child);
                            Color(child, graph);
                            await Task.Delay(interval);
                        }
                    }
                }
                if (Folders != null)
                {
                    foreach (string folder in Folders)
                    {
                        if (!found[0])
                        {
                            Task<Tree> tchild = DFSNOTALL(folder, Target, pathOut, root, true, found, graph,interval);
                            Tree child = await tchild;
                            root.AddChild(child);
                            Color(child, graph);
                            await Task.Delay(interval);
                        }
                    }
                }
                return root;
            }
            else
            {
                Tree root = new Tree(Path, parent, Warna.Merah);
                if (root.parent != null)
                {
                    Color(root, graph);
                    await Task.Delay(interval);
                }
                if (root.FileName == Target)
                {
                    found[0] = true;
                    ColorParent(root, graph);
                    await Task.Delay(interval);
                    pathOut.Add(Path);
                }
                return root;
            }
        }

    }
}
