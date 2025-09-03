using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_TB2.Controllers;
using P_TB2.Models;

namespace UnitTestProject2
{
    [TestClass]
    public class IntegrationWithStubTests
    {
        private class StubSolver : IQuadraticSolver
        {
            private readonly string _response;
            public double LastA { get; private set; }
            public double LastB { get; private set; }
            public double LastC { get; private set; }

            public StubSolver(string response)
            {
                _response = response;
            }

            public string Solve(double a, double b, double c)
            {
                LastA = a;
                LastB = b;
                LastC = c;
                return _response;
            }
        }

        private class ThrowingStubSolver : IQuadraticSolver
        {
            public string Solve(double a, double b, double c)
            {
                throw new System.InvalidOperationException("Stubbed failure");
            }
        }

        [TestMethod]
        public void Controller_Uses_StubSolver_Result_Is_Propagated()
        {
            var stub = new StubSolver("Stubbed Result");
            var controller = new QuadraticController(stub);

            string result = controller.SolveEquation(1, -3, 2);

            Assert.AreEqual("Stubbed Result", result);
            Assert.AreEqual(1, stub.LastA);
            Assert.AreEqual(-3, stub.LastB);
            Assert.AreEqual(2, stub.LastC);
        }

        [TestMethod]
        public void Controller_Handles_Exception_From_StubSolver()
        {
            var controller = new QuadraticController(new ThrowingStubSolver());

            string result = controller.SolveEquation(1, 2, 3);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.StartsWith("Lỗi:"));
        }
    }
}


