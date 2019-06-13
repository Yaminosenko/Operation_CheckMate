using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class TeamManager : MonoBehaviour
{
    public GameObject[] squad1;//La constitution de l'équipe
    public GameObject[] squad2;//La constitution de l'équipe
    public int soldierTurn = 0;//de 0 à 3, [0, 1, 2 ,3]
    public int soldierActionPoint = 2;
    public string _name;
    public bool playerNum = false;
    public bool Launched = false;

    public Camera cam1;
    public Camera cam2;

    void Start ()
    {
        Invoke ("Launcher", 2f);
    }

    void Launcher ()
    {
        Launched = true;
    }

    void Update ()
    {
        if (Launched == true)
            PrimaryLoop();
    }

    void PrimaryLoop()
    {
        if (playerNum == false)
        {
            cam1.enabled = true;
            cam2.enabled = false;
            //Debug.Log("tour joueur 1");
            //squad1 = GameObject.FindGameObjectsWithTag("Player1");
            foreach (GameObject squadMate in squad1)
            {
                if(squadMate.GetComponent<navAgent>().myNumber == soldierTurn)
                {
                    squadMate.GetComponent<navAgent>().journeyEnabled = true;
                }
                else
                {
                    squadMate.GetComponent<navAgent>().journeyEnabled = false;
                }
            }
            //if (soldierTurn > 3)
            //{
            //    //soldierTurn = 0;
            //    Debug.Log("soldat 1 go !");
            //   // playerTurn();
            //}
        }

        if (playerNum == true)
        {
            //cam1.enabled = false;
            //cam2.enabled = true;
            //squad2 = GameObject.FindGameObjectsWithTag("Player2");
            foreach (GameObject squadMate in squad2)
            {
                if(squadMate.GetComponent<navAgent>().myNumber == soldierTurn)
                {
                    squadMate.GetComponent<navAgent>().journeyEnabled = true;
                }
                else
                {
                    squadMate.GetComponent<navAgent>().journeyEnabled = false;
                }
            }
            //if (soldierTurn > 3)
            //{
            //    soldierTurn = 0;
            //    playerTurn();
            //}
        }
    }

    //public void playerTurn ()
    //{
    //    playerNum = !playerNum;
    //}
}
