using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorPatrolState<T> : State<T>
{
    public event Action<T> OnStateComplete;
    public override void Init()
    {base.Init();
    }

    public override void Execute()
    {base.Execute();
        Debug.Log("Patrol");

    }

    public override void Exit()
    { base.Exit();
    }

}
