using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace lab3 {
    class Program {
        static decimal Taylor(decimal x) {
            var n = 9999999L;
            var sum = 1.0M;
            var value = 1.0M;
            for (var i = 1L; i <= n; i++) {
                value *= x / i;
                sum += value;
            }
            return sum;
        }
        static void TestSync() {
            var min = 0.0m;
            var max = 2.0m;
            var interval = 0.1m;
            var k = (int) ((max - min) / interval);
            var x = new decimal[k];
            var y = new decimal[k];

            var start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (var i = 0; i < k; i++) {
                var j = i;

                var value = j * interval + min;
                x[j] = value;
                y[j] = Taylor(value);
            }
            var end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            for (var i = 0; i < k; i++) {
                Console.WriteLine($"x = {x[i]}\ty = {y[i]}");
            }
            Console.WriteLine($"Execution time: {end - start}");
        }

        static void Test() {
            var min = 0.0m;
            var max = 2.0m;
            var interval = 0.1m;
            var k = (int) ((max - min) / interval);
            var x = new decimal[k];
            var y = new decimal[k];
            var tasks = new Task[k];

            var start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (var i = 0; i < k; i++) {
                var j = i;

                tasks[i] = Task.Factory.StartNew(() => {
                    var value = j * interval + min;
                    x[j] = value;
                    y[j] = Taylor(value);
                });
            }
            Task.WaitAll(tasks);
            var end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            for (var i = 0; i < k; i++) {
                Console.WriteLine($"x = {x[i]}\ty = {y[i]}");
            }
            Console.WriteLine($"Execution time: {end - start}");
        }

        public static void Main(string[] args) {
            /* var start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var value = Taylor(1);
            var end = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Console.WriteLine($"Value {value}\nTime {end - start}"); */
            TestSync();
        }
    }
}