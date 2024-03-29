﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PandaPlane : MonoBehaviour {

	public static int enemyLevelIndex;
	public static int stars;
	public static int score = 0;
	public static int highScore;
	public static int collectableNumber;
	int damagedState=0, lastDamagedState=-1; // 0-full 9-utepan skroz
	GameObject damagedPlaneHolder;
	public static int enemiesKilledFromLastCollectable = 0;
	float healthFillRate, armorFillRate;
	GameObject pandaHealthBar, pandaArmorBar;
	public static int health, healthStart, armor, armorStart, healthLvl, armorLvl; //health-ukupan health aviona, armor-ukupan armor aviona, healthLvl-trenutni lvl health-a inicijalno 1, armorLvl-trenutni lvl armor-a inicijalno 0
	public static int mainGunLvl, wingGunLvl, sideGunLvl; //mainGunLvl-trenutni level mainGun-a inicijalno lvl 1, wingGunLvl-trenutni level wingGunLvl-a inicijalno lvl 0, sideGunLvl-trenutni level sideGunLvl-a inicijalno lvl 0
	public static int mainGunDamage, wingGunDamage, sideGunDamage;
	public static int magnetLvl,shieldLvl, doubleStarsLvl, bombLvl, teslaLvl, laserLvl, bladesLvl;
	public static float magnetDuration, shieldDuration, doubleStarsDuration;
	public static int bombDamage, teslaDamage, laserDamage, bladesDamage;
	//public static int[] mainGunDamageValues = new int[] {50, 75, 100, 125, 150, 175, 200, 225, 250, 275, 300};
	public static int[] mainGunDamageValues = new int[] {350, 375, 400, 425, 450, 475, 500, 525, 550, 575, 600};
	public static int[] wingGunDamageValues = new int[] {0, 25, 35, 45, 55, 65, 75, 85, 95, 105, 115};
	public static int[] sideGunDamageValues = new int[] {0, 25, 35, 45, 55, 65, 75, 85, 95, 105, 115};
	//public static int[] healthValues = new int[] {1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000};
	public static int[] healthValues = new int[] {8000, 8100, 8200, 8300, 8400, 8500, 8600, 8700, 8800, 8900, 9000};
	public static int[] armorValues = new int[] {0, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200};

	public static List<int> collectables=new List<int>();
	public static int lastGeneratedCollectable=0, generatedCollectable=0;

	public static float[] magnetDurationValues = new float[] {4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f};
	public static float[] shieldDurationValues = new float[] {4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f};
	public static float[] doubleStarsDurationValues = new float[] {4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f};
	//trajanje za ova cetri power up-a je uvek isto, upgradeuje se samo damage
	public static int[] bombDamageValues = new int[]  {0, 150, 200, 250, 300, 375, 450, 525, 600, 700, 800};
	//public static int[] laserDamageValues = new int[] {0, 200, 275, 350, 425, 500, 575, 650, 725, 800, 875};
	public static int[] laserDamageValues = new int[] {0, 30, 45, 60, 75, 90, 105, 120, 135, 150, 180};
	//public static int[] teslaDamageValues = new int[] {0, 175, 225, 275, 325, 375, 425, 475, 525, 575, 625};
	public static int[] teslaDamageValues = new int[] {0, 15, 25, 35, 45, 55, 65, 75, 85, 95, 120};
	public static int[] bladesDamageValues = new int[] {0, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600};

	public static float bombRadius = 5f;
	public static float teslaDuration = 3f;
	public static float laserDuration = 5f;
	public static float bladesDuration = 7f;
	public static int teslaWeaponPrice = 400, laserWeaponPrice = 400, bladesWeaponPrice = 200, bombWeaponPrice = 600;
	public static int teslaWeaponNumber, laserWeaponNumber, bladesWeaponNumber, bombWeaponNumber;
	int maceDamage;
	public Transform planeHit;
	Animation pandaPlaneAnimation;
	[HideInInspector] public int damageReceived;

	int damageBossLaserLvl1;
	int damageBossTurretLvl1;

	[HideInInspector] public bool continuousDamage = false;
	Text healthText, armorText, scoreText;
	Camera guiCamera;
	bool doubleStarsActive = false;
	bool magnetActive = false;
	bool shieldActive = false;
	Transform shield;
	Transform magnet;
	public static int numberOfKills = 0;
	public static int allowedKeepPlayingNumber = 1;
	Animator indicatorDoubleStars;
	Animator indicatorMagnet;
	Animator indicatorShield;
	Animator dangerZone;
	bool dangerZoneBlinking = false;
	[HideInInspector] public bool invincible = false;

	static PandaPlane instance;
	
	public static PandaPlane Instance
	{
		get
		{
			if(instance == null)
				instance = GameObject.FindObjectOfType(typeof(PandaPlane)) as PandaPlane;
			
			return instance;
		}
	}

	// Use this for initialization
	void Awake () {


		shield = transform.GetChild(0).Find("Shield");
		magnet = transform.GetChild(0).Find("Magnet");
		healthText = GameObject.Find("PandaHealthBar/HealthBarText").GetComponent<Text>();
		armorText = GameObject.Find("PandaArmorBar/ArmorBarText").GetComponent<Text>();
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		scoreText.text = "SCORE: "+score.ToString();

		indicatorDoubleStars = GameObject.Find("IndicatorDoubleStars").GetComponent<Animator>();
		indicatorMagnet = GameObject.Find("IndicatorMagnet").GetComponent<Animator>();
		indicatorShield = GameObject.Find("IndicatorShield").GetComponent<Animator>();

		dangerZone = GameObject.Find("DangerZone").GetComponent<Animator>();
		if(!PlayerPrefs.HasKey("MaxStage"))
		{
			collectableNumber = Random.Range(8,16);
		}
		else
		{
			collectableNumber = Random.Range(15,26);
		}

		pandaHealthBar = GameObject.Find("PandaHealthBar");
		pandaArmorBar = GameObject.Find("PandaArmorBar");
		//damageBossLaserLvl1 = healthValues[healthLvl] * LevelGenerator.Instance.currentStage / healthLvl;
		guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();

		enemyLevelIndex=0;
		//GameObject.Find("StageNumberText").GetComponent<Text>().text = "Stage "+LevelGenerator.currentStage; //I OVDE JE IZBACIO NULL REFERENCE EXCEPTION
		GameObject.Find("SlowTimeScreen").transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = "Stage "+LevelGenerator.currentStage;
		//if(PlayerPrefs.HasKey("HealthLvl"))
		if(PlayerPrefs.HasKey("Sarma"))
		{
			InitialisePlaneState();
		}
		else
		{
			stars=MenuManager.defaultStarsNumber;
			healthLvl=MenuManager.defaultHealthLvl;
			armorLvl=MenuManager.defaultArmorLvl;
			mainGunLvl=MenuManager.defaultMainGunLvl;
			wingGunLvl=MenuManager.defaultWingGunLvl;
			sideGunLvl=MenuManager.defaultSideGunLvl;
			magnetLvl=MenuManager.defaultMagnetLvl;
			shieldLvl=MenuManager.defaultShieldLvl;
			doubleStarsLvl=MenuManager.defaultDoubleStarsLvl;
			bombLvl=MenuManager.defaultBombLvl;
			teslaLvl=MenuManager.defaultTeslaLvl;
			laserLvl=MenuManager.defaultLaserLvl;
			bladesLvl=MenuManager.defaultBladesLvl;
			highScore = 0;

			health=healthValues[healthLvl];
			healthStart = health;
			armor=armorValues[armorLvl];
			armorStart = armor;
			mainGunDamage=mainGunDamageValues[mainGunLvl];
			wingGunDamage=wingGunDamageValues[wingGunLvl];
			sideGunDamage=sideGunDamageValues[sideGunLvl];
			magnetDuration=magnetDurationValues[magnetLvl];
			shieldDuration=shieldDurationValues[shieldLvl];
			doubleStarsDuration=doubleStarsDurationValues[doubleStarsLvl];
			bombDamage=bombDamageValues[bombLvl];
			teslaDamage=teslaDamageValues[teslaLvl];
			laserDamage=laserDamageValues[laserLvl];
			bladesDamage=bladesDamageValues[bladesLvl];

			teslaWeaponNumber = MenuManager.defaultTeslaWeaponNumber;
			laserWeaponNumber = MenuManager.defaultLaserWeaponNumber;
			bladesWeaponNumber = MenuManager.defaultBladesWeaponNumber;
			bombWeaponNumber = MenuManager.defaultBombWeaponNumber;

			SavePlaneState();
		}

		enemyLevelIndex = CalculateEnemyIndex();
//		GameObject.Find("Health").GetComponent<TextMesh>().text=health.ToString();
//		GameObject.Find("Armor").GetComponent<TextMesh>().text=armor.ToString();
//		GameObject.Find("Coins").GetComponent<TextMesh>().text=coins.ToString();
		pandaPlaneAnimation = transform.GetChild(0).GetComponent<Animation>();

	
		if(armorLvl>0)
		{
			SetArmorBar();
		}
		collectables.Clear();
		GenerateCollectableList();
		if(armorLvl<1)
		{
			GameObject.Find("PandaArmor").SetActive(false);
		}

		if(armorLvl<4)
		{
			damagedPlaneHolder = GameObject.Find("DamagedPlaneLV1");
		}
		else if(armorLvl<8)
		{
			damagedPlaneHolder = GameObject.Find("DamagedPlaneLV2");
		}
		else
		{
			damagedPlaneHolder = GameObject.Find("DamagedPlaneLV3");
		}

		SetHealthBar();
		maceDamage=(int)(LevelGenerator.currentStage * (float)healthValues[1]*0.15f);

		
		
	}

	void Start()
	{
		PlaneManager.Instance.DetermineGunsAndArmorLevel();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
//		if(col.GetComponent<EnemyDamage>().damageType == EnemyDamage.type.bossLaserLvl1)
//		{
//			health -= damageBossLaserLvl1;
//		}

		//health -= damageReceived;
		Debug.Log("OnTriggerEnter2D");
		if(col.gameObject.layer.Equals(LayerMask.NameToLayer("EnemyBullet")))
		{
			if(col.tag.Equals("Laser"))
			{
				if(!shieldActive)
				{
					continuousDamage = true;
					StartCoroutine(DoContinuousDamage(col.GetComponent<EnemyDamage>().damage, col.gameObject));
				}
			}
			else if(col.tag.Equals("Mace"))
			{
				if(!shieldActive)
					TakeDamage(maceDamage);
			}
			else if(col.tag.Equals("MineExplosion"))
			{
				col.enabled = false;
				if(!shieldActive)
					TakeDamage(100);
			}
			else if(col.tag.Equals("LongBullet"))
			{
				if(col.name.Contains("Rocket"))
				{
					col.transform.parent.GetComponent<Animation>().Play("Death");
					col.transform.parent.GetComponent<Homming>().enabled=false;
					col.transform.parent.GetComponent<Homming>().AvoidAutoDestruction();
					SoundManager.Instance.Play_EnemyPlaneExplode();
					//     col.transform.parent.gameObject.SetActive(false);
				}
				else
				{
					Destroy(col.gameObject);
				}
				if(!shieldActive)
					TakeDamage(col.GetComponent<EnemyDamage>().damage);
			}
			else if(col.tag.Equals("Blades"))
			{
				if(!shieldActive)
					TakeDamage(col.GetComponent<EnemyDamage>().damage);
				col.enabled = false;
			}
			else
			{
				//col.gameObject.SetActive(false);
				col.gameObject.SendMessage("ResetBullet",SendMessageOptions.DontRequireReceiver);
				if(!shieldActive)
					TakeDamage(col.GetComponent<EnemyDamage>().damage);
			}
		}
		else if(col.gameObject.layer.Equals(LayerMask.NameToLayer("EnemyPlane")))
		{
			if(col.tag.Equals("Meteor"))
			{
				if(!shieldActive)
					TakeDamage(750);
			}
			else
			{
				if(!shieldActive)
				{
					if(col.GetComponent<Enemy>().health<500)
					{
						TakeDamage(col.GetComponent<Enemy>().health);
					}
					else
					{
						TakeDamage(500);
					}
				}
				col.GetComponent<Enemy>().TakeDamage(5000);
			}
		}
		else if(col.tag.Equals("Star"))
		{
			Debug.Log("OnTriggerEnter2D Star pokupi");
			SoundManager.Instance.Play_CollectStar();
			StarDestroyer starScript = col.GetComponent<StarDestroyer>();
			if(starScript.dragging)
				starScript.dragging = false;

			if(doubleStarsActive)
			{
				stars += 2;
				AddScore(10);
			}
			else
			{
				stars++;
				AddScore(5);
			}
			col.enabled = false;
			col.transform.parent.GetComponent<Animation>().Play("StarColect");
			Destroy(col.transform.parent.parent.gameObject, 1f);
		}

		else if(col.name.Contains("PowerUp_Shield"))
		{
			SoundManager.Instance.Play_CollectPowerUp();
			col.enabled = false;
			col.transform.GetChild(0).GetComponent<Animation>().Play("PowerUpCollect");
			Destroy(col.gameObject,1.5f);
			shieldActive = true;
			shield.gameObject.SetActive(true);
			indicatorShield.Play("IndicatorArrival");
			Invoke("TurnOffShieldAnimation",shieldDuration-1);
		}
		else if(col.name.Contains("PowerUp_Magnet"))
		{
			SoundManager.Instance.Play_CollectPowerUp();
			col.enabled = false;
			col.transform.GetChild(0).GetComponent<Animation>().Play("PowerUpCollect");
			Destroy(col.gameObject,1.5f);
			magnetActive = true;
			magnet.gameObject.SetActive(true);
			indicatorMagnet.Play("IndicatorArrival");
			Invoke("TurnOffMagnetAnimation",magnetDuration-1);
		}
		else if(col.name.Contains("PowerUp_DoubleStars"))
		{
			SoundManager.Instance.Play_CollectPowerUp();
			col.enabled = false;
			col.transform.GetChild(0).GetComponent<Animation>().Play("PowerUpCollect");
			Destroy(col.gameObject,1.5f);
			doubleStarsActive = true;
			indicatorDoubleStars.Play("IndicatorArrival");
			Invoke("TurnOffDoubleStarsAnimation",doubleStarsDuration-1);
		}
		else if(col.name.Contains("PowerUp_Health"))
		{
			SoundManager.Instance.Play_CollectPowerUp();
			col.enabled = false;
			col.transform.GetChild(0).GetComponent<Animation>().Play("PowerUpCollect");
			Destroy(col.gameObject,1.5f);
			//Destroy (col.gameObject);
			AddHealthPercent(25);
		}
		else if(col.name.Contains("PowerUp_Armor"))
		{
			SoundManager.Instance.Play_CollectPowerUp();
			col.enabled = false;
			col.transform.GetChild(0).GetComponent<Animation>().Play("PowerUpCollect");
			Destroy(col.gameObject,1.5f);
			//Destroy (col.gameObject);
			AddArmorPercent(25);
		}

	}

	void TurnOffShieldAnimation()
	{
		indicatorShield.Play("IndicatorDeparting");
		StartCoroutine(TurnOffShield());
	}
	void TurnOffMagnetAnimation()
	{
		indicatorMagnet.Play("IndicatorDeparting");
		StartCoroutine(TurnOffMagnet());
	}
	void TurnOffDoubleStarsAnimation()
	{
		indicatorDoubleStars.Play("IndicatorDeparting");
		StartCoroutine(TurnOffDoubleStars());
	}

	IEnumerator TurnOffShield()
	{
		yield return new WaitForSeconds(2f);
		shieldActive = false;
		shield.gameObject.SetActive(false);
	}
	IEnumerator TurnOffMagnet()
	{
		yield return new WaitForSeconds(2f);
		magnetActive = false;
		magnet.gameObject.SetActive(false);
	}
	IEnumerator TurnOffDoubleStars()
	{
		yield return new WaitForSeconds(2f);
		doubleStarsActive = false;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(continuousDamage)
			continuousDamage = false;
	}

	IEnumerator DoContinuousDamage(int damage, GameObject obj)
	{
		while(continuousDamage)
		{
			if(obj != null && obj.activeSelf)
			{
				TakeDamage(damage);
				yield return new WaitForSeconds(0.1f);
			}
			else
				continuousDamage = false;
		}
	}

//	void TakeDamage(int damage)
//	{
//		health-=damage;
//
//		if(health <= 0)
//		{
//			health = 0;
//			continuousDamage = false;
////			pandaPlaneAnimation.Play("PandaPlaneDeath");
////			PlaneManager.Instance.DisablePlayer();
//		}
//		else
//		{
//			StartCoroutine(Hit());
//		}
//		healthText.text = health.ToString();
//	}
	public IEnumerator MoveCameraSlowly(float time)
	{
		while(GameManager.Instance.speed != time)
		{
			yield return null;
			GameManager.Instance.speed = Mathf.MoveTowards(GameManager.Instance.speed, time, 0.2f);
		}
	}

	public void TakeDamage(int damage)
	{
		if(!invincible)
		{
			if(armor>0)
			{
				SoundManager.Instance.Play_PlayerHit();
				if((armor-damage)>=0)
				{
					armor-=damage;
					SetArmorBar();
				}
				else
				{
					int leftover=damage-armor;
					armor=0;
                    string s = name;
                    char[] sc = s.ToCharArray();
					if(health-leftover<0)
					{
						health=0;
						damagedPlaneHolder.SetActive(false);
						continuousDamage = false;
						SoundManager.Instance.Play_PandaPlaneExplode();
						pandaPlaneAnimation.Play("PandaPlaneDeath");
						StartCoroutine(PlaneManager.Instance.TurnOffWeapon(0));
						StartCoroutine(MoveCameraSlowly(0));
						PlaneManager.Instance.DisablePlayer();
						
						SetHealthBar();
						numberOfKills++;
						if(numberOfKills <= allowedKeepPlayingNumber)
							//PopUpHandler.Instance.ShowKeepPlayingScreen();//复活todo
                            PopUpHandler.Instance.ShowGameOverScreen();
						else
							PopUpHandler.Instance.ShowGameOverScreen();
					}
					else
					{
						health-=leftover;
					}
					SetHealthBar();
					SetArmorBar();

				}
				StartCoroutine(Hit());
			}
			else
			{
				if((health-damage)>0)
				{
					SoundManager.Instance.Play_PlayerHit();
					StartCoroutine(Hit());
					health-=damage;
					SetHealthBar();
				}
				else
				{
					damagedPlaneHolder.SetActive(false);
					health=0;
					continuousDamage = false;
					SoundManager.Instance.Play_PandaPlaneExplode();
					pandaPlaneAnimation.Play("PandaPlaneDeath");
					StartCoroutine(PlaneManager.Instance.TurnOffWeapon(0));
					StartCoroutine(MoveCameraSlowly(0));
					PlaneManager.Instance.DisablePlayer();

					SetHealthBar();
					numberOfKills++;
					if(numberOfKills <= allowedKeepPlayingNumber)
						//PopUpHandler.Instance.ShowKeepPlayingScreen();
                        PopUpHandler.Instance.ShowGameOverScreen();
					else
						PopUpHandler.Instance.ShowGameOverScreen();
				}
			}
		}
	}
    
	IEnumerator Hit()
	{
		planeHit.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(0.1f);
		planeHit.GetComponent<Renderer>().enabled = false;
	}

	public void SavePlaneState()
	{
		string sarma = (healthLvl+7) + "#" + (armorLvl+7) + "#" + (mainGunLvl+14) + "#" + (wingGunLvl+21) + "#" + (sideGunLvl+35) + "#" + (magnetLvl+56) + "#" + (shieldLvl+91) + "#" + 
						(doubleStarsLvl+147) + "#" + (laserLvl+238) + "#" + (teslaLvl+385) + "#" + (bladesLvl+623) + "#" + (bombLvl+1008);

		PlayerPrefs.SetString("Sarma",sarma);

		string uzengije = (stars+10) + "#" + (highScore-20) + "#" + (laserWeaponNumber+5) + "#" + (teslaWeaponNumber+5) + "#" + (bladesWeaponNumber+5) + "#" + (bombWeaponNumber+5);

		PlayerPrefs.SetString("Uzengije",uzengije);
		PlayerPrefs.Save();

//		PlayerPrefs.SetInt("HealthLvl", healthLvl);
//		PlayerPrefs.SetInt("ArmorLvl", armorLvl);
//		PlayerPrefs.SetInt("MainGunLvl", mainGunLvl);
//		PlayerPrefs.SetInt("WingGunLvl", wingGunLvl);
//		PlayerPrefs.SetInt("SideGunLvl", sideGunLvl);
//		PlayerPrefs.SetInt("MagnetLvl", magnetLvl);
//		PlayerPrefs.SetInt("ShieldLvl", shieldLvl);
//		PlayerPrefs.SetInt("DoubleStarsLvl", doubleStarsLvl);
//		PlayerPrefs.SetInt("BombLvl", bombLvl);
//		PlayerPrefs.SetInt("TeslaLvl", teslaLvl);
//		PlayerPrefs.SetInt("LaserLvl", laserLvl);
//		PlayerPrefs.SetInt("BladesLvl", bladesLvl);
//		PlayerPrefs.SetInt("Stars", stars);

	}

	public void InitialisePlaneState()
	{
		if(LevelGenerator.currentStage == 1 || LevelGenerator.checkpoint)
		{
			//LevelGenerator.checkpoint = false;
			string sarma_San = PlayerPrefs.GetString("Sarma");
			string[] sarma = sarma_San.Split('#');
			healthLvl = int.Parse(sarma[0])-7;
			armorLvl = int.Parse(sarma[1])-7;
			mainGunLvl = int.Parse(sarma[2])-14;
			wingGunLvl = int.Parse(sarma[3])-21;
			sideGunLvl = int.Parse(sarma[4])-35;
			magnetLvl = int.Parse(sarma[5])-56;
			shieldLvl = int.Parse(sarma[6])-91;
			doubleStarsLvl = int.Parse(sarma[7])-147;
			laserLvl = int.Parse(sarma[8])-238;
			teslaLvl = int.Parse(sarma[9])-385;
			bladesLvl = int.Parse(sarma[10])-623;
			bombLvl = int.Parse(sarma[11])-1008;

			string uzengije = PlayerPrefs.GetString("Uzengije");
			string[] kamaraUzengije = uzengije.Split('#');
			stars = int.Parse(kamaraUzengije[0])-10;
			highScore = int.Parse(kamaraUzengije[1])+20;
			laserWeaponNumber = int.Parse(kamaraUzengije[2])-5;
			teslaWeaponNumber = int.Parse(kamaraUzengije[3])-5;
			bladesWeaponNumber = int.Parse(kamaraUzengije[4])-5;
			bombWeaponNumber = int.Parse(kamaraUzengije[5])-5;

	//		healthLvl=PlayerPrefs.GetInt("HealthLvl");
	//		armorLvl=PlayerPrefs.GetInt("ArmorLvl");
	//		mainGunLvl=PlayerPrefs.GetInt("MainGunLvl");
	//		wingGunLvl=PlayerPrefs.GetInt("WingGunLvl");
	//		sideGunLvl=PlayerPrefs.GetInt("SideGunLvl");
	//		magnetLvl = PlayerPrefs.GetInt("MagnetLvl");
	//		shieldLvl = PlayerPrefs.GetInt("ShieldLvl");
	//		doubleStarsLvl = PlayerPrefs.GetInt("DoubleStarsLvl");
	//		bombLvl = PlayerPrefs.GetInt("BombLvl");
	//		teslaLvl = PlayerPrefs.GetInt("TeslaLvl");
	//		laserLvl = PlayerPrefs.GetInt("LaserLvl");
	//		bladesLvl = PlayerPrefs.GetInt("BladesLvl");
	//		stars = PlayerPrefs.GetInt("Stars");

			health=healthValues[healthLvl];
			healthStart = health;
			armor=armorValues[armorLvl];
			armorStart = armor;
			mainGunDamage=mainGunDamageValues[mainGunLvl];
			wingGunDamage=wingGunDamageValues[wingGunLvl];
			sideGunDamage=sideGunDamageValues[sideGunLvl];
			magnetDuration=magnetDurationValues[magnetLvl];
			shieldDuration=shieldDurationValues[shieldLvl];
			doubleStarsDuration=doubleStarsDurationValues[doubleStarsLvl];
			bombDamage=bombDamageValues[bombLvl];
			teslaDamage=teslaDamageValues[teslaLvl];
			laserDamage=laserDamageValues[laserLvl];
			bladesDamage=bladesDamageValues[bladesLvl];
		}

	}

//	public void UpgradeHealth()
//	{
//		healthLvl=PlayerPrefs.GetInt("HealthLvl");
//		if(healthLvl<10)
//		{
//			Debug.Log("Usao i povecao");
//			healthLvl++;
//			PlayerPrefs.SetInt("HealthLvl", healthLvl);
//		}
//	}
//
//	public void UpgradeArmor()
//	{
//		armorLvl=PlayerPrefs.GetInt("ArmorLvl");
//		if(armorLvl<10)
//		{
//			armorLvl++;
//			PlayerPrefs.SetInt("ArmorLvl", armorLvl);
//		}
//	}
//
//	public void UpgradeMainGun()
//	{
//		mainGunLvl=PlayerPrefs.GetInt("MainGunLvl");
//		if(mainGunLvl<10)
//		{
//			mainGunLvl++;
//			PlayerPrefs.SetInt("MainGunLvl", mainGunLvl);
//		}
//	}
//
//	public void UpgradeWingGun()
//	{
//		wingGunLvl=PlayerPrefs.GetInt("WingGunLvl");
//		if(wingGunLvl<10)
//		{
//			wingGunLvl++;
//			PlayerPrefs.SetInt("WingGunLvl", wingGunLvl);
//		}
//	}
//
//	public void UpgradeSideGun()
//	{
//		sideGunLvl=PlayerPrefs.GetInt("SideGunLvl");
//		if(sideGunLvl<10)
//		{
//			sideGunLvl++;
//			PlayerPrefs.SetInt("SideGunLvl", sideGunLvl);
//		}
//	}
//
//	public void UpgradeMagnet()
//	{
//		magnetLvl = PlayerPrefs.GetInt("MagnetLvl");
//		if(magnetLvl<10)
//		{
//			magnetLvl++;
//			PlayerPrefs.SetInt("MagnetLvl", magnetLvl);
//		}
//	}
//
//	public void UpgradeShield()
//	{
//		shieldLvl = PlayerPrefs.GetInt("ShieldLvl");
//		if(shieldLvl<10)
//		{
//			shieldLvl++;
//			PlayerPrefs.SetInt("ShieldLvl", shieldLvl);
//		}
//	}
//
//	public void UpgradeDoubleStars()
//	{
//		doubleStarsLvl = PlayerPrefs.GetInt("DoubleStarsLvl");
//		if(doubleStarsLvl<10)
//		{
//			doubleStarsLvl++;
//			PlayerPrefs.SetInt("DoubleStarsLvl", doubleStarsLvl);
//		}
//	}
//
//	public void UpgradeBomb()
//	{
//		bombLvl = PlayerPrefs.GetInt("BombLvl");
//		if(bombLvl<10)
//		{
//			bombLvl++;
//			PlayerPrefs.SetInt("BombLvl", bombLvl);
//		}
//	}
//
//	public void UpgradeTesla()
//	{
//		teslaLvl = PlayerPrefs.GetInt("TeslaLvl");
//		if(teslaLvl<10)
//		{
//			teslaLvl++;
//			PlayerPrefs.SetInt("TeslaLvl", teslaLvl);
//		}
//	}
//
//	public void UpgradeLaser()
//	{
//		laserLvl = PlayerPrefs.GetInt("LaserLvl");
//		if(laserLvl<10)
//		{
//			laserLvl++;
//			PlayerPrefs.SetInt("LaserLvl", laserLvl);
//		}
//	}
//
//	public void UpgradeBlades()
//	{
//		bladesLvl = PlayerPrefs.GetInt("BladesLvl");
//		if(bladesLvl<10)
//		{
//			bladesLvl++;
//			PlayerPrefs.SetInt("BladesLvl", bladesLvl);
//		}
//	}

	public void AddHealth(int healthToAdd)
	{
		if((health+healthToAdd>healthValues[healthLvl]))
		{
			health=healthValues[healthLvl];
		}
		else
		{
			health+=healthToAdd;
		}
		SetHealthBar();
	}

	public void AddHealthPercent(int percent)
	{
		int healthToAdd=(percent*healthValues[healthLvl]/100);
		if((health+healthToAdd>healthValues[healthLvl]))
		{
			health=healthValues[healthLvl];
		}
		else
		{
			health+=healthToAdd;
		}
		SetHealthBar();
	}

	public void AddArmor(int armorToAdd)
	{
		if((armor+armorToAdd>armorValues[armorLvl]))
		{
			armor=armorValues[armorLvl];
		}
		else
		{
			armor+=armorToAdd;
		}
		SetArmorBar();
	}

	public void AddArmorPercent(int percent)
	{
		int armorToAdd=(percent*armorValues[armorLvl]/100);
		if((armor+armorToAdd>armorValues[armorLvl]))
		{
			armor=armorValues[armorLvl];
		}
		else
		{
			armor+=armorToAdd;
		}
		SetArmorBar();
	}

	public void AddStars(int numberOfStarsToAdd)
	{
		stars+=numberOfStarsToAdd;
		//GameObject.Find("Stars").GetComponent<TextMesh>().text=stars.ToString();
	}

	public void TakeAwayStars(int numberOfStarsToTake)
	{
		if((stars-numberOfStarsToTake<0))
		{
			stars=0;
		}
		else
		{
			stars-=numberOfStarsToTake;
		}
		//GameObject.Find("Stars").GetComponent<TextMesh>().text=stars.ToString();
	}

	int CalculateEnemyIndex()
	{
		int number = 0;
		if(healthLvl<4)
		{
			number = 1;
		}
		else if(healthLvl<7)
		{
			number = 2;
		}
		else
		{
			number = 3;
		}
		
		if(armorLvl<4)
		{
			number += 1;
		}
		else if(armorLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}
		
		if(mainGunLvl<4)
		{
			number += 1;
		}
		else if(mainGunLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}
		
		
		if(sideGunLvl<4)
		{
			number += 1;
		}
		else if(sideGunLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}
		
		if(wingGunLvl<4)
		{
			number += 1;
		}
		else if(wingGunLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}


		if(doubleStarsLvl<4)
		{
			number += 1;
		}
		else if(doubleStarsLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}


		if(magnetLvl<4)
		{
			number += 1;
		}
		else if(magnetLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}



		if(shieldLvl<4)
		{
			number += 1;
		}
		else if(shieldLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}


		if(bombLvl<4)
		{
			number += 1;
		}
		else if(bombLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}


		if(teslaLvl<4)
		{
			number += 1;
		}
		else if(teslaLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}


		if(laserLvl<4)
		{
			number += 1;
		}
		else if(laserLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}


		if(bladesLvl<4)
		{
			number += 1;
		}
		else if(bladesLvl<7)
		{
			number += 2;
		}
		else
		{
			number += 3;
		}


		return number;

	}

	void SetHealthBar()
	{
		healthFillRate = health/(float)healthStart;
		if(healthFillRate <= 0.1f && !dangerZoneBlinking)
		{
			dangerZone.Play("DangerZoneBlink");
			dangerZoneBlinking = true;
		}
		else if(healthFillRate > 0.1f && dangerZoneBlinking)
		{
			dangerZone.Play("Default");
			dangerZoneBlinking = false;
		}
		if(healthFillRate <=0 && dangerZoneBlinking)
		{
			dangerZone.SetTrigger("StopBlink");
			dangerZoneBlinking = false;
		}


		if(healthFillRate<0.1f)
		{
			damagedState=9;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
				for(int i=0;i<damagedPlaneHolder.transform.childCount;i++)
				{
					damagedPlaneHolder.transform.GetChild(i).gameObject.SetActive(true);
				}
			}


		}
		else if(healthFillRate<0.2f)
		{
			damagedState=8;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate<0.3f)
		{
			damagedState=7;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate<0.4f)
		{
			damagedState=6;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate<0.5f)
		{
			damagedState=5;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate<0.6f)
		{
			damagedState=4;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate<0.7f)
		{
			damagedState=3;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate<0.8f)
		{
			damagedState=2;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate<0.9f)
		{
			damagedState=1;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			damagedPlaneHolder.transform.GetChild(0).gameObject.SetActive(true);
			damagedPlaneHolder.transform.GetChild(1).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(2).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(3).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(4).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(5).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(6).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(7).gameObject.SetActive(false);
			damagedPlaneHolder.transform.GetChild(8).gameObject.SetActive(false);
			}
		}
		else if(healthFillRate>0.9f)
		{
			damagedState=0;
			if(lastDamagedState!=damagedState)
			{
				lastDamagedState=damagedState;
			for(int i=0;i<damagedPlaneHolder.transform.childCount;i++)
			{
				damagedPlaneHolder.transform.GetChild(i).gameObject.SetActive(false);
			}
			}
		}





		pandaHealthBar.GetComponent<Image>().fillAmount = healthFillRate;
		healthText.text = health.ToString();
	}
	
	void SetArmorBar()
	{
		armorFillRate = armor/(float)armorStart;
		pandaArmorBar.GetComponent<Image>().fillAmount = armorFillRate;
		armorText.text = armor.ToString();
	}
	
	public void AddScore(int numberToAdd)
	{
		score += numberToAdd;
		scoreText.text = "SCORE: "+score.ToString();
	}
	
	public void GenerateCollectableList()
	{
		//LEGEND: 1-health; 2-doubleStars; 3-magnet; 4-shield; 5-armor
		collectables.Add(1);
		collectables.Add(2);
		collectables.Add(3);
		collectables.Add(4);
		if(armorLvl>0)
			collectables.Add(5);
		
	}

	public void NewPlane()
	{

		damagedPlaneHolder.SetActive(true);
		health = healthStart;
		SetHealthBar();
		PopUpHandler.Instance.ShowMenu();
		PlaneManager.Instance.EnablePlayer();
		StartCoroutine(MoveCameraSlowly(3));
	}
}