using System;
using P_TB2.Models;

namespace P_TB2.Controllers
{
    public class QuadraticController
    {
        public string SolveEquation(double a, double b, double c)
        {
            try
            {
                var equation = new QuadraticEquation(a, b, c);
                return equation.Solve();
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
