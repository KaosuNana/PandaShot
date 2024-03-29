using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

/**
  * Scene:All
  * Object:WebelinxCMS
  * Description: Scripta za komunikaciju sa native WebelinxCMS-om. Radi i za Android i za iOS. U sebi sadrzi f-je za prikaz Interstial-a, video reklama, za prikaz i sklanjanje banner-a, kao i za uzimanje raznih vrednosti sa servera.
  * Pre upotrebe skripte procitati uputstvo koje se nalazi na putanji Z:\+Unity\Unity Integration Guide for WebelinxCMS\
  **/
public class AdsManager : MonoBehaviour {

	#region AdMob
	[Header("Admob")]
	public string adMobAppID = "";
	public string interstitalAdMobId = "";
	public string videoAdMobId = "";
	InterstitialAd interstitialAdMob;
	private RewardBasedVideoAd rewardBasedAdMobVideo; 
	AdRequest requestAdMobInterstitial, AdMobVideoRequest;
	#endregion
	[Space(15)]
	#region
	[Header("UnityAds")]
	public string unityAdsGameId;
	public string unityAdsVideoPlacementId = "rewardedVideo";
	#endregion

	static AdsManager instance;
    public bool IsAc = true;
	public static AdsManager Instance
	{
		get
		{
			if(instance == null)
				instance = GameObject.FindObjectOfType(typeof(AdsManager)) as AdsManager;
			
			return instance;
		}
	}

	void Awake ()
	{
		gameObject.name = this.GetType().Name;
		DontDestroyOnLoad(gameObject);
		InitializeAds();
	}

	public void ShowInterstitial()
	{
		ShowAdMob();
	}

	public void IsVideoRewardAvailable()
	{
		if(isVideoAvaiable())
		{
			if(Application.loadedLevelName.Equals("MainScene"))
			{
				if(GameObject.Find("Canvas/FreeCoinsMenu")!=null)
				{
					GameObject.Find("Canvas/FreeCoinsMenu").GetComponent<FreeCoins>().WatchVideoAvailableCallback(true);
				}
			}
			else 
			{
				if(GameObject.Find("Canvas").GetComponent<PopUpHandler>()!=null)
				{
					GameObject.Find("Canvas").GetComponent<PopUpHandler>().IsKeepPlayingVideoAvailable(true);
				}
			}
		}
		else
		{
			if(Application.loadedLevelName.Equals("MainScene"))
			{
				if(GameObject.Find("Canvas/FreeCoinsMenu")!=null)
				{
					GameObject.Find("Canvas/FreeCoinsMenu").GetComponent<FreeCoins>().WatchVideoAvailableCallback(false);
				}
			}
			else
			{
				if(GameObject.Find("Canvas").GetComponent<PopUpHandler>()!=null)
				{
					GameObject.Find("Canvas").GetComponent<PopUpHandler>().IsKeepPlayingVideoAvailable(false);
				}
			}
		}
	}

	public void ShowVideoReward(int ID)
	{
		if(Advertisement.IsReady(unityAdsVideoPlacementId))
		{
			UnityAdsShowVideo();
		}
		else if(rewardBasedAdMobVideo.IsLoaded())
		{
			AdMobShowVideo();
		}
	}

	private void RequestInterstitial()
	{
		// Initialize an InterstitialAd.
		interstitialAdMob = new InterstitialAd(interstitalAdMobId);

		// Called when an ad request has successfully loaded.
		interstitialAdMob.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		interstitialAdMob.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		interstitialAdMob.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		interstitialAdMob.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		interstitialAdMob.OnAdLeavingApplication += HandleOnAdLeavingApplication;

		// Create an empty ad request.
		requestAdMobInterstitial = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitialAdMob.LoadAd(requestAdMobInterstitial);
	}

