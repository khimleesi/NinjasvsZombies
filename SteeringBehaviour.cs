using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SteeringBehaviour
{

  public static Vector2 Seek(Vector2 origin, Vector2 target, float seekForce)
    {
        return (target - origin).normalized * seekForce;
    }

    public static Vector2 Flee(Vector2 origin, Vector2 target, float fleeForce)
    {
        return (origin - target).normalized * fleeForce;
    }

    public static Vector2 Arrive(Vector2 origin, Vector2 target, float seekForce, float slowDownArea)
    {
        float distance = (target - origin).magnitude;

        //Slow down area being the range or radius of the invisible "slow down" circle surrounding the target
        float factor = Mathf.Min(distance / slowDownArea, 1.0f);
       
        return (target - origin).normalized * seekForce * factor;
    }

    public static Vector2 FleeRangeBased(Vector2 origin, Vector2 target, float fleeForce, float range)
    {
        float distance = (target - origin).magnitude;
        float factor = 1.0f - Mathf.Min(distance / range, 1.0f); 

        return (origin - target).normalized * fleeForce * factor;
    }

    public static Vector2 Pursue(Vector2 origin, Vector2 target, float fleeForce, float range)
    {
        Vector2 distance = target - origin;
        float factor = distance.magnitude;

        return Seek(origin, target, factor);
    }

    public static Vector2 Evade(Vector2 origin, Vector2 target, float fleeForce, float range)
    {

        float distance = (target - origin).magnitude;
        float factor = 1.0f - Mathf.Min(distance / range, 1.0f);

        return (origin - target).normalized * fleeForce * factor;
    }
}
