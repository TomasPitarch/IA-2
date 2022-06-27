using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField]
    float ghostSpeed=5;
   


    public void GhostMove(Vector3 destiny)
    {
        var dir = (destiny - transform.position).normalized;
       
        transform.position += dir * ghostSpeed * Time.deltaTime;
    }
}
