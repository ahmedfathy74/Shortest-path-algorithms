using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace Final_Algo_Project
{
    class Program
    {
        static void Main(string[] args)
        {

            string map="";
            string querie = "";
            while (true)
            {
                Console.Write("Please Enter Map File Number : ");
                map = Console.ReadLine();
                Console.Write("Please Enter querie File Number : ");
                querie = Console.ReadLine();

                if (map == "1")
                    map = "map1.txt";
                else if (map == "2")
                    map = "map2.txt";
                else
                {
                    map = "SFMap.txt";
                }

                if (querie == "1")
                {
                    querie = "queries1.txt";
                    break;
                }
                else if (querie == "2")
                {
                    querie = "queries2.txt";
                    break;
                }
                else
                {
                    querie = "SFQueries.txt";
                    break;
                }
            }


            //get number of node
            FileStream fs = new FileStream(map, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string scount = sr.ReadLine();
            int ncount = Int32.Parse(scount);
            fs.Close();
            sr.Close();
            ///-----------------------------------------



            retrieve_data rvd = new retrieve_data();

            //list of node
            var nodedata = new List<KeyValuePair<int, KeyValuePair<double, double>>>();
            ////array of intersections 
            int s = ncount + 2;
            var mainarr = new List<KeyValuePair<double, int>>[s];
            List<KeyValuePair<KeyValuePair<int, int>, double>> Mylist = new List<KeyValuePair<KeyValuePair<int, int>, double>>();

            rvd.Construct(map, ref nodedata, ref mainarr,ref Mylist);

            var arr = new List<KeyValuePair<double, int>>[s];




            //--------------------------------------------


            //list of source
            var source = new List<KeyValuePair<double, double>>();
            //list of destination
            var destination = new List<KeyValuePair<double, double>>();
            // list of radius
            var R=new List<double>();
            //loading data
            rvd.load(querie,ref source, ref destination, ref R);



            //-------------------------------------------
            fs = new FileStream("output.txt",FileMode.Truncate);
            StreamWriter swr = new StreamWriter(fs);
            

            MyFunc func = new MyFunc();
            for (int z = 0; z < R.Count; z++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                func.eq(mainarr, ref arr);
                double r = R[z];
                func.SDasnode(source, destination, ref arr, r, nodedata, z);
                int to = arr.Length;
                int[] way = new int[to];
                to = to - 1;
                double shortest = func.dijkstra(0, to, ref way, arr);
                Console.WriteLine();
                Console.WriteLine();
                shortest = Math.Round(shortest, 2);
                Console.WriteLine("Time = " + shortest + " mins");
                swr.WriteLine("Time = " + shortest + " mins");



                int k = to;
                Stack<int> S = new Stack<int>();
                while (k != 0)
                {
                    S.Push(way[k] - 1);
                    k = way[k];
                }
                S.Pop();
                int[] temp = new int[S.Count];
                int index = 0;
                int start = S.Peek();
                int end = -1;
                Console.Write("Path = Source -> ");
                swr.Write("Path = Source -> ");
                while (S.Count != 0)
                {
                    end = S.Peek();
                    temp[index] = end;
                    index++;
                    Console.Write(" " + end + " -> ");
                    swr.Write(" " + end + " -> ");
                    S.Pop();
                }
                Console.WriteLine("Destination");
                swr.WriteLine("Destination");

                double walkdistance = func.walking(arr, start, end, to);
                double car = func.car(map, temp,Mylist);
                double total = walkdistance + car;
                walkdistance = Math.Round(walkdistance,2);
                car=Math.Round(car,2);
                total=Math.Round(total,2);


                Console.WriteLine("Distance = " + total + "Km");
                swr.WriteLine("Distance = " + total + " Km");

                Console.WriteLine("Walking Distance = " + walkdistance + " Km");
                swr.WriteLine("Walking Distance = " + walkdistance + " Km");

                Console.WriteLine("Vehicle Distance = " + car + " Km");
                swr.WriteLine("Vehicle Distance = " + car + " Km");

                sw.Stop();
                Console.WriteLine("the execution time (ms) = " + sw.ElapsedMilliseconds);
                swr.WriteLine("the execution time (ms) = " + sw.ElapsedMilliseconds);

                swr.WriteLine();
            }

            swr.Close();
            fs.Close();
        }
    }
}
