using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button btnPlay;
    public Button btnShop;
    public GameObject daily;
    public GameObject tipPanel;
    void Start()
    {
        if (daily.transform.localScale != Vector3.one)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(btnPlay.gameObject);
        }
        if (PlayerPrefs.GetInt("TipTimes", 3) > 0 && AdsManager.Instance.IsAc)
            tipPanel.SetActive(true);
    }

    void Update()
    {
        btnPlay.transform.GetChild(0).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == btnPlay.gameObject);
        btnShop.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == btnShop.gameObject);
        Debug.Log(EventSystem.current.currentSelectedGameObject);
    }
}
