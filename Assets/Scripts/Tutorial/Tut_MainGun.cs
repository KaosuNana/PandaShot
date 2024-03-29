﻿using UnityEngine;
using System.Collections;

public class Tut_MainGun : MonoBehaviour {
	
	public Transform mainGunHit;
	public Transform bodyNormal;
	public Transform bodyDamaged;
	
	public Tut_BossPlane bossPlane;
	public GameObject wholeParent;
	//public GameObject explosion;
	
	float initialFire = 17;
	float fireInterval = 7;
	
	int health = 25;
	MainGunFireEvent fireEvent;
	bool alwaysVisible = false;
	bool fired = false;
	bool continuousDamage = false;
	[HideInInspector] public int regularDamage;
	[HideInInspector] public int uniqueDamage;
	
	void Start ()
	{
		fireEvent = transform.GetComponent<MainGunFireEvent>();
		
		if(bossPlane.bossLevel == 1)
		{
			fireEvent.numberOfAttacks = 1;
			fireEvent.speed = 0.75f;
		}
		else if(bossPlane.bossLevel == 2)
		{
			fireEvent.numberOfAttacks = 2;
			fireEvent.speed = 1f;
		}
		else
		{
			fireEvent.numberOfAttacks = 3;
			fireEvent.speed = 1.35f;
		}
		
		health = bossPlane.mainGunHealth;
		//InvokeRepeating("MainGunFire",initialFire,fireInterval);
		
		regularDamage = 50;
		uniqueDamage = 150;
		fireEvent.regularDamage = regularDamage;
		fireEvent.uniqueDamage = uniqueDamage;
	}

	void MainGunFireStart()
	{
		InvokeRepeating("MainGunFire",initialFire,fireInterval);
	}
	
	void MainGunFire()
	{
		//animation["MainGunArrivingAnimation"].normalizedTime = 0;
		//animation["MainGunArrivingAnimation"].speed = 1;
		fired = true;
		GetComponent<Animation>().Play();
		if(!alwaysVisible)
			Invoke("MainGunInverse", 2f);
	}
	
