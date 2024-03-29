using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Tut_BossPlane : MonoBehaviour {
	
	//public enum terrain {OnlyGround, Everything, OnlyWater, GroundToWater, WaterToGround};
	//public int[] AvailableTerrainTypes;
	
	// AVAILABLE TERRAIN TYPES:
	// EVERYTHING: -1
	// ONLY GROUND: 0
	// ONLY WATER: 2
	
	public int availableTerrainType;
	public float bgMovingSpeed;
	[HideInInspector] public int health = 400;
	public int bossLevel = 1;
	int levelOfDestruction = 1;

	public GameObject bossSecretWeapon;
	
	public List<Transform> TurretsLevel1;
	public List<Transform> TurretsLevel2;
	public List<Transform> TurretsLevel3;
	
	[HideInInspector] public int turretsFirstWave = 0;
	[HideInInspector] public int turretsSecondWave = 0;
	[HideInInspector] public int turretsThirdWave = 0;
	[HideInInspector] public int currentWave = 0;
	[HideInInspector] public int leftWingCount = 0;
	[HideInInspector] public int rightWingCount = 0;
	
	public int smallWingTurretHealth = 0;
	public int tailGunHealth = 0;
	public int wingTurretSmallHealth = 0;
	public int wingTurretLargeHealth = 0;
	public int wingLaserGunSmallHealth = 0;
	public int wingLaserGunLargeHealth = 0;
	public int mainGunHealth = 0;
	int eventCounter = 0;
	
	[HideInInspector] public int cameraOffsetY;
	bool bossCanMove = false;
	
	Transform camera;
	float healthFillRate;
	int healthStart;
	GameObject healthBar;
	Text healthText;
	int factorX = -20;
	int factorY = 1300;
	// BossLevel1: Gadjaju se oba turreta na malim krilima, dva najsira turreta na krilima i main gun
	// BossLevel2: Gadjaju se oba turreta na malim krilima + tail gun, dva najsira turreta na krilima + dva big laser gun-a i main gun
	// BossLevel3: Gadjaju se oba turreta na malim krilima + tail gun, sva 4 turreta na krilima + sva 4 laser gun-a i main gun
	
	void Start () 
	{
		if(smallWingTurretHealth != 0)
			smallWingTurretHealth += factorX * Tut_PandaPlane.mainGunLvl + factorY;//*Tut_PandaPlane.mainGunLvl;
		if(tailGunHealth != 0)
			tailGunHealth += factorX * Tut_PandaPlane.mainGunLvl + factorY;//*Tut_PandaPlane.mainGunLvl;
		if(wingTurretSmallHealth != 0)
			wingTurretSmallHealth += factorX * Tut_PandaPlane.mainGunLvl + factorY;//*Tut_PandaPlane.mainGunLvl;
		if(wingTurretLargeHealth != 0)
			wingTurretLargeHealth += factorX * Tut_PandaPlane.mainGunLvl + factorY;//*Tut_PandaPlane.mainGunLvl;
		if(wingLaserGunSmallHealth != 0)
			wingLaserGunSmallHealth += factorX * Tut_PandaPlane.mainGunLvl + factorY;//*Tut_PandaPlane.mainGunLvl;
		if(wingLaserGunLargeHealth != 0)
			wingLaserGunLargeHealth += factorX * Tut_PandaPlane.mainGunLvl + factorY;//*Tut_PandaPlane.mainGunLvl;
		if(mainGunHealth != 0)
			mainGunHealth += (int)(factorX * Tut_PandaPlane.mainGunLvl + factorY*1.5f);//*Tut_PandaPlane.mainGunLvl;
		
		health = smallWingTurretHealth*2 + tailGunHealth + wingTurretSmallHealth*2 + wingTurretLargeHealth*2 + wingLaserGunSmallHealth*2 + wingLaserGunLargeHealth*2 + mainGunHealth;
		healthStart = health;
		camera = Camera.main.transform;
		healthBar = GameObject.Find("BossHealthBar");
		healthText = GameObject.Find("BossHealthBar/HealthBarText").GetComponent<Text>();
		//StartCoroutine(AdjustCameraSpeedForBoss());
		//TurretsLevel1 = new List<Transform>();
		//TurretsLevel2 = new List<Transform>();
		//TurretsLevel3 = new List<Transform>();
		
		if(bossLevel == 1)
		{
			turretsFirstWave = 2;
			turretsSecondWave = 2;
			turretsThirdWave = 1;
			
			//			for(int i=0;i<turretsFirstWave;i++)
			//			{
			//				TurretsLevel1[i].collider2D.enabled = true;
			//				//TurretsLevel1[i].SendMessage("ShowTurret", SendMessageOptions.DontRequireReceiver);
			//			}
		}
		else if(bossLevel == 2)
		{
			turretsFirstWave = 3;
			turretsSecondWave = 4;
			turretsThirdWave = 1;
			
			//			for(int i=0;i<turretsFirstWave;i++)
			//			{
			//				TurretsLevel2[i].collider2D.enabled = true;
			//				//TurretsLevel2[i].SendMessage("ShowTurret", SendMessageOptions.DontRequireReceiver);
			//			}
		}
		else
		{
			turretsFirstWave = 3;
			turretsSecondWave = 8;
			turretsThirdWave = 1;
			
			//			for(int i=0;i<turretsFirstWave;i++)
			//			{
			//				TurretsLevel3[i].collider2D.enabled = true;
			//				//TurretsLevel3[i].SendMessage("ShowTurret", SendMessageOptions.DontRequireReceiver);
			//			}
		}
		currentWave = turretsFirstWave;
		rightWingCount = leftWingCount = turretsSecondWave/2;
		SetHealthBar();
	}
	
	public void SetHealthBar()
	{
		healthFillRate = health/(float)healthStart;
		healthBar.GetComponent<Image>().fillAmount = healthFillRate;
		//healthText.text = health.ToString();
	}
	
	void Update () 
	{
		//		eventCounter++;
		//
		//		if(turretsFirstWave > 0)
		//		{
		//
		//		}
		//		else if(turretsSecondWave > 0)
		//		{
		//
		//		}
		//		else
		//		{
		//
		//		}
		if(bossCanMove)
			transform.position = new Vector3(transform.position.x, camera.position.y + cameraOffsetY, transform.position.z);
	}
	
	IEnumerator AdjustCameraSpeedForBoss()
	{
		yield return new WaitForSeconds(1f);
		while(Tut_GameManager.Instance.speed != bgMovingSpeed)
		{
			yield return null;
			Tut_GameManager.Instance.speed = Mathf.MoveTowards(Tut_GameManager.Instance.speed, bgMovingSpeed, 0.2f);
		}
		Tut_LevelGenerator.Instance.SendMessage("PositionBoss",SendMessageOptions.DontRequireReceiver);
		
		transform.GetChild(0).GetComponent<Animation>().Play();
		transform.GetChild(0).GetComponent<Animation>().PlayQueued("BossPlaneMovement1",QueueMode.CompleteOthers);
		Tut_PlaneManager.Instance.dontSlowTime = 0;
		Tut_PlaneManager.Instance.gameActive = true;
		Tut_PlaneManager.Instance.isShooting = true;
		Tut_PlaneManager.Instance.bossTimePart = false;
		Tut_PlaneManager.Instance.transform.GetChild(0).Find("Elipse").GetComponent<Animation>()["Tut_PandaPlaneElipseRotationAnimation"].speed = 1;
		Time.timeScale = 1;
		
		//DialogPanda.dialogPressed = false;
		//DialogBoss.dialogPressed = false;
	}
	
	//	void OnTriggerEnter2D(Collider2D col)
	//	{
	//		if(col.tag.Equals("PlayerBullet"))
	//		{
	//			if(health <= 0)
	//			{
	//				gameObject.SetActive(false);
	//				Camera.main.transform.Find("Splendid").renderer.enabled = true;
	//				Transform warpZone = GameObject.Find("WarpZone").transform;
	//				warpZone.GetComponent<OffsetTexture>().enabled = true;
	//				warpZone.position = new Vector3(warpZone.position.x,Camera.main.transform.position.y,warpZone.position.z);
	//				warpZone.parent = Camera.main.transform;
	//				warpZone.renderer.enabled = true;
	//				Camera.main.transform.position = new Vector3(0,0,-30);
	//				Invoke("StartLoadNewStage",1f);
	//				Tut_GameManager.Instance.bossTime = false;
	//			}
	//			else
	//			{
	//				health-=5;
	//				col.gameObject.SetActive(false);
	//				transform.Find("HealthBar").localScale -= new Vector3(1f/80, 0, 0);
	//				transform.Find("BossHit").renderer.enabled = true;
	//				StartCoroutine(TurnOffDamageSprite());
	//			}
	//		}
	//	}
	
	public void DestroyBoss()
	{
		SoundManager.Instance.Stop_BossPlaneMovement();
		SoundManager.Instance.Stop_BossMusic();
		SoundManager.Instance.Play_BossExplosion();
		//gameObject.SetActive(false);
		transform.GetChild(0).GetComponent<Animation>().Play("DeathAnimation");
		Tut_PopUpHandler.popupType = 5;
		Invoke("BossDestroyed",9f);
		Tut_PlaneManager.Instance.dontSlowTime = 2;
		Invoke("StopBossExplosion",5f);
	}
	
	void StopBossExplosion()
	{
		SoundManager.Instance.Stop_BossExplosion();
	}
	
	void BossDestroyed()
	{
		//Camera.main.transform.Find("Splendid").renderer.enabled = true;
		//Tut_PlaneManager.Instance.guiCamera.transform.Find("Splendid").renderer.enabled = true;
		//Transform warpZone = GameObject.Find("WarpZone").transform;
		//SoundManager.Instance.Stop_BossExplosion();
		GameObject warpZone = Instantiate(Tut_LevelGenerator.Instance.warpZonePrefab) as GameObject;
		//warpZone.GetComponent<OffsetTexture>().enabled = true;
		warpZone.transform.parent = Camera.main.transform;
		//warpZone.transform.position = new Vector3(warpZone.transform.position.x,Camera.main.transform.position.y,warpZone.transform.position.z);
		warpZone.transform.localPosition = new Vector3(0,3.75f,27);
		//warpZone.parent = Camera.main.transform;
		//warpZone.renderer.enabled = true;
		Invoke("ResetCameraPosition",0.5f);
		Invoke("StartLoadNewStage",1.5f);
		Tut_GameManager.Instance.bossTime = false;
	}
	
	IEnumerator TurnOffDamageSprite()
	{
		yield return new WaitForSeconds(0.15f);
		transform.Find("BossHit").GetComponent<Renderer>().enabled = false;
	}
	
	void ResetCameraPosition()
	{
		Camera.main.transform.parent.position = new Vector3(0,0,Camera.main.transform.position.z);
		Tut_GameManager.Instance.speed = 0;
		Tut_LevelGenerator.Instance.StageClear();
	}
	
	void StartLoadNewStage()
	{
		//Tut_PlaneManager.Instance.guiCamera.transform.Find("Splendid").renderer.enabled = false;
		//Tut_LevelGenerator.Instance.StageClear();
		GameObject.Destroy(this.gameObject);
		Tut_LevelGenerator.Instance.DestroyTerrains();
	}
	
	public void EnableSecondWave()
	{
		if(bossLevel == 1)
		{
			for(int i=turretsFirstWave;i<turretsFirstWave+turretsSecondWave;i++)
			{
				TurretsLevel1[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel1[i].SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(bossLevel == 2)
		{
			for(int i=turretsFirstWave;i<turretsFirstWave+turretsSecondWave;i++)
			{
				TurretsLevel2[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel2[i].SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(bossLevel == 3)
		{
			for(int i=turretsFirstWave;i<turretsFirstWave+turretsSecondWave;i++)
			{
				TurretsLevel3[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel3[i].SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
			}
		}
		currentWave = turretsSecondWave;
	}
	
	public void EnableThirdWave()
	{
		if(bossLevel == 1)
		{
			for(int i=turretsFirstWave+turretsSecondWave;i<turretsFirstWave+turretsSecondWave+turretsThirdWave;i++)
			{
				TurretsLevel1[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel1[i].SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(bossLevel == 2)
		{
			for(int i=turretsFirstWave+turretsSecondWave;i<turretsFirstWave+turretsSecondWave+turretsThirdWave;i++)
			{
				TurretsLevel2[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel2[i].SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(bossLevel == 3)
		{
			for(int i=turretsFirstWave+turretsSecondWave;i<turretsFirstWave+turretsSecondWave+turretsThirdWave;i++)
			{
				TurretsLevel3[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel3[i].SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
			}
		}
		currentWave = turretsThirdWave;
	}
	
	void GetAvailableTerrainType()
	{
		Tut_LevelGenerator.Instance.currentBossAvailableTerrainType = availableTerrainType;
	}
	void SetCameraOffsetY()
	{
		cameraOffsetY = Tut_LevelGenerator.Instance.currentBossCameraOffsetY;
		bossCanMove = true;
		//SoundManager.Instance.Play_BossPlaneMovement();
	}
	
	void AdjustSpeed()
	{
		StartCoroutine(AdjustCameraSpeedForBoss());
	}
	
	void FireAway()
	{
		if(bossLevel == 1)
		{
			for(int i=0;i<turretsFirstWave;i++)
			{
				TurretsLevel1[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel1[i].SendMessage("FireAway", SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(bossLevel == 2)
		{
			for(int i=0;i<turretsFirstWave;i++)
			{
				TurretsLevel2[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel2[i].SendMessage("FireAway", SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(bossLevel == 3)
		{
			for(int i=0;i<turretsFirstWave;i++)
			{
				TurretsLevel3[i].GetComponent<Collider2D>().enabled = true;
				TurretsLevel3[i].SendMessage("FireAway", SendMessageOptions.DontRequireReceiver);
			}
		}
		GameObject.Find("PlaneMainGunHolder").SendMessage("MainGunFireStart",SendMessageOptions.DontRequireReceiver);
	}

	void FirstPositionBoss()
	{
		//currentBossCameraOffsetY = 7;
		cameraOffsetY = 7;
		transform.position = new Vector3(0, Camera.main.transform.position.y+cameraOffsetY, -19.5f);//-18
		transform.GetChild(0).GetComponent<Animation>().Play();
		cameraOffsetY = 8;
		bossCanMove = true;
	}

	void LaunchSecretWeapon()
	{
		Invoke("LaunchSecretWeaponWithDelay",3f);
	}

	void LaunchSecretWeaponWithDelay()
	{
		bossSecretWeapon.SetActive(true);
	}
}
