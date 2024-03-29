using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LvManager : MonoBehaviour
{
    public Button[] LvBtns;
    public Transform lvpanel;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < LvBtns.Length; i++)
        {
            if(i == 0)
                LvBtns[i].transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject);
            else
                LvBtns[i].transform.GetChild(3).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject);
            if (EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject && i >= 0 && i <= 3)
            {
                lvpanel.transform.DOLocalMoveY(-1050, 0.5f);
            }else if (EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject && i >= 4 && i <= 7)
            {
                lvpanel.transform.DOLocalMoveY(-650, 0.5f);
            }else if (EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject && i >= 8 && i <= 11)
            {
                lvpanel.transform.DOLocalMoveY(-250, 0.5f);
            }else if (EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject && i >= 12 && i <= 15)
            {
                lvpanel.transform.DOLocalMoveY(150, 0.5f);
            }else if (EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject && i >= 16 && i <= 19)
            {
                lvpanel.transform.DOLocalMoveY(550, 0.5f);
            }else if (EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject && i >= 20 && i <= 23)
            {
                lvpanel.transform.DOLocalMoveY(950, 0.5f);
            }else if (EventSystem.current.currentSelectedGameObject == LvBtns[i].gameObject && i == 24)
            {
                lvpanel.transform.DOLocalMoveY(1050, 0.5f);
            }
        }


    }
}
