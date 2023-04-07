using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{   
    //Increase currentSpeed towards targetSpeed by acceleration
    public static float IncrementTowards(float currentSpeed, float targetSpeed, float acceleration)
    {
        if (currentSpeed == targetSpeed)
        {
            return currentSpeed;
        }
        else
        {
            float direction = Mathf.Sign(targetSpeed - currentSpeed); // must currentSpeed be increased or decreased to get closer to target
            currentSpeed += acceleration * Time.deltaTime * direction;
            return (direction == Mathf.Sign(targetSpeed - currentSpeed)) ? currentSpeed : targetSpeed; // if currentSpeed has now passed targetSpeed then return targetSpeed, otherwise return currentSpeed
        }
    }

}
