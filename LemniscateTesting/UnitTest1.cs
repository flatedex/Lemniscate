using Lemniscate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LemniscateTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Algorithm algorithm = new Algorithm();
            double to = 1;
            double from = -1;
            double step = 1;
            double param = 3;
            List<FunctionArg> right = new List<FunctionArg>();

            right.Add(new FunctionArg() { X = -1, Y = 0.9037 });
            right.Add(new FunctionArg() { X = -1, Y = -0.9037 });
            right.Add(new FunctionArg() { X = 0, Y = 0 });
            right.Add(new FunctionArg() { X = 0, Y = 0 });
            right.Add(new FunctionArg() { X = 1, Y = 0.9037 });
            right.Add(new FunctionArg() { X = 1, Y = -0.9037 });

            List<FunctionArg> inProgram = new List<FunctionArg>();
            inProgram = algorithm.SaveToList(from, to, step, param);

            List<double> rightList = new List<double>();
            foreach (FunctionArg x in right)
            {
                rightList.Add(x.X);
                rightList.Add(x.Y);
            }
            List<double> inProgramList = new List<double>();
            foreach (FunctionArg x in inProgram)
            {
                inProgramList.Add(x.X);
                inProgramList.Add(x.Y);
            }
            Assert.IsTrue(rightList.SequenceEqual(inProgramList));
        }
        [TestMethod]
        public void TestMethod2()
        {
            Algorithm algorithm = new Algorithm();
            double to = 1;
            double from = 0;
            double step = 1;
            double param = 0.3;
            List<FunctionArg> right = new List<FunctionArg>();

            right.Add(new FunctionArg() { X = 0, Y = 0 });
            right.Add(new FunctionArg() { X = 0, Y = 0 });

            List<FunctionArg> inProgram = new List<FunctionArg>();
            inProgram = algorithm.SaveToList(from, to, step, param);

            List<double> rightList = new List<double>();
            foreach (FunctionArg x in right)
            {
                rightList.Add(x.X);
                rightList.Add(x.Y);
            }
            List<double> inProgramList = new List<double>();
            foreach (FunctionArg x in inProgram)
            {
                inProgramList.Add(x.X);
                inProgramList.Add(x.Y);
            }
            Assert.IsTrue(rightList.SequenceEqual(inProgramList));
        }
        [TestMethod]
        public void TestMethod3()
        {
            Algorithm algorithm = new Algorithm();
            double to = -99;
            double from = -100;
            double step = 1;
            double param = 5;
            List<FunctionArg> right = new List<FunctionArg>();

            List<FunctionArg> inProgram = new List<FunctionArg>();
            inProgram = algorithm.SaveToList(from, to, step, param);

            List<double> rightList = new List<double>();
            foreach (FunctionArg x in right)
            {
                rightList.Add(x.X);
                rightList.Add(x.Y);
            }
            List<double> inProgramList = new List<double>();
            foreach (FunctionArg x in inProgram)
            {
                inProgramList.Add(x.X);
                inProgramList.Add(x.Y);
            }
            Assert.IsTrue(rightList.SequenceEqual(inProgramList));
        }

    }


}
