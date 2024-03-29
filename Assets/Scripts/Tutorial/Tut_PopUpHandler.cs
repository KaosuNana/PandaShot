using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tut_PopUpHandler : MonoBehaviour {
	
	[HideInInspector]
	public Menu pauseMenu, slowTimeMenu, gameOverMenu, keepPlayingMenu, resume;
	[HideInInspector]
	public Image GreenButton, GrayButton;
	MenuManager menuMngr;
	Text teslaWeaponNumberText, laserWeaponNumberText, bladesWeaponNumberText, bombWeaponNumberText;
	Text StarsNumberTextInSlowScreen;
	Color buttonUseActiveColor = new Color(0.92941f, 0.93333f, 0.0f);
	GameObject teslaWeapon, laserWeapon, bladesWeapon, bombWeapon, starsCounter;
	public static bool gameStarted = false;
	/// <summary>
	/// The type of the popup. 0 - no popup, 1 - slow time screen, 2 - paused game, 3 - keep playing, 4 - game over, 5 - popup not allowed
	/// </summary>
	public static int popupType = 5;
	
	static Tut_PopUpHandler instance;
	public static Tut_PopUpHandler Instance
	{
		get
		{
			if(instance == null)
				instance = GameObject.FindObjectOfType(typeof(Tut_PopUpHandler)) as Tut_PopUpHandler;
			
			return instance;
		}
	}
	
	void Awake()
	{
		GameObject.Find("TeslaWeaponPriceText").GetComponent<Text>().text = Tut_PandaPlane.teslaWeaponPrice.ToString();
		GameObject.Find("LaserWeaponPriceText").GetComponent<Text>().text = Tut_PandaPlane.laserWeaponPrice.ToString();
		GameObject.Find("BladesWeaponPriceText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponPrice.ToString();
		GameObject.Find("BombWeaponPriceText").GetComponent<Text>().text = Tut_PandaPlane.bombWeaponPrice.ToString();
		
		teslaWeapon = GameObject.Find("PowerUpTeslaHolder");
		laserWeapon = GameObject.Find("PowerUpLaserHolder");
		bladesWeapon = GameObject.Find("PowerUpBladesHolder");
		bombWeapon = GameObject.Find("PowerUpBombHolder");
		
		teslaWeaponNumberText = GameObject.Find("TeslaNumberText").GetComponent<Text>();
		laserWeaponNumberText = GameObject.Find("LaserNumberText").GetComponent<Text>();
		bladesWeaponNumberText = GameObject.Find("BladesNumberText").GetComponent<Text>();
		bombWeaponNumberText = GameObject.Find("BombNumberText").GetComponent<Text>();
		
		StarsNumberTextInSlowScreen = GameObject.Find("StarsNumberTextInSlowScreen").GetComponent<Text>();
		
	}
	
	// Use this for initialization
	void Start () {
		menuMngr = GameObject.Find("Canvas").GetComponent<MenuManager>();
		starsCounter = GameObject.Find("StarsCounterHolder");
		UpdateStateOfUseShopButtons();
		//UpdateStateOfBuyShopButtons();
		UpdateStateOfWeaponsAndStars();
	}
	
	public void UpdateStateOfWeaponsAndStars()
	{
		teslaWeaponNumberText.text = Tut_PandaPlane.teslaWeaponNumber.ToString();
		laserWeaponNumberText.text = Tut_PandaPlane.laserWeaponNumber.ToString();
		bladesWeaponNumberText.text = Tut_PandaPlane.bladesWeaponNumber.ToString();
		bombWeaponNumberText.text = Tut_PandaPlane.bombWeaponNumber.ToString();
		
		StarsNumberTextInSlowScreen.text = Tut_PandaPlane.stars.ToString();
	}
	
	public void UpdateStateOfBuyShopButtons()
	{
		
		if(Tut_PandaPlane.teslaWeaponPrice<=Tut_PandaPlane.stars)
		{
			teslaWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
		}
		else
		{
			teslaWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
		}
		
		if(Tut_PandaPlane.laserWeaponPrice<=Tut_PandaPlane.stars)
		{
			laserWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
		}
		else
		{
			laserWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
		}
		
		if(Tut_PandaPlane.bladesWeaponPrice<=Tut_PandaPlane.stars)
		{
			bladesWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
		}
		else
		{
			bladesWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
		}
		
		if(Tut_PandaPlane.bombWeaponPrice<=Tut_PandaPlane.stars)
		{
			bombWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GreenButton.sprite;
		}
		else
		{
			bombWeapon.transform.GetChild(0).Find("ButtonBuy").GetComponent<Image>().sprite = GrayButton.sprite;
		}
		
		
	}
	
	public void ShowPauseScreen()
	{
		menuMngr.ShowMenu(pauseMenu);
		popupType = 2;
		Time.timeScale = 0;
	}
	
	void UpdateStateOfUseShopButtons()
	{
		if(Tut_PandaPlane.teslaLvl<1 && Tut_PandaPlane.laserLvl<1 && Tut_PandaPlane.bladesLvl<1 && Tut_PandaPlane.bombLvl<1)
		{
			starsCounter.SetActive(false);
		}
		
		if(gameStarted)
		{
			if(Tut_PandaPlane.teslaLvl<1)
			{
				teslaWeapon.SetActive(false);
			}
			else if(Tut_PandaPlane.teslaWeaponNumber>0)
			{
				teslaWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
			
			if(Tut_PandaPlane.laserLvl<1)
			{
				laserWeapon.SetActive(false);
			}
			else if(Tut_PandaPlane.laserWeaponNumber>0)
			{
				laserWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
			
			if(Tut_PandaPlane.bladesLvl<1)
			{
				bladesWeapon.SetActive(false);
			}
			else if(Tut_PandaPlane.bladesWeaponNumber>0)
			{
				bladesWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
			
			if(Tut_PandaPlane.bombLvl<1)
			{
				bombWeapon.SetActive(false);
			}
			else if(Tut_PandaPlane.bombWeaponNumber>0)
			{
				bombWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
		}
		else
		{
			if(Tut_PandaPlane.teslaLvl<1)
			{
				teslaWeapon.SetActive(false);
			}
			else
			{
				teslaWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
			}
			
			if(Tut_PandaPlane.laserLvl<1)
			{
				laserWeapon.SetActive(false);
			}
			else
			{
				laserWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
			}
			
			if(Tut_PandaPlane.bladesLvl<1)
			{
				bladesWeapon.SetActive(false);
			}
			else
			{
				bladesWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
			}
			
			if(Tut_PandaPlane.bombLvl<1)
			{
				bombWeapon.SetActive(false);
			}
			else
			{
				bombWeapon.transform.GetChild(0).Find("ButtonUse").gameObject.SetActive(false);
			}
			
			
			slowTimeMenu.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
			slowTimeMenu.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
		}
		
	}
	
	public void ShowSlowTimeScreen()
	{
//		Tut_PlaneManager.Instance.menuIsActive = true; //IZMENA
		StartCoroutine(Tut_Interface.Instance.SlowTime());
		menuMngr.ShowMenu(slowTimeMenu);
		UpdateStateOfWeaponsAndStars();
		UpdateStateOfBuyShopButtons();
		UpdateStateOfUseShopButtons();
	}
	
	public void ShowGameOverScreen()
	{
		menuMngr.ShowMenu(gameOverMenu);
		if(Tut_PandaPlane.highScore>Tut_PandaPlane.score)
		{
			GameObject.Find("HighScoreStamp").SetActive(false);
			string uzengije = (Tut_PandaPlane.stars+10) + "#" + (Tut_PandaPlane.highScore-20) + "#" + (Tut_PandaPlane.laserWeaponNumber+5) + "#" + (Tut_PandaPlane.teslaWeaponNumber+5) + "#" + (Tut_PandaPlane.bladesWeaponNumber+5) + "#" + (Tut_PandaPlane.bombWeaponNumber+5);
			PlayerPrefs.SetString("Uzengije",uzengije);
			PlayerPrefs.Save();
		}
		else
		{
			Tut_PandaPlane.highScore = Tut_PandaPlane.score;
			string uzengije = (Tut_PandaPlane.stars+10) + "#" + (Tut_PandaPlane.highScore-20) + "#" + (Tut_PandaPlane.laserWeaponNumber+5) + "#" + (Tut_PandaPlane.teslaWeaponNumber+5) + "#" + (Tut_PandaPlane.bladesWeaponNumber+5) + "#" + (Tut_PandaPlane.bombWeaponNumber+5);
			PlayerPrefs.SetString("Uzengije",uzengije);
			PlayerPrefs.Save();
			GameObject.Find("HighScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
		}
		
		popupType = 4;
		GameObject.Find("GameOverScoreText").GetComponent<Text>().text = Tut_PandaPlane.score.ToString();
		
	}
	
	public void ShowKeepPlayingScreen()
	{
		menuMngr.ShowMenu(keepPlayingMenu);
		popupType = 3;
		GameObject.Find("CurrentStarsTextKeep").GetComponent<Text>().text = Tut_PandaPlane.stars.ToString();
		GameObject.Find("StarsNumberTextKeepPlaying").GetComponent<Text>().text = CalculateStarsForKeepPlaying().ToString();
	}
	
	public void BuyTesla()
	{
		if(Tut_PandaPlane.teslaWeaponPrice<=Tut_PandaPlane.stars)
		{
			Tut_PandaPlane.teslaWeaponNumber++;
			Tut_PandaPlane.Instance.TakeAwayStars(Tut_PandaPlane.teslaWeaponPrice);
			teslaWeaponNumberText.text = Tut_PandaPlane.teslaWeaponNumber.ToString();
			StarsNumberTextInSlowScreen.text = Tut_PandaPlane.stars.ToString();
			if(Tut_PandaPlane.teslaWeaponNumber==1)
			{
				teslaWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
			UpdateStateOfBuyShopButtons();
		}
		else
		{
			starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
			SoundManager.Instance.Play_NotEnoughStars();
		}
		
	}
	
	public void UseTesla()
	{
		if(Tut_PandaPlane.teslaWeaponNumber>0)
		{
			SoundManager.Instance.Play_ActivateWeapon();
			//ResumeGame();	
			Tut_PlaneManager.Instance.NormalTimeAfterWeaponUse();
			Tut_PandaPlane.teslaWeaponNumber--;
			teslaWeaponNumberText.text = Tut_PandaPlane.teslaWeaponNumber.ToString();
			if(Tut_PandaPlane.teslaWeaponNumber<=0)
			{
				teslaWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;
			}
			Tut_PlaneManager.Instance.StartTesla();

			if(TutorialEvents.expectingRelease == 2)
			{
				GameObject.Find("BossHealthArmorScoreHolderForce").SetActive(false);
				TutorialEvents.expectingRelease = 3;
			}
		}
		else
		{
			teslaWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
			SoundManager.Instance.Play_NoMoreWeapons();
		}
	}
	
	public void BuyLaser()
	{
		if(Tut_PandaPlane.laserWeaponPrice<=Tut_PandaPlane.stars)
		{
			Tut_PandaPlane.laserWeaponNumber++;
			Tut_PandaPlane.Instance.TakeAwayStars(Tut_PandaPlane.laserWeaponPrice);
			laserWeaponNumberText.text = Tut_PandaPlane.laserWeaponNumber.ToString();
			StarsNumberTextInSlowScreen.text = Tut_PandaPlane.stars.ToString();
			if(Tut_PandaPlane.laserWeaponNumber==1)
			{
				laserWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
			UpdateStateOfBuyShopButtons();
		}
		else
		{
			starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
			SoundManager.Instance.Play_NotEnoughStars();
		}
	}
	
	public void UseLaser()
	{
		if(Tut_PandaPlane.laserWeaponNumber>0)
		{
			SoundManager.Instance.Play_ActivateWeapon();
			//ResumeGame();
			Tut_PlaneManager.Instance.NormalTimeAfterWeaponUse();
			Tut_PandaPlane.laserWeaponNumber--;
			laserWeaponNumberText.text = Tut_PandaPlane.laserWeaponNumber.ToString();
			if(Tut_PandaPlane.laserWeaponNumber<=0)
			{
				laserWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;
			}
			Tut_PlaneManager.Instance.StartLaser();

			if(TutorialEvents.expectingRelease == 2)
			{
				GameObject.Find("BossHealthArmorScoreHolderForce").SetActive(false);
				TutorialEvents.expectingRelease = 3;
			}
		}
		else
		{
			laserWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
			SoundManager.Instance.Play_NoMoreWeapons();
		}
	}
	
	public void BuyBlades()
	{
		if(Tut_PandaPlane.bladesWeaponPrice<=Tut_PandaPlane.stars)
		{
			Tut_PandaPlane.bladesWeaponNumber++;
			Tut_PandaPlane.Instance.TakeAwayStars(Tut_PandaPlane.bladesWeaponPrice);
			bladesWeaponNumberText.text = Tut_PandaPlane.bladesWeaponNumber.ToString();
			StarsNumberTextInSlowScreen.text = Tut_PandaPlane.stars.ToString();
			if(Tut_PandaPlane.bladesWeaponNumber==1)
			{
				bladesWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
			UpdateStateOfBuyShopButtons();
		}
		else
		{
			starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
			SoundManager.Instance.Play_NotEnoughStars();
		}
	}
	
	public void UseBlades()
	{
		if(Tut_PandaPlane.bladesWeaponNumber>0)
		{
			SoundManager.Instance.Play_ActivateWeapon();
			//ResumeGame();
			Tut_PlaneManager.Instance.NormalTimeAfterWeaponUse();
			Tut_PandaPlane.bladesWeaponNumber--;
			bladesWeaponNumberText.text = Tut_PandaPlane.bladesWeaponNumber.ToString();
			if(Tut_PandaPlane.bladesWeaponNumber<=0)
			{
				bladesWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;
			}
			Tut_PlaneManager.Instance.StartBlades();

			if(TutorialEvents.expectingRelease == 2)
			{
				GameObject.Find("BossHealthArmorScoreHolderForce").SetActive(false);
				TutorialEvents.expectingRelease = 3;
			}
		}
		else
		{
			bladesWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
			SoundManager.Instance.Play_NoMoreWeapons();
		}
	}
	
	public void BuyBomb()
	{
		if(Tut_PandaPlane.bombWeaponPrice<=Tut_PandaPlane.stars)
		{
			Tut_PandaPlane.bombWeaponNumber++;
			Tut_PandaPlane.Instance.TakeAwayStars(Tut_PandaPlane.bombWeaponPrice);
			bombWeaponNumberText.text = Tut_PandaPlane.bombWeaponNumber.ToString();
			StarsNumberTextInSlowScreen.text = Tut_PandaPlane.stars.ToString();
			if(Tut_PandaPlane.bombWeaponNumber==1)
			{
				bombWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = buttonUseActiveColor;
			}
			UpdateStateOfBuyShopButtons();
		}
		else
		{
			starsCounter.transform.GetChild(0).GetComponent<Animator>().Play("StarsCounterGamePlayScale");
			SoundManager.Instance.Play_NotEnoughStars();
		}
	}
	
	public void UseBomb()
	{
		if(Tut_PandaPlane.bombWeaponNumber>0)
		{
			SoundManager.Instance.Play_ActivateWeapon();
			//ResumeGame();
			Tut_PlaneManager.Instance.NormalTimeAfterWeaponUse();
			Tut_PandaPlane.bombWeaponNumber--;
			bombWeaponNumberText.text = Tut_PandaPlane.bombWeaponNumber.ToString();
			if(Tut_PandaPlane.bombWeaponNumber<=0)
			{
				bombWeapon.transform.GetChild(0).Find("ButtonUse").GetComponent<Image>().color = Color.white;
			}
			Tut_PlaneManager.Instance.StartBomb();

			if(TutorialEvents.expectingRelease == 2)
			{
				GameObject.Find("BossHealthArmorScoreHolderForce").SetActive(false);
				TutorialEvents.expectingRelease = 3;
			}
		}
		else
		{
			bombWeapon.transform.GetChild(0).GetComponent<Animator>().Play("BuyButtonGamePlayScale");
			SoundManager.Instance.Play_NoMoreWeapons();
		}
	}
	
	public void LoadMainScene()
	{
		SoundManager.Instance.Stop_GameplayMusic();
		StartCoroutine("LoadMain");
	}
	
	IEnumerator LoadMain()
	{
		Time.timeScale = 1;
		Tut_PandaPlane.Instance.GetComponent<Collider2D>().enabled = false;
		Tut_PlaneManager.Instance.guiCamera.transform.Find("LoadingHolder 1").GetChild(0).GetComponent<Animation>().Play("LoadingArrival2New");
		SoundManager.Instance.Play_DoorClosing();
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("MainScene");
		popupType = 5;
	}
	
	public void RestartGame()
	{
		StartCoroutine("Restart");
	}
	
	IEnumerator Restart()
	{
		Tut_PlaneManager.Instance.guiCamera.transform.Find("LoadingHolder 1").GetChild(0).GetComponent<Animation>().Play("LoadingArrival2New");
		SoundManager.Instance.Play_DoorClosing();
		LevelGenerator.terrainsPassed = 0;
		gameStarted = false;
		Tut_PandaPlane.score = 0;
		MoveBg.hasBridge = false;
		LevelGenerator.checkpoint = true;
		LevelGenerator.currentStage = 1;
		LevelGenerator.currentBossPlaneLevel = 1;
		LevelGenerator.currentBossShipLevel = 1;
		LevelGenerator.currentBossTankLevel = 1;
		Tut_PandaPlane.numberOfKills = 0;
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("Woods");
	}
	
	public void KeepPlaying()
	{
		Tut_PandaPlane.Instance.TakeAwayStars(CalculateStarsForKeepPlaying());
	}
	
	public void ResumeGame()
	{
		//StartCoroutine("Resume");
		if(TutorialEvents.expectingRelease != 2)
			StartCoroutine(Resume ());
	}
	
	public void ResumeGameFromPause()
	{
		StartCoroutine("ResumeFromPause");
		Time.timeScale = 0.1f;
		popupType = 1;
	}
	
	public IEnumerator Resume()
	{
		//yield return new WaitForSeconds(0.5f);
		//		Tut_Interface.Instance.normalTime = true;
		//StopCoroutine(Tut_Interface.Instance.SlowTime());
		//StartCoroutine(Tut_Interface.Instance.NormalTime());
		//if(Tut_PlaneManager.pressedAndHold)
		{
			Tut_PlaneManager.Instance.menuIsActive = false;
			
			if(!gameStarted)
			{
				gameStarted=true;
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
			//				if(Tut_PlaneManager.pressedAndHold)
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
		int neededStars=0;
		int currentStage = LevelGenerator.currentStage;
		int currentTerrainInStage = LevelGenerator.terrainsPassed;
		
		neededStars = (int)((2*Mathf.Pow(currentTerrainInStage,2)+5*currentTerrainInStage)+Mathf.Pow(currentStage,2)*100);
		return neededStars;
	}
	
	public void Ressurect()
	{
		if(Tut_PandaPlane.stars >= CalculateStarsForKeepPlaying())
		{
			Tut_PandaPlane.Instance.TakeAwayStars(CalculateStarsForKeepPlaying());
			Tut_PandaPlane.Instance.NewPlane();
		}
		else
		{
			GameObject.Find("CurrentStarsHolder").transform.GetChild(0).GetComponent<Animator>().Play("NotEnoughStars");
		}
	}
	
//	public void SpeedUpPandaDialog()
//	{
//		DialogPanda.dialogPressed = true;
//		Tut_PlaneManager.Instance.gameActive = false;
//	}
//	
//	public void SpeedUpBossDialog()
//	{
//		DialogBoss.dialogPressed = true;
//		Tut_PlaneManager.Instance.gameActive = false;
//	}
//	
//	public void NormalPandaDialog()
//	{
//		DialogPanda.dialogPressed = false;
//		Tut_PlaneManager.Instance.gameActive = true;
//	}
//	
//	public void NormalBossDialog()
//	{
//		DialogBoss.dialogPressed = false;
//		Tut_PlaneManager.Instance.gameActive = true;
//	}
	
}
