using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;

namespace dartboard
{
    class FindPiThread
    {
        int numDarts;
        int ct;
        Random rand;
        public FindPiThread(int nmD)
        {
            ct = 0;
            numDarts = nmD;
            rand = new Random();
        }
        public int getCount()
        {
            return ct;
        }
        public void throwDarts()
        { 
            while(numDarts > 0)
            {
                numDarts--;
                double x = rand.NextDouble();
                double y = rand.NextDouble();
                if(Math.Sqrt(x * x + y * y) <= 1)
                {
                    ct++;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string thrws;
            int numThrows;
            Console.WriteLine("How many throws should the program make?");
            thrws = Console.ReadLine();
            numThrows = Int32.Parse(thrws);

            string thrds;
            int numThreads;
            Console.WriteLine("How many threads should run?");
            thrds = Console.ReadLine();
            numThreads = Int32.Parse(thrds);

            List<Thread> threads = new List<Thread>();
            List<FindPiThread> findThreads = new List<FindPiThread>();

            for(int i = 0; i < numThreads; i++)
            {
                FindPiThread p = new FindPiThread(numThrows);
                findThreads.Add(p);
                Thread t = new Thread(new ThreadStart(p.throwDarts));
                threads.Add(t);
                t.Start();
                Thread.Sleep(16);
            }
            for(int i = 0; i < numThreads; i++)
            {
                threads[i].Join();
            }

            int count = 0;
            foreach(FindPiThread p in findThreads)
            {
                count += p.getCount();
            }

            double pi = 4 * (count / (double)(numThrows * numThreads));

            Console.WriteLine($"Pi evaluation: {pi}\n");
        }
    }
}
