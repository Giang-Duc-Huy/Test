using System;
using P_TB2.Models;

namespace P_TB2.Controllers
{
    public class QuadraticController
    {
        private readonly IQuadraticSolver _solver;

        public QuadraticController() : this(new QuadraticEquation(0, 0, 0))
        {
        }

        public QuadraticController(IQuadraticSolver solver)
        {
            _solver = solver;
        }

        public string SolveEquation(double a, double b, double c)
        {
            try
            {
                return _solver.Solve(a, b, c);
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        public bool ValidateInput(string input)
        {
            return double.TryParse(input, out _);
        }

        public string ClearResult()
        {
            return string.Empty;
        }
    }
}
