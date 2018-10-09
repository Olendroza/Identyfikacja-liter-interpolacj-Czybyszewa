using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projekt_studia
{
    class classifier
    {
        public List<List<Int64>> Patterns = new List<List<Int64>>(); //0-a,1-o,2-u na koncu listy mam ilosc elementow uzytych do wzoru

        public classifier()
        {
            for(int i =1;i<27;i++)
            {
                List<Int64> p = new List<Int64>();
                for (int j = 0; j < 258; j++)
                    p.Add(0);
                Patterns.Add(p);
            }
        }
        public void savePatterns()
        {
            using (StreamWriter sw = new StreamWriter(@"C:\\Users\\Piotr\\Documents\\projekty vs2017\\Projektstudia\\Projekt studia\wz.txt")) // tu jest sciezka
            {
                foreach(var item in Patterns)
                {
                    foreach(var item2 in item)
                    {
                        sw.Write(item2+",");
                    }
                    sw.WriteLine();
                }
               
            }
            
            
        }
        public void loadPatterns()// poprawic
        {
            string[] tab = File.ReadAllLines(@"C:\\Users\\Piotr\\Documents\\projekty vs2017\\Projektstudia\\Projekt studia\wz.txt");
            foreach(var item in tab)
            {
                string[]tab2=item.Split(',');
                List<Int64> p = new List<Int64>();
                for(int i =0; i<tab2.Length-1;i++)
                {
                    p.Add(Convert.ToInt64(tab2[i]));
                }
                Patterns.Add(p);
            }
            

        }
        public void makePattern(List<Int64> interpolationLists, int letter)
        {
            Int64 n = Patterns[letter].Count - 1;
            for (int i = 1; i < interpolationLists.Count-1; i++)
            {
                Patterns[letter][i] = Patterns[letter][i] * (n / n + 1) + interpolationLists[i] * (1 / n + 1);   
            }

        }
        public string getLetter(Int64 index)
        {
            byte[] bytes = BitConverter.GetBytes(97 + index);
            string letter = Encoding.Unicode.GetString(bytes);

            return letter;
        }
        public string classify(List<Int64> letter)
        {
            List<Int64> neighbours = new List<Int64>(Patterns.Count);
            List<double> distance = new List<double>(Patterns.Count);
            for (int i = 0; i < neighbours.Count; i++)
            {
                neighbours[i] = 0;
            }
            for (int i = 0; i < letter.Count; i++)
            {
                for (int j = 0; j < distance.Count; j++)
                {
                    distance[i] = Math.Abs(letter[i] - Patterns[j][i]);
                }
                double min = distance.Min();
                for (int j = 0; j < distance.Count; j++)
                {
                    if (distance[j] == min)
                        neighbours[j]++;
                }
            }
            // elminacja podobienstwa do dwoch i wiecej liter
            //znajdz najwiekszy
            Int64 max = neighbours.Max();
            //policz ile podobienstw
            int count = -1;
            for(int i=0;i<neighbours.Count;i++)
            {
                if (neighbours[i] == max)
                    count++;
            }
            if (count != 0)
            {
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (neighbours[i] == max)
                        count++;
                }
                //znajdz indeksy nawiekszych i dodaj je do listy
                List<int> similarLetters = new List<int>(count);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (neighbours[i] == max)
                        similarLetters.Add(i);
                }
                //zerowac sasiadow i distance
                for (int i = 0; i < neighbours.Count; i++)
                {
                    neighbours[i] = 0;
                    distance[i] = 1000;
                }
                //znalesc dystanse od obiektow z listy
                for (int i = 0; i < letter.Count; i++)
                {
                    for (int j = 0; j < similarLetters.Count; j++)
                    {
                        distance[similarLetters[j]] = Math.Abs(letter[i] - Patterns[similarLetters[j]][i]);

                    }
                    double min = distance.Min();
                    for (int j = 0; j < distance.Count; j++)
                    {
                        if (distance[j] == min)
                            neighbours[j]++;
                    }
                }
                Int64 trueNeighbour = neighbours.Max();
                return this.getLetter(trueNeighbour);
            }
            else
            {
                return this.getLetter(max);
            }
            

        }
        
    }
}
