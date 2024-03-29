using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static int musicOn = 1;
	public static int soundOn = 1;
	public static bool forceTurnOff = false;
	float initialGameplayMusicVolume;
	float initialBossExplosionVolume;

	public AudioSource buttonClick;
	public AudioSource upgradePlane;
	public AudioSource doorClosing;
	public AudioSource doorOpening;
	public AudioSource cloudsPassing;
	public AudioSource fireBullet;
	public AudioSource launchBlades;
	public AudioSource launchBomb;
	public AudioSource collectStar;
	public AudioSource collectPowerUp;
	public AudioSource noMoreWeapons;
	public AudioSource bossPlaneArrival;
	public AudioSource dialogPopupArrival;
	public AudioSource dialogTextTyping;
	public AudioSource shipExplode;
	public AudioSource tankExplode;
	public AudioSource dialogTextTypingBoss;
	public AudioSource bossPlaneMovement;
	public AudioSource enemyPlaneExplode;
	public AudioSource turretExplode;
	public AudioSource launchLaser;
	public AudioSource launchTesla;
	public AudioSource notEnoughStars;
	public AudioSource bossUniqueAttack;
	public AudioSource logoGlow;
	public AudioSource playerHit;
	public AudioSource enemyHit;
	public AudioSource bossTankMovement;
	public AudioSource meteorMovement;
	public AudioSource bossShipMovement;
	public AudioSource enemyFire;
	public AudioSource bossMainGunFire;
	public AudioSource missileLaunch;
	public AudioSource menuMusic;
	public AudioSource gameplayMusic;
	public AudioSource bossMusic;
	public AudioSource stageClear;
	public AudioSource gameOver;
	public AudioSource activateWeapon;
	public AudioSource bossTime;
	public AudioSource enemyLaser;
	public AudioSource planeResurrect;
	public AudioSource bossTurretHit;
	public AudioSource bossTurretExplosion;
	public AudioSource pandaPlaneExplode;
	public AudioSource bossExplosion;
	public AudioSource goblinLaugh;
	public AudioSource bombCountdown;
	public AudioSource bombLaunchMissiles;
	public AudioSource bombBroken;
	public AudioSource goblinTesla;
	public AudioSource goblinMaceSwinging;
	public AudioSource goblinMaceHit;
	public AudioSource helicopterMoving;
	public AudioSource BossUniqueBlades;


	static SoundManager instance;

	public static SoundManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;
			}

			return instance;
		}
	}

	void Start () 
	{
		DontDestroyOnLoad(this.gameObject);

		if(PlayerPrefs.HasKey("SoundOn"))
		{
			musicOn = PlayerPrefs.GetInt("MusicOn");
			soundOn = PlayerPrefs.GetInt("SoundOn");
		}

		initialGameplayMusicVolume = gameplayMusic.volume;
		initialBossExplosionVolume = bossExplosion.volume;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	public void Play_ButtonClick()
	{
		if(buttonClick.clip != null && soundOn == 1)
			buttonClick.Play();
	}

	public void Play_UpgradePlane()
	{
		if(upgradePlane.clip != null && soundOn == 1)
			upgradePlane.Play();
	}

	public void Play_DoorClosing()
	{
		if(doorClosing.clip != null && soundOn == 1)
			doorClosing.Play();
	}

	public void Play_DoorOpening()
	{
		if(doorOpening.clip != null && soundOn == 1)
			doorOpening.Play();
	}

	public void Play_CloudsPassing()
	{
		if(cloudsPassing.clip != null && soundOn == 1)
			cloudsPassing.Play();
	}

	public void Stop_CloudsPassing()
	{
		if(cloudsPassing.clip != null && soundOn == 1)
			cloudsPassing.Stop();
	}

	public void Play_FireBullet()
	{
		if(fireBullet.clip != null && soundOn == 1)
			fireBullet.Play();
	}

	public void Play_LaunchBlades()
	{
		if(launchBlades.clip != null && soundOn == 1)
			launchBlades.Play();
	}

	public void Play_LaunchBomb()
	{
		if(launchBomb.clip != null && soundOn == 1)
			launchBomb.Play();
	}

	public void Play_CollectStar()
	{
		if(collectStar.clip != null && soundOn == 1)
			collectStar.Play();
	}

	public void Play_CollectPowerUp()
	{
		if(collectPowerUp.clip != null && soundOn == 1)
			collectPowerUp.Play();
	}

	public void Play_NoMoreWeapons()
	{
		if(noMoreWeapons.clip != null && soundOn == 1)
			noMoreWeapons.Play();
	}

	public void Play_BossPlaneArrival()
	{
		if(bossPlaneArrival.clip != null && soundOn == 1)
			bossPlaneArrival.Play();
	}

	public void Play_DialogPopupArrival()
	{
		if(dialogPopupArrival.clip != null && soundOn == 1)
			dialogPopupArrival.Play();
	}

	public void Play_DialogTextTyping()
	{
		if(dialogTextTyping.clip != null && soundOn == 1)
			dialogTextTyping.Play();
	}

	public void Play_ShipExplode()
	{
		if(shipExplode.clip != null && soundOn == 1)
			shipExplode.Play();
	}

	public void Play_TankExplode()
	{
		if(tankExplode.clip != null && soundOn == 1)
			tankExplode.Play();
	}

	public void Play_DialogTextTypingBoss()
	{
		if(dialogTextTypingBoss.clip != null && soundOn == 1)
			dialogTextTypingBoss.Play();
	}

	public void Play_BossPlaneMovement()
	{
		if(bossPlaneMovement.clip != null && soundOn == 1)
			bossPlaneMovement.Play();
	}

	public void Stop_BossPlaneMovement()
	{
		if(bossPlaneMovement.clip != null && soundOn == 1)
			bossPlaneMovement.Stop();
	}

	public void Play_EnemyPlaneExplode()
	{
		if(enemyPlaneExplode.clip != null && soundOn == 1)
			enemyPlaneExplode.Play();
	}

	public void Play_TurretExplode()
	{
		if(turretExplode.clip != null && soundOn == 1)
			turretExplode.Play();
	}

	public void Play_LaunchLaser()
	{
		if(launchLaser.clip != null && soundOn == 1)
			launchLaser.Play();
	}

	public void Play_LaunchTesla()
	{
		if(launchTesla.clip != null && soundOn == 1)
			launchTesla.Play();
	}

	public void Play_NotEnoughStars()
	{
		if(notEnoughStars.clip != null && soundOn == 1)
			notEnoughStars.Play();
	}

	public void Play_BossUniqueAttack()
	{
		if(bossUniqueAttack.clip != null && soundOn == 1)
			bossUniqueAttack.Play();
	}

	public void Play_LogoGlow()
	{
		if(logoGlow.clip != null && soundOn == 1)
			logoGlow.Play();
	}

	public void Play_PlayerHit()
	{
		if(playerHit.clip != null && soundOn == 1)
			playerHit.Play();
	}

	public void Play_EnemyHit()
	{
		if(enemyHit.clip != null && soundOn == 1)
			enemyHit.Play();
	}

	public void Play_BossTankMovement()
	{
		if(bossTankMovement.clip != null && soundOn == 1)
			bossTankMovement.Play();
	}

	public void Stop_BossTankMovement()
	{
		if(bossTankMovement.clip != null && soundOn == 1)
			bossTankMovement.Stop();
	}

	public void Play_MeteorMovement()
	{
		if(meteorMovement.clip != null && soundOn == 1)
			meteorMovement.Play();
	}

	public void Play_BossShipMovement()
	{
		if(bossShipMovement.clip != null && soundOn == 1)
			bossShipMovement.Play();
	}

	public void Stop_BossShipMovement()
	{
		if(bossShipMovement.clip != null && soundOn == 1)
			bossShipMovement.Stop();
	}

	public void Play_EnemyFire()
	{
		if(enemyFire.clip != null && soundOn == 1)
			enemyFire.Play();
	}

	public void Play_BossMainGunFire()
	{
		if(bossMainGunFire.clip != null && soundOn == 1)
			bossMainGunFire.Play();
	}

	public void Play_MissileLaunch()
	{
		if(missileLaunch.clip != null && soundOn == 1)
			missileLaunch.Play();
	}

	public void Play_MenuMusic()
	{
		if(menuMusic.clip != null && musicOn == 1)
			menuMusic.Play();
	}

	public void Stop_MenuMusic()
	{
		if(menuMusic.clip != null && musicOn == 1)
			menuMusic.Stop();
	}

	public void Play_GameplayMusic()
	{
		if(gameplayMusic.clip != null && musicOn == 1)
		{
			gameplayMusic.volume = initialGameplayMusicVolume;
			gameplayMusic.Play();
		}
	}

	public void Stop_GameplayMusic()
	{
		if(gameplayMusic.clip != null && musicOn == 1)
		{
			StartCoroutine(FadeOut(gameplayMusic, 0.005f));
		}
	}

	IEnumerator FadeOut(AudioSource sound, float time)
	{
		float originalVolume = sound.volume;
		while(sound.volume != 0)
		{
			sound.volume = Mathf.MoveTowards(sound.volume, 0, time);
			yield return null;
		}
		sound.Stop();
		sound.volume = originalVolume;
	}

	public void Play_BossMusic()
	{
		if(bossMusic.clip != null && musicOn == 1)
			bossMusic.Play();
	}

	public void Stop_BossMusic()
	{
		if(bossMusic.clip != null && musicOn == 1)
			bossMusic.Stop();
	}

	public void Play_StageClear()
	{
		if(stageClear.clip != null && soundOn == 1)
			stageClear.Play();
	}

	public void Play_GameOver()
	{
		if(gameOver.clip != null && soundOn == 1)
			gameOver.Play();
	}

	public void Play_ActivateWeapon()
	{
		if(activateWeapon.clip != null && soundOn == 1)
			activateWeapon.Play();
	}

	public void Play_BossTime()
	{
		if(bossTime.clip != null && soundOn == 1)
			bossTime.Play();
	}

	public void Play_EnemyLaser()
	{
		if(enemyLaser.clip != null && soundOn == 1)
			enemyLaser.Play();
	}

	public void Play_PlaneResurrect()
	{
		if(planeResurrect.clip != null && soundOn == 1)
			planeResurrect.Play();
	}

	public void Play_BossTurretHit()
	{
		if(bossTurretHit.clip != null && soundOn == 1)
			bossTurretHit.Play();
	}

	public void Play_BossTurretExplosion()
	{
		if(bossTurretExplosion.clip != null && soundOn == 1)
			bossTurretExplosion.Play();
	}

	public void Play_PandaPlaneExplode()
	{
		if(pandaPlaneExplode.clip != null && soundOn == 1)
			pandaPlaneExplode.Play();
	}

	public void Play_BossExplosion()
	{
		if(bossExplosion.clip != null && soundOn == 1)
		{
			bossExplosion.volume = initialBossExplosionVolume;
			bossExplosion.Play();
		}
	}

	public void Stop_BossExplosion()
	{
		if(bossExplosion.clip != null && soundOn == 1)
			StartCoroutine(FadeOut(bossExplosion, 0.0035f));
	}

	public void Play_GoblinLaugh()
	{
		if(goblinLaugh.clip != null && soundOn == 1 && !goblinLaugh.isPlaying)
			goblinLaugh.Play();
	}

	public void Play_BombCountdown()
	{
		if(bombCountdown.clip != null && soundOn == 1 && !bombCountdown.isPlaying)
			bombCountdown.Play();
	}

	public void Play_BombLaunchMissiles()
	{
		if(bombLaunchMissiles.clip != null && soundOn == 1 && !bombLaunchMissiles.isPlaying)
			bombLaunchMissiles.Play();
	}

	public void Play_BombBroken()
	{
		if(bombBroken.clip != null && soundOn == 1 && !bombBroken.isPlaying)
			bombBroken.Play();
	}

	public void Stop_BombBroken()
	{
		if(bombBroken.clip != null && soundOn == 1)
			bombBroken.Stop();
	}

	public void Play_GoblinTesla()
	{
		if(goblinTesla.clip != null && soundOn == 1 && !goblinTesla.isPlaying)
			goblinTesla.Play();
	}

	public void Stop_GoblinTesla()
	{
		if(goblinTesla.clip != null && soundOn == 1)
			goblinTesla.Stop();
	}

	public void Play_GoblinMaceSwinging()
	{
		if(goblinMaceSwinging.clip != null && soundOn == 1 && !goblinMaceSwinging.isPlaying)
			goblinMaceSwinging.Play();
	}

	public void Stop_GoblinMaceSwinging()
	{
		if(goblinMaceSwinging.clip != null && soundOn == 1)
			goblinMaceSwinging.Stop();
	}

	public void Play_GoblinMaceHit()
	{
		if(goblinMaceHit.clip != null && soundOn == 1 && !goblinMaceHit.isPlaying)
		{
			goblinMaceHit.Play();
		}
	}

	public void Play_HelicopterMoving()
	{
		if(helicopterMoving.clip != null && soundOn == 1 && !helicopterMoving.isPlaying)
		{
			helicopterMoving.Play();
		}
	}

	public void Stop_HelicopterMoving()
	{
		if(helicopterMoving.clip != null && soundOn == 1)
		{
			helicopterMoving.Stop();
		}
	}

	public void Play_BossUniqueBlades()
	{
		if(BossUniqueBlades.clip != null && soundOn == 1 && !BossUniqueBlades.isPlaying)
		{
			BossUniqueBlades.Play();
		}
	}
	
}
