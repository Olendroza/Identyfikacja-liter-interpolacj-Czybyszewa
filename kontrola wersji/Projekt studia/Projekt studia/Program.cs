using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projekt_studia //dodac normalizacje 
{
    class Program
    {
        static void Main(string[] args)
        {
            classifier cls = new classifier();
            //imageSupport imS = new imageSupport(@"C:\Users\Piotr\Documents\projekty vs2017\p.jpg");
            //imS.binarization();
            //  imS.ResizeImage();
            //imS.save(@"C:\Users\Piotr\Documents\projekty vs2017\ppbin.jpg");
            //  imS.binarization();
            //    Console.WriteLine(imS.btm.GetPixel(0, 0).R);
            //    Console.ReadKey();
            //imS.btm=imS.cut();
            //imS.save(@"C:\Users\Piotr\Documents\projekty vs2017\ppcut.jpg");
            //imS.ResizeImage();
            //imS.makePointsList();
            //imS.makeInterpolationList();
            //cls.loadPatterns();
           //cls.makePattern(imS.interpolationList, 0);
            //cls.savePatterns();





            // latwiej mi sie patrzy jak nie mam innych rzeczy
            imageSupport ima0 = new imageSupport(@"C:\Users\Piotr\Documents\projekty vs2017\a0.jpg");
            ima0.binarization();
            ima0.btm = ima0.cut();
            ima0.ResizeImage();
            ima0.save(@"C:\Users\Piotr\Documents\projekty vs2017\pa0.jpg");
            ima0.makePointsList();
            ima0.makeInterpolationList();
            cls.makePattern(ima0.interpolationList, 0);
            cls.savePatterns();

            imageSupport ima1 = new imageSupport(@"C:\Users\Piotr\Documents\projekty vs2017\a1.jpg");
            ima1.binarization();
            ima1.btm = ima1.cut();
            ima1.ResizeImage();
            ima1.save(@"C:\Users\Piotr\Documents\projekty vs2017\pa1.jpg");
            ima1.makePointsList();
            ima1.makeInterpolationList();
            cls.makePattern(ima1.interpolationList, 0);
           //cls.savePatterns();

            imageSupport ima2 = new imageSupport(@"C:\Users\Piotr\Documents\projekty vs2017\a2.jpg");
            ima2.binarization();
            ima2.btm = ima2.cut();
            ima2.ResizeImage();
            ima2.save(@"C:\Users\Piotr\Documents\projekty vs2017\pa2.jpg");
            ima2.makePointsList();
            ima2.makeInterpolationList();
            cls.makePattern(ima2.interpolationList, 0);
            //cls.savePatterns();

            imageSupport ima3 = new imageSupport(@"C:\Users\Piotr\Documents\projekty vs2017\a3.jpg");
            ima3.binarization();
            ima3.btm = ima3.cut();
            ima3.ResizeImage();
            //ima3.save(@"C:\Users\Piotr\Documents\projekty vs2017\pa3.jpg");
            ima3.makePointsList();
            ima3.makeInterpolationList();
            string napis = cls.classify(ima3.interpolationList);
            Console.WriteLine("to literka "+napis);
            Console.ReadKey();



        }
    }
}
