using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] List<Edge> childs;

    public Node previousNode=null;
    public float previousCost=float.PositiveInfinity;
    [SerializeField]float heuristic = float.PositiveInfinity;
    [SerializeField] float ClasicDistanceHeuristic= float.PositiveInfinity;

    private void Start()
    {
        for(int i=0; i<childs.Count;i++)
        {
            childs[i].UpdateValues(transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);
        if (childs!=null)
        {
            for (int i = 0; i < childs.Count; i++)
            {
                if (childs[i].target == null) continue;

                var diff = childs[i].target.transform.position - transform.position;
                var arrowPos = transform.position + diff * 0.8f;

                Gizmos.color = Color.white;
                Gizmos.DrawLine(transform.position, childs[i].target.transform.position);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(arrowPos, 0.25f);
            }
        }
       
    }
    public void ConnectNodes(Node otherNode,float newDistance)
    {
        if (childs==null)
        {
            childs = new List<Edge>();
        }

        var newEdge = new Edge(newDistance,otherNode);
        childs.Add(newEdge);
    }
    public void ClearConnections()
    {
        childs.Clear();
    }

    public List<Node> FindPath(Node destination)
    {
        if (destination == this)
        {
            //print("lo encontré de una!");
            return null;
        }
        
        //Esta lista almacena los nodos que queremos explorar
        var openSet = new List<Node>();
        
        //Me agrego a mi mismo para empezar la busqueda desde aca
        openSet.Add(this);

        //Recorro todos los objetos que quiero explorar
        for (int i = 0; i < openSet.Count; i++)
        {
            //Accedo al nodo que quiero explorar
            Node open = openSet[i];

            //Exploro el nodo verificando cada uno de los hijos del mismos
            for (int j = 0; j < open.childs.Count; j++)
            {
                //Accedo a una de las aristas del nodo a explorar (el abierto)
                Edge child = open.childs[j];

                //Primero verifico si el nodo al que se conecta la arista (target)
                //es el destino al que quiero llegar
                if (child.target == destination)
                {
                    //Le digo al nodo hijo que su padre (open) fue el primero en visitarlo
                    child.target.previousNode = open;


                    Node ActualNode = destination;
                    List<Node> PathFinded = new List<Node>();
                    
                    while(ActualNode!=this)
                    {
                        PathFinded.Add(ActualNode);
                        ActualNode = ActualNode.previousNode;
                    }
                    
                    //print("lo encontré");

                    PathFinded.Reverse();
                    return PathFinded;
                }

                //Verifico si este hijo del abierto no está en la lista de abierto, osea
                //verifico que no haya sido recorrido o que no se vaya a recorrer
                if (!openSet.Contains(child.target))
                {
                    //Le digo al nodo hijo que su padre (open) fue el primero en visitarlo
                    child.target.previousNode = open;
                    
                    //Si este hijo del abierto no es el destino puede ser un nodo
                    //que me lleve al destino, por lo que lo agrego al openSet para
                    //explorarlo mas tarde
                    openSet.Add(child.target);
                }
            }
        }
        return null;
    }

    public List<Node> FindPathDijkstra(Node destination)
    {

       
        if (destination == this)
        {
            print("lo encontré de una!");
            return null;
        }

        //Esta lista almacena los nodos que queremos explorar
        var openSet = new List<Node>();

        //Me agrego a mi mismo para empezar la busqueda desde aca
        openSet.Add(this);
        previousCost = 0;

        //Recorro todos los objetos que quiero explorar
        for (int i = 0; i < openSet.Count; i++)
        {
            //Accedo al nodo que quiero explorar
            Node open = openSet[i];
           

            //Exploro el nodo verificando cada uno de los hijos del mismos
            for (int j = 0; j < open.childs.Count; j++)
            {
               
                //Accedo a una de las aristas del nodo a explorar (el abierto)
                Edge child = open.childs[j];


                //Verifico por los costos si este nodo es valido
                
                //print("El nodo:"+open.name + " Le pregunta a"+ child.target.name+"por el costo de:"+child.target.previousCost);


                print("El nodo" + open.name + "(de ahora tiene):" + open.previousCost + " y la distancia hasta el siguiente es de: " + child.distance+"pero el costoprevio del target"+ child.target.name+" es" + child.target.previousCost);

                if (open.previousCost + child.distance < child.target.previousCost)
                {
                    child.target.previousCost = open.previousCost + child.distance;
                    child.target.previousNode = open;


                    openSet.Add(child.target);
                }

                             
            }
            
        }
        
        Node ActualNode = destination;

        if (ActualNode.previousNode == null)
        {
            print("no existe");
            return null;
        }
        else
        {
            print("existe");
            List<Node> PathFinded = new List<Node>();
            while (ActualNode != this)
            {
                if (PathFinded.Contains(ActualNode))
                {
                    Debug.LogError("Loop infinito");
                    return null;
                }
                PathFinded.Add(ActualNode);
                ActualNode = ActualNode.previousNode;
                
            }
            PathFinded.Reverse();
            return PathFinded;
        }




        
    }

    public List<Node> FindPathDijkstraOptimization(Node destination)
    {

        print("nombre del nodo origne" + gameObject.name);
        if (destination == this)
        {
            //print("lo encontré de una!");
            return null;
        }

        //Esta lista almacena los nodos que queremos explorar
        var openSet = new List<Node>();
       
        //Me agrego a mi mismo para empezar la busqueda desde aca
        openSet.Add(this);
        previousCost = 0;

        //Recorro todos los objetos que quiero explorar
        for (int i = 0; i < openSet.Count; i++)
        {
            //Accedo al nodo que quiero explorar
            Node open = openSet[i];
           

            //Exploro el nodo verificando cada uno de los hijos del mismos
            for (int j = 0; j < open.childs.Count; j++)
            {
                
                //Accedo a una de las aristas del nodo a explorar (el abierto)
                Edge child = open.childs[j];



                //Verifico por los costos si este nodo es valido
                print("El nodo" + open.name + "(de ahora tiene):" + open.previousCost + " y la distancia hasta el siguiente es de: " + child.distance + "pero el costoprevio del target" + child.target.name + " es" + child.target.previousCost);

                if (open.previousCost + child.distance < child.target.previousCost && open.previousCost + child.distance< destination.previousCost)
                {
                    child.target.previousCost = open.previousCost + child.distance;
                    child.target.previousNode = open;


                    openSet.Add(child.target);
                }


            }

        }
        

        Node ActualNode = destination;

        if (ActualNode.previousNode == null)
        {
            print("no existe");
            return null;
        }
        else
        {
            print("existe");
            List<Node> PathFinded = new List<Node>();
            while (ActualNode != this)
            {
                
                PathFinded.Add(ActualNode);
                ActualNode = ActualNode.previousNode;

            }
            PathFinded.Reverse();
            return PathFinded;
        }





    }

    public List<Node> FindPathAStar(Node destination)
    {
        ClasicDistanceHeuristic = (transform.position - destination.transform.position).magnitude;

        //print("nombre del nodo origne" + gameObject.name);
        if (destination == this)
        {
            print("lo encontré de una!");
            return null;
        }

        //Esta lista almacena los nodos que queremos explorar
        //var openSet = new List<Node>();
        var openSet = new ColaPrioridad();
        openSet.InicializarCola();

        //Me agrego a mi mismo para empezar la busqueda desde aca
        previousCost = 0;
        openSet.AcolarPrioridad(this, previousCost + ClasicDistanceHeuristic);

        
        //Recorro todos los objetos que quiero explorar
        while(!openSet.ColaVacia())
        {
            //Accedo al nodo que quiero explorar

            Node open = openSet.Primero();

            print("open set abierto analizando" + open.name );


            if (open == destination)
            {
                print("lo encontre en la lista de abiertos");
                break;
            }

            open.ClasicDistanceHeuristic = (transform.position - destination.transform.position).magnitude;
            print("la heuristica del abierto es:"+ open.ClasicDistanceHeuristic);

            //Exploro el nodo verificando cada uno de los hijos del mismos
            for (int j = 0; j < open.childs.Count; j++)
            {

                //Accedo a una de las aristas del nodo a explorar (el abierto)
                Edge child = open.childs[j];
                print("hijos de :" + open.name+" son:"+child.target.name);


                //Verifico por los costos si este nodo es valido

                var totalCoast = open.previousCost + child.distance;
                child.target.ClasicDistanceHeuristic = (child.target.transform.position - destination.transform.position).magnitude;
                var ValueToConsider = totalCoast + child.target.ClasicDistanceHeuristic; // Costo TOTAL del vecino
                
               

                print("Con un costo total de" + totalCoast + "y un valor a considerar de:" + ValueToConsider+ child.distance);

                if (ValueToConsider < child.target.previousCost+child.target.ClasicDistanceHeuristic)
                {
                    child.target.previousCost = totalCoast;
                    child.target.previousNode = open;

                    print("Agrego a la lista de abiertos a:"+child.target);
                    openSet.Desacolar();
                    openSet.AcolarPrioridad(child.target,child.target.previousCost+child.target.ClasicDistanceHeuristic);
                }


            }
            
            print ("el que sigue enj prioridad es"+openSet.Primero());
            print("la cola esta vacia?" + openSet.ColaVacia());
          
            

        }


        Node ActualNode = destination;
        print("Actual node es" + ActualNode.name);

        if (ActualNode.previousNode == null)
        {
            print("no existe");
            return null;
        }
        else
        {
            print("existe");
            List<Node> PathFinded = new List<Node>();
            while (ActualNode != this)
            {

                PathFinded.Add(ActualNode);
                ActualNode = ActualNode.previousNode;

            }
            PathFinded.Reverse();
            return PathFinded;
        }


    }

    
}

[Serializable]
public class Edge
{
    public float distance;
    public Node target;


    public Edge(float newDistance,Node Othernode)
    {
        distance = newDistance;
        target = Othernode;
    }

    public void UpdateValues(Vector3 origen)
    {
        distance = (origen-target.transform.position).magnitude;
    }


}

