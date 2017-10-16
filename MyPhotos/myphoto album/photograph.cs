﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace manning.MyphotoAlbum
{
    /// <summary>
    /// The photograph class represents a photographic
    /// image stored in the file system
    /// </summary>
  public  class photograph: IDisposable
    {
        private string _filename;
        public string Filename
        {
            get { return this._filename; }
        }
        private Bitmap _bitmap;
        public Bitmap Image
        {
            get
            {
                if(_bitmap==null)
                    _bitmap=new Bitmap(_filename);
                return _bitmap;
            }
        }
        private string _caption = "";
        public string Caption
        {
            get { return _caption; }
            set
            {
                if(_caption!=value)
                {
                    _caption = value;
                    HasChanged = true;
                }
            }
        }
        private string _photographer = "";
        public string Photographer
        {
            get { return _photographer; }
            set
            {
                if(_photographer!=value)
                {
                    _photographer = value;
                    HasChanged = true;
                }
            }
        }
        private DateTime _dateTaken = DateTime.Now;
        public DateTime DateTaken
        {
            get { return _dateTaken; }
            set
            {
                if(_dateTaken!=value)
                {
                    _dateTaken = value;
                    HasChanged = true;
                }
            }
        }
        private string _notes = "";
        public string Notes
        {
            get { return _notes; }
            set
            {
                if(_notes!=value)
                {
                    _notes = value;
                    HasChanged = true;
                }
            }
        }
        private bool _hasChanged = true;
        public bool HasChanged
        {
            get { return _hasChanged; }
            set { _hasChanged = value; }
        }
        public photograph(string fileName)
        {
            _filename = fileName;
            _bitmap = null;
            _caption = System.IO.Path.GetFileNameWithoutExtension(fileName);
        }
        public override bool Equals(object obj)
        {
            if(obj is photograph)
            {
                photograph p = (photograph)obj;
                return string.Equals(Filename, p.Filename, StringComparison.InvariantCultureIgnoreCase);  
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Filename.ToLowerInvariant().GetHashCode();
        }
        public override string ToString()
        {
            return Filename;
        }
        public void ReleaseImage()
        {
            if(_bitmap!=null)
              {
                _bitmap.Dispose();
                _bitmap = null;
              }
        }
        public void Dispose()
        {
            ReleaseImage();
        }
    }
}
