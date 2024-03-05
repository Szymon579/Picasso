using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordsGame
{
    class PictureBoxHandler
    {
        private Client client;
        private Bitmap bmp;
        private Graphics graphics;
        private bool paint = false;
        private Point pCurrent, pPrevious;
        private Pen pen = new Pen(Color.Black, 2);
        private PictureBox pictureBox;

        
        public PictureBoxHandler(ref Client client, ref PictureBox pictureBox)
        {
            this.client = client;
            this.pictureBox = pictureBox;
        }
        
        public void InitPainting()
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bmp);
            graphics.Clear(Color.White);
            pictureBox.Image = bmp;
        }


        public void MouseDownEvent(object sender, MouseEventArgs e)
        {
            //PictureBox pictureBox = sender as PictureBox;
            //if (pictureBox == null) return;

            paint = true;
            pCurrent = e.Location;
            pPrevious = e.Location;
       
        }

        public void MouseUpEvent(object sender, MouseEventArgs e)
        {
            //PictureBox pictureBox = sender as PictureBox;
            //if (pictureBox == null) return;

            paint = false;
            client.SendCanvas(bmp);
        }

        public void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            //PictureBox pictureBox = sender as PictureBox;
            //if (pictureBox == null) return;

            if (paint)
            {
                pCurrent = e.Location;
                graphics.DrawLine(pen, pCurrent, pPrevious);
                pPrevious = pCurrent;
            }
            pictureBox.Refresh();
        }

    }
}
