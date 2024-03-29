using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpHandler : MonoBehaviour
{
    [Header("NotEnough")]
    public GameObject noEnough;

    private int index = -1;
    public Button enough;
    [Header("Pause")]
    public Button pHome;
    public Button pRegame;

    [HideInInspector]
    public Menu pauseMenu, slowTimeMenu, gameOverMenu, keepPlayingMenu, resume;
    [HideInInspector]
    public Image GreenButton, GrayButton;
    MenuManager menuMngr;

    public Text teslaWeaponNumberText, laserWeaponNumberText, bladesWeaponNumberText, bombWeaponNumberText;
    public Text TeslaWeaponPriceText, LaserWeaponPriceText, BladesWeaponPriceText, BombWeaponPriceText;
    public Text StarsNumberTextInSlowScreen;
    Color buttonUseActiveColor = new Color(0.92941f, 0.93333f, 0.0f);
    public GameObject teslaWeapon, laserWeapon, bladesWeapon, bombWeapon, starsCounter;
    public Button teslabuttonUse, laserbuttonUse, bladesbuttonUse, bombbuttonUse;
    public Button teslabuttonBuy, laserbuttonBuy, bladesbuttonBuy, bombbuttonBuy;
    public static bool gameStarted = false;
    public bool bossDestroyed = false;
    /// <summary>
    /// The type of the popup. 0 - no popup, 1 - slow time screen, 2 - paused game, 3 - keep playing, 4 - game over, 5 - popup not allowed, 6 - no video available
    /// </summary>
    public static int popupType = 5;
    bool videoReady = false;
    bool progressQuit;

    static PopUpHandler instance;
    public static PopUpHandler Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(PopUpHandler)) as PopUpHandler;

            return instance;
        }
    }

    void Update()
    {
        pHome.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == pHome.gameObject);
        pRegame.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == pRegame.gameObject);
        teslabuttonUse.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == teslabuttonUse.gameObject);
        teslabuttonBuy.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == teslabuttonBuy.gameObject);
        laserbuttonUse.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == laserbuttonUse.gameObject);
        laserbuttonBuy.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == laserbuttonBuy.gameObject);
        bladesbuttonUse.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == bladesbuttonUse.gameObject);
        bladesbuttonBuy.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == bladesbuttonBuy.gameObject);
        bombbuttonUse.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == bombbuttonUse.gameObject);
        bombbuttonBuy.transform.GetChild(1).gameObject.SetActive(EventSystem.current.currentSelectedGameObject == bombbuttonBuy.gameObject);
    }

    void ShowNoGold()
    {
        noEnough.SetActive(true);
        PlayerPrefs.SetInt("OpenEnough",1);
    }
    public void SetFocus()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(teslabuttonUse.gameObject.activeSelf ? teslabuttonUse.gameObject : teslabuttonBuy.gameObject);

    }
    void Awake()
    {
        TeslaWeaponPriceText.text = PandaPlane.teslaWeaponPrice.ToString();
        LaserWeaponPriceText.text = PandaPlane.laserWeaponPrice.ToString();
        BladesWeaponPriceText.text = PandaPlane.bladesWeaponPrice.ToString();
        BombWeaponPriceText.text = PandaPlane.bombWeaponPrice.ToString();

        //teslaWeapon = GameObject.Find("PowerUpTeslaHolder");
        //laserWeapon = GameObject.Find("PowerUpLaserHolder");
        //bladesWeapon = GameObject.Find("PowerUpBladesHolder");
        //bombWeapon = GameObject.Find("PowerUpBombHolder");

        //teslaWeaponNumberText = GameObject.Find("TeslaNumberText").GetComponent<Text>();
        //laserWeaponNumberText = GameObject.Find("LaserNumberText").GetComponent<Text>();
        //bladesWeaponNumberText = GameObject.Find("BladesNumberText").GetComponent<Text>();
        //bombWeaponNumberText = GameObject.Find("BombNumberText").GetComponent<Text>();

        //StarsNumberTextInSlowScreen = GameObject.Find("StarsNumberTextInSlowScreen").GetComponent<Text>();

    }

    // Use this for initialization
    void Start()
    {
        enough.onClick.AddListener(CloseEnough);
        menuMngr = GameObject.Find("Canvas").GetComponent<MenuManager>();
        starsCounter = GameObject.Find("StarsCounterHolder");
        UpdateStateOfUseShopButtons();
        //UpdateStateOfBuyShopButtons();
        UpdateStateOfWeaponsAndStars();
        transform.Find("KeepPlayingPopUp/AnimationHolder/PopUpHolder/AnimationHolder/Or").gameObject.SetActive(false);
        transform.Find("KeepPlayingPopUp/AnimationHolder/PopUpHolder/AnimationHolder/WatchVideoButton").gameObject.SetActive(false);
        //WebelinxCMS.Instance.IsVideoRewardAvailable(4);
        SetNav();
    }

    void CloseEnough()
    {
        noEnough.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        if(index == 1)
            EventSystem.current.SetSelectedGameObject(teslabuttonBuy.gameObject);        
        if(index == 2)
            EventSystem.current.SetSelectedGameObject(laserbuttonBuy.gameObject);        
        if(index == 3)
            EventSystem.current.SetSelectedGameObject(bladesbuttonBuy.gameObject);        
        if(index == 4)
            EventSystem.current.SetSelectedGameObject(bombbuttonBuy.gameObject);
        PlayerPrefs.SetInt("OpenEnough", 0);
    }
    public void SetNav()
    {
        teslabuttonBuy.gameObject.SetActive(PandaPlane.teslaWeaponNumber == 0);
        teslabuttonUse.gameObject.SetActive(!teslabuttonBuy.gameObject.activeSelf);
        laserbuttonBuy.gameObject.SetActive(PandaPlane.laserWeaponNumber == 0);
        laserbuttonUse.gameObject.SetActive(!laserbuttonBuy.gameObject.activeSelf);
        bladesbuttonBuy.gameObject.SetActive(PandaPlane.bladesWeaponNumber == 0);
        bladesbuttonUse.gameObject.SetActive(!bladesbuttonBuy.gameObject.activeSelf);
        bombbuttonBuy.gameObject.SetActive(PandaPlane.bombWeaponNumber == 0);
        bombbuttonUse.gameObject.SetActive(!bombbuttonBuy.gameObject.activeSelf);
        teslabuttonUse.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = null,
            selectOnRight = PandaPlane.laserWeaponNumber > 0 ? laserbuttonUse : laserbuttonBuy
        };
        teslabuttonBuy.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = null,
            selectOnRight = PandaPlane.laserWeaponNumber > 0 ? laserbuttonUse : laserbuttonBuy
        };
        laserbuttonUse.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = PandaPlane.teslaWeaponNumber > 0 ? teslabuttonUse : teslabuttonBuy,
            selectOnRight = PandaPlane.bladesWeaponNumber > 0 ? bladesbuttonUse : bladesbuttonBuy
        };
        laserbuttonBuy.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = PandaPlane.teslaWeaponNumber > 0 ? teslabuttonUse : teslabuttonBuy,
            selectOnRight = PandaPlane.bladesWeaponNumber > 0 ? bladesbuttonUse : bladesbuttonBuy
        };
        bladesbuttonUse.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = PandaPlane.laserWeaponNumber > 0 ? laserbuttonUse : laserbuttonBuy,
            selectOnRight = PandaPlane.bombWeaponNumber > 0 ? bombbuttonUse : bombbuttonBuy
        };
        bladesbuttonBuy.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = PandaPlane.laserWeaponNumber > 0 ? laserbuttonUse : laserbuttonBuy,
            selectOnRight = PandaPlane.bombWeaponNumber > 0 ? bombbuttonUse : bombbuttonBuy
        };
        bombbuttonUse.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = PandaPlane.bladesWeaponNumber > 0 ? bladesbuttonUse : bladesbuttonBuy,
            selectOnRight = null
        };
        bombbuttonBuy.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = null,
            selectOnDown = pHome,
            selectOnLeft = PandaPlane.bladesWeaponNumber > 0 ? bladesbuttonUse : bladesbuttonBuy,
            selectOnRight = null
        };
        pHome.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = PandaPlane.teslaWeaponNumber > 0 ? teslabuttonUse : teslabuttonBuy,
            selectOnDown = null,
            selectOnLeft = null,
            selectOnRight = pRegame
        };
        pRegame.navigation = new Navigation()
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = PandaPlane.teslaWeaponNumber > 0 ? teslabuttonUse : teslabuttonBuy,
            selectOnDown = null,
            selectOnLeft = pHome,
            selectOnRight = null
        };
    }

    public void IsKeepPlayingVideoAvailable(bool status)
    {
        if (status)
        {
            transform.Find("KeepPlayingPopUp/AnimationHolder/PopUpHolder/AnimationHolder/Or").gameObject.SetActive(true);
            transform.Find("KeepPlayingPopUp/AnimationHolder/PopUpHolder/AnimationHolder/WatchVideoButton").gameObject.SetActive(true);
            videoReady = true;
        }
        else
            ShowNoVideoAvailablePopUp();
    }

    public void ShowWatchVideo()
    {
        AdsManager.Instance.ShowVideoReward(4);
    }

    public void WatchVideoCompleted(string nista)
    {
        //		Ressurect();
        PandaPlane.Instance.NewPlane();
    }

    public void UpdateStateOfWeaponsAndStars()
    {
        teslaWeaponNumberText.text = PandaPlane.teslaWeaponNumber.ToString();
        laserWeaponNumberText.text = PandaPlane.laserWeaponNumber.ToString();
        bladesWeaponNumberText.text = PandaPlane.bladesWeaponNumber.ToString();
        bombWeaponNumberText.text = PandaPlane.bombWeaponNumber.ToString();

        StarsNumberTextInSlowScreen.text = PandaPlane.stars.ToString();
    }

    public void UpdateStateOfBuyShopButtons()
    {

        if (PandaPlane.teslaWeaponPrice <= PandaPlane.stars)
        {
            //teslaWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
            teslabuttonBuy.GetComponent<Image>().sprite = GreenButton.sprite;
        }
        else
        {
            //teslaWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
            teslabuttonBuy.GetComponent<Image>().sprite = GrayButton.sprite;
        }

        if (PandaPlane.laserWeaponPrice <= PandaPlane.stars)
        {
            //laserWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
            laserbuttonBuy.GetComponent<Image>().sprite = GreenButton.sprite;
        }
        else
        {
            //laserWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
            laserbuttonBuy.GetComponent<Image>().sprite = GrayButton.sprite;
        }

        if (PandaPlane.bladesWeaponPrice <= PandaPlane.stars)
        {
            //bladesWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
            bladesbuttonBuy.GetComponent<Image>().sprite = GreenButton.sprite;
        }
        else
        {
            //bladesWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
            bladesbuttonBuy.GetComponent<Image>().sprite = GrayButton.sprite;
        }

        if (PandaPlane.bombWeaponPrice <= PandaPlane.stars)
        {
            //bombWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
            bombbuttonBuy.GetComponent<Image>().sprite = GreenButton.sprite;
        }
        else
        {
            //bombWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
            bombbuttonBuy.GetComponent<Image>().sprite = GrayButton.sprite;
        }


    }

    public void ShowPauseScreen()
    {
        PlaneManager.Instance.menuIsActive = true;
        Debug.Log("TEST ShowPauseScreen poziv");
        menuMngr.ShowMenu(pauseMenu);
        popupType = 2;
        Time.timeScale = 0;
        Debug.Log("TEST timescale na 0");
    }

    void UpdateStateOfUseShopButtons()
    {
        if (PandaPlane.teslaLvl < 1 && PandaPlane.laserLvl < 1 && PandaPlane.bladesLvl < 1 && PandaPlane.bombLvl < 1)
        {
            starsCounter.SetActive(false);
        }

        if (gameStarted)
        {
            if (PandaPlane.teslaLvl < 1)
            {
                teslaWeapon.SetActive(false);
            }
            else if (PandaPlane.teslaWeaponNumber > 0)
            {
                teslaWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
                teslaWeapon.transform.GetChild(0).Find("PowerUpIcon").GetComponent<Button>().interactable = true;
            }

            if (PandaPlane.laserLvl < 1)
            {
                laserWeapon.SetActive(false);
            }
            else if (PandaPlane.laserWeaponNumber > 0)
            {
                laserWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
                laserWeapon.transform.GetChild(0).Find("PowerUpIcon").GetComponent<Button>().interactable = true;
            }

            if (PandaPlane.bladesLvl < 1)
            {
                bladesWeapon.SetActive(false);
            }
            else if (PandaPlane.bladesWeaponNumber > 0)
            {
                bladesWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
                bladesWeapon.transform.GetChild(0).Find("PowerUpIcon").GetComponent<Button>().interactable = true;
            }

            if (PandaPlane.bombLvl < 1)
            {
                bombWeapon.SetActive(false);
            }
            else if (PandaPlane.bombWeaponNumber > 0)
            {
                bombWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
                bombWeapon.transform.GetChild(0).Find("PowerUpIcon").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (PandaPlane.teslaLvl < 1)
            {
                teslaWeapon.SetActive(false);
            }
            else
            {
                //teslaWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
            }

            if (PandaPlane.laserLvl < 1)
            {
                laserWeapon.SetActive(false);
            }
            else
            {
                //laserWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
            }

            if (PandaPlane.bladesLvl < 1)
            {
                bladesWeapon.SetActive(false);
            }
            else
            {
                //bladesWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
            }

            if (PandaPlane.bombLvl < 1)
            {
                bombWeapon.SetActive(false);
            }
            else
            {
                //bombWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
            }


            slowTimeMenu.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            slowTimeMenu.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

    }

    public void ShowSlowTimeScreen()
    {
        if (PandaPlane.health <= 0)
        {
            ShowGameOverScreen();
        }
        else
        {
            //PlaneManager.Instance.menuIsActive = true; //IZMENA
            //StartCoroutine(Interface.Instance.SlowTime());
            menuMngr.ShowMenu(slowTimeMenu);
            UpdateStateOfWeaponsAndStars();
            UpdateStateOfBuyShopButtons();
            UpdateStateOfUseShopButtons();
        }
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 1;
        SoundManager.Instance.Stop_GameplayMusic();
        SoundManager.Instance.Stop_BossMusic();
        SoundManager.Instance.Stop_BossPlaneMovement();
        SoundManager.Instance.Stop_BossShipMovement();
        SoundManager.Instance.Stop_BossTankMovement();
        menuMngr.ShowMenu(gameOverMenu);
        if (PandaPlane.highScore > PandaPlane.score)
        {
            //GameObject.Find("HighScoreStamp").SetActive(false);
            string uzengije = (PandaPlane.stars + 10) + "#" + (PandaPlane.highScore - 20) + "#" + (PandaPlane.laserWeaponNumber + 5) + "#" + (PandaPlane.teslaWeaponNumber + 5) + "#" + (PandaPlane.bladesWeaponNumber + 5) + "#" + (PandaPlane.bombWeaponNumber + 5);
            PlayerPrefs.SetString("Uzengije", uzengije);
            PlayerPrefs.Save();
        }
        else
        {
            PandaPlane.highScore = PandaPlane.score;
            string uzengije = (PandaPlane.stars + 10) + "#" + (PandaPlane.highScore - 20) + "#" + (PandaPlane.laserWeaponNumber + 5) + "#" + (PandaPlane.teslaWeaponNumber + 5) + "#" + (PandaPlane.bladesWeaponNumber + 5) + "#" + (PandaPlane.bombWeaponNumber + 5);
            PlayerPrefs.SetString("Uzengije", uzengije);
            PlayerPrefs.Save();
            GameObject.Find("HighScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
        }

        popupType = 4;
        //GameObject.Find("GameOverScoreText").GetComponent<Text>().text = PandaPlane.score.ToString();
        GameOverScoreText.text = PandaPlane.score.ToString();
        SoundManager.Instance.Play_GameOver();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnHome.gameObject);
    }

    public Button btnHome;
    public Text GameOverScoreText;
    public void ShowNoVideoAvailablePopUp()
    {
        if (videoReady)
        {
            popupType = 6;
            SoundManager.Instance.Play_ButtonClick();
            transform.Find("KeepPlayingPopUp/AnimationHolder/PopUpHolderNoVideo/AnimationHolder").GetComponent<Animator>().Play("ConfirmationMessageShow");
        }
    }

    public void CloseNoVideoAvailablePopUp()
    {
        //ShowKeepPlayingScreen();
        popupType = 3;
        SoundManager.Instance.Play_ButtonClick();
        transform.Find("KeepPlayingPopUp/AnimationHolder/PopUpHolderNoVideo/AnimationHolder").GetComponent<Animator>().Play("ConfirmationMessageClose");
    }

    public void ShowKeepPlayingScreen()
    {
        int numberOfPlays;
        if (PlayerPrefs.HasKey("NumOfPlays"))
        {
            numberOfPlays = PlayerPrefs.GetInt("NumOfPlays");
        }
        else
        {
            numberOfPlays = 1;
        }

        AdsManager.Instance.IsVideoRewardAvailable();
        Time.timeScale = 1;
        menuMngr.ShowMenu(keepPlayingMenu);
        popupType = 3;
        GameObject.Find("CurrentStarsTextKeep").GetComponent<Text>().text = PandaPlane.stars.ToString();
        GameObject.Find("StarsNumberTextKeepPlaying").GetComponent<Text>().text = CalculateStarsForKeepPlaying().ToString();

    }

    public void BuyTesla()
    {
        if (PandaPlane.teslaWeaponPrice <= PandaPlane.stars)
        {
            PandaPlane.teslaWeaponNumber++;
            PandaPlane.Instance.TakeAwayStars(PandaPlane.teslaWeaponPrice);
            teslaWeaponNumberText.text = PandaPlane.teslaWeaponNumber.ToString();
            StarsNumberTextInSlowScreen.text = PandaPlane.stars.ToString();
            if (PandaPlane.teslaWeaponNumber == 1)
            {
                //teslaWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;//todo
                teslabuttonUse.GetComponent<Image>().color = buttonUseActiveColor;
            }
            UpdateStateOfBuyShopButtons();
            SoundManager.Instance.Play_UpgradePlane();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(teslabuttonUse.gameObject);
        }
        else
        {
            starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
            SoundManager.Instance.Play_NotEnoughStars();
            ShowNoGold();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(enough.gameObject);
            index = 1;
        }

        SetNav();

    }

    public void UseTesla()
    {
        if (PandaPlane.teslaWeaponNumber > 0)
        {
            SoundManager.Instance.Play_ActivateWeapon();
            //ResumeGame();	
            PlaneManager.Instance.NormalTimeAfterWeaponUse();
            PandaPlane.teslaWeaponNumber--;
            teslaWeaponNumberText.text = PandaPlane.teslaWeaponNumber.ToString();
            if (PandaPlane.teslaWeaponNumber <= 0)
            {
                //teslaWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;//todo
                teslabuttonUse.GetComponent<Image>().color = Color.white;
            }
            PlaneManager.Instance.StartTesla();
        }
        else
        {
            teslaWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
            SoundManager.Instance.Play_NoMoreWeapons();
        }
        SetNav();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(teslabuttonBuy.gameObject);
    }

    public void BuyLaser()
    {
        if (PandaPlane.laserWeaponPrice <= PandaPlane.stars)
        {
            PandaPlane.laserWeaponNumber++;
            PandaPlane.Instance.TakeAwayStars(PandaPlane.laserWeaponPrice);
            laserWeaponNumberText.text = PandaPlane.laserWeaponNumber.ToString();
            StarsNumberTextInSlowScreen.text = PandaPlane.stars.ToString();
            if (PandaPlane.laserWeaponNumber == 1)
            {
                //laserWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
                laserbuttonUse.GetComponent<Image>().color = buttonUseActiveColor;
            }
            UpdateStateOfBuyShopButtons();
            SoundManager.Instance.Play_UpgradePlane();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(laserbuttonUse.gameObject);
        }
        else
        {
            starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
            SoundManager.Instance.Play_NotEnoughStars();
            ShowNoGold();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(enough.gameObject);
            index = 2;
        }
        SetNav();

    }

    public void UseLaser()
    {
        if (PandaPlane.laserWeaponNumber > 0)
        {
            SoundManager.Instance.Play_ActivateWeapon();
            //ResumeGame();
            PlaneManager.Instance.NormalTimeAfterWeaponUse();
            PandaPlane.laserWeaponNumber--;
            laserWeaponNumberText.text = PandaPlane.laserWeaponNumber.ToString();
            if (PandaPlane.laserWeaponNumber <= 0)
            {
                //laserWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;
                laserbuttonUse.GetComponent<Image>().color = Color.white;
            }
            PlaneManager.Instance.StartLaser();
            SoundManager.Instance.Play_UpgradePlane();
        }
        else
        {
            laserWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
            SoundManager.Instance.Play_NoMoreWeapons();
        }
        SetNav();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(laserbuttonBuy.gameObject);
    }

    public void BuyBlades()
    {
        if (PandaPlane.bladesWeaponPrice <= PandaPlane.stars)
        {
            PandaPlane.bladesWeaponNumber++;
            PandaPlane.Instance.TakeAwayStars(PandaPlane.bladesWeaponPrice);
            bladesWeaponNumberText.text = PandaPlane.bladesWeaponNumber.ToString();
            StarsNumberTextInSlowScreen.text = PandaPlane.stars.ToString();
            if (PandaPlane.bladesWeaponNumber == 1)
            {
                //bladesWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
                bladesbuttonUse.GetComponent<Image>().color = buttonUseActiveColor;
            }
            UpdateStateOfBuyShopButtons();
            SoundManager.Instance.Play_UpgradePlane();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(bladesbuttonUse.gameObject);
        }
        else
        {
            starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
            SoundManager.Instance.Play_NotEnoughStars();
            ShowNoGold();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(enough.gameObject);
            index = 3;
        }
        SetNav();

    }

    public void UseBlades()
    {
        if (PandaPlane.bladesWeaponNumber > 0)
        {
            SoundManager.Instance.Play_ActivateWeapon();
            //ResumeGame();
            PlaneManager.Instance.NormalTimeAfterWeaponUse();
            PandaPlane.bladesWeaponNumber--;
            bladesWeaponNumberText.text = PandaPlane.bladesWeaponNumber.ToString();
            if (PandaPlane.bladesWeaponNumber <= 0)
            {
                //bladesWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;
                bladesbuttonUse.GetComponent<Image>().color = Color.white;
            }
            PlaneManager.Instance.StartBlades();
        }
        else
        {
            bladesWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
            SoundManager.Instance.Play_NoMoreWeapons();
        }
        SetNav();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(bladesbuttonBuy.gameObject);
    }

    public void BuyBomb()
    {
        if (PandaPlane.bombWeaponPrice <= PandaPlane.stars)
        {
            PandaPlane.bombWeaponNumber++;
            PandaPlane.Instance.TakeAwayStars(PandaPlane.bombWeaponPrice);
            bombWeaponNumberText.text = PandaPlane.bombWeaponNumber.ToString();
            StarsNumberTextInSlowScreen.text = PandaPlane.stars.ToString();
            if (PandaPlane.bombWeaponNumber == 1)
            {
                //bombWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
                bombbuttonUse.GetComponent<Image>().color = buttonUseActiveColor;
            }
            UpdateStateOfBuyShopButtons();
            SoundManager.Instance.Play_UpgradePlane();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(bombbuttonUse.gameObject);
        }
        else
        {
            starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
            SoundManager.Instance.Play_NotEnoughStars();
            ShowNoGold();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(enough.gameObject);
            index = 4;
        }
        SetNav();

    }

    public void UseBomb()
    {
        if (PandaPlane.bombWeaponNumber > 0)
        {
            SoundManager.Instance.Play_ActivateWeapon();
            //ResumeGame();
            PlaneManager.Instance.NormalTimeAfterWeaponUse();
            PandaPlane.bombWeaponNumber--;
            bombWeaponNumberText.text = PandaPlane.bombWeaponNumber.ToString();
            if (PandaPlane.bombWeaponNumber <= 0)
            {
                //bombWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;
                bombbuttonUse.GetComponent<Image>().color = Color.white;
            }
            PlaneManager.Instance.StartBomb();
        }
        else
        {
            bombWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
            SoundManager.Instance.Play_NoMoreWeapons();
        }
        SetNav();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(bombbuttonBuy.gameObject);
    }

    public void LoadMainScene()
    {
        Time.timeScale = 1;
        SoundManager.Instance.Stop_GameplayMusic();
        SoundManager.Instance.Stop_BossMusic();
        SoundManager.Instance.Stop_BossPlaneMovement();
        SoundManager.Instance.Stop_BossShipMovement();
        SoundManager.Instance.Stop_BossTankMovement();
        StartCoroutine("LoadMain");
    }

    IEnumerator LoadMain()
    {
        Time.timeScale = 1;
        PandaPlane.Instance.GetComponent<Collider2D>().enabled = false;
        PlaneManager.Instance.guiCamera.transform.Find("LoadingHolder 1").GetChild(0).GetComponent<Animation>().Play("LoadingArrival2New");
        SoundManager.Instance.Play_DoorClosing();
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("MainScene");
        SoundManager.Instance.Stop_BossPlaneMovement();
        SoundManager.Instance.Stop_BossShipMovement();
        SoundManager.Instance.Stop_BossTankMovement();
        popupType = 5;
    }

    public void RestartGame()
    {
        StartCoroutine("Restart");
    }

    IEnumerator Restart()
    {
        PlaneManager.Instance.guiCamera.transform.Find("LoadingHolder 1").GetChild(0).GetComponent<Animation>().Play("LoadingArrival2New");
        SoundManager.Instance.Play_DoorClosing();
        LevelGenerator.terrainsPassed = 0;
        gameStarted = false;
        PandaPlane.score = 0;
        MoveBg.hasBridge = false;
        LevelGenerator.checkpoint = true;
        LevelGenerator.currentStage = 1;
        LevelGenerator.currentBossPlaneLevel = 1;
        LevelGenerator.currentBossShipLevel = 1;
        LevelGenerator.currentBossTankLevel = 1;
        PandaPlane.numberOfKills = 0;
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Woods");
        SoundManager.Instance.Stop_BossPlaneMovement();
        SoundManager.Instance.Stop_BossShipMovement();
        SoundManager.Instance.Stop_BossTankMovement();
    }

    public void KeepPlaying()
    {
        PandaPlane.Instance.TakeAwayStars(CalculateStarsForKeepPlaying());
    }

    public void ResumeGame()
    {
        //StartCoroutine("Resume");
        //StartCoroutine(Resume ());//todo
    }
    public Menu EmptyMenu;
    public void ResumeGameFromPause()
    {
        //StartCoroutine("ResumeFromPause");
        //Time.timeScale = 0.1f;
        Time.timeScale = 1f;
        menuMngr.ShowMenu(EmptyMenu);
        popupType = 1;

    }

    public IEnumerator Resume()
    {
        //yield return new WaitForSeconds(0.5f);
        //		Interface.Instance.normalTime = true;
        //StopCoroutine(Interface.Instance.SlowTime());
        //StartCoroutine(Interface.Instance.NormalTime());
        //if(PlaneManager.pressedAndHold)
        {
            PlaneManager.Instance.menuIsActive = false;

            if (!gameStarted)
            {
                gameStarted = true;
                menuMngr.ShowMenu(resume);
                yield return new WaitForSeconds(0.5f);
                teslaWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(true);
                laserWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(true);
                bladesWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(true);
                bombWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(true);
                slowTimeMenu.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                slowTimeMenu.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                popupType = 0;
            }
            //			else
            //			{
            //				Debug.Log("3 samo sgohwon menu resume");
            //				yield return new WaitForSeconds(0.1f);
            //				if(PlaneManager.pressedAndHold)
            //				{
            //					Debug.Log("4 reusme");
            //					menuMngr.ShowMenu(resume);
            //				}
            //			}
        }
    }

    public void ShowMenu()
    {
        menuMngr.ShowMenu(resume);
        popupType = 0;
    }

    IEnumerator ResumeFromPause()
    {
        //		if(!gameStarted)
        //		{
        //			gameStarted=true;
        //			menuMngr.ShowMenu(pauseMenu);
        //			yield return new WaitForSeconds(0.5f);
        //			teslaWeapon.transform.GetChild(0).FindChild("ButtonUse").gameObject.SetActive(true);
        //			laserWeapon.transform.GetChild(0).FindChild("ButtonUse").gameObject.SetActive(true);
        //			bladesWeapon.transform.GetChild(0).FindChild("ButtonUse").gameObject.SetActive(true);
        //			bombWeapon.transform.GetChild(0).FindChild("ButtonUse").gameObject.SetActive(true);
        //			slowTimeMenu.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //			slowTimeMenu.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //		}
        //		else
        //			//		yield return new WaitForSeconds(0.1f);

        menuMngr.ShowMenu(slowTimeMenu);
        yield return null;
    }

    int CalculateStarsForKeepPlaying()
    {
        //((2*trenutni_teren)na_kvadrat+5*trenutni_teren)+trenutni_stage*trenutni_stage*100
        int neededStars = 0;
        int currentStage = LevelGenerator.currentStage;
        int currentTerrainInStage = LevelGenerator.terrainsPassed;

        neededStars = (int)((2 * Mathf.Pow(currentTerrainInStage, 2) + 5 * currentTerrainInStage) + Mathf.Pow(currentStage, 2) * 50);
        return neededStars;
    }

    public void Ressurect()
    {
        if (PandaPlane.stars >= CalculateStarsForKeepPlaying())
        {
            PandaPlane.Instance.TakeAwayStars(CalculateStarsForKeepPlaying());
            PandaPlane.Instance.NewPlane();
        }
        else
        {
            GameObject.Find("CurrentStarsHolder").transform.GetChild(0).GetComponent<Animator>().Play("NotEnoughStars");
            SoundManager.Instance.Play_NotEnoughStars();
        }
    }

    public void SpeedUpDialog()
    {
        DialogPanda.dialogPressed = true;
        PlaneManager.Instance.gameActive = false;
    }

    //	public void SpeedUpBossDialog()
    //	{
    //		DialogBoss.dialogPressed = true;
    //		PlaneManager.Instance.gameActive = false;
    //	}

    public void NormalDialog()
    {
        DialogPanda.dialogPressed = false;
        PlaneManager.Instance.gameActive = true;
    }

    //	public void NormalBossDialog()
    //	{
    //		DialogBoss.dialogPressed = false;
    //		PlaneManager.Instance.gameActive = true;
    //	}

    public void AnaliticKeepPlayingQuit()
    {
        //		if(GooglePlayGameServices.Instance.GameAnaliticOn)
        //		{
        //			GA.API.Design.NewEvent("ContinueDie"+":"+"Quit");
        //		}
    }

    public void AnaliticKeepPlayingStars()
    {
        //		if(GooglePlayGameServices.Instance.GameAnaliticOn)
        //		{
        //			GA.API.Design.NewEvent("ContinueDie"+":"+"Stars");
        //		}
    }

    public void AnaliticKeepPlayingVideo()
    {
        //		if(GooglePlayGameServices.Instance.GameAnaliticOn)
        //		{
        //			GA.API.Design.NewEvent("ContinueDie"+":"+"Video");
        //		}
    }

    public void OpenConfirmationMessagePopup()
    {
        popupType = 5;
        SoundManager.Instance.Play_ButtonClick();
        transform.Find("PausePopUp/AnimationHolder/PopUpHolderConfirmationMessage/AnimationHolder").GetComponent<Animator>().Play("ConfirmationMessageShow");
    }

    public void CloseConfirmationMessagePopup()
    {
        popupType = 2;
        SoundManager.Instance.Play_ButtonClick();
        transform.Find("PausePopUp/AnimationHolder/PopUpHolderConfirmationMessage/AnimationHolder").GetComponent<Animator>().Play("ConfirmationMessageClose");
    }

    public void ShowBanner()
    {
    }

    public void HideBanner()
    {
    }

    public void ShowInterstitial()
    {
        AdsManager.Instance.ShowInterstitial();
    }

    void OnApplicationPause(bool status)
    {
        if (status) // game paused
        {
            if (!LevelGenerator.Instance.stageCleared && PandaPlane.health > 0)
            {
                Debug.Log("TEST PAUSE poziva se pause screen");
                if (!progressQuit)
                {
                    //ShowPauseScreen();
                }
            }
            else
            {
                Debug.Log("TEST PAUSE ne poziva se pause screen");
            }
        }

    }

    public void ProgresQuitChange(bool newValue)
    {
        progressQuit = newValue;
    }
}
