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
    public Triangle()
    {

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