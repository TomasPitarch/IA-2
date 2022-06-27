using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorGoToEventState <T> : State<T>
{
    public event Action<T> OnStateComplete;
    public override void Init()
    {
        base.Init();
    }

    public override void Execute()
    {base.Execute();
    Debug.Log("Go");
    }

    public override void Exit()
    { base.Exit();
    }

   
}
