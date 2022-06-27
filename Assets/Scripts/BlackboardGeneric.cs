using System.Collections.Generic;
using UnityEngine;

public class BlackboardGeneric : MonoBehaviour
{
    Dictionary<string, object> memories = new Dictionary<string, object>();

    public Dictionary<string, object> Memories { get => memories; set => memories = value; }

    public GenericMemory<T> Get<T>(string name)
    {
        if (Memories.ContainsKey(name))
        {
            return (GenericMemory<T>)Memories[name];
        }
        else
        {
            return null;
        }
        

    }
    public void Set<T>(string name, T theObject)
    {
        if (Memories.ContainsKey(name))
        {
            Memories[name] = new GenericMemory<T>(theObject);
        }
        else
        {
            GenericMemory<T> Aux = new GenericMemory<T>(theObject);
            Memories.Add(name, Aux);
        }

    }

    //
    //   GenericMemory<Vector3> memoriaVida = Get<Vector3>("vida");
    //   memoriaVida.Get();
   
}
public class GenericMemory<T>
{
    private T value;

    public GenericMemory(T newValue)
    {
        value = newValue;
    }
    public T Get()
    {
        return value;
    }
}