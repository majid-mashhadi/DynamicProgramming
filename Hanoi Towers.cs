using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming
{
    class Hanoi_Towers
    {
        private class Tower
        {
            int index;
            Stack<int> disks;
            public Tower(int index)
            {
                this.index = index;
                disks = new Stack<int>();
            }

            public bool push(int data)
            {
                if (disks.Count != 0 && disks.Peek() <= data)
                {
                    Console.WriteLine("Error Placing Disk " + data.ToString());
                    return false;
                }
                else
                {
                    disks.Push(data);
                    return true;
                }
            }

            public int pop()
            {
                if (disks.Count == 0) return -1;
                return disks.Pop();
            }

            public void moveTopTo(Tower destination)
            {
                destination.push(pop());
            }

            public void moveDisks(int n, Tower destination, Tower buffer)
            {
                if (n > 0)
                {
                    this.moveDisks(n - 1,  buffer, destination);
                    this.moveTopTo(destination);
                    buffer.moveDisks(n - 1, destination, this);
                }
            }

            public void print()
            {
                Console.WriteLine("Contents of Tower " + index.ToString());
                for (int i = disks.Count() - 1; i >= 0; i--)
                {
                    Console.WriteLine(" " + disks.ElementAt(i));
                }
            }
        }

        public void solve()
        {
            int diskCount =5;

            Tower[] Towers = new Tower[3];
            Towers[0] = new Tower(0);
            Towers[1] = new Tower(1);
            Towers[2] = new Tower(2);
            for (int i = diskCount - 1; i >= 0; i--)
            {
                Towers[0].push(i);
            }

            Towers[0].print();

            Towers[0].moveDisks(diskCount, Towers[1], Towers[2]);

            Towers[0].print();
            Towers[1].print();
            Towers[2].print();
        }
    }
}