	IEnumerator DoContinuousDamage(int damage)
	{
		while(continuousDamage)
		{
			TakeDamage(damage);
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	void TakeDamage(int damage)
	{
		if(damage >= health)
		{
			bossPlane.health-=health;
			health-=damage;
			
		}
		else
		{
			health-=damage;
			bossPlane.health-=damage;
		}
		bossPlane.SetHealthBar();
		
		if(health<=0)
		{
			BossStars.Instance.spawnPosition = transform.position;
			BossStars.Instance.GenerateCoins(25,36);
			continuousDamage = false;
			//disable all hits just in case
			mainGunHit.GetComponent<Renderer>().enabled = false;
			
			CancelInvoke("FireUniqueWeapon");
			GetComponent<Collider2D>().enabled = false;
			//col.gameObject.SetActive(false);
			//transform.parent.gameObject.SetActive(false);
			
			//bodyNormal.renderer.enabled = false;
			//bodyDamaged.renderer.enabled = true;
			bodyNormal.gameObject.SetActive(false);
			bodyDamaged.gameObject.SetActive(true);
			fireEvent.TurnOffLasers();
			Tut_PandaPlane.Instance.invincible = true;
			
			Invoke("DestroyBoss",0.15f);
		}
		else
		{
			//health-=damage;
			SoundManager.Instance.Play_BossTurretHit();
			StartCoroutine("Hit");
		}
	}
	
	void MainGunInverse()
	{
		fireEvent.dontFire = true;
		GetComponent<Animation>()["MainGunArrivingAnimation"].normalizedTime = 1;
		GetComponent<Animation>()["MainGunArrivingAnimation"].speed = -1;
		GetComponent<Animation>().Play();
		Invoke("ResetAnimation",2f);
		fired = false;
	}
	
	void ResetAnimation()
	{
		fireEvent.dontFire = false;
		GetComponent<Animation>()["MainGunArrivingAnimation"].normalizedTime = 0;
		GetComponent<Animation>()["MainGunArrivingAnimation"].speed = 1;
		
	}
	
	//	void OnTriggerEnter2D(Collider2D col)
	//	{
	//		if(col.gameObject.tag.Equals("PlayerBullet"))
	//		{
	//			if(health<=0)
	//			{
	//				//disable all hits just in case
	//				mainGunHit.renderer.enabled = false;
	//
	//				CancelInvoke("FireUniqueWeapon");
	//				collider2D.enabled = false;
	//				col.gameObject.SetActive(false);
	//				//transform.parent.gameObject.SetActive(false);
	//				
	//				//bodyNormal.renderer.enabled = false;
	//				//bodyDamaged.renderer.enabled = true;
	//				bodyNormal.gameObject.SetActive(false);
	//				bodyDamaged.gameObject.SetActive(true);
	//				fireEvent.TurnOffLasers();
	//
	//				Invoke("DestroyBoss",0.15f);
	//			}
	//			else
	//			{
	//				health--;
	//				col.gameObject.SetActive(false);
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
			TakeDamage(Tut_PandaPlane.mainGunDamage);
		}
		else if(col.tag.Equals("WingBullet"))
		{
			col.gameObject.SetActive(false);
			TakeDamage(Tut_PandaPlane.wingGunDamage);
		}
		else if(col.tag.Equals("SideBullet"))
		{
			col.gameObject.SetActive(false);
			TakeDamage(Tut_PandaPlane.sideGunDamage);
		}
		else if(col.tag.Equals("Laser"))
		{
			//health-=20;
			continuousDamage = true;
			StartCoroutine(DoContinuousDamage(Tut_PandaPlane.laserDamage));
		}
		else if(col.tag.Equals("Tesla"))
		{
			//health-=10;
			continuousDamage = true;
			StartCoroutine(DoContinuousDamage(Tut_PandaPlane.teslaDamage));
		}
		else if(col.tag.Equals("Blades"))
		{
			//health-=5;
			TakeDamage(Tut_PandaPlane.bladesDamage);
		}
		else if(col.tag.Equals("Bomb"))
		{
			//health-=50;
			TakeDamage(Tut_PandaPlane.bombDamage);
		}
		
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if(continuousDamage)
			continuousDamage = false;
	}
	
	void DestroyBoss()
	{
		bossPlane.DestroyBoss();
		//explosion.SetActive(true);
	}
	
	IEnumerator Hit()
	{
		mainGunHit.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(0.1f);
		mainGunHit.GetComponent<Renderer>().enabled = false;
	}
	
	void HideTurret()
	{
		if(wholeParent.activeSelf)
			wholeParent.SetActive(false);
	}
	
	void ShowTurret()
	{
		if(!wholeParent.activeSelf)
			wholeParent.SetActive(true);
	}
	
	void FireAway()
	{
		//if(!animation.isPlaying)
		if(!fired)
		{
			GetComponent<Animation>()["MainGunArrivingAnimation"].normalizedTime = 0;
			GetComponent<Animation>()["MainGunArrivingAnimation"].speed = 1;
			GetComponent<Animation>().Play();
		}
		
		fireEvent.dontFire = true;
		CancelInvoke("MainGunFire");
		CancelInvoke("MainGunInverse");
		alwaysVisible = true;
		if(bossPlane.bossLevel == 1)
		{
			initialFire = 0.5f;
			fireInterval = 2f;
		}
		else if(bossPlane.bossLevel == 2)
		{
			fireEvent.numberOfAttacks = 1;
			initialFire = 0.5f;
			fireInterval = 7f;
		}
		else if(bossPlane.bossLevel == 3)
		{
			fireEvent.numberOfAttacks = 1;
			initialFire = 0.5f;
			fireInterval = 7f;
		}
		InvokeRepeating("FireUniqueWeapon", initialFire, fireInterval);
	}
	
	void FireUniqueWeapon()
	{
		fireEvent.bossLevel = bossPlane.bossLevel;
		fireEvent.UniqueFire();
	}
}