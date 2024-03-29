using UnityEngine;
using System.Collections;

public class Tut_Enemy : MonoBehaviour {
	
	GameObject thisEnemy;
	int  healthIndex;
	public int damage=0;
	public int health;
	public int minStage;
	public bool HasGun;
	
	bool canShoot = false;
	Transform enemyBulletPool;
	Transform mainCameraTransform;
	
	static int bulletIndex=0;
	[SerializeField][HideInInspector]
	public float fireRate = 0.5f;
	float fireRateCounter = 0;
	int[] healthValuesMainGunForAirBasic = new int[] {50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
	int[] healthValuesWingGunLvlForAirBasic = new int[] {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110};
	int[] healthValuesSideGunLvlForAirBasic = new int[] {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110};
	int[] healthValuesBombLvlForAirBasic = new int[] {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110};
	int[] healthValuesTeslaLvlForAirBasic = new int[] {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110};
	int[] healthValuesLaserLvlForAirBasic = new int[] {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110};
	int[] healthValuesBladesLvlForAirBasic = new int[] {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110};
	
	int[] healthValuesMainGunForAirIntermediate = new int[] {100, 150, 300, 450, 600, 750, 900, 1050, 1200, 1350, 1500};
	int[] healthValuesWingGunLvlForAirIntermediate = new int[] {10, 25, 40, 55, 70, 85, 100, 115, 130, 145, 160};
	int[] healthValuesSideGunLvlForAirIntermediate = new int[] {10, 25, 40, 55, 70, 85, 100, 115, 130, 145, 160};
	int[] healthValuesBombLvlForAirIntermediate = new int[] {10, 25, 40, 55, 70, 85, 100, 115, 130, 145, 160};
	int[] healthValuesTeslaLvlForAirIntermediate = new int[] {10, 25, 40, 55, 70, 85, 100, 115, 130, 145, 160};
	int[] healthValuesLaserLvlForAirIntermediate = new int[] {10, 25, 40, 55, 70, 85, 100, 115, 130, 145, 160};
	int[] healthValuesBladesLvlForAirIntermediate = new int[] {10, 25, 40, 55, 70, 85, 100, 115, 130, 145, 160};
	
	int[] healthValuesMainGunForAirAdvanced = new int[] {150, 200, 400, 600, 800, 1000, 1200, 1400, 1600, 1800, 2000};
	int[] healthValuesWingGunLvlForAirAdvanced = new int[] {20, 30, 60, 90, 120, 150, 170, 190, 210, 230, 250};
	int[] healthValuesSideGunLvlForAirAdvanced = new int[] {20, 30, 60, 90, 120, 150, 170, 190, 210, 230, 250};
	int[] healthValuesBombLvlForAirAdvanced = new int[] {20, 30, 60, 90, 120, 150, 170, 190, 210, 230, 250};
	int[] healthValuesTeslaLvlForAirAdvanced = new int[] {20, 30, 60, 90, 120, 150, 170, 190, 210, 230, 250};
	int[] healthValuesLaserLvlForAirAdvanced = new int[] {20, 30, 60, 90, 120, 150, 170, 190, 210, 230, 250};
	int[] healthValuesBladesLvlForAirAdvanced = new int[] {20, 30, 60, 90, 120, 150, 170, 190, 210, 230, 250};
	
	int[] healthValuesMainGunForGroundAndWaterStatic = new int[] {70, 130, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesWingGunLvlForGroundAndWaterStatic = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesSideGunLvlForGroundAndWaterStatic = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesBombLvlForGroundAndWaterStatic = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesTeslaLvlForGroundAndWaterStatic = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesLaserLvlForGroundAndWaterStatic = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesBladesLvlForGroundAndWaterStatic = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	
	int[] healthValuesMainGunForGroundAndWaterMobile = new int[] {90, 140, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesWingGunLvlForGroundAndWaterMobile = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesSideGunLvlForGroundAndWaterMobile = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesBombLvlForGroundAndWaterMobile = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesTeslaLvlForGroundAndWaterMobile = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesLaserLvlForGroundAndWaterMobile = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	int[] healthValuesBladesLvlForGroundAndWaterMobile = new int[] {50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550};
	
	public enum EnemyType{
		AirBasic,  // leteci neprijatelj bez pucaljki
		AirIntermediate,  // leteci neprijatelj sa pucaljkama, kao i razne prepreke
		AirAdvanced,  //napredne letelice
		GroundAndWaterStatic,  //zemljani mobilni(krecu se) neprijatelj(tenk, razna vozila...) svi pucaju
		GroundAndWaterMobile//vodeni staticni neprijatelj(brod, podmornica...) svi pucaju
	};
	[SerializeField]
	public enum AirBasic{
		AirBasic1StraighForward,
		AirBasic2UpperLeftLowerRight,
		AirBasic3UpperRightLowerLeft,
		AirBasic4Upper180RightToLeft,
		AirBasic5LeftMiddleRightMiddlePlusOffset,
		AirBasic6RightMiddleLeftMiddlePlusOffset,
		AirBasic7MiddleLeftToRightWithJump,
		AirBasic8LeftToRightMiddleDown,
		AirBasic9TwoEnemiesMiddleThenLeftRight,
		AirBasic10FromRightAndLeftToMiddle,
		AirBasic11UpperLeftMiddleRightTwoWaves,
		AirBasic12MiddleToRightTurnAndBackStraightLine,
		AirBasic13CikCak,
		AirBasic14TwoWavesStraightForwardAndeLeftToRight,
		AirBasic15TwoWavesStraightForwardLeftAndRight,
		AirBasic16Loop,
		
	};
	[SerializeField]
	public enum AirIntermediate{
		AirIntermediate17FireBalls,
		AirIntermediate18FlyingMines,
		AirIntermediate19AirLavirint,
		AirIntermediate20GroundThunder,
		AirIntermediate21DumbSentinels,
		AirIntermediate22OneAndFire,
		AirIntermediate23FourAndFire,
		AirIntermediate24BottomLeftThenBottomRight,
		AirIntermediate25AirplaneBomb,
		AirIntermediate26CikCakWithPauseAndFire,
		AirIntermediate27RocketFire,
		AirIntermediate28UpperRightToMiddleWithFire,
		
	};
	[SerializeField]
	public enum AirAdvanced{
		AirAdvanced29TeslaPlane,
		AirAdvanced30OneToMany,
		AirAdvanced31WithDestructableObjectShield,
		AirAdvanced32WithBlades,
		AirAdvanced33FireWithRotation,
		AirAdvanced34FireForwardAllDirections,
		AirAdvanced35AirplaneWithLaser,
		
		
	};
	[SerializeField]
	public enum GroundAndWaterStatic{
		TankStatic,
		Turret1,
		Turret2,
	};
	[SerializeField]
	public enum GroundAndWaterMobile{
		TankMobile,
		ShipMobile,
		
	};
	
	public EnemyType TypeOfEnemy; // has a drop down list in inspector.
	[HideInInspector]
	public AirBasic AirBasicReference;
	[HideInInspector]
	public AirIntermediate AirIntermediateReference;
	[HideInInspector]
	public AirAdvanced AirAdvancedReference;
	[HideInInspector]
	public GroundAndWaterStatic GroundAndWaterStaticReference;
	[HideInInspector]
	public GroundAndWaterMobile GroundAndWaterMobileReference;
	
	bool continuousDamage = false;
	
	// Use this for initialization
	void Start () {
		thisEnemy = this.gameObject;
		enemyBulletPool = GameObject.Find("EnemyBulletPool").transform;
		
		if(!HasGun)
		{
			Object.Destroy(thisEnemy.transform.Find("BulletPool").transform.gameObject);
		}
		
		InitializeEnemyHealth();
		fireRate = 1.75f;//0.95f;
		mainCameraTransform = Camera.main.transform;
		damage = (int)(Tut_LevelGenerator.currentStage * (float)Tut_PandaPlane.healthValues[1]*0.05f);
	}
	
	// Update is called once per frame
	void Update () {
		//		TargetAndFire();
		if(HasGun && canShoot)
		{
			if(fireRateCounter >= fireRate)
			{
				FireBullet();
				fireRateCounter = 0;
			}
			else
			{
				fireRateCounter+=Time.deltaTime;
			}
		}
		
	}
	
	public void InitializeEnemyHealth()
	{
		
		health=0;
		if(TypeOfEnemy==EnemyType.AirBasic)
		{
			health=healthValuesMainGunForAirBasic[Tut_PandaPlane.healthLvl];
			health+=healthValuesWingGunLvlForAirBasic[Tut_PandaPlane.wingGunLvl];
			health+=healthValuesSideGunLvlForAirBasic[Tut_PandaPlane.sideGunLvl];
			health+=healthValuesBombLvlForAirBasic[Tut_PandaPlane.bombLvl];
			health+=healthValuesTeslaLvlForAirBasic[Tut_PandaPlane.teslaLvl];
			health+=healthValuesLaserLvlForAirBasic[Tut_PandaPlane.laserLvl];
			health+=healthValuesBladesLvlForAirBasic[Tut_PandaPlane.bladesLvl];
			
			
			if(AirBasicReference==AirBasic.AirBasic1StraighForward)
			{
				health+=10;
			}
			else if(AirBasicReference==AirBasic.AirBasic2UpperLeftLowerRight)
			{
				health+=10;
			}
			else if(AirBasicReference==AirBasic.AirBasic3UpperRightLowerLeft)
			{
				health+=15;
			}
			else if(AirBasicReference==AirBasic.AirBasic4Upper180RightToLeft)
			{
				health+=15;
			}
			else if(AirBasicReference==AirBasic.AirBasic5LeftMiddleRightMiddlePlusOffset)
			{
				health+=20;
			}
			else if(AirBasicReference==AirBasic.AirBasic6RightMiddleLeftMiddlePlusOffset)
			{
				health+=20;
			}
			else if(AirBasicReference==AirBasic.AirBasic7MiddleLeftToRightWithJump)
			{
				health+=25;
			}
			else if(AirBasicReference==AirBasic.AirBasic8LeftToRightMiddleDown)
			{
				health+=25;
			}
			else if(AirBasicReference==AirBasic.AirBasic9TwoEnemiesMiddleThenLeftRight)
			{
				health+=30;
			}
			else if(AirBasicReference==AirBasic.AirBasic10FromRightAndLeftToMiddle)
			{
				health+=30;
			}
			else if(AirBasicReference==AirBasic.AirBasic11UpperLeftMiddleRightTwoWaves)
			{
				health+=35;
			}
			else if(AirBasicReference==AirBasic.AirBasic12MiddleToRightTurnAndBackStraightLine)
			{
				health+=35;
			}
			else if(AirBasicReference==AirBasic.AirBasic13CikCak)
			{
				health+=40;
			}
			else if(AirBasicReference==AirBasic.AirBasic14TwoWavesStraightForwardAndeLeftToRight)
			{
				health+=40;
			}
			else if(AirBasicReference==AirBasic.AirBasic15TwoWavesStraightForwardLeftAndRight)
			{
				health+=45;
			}
			else if(AirBasicReference==AirBasic.AirBasic16Loop)
			{
				health+=45;
			}
		}
		else if(TypeOfEnemy==EnemyType.AirIntermediate)
		{
			health=healthValuesMainGunForAirIntermediate[Tut_PandaPlane.healthLvl];
			health+=healthValuesWingGunLvlForAirIntermediate[Tut_PandaPlane.wingGunLvl];
			health+=healthValuesSideGunLvlForAirIntermediate[Tut_PandaPlane.sideGunLvl];
			health+=healthValuesBombLvlForAirIntermediate[Tut_PandaPlane.bombLvl];
			health+=healthValuesTeslaLvlForAirIntermediate[Tut_PandaPlane.teslaLvl];
			health+=healthValuesLaserLvlForAirIntermediate[Tut_PandaPlane.laserLvl];
			health+=healthValuesBladesLvlForAirIntermediate[Tut_PandaPlane.bladesLvl];
			
			if(AirIntermediateReference==AirIntermediate.AirIntermediate17FireBalls)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate18FlyingMines)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate19AirLavirint)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate20GroundThunder)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate21DumbSentinels)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate22OneAndFire)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate23FourAndFire)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate24BottomLeftThenBottomRight)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate25AirplaneBomb)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate26CikCakWithPauseAndFire)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate27RocketFire)
			{
				
			}
			else if(AirIntermediateReference==AirIntermediate.AirIntermediate28UpperRightToMiddleWithFire)
			{
				
			}
			
			
		}
		else if(TypeOfEnemy==EnemyType.AirAdvanced)
		{
			health=healthValuesMainGunForAirAdvanced[Tut_PandaPlane.healthLvl];
			health+=healthValuesWingGunLvlForAirAdvanced[Tut_PandaPlane.wingGunLvl];
			health+=healthValuesSideGunLvlForAirAdvanced[Tut_PandaPlane.sideGunLvl];
			health+=healthValuesBombLvlForAirAdvanced[Tut_PandaPlane.bombLvl];
			health+=healthValuesTeslaLvlForAirAdvanced[Tut_PandaPlane.teslaLvl];
			health+=healthValuesLaserLvlForAirAdvanced[Tut_PandaPlane.laserLvl];
			health+=healthValuesBladesLvlForAirAdvanced[Tut_PandaPlane.bladesLvl];
			
			if(AirAdvancedReference==AirAdvanced.AirAdvanced29TeslaPlane)
			{
				
			}
			else if(AirAdvancedReference==AirAdvanced.AirAdvanced30OneToMany)
			{
				
			}
			else if(AirAdvancedReference==AirAdvanced.AirAdvanced31WithDestructableObjectShield)
			{
				
			}
			else if(AirAdvancedReference==AirAdvanced.AirAdvanced32WithBlades)
			{
				
			}
			else if(AirAdvancedReference==AirAdvanced.AirAdvanced33FireWithRotation)
			{
				
			}
			else if(AirAdvancedReference==AirAdvanced.AirAdvanced34FireForwardAllDirections)
			{
				
			}
			else if(AirAdvancedReference==AirAdvanced.AirAdvanced35AirplaneWithLaser)
			{
				
			}
		}
		else if(TypeOfEnemy==EnemyType.GroundAndWaterStatic)
		{
			health=healthValuesMainGunForGroundAndWaterStatic[Tut_PandaPlane.healthLvl];
			health+=healthValuesWingGunLvlForGroundAndWaterStatic[Tut_PandaPlane.wingGunLvl];
			health+=healthValuesSideGunLvlForGroundAndWaterStatic[Tut_PandaPlane.sideGunLvl];
			health+=healthValuesBombLvlForGroundAndWaterStatic[Tut_PandaPlane.bombLvl];
			health+=healthValuesTeslaLvlForGroundAndWaterStatic[Tut_PandaPlane.teslaLvl];
			health+=healthValuesLaserLvlForGroundAndWaterStatic[Tut_PandaPlane.laserLvl];
			health+=healthValuesBladesLvlForGroundAndWaterStatic[Tut_PandaPlane.bladesLvl];
			
			
			if(GroundAndWaterStaticReference==GroundAndWaterStatic.TankStatic)
			{
				
			}
			else if(GroundAndWaterStaticReference==GroundAndWaterStatic.Turret1)
			{
				
			}
			else if(GroundAndWaterStaticReference==GroundAndWaterStatic.Turret2)
			{
				
			}
			
		}
		else if(TypeOfEnemy==EnemyType.GroundAndWaterMobile)
		{
			health=healthValuesMainGunForGroundAndWaterMobile[Tut_PandaPlane.healthLvl];
			health+=healthValuesSideGunLvlForGroundAndWaterMobile[Tut_PandaPlane.wingGunLvl];
			health+=healthValuesSideGunLvlForGroundAndWaterMobile[Tut_PandaPlane.sideGunLvl];
			health+=healthValuesBombLvlForGroundAndWaterMobile[Tut_PandaPlane.bombLvl];
			health+=healthValuesTeslaLvlForGroundAndWaterMobile[Tut_PandaPlane.teslaLvl];
			health+=healthValuesLaserLvlForGroundAndWaterMobile[Tut_PandaPlane.laserLvl];
			health+=healthValuesBladesLvlForGroundAndWaterMobile[Tut_PandaPlane.bladesLvl];
			
			if(GroundAndWaterMobileReference==GroundAndWaterMobile.TankMobile)
			{
				
			}
			else if(GroundAndWaterMobileReference==GroundAndWaterMobile.ShipMobile)
			{
				
			}
			
		}
		
		//		Debug.Log("Ukupan health neprijatelja je: "+health);
		//			mainGunLvl, wingGunLvl, sideGunLvl;
	}
	
	//	public void TakeDamage(int damage)
	//	{
	//		if(health-damage<0)
	//		{
	//			if(continuousDamage)
	//				continuousDamage = false;
	////			Object.Destroy(this.gameObject);
	////			this.gameObject.SetActive(false);
	////			transform.parent.GetComponent<Animation>().Play("DeathPlane2"); // za avione
	////			transform.parent.GetComponent<Animation>().Play("DeathPlane1");
	//			health=0;
	//			canShoot = false;
	////			gameObject.transform.parent.parent.gameObject.SetActive(false);
	//			transform.parent.GetComponent<Animation>().Play("Death");
	//			CoinsOnDeath();
	//
	//		}
	//
	//		else
	//		{
	//			StartCoroutine("Flash");
	//			health-=damage;
	//		}
	//	}
	
	public void TakeDamage(int damage)
	{
		if(health-damage<0)
		{
			if(name.Contains("Helicopter") && GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Stop();
			
			if(TypeOfEnemy == EnemyType.GroundAndWaterMobile)
			{
				if(GroundAndWaterMobileReference == GroundAndWaterMobile.ShipMobile)
				{
					SoundManager.Instance.Play_ShipExplode();
				}
				else if(GroundAndWaterMobileReference == GroundAndWaterMobile.TankMobile)
				{
					SoundManager.Instance.Play_TankExplode();
				}
			}
			else if(TypeOfEnemy == EnemyType.GroundAndWaterStatic)
			{
				if(GroundAndWaterStaticReference == GroundAndWaterStatic.TankStatic)
				{
					SoundManager.Instance.Play_TankExplode();
				}
				else if(GroundAndWaterStaticReference == GroundAndWaterStatic.Turret1 || GroundAndWaterStaticReference == GroundAndWaterStatic.Turret2)
				{
					SoundManager.Instance.Play_TurretExplode();
				}
			}
			else// if(TypeOfEnemy == EnemyType.AirBasic)
			{
				SoundManager.Instance.Play_EnemyPlaneExplode();
			}
			
			
			if(continuousDamage)
				continuousDamage = false;
			//   Object.Destroy(this.gameObject);
			//   this.gameObject.SetActive(false);
			//   transform.parent.GetComponent<Animation>().Play("DeathPlane2"); // za avione
			//   transform.parent.GetComponent<Animation>().Play("DeathPlane1");
			health=0;
			canShoot = false;
			//   gameObject.transform.parent.parent.gameObject.SetActive(false);
			transform.parent.GetComponent<Animation>().Play("Death");
			Tut_PandaPlane.Instance.AddScore(25*Tut_LevelGenerator.currentStage);
			CoinsOnDeath();
//			Tut_PandaPlane.enemiesKilledFromLastCollectable++;
//			if(Tut_PandaPlane.collectableNumber<=Tut_PandaPlane.enemiesKilledFromLastCollectable)
//			{
//				Tut_PandaPlane.enemiesKilledFromLastCollectable=0;
//				Tut_PandaPlane.collectableNumber = Random.Range(15,26);
//				CollectableOnDeath();
//			}
			
		}
		
		else
		{
			SoundManager.Instance.Play_EnemyHit();
			StartCoroutine("Flash");
			health-=damage;
		}
	}
	
	
	
	void FireBullet()
	{
		if(transform.position.y > mainCameraTransform.position.y - 22f)
		{
			if(bulletIndex == enemyBulletPool.childCount)
				bulletIndex = 0;
			
			Tut_EnemyBullet tempScript = enemyBulletPool.GetChild(bulletIndex).GetComponent<Tut_EnemyBullet>();
			tempScript.GetComponent<EnemyDamage>().damage = damage;
			if(tempScript.available)
			{
				tempScript.enemyPosition = transform.position;
				tempScript.initialized = true;
				SoundManager.Instance.Play_EnemyFire();
				//break;
			}
			bulletIndex++;
		}
	}
	
	void OnBecameVisible()
	{
		if(health > 0)
		{
			canShoot = true;
			if(name.Contains("Helicopter") && !GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
		}
	}
	
	void OnBecameInvisible()
	{
		if(gameObject != null && mainCameraTransform != null)
			if(transform.position.y < mainCameraTransform.position.y - 22)
				canShoot = false;
		
		if(name.Contains("Helicopter") && GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Stop();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(transform.position.y < mainCameraTransform.position.y + 22)
		{
			if(col.tag.Equals("PlayerBullet"))
			{
				//			gameObject.SetActive(false);
				col.gameObject.SetActive(false);
				TakeDamage(Tut_PandaPlane.mainGunDamage);//25
				//			TakeDamage(30);
				//			TakeDamage(5);
			}
			else if(col.gameObject.tag.Equals("WingBullet"))
			{
				col.gameObject.SetActive(false);
				TakeDamage(Tut_PandaPlane.wingGunDamage);
			}
			else if(col.gameObject.tag.Equals("SideBullet"))
			{
				col.gameObject.SetActive(false);
				TakeDamage(Tut_PandaPlane.sideGunDamage);
			}
			else if(col.tag.Equals("Shield"))
			{
				StartCoroutine(col.GetComponent<Bomb>().ShieldHit());
				TakeDamage(5000);
			}
			else if(col.tag.Equals("Bomb"))
			{
				TakeDamage(Tut_PandaPlane.bombDamage);
			}
			else if(col.tag.Equals("Laser"))
			{
				continuousDamage = true;
				StartCoroutine(DoContinuousDamage(Tut_PandaPlane.laserDamage));
			}
			else if(col.tag.Equals("Tesla"))
			{
				continuousDamage = true;
				StartCoroutine(DoContinuousDamage(Tut_PandaPlane.teslaDamage));
			}
			else if(col.tag.Equals("Blades"))
			{
				TakeDamage(Tut_PandaPlane.bladesDamage);
			}
		}
		//		else if(col.tag.Equals("PlayerRocket"))
		//		{
		//			Debug.Log("###Rockets### "+"BOOOOOOOM raketa pogodila cilj");
		////						gameObject.transform.parent.parent.gameObject.SetActive(false);
		////			col.gameObject.SetActive(false);
		//			RocketsTest.targetAquired=false;
		//			RocketsTest.target=null;
		//			gameObject.tag = "Untagged";
		//			col.gameObject.transform.parent.GetComponent<RocketsTest>().TargetDestroyed();
		//			col.gameObject.transform.parent.GetComponent<Animation>().Play("RocketExplosion");
		//			TakeDamage(300);
		//			//			TakeDamage(30);
		//			//			TakeDamage(5);
		//		}
	}
	
	IEnumerator DoContinuousDamage(int damage)
	{
		while(continuousDamage)
		{
			Debug.Log("KOJ TI DAMAGE: " + damage);
			TakeDamage(damage);
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if(continuousDamage)
			continuousDamage = false;
	}
	
	IEnumerator Flash()
	{
		transform.Find("EnemyHIT").GetComponent<SpriteRenderer>().enabled=true;
		yield return new WaitForSeconds(0.1f);
		transform.Find("EnemyHIT").GetComponent<SpriteRenderer>().enabled=false;
	}
	
	void CoinsOnDeath()
	{
		//		int number = Random.Range(1,20);
		//		if((number%2)==1)
		//		{
		//			Debug.Log("Neparan broj");
		//		}
		//		else
		//		{
		//			Debug.Log("paran broj daj mu coine");
		//		}
		
		int number = Random.Range(0,5);
		
		for(int i=0;i<number;i++)
		{
			float rotate = Random.Range(-179, 179);
			GameObject Star = (GameObject) Instantiate(Resources.Load("StarHolder"));
			Star.transform.rotation = Quaternion.Euler(0,0,rotate);
			Star.transform.position = new Vector3(transform.position.x+Random.Range(-2f,2f), transform.position.y+Random.Range(-2f,2f), -45);
			
		}
		
	}
	
	void CollectableOnDeath()
	{
		do
		{
			Tut_PandaPlane.generatedCollectable = Random.Range(0, Tut_PandaPlane.collectables.Count);
		}
		while(Tut_PandaPlane.generatedCollectable==Tut_PandaPlane.lastGeneratedCollectable);
		
		
		Tut_PandaPlane.lastGeneratedCollectable=Tut_PandaPlane.generatedCollectable;
		
		GameObject Collectable;
		
		switch(Tut_PandaPlane.collectables[Tut_PandaPlane.generatedCollectable])
		{
			//LEGEND: 1-health; 2-doubleStars; 3-magnet; 4-shield; 5-armor
		case 1:  //generisi health
			Collectable = (GameObject) Instantiate(Resources.Load("Collectables/PowerUp_Health"));
			Collectable.transform.position = new Vector3(transform.position.x,transform.position.y,-46);
			Collectable.transform.GetChild(0).GetComponent<Animation>().Play();
			Collectable.transform.GetChild(0).GetComponent<Animation>().PlayQueued("PowerUpIdle",QueueMode.CompleteOthers);
			break;
		case 2:  //generisi doubleStars
			Collectable = (GameObject) Instantiate(Resources.Load("Collectables/PowerUp_DoubleStars"));
			Collectable.transform.position = new Vector3(transform.position.x,transform.position.y,-46);
			Collectable.transform.GetChild(0).GetComponent<Animation>().Play();
			Collectable.transform.GetChild(0).GetComponent<Animation>().PlayQueued("PowerUpIdle",QueueMode.CompleteOthers);
			break;
		case 3:  //generisi magnet
			Collectable = (GameObject) Instantiate(Resources.Load("Collectables/PowerUp_Magnet"));
			Collectable.transform.position = new Vector3(transform.position.x,transform.position.y,-46);
			Collectable.transform.GetChild(0).GetComponent<Animation>().Play();
			Collectable.transform.GetChild(0).GetComponent<Animation>().PlayQueued("PowerUpIdle",QueueMode.CompleteOthers);
			break;
		case 4:  //generisi shield
			Collectable = (GameObject) Instantiate(Resources.Load("Collectables/PowerUp_Shield"));
			Collectable.transform.position = new Vector3(transform.position.x,transform.position.y,-46);
			Collectable.transform.GetChild(0).GetComponent<Animation>().Play();
			Collectable.transform.GetChild(0).GetComponent<Animation>().PlayQueued("PowerUpIdle",QueueMode.CompleteOthers);
			break;
		case 5:  //generisi armor
			Collectable = (GameObject) Instantiate(Resources.Load("Collectables/PowerUp_Armor"));
			Collectable.transform.position = new Vector3(transform.position.x,transform.position.y,-46);
			Collectable.transform.GetChild(0).GetComponent<Animation>().Play();
			Collectable.transform.GetChild(0).GetComponent<Animation>().PlayQueued("PowerUpIdle",QueueMode.CompleteOthers);
			break;
			
		}
		
	}
	
}