	public void ShowAdMob()
	{
		if(interstitialAdMob.IsLoaded())
		{
			interstitialAdMob.Show();
		}
		else
		{
			interstitialAdMob.LoadAd(requestAdMobInterstitial);
		}
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
		interstitialAdMob.LoadAd(requestAdMobInterstitial);
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	private void RequestRewardedVideo()
	{
		// Called when an ad request has successfully loaded.
		rewardBasedAdMobVideo.OnAdLoaded += HandleRewardBasedVideoLoadedAdMob;
		// Called when an ad request failed to load.
		rewardBasedAdMobVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoadAdMob;
		// Called when an ad is shown.
		rewardBasedAdMobVideo.OnAdOpening += HandleRewardBasedVideoOpenedAdMob;
		// Called when the ad starts to play.
		rewardBasedAdMobVideo.OnAdStarted += HandleRewardBasedVideoStartedAdMob;
		// Called when the user should be rewarded for watching a video.
		rewardBasedAdMobVideo.OnAdRewarded += HandleRewardBasedVideoRewardedAdMob;
		// Called when the ad is closed.
		rewardBasedAdMobVideo.OnAdClosed += HandleRewardBasedVideoClosedAdMob;
		// Called when the ad click caused the user to leave the application.
		rewardBasedAdMobVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplicationAdMob;
		// Create an empty ad request.
		AdMobVideoRequest = new AdRequest.Builder().AddTestDevice("F80B828A3D297B1D6DF37BCFE9DA3AEA").Build();
		// Load the rewarded video ad with the request.
		this.rewardBasedAdMobVideo.LoadAd(AdMobVideoRequest, videoAdMobId);
	}

	public void HandleRewardBasedVideoLoadedAdMob(object sender, EventArgs args)
	{
		if(Application.loadedLevelName.Equals("MainScene"))
		{
			if(GameObject.Find("Canvas/FreeCoinsMenu")!=null)
			{
				GameObject.Find("Canvas/FreeCoinsMenu").GetComponent<FreeCoins>().WatchVideoReward("empty");
			}
		}
		else
		{
			if(GameObject.Find("Canvas").GetComponent<PopUpHandler>()!=null)
			{
				GameObject.Find("Canvas").GetComponent<PopUpHandler>().WatchVideoCompleted("empty");
			}
		}
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
		
	}

	public void HandleRewardBasedVideoFailedToLoadAdMob(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);

	}

	public void HandleRewardBasedVideoOpenedAdMob(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStartedAdMob(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosedAdMob(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
		this.rewardBasedAdMobVideo.LoadAd(AdMobVideoRequest, videoAdMobId);
	}

	public void HandleRewardBasedVideoRewardedAdMob(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);

	}

	public void HandleRewardBasedVideoLeftApplicationAdMob(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

	void InitializeAds()
	{
		MobileAds.Initialize(adMobAppID);
		this.rewardBasedAdMobVideo = RewardBasedVideoAd.Instance;
		this.RequestRewardedVideo();
		Advertisement.Initialize(unityAdsGameId);
		RequestInterstitial();
	}


	void AdMobShowVideo()
	{
		rewardBasedAdMobVideo.Show();	
	}

	void UnityAdsShowVideo()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResultUnity;

		Advertisement.Show(unityAdsVideoPlacementId, options);
	}

	void HandleShowResultUnity (ShowResult result)
	{
		if(result == ShowResult.Finished) {
			Debug.Log("Video completed - Offer a reward to the player");
			if(Application.loadedLevelName.Equals("MainScene"))
			{
				if(GameObject.Find("Canvas/FreeCoinsMenu")!=null)
				{
					GameObject.Find("Canvas/FreeCoinsMenu").GetComponent<FreeCoins>().WatchVideoReward("empty");
				}
			}
			else
			{
				if(GameObject.Find("Canvas").GetComponent<PopUpHandler>()!=null)
				{
					GameObject.Find("Canvas").GetComponent<PopUpHandler>().WatchVideoCompleted("empty");
				}
			}
			Advertisement.Initialize(unityAdsGameId);
		}else if(result == ShowResult.Skipped) {
			Debug.LogWarning("Video was skipped - Do NOT reward the player");

		}else if(result == ShowResult.Failed) {
			Debug.LogError("Video failed to show");
		}
	}

	bool isVideoAvaiable()
	{
		#if !UNITY_EDITOR
		if(Advertisement.IsReady(unityAdsVideoPlacementId))
		{
			return true;
		}
		else if(rewardBasedAdMobVideo.IsLoaded())
		{
			return true;
		}
		#endif
		return false;
	}
}
