using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StairCase : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Transform[] Points;
    [SerializeField] private GameObject player;
    [SerializeField] Transport transport;
    [SerializeField] Poster poster;

    public Action ResetCheckPoint;

    CheckPoint checkPoint;

    private bool teleport = false;

    private bool isChecking = false;
    [SerializeField] private float distanceOffset = 1.5f;



    private void Start()
    {
        //the first one is the normal poster as a guide
        poster.ModifyPoster(0, true);


    }


    public void CheckPointUpdate(Transform point, int times)
    {
        //update poster check: L1H for 2rd poster, L2H for 3rd poster
        if (point.name == "L1H")
        {
            Debug.Log("reach L1H");
            poster.visitedPoster[0] = true;
            poster.RandomPoster(1);

        }

        else if (point.name == "L2L")
        {
            Debug.Log("reach L2L");

            //check if it is the first time reach here, if no, check poster 2, 
            if (times % 2 == 0 && times != 0)//even time, check
            {
                //check the poster is abnormal
                if (poster.getPosterValue(1) == false)
                {
                    //go to L3L
                    Debug.Log($"reach L2L, right way, teleport to L3L, with times{times}");
                    teleport = true;

                    transport.TeleportOnY(player, Points[(int)StairCasePoint.L3L].position);
                }

                //true means it is the right way
                //if it is the right way
                poster.visitedPoster[1] = true;

            }
        }

        else if (point.name == "L2H")
        {
            Debug.Log("reach L2H");

            //check the right way or not based on poster
            if (poster.getPosterValue(1))
            {
                //true means it is the right way
                //if it is the right way
                poster.RandomPoster(2);
                poster.visitedPoster[1] = true;

            }
            else
            {
                //player go to the wrong way,
                Debug.Log("reach L2H,goes the wrong way, teleport to L1H");
                ResetCheckPoint();
                poster.visitedPoster[1] = false;
                transport.TeleportOnY(player, Points[(int)StairCasePoint.L1H].position);
            }
        }


        else if (point.name == "L3L")
        {
            Debug.Log("reach L3L");

            if (teleport)
            {
                Debug.Log("from teleport, ignore");
                teleport = false;
            }

            //check if it is the first time reach here, if no, check poster 3, 
            else if (times % 2 == 0 && times != 0)//odd, first time
            {
                
                //check the poster is abnormal
                if (poster.getPosterValue(2) == false)
                {
                    //go to FL
                    Debug.Log($"reach L3L, right way, teleport to FL, with times: {times}");
                    teleport = true;
                    transport.TeleportOnY(player, Points[(int)StairCasePoint.FL].position);
                }

                //true means it is the right way
                //if it is the right 
                poster.visitedPoster[2] = true;

            }
        }

        else if (point.name == "L3H")
        {
            Debug.Log("reach L3H");

            //check the right way or not based on poster
            if (poster.getPosterValue(2))
            {
                //true means it is the right way
                //if it is the right way
                poster.visitedPoster[2] = true;

            }
            else
            {
                //player go to the wrong way,
                Debug.Log("reach L3H,goes the wrong way, teleport to L1H");
                ResetCheckPoint();
                poster.visitedPoster[2] = false;
                poster.visitedPoster[1] = false;
                point.transform.GetComponent<CheckPoint>().ResetTimes();
                
                transport.TeleportOnY(player, Points[(int)StairCasePoint.L1H].position);
            }
        }


    }

    public void GameFinish()
    {
        Debug.Log("end level");
        levelManager.EndLevel();
    }

    enum StairCasePoint
    {
        L1H,
        L2L,
        L2H,
        L3L,
        L3H,
        FL,
        Finish
    }
}
