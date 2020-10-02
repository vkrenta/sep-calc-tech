using System;
using System.Threading;

namespace lab1 {

  class Program {
    public const double e = 0.01;
    public delegate double Func(double x);
    public static bool done;
    public static object locker = new object();
    public static void Go() {
      lock(locker) {
        if (!done) {
          Console.WriteLine("Done");
          done = true;
        }
      }
    }
    public static double g1(double x) {
      return x / Math.Tan(x);
    }
    public static double g2(double x) {
      return (3 * x + 5) * (2 * x * x - 1);
    }
    public static double calc(double x, Func g) {
      double temp, d;

      do {
        temp = g(x);
        d = Math.Abs(temp - x);
        x = temp;
      } while (d >= e);

      return x;
    }

    static void Main(string[] args) {
      new Thread(() => {
        var x = calc(0.5, g2);
        Console.WriteLine($"Equation 2: x = {x}, error = {e}");
      }).Start();
      new Thread(() => {
        var x = calc(0.34, g1);
        Console.WriteLine($"Equation 1: x = {x}, error = {e}");
      }).Start();

      Go();

      // new Thread(() => Go()).Start();
    }
  }
}