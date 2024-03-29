using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITip : MonoBehaviour
{
    public Button closeBtn;
    public Button playButton;
    public GameObject daily;
    public Button dailybtn;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeBtn.gameObject);
        closeBtn.onClick.AddListener(() =>
        {
            //AudioKit.PlaySound(SoundData.path + SoundData.ButtonClick);
            PlayerPrefs.SetInt("TipTimes", PlayerPrefs.GetInt("TipTimes", 3) - 1);
            gameObject.SetActive(false);
            if (daily.transform.localScale.x == 1)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(dailybtn.gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(playButton.gameObject);
            }

            AdsManager.Instance.IsAc = false;
        });
    }
}
