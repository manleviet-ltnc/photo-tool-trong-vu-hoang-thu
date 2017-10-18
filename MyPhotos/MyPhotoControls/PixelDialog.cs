﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using manning.MyphotoAlbum;

namespace manning.MyPhotoControls
{
    public partial class PixelDialog : Form
    {
        public PixelDialog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetPixelData(int x, int y, int red,int green,int blue)
        {
            lblX.Text = x.ToString();
            lblY.Text = y.ToString();
            lblRed.Text = red.ToString();
            lblGreen.Text = green.ToString();
            lblBlue.Text = blue.ToString();
        }

        public void ClearPixelData()
        {
            SetPixelData(0, 0, 0, 0, 0);
        }
        public void UpdatePixelData(int xPos, int yPos, Bitmap bmp, 
                                    Rectangle displayRect, Rectangle bmpRect,
                                    PictureBoxSizeMode sizeMode)
        {
            // Determine (x,y) position within image
            int x = 0, y = 0;
            switch(sizeMode)
            {
                case PictureBoxSizeMode.AutoSize:
                case PictureBoxSizeMode.CenterImage:
                    throw new NotSupportedException("The Autosize and CenterImage size modes"
                                                     + "are not supported at this time.");
                case PictureBoxSizeMode.Normal:
                    // Rectangle coords are image coords
                    if (xPos >= bmp.Width || yPos >= bmp.Height)
                        return; // posion outside image
                    x = xPos - bmpRect.X;
                    y = xPos - bmpRect.Y;
                    break;
                case PictureBoxSizeMode.StretchImage:
                    // Translate rect coord to image
                    x = xPos * bmp.Width / displayRect.Width;
                    y = yPos * bmp.Height / displayRect.Height;
                    break;
                case PictureBoxSizeMode.Zoom:
                    // Determine image rectangle
                    Rectangle r2 = ImageUtility.ScaleToFit(bmp, displayRect);
                    if (!r2.Contains(xPos, yPos))
                        return; // position outside image
                    x = (xPos - r2.Left) * bmp.Width / r2.Width;
                    y = (yPos - r2.Top) * bmp.Height / r2.Height;
                    break;
            }
            Color c = bmp.GetPixel(x, y);
            SetPixelData(x, y, c.R, c.G, c.B);
        }
    }
}
