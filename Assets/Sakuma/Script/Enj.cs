﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enj : MonoBehaviour
{




    [SerializeField]
    private GameObject[] sterPos;

    [SerializeField]
    private GameObject lineObj;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject lineParent;


    [SerializeField]
    private GameObject padControllerObj;
    private PadController2 padController;
    [SerializeField]
    private GameObject gameControllerObj;
    private GameController gameController;

    [SerializeField]
    private GameObject gageObj;
    public  Image image;

    public int summonNum;

    [SerializeField]
    //public float attacktime;
    public float time=5;

    [SerializeField]
    private GameObject sumonbdobj;


    [SerializeField]
    private GameObject StatusManagerObj;
    private StatusManager statusManager;




    private void Update()
    {

        if (gameController.gameMode ==2&&padController.sumonMode ==false)
        {
            time -= Time.deltaTime;
            image.fillAmount =1- time / statusManager.gageSpeed;
        }
        if (time < 0)
        {
            sumonbdobj.SetActive(false);
            padController.sumonbd = false;
            time = statusManager.gageSpeed;
            image.fillAmount = 0;
            gameController.ModeChange(4, 0);
            AudioManager.Instance.PlaySE(AudioManager.SeName.failed);
            //Debug.Log(123);
            padController.BoardReset();
        }

    }


    public void NextGame()
    {
        BoardReset();
        RandSelect();
    }


    private void Start()
    {
        statusManager = StatusManagerObj.GetComponent<StatusManager>();
        time = statusManager.gageSpeed ;
        gameController = gameControllerObj.GetComponent<GameController>();
        padController = padControllerObj.GetComponent<PadController2>();
        image = gageObj.GetComponent<Image>();
    }



    public void RandSelect()
    {

        time = statusManager.gageSpeed;
        summonNum = Random.Range(0, gameController.nomalAttack.Length);
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();


        for (int i = 0; i < gameController.nomalAttack[summonNum].Code.Length; i++)
        {
            int num = 0;
            for (int a = 1; a <= 14; a++)
            {
                for (int b = a + 1; b <= 14; b++)
                {
                    num++;
                    if (gameController.nomalAttack[summonNum].Code[i] == num)
                    {
                        sterPos[a-1].GetComponent<Image>().enabled = true;
                        sterPos[b-1].GetComponent<Image>().enabled = true;

                        GameObject obj = (GameObject)Instantiate(lineObj, transform.position, Quaternion.identity, lineParent.transform);
                        UILineRenderer data2 = obj.GetComponent<UILineRenderer>();
                        data2.points[0] = sterPos[a-1].GetComponent<RectTransform>().anchoredPosition;
                        data2.points[1] = sterPos[b-1].GetComponent<RectTransform>().anchoredPosition;

                    }

                }
            }


        }

    }

    public  void BoardReset()
    {

        for (int i = 0; i < sterPos.Length ; i++)
        {

            sterPos[i].GetComponent<Image>().enabled = false ;

        }
        foreach (Transform n in lineParent.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }
}
