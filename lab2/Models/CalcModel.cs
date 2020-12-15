using System;
using System.Collections.Generic;
namespace lab2.Models {
    public static class CalcModel {
        public static double veryLongFunction() {
            var sum = 0.0;
            for (var i = 0.0; i < 1000000; i += 0.01) {
                sum += Math.Cos(i) * Math.Sqrt(_funcId) + 0.000001;
            }
            return sum;
        }

        static int _funcId = 0;
        public static int funcId {
            get => _funcId++;
        }
        public class FuncItem {
            public int id { get; set; }
            public double value { get; set; }
        }
        public static List<FuncItem> funcList = new List<FuncItem>();
    }
}