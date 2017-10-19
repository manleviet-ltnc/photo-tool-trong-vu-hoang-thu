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
    public partial class PhotoEditDialog : manning.MyPhotoControls.BaseEditDialog
    {
        private photograph _photo;
        private photograph Photo
        {
            get { return _photo; }
        }
        private AlbumManager _manager = null;
        private AlbumManager Manager
        {
            get { return _manager; }
        }
        protected PhotoEditDialog()
        {
            InitializeComponent();
        }

        public PhotoEditDialog(photograph photo):this()
        {
            if (photo == null)
                throw new ArgumentNullException(" The photo parameter cannot be null");
            InitializeDialog(photo);
        }

        public PhotoEditDialog(AlbumManager mgr):this()
        {
            if (mgr == null)
                throw new ArgumentNullException("The mgr parameter cannot be null");
            _manager = mgr;
            InitializeDialog(mgr.Current);
        }

        private void InitializeDialog(photograph photo)
        {
            _photo = photo;
            ResetDialog();
            mskDateTaken.ValidatingType = typeof(CurrentDate);
        }

        protected override void ResetDialog()
        {
            photograph photo = Photo;
            if(photo!=null)
            {
                txtPhotoFile.Text = photo.Filename;
                txtCaption.Text = photo.Caption;
                mskDateTaken.Text = photo.DateTaken.ToString();
                txtPhotographer.Text = photo.Photographer;
                txtNotes.Text = photo.Notes;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
                SaveSettings();
        }
        private void SaveSettings()
        {
            photograph photo = Photo;
            if(photo!=null)
            {
                photo.Caption = txtCaption.Text;
                photo.Photographer = txtPhotographer.Text;
                photo.Notes = txtNotes.Text;
                try
                {
                    photo.DateTaken = DateTime.Parse(mskDateTaken.Text);
                }
                catch (FormatException) { }
            }
        }

       
        private void txtCaption_TextChanged(object sender, EventArgs e)
        {
            Text = txtCaption.Text + "-Properties";
        }

        private static class CurrentDate
        {
            public static DateTime Parse(string input)
            {
                DateTime result = DateTime.Parse(input);
                if (result > DateTime.Now)
                    throw new FormatException("The given date is in the future.");
                return result;
            }
        }

        private void mskDateTaken_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if(!e.IsValidInput)
            {
                DialogResult result = MessageBox.Show("The Date Taken entry is invalid or"
                                                     + "in the future may be ignore."
                                                     + "Do you wish to correct this?",
                                                     "Photo properties",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                e.Cancel = (result == DialogResult.Yes);
            }
        }
    }
}
