using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaPrioridad 
{   
    NodoPrioridad mayorPrioridad;

    public void AcolarPrioridad(Node x, float prioridad)
    {
        NodoPrioridad nuevo = new NodoPrioridad();
        nuevo.Info = x;
        nuevo.Prioridad = prioridad;
        if (mayorPrioridad == null || prioridad > mayorPrioridad.Prioridad)
        {
            nuevo.Sig = mayorPrioridad;
            mayorPrioridad = nuevo;
        }
        else
        {
            NodoPrioridad aux = mayorPrioridad;
            while (aux.Sig != null && aux.Sig.Prioridad >= prioridad)
            {
                aux = aux.Sig;
            }
            nuevo.Sig = aux.Sig;
            aux.Sig = nuevo;
        }

    }



    public bool ColaVacia()
    {
        return (mayorPrioridad == null);

    }

    public void Desacolar()
    {
        mayorPrioridad = mayorPrioridad.Sig;
    }

    public void InicializarCola()
    {
        mayorPrioridad = null;
    }

    public Node Primero()
    {
        return mayorPrioridad.Info;

    }

    public float Prioridad()
    {
        return mayorPrioridad.Prioridad;
    }
    
}
public class NodoPrioridad
{
    Node info;
    float prioridad;
    NodoPrioridad sig;

    public Node Info { get => info; set => info = value; }
    public float Prioridad { get => prioridad; set => prioridad = value; }
    public NodoPrioridad Sig { get => sig; set => sig = value; }
}