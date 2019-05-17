using System;

public struct Point
{
    public Point(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    int x;
    int y;
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
        ang1 = Math.Atan2(a.y - o.y, a, x - o.x);
        ang1 = Math.Atan2(b.y - o.y, b, x - o.x);
    }
    public Arc(Point _o, double _radius, double _ang1, double _ang2)
    {
        o = _o;
        radius = _radius;
        ang1 = _ang1;
        ang2 = ang2;
        //calc Points
        a = new Point(o.x * Math.Cos(ang1) ,o.y * Math.Sin(ang1));
        b = new Point(o.x * Math.Cos(ang2), o.y * Math.Sin(ang2));
    }
    Point a;
    Point b;
    Point _o;
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
    public Quadrilater()
    {

    }
}