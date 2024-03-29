using UnityEngine;
using System.Collections;

public class MiddleTurretTank : MonoBehaviour {

	public Transform turretHit;
	public Transform wingHit;
	public Transform normalWing;
	public Transform damagedTurret;
	public Transform damagedWing;
	public Transform turretGun;
	
	public BossTank bossTank;
	public GameObject wholeParent;
	public GameObject explosion;
	
	public bool leftWing;
	
	//public Transform EngineLaserGunLargeNormal;
	//public Transform EngineLaserGunLargeDamaged;
	//public Transform EngineLaserGunSmallNormal;
	//public Transform EngineLaserGunSmallDamaged;
	
	bool canShoot = false;
	public Transform bulletPool;
	public Animation turretArrival;
	
	int health = 10;
	float fireRate = 0.4f;
	float fireRateCounter = 0;
	int bulletIndex = 0;
	int burstCount = 0;
	int burstLimit = 5;
	bool alreadyInvokedReset = false;
	float burstDelay = 3.5f;
	public bool notIncludeInWing = false;
	bool continuousDamage = false;
	int damage;
	
	void Start () 
	{
		//		if(wholeParent.name.Contains("1"))
		//			health = bossShip.turretMiddleHealth;
		//		else
		//			health = bossShip.turretMiddleHealth;
		
		if(bossTank.bossLevel == 1)
		{
			health = bossTank.turretMiddleHealth;
			fireRate = 0.4f;
			SmallWingTurretBullet.speed = 5;
			burstLimit = 5;
			burstDelay = 3.5f;
		}
		else if(bossTank.bossLevel == 2)
		{
			health = bossTank.turretMiddleHealth;
			fireRate = 0.2f;
			SmallWingTurretBullet.speed = 10;
			burstLimit = 5;
			burstDelay = 2.5f;
		}
		else if(bossTank.bossLevel == 3)
		{
			health = bossTank.turretMiddleHealth;
			fireRate = 0.2f;
			SmallWingTurretBullet.speed = 10;
			burstLimit = 10;
			burstDelay = 2.5f;
		}
		//bulletPool.parent = bossPlane.transform;
		damage = 30 + (LevelGenerator.currentStage-1)*10;
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
	
	void TakeDamage(int damage)
	{
		if(damage >= health)
		{
			bossTank.health-=health;
			health-=damage;
			
		}
		else
		{
			health-=damage;
			bossTank.health-=damage;
		}
		bossTank.SetHealthBar();
		
		if(health<=0)
		{
			SoundManager.Instance.Play_BossTurretExplosion();
			BossStars.Instance.spawnPosition = transform.position;
			BossStars.Instance.GenerateCoins(10,21);
			continuousDamage = false;
			//disable all hits just in case
			turretHit.GetComponent<Renderer>().enabled = false;
			wingHit.GetComponent<Renderer>().enabled = false;
			
			CancelInvoke("Fire");
			GetComponent<Collider2D>().enabled = false;
			//col.gameObject.SetActive(false);
			transform.parent.gameObject.SetActive(false);
			damagedTurret.GetComponent<Renderer>().enabled = true;
			explosion.SetActive(true);
			
			
			if(leftWing)
				bossTank.leftWingCount--;
			else
				bossTank.rightWingCount--;
			
			
			if(leftWing)
			{
				if(bossTank.leftWingCount == 0 && normalWing.gameObject.activeSelf)
				{
					normalWing.gameObject.SetActive(false);
					damagedWing.gameObject.SetActive(true);
					
					//if(EngineLaserGunLargeNormal.gameObject.activeSelf)
					//{
					//	EngineLaserGunLargeNormal.gameObject.SetActive(false);
					//	EngineLaserGunLargeDamaged.gameObject.SetActive(true);
					//}
					//if(EngineLaserGunSmallNormal.gameObject.activeSelf)
					//{
					//	EngineLaserGunSmallNormal.gameObject.SetActive(false);
					//	EngineLaserGunSmallDamaged.gameObject.SetActive(true);
					//}
				}
			}
			else
			{
				if(bossTank.rightWingCount == 0 && normalWing.gameObject.activeSelf)
				{
					normalWing.gameObject.SetActive(false);
					damagedWing.gameObject.SetActive(true);
					
					//if(EngineLaserGunLargeNormal.gameObject.activeSelf)
					//{
					//	EngineLaserGunLargeNormal.gameObject.SetActive(false);
					//	EngineLaserGunLargeDamaged.gameObject.SetActive(true);
					//}
					//if(EngineLaserGunSmallNormal.gameObject.activeSelf)
					//{
					//	EngineLaserGunSmallNormal.gameObject.SetActive(false);
					//	EngineLaserGunSmallDamaged.gameObject.SetActive(true);
					//}
				}
			}
			
			bossTank.currentWave--;
			if(bossTank.currentWave == 0)
			{
				bossTank.EnableThirdWave();
			}
		}
		else
		{
			SoundManager.Instance.Play_BossTurretHit();
			//health-=damage;
			StartCoroutine("Hit");
		}
	}
	
	//	void OnTriggerEnter2D(Collider2D col)
	//	{
	//		if(col.gameObject.tag.Equals("PlayerBullet"))
	//		{
	//			if(health<=0)
	//			{
	//				//disable all hits just in case
	//				turretHit.renderer.enabled = false;
	//				wingHit.renderer.enabled = false;
	//				
	//				CancelInvoke("Fire");
	//				collider2D.enabled = false;
	//				col.gameObject.SetActive(false);
	//				transform.parent.gameObject.SetActive(false);
	//				damagedTurret.renderer.enabled = true;
	//				explosion.SetActive(true);
	//				
	//				
	//				if(leftWing)
	//					bossShip.leftWingCount--;
	//				else
	//					bossShip.rightWingCount--;
	//				
	//				
	//				if(leftWing)
	//				{
	//					if(bossShip.leftWingCount == 0 && normalWing.gameObject.activeSelf)
	//					{
	//						normalWing.gameObject.SetActive(false);
	//						damagedWing.gameObject.SetActive(true);
	//						
	//						//if(EngineLaserGunLargeNormal.gameObject.activeSelf)
	//						//{
	//						//	EngineLaserGunLargeNormal.gameObject.SetActive(false);
	//						//	EngineLaserGunLargeDamaged.gameObject.SetActive(true);
	//						//}
	//						//if(EngineLaserGunSmallNormal.gameObject.activeSelf)
	//						//{
	//						//	EngineLaserGunSmallNormal.gameObject.SetActive(false);
	//						//	EngineLaserGunSmallDamaged.gameObject.SetActive(true);
	//						//}
	//					}
	//				}
	//				else
	//				{
	//					if(bossShip.rightWingCount == 0 && normalWing.gameObject.activeSelf)
	//					{
	//						normalWing.gameObject.SetActive(false);
	//						damagedWing.gameObject.SetActive(true);
	//						
	//						//if(EngineLaserGunLargeNormal.gameObject.activeSelf)
	//						//{
	//						//	EngineLaserGunLargeNormal.gameObject.SetActive(false);
	//						//	EngineLaserGunLargeDamaged.gameObject.SetActive(true);
	//						//}
	//						//if(EngineLaserGunSmallNormal.gameObject.activeSelf)
	//						//{
	//						//	EngineLaserGunSmallNormal.gameObject.SetActive(false);
	//						//	EngineLaserGunSmallDamaged.gameObject.SetActive(true);
	//						//}
	//					}
	//				}
	//				
	//				bossShip.currentWave--;
	//				if(bossShip.currentWave == 0)
	//				{
	//					bossShip.EnableThirdWave();
	//				}
	//			}
	//			else
	//			{
	//				health--;
	//				//StopCoroutine("Hit");
	//				StartCoroutine("Hit");
	//			}
	//		}
	//	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.tag.Equals("PlayerBullet"))
		{
			//health--;
			col.gameObject.SetActive(false);
			TakeDamage(PandaPlane.mainGunDamage);
		}
		else if(col.tag.Equals("WingBullet"))
		{
			col.gameObject.SetActive(false);
			TakeDamage(PandaPlane.wingGunDamage);
		}
		else if(col.tag.Equals("SideBullet"))
		{
			col.gameObject.SetActive(false);
			TakeDamage(PandaPlane.sideGunDamage);
		}
		else if(col.tag.Equals("Laser"))
		{
			//health-=20;
			continuousDamage = true;
			StartCoroutine(DoContinuousDamage(PandaPlane.laserDamage, col.gameObject));
		}
		else if(col.tag.Equals("Tesla"))
		{
			//health-=10;
			continuousDamage = true;
			StartCoroutine(DoContinuousDamage(PandaPlane.teslaDamage, col.gameObject));
		}
		else if(col.tag.Equals("Blades"))
		{
			//health-=5;
			TakeDamage(PandaPlane.bladesDamage);
		}
		else if(col.tag.Equals("Bomb"))
		{
			//health-=50;
			TakeDamage(PandaPlane.bombDamage);
		}
		
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if(continuousDamage)
			continuousDamage = false;
	}
	
