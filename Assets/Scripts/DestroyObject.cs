using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	public float time = 3f;

	void Start () 
	{
		GameObject.Destroy(this.gameObject,time);
	}
	

}
