using Xunit;
using Shapes;
using System;
using System.Collections.Generic;

namespace ShapesTests
{
    public class ShapeTests
    {
        public static TheoryData<double, double, double, bool> TriangleData => new()
        {
            { 1, 3, 3, true },
            { -1, 1, 1, false },
            { 1, -1, 1, false },
            { 1, 1, -1, false },
            { -1, -1, 1, false },
            { 1, -1, -1, false },
            { -1, -1, -1, false },
            { 0, 1, 1, false },
            { 1, 0, 1, false },
            { 1, 1, 0, false },
            { 0, 0, 1, false },
            { 1, 0, 0, false },
            { 0, 0, 0, false },
        };
        public static TheoryData<double, double, double, bool> RightTriangleData => new()
        {
            { 3,4,5,true},
            { 6,8, 10, true},
            { 7,8,14, false},
            { 34, 32, 12, false}
        };
        public static TheoryData<double, double, double, double> TriangleArea => new()
        {
            { 5, 4, 3, 6 },
            { 3, 4, 3, 4.47 },
            { 6, 8, 7, 20.33 },
        };
        public static TheoryData<double, double> CircleArea => new()
        {
            { 5, 78.54 },
            { 8, 201.06 },
            { 2.7, 22.90 },
        };

        [Theory]

        [MemberData(nameof(TriangleData))]
        public void UseNegativeOrZeroValuesOnTriangle(double a, double b, double c, bool isValid)
        {
            Action act = () => { new Triangle(a, b, c); };
            var ex = Record.Exception(act);

            if (isValid)
            {
                Assert.Null(ex);
            }
            else
            {
                Assert.Equal("Sides can't be negative or zero.", ex.Message);
            }
        }
        [Fact]
        public void UseNegativeValuesOrZeroOnCircle()
        {
            Action act = () => { new Circle(1); };
            var ex1 = Record.Exception(act);

            act = () => { new Circle(0); };
            var ex2 = Record.Exception(act);

            act = () => { new Circle(-1); };
            var ex3 = Record.Exception(act);

            Assert.Null(ex1);
            Assert.Equal("Radius can't be negative or zero.", ex2.Message);
            Assert.Equal("Radius can't be negative or zero.", ex3.Message);

        }
        [Theory]
        [MemberData(nameof(RightTriangleData))]
        public void CheckRightTriangle(double a, double b, double c, bool isRight)
        {
            Triangle triangle = new(a,b,c);
            Assert.Equal(isRight, triangle.TriangleIsRight());
        }

        [Fact]
        public void TriangleExistTest()
        {
            var ex1 = Record.Exception(() => new Triangle(2, 4, 8)); // Side c more then a + b => This triangle don't exist.
            var ex2 = Record.Exception(() => new Triangle(2, 4, 5)); // Normal case.

            Assert.Null(ex2);
            Assert.Equal("Triangle don't exist.", ex1.Message);
        }
        [Theory]
        [MemberData(nameof(TriangleArea))]
        public void CurrectnessOfCalculationsOfTriangleArea(double a, double b, double c, double area)
        {
            double eps = 0.01; // accuracy
            Triangle triangle = new(a,b,c);
            Assert.True(Math.Abs(area - triangle.GetArea()) < eps);
        }

        [Theory]
        [MemberData(nameof(CircleArea))]
        public void CurrectnessOfCalculationsOfCircleArea(double r, double area)
        {
            double eps = 0.01; // accuracy
            Circle circle = new(r);
            Assert.True(Math.Abs(area - circle.GetArea()) < eps);
        }

        [Fact]
        public void CompileTimeTest()
        {
            List<Shape> shapes = new List<Shape>()
            {
                new Triangle(1,3,3),
                new Triangle(2,4,3),
                new Circle(4),
                new Circle(7),
            };
            foreach(Shape shape in shapes)
            {
                var result = GetShapeArea(shape);
                Assert.NotEqual(default, result);
            }

        }

        public double GetShapeArea(Shape shape)
        {
            return shape.GetArea();
        }
    }
}