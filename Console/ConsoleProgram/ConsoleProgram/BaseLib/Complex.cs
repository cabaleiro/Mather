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
    //Polar: rcos ang + rsin ang i OR radiusLangle
    //Exponential: r e^(i angle)

    public void makeCartesian()
    {
        real = radius * Math.Cos(angle);
        imaginary = radius * Math.Sin(angle);
    }
    public void makePolar()
    {
        radius = Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2));
        if (real != 0)
        {
            angle = Math.Atan(imaginary / real);
        }
        else
        {
            angle = 90;
        }
    }
    //Text output
    public override string ToString()
    {
        if (isCartesian == true)
            { return printCartesian(); }
        else
            { return printPolar(); }
    }
    public string printCartesian()
    {
        string printer = "";
        if (real != 0)
        {
            printer += real.ToString();
            if (imaginary != 0)
            {
                printer += " + ";
                if (imaginary == 1)
                {
                    printer += "i";
                    return printer;
                }
                else
                {
                    printer += imaginary.ToString();
                    printer += " i";
                    return printer;
                }
            }
            else
            {
                return printer;
            }
        }
        else
        {
            if (imaginary != 0)
            {
                if (imaginary == 1)
                {
                    printer += "i";
                    return printer;
                }
                else
                {
                    printer += imaginary.ToString();
                    printer += " i";
                    return printer;
                }
            }
            else
            {
                return "0";
            }
        }
    }
    public string printPolar()
    {
        //return (radius.ToString() + " cos (" + angle.ToString() + ")" + " + " + radius.ToString() + "i " + "sin(" + angle.ToString() + ")");
        if (radius != 0)
        { 
            return (radius.ToString() + " |_ " + angle.ToString() + "°");
        }
        else
        {
            return "0";
        }
    }

    //Operators
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
        resultNum.radius = _num1.radius * _num2.radius;
        resultNum.angle = _num1.angle + _num2.angle;
        resultNum.makeCartesian();
        return resultNum;
    }
    public static Complex operator /(Complex _num1, Complex _num2)
    {
        Complex resultNum = new Complex(false);
        resultNum.radius = _num1.radius / _num2.radius;
        resultNum.angle = _num1.angle - _num2.angle;
        resultNum.makeCartesian();
        return resultNum;
    }
    public static Complex operator ^(Complex _vec1, double _exp)
    {
        Complex resultNum = new Complex(false);
        resultNum.radius = Math.Pow(_vec1.radius, _exp);
        resultNum.angle = _vec1.angle * _exp;
        return resultNum;
    }

    //add override for stdout << "real + imaginary i"
    //add override for root and ^^ using DeMoivre's Theorem
}
