using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialEvents : MonoBehaviour {

	public static bool timeCountStarted = false;
	float timeElapsed = 0;
	public static int tutorialNo = 0;
	Transform tutorialPopups;
	Tut_DialogBossAndSensei dialog;
	public bool eventTriggered = false;
	/// <summary>
	/// 0 - normal, 1 - kada izadje popup da treba da pusti, 2 - kada zapravo pusti
	/// </summary>
	public static int expectingRelease = 0;
	public static bool releasedFinger = false;
	GameObject boss;

	// Use this for initialization
	void Start () 
	{
		dialog = GameObject.Find("BossDialogHolder/AnimationHolder").GetComponent<Tut_DialogBossAndSensei>();
		boss = GameObject.Find("BossPlaneHOLDER_Ultimate");
		boss.SetActive(false);
		DialogPanda.pandaCanSpeak = false;
		//Tut_PlaneManager.Instance.dontSlowTime = 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(timeCountStarted)
		{
			timeElapsed += Time.deltaTime;
			if((int)timeElapsed == 2 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				dialog.DetermineDialogProperties(tutorialNo);
				GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
				tutorialNo++;
			}
			if((int)timeElapsed == 16 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				dialog.DetermineDialogProperties(tutorialNo);
				//GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
				dialog.GetComponent<Animation>().Play();
				SoundManager.Instance.Play_DialogPopupArrival();
				tutorialNo++;
			}
			if((int)timeElapsed == 23 && !eventTriggered)
			{
				eventTriggered = true;
				expectingRelease = 1;
				Invoke("ResetEventTrigger",1.5f);
				dialog.DetermineDialogProperties(tutorialNo);
				//GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
				dialog.GetComponent<Animation>().Play();
				SoundManager.Instance.Play_DialogPopupArrival();
				tutorialNo++;
			}
//			if((int)timeElapsed == 30 && !eventTriggered)
//			{
//				eventTriggered = true;
//				//expectingRelease = true;
//				if(releasedFinger)
//				{
//					tutorialNo++;
//				}
//				Invoke("ResetEventTrigger",1.5f);
//				dialog.DetermineDialogProperties(tutorialNo);
//				//GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
//				dialog.animation.Play();
//				tutorialNo++;
//			}
			if((int)timeElapsed == 30 && !eventTriggered && !releasedFinger)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				Tut_PopUpHandler.Instance.ShowSlowTimeScreen();
				ForceClickedYES();
			}

			if((int)timeElapsed == 37 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				dialog.gameObject.SetActive(true);
				dialog.DetermineDialogProperties(tutorialNo);
				//GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
				dialog.GetComponent<Animation>().Play();
				SoundManager.Instance.Play_DialogPopupArrival();
				tutorialNo++;
			}

			if((int)timeElapsed == 43 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				boss.SetActive(true);
				boss.SendMessage("FirstPositionBoss",SendMessageOptions.DontRequireReceiver);
				Tut_PlaneManager.Instance.dontSlowTime = 2;
			}

			if((int)timeElapsed == 47 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				dialog.DetermineDialogProperties(tutorialNo);
				//GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
				dialog.GetComponent<Animation>().Play();
				SoundManager.Instance.Play_DialogPopupArrival();
				tutorialNo++;
			}

			if((int)timeElapsed == 51 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				boss.transform.GetChild(0).GetComponent<Animation>()["Tut_BossPlaneArrival"].speed = 1;
				GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play("BossInterfaceDeparting");
			}
			if((int)timeElapsed == 55 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				dialog.DetermineDialogProperties(tutorialNo);
				GameObject bossInterfaceHolder = GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder");
				bossInterfaceHolder.GetComponent<Animation>().Play();
				SoundManager.Instance.Play_DialogPopupArrival();
				tutorialNo++;
				GameObject.Find("PandaDialogHolder/AnimationHolder").SendMessage("ChangeLetterPause",SendMessageOptions.DontRequireReceiver);
				DialogPanda.pandaCanSpeak = true;
				DialogPanda.noTutorial = false;

				SoundManager.Instance.Stop_GameplayMusic();
			}
			if((int)timeElapsed == 68 && !eventTriggered)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				DialogPanda.noTutorial = true;
				boss.transform.GetChild(0).GetComponent<Animation>().Play("BossPlaneMovement1");
				boss.SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
				Tut_PlaneManager.Instance.dontSlowTime = 0;
				SoundManager.Instance.Play_BossMusic();
			}
			if((int)timeElapsed == 95 && !eventTriggered && Tut_PandaPlane.health > 0)
			{
				eventTriggered = true;
				Invoke("ResetEventTrigger",1.5f);
				dialog.DetermineDialogProperties(tutorialNo);
				//GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play();
				dialog.GetComponent<Animation>().Play();
				SoundManager.Instance.Play_DialogPopupArrival();
				DialogPanda.pandaCanSpeak = false;
				tutorialNo++;
				boss.SendMessage("LaunchSecretWeapon",SendMessageOptions.DontRequireReceiver);
				Tut_PlaneManager.Instance.dontSlowTime = 2;
			}
		}
	}

	void ResetEventTrigger()
	{
		eventTriggered = false;
	}

	void ForceClickedYES()
	{
		if(expectingRelease == 1)
		{
			//releasedFinger = true;
			tutorialNo+=2;
			Tut_PlaneManager.Instance.menuIsActive = true;
			expectingRelease = 2;
			Tut_PandaPlane.bladesWeaponNumber=1;
			Tut_PandaPlane.bombWeaponNumber=1;
			Tut_PandaPlane.laserWeaponNumber=1;
			Tut_PandaPlane.teslaWeaponNumber=1;
			Transform powerUpsHolder = GameObject.Find("PowerUpsHolder").transform;
			powerUpsHolder.Find("PowerUpTeslaHolder").gameObject.SetActive(true);
			powerUpsHolder.Find("PowerUpLaserHolder").gameObject.SetActive(true);
			powerUpsHolder.Find("PowerUpBladesHolder").gameObject.SetActive(true);
			powerUpsHolder.Find("PowerUpBombHolder").gameObject.SetActive(true);
			powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
			powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
			powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
			powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
			powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
			powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/NumberHolder/BladesNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
			powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/NumberHolder/LaserNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
			powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/NumberHolder/TeslaNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
			powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/NumberHolder/BombNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
			Time.timeScale = 0.01f;
			GameObject.Find("BossDialogHolder/AnimationHolder").transform.localScale = new Vector3(0.001f,0.001f,0.001f);
			GameObject.Find("BossDialogHolder/AnimationHolder").gameObject.SetActive(false);
			GameObject forceDialog = GameObject.Find("BossDialogHolderForce");
			forceDialog.transform.parent.Find("BossHealthArmorScoreHolderForce").gameObject.SetActive(true);
			forceDialog.transform.GetChild(0).GetComponent<Animator>().Play("DialogArrivalBoss");
			SoundManager.Instance.Play_DialogPopupArrival();
		}
	}

	void ForceClickedNO()
	{
		Tut_PopUpHandler.Instance.ShowSlowTimeScreen();
		expectingRelease = 2;
		Tut_PandaPlane.bladesWeaponNumber=1;
		Tut_PandaPlane.bombWeaponNumber=1;
		Tut_PandaPlane.laserWeaponNumber=1;
		Tut_PandaPlane.teslaWeaponNumber=1;
		Transform powerUpsHolder = GameObject.Find("PowerUpsHolder").transform;
		powerUpsHolder.Find("PowerUpTeslaHolder").gameObject.SetActive(true);
		powerUpsHolder.Find("PowerUpLaserHolder").gameObject.SetActive(true);
		powerUpsHolder.Find("PowerUpBladesHolder").gameObject.SetActive(true);
		powerUpsHolder.Find("PowerUpBombHolder").gameObject.SetActive(true);
		powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
		powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
		powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
		powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/ButtonUse").GetComponent<Image>().color = new Color(0.92941f, 0.93333f, 0.0f);
		powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/ButtonBuy").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/PriceHolder").gameObject.SetActive(false);
		powerUpsHolder.Find("PowerUpBladesHolder/AnimationHolder/NumberHolder/BladesNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
		powerUpsHolder.Find("PowerUpLaserHolder/AnimationHolder/NumberHolder/LaserNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
		powerUpsHolder.Find("PowerUpTeslaHolder/AnimationHolder/NumberHolder/TeslaNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
		powerUpsHolder.Find("PowerUpBombHolder/AnimationHolder/NumberHolder/BombNumberText").GetComponent<Text>().text = Tut_PandaPlane.bladesWeaponNumber.ToString();
		Time.timeScale = 0.01f;
		GameObject.Find("BossDialogHolder/AnimationHolder").transform.localScale = new Vector3(0.001f,0.001f,0.001f);
		GameObject.Find("BossDialogHolder/AnimationHolder").gameObject.SetActive(false);
		GameObject forceDialog = GameObject.Find("BossDialogHolderForce");
		forceDialog.transform.parent.Find("BossHealthArmorScoreHolderForce").gameObject.SetActive(true);
		forceDialog.transform.GetChild(0).GetComponent<Animator>().Play("DialogArrivalBoss");
		SoundManager.Instance.Play_DialogPopupArrival();
	}
}
