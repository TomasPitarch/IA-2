using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSensor : MonoBehaviour
{
    public event Action<Vector3> OnSoundEventCloseRange=delegate(Vector3 vector3) {  };
    public event Action<Vector3> OnSoundEventLongRange=delegate(Vector3 vector3) {  };

    [SerializeField] 
    private float Range;
   
    
    public void SoundHandler(IListenable soundSource)
    {
        var soundDistance = (transform.position - soundSource.SoundPosition()).magnitude;

        switch (soundSource.SoundType())
        {
            case SoundReach.Short:
            {
                if (soundDistance < Range)
                {
                    OnSoundEventCloseRange(soundSource.SoundPosition());
                }
                break;
            }
            
            case SoundReach.Long:
            {
                OnSoundEventLongRange(soundSource.SoundPosition());
                break;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position,Range);
        
    }
    
    
    
}

public enum SoundReach
{
    Short,
    Long
}

public interface IListenable
{
    public event Action<IListenable> OnSoundPlay;
    public SoundReach SoundType();
    void SuscribeToSoundSensor();
    public Vector3 SoundPosition();
    
    
}
