using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask playerMask;
   
    [SerializeField] private float _visionRange;
    [SerializeField] private float _visionAngle;
    [SerializeField] private bool isObjectInSigth;

    private void Update()
    {
       var objectsInRange= Physics.OverlapSphere(transform.position, _visionRange,playerMask);
       foreach (var target in objectsInRange)
       {
           isObjectInSigth = LineOfSigth(target.transform, _visionRange);
       }
    }

    public bool LineOfSigth(Transform targetTransform, float range)
    {
        //Range
        var diff = targetTransform.position - transform.position;
        //var distance = diff.magnitude;
        //if (distance > range)
        //{
        //    return false;
        //}

        //Angle
        var targetAngle = Vector3.Angle(diff, transform.forward);
        if (targetAngle > _visionAngle / 2)
        {
            return false;
        }

        //obstacle
        if (Physics.Raycast(transform.position, diff.normalized, _visionRange,obstacleMask))
        {
            return false;
        }
        
        return true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,_visionRange);
        
        Gizmos.DrawRay(transform.position,Quaternion.Euler(0,_visionAngle/2,0)*transform.forward*_visionRange);
        Gizmos.DrawRay(transform.position,Quaternion.Euler(0,-_visionAngle/2,0)*transform.forward*_visionRange);
     


    }
}
