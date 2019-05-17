using System;
using System.Collections.Generic;

public class Statistics
{
	public Statistics(List<double> _input)
	{
        input = _input;
	}
    public double Summatoire(List<double> _input)
    {
        double sum = 0;
        foreach (double x in _input)
        {
            sum += x;
        }
        return sum;
    }
    public double Average()
    {
        double sum = Summatoire(input);
        return Summatoire(input) / input.Count;
    }
    public double MobileAverage(int _lower)
    {
        List<double> temp = input;
        temp.RemoveRange(0, _lower);
        return Summatoire(temp) / temp.Count;
    }


    List<double> input;
}
