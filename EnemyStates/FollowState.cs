using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FollowState : IEnemyState
{
    private Enemy _parent;

    public void Enter(Enemy parent)
    {
        _parent = parent;
    }

    public void Exit()
    {
        _parent.Velocity = Vector2.zero;
    }

    public void Update()
    {
        if (_parent.Target != null)
        {
            _parent.Velocity += SteeringBehaviour.Seek(_parent.transform.position, _parent.Target.transform.position, _parent.Speed);
            _parent.Velocity = Vector2.ClampMagnitude(_parent.Velocity, _parent.MaxSpeed);
            Vector2 steering = _parent.Velocity / _parent.RigidBody.mass;
            _parent.RigidBody.AddForce(steering);
            _parent.AnimatorController.SetBool("IsFollowing", true);
        }

        else
        {
            _parent.AnimatorController.SetBool("IsFollowing", false);
            _parent.ChangeState(new IdleState());
        }
    }
}

