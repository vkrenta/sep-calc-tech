using System;
namespace lab1_a {
    public class ThreadTest {
        public delegate long Now();
        public static int exp = 4;
        public static string calc(int delay, string data) {
            Now now = () => DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var start = now();
            while (now() - start <= delay);
            var finish = now();
            var result = $"[{start - Math.Floor(start / Math.Pow(10, exp)) * Math.Pow(10, exp)}]" +
                $"[Finished in: {finish - start}, " +
                $"Returned data: {data}]" +
                $"[{finish - Math.Floor(finish / Math.Pow(10, exp)) * Math.Pow(10, exp)}]";
            return result;

        }
    }
}