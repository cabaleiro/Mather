using System;

public class Complex
{
	public Complex(double _real = 0, double _imag = 0)
	{
        real = _real;
        imaginary = _imag;
        isCartesian = true;
        //Calculate radius and angle
        makePolar();
	}

    public Complex(double _rad = 0, double _ang = 0, byte _isCartesian)
    {
        radius = _rad;
        angle = _ang;
        isCartesian = false;
        //Calculate real and imaginary
        makeCartesian();
    }
    private double real;
    private double imaginary;
    private double radius;
    private double angle;
    private byte isCartesian;

    //Cartesian: a + bi
    //Polar: rcos ang + rsin ang i
    //Exponential: r e^(i angle)
    public static makeCartesian()
    {
        real = radius * Math.Cos(angle);
        imaginary = radius * Math.Sin(angle);
    }
    public static makePolar()
    {
        radius = Math.Sqrt(real ^ 2 + imaginary ^ 2);
        angle = Math.Atan(imaginary / real);
    }
    public static ToString()
    {
        if (isCartesian == true)
            { return printCartesian(); }
        else
            { return printPolar(); }
    }
    public static printCartesian()
    {
        return (real.ToString() + " + " + imaginary.ToString() + "i");
    }
    public static printPolar()
    {
        //return (radius.ToString() + " cos (" + angle.ToString() + ")" + " + " + radius.ToString() + "i " + "sin(" + angle.ToString() + ")");
        return (radius.ToString() + " |_ " + angle.ToString());
    }

    public static Exponent(double _exp)
    {
        Complex resultNum = new Complex();
        resultNum.radius = radius ^ _exp;
        resultNum.angle = angle * _exp;
    }

    public static Complex operator +(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex();
        resultNum.real = _num1.real + _num2.real;
        resultNum.imaginary = _num1.imaginary + _num2.imaginary;
        return resultNum;
    }
    public static Complex operator -(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex();
        resultNum.real = _num1.real - _num2.real;
        resultNum.imaginary = _num1.imaginary - _num2.imaginary;
        return resultNum;
    }
    public static Complex operator *(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex();
        resultNum.real = _num1.radius * _num2.radius;
        resultNum.imaginary = _num1.angle + _num2.angle;
        return resultNum;
    }
    public static Complex operator /(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex();
        resultNum.real = _num1.radius / _num2.radius;
        resultNum.imaginary = _num1.angle - _num2.angle;
        return resultNum;
    }
    //add override for stdout << "real + imaginary i"
    //add override for root and ^^ using DeMoivre's Theorem
}
