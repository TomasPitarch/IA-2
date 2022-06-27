using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
   [SerializeField]
   BlackboardGeneric memory;
   //FSM
   //DecisionTree
   //BlackBoard

   private void Start()
   {
      memory.Set("Body",this.gameObject);
   }

  
   
}


