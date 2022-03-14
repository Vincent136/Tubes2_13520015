using System.Collections.Generic;
using System.Linq;

namespace StiMole
{
    internal class DFS
    {
        //Path: starting path, Target: file name, pathOut: all the path that leads to the file, searchAll: set to true to search all instances
        public static Tree Search(string Path, string Target, List<string> pathOut, bool searchAll)
        {
            if(searchAll)
            {
                return DFSall(Path, Target, pathOut, null, true);
            }
            else
	{

	}
            {
                List<bool> found = new List<bool>();
                found.Add(false);
                return DFSNOTALL(Path, Target, pathOut, null, true, found);
            }
        }
        public static Tree DFSall(string Path, string Target, List<string> pathOut, Tree parent, bool isFolder)
        {
            if (isFolder)
            {
                string[] Folders = ReadDirectory.FoldersInDirectory(Path);
                string[] Files = ReadDirectory.FilesInDirectory(Path);

                Tree root = new Tree(Path, parent, Warna.Merah);

                if (Files != null)
                {
                    foreach (string file in Files)
                    {
                        Tree child = DFSall(file, Target, pathOut, root, false);
                        root.AddChild(child);
                    }
                }
                if (Folders != null)
                {
                    foreach (string folder in Folders)
                    {
                        Tree child = DFSall(folder, Target, pathOut, root, true);
                        root.AddChild(child);
                    }
                }
                return root;
            }
            else
            {
                Tree root = new Tree(Path, parent, Warna.Merah);
                if(root.FileName == Target)
                {
                    Color(root);
                    pathOut.Add(Path);
                }
                return root;
            }
        }
        public static void Color(Tree root)
        {
            root.Found();
            if(root.parent != null)
            {
                Color(root.parent);
            }
        }

        public static Tree DFSNOTALL(string Path, string Target, List<string> pathOut, Tree parent, bool isFolder, List<bool> found)
        {

            if (isFolder)
            {
                string[] Folders = ReadDirectory.FoldersInDirectory(Path);
                string[] Files = ReadDirectory.FilesInDirectory(Path);

                Tree root = new Tree(Path, parent, Warna.Merah);

                if (Files != null)
                {
                    foreach (string file in Files)
                    {
                        if (!found[0])
                        {
                            Tree child = DFSNOTALL(file, Target, pathOut, root, false, found);
                            root.AddChild(child);
                        }
                    }
                }
                if (Folders != null)
                {
                    foreach (string folder in Folders)
                    {
                        if (!found[0])
                        {
                            Tree child = DFSNOTALL(folder, Target, pathOut, root, true, found);
                            root.AddChild(child);
                        }
                    }
                }
                return root;
            }
            else
            {
                Tree root = new Tree(Path, parent, Warna.Merah);
                if (root.FileName == Target)
                {
                    found[0] = true;
                    Color(root);
                    pathOut.Add(Path);
                }
                return root;
            }
        }

    }
}
