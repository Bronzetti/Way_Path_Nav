using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //tranfere os dados para diferentes classes de forma que eles se entendam
public struct Link //struct para construção das conexões de um ponto ao outro
{
    public enum direction { UNI, BI } //variável de direção com sentidos e direções para grafos. Sendo UNI um único sentido e BI, ambos sentidos
    public GameObject node1; //ponto inicial
    public GameObject node2; //segundo ponto 
    public direction dir; //variável de direção
}
public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints; //array
    public Link[] links; //array de informação do link do struct criado anteriormente 
    public Graph graph = new Graph(); //pega as informações do arquivo Graph
    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length > 0) //se o tamanho do meu array for diferente, ou seja, maior que zero
        { 
            foreach (GameObject wp in waypoints)//completa o array criado
            { 
                graph.AddNode(wp); //abastecimento do array, ponto no mapa 
            } 
            foreach (Link l in links) //cada ponto colocado no mapa, é possível adicionar uma linha ligando um ponto ao outro
            { 
                graph.AddEdge(l.node1, l.node2); //adição da aresta entre dois pontos
                if (l.dir == Link.direction.BI)// condição se a aresta será ida e volta 
                    graph.AddEdge(l.node2, l.node1); //ponto 2 volta para ponto 1, trajeto da volta 
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        graph.debugDraw(); //Desenha a aresta no cena
    }
}
