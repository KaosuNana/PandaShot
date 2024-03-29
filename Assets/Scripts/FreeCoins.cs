using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FreeCoins : MonoBehaviour {

	Text starsNumberTextFreeCoins;
	int stars;

	public static int followUsReward = 500;
	public static int watchVideoReward = 500;
	bool leftApp;
	public static string IDstranice = System.String.Empty;
	static bool otisaoDaLajkuje = false;
	string stranica;
//	GameObject pagePanda_Disabled;
//	GameObject pageWebelinx_Disabled;
	GameObject videoNotAvailable;
	GameObject watchVideoText;

	// Use this for initialization
	void Start () {
	
//		pagePanda_Disabled = transform.Find("ContentHolder/ContentHolder/AnimationHolder/FollowUsPanda/TransparentBgDisabled").gameObject;
//		pageWebelinx_Disabled = transform.Find("ContentHolder/ContentHolder/AnimationHolder/FollowUsWebelinx/TransparentBgDisabled").gameObject;
		watchVideoText = transform.Find("ContentHolder/ContentHolder/AnimationHolder/WatchVideo/WatchVideoText").gameObject;
		videoNotAvailable = transform.Find("ContentHolder/ContentHolder/AnimationHolder/WatchVideo/TransparentBgNotAvailable").gameObject;

//		transform.Find("ContentHolder/ContentHolder/AnimationHolder/FollowUsPanda/StarsValue").GetComponent<Text>().text = followUsReward.ToString();
//		transform.Find("ContentHolder/ContentHolder/AnimationHolder/FollowUsWebelinx/StarsValue").GetComponent<Text>().text = followUsReward.ToString();
		transform.Find("ContentHolder/ContentHolder/AnimationHolder/WatchVideo/StarsValue").GetComponent<Text>().text = watchVideoReward.ToString();

//		if(PlayerPrefs.HasKey("LikePanda"))
//		{
//			pagePanda_Disabled.SetActive(true);
//			pagePanda_Disabled.transform.parent.GetComponent<Button>().interactable = false;
//		}
//		if(PlayerPrefs.HasKey("LikeWebelinx"))
//		{
//			pageWebelinx_Disabled.SetActive(true);
//			pageWebelinx_Disabled.transform.parent.GetComponent<Button>().interactable = false;
//		}
//		if(PlayerPrefs.HasKey("Uzengije"))
//		{
//			string uzengije = PlayerPrefs.GetString("Uzengije");
//			string[] kamaraUzengije = uzengije.Split('#');
//			stars = int.Parse(kamaraUzengije[0])-10;
//		}
//		else
//		{
//			stars=MenuManager.defaultStarsNumber;
//		}
//		starsNumberTextFreeCoins = GameObject.Find("StarsNumberFreeCoinsText").GetComponent<Text>();
//		starsNumberTextFreeCoins.text = stars.ToString();
	}

	IEnumerator otvaranjeStranice(string url, string stranicaa)
	{
		leftApp = false;
		string ID_stranice = "";
		if(stranicaa == "Panda")
		{
			ID_stranice = "1403841783274579";
			stranica = stranicaa;
		}
		else if(stranicaa == "Webelinx")
		{
			ID_stranice = "437444296353647";
			stranica = stranicaa;
		}
		
		WWW www = new WWW("http://www.google.com");
		yield return www;
		
		if(string.IsNullOrEmpty(www.error))
		{
			otisaoDaLajkuje = true;
			
//			Application.OpenURL("fb://page/"+ID_stranice);
//			Debug.Log("poceo da otvara");
//			System.DateTime timeToShowNextElement = System.DateTime.Now.AddSeconds(1f);
//			while (System.DateTime.Now < timeToShowNextElement)
//			{
//				yield return null;
//			}
//			if(leftApp)
//			{
//				leftApp = false;
//				Debug.Log("ima facebook aplikaciju trebalo bi");
//			}
//			else
//			{
//				Application.OpenURL(url);
//				Debug.Log("nema facebook aplikaciju izgleda, otvara url");
//			}
#if UNITY_EDITOR || UNITY_IOS
			Application.OpenURL(url);
#elif UNITY_ANDROID
			if(checkPackageAppIsPresent("com.facebook.katana"))
			{
				Application.OpenURL("fb://page/"+ID_stranice);
			}
			else
			{
				Application.OpenURL(url);
			}
#endif			
			PlayerPrefs.SetInt("Like"+stranica,1);
			PlayerPrefs.Save();
		}
		
	}

	private bool checkPackageAppIsPresent(string package)
	{
		#if UNITY_ANDROID
		AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
		
		//take the list of all packages on the device
		AndroidJavaObject appList = packageManager.Call<AndroidJavaObject>("getInstalledPackages",0);
		int num = appList.Call<int>("size");
		for(int i = 0; i < num; i++)
		{
			AndroidJavaObject appInfo = appList.Call<AndroidJavaObject>("get", i);
			string packageNew = appInfo.Get<string>("packageName");
			if(packageNew.CompareTo(package) == 0)
			{
				return true;
			}
		}
		return false;
		#endif
		return false;
	}

	public void OpenPageWebelinx()
	{
		SoundManager.Instance.Play_ButtonClick();
		IDstranice = "437444296353647";
		StartCoroutine(otvaranjeStranice("https://www.facebook.com/WebelinxGamesApps","Webelinx"));
		otisaoDaLajkuje = true;
		PlayerPrefs.SetInt("otisaoDaLajkuje",followUsReward);
		PlayerPrefs.SetString("IDstranice",IDstranice);
		PlayerPrefs.SetString("stranica",stranica);
		PlayerPrefs.Save();
	}
	
	public void OpenPagePanda()
	{
		SoundManager.Instance.Play_ButtonClick();
		IDstranice = "1403841783274579";
		StartCoroutine(otvaranjeStranice("https://www.facebook.com/pages/Panda-Commander-Air-Combat/1403841783274579","Panda"));
		otisaoDaLajkuje = true;
		PlayerPrefs.SetInt("otisaoDaLajkuje",followUsReward);
		PlayerPrefs.SetString("IDstranice",IDstranice);
		PlayerPrefs.SetString("stranica",stranica);
		PlayerPrefs.Save();
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if(pauseStatus) //game paused
		{
			if(otisaoDaLajkuje)
			{
				leftApp = true;
				//FB.API("/"+FB.UserId+"/likes",Facebook.HttpMethod.GET, UserLikesCallback);
				otisaoDaLajkuje = false;
				//GameObject.Find("LifeManager").SendMessage("GiveReward",lokacijaProvere+"#"+PlayerPrefs.GetInt("otisaoDaLajkuje"));
				//shopHolder.SendMessage("DisableCard",stranica,SendMessageOptions.DontRequireReceiver);
				DisableFollowButton(stranica);
				//GiveReward(lokacijaProvere+"#"+PlayerPrefs.GetInt("otisaoDaLajkuje"));
				GiveReward(PlayerPrefs.GetInt("otisaoDaLajkuje"));
				PlayerPrefs.DeleteKey("otisaoDaLajkuje");
				PlayerPrefs.DeleteKey("IDstranice");
				PlayerPrefs.DeleteKey("stranica");
				PlayerPrefs.SetInt("Like"+stranica,1);
				PlayerPrefs.Save();
			}
		}
		else //game resumed
		{
			leftApp = false;
		}
	}

	void DisableFollowButton(string pageName)
	{
//		if(pageName.Equals("Panda"))
//		{
//			pagePanda_Disabled.SetActive(true);
//			pagePanda_Disabled.transform.parent.GetComponent<Button>().interactable = false;
//		}
//		else if(pageName.Equals("Webelinx"))
//		{
//			pageWebelinx_Disabled.SetActive(true);
//			pageWebelinx_Disabled.transform.parent.GetComponent<Button>().interactable = false;
//		}
	}

	void GiveReward(int reward)
	{
		Shop.stars += reward;
		string uzengije = (Shop.stars+10) + "#" + (Shop.highScore-20) + "#" + (Shop.laserNumber+5) + "#" + (Shop.teslaNumber+5) + "#" + (Shop.bladesNumber+5) + "#" + (Shop.bombNumber+5);
		PlayerPrefs.SetString("Uzengije",uzengije);
		PlayerPrefs.Save();
		StartCoroutine(MoneyCounter(reward));
	}
	
	IEnumerator MoneyCounter(int kolicina)
	{
		Text moneyText = GameObject.Find("StarsNumberFreeCoinsText").GetComponent<Text>();
		int current = int.Parse(moneyText.text);
		int suma = current + kolicina;
		int korak = (suma - current)/10;
		ShopNotificationClass.numberNotification = GameObject.Find("Canvas").transform.Find("ShopMenu").GetComponent<Shop>().ShopNotification();

		while(current != suma)
		{
			current += korak;
			moneyText.text = current.ToString();
			yield return new WaitForSeconds(0.07f);
		}
		moneyText.text = Shop.stars.ToString();
	}

	public void CheckWatchVideo()
	{
		SoundManager.Instance.Play_ButtonClick();
		AdsManager.Instance.IsVideoRewardAvailable();
	}

	public void WatchVideoAvailableCallback(bool available)
	{
		Debug.Log("WatchVideoAvailableCallback "+available);
		if(available)
		{
			AdsManager.Instance.ShowVideoReward(4);
		}
		else
		{
			Debug.Log("WatchVideoAvailableCallback GASI IH"+available);
			watchVideoText.SetActive(false);
			videoNotAvailable.SetActive(true);
		}
	}

	public void ResetWatchVideoObjects()
	{
		watchVideoText.SetActive(true);
		videoNotAvailable.SetActive(false);
	}

	public void WatchVideoReward(string empty)
	{
		GiveReward(watchVideoReward);
	}

	void VideoNotAvailable(string empty)
	{
		watchVideoText.SetActive(false);
		videoNotAvailable.SetActive(true);
	}

	public void ShowBanner()
	{
	}

	public void HideBanner()
	{
	}


}
