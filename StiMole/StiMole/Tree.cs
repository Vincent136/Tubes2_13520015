using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiMole
{
    delegate void TreeVisitor(string nodeFileName);
    enum Warna
    {
        Hitam,
        Merah,
        Biru
    }
    internal partial class Tree
    {
        public static int counter { get; set; }

        public int  id { get; private set; }

        public string FileName { get; private set; }

        public string Path { get; private set; }

        public Warna warna { get; private set; }

        public LinkedList<Tree> children;

        public Tree parent;

        public Tree(string Path)
        {
            string[] pathSplit = Path.Split('\\');
            this.FileName = pathSplit.Last();
            this.Path = Path;
            warna = Warna.Hitam;
            parent = null;
            counter++;
            id = counter;
            children = new LinkedList<Tree>();
        }

        public Tree(string Path, Tree parent)
        {
            string[] pathSplit = Path.Split('\\');
            this.FileName = pathSplit.Last();
            this.Path = Path;
            warna = Warna.Hitam;
            this.parent = parent;
            counter++;
            id = counter;
            children = new LinkedList<Tree>();
        }

        public void AddChild(string Path)
        {
            children.AddLast(new Tree(Path, this));
        }

        public void AddChild(Tree child)
        {
            children.AddLast(child);
            child.parent = this;
        }

        public Tree GetChild(int i)
        {
            i = i + 1;
            foreach (Tree n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public void Found()
        {
            warna = Warna.Biru;
        }

        public void NotFound()
        {
            warna = Warna.Merah;
        }
        
        public void resetCounter()
        {
            counter = 0;
        }

        public void Traverse(Tree node, TreeVisitor visitor)
        {
            visitor(node.FileName);
            foreach (Tree kid in node.children)
                Traverse(kid, visitor);
        }
    }
}
