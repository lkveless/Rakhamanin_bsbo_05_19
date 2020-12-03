using System;
using System.Threading;
using System.Collections.Generic;

namespace ConsoleApp13
{
    class Program
    {
        static bool flag = true;
        public static Queue<int> queue = new Queue<int>();

        static void FirstGen()
        {
            Random ran1 = new Random();
            while (flag)
            {
                if ((queue.Count <= 100) & (queue.Count > 80))
                {
                    int qadd = ran1.Next(1, 100);
                    queue.Enqueue(qadd);
                    Console.WriteLine("Первый производитель добавляет элемент " + qadd);
                    Thread.Sleep(300);
                }
            }
        }

        static void SecondGen()
        {
            Random ran2 = new Random();
            while (flag)
            {
                if ((queue.Count <= 100) & (queue.Count > 80))
                {
                    int qadd = ran2.Next(1, 100);
                    queue.Enqueue(qadd);
                    Console.WriteLine("Второй производитель добавляет элемент " + qadd);
                    Thread.Sleep(300);
                }
            }
        }

        static void ThirdGen()
        {
            Random ran3 = new Random();
            while (flag)
            {
                if ((queue.Count <= 100) & (queue.Count > 80))
                {
                    int qadd = ran3.Next(1, 100);
                    queue.Enqueue(qadd);
                    Console.WriteLine("Третий производитель добавляет элемент" + qadd);
                    Thread.Sleep(300);
                }
            }
        }

        static void FirstUser()
        {
            while (flag)
            {
                queue.Dequeue();
                Console.WriteLine("Первый потребитель выводит элемент");
                Thread.Sleep(300);
            }
        }

        static void SecondUser()
        {
            while (flag)
            {
                queue.Dequeue();
                Console.WriteLine("Второй потребитель выводит элемент" + "////////////" + queue.Count);
                Thread.Sleep(300);
            }
        }

        static void Main(string[] args)
        {
            Thread gen1 = new Thread(FirstGen);
            Thread gen2 = new Thread(SecondGen);
            Thread gen3 = new Thread(ThirdGen);
            Thread user1 = new Thread(FirstUser);
            Thread user2 = new Thread(SecondUser);

            Random queueR = new Random();
            for (int i = 0; i < 200; i++)
            {
                queue.Enqueue(queueR.Next(1, 100));
            }
            
            gen1.Start();
            gen2.Start();
            gen3.Start();
            user1.Start();
            user2.Start();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Q)
                {
                    flag = false;
                    Environment.Exit(0);
                }
            }
        }              
    }         
}
