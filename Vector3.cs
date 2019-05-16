using System;

public class Vector3
{
	public Vector3(double _x = 0, double _y = 0, double _z = 0)
	{
        x = _x;
        y = _y;
        z = _z;
        calcLength();
	}
    private double x;
    private double y;
    private double z;
    private double length;

    public static calcLength()
    {
        length = Math.Sqrt(x ^ 2 + y ^ 2 + z ^ 2);
    }

    public static ToString()
    {
        return "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ")";
    }

    public static Vector3 operator +(Vector3 _vec1, Vector3 _vec2)
    {
        Vector3 resultVector = new Vector3();
        resultVector.x = _vec1.x + _vec2.x;
        resultVector.y = _vec1.y + _vec2.y;
        resultVector.z = _vec1.z + _vec2.z;
        return resultVector;
    }
    public static Vector3 operator -(Vector3 _vec1, Vector3 _vec2)
    {
        Vector3 resultVector = new Vector3();
        resultVector.x = _vec1.x - _vec2.x;
        resultVector.y = _vec1.y - _vec2.y;
        resultVector.z = _vec1.z - _vec2.z;
        return resultVector;
    }
    public static Vector3 operator *(Vector3 _vec1, Vector3 _vec2)
    {
        double resultVal = 0;
        resultVal += _vec1.x * _vec2.x;
        resultVal += _vec1.y * _vec2.y;
        resultVal += _vec1.z * _vec2.z;
        return resultVal;
    }
    public static Vector3 operator *(Vector3 _vec1, double _constant)
    {
        double resultVal = 0;
        resultVal += _vec1.x * _constant;
        resultVal += _vec1.y * _constant;
        resultVal += _vec1.z * _constant;
        return resultVal;
    }
    public static Vector3 operator *(Vector3 _vec1, int _constant)
    {
        double resultVal = 0;
        resultVal += _vec1.x * _constant;
        resultVal += _vec1.y * _constant;
        resultVal += _vec1.z * _constant;
        return resultVal;
    }
    public static Vector3 operator ^(Vector3 _vec1, Vector3 _vec2)
    {
        Vector3 resultVector = new Vector3();
        resultVector.x = (_vec1.y * _vec2.z) - (_vec1.z * _vec2.y);
        resultVector.y = (_vec1.x * _vec2.z) - (_vec1.z * _vec2.x);
        resultVector.z = (_vec1.x * _vec2.y) - (_vec1.y * _vec2.x);
        return resultVal;
    }
}
