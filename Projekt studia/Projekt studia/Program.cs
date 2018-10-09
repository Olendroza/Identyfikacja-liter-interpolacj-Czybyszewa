using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projekt_studia 
{
    class Program
    {
        public static void addThisImageToPattern(classifier cls, string path, int letter)
        {
            imageSupport pattern = new imageSupport(@path);
            pattern.binarization();
            pattern.btm = pattern.cut();
            pattern.ResizeImage();
            pattern.binarization();
            pattern.makePointsList();
            pattern.makeInterpolationList();
            cls.makePattern(pattern.interpolationList, letter);
        }

        public static string tellMeWhatIsThisLetter(classifier cls, imageSupport letter)
        {
            string s = cls.classify(letter.interpolationList);
            return s;
        }
        static void Main(string[] args)
        {
            classifier cls = new classifier();
            Console.WriteLine("Program do sprawdzania literek");
            Console.WriteLine("1- Zrób wzór z alfabetu domyslnego");
            Console.WriteLine("2- Wczytaj gotowy wzor");
            Console.WriteLine("3- Klasyfikuj litere");
            ConsoleKeyInfo cki = Console.ReadKey();
            
            if (cki.KeyChar=='1')
            {
                //tworzenie alfabetu
                int sterujaca = -1;
                for (int i = 0; i < 3*26; i++)
                {
                    Console.Write(i + " ");
                    if (i % 3 == 0)
                    {
                        Console.Write("Laduje" + i + "   ");
                        sterujaca++;
                    }
                    string temp = Convert.ToString(i);
                    string str = "C:\\Users\\Piotr\\Documents\\projekty vs2017\\alfabet\\" + temp + ".jpg"; // ścieżka do plików ze wzormai alfabetu
                    addThisImageToPattern(cls, str,sterujaca);
                }
                //koniec tworzenia alfabetu
                Console.Clear();
                Console.WriteLine("Załadowano obrazki z folderu alfabet");
                cls.savePatterns();
                Console.WriteLine("zapisano");  
            }
            if(cki.KeyChar == '2')
            {
                cls.loadPatterns(); // ścieżka do wzorca jest w klasyfikatorze
                Console.Clear();
                Console.WriteLine("wczytano ze wzoru.txt");
            }
            Console.WriteLine("rozpoczynam testy");
            if (cki.KeyChar == '3')
            {
                for (int i = 0; i < 3*26; i+=3)
                {   
                    Console.WriteLine("Wpisz sciezke do obrazka");
                    string str = Console.ReadLine();
                    imageSupport test = new imageSupport(@str);
                      test.binarization();
                      test.btm = test.cut();
                      test.ResizeImage();
                      test.binarization();
                      test.makePointsList();
                      test.makeInterpolationList();
                      Console.WriteLine("Zakwalifikowałem litere " + i + " jako: " +tellMeWhatIsThisLetter(cls,test) );
                }
            }
            Console.ReadKey();
        }
    }
}
