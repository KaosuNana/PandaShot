using UnityEngine;
using System.Collections;

public class Tut_EnemyBullet : MonoBehaviour {
	
	public bool available = true;
	public bool initialized = false;
	public int speed;
	public bool FireStraight=true;
	float startTime;
	Vector3 targetPosition, endPosition;
	[HideInInspector] public Vector3 enemyPosition;
	Vector3 directionVector;
	
	void Start () 
	{
	}
	
	void Update () 
	{
		
		if(available && initialized)
		{
			available = false;
			initialized = false;
			//transform.position = this.gameObject.transform.parent.gameObject.transform.position;
			transform.position = new Vector3(enemyPosition.x,enemyPosition.y,transform.position.z);
			if(!FireStraight)
			{
				targetPosition = Tut_PlaneManager.Instance.transform.position+new Vector3(0,0,0);
				//float angle = Mathf.Atan2((targetPosition.x-transform.position.x),(targetPosition.y-transform.position.y))*Mathf.Rad2Deg;
				//transform.rotation = Quaternion.Euler(0,0,90+angle);
				directionVector = targetPosition - transform.position;
				directionVector.Normalize();
				//targetPosition=targetPosition*2;
				startTime = Time.time;
				//transform.rotation = this.gameObject.transform.parent.parent.FindChild("Gun").rotation;
			}
			
		}
		//		if(!available && (transform.position.y < PlaneManager.Instance.boundDown || transform.position.y > PlaneManager.Instance.boundUp || transform.position.x > PlaneManager.Instance.boundRight || transform.position.x < PlaneManager.Instance.boundLeft))
		//		{
		//			available = true;
		//			
		//			transform.localPosition = new Vector3(0,0,0);
		//			transform.gameObject.SetActive(true);
		//			transform.gameObject.SetActive(true);
		//		}
		else if(!available)
		{
			if(FireStraight)
			{
				transform.Translate(Vector3.down*Time.deltaTime*speed); //puca pravo
			}
			else
			{
				//transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) * speed/10); // puca na trenutnu poziciju PandaPlane-a
				transform.Translate(directionVector*Time.deltaTime*speed);
			}
			
		}
		
		
		
	}
	
	void ResetBullet()
	{
		if(!available)
		{
			available = true;
			
			transform.localPosition = new Vector3(0,0,0);
			//gameObject.SetActive(true);
		}
	}
	
	void OnBecameInvisible()
	{
		ResetBullet();
	}
}
