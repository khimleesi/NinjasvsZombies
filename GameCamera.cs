using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private Transform   _target;
    private float       _trackSpeed = 10.0f;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    // Executed after player movement
    void LateUpdate()
    {
        if (_target)
        {
            float x = Tools.IncrementTowards(transform.position.x, _target.position.x, _trackSpeed);
            float y = Tools.IncrementTowards(transform.position.y, _target.position.y, _trackSpeed);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
