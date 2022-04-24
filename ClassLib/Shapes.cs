using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public interface Shape
    {
        public double GetArea();
    }
    public class Circle: Shape
    {
        private double _radius;
        public Circle(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException("Radius can't be negative or zero.");
            }
            _radius  = radius;
        }
        public double GetArea()
        {
            return Math.PI * _radius * _radius;
        }
    }
    public class Triangle: Shape
    {
        private double _aSide;
        private double _bSide;
        private double _cSide;
        public Triangle(double aSide, double bSide, double cSide)
        {
            if(aSide <= 0 || bSide <= 0 || cSide <= 0)
            {
                throw new ArgumentException("Sides can't be negative or zero.");
            }
            if((aSide+bSide <=cSide) || (aSide+ cSide <= bSide) || (bSide + cSide <= aSide))
            {
                throw new Exception("Triangle don't exist.");
            }
            _aSide = aSide;
            _bSide = bSide;
            _cSide = cSide;
        }
        public double GetArea()
        {
            double halfP = (_aSide + _bSide + _cSide) / 2;
            return Math.Sqrt(halfP * (halfP - _aSide) * (halfP - _bSide) * (halfP - _cSide));
        }
        public bool TriangleIsRight()
        {
            List<double> sides = new() { _aSide, _bSide, _cSide };
            double maxSide = sides.Max();
            int idx = sides.IndexOf(maxSide);
            sides.RemoveAt(idx);
            return sides.Select(val => val * val).Sum() == maxSide * maxSide;
        }
    }
}
