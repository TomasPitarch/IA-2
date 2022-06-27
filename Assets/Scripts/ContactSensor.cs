using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ContactSensor : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==mask)
        {
            Debug.Log("Entro en contacto");
        }
    }
}
