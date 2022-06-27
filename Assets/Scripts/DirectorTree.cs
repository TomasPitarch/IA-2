using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectorStates
{
    Iddle,Patrol,Go
}
public class DirectorTree : MonoBehaviour
{
    public event Action<DirectorStates> OnTreeElection = delegate{ };

    public bool _isPatrolTime= false;
    public bool _hasInterest = false;

    INode _root;
   
    private void Start()
    {
        InitializedTree();
        Execute();
       
    }

    public void Execute()
    {
        Debug.Log("execute tree analysis");
        _root.Execute(); 
    }

    void InitializedTree()
    {

        //Actions
        //INode shoot = new ActionNode(()=>print("Shoot"));
        INode iddle = new ActionNode(this.iddle);
        INode patrol = new ActionNode(this.patrol);
        INode goToPoint = new ActionNode(go);

       
        /*Dictionary<INode, int> itemsRandom = new Dictionary<INode, int>();
        itemsRandom[shoot] = 25;
        itemsRandom[chase] = 15;
        itemsRandom[patrol] = 5;
        itemsRandom[scan] = 1;

        INode random = new RandomNode(itemsRandom);
        */

        //Questions
        
       
        
        INode qIsPatrolTime = new QuestionNode(() => _isPatrolTime, patrol, iddle);
        INode qHasInterest = new QuestionNode(() => _hasInterest, goToPoint, qIsPatrolTime);
       
       
      
      
       

        _root = qHasInterest;
    }

    void iddle()
    {
        Debug.Log("iddle elegido");
        OnTreeElection(DirectorStates.Iddle);
    }

    void patrol()
    {
        Debug.Log("patrol elegido");

        OnTreeElection(DirectorStates.Patrol);
    }

    void go()
    {
        
        OnTreeElection(DirectorStates.Go);
        Debug.Log("go elegido");
    }
}
