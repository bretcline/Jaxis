using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;

namespace NCalcTest
{
    class Program
    {
        private int a = 5;
        private int b = 10;

        static void Main(string[] args)
        {
            var t = new Program();
            t.Test();

        }

        public void Test( )
        {
            var t = new Tuple<int, int, int>(10,7,100);

            var exp = new NCalc.Expression("Round((Item1/Item2)*Item3,2)");
            exp.Parameters["Item1"] = t.Item1;
            exp.Parameters["Item2"] = t.Item2;
            exp.Parameters["Item3"] = t.Item3;
            //exp.EvaluateParameter += delegate(string name, ParameterArgs args)
            //{
            //    if (name == "Item1")
            //        args.Result = t.Item1;
            //    if (name == "Item2")
            //        args.Result = t.Item2;
            //    if (name == "Item3")
            //        args.Result = t.Item3;
            //};
            Console.WriteLine( exp.Evaluate() );
        }
    }
}
