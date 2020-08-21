using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Final_Algo_Project
{
    class retrieve_data
    {
        private
        int node,node2;
        double r,xnode,ynode,xnode2,ynode2,speed,dist,time;
        public retrieve_data()
        {
            r = 0;
            node = -1;
            node2 = -1;
            xnode = -1;
            ynode = -1;
            xnode2 = -1;
            ynode2 = -1;
            speed = -1;
            dist = -1;
            time = -1;
        }



        public void Construct(string map, ref List<KeyValuePair<int, KeyValuePair<double, double>>> nodedata,
                              ref List<KeyValuePair<double, int>>[] arr,ref List<KeyValuePair<KeyValuePair<int, int>, double>> Mylist)
        {
            
            FileStream fs = new FileStream(map, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string scount;
            int ncount;

            while (sr.Peek() != -1)
            {
                //number of node
                scount = sr.ReadLine();
                ncount = Int32.Parse(scount);

                //loop to load Data of each node
                for (int i = 0; i < ncount; i++)
                {
                    scount = sr.ReadLine();
                    string[] rec = scount.Split(' ');
                    node = Int32.Parse(rec[0]);
                    xnode = double.Parse(rec[1]);
                    ynode = double.Parse(rec[2]);
                    nodedata.Add(new KeyValuePair<int, KeyValuePair<double, double>>(node, new KeyValuePair<double, double>(xnode, ynode)));
                }


                //loop to initialize array of intersections
                arr = new List<KeyValuePair<double, int>>[ncount + 2];
                for (int i = 0; i < ncount + 2; i++)
                {
                    arr[i] = new List<KeyValuePair<double, int>>();
                }

                //number of intersections
                scount = sr.ReadLine();
                ncount = Int32.Parse(scount);

                //loop to load Data of each intersection
                for (int i = 0; i < ncount; i++)
                {
                    scount = sr.ReadLine();
                    string[] rec = scount.Split(' ');
                    node = Int32.Parse(rec[0]);
                    node = node + 1;
                    node2 = Int32.Parse(rec[1]);
                    node2 = node2 + 1;
                    dist = double.Parse(rec[2]);
                    speed = double.Parse(rec[3]);
                    time = (dist / speed) * 60;
                    arr[node].Add(new KeyValuePair<double, int>(time, node2));
                    arr[node2].Add(new KeyValuePair<double, int>(time, node));
                    Mylist.Add(new KeyValuePair<KeyValuePair<int, int>, double>(new KeyValuePair<int, int>(node - 1, node2 - 1), dist));
                }
            }
            fs.Close();
            sr.Close();
        }



        public void load(string querie,ref List<KeyValuePair<double, double>> source, 
            ref List<KeyValuePair<double, double>> distnation, ref List<double> R)
        {
            FileStream fs = new FileStream(querie, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string scount;
            int ncount;
            while (sr.Peek() != -1)
            {
                //number of queries
                scount = sr.ReadLine();
                ncount = Int32.Parse(scount);
                //loop to load queries
                for (int i = 0; i < ncount; i++)
                {
                    scount = sr.ReadLine();
                    string[] rec = scount.Split(' ');
                    xnode = float.Parse(rec[0]);
                    ynode = float.Parse(rec[1]);
                    xnode2 = float.Parse(rec[2]);
                    ynode2 = float.Parse(rec[3]);
                    r = double.Parse(rec[4]);
                    source.Add(new KeyValuePair<double, double>(xnode, ynode));
                    distnation.Add(new KeyValuePair<double, double>(xnode2, ynode2));
                    R.Add(r);
                }
            }
            fs.Close();
            sr.Close();
        }




    }
}
