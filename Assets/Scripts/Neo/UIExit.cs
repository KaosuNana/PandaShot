using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIExit : MonoBehaviour
{
    public Button btnExit;
    public Button btnCancel;
    public Button btnPlay;

    void Start()
    {
        btnExit.onClick.AddListener(OnExitGame);
        btnCancel.onClick.AddListener(OnClose);
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnExit.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        btnExit.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == btnExit.gameObject);
        btnCancel.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == btnCancel.gameObject);
        if (Input.GetButtonDown("Cancel"))
            btnExit.onClick.Invoke();
    }


    private void OnClose()
    {
        this.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnPlay.gameObject);
    }

    private void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
