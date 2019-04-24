using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math_functions;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Sum()
        {
            Assert.AreEqual(0, Mathematic_functions.Sum(0, 0));
            Assert.AreEqual(4, Mathematic_functions.Sum(2, 2));
            Assert.AreEqual(-16, Mathematic_functions.Sum(-8, -8));
            Assert.AreEqual(-5, Mathematic_functions.Sum(-10, 5));
            Assert.AreEqual(7.3, Mathematic_functions.Sum(2.5, 4.8));
        }

        [TestMethod]
        public void Min()
        {
            Assert.AreEqual(0, Mathematic_functions.Min(0, 0));
            Assert.AreEqual(0, Mathematic_functions.Min(2, 2));
            Assert.AreEqual(0, Mathematic_functions.Min(-8, -8));
            Assert.AreEqual(-15, Mathematic_functions.Min(-10, 5));
            Assert.AreEqual(-2.3, Mathematic_functions.Min(2.5, 4.8));
        }

        [TestMethod]
        public void Mult()
        {
            Assert.AreEqual(0, Mathematic_functions.Mult(0, 0));
            Assert.AreEqual(4, Mathematic_functions.Mult(2, 2));
            Assert.AreEqual(64, Mathematic_functions.Mult(-8, -8));    
            Assert.AreEqual(-50, Mathematic_functions.Mult(-10, 5));   
            Assert.AreEqual(12, Mathematic_functions.Mult(2.5, 4.8));
        }

        [TestMethod]
        public void Div()
        {
            /// delenie nulov
            Assert.AreEqual(0, Mathematic_functions.Div(8, 0));
            Assert.AreEqual(0, Mathematic_functions.Div(0, 0));
            Assert.AreEqual(1, Mathematic_functions.Div(2, 2));
            Assert.AreEqual(1, Mathematic_functions.Div(-8, -8));    
            Assert.AreEqual(-2, Mathematic_functions.Div(-10, 5));   
            Assert.AreEqual(1.92, Mathematic_functions.Div(4.8, 2.5));
        }

        [TestMethod]
        public void Fact()
        {
            /// faktorial z desatinneho cisla
            try
            {
                Mathematic_functions.Fact(8.5);
                Assert.Fail("factorial of decimal number");
            }
            catch (Exception)
            {
            }
            Assert.AreEqual(0, Mathematic_functions.Fact(0));
            Assert.AreEqual(24, Mathematic_functions.Fact(4));
        }

        [TestMethod]
        public void Pow()
        {
            Assert.AreEqual(0, Mathematic_functions.Pow(0));
            Assert.AreEqual(25, Mathematic_functions.Pow(5));
            Assert.AreEqual(64, Mathematic_functions.Pow(8));
        }

        [TestMethod]
        public void Pow_n()
        {
            Assert.AreEqual(0, Mathematic_functions.Pow_n(0, 0));
            Assert.AreEqual(25, Mathematic_functions.Pow_n(5, 2));
            Assert.AreEqual(16, Mathematic_functions.Pow_n(2, 4));
        }

        [TestMethod]
        public void Log10()
        {
            Assert.AreNotEqual(0, Mathematic_functions.Log10(0));
            Assert.AreEqual(1, Mathematic_functions.Log10(10));
        }

        [TestMethod]
        public void Sqrt()
        {
            Assert.AreEqual(8, Mathematic_functions.Sqrt(64));
            Assert.AreEqual(0, Mathematic_functions.Sqrt(0));
            Assert.AreEqual(10, Mathematic_functions.Sqrt(100));
        }
        [TestMethod]
        public void Sin()
        {
            Assert.AreEqual(0, Mathematic_functions.Sin(0));
            Assert.AreNotEqual(10, Mathematic_functions.Sin(100));
        }
    }
}
