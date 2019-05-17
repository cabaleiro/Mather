using System;

public class Complex
{
	public Complex(bool _isCartesian, double _val1 = 0, double _val2 = 0)
	{
        if(_isCartesian == true)
        {
            real = _val1;
            imaginary = _val2;
            isCartesian = true;
            //Calculate radius and angle
            makePolar();
        }
        else
        {
            radius = _val1;
            angle = _val2;
            isCartesian = false;
            //Calculate real and imaginary
            makeCartesian();
        }
    }

    private double real;
    private double imaginary;
    private double radius;
    private double angle;
    private bool isCartesian;

    //Cartesian: a + bi
    //Polar: rcos ang + rsin ang i
    //Exponential: r e^(i angle)
    public void makeCartesian()
    {
        real = radius * Math.Cos(angle);
        imaginary = radius * Math.Sin(angle);
    }
    public void makePolar()
    {
        radius = Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2));
        angle = Math.Atan(imaginary / real);
    }
    public override string ToString()
    {
        if (isCartesian == true)
            { return printCartesian(); }
        else
            { return printPolar(); }
    }
    public string printCartesian()
    {
        return (real.ToString() + " + " + imaginary.ToString() + "i");
    }
    public string printPolar()
    {
        //return (radius.ToString() + " cos (" + angle.ToString() + ")" + " + " + radius.ToString() + "i " + "sin(" + angle.ToString() + ")");
        return (radius.ToString() + " |_ " + angle.ToString());
    }

    public Complex Exponent(double _exp)
    {
        Complex resultNum = new Complex(false);
        resultNum.radius = Math.Pow(radius, _exp);
        resultNum.angle = angle * _exp;
        return resultNum;
    }

    public static Complex operator +(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex(true);
        resultNum.real = _num1.real + _num2.real;
        resultNum.imaginary = _num1.imaginary + _num2.imaginary;
        return resultNum;
    }
    public static Complex operator -(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex(true);
        resultNum.real = _num1.real - _num2.real;
        resultNum.imaginary = _num1.imaginary - _num2.imaginary;
        return resultNum;
    }
    public static Complex operator *(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex(false);
        resultNum.real = _num1.radius * _num2.radius;
        resultNum.imaginary = _num1.angle + _num2.angle;
        return resultNum;
    }
    public static Complex operator /(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex(false);
        resultNum.real = _num1.radius / _num2.radius;
        resultNum.imaginary = _num1.angle - _num2.angle;
        return resultNum;
    }
    //add override for stdout << "real + imaginary i"
    //add override for root and ^^ using DeMoivre's Theorem
}
