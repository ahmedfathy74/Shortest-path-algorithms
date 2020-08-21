using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Final_Algo_Project
{
    class MyFunc
    {
        public double dijkstra(int source, int destination, ref int[] way, List<KeyValuePair<double, int>>[] arr)
        {
            PriorityQueue pq = new PriorityQueue();
            int count = arr.Length;
            double[] dis = new double[count];
            bool[] boolean = new bool[count];
            for (int i = 0; i < count; i++)
            {
                dis[i] = double.MaxValue;
                boolean[i] = false;
            }
            dis[0] = 0;

            pq.enqueue(new KeyValuePair<double, int>(0, 0));

            while (pq.Count != 0)
            {
                KeyValuePair<double, int> temp = new KeyValuePair<double, int>();
                temp = pq.first();
                pq.dequeue();
                int now = temp.Value;
                double weig = temp.Key;
                if (now == count - 1)
                {
                    break;
                }
                if (temp.Value == count - 1)
                {
                    dis[count - 1] = weig ;
                    break;
                }
                if (boolean[now] == false)
                {
                    for (int i = 0; i < arr[now].Count; i++)
                    {
                        if (weig + arr[now][i].Key < dis[arr[now][i].Value])
                        {

                            dis[arr[now][i].Value] = weig + arr[now][i].Key;
                            pq.enqueue(new KeyValuePair<double, int>(dis[arr[now][i].Value], arr[now][i].Value));
                            way[arr[now][i].Value] = now;
                        }
                    }
                    boolean[now] = true;
                }

            }
            return dis[count - 1];
        }

        public void SDasnode(List<KeyValuePair<double, double>> source, List<KeyValuePair<double, double>> destination, ref List<KeyValuePair<double, int>>[] arr, double r, List<KeyValuePair<int, KeyValuePair<double, double>>> nodedata, int index)
        {
            arr[0] = new List<KeyValuePair<double, int>>();
            r = r / 1000;
            double x1 = source[index].Key;
            double y1 = source[index].Value;

            double d, x2, y2;
            int node;
            for (int i = 0; i < nodedata.Count; i++)
            {
                node = nodedata[i].Key;
                x2 = nodedata[i].Value.Key;
                y2 = nodedata[i].Value.Value;
                d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                if (d <= r)
                {
                    double time = (d / 5) * 60;
                    arr[0].Add(new KeyValuePair<double, int>(time, node + 1));
                    arr[node + 1].Add(new KeyValuePair<double, int>(time, 0));
                }
            }

            x1 = destination[index].Key;
            y1 = destination[index].Value;
            int distnationnode = nodedata.Count + 1;
            arr[distnationnode] = new List<KeyValuePair<double, int>>();
            for (int i = 0; i < nodedata.Count; i++)
            {
                node = nodedata[i].Key;
                x2 = nodedata[i].Value.Key;
                y2 = nodedata[i].Value.Value;
                d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                if (d <= r)
                {
                    double time = (d / 5) * 60;
                    arr[distnationnode].Add(new KeyValuePair<double, int>(time, node + 1));
                    arr[node + 1].Add(new KeyValuePair<double, int>(time, distnationnode));
                }
            }
            return;
        }

        public double walking(List<KeyValuePair<double, int>>[] arr, int start, int end, int to)
        {
            double distance = 0;
            double distance2 = 0;

            for (int i = 0; i < arr[0].Count; i++)
            {
                if (arr[0][i].Value == start + 1)
                {
                    distance = (arr[0][i].Key);
                    distance = distance / 60;
                    distance = distance * 5;
                    break;
                }
            }

            for (int i = 0; i < arr[to].Count; i++)
            {
                if (arr[to][i].Value == end + 1)
                {
                    distance2 = distance2 + (arr[to][i].Key);
                    distance2 = distance2 / 60;
                    distance2 = distance2 * 5;
                    break;
                }
            }
            double finaldistance = distance + distance2;
            return finaldistance;
        }

        public double car(string map, int[] temp, List<KeyValuePair<KeyValuePair<int, int>, double>> Mylist)
        {
            double total = 0;             
            int node1; 
            int node2;
            double dist;
            for (int i = 0; i < Mylist.Count; i++)
            {
                node1 = Mylist[i].Key.Key;
                node2 = Mylist[i].Key.Value;
                dist = Mylist[i].Value;
                for (int j = 0; j < temp.Length-1; j++)
                {
                    if ((node1 == temp[j] && node2 == temp[j + 1]) || (node2 == temp[j] && node1 == temp[j+1]))
                    {
                        total=total+dist;
                    }
                }

            }
            return total;
        }


        public void eq(List<KeyValuePair<double, int>>[] mainarr, ref List<KeyValuePair<double, int>>[] arr)
        {
            for (int i = 0; i < mainarr.Length; i++)
            {
                arr[i] = new List<KeyValuePair<double, int>>();
                for (int j = 0; j < mainarr[i].Count; j++)
                {
                    double weig = mainarr[i][j].Key;
                    int to = mainarr[i][j].Value;
                    arr[i].Add(new KeyValuePair<double, int>(weig, to));
                }
            }
            return;
        }


    }
}
