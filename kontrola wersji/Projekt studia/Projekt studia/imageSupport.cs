using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Projekt_studia
{
    
    class imageSupport
    {
        public Bitmap btm;
        public List<Int64> pointsList= new List<Int64>();
        public List<Int64> interpolationList = new List<Int64>();
        public imageSupport(string path)
        {
            this.btm = new Bitmap(@path);
        }
        public void binarization()
        {

            for (int i = 0; i < btm.Width; i++)
            {
                for (int j = 0; j < btm.Height; j++)
                {
                    int r = btm.GetPixel(i, j).R;
                    int b = btm.GetPixel(i, j).B;
                    int g = btm.GetPixel(i, j).G;

                    if (r+g+b<350)
                    {
                        Color kolor = Color.FromArgb(0, 0, 0);
                        btm.SetPixel(i, j, kolor);
                    }
                    else
                    {
                        Color kolor = Color.FromArgb(255, 255, 255);
                        btm.SetPixel(i, j, kolor);
                    }
                }
            }

        }
        public void makePointsList()
        {
            
            
            for (int i = 0; i < btm.Width; i++)
            {
                int counter = 0;
                int sum = 0;
                for (int j = 0; j < btm.Height; j++)
                {
                    int r = btm.GetPixel(i, j).R;
                    if (r==255)
                    {
                        counter++;
                        sum += j;
                    }
                }
                if (counter == 0)
                    counter++;

                this.pointsList.Add(Convert.ToInt64(sum/counter));
            }
        }
        public void makeInterpolationList()
        {

            interpolationList.Add(1);
            interpolationList.Add(this.pointsList[1]);
            for(int i=2;i<pointsList.Count;i++)
            {
                interpolationList.Add(2 * this.pointsList[i] * interpolationList[i - 1] - interpolationList[i - 2]);
            }
        }
        public void save(string path)
        {
            btm.Save(@path);
        }
        public void ResizeImage()
        {
            Bitmap resized = new Bitmap(this.btm, new Size(256, 256));
            this.btm = resized;
            //resized.Save(@"C:\\Users\\Piotr\\Documents\\projekty vs2017\\btm.jpg"); testy
            //this.btm.Save(@"C:\\Users\\Piotr\\Documents\\projekty vs2017\\btm.jpg"); testy
        }
        public Bitmap cut()
        {
            Bitmap btm = this.btm;
            int x = 0;
            int x1 = 0;
            int y = 0;
            int y1 = 0;
            int width = 0;
            int height = 0;

            for (int i = 0; i < btm.Height; i++)// \/
            {
                for (int j = 0; j < btm.Width; j++)// -> 
                {
                    if (btm.GetPixel(j, i).R < 200)
                    {
                        y = i;
                        j = btm.Width;
                        i = btm.Height;
                    }

                }
            }
            for (int i = btm.Height-1; i > 0; i--)// /\
            {
                for (int j = 0 ; j <btm.Width; j++)// nie dziala jeszcze 
                {
                    if (btm.GetPixel(j, i).R < 200)
                    {
                        y1 = i;
                        j = btm.Width;
                        i = 0;
                    }

                }
            }
            height = y1 - y + 1;

            for (int i = 0; i < btm.Width; i++)
            {
                for (int j = 0; j < btm.Height; j++)
                {
                    if (btm.GetPixel(i, j).R <200)
                    {
                        x = i;
                        j = btm.Height - 1;
                        i = btm.Width - 1;
                    }
                }
            }
            for (int i = btm.Width - 1; i > 0; i--)
            {
                for (int j = btm.Height - 1; j > 0; j--)
                {
                    if (btm.GetPixel(i, j).R <200)
                    {
                        x1 = i;
                        i = 0;
                        j = 0;
                    }
                }
            }
                width = x1 - x + 1;
            

            Rectangle part = new Rectangle(x, y, width, height);
            //Console.ReadKey();
          //  Bitmap bmp = new Bitmap(width, height);
           // Graphics g = Graphics.FromImage(bmp);
            //g.DrawImage(picture, x, y, part, GraphicsUnit.Pixel);

//            g.Dispose();


            Bitmap bmpImage = new Bitmap(this.btm);
            return bmpImage.Clone(part, bmpImage.PixelFormat);
           // return bmp;


        }
    }
}
