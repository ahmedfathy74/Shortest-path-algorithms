using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Algo_Project
{
    class PriorityQueue
    {

        public List<KeyValuePair<double, int>> list;
        public int Count { get { return list.Count; } }

        public PriorityQueue()
        {
            list = new List<KeyValuePair<double, int>>();
        }

        public PriorityQueue(int count)
        {
            list = new List<KeyValuePair<double, int>>(count);
        }


        public void enqueue(KeyValuePair<double, int> x)
        {
            list.Add(x);
            int i = Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (list[p].Key <= x.Key) break;

                list[i] = list[p];
                i = p;
            }

            if (Count > 0) list[i] = x;
        }

        public KeyValuePair<double, int> dequeue()
        {
             KeyValuePair<double, int> min = first();
            KeyValuePair<double, int> root = list[Count - 1];
            list.RemoveAt(Count - 1);

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < Count && list[b].Key < list[a].Key ? b : a;

                if (list[c].Key >= root.Key) break;
                list[i] = list[c];
                i = c;
            }

            if (Count > 0) list[i] = root;
            return min;
        }

        public KeyValuePair<double, int> first()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");
            return list[0];
        }

        public void Clear()
        {
            list.Clear();
        }



    }
}
