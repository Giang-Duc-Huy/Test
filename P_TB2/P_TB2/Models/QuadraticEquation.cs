using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_TB2.Models
{
    public class QuadraticEquation
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public QuadraticEquation(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public string Solve()
        {
            if (A == 0)
            {
                if (B == 0)
                {
                    if (C == 0)
                        return "Phương trình có vô số nghiệm";
                    else
                        return "Phương trình vô nghiệm";
                }
                else
                {
                    double x = -C / B;
                    return $"Phương trình bậc 1: x = {x:F2}";
                }
            }

            double delta = B * B - 4 * A * C;

            if (delta < 0)
            {
                return "Phương trình vô nghiệm";
            }
            else if (delta == 0)
            {
                double x = -B / (2 * A);
                return $"Phương trình có nghiệm kép: x = {x:F2}";
            }
            else
            {
                double x1 = (-B + Math.Sqrt(delta)) / (2 * A);
                double x2 = (-B - Math.Sqrt(delta)) / (2 * A);
                return $"Phương trình có 2 nghiệm phân biệt:x₁ = {x1:F2}    x₂ = {x2:F2}";
            }
        }

        public bool IsValidInput()
        {
            return true; 
        }
    }
}
