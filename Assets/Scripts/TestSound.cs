using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour,IListenable
{
    [SerializeField] private SoundReach soundType;
     public event Action<IListenable> OnSoundPlay = delegate{ };

     private void Awake()
     {
         SuscribeToSoundSensor();
     }

     void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnSoundPlay(this);
        }
    }

   
    public SoundReach SoundType()
    {
        return soundType;
    }

    public void SuscribeToSoundSensor()
    {
        OnSoundPlay+=GameObject.Find("Sensor").GetComponent<SoundSensor>().SoundHandler;
    }

    public Vector3 SoundPosition()
    {
        return transform.position;
    }
}
