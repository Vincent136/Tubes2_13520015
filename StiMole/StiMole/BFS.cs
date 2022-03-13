using System.Collections.Generic;

namespace StiMole
{
    internal class BFS
    {
        public static Tree BFSAll(string path,string Target, List<string> pathOut)
        {
            return MTree.MakeTree(path);
        }

        public static Tree BFSNOTALL(string path,string Target, List<string> pathOut)
        {
            return new Tree(path);
        }
    }
}
