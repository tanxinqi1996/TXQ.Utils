using System.Collections.Generic;
using System.Windows.Forms;

namespace TXQ.Utils.Tool
{
    public static class EXTreeNode
    {
        public static List<TreeNode> GetAllTreeNodes(this TreeNode node)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (TreeNode item in node.Nodes)
            {
                nodes.Add(item);
                if (item.Nodes.Count != 0)
                {
                    nodes.AddRange(item.GetAllTreeNodes());
                }
            }
            return nodes;
        }
        public static List<TreeNode> GetAllTreeNodes(this TreeView tree)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (TreeNode item in tree.Nodes)
            {
                nodes.Add(item);
                if (item.Nodes.Count != 0)
                {
                    nodes.AddRange(GetAllTreeNodes(item));
                }
            }
            return nodes;
        }
    }
}

