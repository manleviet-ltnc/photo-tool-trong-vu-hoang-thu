using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Manning.MyPhotoAlbum;
using Manning.MyPhotoControls;

namespace MyAlbumExplorer
{
    internal class AlbumTreeView : TreeView
    {
        public AlbumDirectoryNode AddAlbumDirectory(string name,string albumDir)
        {
            AlbumDirectoryNode node = new AlbumDirectoryNode(name, albumDir);
            this.Nodes.Add(node);
            return node;
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);
            if (e.Node is AlbumDirectoryNode)
                ExpandAlbumDirectory(e.Node as AlbumDirectoryNode);
            else if (e.Node is AlbumNode)
                ExpandAlbum(e.Node as AlbumNode);

        }

        private void ExpandAlbumDirectory(AlbumDirectoryNode node )
        {
            BeginUpdate();
            try
            {
                node.CreateAlbumNodes();
            }
            finally { EndUpdate(); }
        }

        private void ExpandAlbum(AlbumNode node)
        {
            AlbumManager mgr = node.GetManager(true);
            if(mgr!=null)
            {
                BeginUpdate();
                try
                {
                    node.Nodes.Clear();
                    foreach(Photograph p in mgr.Album)
                    {
                        PhotoNode newNode = new PhotoNode(p);
                        node.Nodes.Add(newNode);
                    }
                }
                finally { EndUpdate(); }
            }
        }
    }
}
