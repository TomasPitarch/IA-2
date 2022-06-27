using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
   [SerializeField]
   private List<Node> EspectralPath;
   [SerializeField]
   private BlackboardGeneric memory;
   [SerializeField] 
   private SoundSensor _soundSensor;
   private Node StartNode;
    
   
   
   private FSM<DirectorStates> _fsm;
   [SerializeField]
   private DirectorTree _tree;

   private void Awake()
   {
      _soundSensor.OnSoundEventLongRange += SoundEventHandler;
      StartNode = EspectralPath[0];
      
      SetFSM();
      _tree.OnTreeElection += DecisionTreeToFSMHandler;
   }
   
   private void SetFSM()
   {
      _fsm = new FSM<DirectorStates>();

      var GoState = new DirectorGoToEventState<DirectorStates>();
      var IddleState = new DirectorIddleState<DirectorStates>();
      var PatrolState = new DirectorPatrolState<DirectorStates>();
      
      GoState.AddTransition(DirectorStates.Iddle,IddleState);
      GoState.AddTransition(DirectorStates.Patrol,PatrolState);
      
      IddleState.AddTransition(DirectorStates.Go,GoState);
      IddleState.AddTransition(DirectorStates.Patrol,PatrolState);
      
      PatrolState.AddTransition(DirectorStates.Go,GoState);
      PatrolState.AddTransition(DirectorStates.Iddle,IddleState);

      //GoState.OnStateComplete += _fsm.Transition;
      //IddleState.OnStateComplete += _fsm.Transition;
      //PatrolState.OnStateComplete += _fsm.Transition;
      
      _fsm.SetInit(IddleState);


   }
   void DecisionTreeToFSMHandler(DirectorStates input)
   {
      Debug.Log("decisiontofsm:"+input);
      _fsm.Transition(input);
   }

   private void Update()
   {
      _fsm.OnUpdate();
   }

   private void SoundEventHandler(Vector3 eventPosition)
   {
      Debug.Log("sound event handler");
      memory.Set("LongSoundEvent",eventPosition);

      StartNode=FindClosestNode(memory.Get<GameObject>("Body").Get().transform.position);
      //memory.Set("PathToDo",StartNode.FindPathAStar(FindClosestNode(eventPosition)));

   }

   private void OnDrawGizmos()
   {
      Gizmos.color=Color.green;
      if (StartNode!=null)
      {
         Gizmos.DrawSphere(StartNode.transform.position,1);
      }

      if ( memory.Get<List<Node>>("PathToDo") != null)
      {
         var ThePath = memory.Get<List<Node>>("PathToDo").Get();
         Gizmos.color = Color.green;

         if (ThePath != null)
         {
            int i;
            for (i = 1; i < ThePath.Count; i++)
            {
               Gizmos.DrawLine(ThePath[i-1].transform.position, ThePath[i].transform.position);

            }
            //Gizmos.DrawLine(ThePath[i-1].transform.position, ThePath[0].transform.position);
         } 
      }
     

     

   }
  

   Node FindClosestNode(Vector3 postionOfInterest)
   {
      var ClosestNode = EspectralPath[0];
      foreach (var node in EspectralPath)
      {
         var distance = (postionOfInterest - node.transform.position).magnitude;
         if (distance < (postionOfInterest - ClosestNode.transform.position).magnitude)
         {
            ClosestNode = node;
         }
      }

      return ClosestNode;
   }
}
