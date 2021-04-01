using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour //movimentação do tank
{
    Transform goal;//pontos que o objeto irá
    float speed= 5.0f;//velocidade do tank
    float accuracy= 1.0f;//aproximação do objeto com o ponto pra poder já rotacionar para o próximo
    float rotSpeed= 2.0f;//velocidade de rotação do objeto
    public GameObject wpManager;//ciração do gameobject wpManager 
    GameObject[] wps; // array com index wps
    GameObject currentNode;//gameobject de cada nó
    int currentWP= 0; //necessita ser igual a zero inicialmente para iniciar a movimentação
    Graph g;//refenencia meu arquivo Graph 

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;//gerenciamento dos componentes dentro do WPManager 
        g = wpManager.GetComponent<WPManager>().graph; //cada waypoint possui uma informação desse gerenciamento dentro do array
        currentNode = wps[0];//matriz abastecida com cada ponto criado no mapa para que seja feito a trajetória correta

    }
    public void GoToHeli()//Método 
    {
        g.AStar(currentNode, wps[1]);// g.ASrtar e local aonde tá criando meu mapa
        currentWP = 0; //ponto final no qual estará percorrendo 
    }
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[6]); 
        currentWP = 0; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())//condicional que pega o tamanho certo do path que o objeto caminhará
            return; //pega o node mais próximo do momento atual caminhando pro próximo if

        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
            {
            currentWP++;
            }

        if (currentWP < g.getPathLength()) //currentWP com tamanho inferior ao getPath
        { 
            goal = g.getPathPoint(currentWP).transform;// verifica o atual e já calcula pro próximo
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);//movimentação mirando pro próximo ponto com rotação
            Vector3 direction = lookAtGoal - this.transform.position; 
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 
                Time.deltaTime * rotSpeed); //Slerp com movimentação mais suave
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
