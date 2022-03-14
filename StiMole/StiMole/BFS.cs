using System.Collections.Generic;

namespace StiMole
{
    internal class BFS
    {   
        public static Tree BFSAll(string path,string Target, List<string> pathOut)
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

        
      
        public static Tree BFSNOTALL(string path,string Target, List<string> pathOut,bool isFolder)
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
                        DFS.Color(Current);
                        return root;
                    }
                    else
                    {
                        Current.NotFound();
                    }

                    string[] Folders = ReadDirectory.FoldersInDirectory(Current.Path);
                    string[] Files = ReadDirectory.FilesInDirectory(Current.Path);

                    if (Files != null)
                    {
                        foreach (string file in Files)
                        {
                            Tree child = new Tree(file);
                            Current.AddChild(child);
                            queue.Enqueue(child);
                        }
                    }
                    if (Folders != null)
                    {
                        foreach (string folder in Folders)
                        {
                            Tree child = new Tree(folder);  
                            Current.AddChild(child);
                            queue.Enqueue(child);
                        }
                    }

                }
            }
            return root;
        }


    }
}
