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
            this.side1 = Math.Round(side1,4);
            this.side2 = Math.Round(side2, 4);
            this.side3 = Math.Round(side3, 4);

        }

        public double Side1
        {
            get { return side1; }
            set { side1 = Math.Round(value, 4); }
        }
        public double Side2
        {
            get { return side2; }
            set { side2 = Math.Round(value, 4); }
        }
        public double Side3
        {
            get { return side3; }
            set { side3 = Math.Round(value, 4); }
        }

        public double Area()
        {
            double s = Perimeter() / 2;
            double resultArea = Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3));

            return resultArea;
        }

        public double Perimeter() => side1 + side2 + side3;

        public double[] GetOrderedSides()
        {
            double[] sides = { side1, side2, side3 };
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
            return Side1 < (side2 + side3) && side2 < (side1 + side3) && side3 < (side1 + side2);
        }

        public bool IsRightAngled()
        {
            var sides = new[] { side1, side2, side3 };
            Array.Sort(sides);
            return Math.Abs(sides[2] * sides[2] - (sides[0] * sides[0] + sides[1] * sides[1])) < 0.001;
        }

        public bool IsEquilateral()
        {
            return side1 == side2 && side2 == side3;
        }

        public bool IsIsosceles()
        {
            return side1 == side2 || side1 == side3 || side2 == side3;
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
