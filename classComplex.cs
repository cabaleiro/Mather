using System;

public class Complex
{
	public Complex(double _real, double _imag)
	{
        real = _real;
        imaginary = _imag;
        isCartesian = true;
        //Calculate radius and angle
	}

    public Complex(double _rad, double _ang, byte _isCartesian)
    {
        radius = _rad;
        angle = _ang;
        isCartesian = false;
        //Calculate real and imaginary
    }
    double real;
    double imaginary;
    double radius;
    double angle;
    byte isCartesian;

    //Cartesian: a + bi
    //Polar: rcos ang + rsin ang i
    //Exponential: r e^(i angle)
    printCartesian()
    {
        return (real.ToString() + " + " + imaginary.ToString() + "i");
    }
    printPolar()
    {
        return (radius.ToString() + " cos (" + angle.ToString() + ")" + " + " + radius.ToString() + "i " + "sin(" + angle.ToString() + ")");
    }


    //add override for stdout << "real + imaginary i"
    //add override for * template (using real and imag as binom) (or using polar)
    //add override for + template (real + real, imag + imag)
    //add override for - template (^^)
    //add override for / template (multip by conjugate up and down) (or using polar)
    //add override for ** int (using *)
    //add override for root and ^^ using DeMoivre's Theorem
}
