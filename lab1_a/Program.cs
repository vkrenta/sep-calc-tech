using System;
using System.Threading;

namespace lab1_a {
    class Program {

        static Thread t1 = new Thread(() => {
            var data = ThreadTest.calc(3000, "first thread");
            Console.WriteLine(data);
        });

        static Thread t2 = new Thread(() => {
            var data = ThreadTest.calc(1000, "second thread");
            Console.WriteLine(data);
        });
        static void test1() {
            var data = ThreadTest.calc(1000, "Hello");
            Console.WriteLine(data);
            var data2 = ThreadTest.calc(2000, "Hihihi");
            Console.WriteLine(data2);
        }
        static void test2() {
            t1.Start();
            t2.Start();
        }

        static void test3() {
            t1.Start();
            t1.Join();
            t2.Start();
            t2.Join();

        }

        static object locker = new object();
        static void Go() {
            lock(locker) {
                Console.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
                Thread.Sleep(200);
                Console.WriteLine(Thread.CurrentThread.Name);
            }
        }

        static void test4() {
            for (var i = 0; i < 5; i++) {
                var thread = new Thread(Go);
                thread.Name = $"Thread {i}";
                thread.Start();
            }

        }

        static void Main(string[] args) {
            Console.WriteLine("Program started");

            test4();

            Console.WriteLine("End of sync operations");
        }
    }
}