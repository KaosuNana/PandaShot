using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
  * Scene:N/A
  * Object:N/A
  * Description: Ova skripta se koristi da docara efekat kucanja teksta kao na kucacoj masini. Potrebno je skriptu nakaciti na GameObject koji ima Componentu Text novog UI sistema(od 4.6 verzija Unity-a, pa na dalje.
  * 
  **/
public class Tut_DialogBossAndSensei : MonoBehaviour {
	public static int versionDialog;
	public static int randomNumber;

	string senseiName = "Rikuto-Sensei";
	string bossName = "Wizmaz";
	public string currentName = "Rikuto-Sensei";

	string[] messages = new string[] {
		"Tap, hold & move your finger to navigate the plane!",
		/*"Tap & hold anywhere to continue playing",*/
		"Collect stars to buy upgrades & weapons for your plane!",
		"Release finger to use super weapons!",
		"You didn't release your finger, I don't have time for this. Now click on weapon to use it!",
		"Click on weapon to use it!",
		"Upgrade your weapons in the hangar.",
		"Beware! This is a very powerful enemy!",
		"Wizmaz: Mighty Wizmaz crunch you in no time! Foolish little panda!",
		"This ends now!"
	};
	//string[] bossName = new string[] {"","Rikuto-Sensei","Rikuto-Sensei","Rikuto-Sensei","Rikuto-Sensei","Rikuto-Sensei","Rikuto-Sensei","Trugon","Kerugm","Gixemuek","Wizmaz"};
//	string[] bossDialogStage1 = new string[]   {"Brazut: How dare you come to Brazut?! Squash panda! Waaargh!", 
//		"Brazut: You wanna beat Brazut with that little toy? Tee hee har har hur ur!!!", 
//		"Brazut: Wanna fight Brazut?! Who you think you arrrrr?!"};
//	
//	string[] bossDialogStage2 = new string[]   {"Zerunx: Hoy, you funny panda! You can't beat the mighty Zerunx!", 
//		"Zerunx: This is war! Ghee'Haad! Zerunx smash you!", 
//		"Zerunx: Grrrrr!!! Boring panda! Feel the anger of Zerunx!"};
//	
//	string[] bossDialogStage3 = new string[]   {"Gezbek: G'Roat!!! Gezbek crunch pandas!!! Dinner time!", 
//		"Gezbek: Gezbek hungry! Gezbek eat panda! Nom nom nom!!!", 
//		"Gezbek: Gezbek crush your funny plane! Goblins, we eat panda for dinner!"};
//	
//	string[] bossDialogStage4 = new string[]   {"Spitspaz: Ironpaw, we finally meet! Spitspaz make panda burgers! Yum yum!", 
//		"Spitspaz: Grrrrr, you really have the guts! Too bad Spitspaz must smash your toy plane!", 
//		"Spitspaz: Spitspaz hurt panda! Prepare for pain! Waaargh!"};
//	
//	string[] bossDialogStage5 = new string[]   {"Nazgel: Ironpaw, I was waiting for you! Now you feel my rage! Aarghh!", 
//		"Nazgel: Nobody mess with Nazgel! Now you gonna pay!", 
//		"Nazgel: Nazgel destroy pandas for fun! You be next! Grrrrr!!!"};
//	
//	string bossDialogStage6 = "Mornuk: Panda beat Mornuk in that toy?! Tee hee har har hur ur!";
//	string bossDialogStage7 = "Trugon: Do you know who is I? Trugon smash you in pieces!!! Waaargh!";
//	string bossDialogStage8 = "Kerugm: Mighty Kerugm crunch you in no time! Foolish little panda!";
//	string bossDialogStage9 = "Gixemuek: RAWR!!!! This is your end! Gixemux smash panda!!!";
//	string bossDialogStage10 = "Wizmaz: You destroyed my best warriors! Now you gonna pay Ironpaw!!! GRRRRR!!!";
//	
//	public static bool dialogPressed = false;
	
	float letterPause = 0.035f;
	Text BossMessageText;
	string message;
	// Use this for initialization
	void Awake () {
		//versionDialog = 1;//Random.Range(0,3);
//		if(LevelGenerator.currentStage<11)
//		{
//			GameObject.Find("BossNameText").GetComponent<Text>().text = bossName[LevelGenerator.currentStage];
//		}
//		else
//		{
//			randomNumber = Random.Range(1,11);
//			GameObject.Find("BossNameText").GetComponent<Text>().text = bossName[randomNumber];
//		}
//		
//		switch(LevelGenerator.currentStage)
//		{
//		case 1:
//			message = bossDialogStage1[versionDialog];
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference1").GetComponent<Image>().sprite;
//			break;
//		case 2:
//			message = bossDialogStage2[versionDialog];
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference2").GetComponent<Image>().sprite;
//			break;
//		case 3:
//			message = bossDialogStage3[versionDialog];
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference3").GetComponent<Image>().sprite;
//			break;
//		case 4:
//			message = bossDialogStage4[versionDialog];
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference4").GetComponent<Image>().sprite;
//			break;
//		case 5:
//			message = bossDialogStage5[versionDialog];
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference5").GetComponent<Image>().sprite;
//			break;
//		case 6:
//			message = bossDialogStage6;
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference6").GetComponent<Image>().sprite;
//			break;
//		case 7:
//			message = bossDialogStage7;
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference7").GetComponent<Image>().sprite;
//			break;
//		case 8:
//			message = bossDialogStage8;
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference8").GetComponent<Image>().sprite;
//			break;
//		case 9:
//			message = bossDialogStage9;
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference9").GetComponent<Image>().sprite;
//			break;
//		case 10:
//			message = bossDialogStage10;
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference10").GetComponent<Image>().sprite;
//			break;
//		default:
//			message = bossName[randomNumber] +": Do you know who is I? "+ bossName[randomNumber] +" smash you in pieces!!! Waaargh!";
//			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference"+randomNumber).GetComponent<Image>().sprite;
//			break;
//		}
		BossMessageText = GameObject.Find("BossMessageText").GetComponent<Text>();
		GameObject.Find("BossNameText").GetComponent<Text>().text = currentName;
		BossMessageText.text = "";
	}

	public void DetermineDialogProperties(int number)
	{
		BossMessageText.text = "";
		if(number >= 7)
		{
			currentName = bossName;
			GameObject.Find("BossFace").GetComponent<Image>().sprite = GameObject.Find("BossReference1").GetComponent<Image>().sprite;
			GameObject.Find("BossNameText").GetComponent<Text>().text = currentName;
		}
		message = messages[number];

	}

	public IEnumerator TypeTextBoss () 
	{
		if(TutorialEvents.expectingRelease == 2)
		{
			BossMessageText = GameObject.Find("BossMessageTextForce").GetComponent<Text>();
			BossMessageText.text = System.String.Empty;
			if(TutorialEvents.releasedFinger)
				message = "Click on weapon to use it!";
			else
				message = "You didn't release your finger, I don't have time for this. Now click on weapon to use it!";
			System.DateTime timeToShowNextElement;

			for(int i=0; i<message.Length;i++)
			{
				if(!SoundManager.Instance.dialogTextTyping.isPlaying)
					SoundManager.Instance.Play_DialogTextTyping();
				BossMessageText.text += message[i];
				timeToShowNextElement = System.DateTime.Now.AddSeconds(letterPause);
				while(System.DateTime.Now < timeToShowNextElement)
				{
					yield return null;
				}

				//yield return new WaitForSeconds (letterPause);
			}
			timeToShowNextElement = System.DateTime.Now.AddSeconds(2f);
			while(System.DateTime.Now < timeToShowNextElement)
			{
				yield return null;
			}
			//yield return new WaitForSeconds(1f);
			GameObject.Find("BossDialogHolderForce/AnimationHolder").GetComponent<Animator>().Play ("DialogDepartingBoss");
		}
		else
		{
			for(int i=0; i<message.Length;i++)
			{
				if(!SoundManager.Instance.dialogTextTyping.isPlaying)
					SoundManager.Instance.Play_DialogTextTyping();
				BossMessageText.text += message[i];
				yield return new WaitForSeconds (letterPause);
			}
			yield return new WaitForSeconds(1f);
			//"OVDE VEROVATNO CE DA BUDE DOVOLJNO DA SE STAVI USLOV KADA JE EXPECTINGRELEASE == 0, I DA SE PROVERI DA LI MOZE JOS NEGDE OSIM NA USE WEAPON, POSTO BI TO JEDINO TREBALO DA MOZE!!!!!"
			if(TutorialEvents.expectingRelease == 0 || TutorialEvents.expectingRelease == 3)
				GameObject.Find("BossDialogHolder/AnimationHolder").GetComponent<Animation>().Play ("DialogDepartingBoss");
		}
	}

	public IEnumerator TypeTextSenseiShop()
	{
		message = "This is the hangar, here you can spend your precious stars to upgrade your plane.";
		for(int i=0; i<message.Length;i++)
		{
			if(!SoundManager.Instance.dialogTextTyping.isPlaying)
				SoundManager.Instance.Play_DialogTextTyping();
			BossMessageText.text += message[i];
			yield return new WaitForSeconds (0.035f);
		}
		yield return new WaitForSeconds(1f);
		GameObject.Find("BossDialogHolder/AnimationHolder").GetComponent<Animation>().Play ("DialogDepartingSenseiShop");
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").GetComponent<Animation>().Play("BossInterfaceDeparting");
		yield return new WaitForSeconds(1f);
		GameObject.Find("BossHealthArmorScoreHolder/AnimationHolder").SetActive(false);
	}
	
	
	
	
}
