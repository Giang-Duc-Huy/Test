using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using P_TB2.Models;
using P_TB2.Controllers;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        private QuadraticController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new QuadraticController();
        }

        #region Test QuadraticEquation Model

        [TestMethod]
        public void TestQuadraticEquation_Constructor()
        {
            var equation = new QuadraticEquation(1, 2, 3);
            Assert.AreEqual(1, equation.A);
            Assert.AreEqual(2, equation.B);
            Assert.AreEqual(3, equation.C);
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_TwoDistinctRoots()
        {
            var equation = new QuadraticEquation(1, -5, 6);

            string result = equation.Solve();
            Assert.IsTrue(result.Contains("2 nghiệm phân biệt"));
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_OneRoot()
        {
            var equation = new QuadraticEquation(1, -4, 4);
            string result = equation.Solve();
            Assert.IsTrue(result.Contains("nghiệm kép"));
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_NoRealRoots()
        {
            var equation = new QuadraticEquation(1, 0, 1);
            string result = equation.Solve();
            Assert.AreEqual("Phương trình vô nghiệm", result);
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_LinearEquation()
        {
            var equation = new QuadraticEquation(0, 2, -4);
            string result = equation.Solve();

            Assert.IsTrue(result.Contains("Phương trình bậc 1"));
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_ConstantZero()
        {
            var equation = new QuadraticEquation(0, 0, 0);
            string result = equation.Solve();
            Assert.AreEqual("Phương trình có vô số nghiệm", result);
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_ConstantNonZero()
        {
            var equation = new QuadraticEquation(0, 0, 5);
            string result = equation.Solve();

            Assert.AreEqual("Phương trình vô nghiệm", result);
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_ComplexRoots()
        {
            var equation = new QuadraticEquation(1, 2, 5);

            string result = equation.Solve();

            Assert.AreEqual("Phương trình vô nghiệm", result);
        }

        [TestMethod]
        public void TestQuadraticEquation_IsValidInput()
        {
            var equation = new QuadraticEquation(1, 2, 3);

            bool result = equation.IsValidInput();

         
            Assert.IsTrue(result);
        }

        #endregion

        #region Test QuadraticController

        [TestMethod]
        public void TestQuadraticController_SolveEquation_TwoDistinctRoots()
        {
            // Arrange
            double a = 1, b = -5, c = 6;

            // Act
            string result = _controller.SolveEquation(a, b, c);

            // Assert
            Assert.IsTrue(result.Contains("2 nghiệm phân biệt"));
            // Chỉ kiểm tra có kết quả
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestQuadraticController_SolveEquation_OneRoot()
        {
            // Arrange
            double a = 1, b = -4, c = 4;

            // Act
            string result = _controller.SolveEquation(a, b, c);

            // Assert
            Assert.IsTrue(result.Contains("nghiệm kép"));
            // Chỉ kiểm tra có kết quả
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestQuadraticController_SolveEquation_NoRealRoots()
        {
            // Arrange
            double a = 1, b = 0, c = 1;

            // Act
            string result = _controller.SolveEquation(a, b, c);

            // Assert
            Assert.AreEqual("Phương trình vô nghiệm", result);
        }

        [TestMethod]
        public void TestQuadraticController_SolveEquation_LinearEquation()
        {
            // Arrange
            double a = 0, b = 2, c = -4;

            // Act
            string result = _controller.SolveEquation(a, b, c);

            // Assert
            Assert.IsTrue(result.Contains("Phương trình bậc 1"));
            // Chỉ kiểm tra có kết quả
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestQuadraticController_ValidateInput_ValidNumber()
        {
            // Arrange
            string input = "3.14";

            // Act
            bool result = _controller.ValidateInput(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestQuadraticController_ValidateInput_InvalidNumber()
        {
            // Arrange
            string input = "abc";

            // Act
            bool result = _controller.ValidateInput(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestQuadraticController_ValidateInput_EmptyString()
        {
            // Arrange
            string input = "";

            // Act
            bool result = _controller.ValidateInput(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestQuadraticController_ValidateInput_NullString()
        {
            // Arrange
            string input = null;

            // Act
            bool result = _controller.ValidateInput(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestQuadraticController_ClearResult()
        {
            // Act
            string result = _controller.ClearResult();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        #endregion

        #region Test Edge Cases

        [TestMethod]
        public void TestQuadraticEquation_Solve_VeryLargeNumbers()
        {
    
            var equation = new QuadraticEquation(1e10, 1e10, 1e10);

         
            string result = equation.Solve();

     
            Assert.IsTrue(result.Contains("Phương trình vô nghiệm") || 
                         result.Contains("nghiệm") || 
                         result.Contains("Phương trình bậc 1"));
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_VerySmallNumbers()
        {
       
            var equation = new QuadraticEquation(1e-10, 1e-10, 1e-10);

        
            string result = equation.Solve();

        
            Assert.IsTrue(result.Contains("nghiệm") || 
                         result.Contains("Phương trình bậc 1") ||
                         result.Contains("Phương trình vô nghiệm"));
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_NegativeCoefficients()
        {
            var equation = new QuadraticEquation(-1, -2, -3);
            string result = equation.Solve();

            Assert.IsTrue(result.Contains("nghiệm") || 
                         result.Contains("Phương trình vô nghiệm"));
        }

        [TestMethod]
        public void TestQuadraticEquation_Solve_ZeroBAndC()
        {
            var equation = new QuadraticEquation(1, 0, 0);

            string result = equation.Solve();
            Assert.IsTrue(result.Contains("nghiệm kép"));
            Assert.IsTrue(result.Length > 0);
        }

        #endregion

        #region Test Integration

        [TestMethod]
        public void TestIntegration_CompleteWorkflow()
        {
            // Arrange
            string inputA = "1";
            string inputB = "-5";
            string inputC = "6";

            // Act
            bool isValidA = _controller.ValidateInput(inputA);
            bool isValidB = _controller.ValidateInput(inputB);
            bool isValidC = _controller.ValidateInput(inputC);

            string result = "";
            if (isValidA && isValidB && isValidC)
            {
                result = _controller.SolveEquation(
                    double.Parse(inputA), 
                    double.Parse(inputB), 
                    double.Parse(inputC)
                );
            }

            // Assert
            Assert.IsTrue(isValidA);
            Assert.IsTrue(isValidB);
            Assert.IsTrue(isValidC);
            Assert.IsTrue(result.Contains("2 nghiệm phân biệt"));
            // Chỉ kiểm tra có kết quả
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestIntegration_InvalidInputWorkflow()
        {
            // Arrange
            string inputA = "1";
            string inputB = "abc";
            string inputC = "6";

            // Act
            bool isValidA = _controller.ValidateInput(inputA);
            bool isValidB = _controller.ValidateInput(inputB);
            bool isValidC = _controller.ValidateInput(inputC);

            // Assert
            Assert.IsTrue(isValidA);
            Assert.IsFalse(isValidB);
            Assert.IsTrue(isValidC);
        }

        #endregion

        #region Test Mathematical Accuracy

        [TestMethod]
        public void TestMathematicalAccuracy_PerfectSquare()
        {
            var equation = new QuadraticEquation(1, -6, 9);

            string result = equation.Solve();

            Assert.IsTrue(result.Contains("nghiệm kép"));
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestMathematicalAccuracy_StandardForm()
        {
            var equation = new QuadraticEquation(1, -7, 12);
            string result = equation.Solve();

            Assert.IsTrue(result.Contains("2 nghiệm phân biệt"));
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestMathematicalAccuracy_DecimalCoefficients()
        {
            var equation = new QuadraticEquation(0.5, -1.5, 1);
            string result = equation.Solve();

            Assert.IsTrue(result.Contains("2 nghiệm phân biệt"));
            Assert.IsTrue(result.Length > 0);
        }

        #endregion

        #region Test Input Validation

        [TestMethod]
        public void TestInputValidation_VariousFormats()
        {
            string[] validInputs = { "123", "-123", "123.456", "-123.456", "0", "0.0" };
            string[] invalidInputs = { "abc", "12a", "a12", "", " ", null };

            foreach (string input in validInputs)
            {
                bool result = _controller.ValidateInput(input);
                Assert.IsTrue(result, $"Input '{input}' phải hợp lệ");
            }

            foreach (string input in invalidInputs)
            {
                bool result = _controller.ValidateInput(input);
                Assert.IsFalse(result, $"Input '{input}' phải không hợp lệ");
            }
        }

        [TestMethod]
        public void TestInputValidation_BoundaryValues()
        {
            Assert.IsTrue(_controller.ValidateInput("123"));
            Assert.IsTrue(_controller.ValidateInput("-123"));
            Assert.IsTrue(_controller.ValidateInput("0"));
        }

        #endregion
    }
}
