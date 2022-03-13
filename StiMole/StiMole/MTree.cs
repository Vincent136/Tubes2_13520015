namespace StiMole
{
    internal class MTree
    {
        public static Tree MakeTree(string Path)
        {
            string[] Folders = ReadDirectory.FoldersInDirectory(Path);
            string[] Files = ReadDirectory.FilesInDirectory(Path);

            Tree root = new Tree(Path);

            if (Files != null)
            {
                foreach (string file in Files)
                {
                    Tree child = MakeTree(file, false);
                    root.AddChild(child);
                }
            }
            if (Folders != null)
            {
                foreach (string folder in Folders)
                {
                    Tree child = MakeTree(folder, true);
                    root.AddChild(child);
                }
            }

            return root;
        }

        public static Tree MakeTree(string s, bool isFolder)
        {
            if (isFolder)
            {
                string[] Folders = ReadDirectory.FoldersInDirectory(s);
                string[] Files = ReadDirectory.FilesInDirectory(s);

                Tree root = new Tree(s);

                if (Files != null)
                {
                    foreach (string file in Files)
                    {
                        Tree child = MakeTree(file, false);
                        root.AddChild(child);
                    }
                }
                if (Folders != null)
                {
                    foreach (string folder in Folders)
                    {
                        Tree child = MakeTree(folder, true);
                        root.AddChild(child);
                    }
                }
                return root;
            }
            else
            {
                Tree root = new Tree(s);
                return root;
            }
        }
    }
}

