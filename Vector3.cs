using System;

public class Vector3
{
	public Vector3(double _x = 0, double _y = 0, double _z = 0)
	{

	}
    private double x;
    private double y;
    private double z;
    private double length;

    public static Length()
    {
        length = Math.Sqrt(x ^ 2 + y ^ 2 + z ^ 2);
        return length;
    }

    public static Vector3 operator +(Vector3 _Vec1, Vector3 _Vec2)
    {
        Vector3 resultVector = new Vector3();
        resultVector.x = _Vec1.x + _Vec2.x;
        resultVector.y = _Vec1.y + _Vec2.y;
        resultVector.z = _Vec1.z + _Vec2.z;
        return resultVector;
    }
    public static Vector3 operator -(Vector3 _Vec1, Vector3 _Vec2)
    {
        Vector3 resultVector = new Vector3();
        resultVector.x = _Vec1.x - _Vec2.x;
        resultVector.y = _Vec1.y - _Vec2.y;
        resultVector.z = _Vec1.z - _Vec2.z;
        return resultVector;
    }
    public static Vector3 operator *(Vector3 _Vec1, Vector3 _Vec2)
    {
        double resultVal = 0;
        resultVal += _Vec1.x * _Vec2.x;
        resultVal += _Vec1.y * _Vec2.y;
        resultVal += _Vec1.z * _Vec2.z;
        return resultVal;
    }
    public static Vector3 operator *(Vector3 _Vec1, double _constant)
    {
        double resultVal = 0;
        resultVal += _Vec1.x * _constant;
        resultVal += _Vec1.y * _constant;
        resultVal += _Vec1.z * _constant;
        return resultVal;
    }
    public static Vector3 operator *(Vector3 _Vec1, int _constant)
    {
        double resultVal = 0;
        resultVal += _Vec1.x * _constant;
        resultVal += _Vec1.y * _constant;
        resultVal += _Vec1.z * _constant;
        return resultVal;
    }
    public static Vector3 operator ^(Vector3 _Vec1, Vector3 _Vec2)
    {
        Vector3 resultVector = new Vector3();
        resultVector.x = (_Vec1.y * _Vec2.z) - (_Vec1.z * _Vec2.y);
        resultVector.y = (_Vec1.x * _Vec2.z) - (_Vec1.z * _Vec2.x);
        resultVector.z = (_Vec1.x * _Vec2.y) - (_Vec1.y * _Vec2.x);
        return resultVal;
    }
}
