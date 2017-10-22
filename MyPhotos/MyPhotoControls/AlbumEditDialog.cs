using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using manning.MyphotoAlbum;

namespace manning.MyPhotoControls
{
    public partial class AlbumEditDialog : manning.MyPhotoControls.BaseEditDialog
    {
        private AlbumManager _manager;
        private AlbumManager Manager
        {
            get { return _manager; }
        }
        public AlbumEditDialog(AlbumManager mgr)
        {
            if (mgr == null)
                throw new ArgumentException("AlbumManager cannot be null");
            InitializeComponent();

            _manager = mgr;
            ResetDialog();
        }

        private void cbxPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = cbxPassword.Checked;
            txtPassword.Enabled = enabled;
            lblConfirm.Enabled = enabled;
            txtConfirm.Enabled = enabled;

            // if enabled, assign focus
            if (enabled)
                txtPassword.Focus();


        }

        protected override void ResetDialog()
        {
            PhotoAlbum album = Manager.Album;

            // Assign text boxes
            txtAlbumFile.Text = Manager.FullName;
            txtTitle.Text = album.Title;

            // Assign radio button
            switch (album.PhotoDescriptor)
            {
                case PhotoAlbum.DescriptorOption.Caption:
                    rbtnCaption.Checked = true;
                    break;
                case PhotoAlbum.DescriptorOption.DateTaken:
                    rbtnDateTaken.Checked = true;
                    break;
                case PhotoAlbum.DescriptorOption.FileName:
                    rbtnFileName.Checked = true;
                    break;
            }


            base.ResetDialog();
        }
    }
}
