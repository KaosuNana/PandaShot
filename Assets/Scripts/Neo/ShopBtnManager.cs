using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopBtnManager : MonoBehaviour
{
    public Button[] intensifyButtons;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(intensifyButtons[0].gameObject);
    }

    void Update()
    {
        for (int i = 0; i < intensifyButtons.Length; i++)
        {
            if(i == 0)
                intensifyButtons[i].transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == intensifyButtons[i].gameObject);
            else
                intensifyButtons[i].transform.GetChild(2).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == intensifyButtons[i].gameObject);
        }
    }
}
