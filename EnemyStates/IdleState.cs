using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class IdleState : IEnemyState
{

    private Enemy _parent;

    public void Enter(Enemy parent)
    {
       _parent = parent;
 
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        if (_parent.Target != null)
        {
            _parent.ChangeState(new FollowState());
        }
    }
}

