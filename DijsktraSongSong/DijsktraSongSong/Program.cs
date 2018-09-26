using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace DijsktraTuanTu
{

    class Program
    {
        static int n, diemDau, diemCuoi, i = 0;
        static int[,] arr;
        static int voCung = 0;
        static int[] Len, S, P;

        class Test
        {
            private int d, c;

            public Test(int d, int c)
            {
                this.d = d;
                this.c = c;
            }

            public void KhoiGan()
            {
                for (int i = d; i < c; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j && arr[i, j] == 0)
                        {
                            arr[i, j] = voCung;
                        }
                    }
                    Len[i] = voCung;
                    S[i] = 0;
                    P[i] = diemDau;
                }
                Len[diemDau] = 0;
            }

            public void TinhLen()
            {
                for (int j = d; j < c; j++)
                {
                    if (S[j] == 0 && Len[i] + arr[i, j] < Len[j])
                    {
                        Len[j] = Len[i] + arr[i, j];
                        P[j] = i;
                    }
                }
            }

        }
        

        static void Main(string[] args)
        {
            string path = @"data1.txt";
            ReadFile(path);
            Dijsktra();
            Console.ReadKey();
        }
        public static void HienMang()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void Dijsktra()
        {
            Thread bxl1, bxl2, bxl3, bxl4;
            Test test1 = new Test(0,2);
            Test test2 = new Test(2,4);
            Test test3 = new Test(4,6);

            bxl1 = new Thread(test1.KhoiGan);
            bxl2 = new Thread(test2.KhoiGan);
            bxl3 = new Thread(test3.KhoiGan);

            bxl1.Start();
            bxl2.Start();
            bxl3.Start();

            bxl1.Join();
            bxl2.Join();
            bxl3.Join();

            while (S[diemCuoi] == 0)
            {
                for (i = 0; i < n; i++)
                {
                    if (S[i] == 0 && Len[i] < voCung)
                    {
                        break;
                    }
                }
                if (i >= n)
                {
                    Console.WriteLine("Done dijsktra"); break;
                }

                for (int j = 0; j < n; j++)
                {
                    if (S[j] == 0 && Len[i] > Len[j]) i = j;
                }

                S[i] = 1;

                bxl1 = new Thread(test1.TinhLen);
                bxl2 = new Thread(test2.TinhLen);
                bxl3 = new Thread(test3.TinhLen);

                bxl1.Start();
                bxl2.Start();
                bxl3.Start();

                bxl1.Join();
                bxl2.Join();
                bxl3.Join();
            }

            Console.WriteLine("Done dijsktra");
            Console.WriteLine("Duong di: ");
            List<int> paths = new List<int>();

            while (i != diemDau)
            {
                paths.Add(i + 1);
                i = P[i];
            }
            paths.Add(diemDau + 1);
            paths.Reverse();
            foreach (int path in paths)
            {
                Console.Write(path + "==>");
            }
        }
        public static void ReadFile(string path)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);
                n = Convert.ToInt32(reader.ReadLine());
                string[] a = reader.ReadLine().Split(' ');
                diemDau = int.Parse(a[0]) - 1;
                diemCuoi = int.Parse(a[1]) - 1;
                arr = new int[n, n];
                Len = new int[n];
                S = new int[n];
                P = new int[n];
                int i = 0, j;
                while (reader.Peek() != -1)
                {
                    j = 0;
                    a = reader.ReadLine().Split(' ');
                    foreach (string pt in a)
                    {
                        arr[i, j] = int.Parse(pt);
                        voCung += int.Parse(pt);
                        j++;
                    }
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when read file " + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
