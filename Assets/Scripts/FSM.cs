using System;
using UnityEngine;


public class FSM<T> 
{
    IState<T> _current;
    public FSM(IState<T> init)
    {
        SetInit(init);
    }
    public FSM()
    {
    }
    public void SetInit(IState<T> init)
    {
        _current = init;
        _current.Init();
    }
    public void OnUpdate()
    {
        if (_current == null)
        {   
            return;
        }
        
        _current.Execute();
       
    }
    public void Transition(T input)
    {
        Debug.Log("transition:"+input);
        var newState = _current.GetTransition(input);
        if (newState != null)
        {
            _current.Exit();
            SetInit(newState);
        }
    }
    public IState<T> GetCurrentState => _current;
}
public  interface IState<T> 
{
    event Action<T> OnStateComplete;
    /// <summary>
    /// AWAKE
    /// </summary>
    void Init();
    /// <summary>
    /// EXECUTE / UPDATE
    /// </summary>
    void Execute();
    /// <summary>
    /// SLEEP
    /// </summary>
    void Exit();
    void AddTransition(T input, IState<T> state);
    void RemoveTransition(T input);
    void RemoveTransition(IState<T> state);
    IState<T> GetTransition(T input);
}
