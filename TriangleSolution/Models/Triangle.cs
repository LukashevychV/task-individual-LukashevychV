using System.Linq;
using System;

namespace Triangles.Models
{
    public class Triangle
    {
        private double side1;
        private double side2;
        private double side3;

        public Triangle(double side1, double side2, double side3)
        {
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
            
        }

        public double Side1 
        {
            get { return side1; } 
            set { side1 = value; }
        }
        public double Side2
        {
            get { return side2; }
            set { side2 = value; }
        }
        public double Side3
        {
            get { return side3; }
            set { side3 = value; }
        }

        public double Area()
        {
            var s = Perimeter() / 2;
            return Math.Sqrt(s * (s - Side1) * (s - Side2) * (s - Side3));
        }

        public double Perimeter() => Side1 + Side2 + Side3;

        public double[] GetOrderedSides()
        {
            double[] sides = { Side1, Side2, Side3 };
            Array.Sort(sides);
            return sides;
        }

        public double[] GetReducedSides(double perimeter)
        {
            double[] sides = GetOrderedSides();
            return new double[]
            {
                sides[0] / perimeter,
                sides[1] / perimeter,
                sides[2] / perimeter
            };
        }

        public bool IsValid()
        {
            return Side1 < (Side2 + Side3) && Side2 < (Side1 + Side3) && Side3 < (Side1 + Side2);
        }

        public bool IsRightAngled()
        {
            var sides = new[] { side1, side2, side3 };
            Array.Sort(sides);
            return Math.Abs(sides[2] * sides[2] - (sides[0] * sides[0] + sides[1] * sides[1])) < 0.001;
        }

        public bool IsEquilateral()
        {
            return Side1 == Side2 && Side2 == Side3;
        }

        public bool IsIsosceles()
        {
            return Side1 == Side2 || Side1 == Side3 || Side2 == Side3;
        }

        public bool AreCongruent(Triangle other)
        {
            double[] sides = GetOrderedSides();
            double[] otherSides = other.GetOrderedSides();
            return sides.SequenceEqual(otherSides);
        }

        public bool AreSimilar(Triangle other)
        {
            var sides = GetOrderedSides();
            var otherSides = other.GetOrderedSides();

            double ratio = sides[0] / otherSides[0];
            return Math.Abs(sides[1] / otherSides[1] - ratio) < 0.00001 &&
                   Math.Abs(sides[2] / otherSides[2] - ratio) < 0.00001;
        }
    }
}
