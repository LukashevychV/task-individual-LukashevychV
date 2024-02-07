using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System;
using Triangles.Models;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Triangles.Controllers
{
    [Route("triangle")]
    public class TriangleController : Controller
    {



        [HttpGet("Info")]
        public string Info([FromQuery] Triangle triangle)
        {
            if (triangle == null || !triangle.IsValid())
            {
                return "Invalid triangle dimensions.";
            }

            var sides = new[] { triangle.Side1, triangle.Side2, triangle.Side3 }.OrderBy(s => s).ToArray();
            double perimeter = triangle.Perimeter();
            double area = triangle.Area();
            var reducedSides = sides.Select(side => Math.Round(side / perimeter, 2)).ToArray();

            string info = $@"Triangle:
({sides[0]}, {sides[1]}, {sides[2]})
Reduced:
({reducedSides[0]}, {reducedSides[1]}, {reducedSides[2]})

Area = {Math.Round(area, 2):F2}
Perimeter = {Math.Round(perimeter, 2)}";

            return info;
        }

        [HttpGet("IsRightAngled")]
        public bool IsRightAngled([FromQuery] Triangle triangle)
        {
            if (!triangle.IsValid())
            {
                return false;
            }

            var isRightAngled = triangle.IsRightAngled();
            return isRightAngled;
        }

        [HttpGet("Perimeter")]
        public string Perimeter([FromQuery] Triangle triangle)
        {
            if (!triangle.IsValid())
            {
                return ("Invalid triangle dimensions.");
            }

            var perimeter = triangle.Perimeter();
            return ($"{perimeter}");
        }

        [HttpGet("Area")]
        public string Area([FromQuery] Triangle triangle)
        {
            if (!triangle.IsValid())
            {
                return ("Invalid triangle dimensions.");
            }

            var area = triangle.Area();
            return (area.ToString("F4"));
        }

        [HttpGet("IsEquilateral")]
        public bool IsEquilateral([FromQuery] Triangle triangle)
        {
            if (!triangle.IsValid())
            {
                return false;
            }

            var isEquilateral = triangle.IsEquilateral();
            return isEquilateral;
        }

        [HttpGet("IsIsosceles")]
        public bool IsIsosceles([FromQuery] Triangle triangle)
        {
            if (!triangle.IsValid())
            {
                return false;
            }

            var isIsosceles = triangle.IsIsosceles();
            return isIsosceles;
        }

        [HttpGet("AreCongruent")]
        public bool AreCongruent([FromQuery] Triangle tr1, [FromQuery] Triangle tr2)
        {
            if (!tr1.IsValid() || !tr2.IsValid())
            {
                return false;
            }

            bool areCongruent = tr1.AreCongruent(tr2);
            return areCongruent;
        }

        [HttpGet("AreSimilar")]
        public bool AreSimilar([FromQuery] Triangle tr1, [FromQuery] Triangle tr2)
        {
            if (!tr1.IsValid() || !tr2.IsValid())
            {
                return false;
            }

            var areSimilar = tr1.AreSimilar(tr2);
            return areSimilar;
        }

        [HttpGet("InfoGreatestPerimeter")]
        public string InfoGreatestPerimeter([FromQuery] Triangle[] tr)
        {

            foreach (var item in tr)
            {
                if (!item.IsValid())
                    return "Invalid triangle dimensions.";
            }

            Triangle triangleWithGreatestPerimeter = null;
            double maxPerimeter = 0;
            foreach (var triangle in tr)
            {
                if (!triangle.IsValid())
                {
                    return "Invalid triangle dimensions.";
                }
                double currentPerimeter = triangle.Perimeter();
                if (currentPerimeter > maxPerimeter)
                {
                    maxPerimeter = currentPerimeter;
                    triangleWithGreatestPerimeter = triangle;
                }
            }
            if (triangleWithGreatestPerimeter == null)
            {
                return "Could not find a valid triangle with the greatest perimeter.";
            }

            var sides = triangleWithGreatestPerimeter.GetOrderedSides();
            var area = triangleWithGreatestPerimeter.Area();
            var reducedSides = sides.Select(side => side / maxPerimeter).ToArray();

            string info = $"Triangle:\n({sides[0]}, {sides[1]}, {sides[2]})\nReduced:\n({reducedSides[0]:F2}, {reducedSides[1]:F2}, {reducedSides[2]:F2})\n\nArea = {area:F2}\nPerimeter = {maxPerimeter:F2}";

            return info;

        }

        [HttpGet("InfoGreatestArea")]
        public string InfoGreatestArea([FromQuery] Triangle[] tr)
        {
            if (tr == null || tr.Length == 0)
            {
                return "No triangles provided.";
            }

            Triangle triangleWithGreatestArea = null;
            double maxArea = 0;
            foreach (var triangle in tr)
            {
                if (!triangle.IsValid())
                {
                    return "Invalid triangle dimensions.";
                }
                double currentArea = triangle.Area();
                if (currentArea > maxArea)
                {
                    maxArea = currentArea;
                    triangleWithGreatestArea = triangle;
                }
            }

            if (triangleWithGreatestArea == null)
            {
                return "Could not find a valid triangle with the greatest area.";
            }

            var sides = triangleWithGreatestArea.GetOrderedSides();
            var perimeter = triangleWithGreatestArea.Perimeter();
            var reducedSides = sides.Select(side => side / perimeter).ToArray();

            string info = $"Triangle:\n({sides[0]}, {sides[1]}, {sides[2]})\nReduced:\n({reducedSides[0]:F2}, {reducedSides[1]:F2}, {reducedSides[2]:F2})\n\nArea = {maxArea:F2}\nPerimeter = {perimeter:F2}";

            return info;
        }

        [HttpGet("NumbersPairwiseNotSimilar")]
        public string NumbersPairwiseNotSimilar([FromQuery] Triangle[] tr)
        {
            if (tr == null || tr.Length == 0)
            {
                return "No triangles provided.";
            }

            for (int i = 0; i < tr.Length; i++)
            {
                if (!tr[i].IsValid())
                {
                    return $"Invalid dimensions for triangle at index {i}.";
                }
            }
            var nonSimilarPairs = new List<string>();
            for (int i = 0; i < tr.Length - 1; i++)
            {
                for (int j = i + 1; j < tr.Length; j++)
                {
                    if (!tr[i].AreSimilar(tr[j]))
                    {
                        nonSimilarPairs.Add($"({i + 1}, {j + 1})");
                    }
                }
            }

            string responseContent = string.Join(Environment.NewLine, nonSimilarPairs);

            return responseContent;
        }
    }
}

