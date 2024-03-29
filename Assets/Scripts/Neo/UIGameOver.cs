using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public Button btnHome;
    public Button btnRegame;


    void Update()
    {
        btnHome.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == btnHome.gameObject);
        btnRegame.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == btnRegame.gameObject);
    }
}