	IEnumerator Hit()
	{
		turretHit.GetComponent<Renderer>().enabled = true;
		wingHit.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(0.1f);
		turretHit.GetComponent<Renderer>().enabled = false;
		wingHit.GetComponent<Renderer>().enabled = false;
	}
	
	void HideTurret()
	{
		wholeParent.SetActive(false);
	}
	
	void ShowTurret()
	{
		wholeParent.SetActive(true);
	}
	
	void FireAway()
	{
		turretArrival.Play();
		InvokeRepeating("Fire",2,fireRate);
	}
	
	void Update()
	{
		TargetAndFire();
	}
	
	void Fire()
	{
		//if(fireRateCounter >= fireRate)
		{
			fireRateCounter = 0;
			if(burstCount < burstLimit)
			{
				FireBullet();
				burstCount++;
			}
			else
			{
				if(!alreadyInvokedReset)
				{
					Invoke("ResetBurstCount",burstDelay);
					alreadyInvokedReset = true;
				}
			}
		}
		fireRateCounter+=Time.deltaTime;
		//TargetAndFire();
	}
	
	void TargetAndFire()
	{
		float speedOfRotation = Time.deltaTime*200;
		Vector3 targetPos = PlaneManager.Instance.transform.position;
		Vector3 diffPos = targetPos-turretGun.position;
		float angle = -Mathf.Atan2(diffPos.y, diffPos.x) * Mathf.Rad2Deg;
		angle -=90;
		Quaternion rot = Quaternion.AngleAxis(angle,  Vector3.back);
		turretGun.rotation = Quaternion.RotateTowards(turretGun.rotation, rot,speedOfRotation*100);
	}
	
	void ResetBurstCount()
	{
		if(burstCount >= burstLimit)
		{
			burstCount = 0;
			alreadyInvokedReset = false;
		}
	}
	
	void FireBullet()
	{
		if(bulletIndex == bulletPool.childCount)
			bulletIndex = 0;
		
		GameObject bullllet = Instantiate(bulletPool.GetChild(bulletIndex).gameObject) as GameObject;
		
		SmallWingTurretBullet tempScript = bullllet.GetComponent<SmallWingTurretBullet>();
		bullllet.GetComponent<EnemyDamage>().damage = damage;
		
		if(tempScript.available)
		{
			//tempScript.initialized = true;
			tempScript.damage = damage;
			tempScript.available = false;
			tempScript.transform.position = turretGun.position;
			tempScript.direction = turretGun.position - PlaneManager.Instance.transform.position;
			tempScript.FireBullet();
			//break;
		}
		//bulletIndex++;
	}
}