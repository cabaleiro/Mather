using System;

public struct Particle
{
    Vector3 position;
    Vector3 speed;
    Vector3 acceleration;
    Vector3 mass;
}

public struct Body
{
    Particle centerOfMass;
    Vector3 angularPosition;
    Vector3 angularSpeed;
    Vector3 angularAccelation;
    Geometry shape;
}


public class Physics2D
{
	public Physics2D()
	{
	}

    public static double EvalPosition()
    {
        return 0;
    }

    public static void ResultantForce()
    {
        //Input List<Vector2>
        //Create new Vector2
        //Sum all into new Vector2
        //return new Vector2
    }
    //Lots to add
}
