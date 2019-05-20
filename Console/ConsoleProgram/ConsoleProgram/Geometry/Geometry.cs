using System;

public struct Point
{
    public Point(double _x, double _y)
    {
        x = _x;
        y = _y;
    }
    double x;
    double y;

    public double X { get => x; set => x = value; }
    public double Y { get => y; set => y = value; }
}
public struct Line
{
    public Line(Point _a, Point _b)
    {
        a = _a;
        b = _b;
    }
    Point a;
    Point b;

    public Point A { get => a; set => a = value; }
    public Point B { get => b; set => b = value; }
}
public struct Arc
{
    public Arc(Point _a, Point _b, Point _o, double _radius)
    {
        a = _a;
        b = _b;
        o = _o;
        radius = _radius;
        //calc angs
        ang1 = Math.Atan2(a.Y - o.Y, a.X - o.X);
        ang2 = Math.Atan2(b.Y - o.Y, b.X - o.X);
    }
    public Arc(Point _o, double _radius, double _ang1, double _ang2)
    {
        o = _o;
        radius = _radius;
        ang1 = _ang1;
        ang2 = _ang2;
        //calc Points
        a = new Point(o.X * Math.Cos(ang1) ,o.Y * Math.Sin(ang1));
        b = new Point(o.X * Math.Cos(ang2), o.Y * Math.Sin(ang2));
    }
    Point a;
    Point b;
    Point o;
    double radius;
    double ang1;
    double ang2;
}
public struct Circle
{
    public Circle(Point _o, double _radius)
    {
        o = _o;
        radius = _radius;
    }
    Point o;
    double radius;
}

public class Geometry
{
	public Geometry()
	{

	}
}
public class Triangle
{
    Point A;
    Point B;
    Point C;
    double[] angles = { 0, 0, 0 };
    double[] sides = { 0, 0, 0 };
    bool isStraight = false;


    //The vectors are defined in the anti-clockwise direction
    //The first side is the side that opens to the first angle, in any order
    public Triangle(double ang1, double ang2, double ang3, double side1, double side2, double side3)
    {
        int counterSides = 0;
        int counterAngles = 0;
        if (ang1 != 0)
        {
            angles[0] = ang1;
            counterAngles += 1;
            if (ang1 == 90)
                isStraight = true;

        }
        if (ang2 != 0)
        {
            angles[1] = ang2;
            counterAngles += 1;
            if (ang1 == 90)
                isStraight = true;
        }
        if (ang3 != 0)
        {
            angles[2] = ang3;
            counterAngles += 1;
            if (ang1 == 90)
                isStraight = true;
        }
        if (side1 != 0)
        {
            sides[0] = side1;
            counterSides += 1;
        }
        if (side2 != 0)
        {
            sides[1] = side2;
            counterSides += 1;
        }
        if (side3 != 0)
        {
            sides[2] = side3;
            counterSides += 1;
        }

        //Check if two sides or two angles are equal
        //Check if angle sum >180
        //If counter of non-zero values is > 3, check if values are compatible

        //
    }
    public double SineLawSide(double knownSide, double knownAngle, double unknownAngle)
    {
        //a = sinA * b / sinB
        return 0;
    }
    public double SineLawAngle(double knownAngle, double knownSide, double unknownSide)
    {
        //sinA = a * sinB / b
        return 0;
    }
    public double CosineLawSide()
    {
        //a^2 = b^2 + c^2 -2bc cosA
        return 0;
    }
    public double CosineLawAngle()
    {
        //cosA = (b^2 + c^2 - a^2)/(2bc)
        return 0;
    }
    public double Area()
    {   
        //if angle != 90
        //Area = sqrt(s*(s-a)(s-b)(s-c))
        //if angle = 90
        //
        return 0;
    }
}
public class Quadrilater
{
    public Quadrilater()
    {

    }
}
public class RegularPolygon
{
    public RegularPolygon()
    {

    }
}