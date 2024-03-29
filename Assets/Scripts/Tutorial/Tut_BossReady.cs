using UnityEngine;
using System.Collections;

public class Tut_BossReady : MonoBehaviour {

	void Ready()
	{
		transform.parent.SendMessage("FireAway",SendMessageOptions.DontRequireReceiver);
		SoundManager.Instance.Play_BossMusic();
	}

	void TemporarilyStop()
	{
		GetComponent<Animation>()["Tut_BossPlaneArrival"].speed = 0;
	}
	
	void BossPlaneArrival()
	{
		if(transform.parent.name.Contains("Plane"))
			SoundManager.Instance.Play_BossPlaneArrival();
	}
	
	void BossPlaneMovement()
	{
		if(transform.parent.name.Contains("Plane"))
			SoundManager.Instance.Play_BossPlaneMovement();
	}
	
	void StopPlaneMovementSound()
	{
		if(transform.parent.name.Contains("Plane"))
			SoundManager.Instance.Stop_BossPlaneMovement();
	}

	void BombExplode()
	{
		SoundManager.Instance.Play_LaunchBomb();
		transform.Find("BombCloud").gameObject.SetActive(true);
		transform.Find("Weapon").gameObject.SetActive(false);
		Camera.main.GetComponent<Animation>().Play();
	}

	void LaunchingBomb()
	{
		SoundManager.Instance.Play_MeteorMovement();
	}
}
