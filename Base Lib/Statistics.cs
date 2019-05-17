using System;

public class Statistics
{
	public Statistics(List <double> _input)
	{
        input = _input;
	}
    public static Summatoire()
    {
        double sum = 0;
        foreach (double x in input)
        {
            sum += x;
        }
        return sum;
    }
    public static Average()
    {
        double sum = Summatoire();
        return Summatoire() / input.Count();
    }
    public static MobileAverage(int _lower)
    {
        List<double> temp = input;
        for (int i = 0; i <= _lower; i++)
        {
            temp.RemoveAt(0);
        }
        return temp.Summatoire() / temp.input.Count();
    }


    List<double> input;
}
