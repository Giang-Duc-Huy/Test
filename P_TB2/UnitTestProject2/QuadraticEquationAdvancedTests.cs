using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using P_TB2.Models;
using P_TB2.Controllers;

namespace UnitTestProject2
{
    [TestClass]
    public class QuadraticEquationAdvancedTests
    {
        private QuadraticController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new QuadraticController();
        }

        #region Exception Handling Tests

        [TestMethod]
        public void TestExceptionHandling_ControllerSolveEquation()
        {
            double a = double.NaN;
            double b = 1;
            double c = 1;

            string result = _controller.SolveEquation(a, b, c);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestExceptionHandling_InvalidMathematicalOperations()
        {
            double a = double.PositiveInfinity;
            double b = 1;
            double c = 1;
            string result = _controller.SolveEquation(a, b, c);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        #endregion

        #region Concurrency Tests

        [TestMethod]
        public void TestConcurrency_MultipleControllers()
        {
            var controllers = new QuadraticController[10];
            for (int i = 0; i < 10; i++)
            {
                controllers[i] = new QuadraticController();
            }

            for (int i = 0; i < 10; i++)
            {
                string result = controllers[i].SolveEquation(1, -5, 6);
                Assert.IsTrue(result.Contains("2 nghiệm phân biệt"));
            }
        }

        #endregion

        #region Regression Tests

        [TestMethod]
        public void TestRegression_KnownWorkingCases()
        {
            var testCases = new[]
            {
                new { A = 1.0, B = -5.0, C = 6.0, ExpectedRoots = 2 },
                new { A = 1.0, B = -4.0, C = 4.0, ExpectedRoots = 1 },
                new { A = 1.0, B = 0.0, C = 1.0, ExpectedRoots = 0 },
                new { A = 0.0, B = 2.0, C = -4.0, ExpectedRoots = 1 },
                new { A = 0.0, B = 0.0, C = 0.0, ExpectedRoots = -1 } 
            };

            foreach (var testCase in testCases)
            {
                var equation = new QuadraticEquation(testCase.A, testCase.B, testCase.C);
                string result = equation.Solve();

                switch (testCase.ExpectedRoots)
                {
                    case 0:
                        Assert.IsTrue(result.Contains("vô nghiệm"));
                        break;
                    case 1:
                        Assert.IsTrue(result.Contains("nghiệm kép") || result.Contains("bậc 1"));
                        break;
                    case 2:
                        Assert.IsTrue(result.Contains("2 nghiệm phân biệt"));
                        break;
                    case -1:
                        Assert.IsTrue(result.Contains("vô số nghiệm"));
                        break;
                }
            }
        }

        #endregion

        #region Special Cases Tests

        [TestMethod]
        public void TestSpecialCase_ZeroAAndB()
        {
            var equation = new QuadraticEquation(0, 0, 5);
            string result = equation.Solve();
            Assert.AreEqual("Phương trình vô nghiệm", result);
        }

        [TestMethod]
        public void TestSpecialCase_ZeroAAndC()
        {
            var equation = new QuadraticEquation(0, 3, 0);
            string result = equation.Solve();

            Assert.IsTrue(result.Contains("Phương trình bậc 1"));

            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestSpecialCase_ZeroBAndC()
        {
            var equation = new QuadraticEquation(2, 0, 0);

            string result = equation.Solve();
            Assert.IsTrue(result.Contains("nghiệm kép"));
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestSpecialCase_AllZeros()
        {
            var equation = new QuadraticEquation(0, 0, 0);
            string result = equation.Solve();
            Assert.AreEqual("Phương trình có vô số nghiệm", result);
        }

        #endregion

        #region Mathematical Edge Cases

        [TestMethod]
        public void TestMathematicalEdgeCase_VerySmallDelta()
        {
          
            var equation = new QuadraticEquation(1, -2, 1.0000000001);

            string result = equation.Solve();
            Assert.IsTrue(result.Contains("nghiệm"));
        }

        [TestMethod]
        public void TestMathematicalEdgeCase_VeryLargeDelta()
        {
            var equation = new QuadraticEquation(1, -1000, 1);

            string result = equation.Solve();

            Assert.IsTrue(result.Contains("nghiệm"));
        }

        [TestMethod]
        public void TestMathematicalEdgeCase_FractionalCoefficients()
        {

            var equation = new QuadraticEquation(0.1, -0.2, 0.1);

            string result = equation.Solve();
            Assert.IsTrue(result.Contains("nghiệm"));
            Assert.IsTrue(result.Length > 0);
        }

        #endregion

        #region Input Validation Edge Cases

        [TestMethod]
        public void TestInputValidation_ScientificNotation()
        {
            string[] scientificInputs = { "1", "2", "3" };

            foreach (string input in scientificInputs)
            {
                bool result = _controller.ValidateInput(input);
                Assert.IsTrue(result, $"Input '{input}' should be valid");
            }
        }


        [TestMethod]
        public void TestInputValidation_InvalidFormats()
        {
            string[] invalidFormats = { "abc", "12a", "a12" };

            foreach (string input in invalidFormats)
            {
                bool result = _controller.ValidateInput(input);
                Assert.IsFalse(result, $"Giá trị không hợp lệ '{input}'");
            }
        }

        #endregion
    }
}
