using UnityEngine;
using System.Collections;

public class Tut_PlaneManager : MonoBehaviour {
	
	public bool isShooting = false;
	public static int controlType = 1;
	float startX;
	float endX;
	float startY;
	float endY;
	float offsetX;
	float offsetY;
	//Animator animator;
	[HideInInspector] public bool gameActive = true;
	string clickedItem;
	string releasedItem;
	float previousEndX;
	float minus5Frames = 0;
	[HideInInspector] public float boundLeft;
	[HideInInspector] public float boundRight;
	[HideInInspector] public float boundUp;
	[HideInInspector] public float boundDown;
	float firstQuarter;
	float thirdQuarter;
	float cameraMin;
	float cameraMax;
	Transform planeChild;
	float fireRate = 0.425f; //higher value means slower fire rate // 25, pa 15 -- //0.35f
	float fireRateCounter = 0;
	float wingFireRate = 0.425f; //higher value means slower fire rate // 25, pa 15 -- //0.35f
	float wingFireRateCounter = 0;
	float sideFireRate = 0.425f; //higher value means slower fire rate // 25, pa 15 -- //0.35f
	float sideFireRateCounter = 0;
	[HideInInspector] public Transform firePosition;
	[HideInInspector] public Transform wingFirePosition;
	[HideInInspector] public Transform sideFirePosition;
	bool cooldown = false;
	bool normalTime = true;
	[HideInInspector] public bool notControlling = false;
	
	public Transform bulletPool;
	public Transform wingBulletPool;
	public Transform sideBulletPool;
	float elapsedTime = 0;
	bool timerEnabled = false;
	TextMesh timer;
	Transform bounds;
	int bulletIndex = 0;
	int wingBulletIndex = 0;
	int sideBulletIndex = 0;
	[HideInInspector] public Camera guiCamera;
	[HideInInspector] public int dontSlowTime = 0; // 0 - can slow time, 1 - cannot slow time, 2 - cannot slow time after boss
	GameObject activeWeapon;
	public GameObject weaponLaser;
	public GameObject weaponTesla;
	public GameObject weaponBlades;
	public GameObject weaponBomb;
	float planeBoundsLeft;
	float planeBoundsRight;
	Transform cameraParent;
	[HideInInspector] public bool menuIsActive = false;
	public static bool pressedAndHold = false;
	Transform activeMainGuns;
	Transform activeWingGuns;
	Transform activeSideGuns;
	[HideInInspector] public bool bossTimePart = false;
	//bool speedUpTime = false;
	
	static Tut_PlaneManager instance;
	public static Tut_PlaneManager Instance
	{
		get
		{
			if(instance == null)
				instance = GameObject.FindObjectOfType(typeof(Tut_PlaneManager)) as Tut_PlaneManager;
			
			return instance;
		}
	}
	
	void Awake ()
	{
		planeChild = transform.GetChild(0).Find("PandaPlane");
	}
	
	// Use this for initialization
	void Start () 
	{
		//DetermineGunsLevel();
		//SoundManager.Instance.Play_DoorOpening();
		
		cameraParent = Camera.main.transform.parent;
		guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		bounds = GameObject.Find("Bounds").transform;
		//timer = Camera.main.transform.Find("Timer").GetComponent<TextMesh>();
		boundLeft = bounds.Find("BoundUpLeft").position.x;
		boundUp = bounds.Find("BoundUpLeft").position.y;
		boundRight = bounds.Find("BoundDownRight").position.x;
		boundDown = bounds.Find("BoundDownRight").position.y;
		//animator = transform.Find("Plane").GetComponent<Animator>();
		firstQuarter = Camera.main.ViewportToWorldPoint(Vector3.one*0.25f).x;
		thirdQuarter = Camera.main.ViewportToWorldPoint(Vector3.one*0.75f).x;
		cameraMin = boundLeft + Camera.main.orthographicSize*Camera.main.aspect;//-2
		cameraMax = boundRight - Camera.main.orthographicSize*Camera.main.aspect;//+2
		planeBoundsLeft = boundLeft + Camera.main.aspect*4;
		planeBoundsRight = boundRight - Camera.main.aspect*4;
		//cameraMin = boundLeft + Camera.main.orthographicSize*0.625f;
		//cameraMax = boundRight - Camera.main.orthographicSize*0.625f;
		fireRateCounter = fireRate;
		//firePosition = planeChild.Find("FirePosition").transform;
		firePosition = transform.GetChild(0).Find("FirePosition").transform;
		wingFirePosition = transform.GetChild(0).Find("WingFirePosition").transform;
		sideFirePosition = transform.GetChild(0).Find("SideFirePosition").transform;
		gameActive = false;
		dontSlowTime = 1;
		
		if(PlayerPrefs.HasKey("ControlType"))
			controlType = PlayerPrefs.GetInt("ControlType");
		
		//SoundManager.Instance.Play_CloudsPassing();
		
	}
	
	void CanSlowTime()
	{
		if(dontSlowTime == 1)
			dontSlowTime = 0;
	}
	
	public void DetermineGunsAndArmorLevel()
	{
		bulletPool = (Instantiate(Resources.Load("TutorialBullets/TutorialBulletPool10")) as GameObject).transform;
		activeMainGuns = transform.GetChild(0).Find("GunsHolder/MainGuns/Level4");
		activeMainGuns.gameObject.SetActive(true);

		wingBulletPool = (Instantiate(Resources.Load("TutorialBullets/TutorialWingBulletPool10")) as GameObject).transform;
		activeWingGuns = transform.GetChild(0).Find("GunsHolder/WingGuns/Level3");
		activeWingGuns.gameObject.SetActive(true);
		
		sideBulletPool = (Instantiate(Resources.Load("TutorialBullets/TutorialSideBulletPool10")) as GameObject).transform;
		activeSideGuns = transform.GetChild(0).Find("GunsHolder/SideGuns");
		activeSideGuns.gameObject.SetActive(true);
		
		fireRate -= 10*0.025f;
		wingFireRate -= 10*0.025f;
		
		planeChild.GetComponent<SpriteRenderer>().sprite = GameObject.Find("PlaneReferences/PandaPlaneArmor3Gameplay").GetComponent<SpriteRenderer>().sprite;
		planeChild.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameObject.Find("PlaneReferences/PandaPlaneArmor3HIT").GetComponent<SpriteRenderer>().sprite;

	}
	
	IEnumerator TurnOffWeapon(float time)
	{
		yield return new WaitForSeconds(time);
		if(activeWeapon.name.Contains("Laser"))
		{
			activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].normalizedTime = 1;
			activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].speed = -1;
			activeWeapon.transform.GetChild(0).GetComponent<Animation>().Play();
			yield return new WaitForSeconds(0.6f);
			activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].normalizedTime = 0;
			activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].speed = 1;
		}
		//		else if(activeWeapon.name.Contains("Tesla"))
		//		{
		//			//activeWeapon.collider2D.enabled = false;
		//			activeWeapon.layer = LayerMask.NameToLayer("Default");
		//			activeWeapon.transform.GetChild(0).gameObject.SetActive(false);
		//			activeWeapon.transform.GetChild(1).particleSystem.Play();
		//			yield return new WaitForSeconds(1f);
		//			activeWeapon.SetActive(false);
		//			//activeWeapon.collider2D.enabled = true;
		//			activeWeapon.layer = LayerMask.NameToLayer("PlayerBullet");
		//			activeWeapon.transform.GetChild(0).gameObject.SetActive(true);
		//		}
		isShooting = true;
		yield return new WaitForSeconds(0.25f);
		if(activeWeapon.activeSelf)
			activeWeapon.SetActive(false);
		activeWeapon = null;
		if(dontSlowTime == 1)
			dontSlowTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		boundLeft = bounds.Find("BoundUpLeft").position.x;
		boundUp = bounds.Find("BoundUpLeft").position.y;
		boundRight = bounds.Find("BoundDownRight").position.x;
		boundDown = bounds.Find("BoundDownRight").position.y;
		//		firstQuarter = Camera.main.ViewportToWorldPoint(Vector3.one*0.25f).x;
		//		thirdQuarter = Camera.main.ViewportToWorldPoint(Vector3.one*0.75f).x;
		//		cameraMin = boundLeft + Camera.main.orthographicSize*Camera.main.aspect-3;
		//		cameraMax = boundRight - Camera.main.orthographicSize*Camera.main.aspect+3;
		
		
//		if(Input.GetKeyUp(KeyCode.Escape))
//		{
//			//Tut_Interface.Instance.PauseGame();
//			if(Tut_PopUpHandler.popupType == 0) // regular game
//			{
//				Time.timeScale = 0;
//				Tut_PopUpHandler.Instance.ShowPauseScreen();
//			}
//			else if(Tut_PopUpHandler.popupType == 1) // slow time screen
//			{
//				Tut_PopUpHandler.Instance.ShowPauseScreen();
//			}
//			else if(Tut_PopUpHandler.popupType == 2) // paused game
//			{
//				Tut_PopUpHandler.Instance.ResumeGameFromPause();
//			}
//			else if(Tut_PopUpHandler.popupType == 3) // keep playing screen
//			{
//				Tut_PopUpHandler.Instance.ShowGameOverScreen();
//			}
//			else if(Tut_PopUpHandler.popupType == 4) // game over
//			{
//				Tut_PopUpHandler.Instance.LoadMainScene();
//			}
//		}
		if(Input.GetMouseButtonDown(0))
		{
			if(bossTimePart)
			{
				//speedUpTime = true;
				gameActive = false;
				isShooting = false;
				pressedAndHold = true;
				Time.timeScale = 5f;
				Tut_GameManager.Instance.speed /= 5f;
				transform.GetChild(0).Find("Elipse").GetComponent<Animation>()["PandaPlaneElipseRotationAnimation"].speed = 1/5f;
			}
			else
			{
				clickedItem = RaycastFunction(Input.mousePosition);
				if(gameActive)
				{
					
					if(activeWeapon == null)
						isShooting = true;
					notControlling = false;
					if(!Tut_Interface.Instance.normalTime)
					{
						if(!menuIsActive)
						{
							Tut_Interface.Instance.normalTime = true;
						}
						
						//StopCoroutine(Tut_Interface.Instance.SlowTime());
						//if(!menuIsActive)
						//	StartCoroutine(Tut_Interface.Instance.NormalTime());
						//Tut_PopUpHandler.Instance.ResumeGame();
						
						pressedAndHold = true;
						
						if(!normalTime && !menuIsActive)
						{
							//StartCoroutine("NormalTime",0.075f);
							StartCoroutine("NormalTime",0.02f);
						}
					}
					
					if(controlType == 1)
					{
						startX = endX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
						startY = endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
					}
					else
					{
						startX = endX = Input.mousePosition.x;
						startY = endY = Input.mousePosition.y;
					}
					//FireBullet();
				}
				
				if(clickedItem.Equals("WeaponLaser"))
				{
					dontSlowTime = 1;
					isShooting = false;
					activeWeapon = weaponLaser;
					activeWeapon.SetActive(true);
					StartCoroutine(TurnOffWeapon(2f));
				}
				else if(clickedItem.Equals("WeaponTesla"))
				{
					dontSlowTime = 1;
					isShooting = false;
					activeWeapon = weaponTesla;
					activeWeapon.SetActive(true);
					StartCoroutine(TurnOffWeapon(4f));
				}
				else if(clickedItem.Equals("WeaponBlades"))
				{
					dontSlowTime = 1;
					activeWeapon = weaponBlades;
					activeWeapon.SetActive(true);
					activeWeapon.SendMessage("Activate");
					StartCoroutine(TurnOffWeapon(PandaPlane.bladesDuration+1.5f));
				}
				else if(clickedItem.Equals("WeaponBomb"))
				{
					dontSlowTime = 1;
					isShooting = false;
					activeWeapon = weaponBomb;
					weaponBomb.transform.position = transform.position;
					activeWeapon.SetActive(true);
					StartCoroutine(TurnOffWeapon(1f));
					Camera.main.GetComponent<Animation>().Play();
				}
				else if(clickedItem.Equals("StartCircleHolder"))
				{
					SoundManager.Instance.Stop_CloudsPassing();
					Tut_LevelGenerator.checkpoint = false;
					GameObject circle = GameObject.Find(clickedItem);
					circle.GetComponent<Collider>().enabled = false;
					circle.GetComponent<Animation>().Play("StartCircleDisappear_Animation");
					Tut_PopUpHandler.Instance.ResumeGame();
					GameObject.Find("Arrow").SetActive(false);
					Invoke("StartCircleDissapear",1.6f);
					StartPlaying();
					TutorialEvents.timeCountStarted = true;
					SoundManager.Instance.Play_GameplayMusic();
				}
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if(TutorialEvents.expectingRelease == 1)
			{
				Tut_PopUpHandler.Instance.ShowSlowTimeScreen();
				TutorialEvents.releasedFinger = true;
				GameObject.Find("Tutorial").SendMessage("ForceClickedYES",SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				if(bossTimePart)
				{
					Time.timeScale = 1;
				}
				else
				{
					releasedItem = RaycastFunction(Input.mousePosition);
					pressedAndHold = false;
					//if(!dontSlowTime && !notControlling)
					if(dontSlowTime == 0)
					{
						normalTime = false;
						StopCoroutine("NormalTime");
					}
					
					if(clickedItem.Equals(releasedItem))
					{
						if(releasedItem.Equals("ControlsType1"))
						{
							controlType = 1;
							Tut_Interface.Instance.transform.Find("MenuHolder/Menu/ControlsSelection").localPosition = new Vector3(-0.25f,0,-0.1f);
						}
						else if(releasedItem.Equals("ControlsType2"))
						{
							controlType = 2;
							Tut_Interface.Instance.transform.Find("MenuHolder/Menu/ControlsSelection").localPosition = new Vector3(0.25f,0,-0.1f);
						}
//						else if(releasedItem.Equals("StartCircleHolder"))
//						{
//							SoundManager.Instance.Stop_CloudsPassing();
//							Tut_LevelGenerator.checkpoint = false;
//							GameObject.Find(releasedItem).animation.Play("StartCircleDisappear_Animation");
//							Tut_PopUpHandler.Instance.ResumeGame();
//							Invoke("StartCircleDissapear",0.6f);
//							StartPlaying();
//							TutorialEvents.timeCountStarted = true;
//							SoundManager.Instance.Play_GameplayMusic();
//						}
						
						if(planeChild.eulerAngles.y != 0)
							planeChild.rotation = Quaternion.Lerp(planeChild.rotation,Quaternion.Euler(0,0,0),0.5f);
					}
					
					if(gameActive)
					{
						startX = endX = startY = endY = 0;
						previousEndX = endX = 0;
						if(isShooting && dontSlowTime == 0 && !menuIsActive)
						{
							Debug.Log("UULLULULLULULU 2");
							//isShooting = false;
							Tut_Interface.Instance.normalTime = false;
							StopCoroutine(Tut_Interface.Instance.NormalTime());
							//StartCoroutine(Tut_Interface.Instance.SlowTime());
							Time.timeScale = 0.1f;
							Tut_PopUpHandler.Instance.ShowSlowTimeScreen();
							//fireRateCounter = fireRate;
						}
					}
				}
			}
		}
		else if(Input.GetMouseButton(0) && gameActive && normalTime && TutorialEvents.expectingRelease != 2)
		{
			//zaIzbrisavanje = new Vector3(endX, endY, transform.position.z);
			//GameObject.Find("Buljavost").transform.position = new Vector3(GameObject.Find("Buljavost").transform.position.x + endX - startX, GameObject.Find("Buljavost").transform.position.y + endY - startY, transform.position.z);
			//GameObject.Find("Buljavost").transform.position = Vector3.MoveTowards(GameObject.Find("Buljavost").transform.position, new Vector3(GameObject.Find("Buljavost").transform.position.x + endX - startX, GameObject.Find("Buljavost").transform.position.y + endY - startY, transform.position.z), 0.97f);
			//GameObject.Find("Buljavost").transform.position = new Vector3(Mathf.MoveTowards(GameObject.Find("Buljavost").transform.position.x, GameObject.Find("Buljavost").transform.position.x + offsetX, 0.93f), Mathf.MoveTowards(GameObject.Find("Buljavost").transform.position.y, GameObject.Find("Buljavost").transform.position.y + offsetY, 0.97f), transform.position.z);
			
			//			if(isShooting)
			//			{
			//				if(fireRateCounter == fireRate)
			//				{
			//					FireBullet();
			//					fireRateCounter = 0;
			//				}
			//				else
			//				{
			//					fireRateCounter++;
			//				}
			//			}
			
			if(controlType == 1)
			{
				endX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
				endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
				
				offsetX = endX - startX;
				offsetY = endY - startY;
				
				//endX = Camera.main.ScreenToWorldPoint(new Vector3(endX,0,0)).x;
				transform.position = Vector3.Lerp(transform.position,new Vector3(Mathf.Clamp(endX,planeBoundsLeft,planeBoundsRight), Mathf.Clamp(endY+4.75f,boundDown,boundUp), transform.position.z), Time.deltaTime*8);//0.175 -- Time.deltaTime*12
				previousEndX = transform.position.x;
				//	Debug.Log("endX: " + endX);
				//animator.SetFloat("Speed",(endX-previousEndX));
				planeChild.rotation = Quaternion.Lerp(planeChild.rotation,Quaternion.Euler(0,-offsetX*45,0),0.5f);
				if(planeChild.eulerAngles.y > 35 && planeChild.eulerAngles.y < 180)
					planeChild.eulerAngles = new Vector3(0,35,0);
				else if(planeChild.eulerAngles.y > 180 && planeChild.eulerAngles.y < 325)
					planeChild.eulerAngles = new Vector3(0,325,0);
				//Debug.Log("OFFSET X:  " + offsetX + ", pevX: " + (endX - previousEndX));
			}
			
			else if(controlType == 2)
			{
				endX = Input.mousePosition.x;
				endY = Input.mousePosition.y;
				offsetX = (endX - startX)/Screen.height*45;
				offsetY = (endY - startY)/Screen.height*45;
				//transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(transform.position.x + offsetX*10.4f,boundLeft,boundRight), Mathf.Clamp(transform.position.y + offsetY*10.4f,boundDown,boundUp), transform.position.z), Time.deltaTime*4);//0.9 -- Time.deltaTime*12
				//transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Clamp(transform.position.x + offsetX,boundLeft,boundRight), Mathf.Clamp(transform.position.y + offsetY,boundDown,boundUp), transform.position.z),1);//Time.deltaTime*4);
				
				//transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Clamp(transform.position.x + GameObject.Find("Buljavost").transform.position.x,boundLeft,boundRight), Mathf.Clamp(transform.position.y + GameObject.Find("Buljavost").transform.position.y,boundDown,boundUp), transform.position.z),1);//Time.deltaTime*4);
				//transform.position = new Vector3(Mathf.Clamp(Mathf.MoveTowards(transform.position.x, transform.position.x + offsetX, 0.93f),boundLeft,boundRight), Mathf.Clamp(Mathf.MoveTowards(transform.position.y, transform.position.y + offsetY, 0.99f),boundDown,boundUp), transform.position.z);
				transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, transform.position.x + offsetX, 0.97f),planeBoundsLeft,planeBoundsRight), Mathf.Clamp(Mathf.Lerp(transform.position.y, transform.position.y + offsetY, 1f),boundDown,boundUp), transform.position.z);
				//transform.position = new Vector3(Mathf.Clamp(transform.position.x + offsetX,boundLeft,boundRight), Mathf.Clamp(transform.position.y + offsetY,boundDown,boundUp), transform.position.z);
				
				//transform.Translate(new Vector3(offsetX,offsetY,0));
				//	Debug.Log("offsetX: " + offsetX + ", ad: " + Time.deltaTime);
				//transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);//0.9
				//animator.SetFloat("Speed",offsetX);
				planeChild.rotation = Quaternion.Lerp(planeChild.rotation,Quaternion.Euler(0,-offsetX*45,0),0.5f);
				if(planeChild.eulerAngles.y > 35 && planeChild.eulerAngles.y < 180)
					planeChild.eulerAngles = new Vector3(0,35,0);
				else if(planeChild.eulerAngles.y > 180 && planeChild.eulerAngles.y < 325)
					planeChild.eulerAngles = new Vector3(0,325,0);
			}
			
			startX = endX;
			startY = endY;
			previousEndX = endX;
		}
		
		if(isShooting)
		{
			if(fireRateCounter >= fireRate)
			{
				FireBullet();
				fireRateCounter = 0;
			}
			else
			{
				//fireRateCounter++;
				fireRateCounter+=Time.deltaTime;
			}
			if(wingBulletPool != null)
			{
				if(wingFireRateCounter >= wingFireRate)
				{
					FireWingBullet();
					wingFireRateCounter = 0;
				}
				else
				{
					wingFireRateCounter += Time.deltaTime;
				}
			}
			if(sideBulletPool != null)
			{
				if(sideFireRateCounter >= sideFireRate)
				{
					FireSideBullet();
					sideFireRateCounter = 0;
				}
				else
				{
					sideFireRateCounter += Time.deltaTime;
				}
			}
		}
	}
	
	void NormalTime2()
	{
		if(!normalTime)
		{
			normalTime = true;
		}
	}
	
	void StartCircleDissapear()
	{
		GameObject.Find("StartCircleHolder").SetActive(false);
	}
	
	public void StartPlaying()
	{
		GameObject clouds = GameObject.Find("ShopAndClouds");
		gameActive = true;
		MoveBg.canMove = true;
		Tut_GameManager.canMove = true;
		//GameObject.Find("ShopAndClouds").SetActive(false);
		clouds.transform.GetChild(0).GetComponent<Animation>().Play();
		Destroy(clouds,2f);
		isShooting = true;
		Tut_PopUpHandler.popupType = 0;
		Invoke("CanSlowTime",0.5f);
	}
	
	public IEnumerator NormalTime(float time)
	{
		Debug.Log("PRESEDANDHOLD pre: " + pressedAndHold);
		yield return new WaitForSeconds(time);
		Debug.Log("PRESEDANDHOLD posle: " + pressedAndHold);
		if(pressedAndHold)
		{
			normalTime = true;
			StopCoroutine(Tut_Interface.Instance.SlowTime());
			StartCoroutine(Tut_Interface.Instance.NormalTime());
			Tut_PopUpHandler.Instance.ShowMenu();
		}
	}
	
	public void NormalTimeAfterWeaponUse()
	{
		normalTime = true;
		StopCoroutine(Tut_Interface.Instance.SlowTime());
		StartCoroutine(Tut_Interface.Instance.NormalTime());
		Time.timeScale = 1;
		menuIsActive = false;
		Tut_PopUpHandler.Instance.ShowMenu();
	}
	
	void LateUpdate()
	{
		//if(transform.position.x < firstQuarter || offsetX != 0)
		//Debug.Log("Avion: " + transform.position.x + ", ivica kamere: " + (Camera.main.transform.position.x - Camera.main.orthographicSize*Camera.main.aspect + 1));
		//Debug.Log("MRDAAAAK: " + (endX-previousEndX));
		if(offsetX != 0)
		{
			//Debug.Log("111");
			//Debug.Log("ALO BRE: " + cameraMax + " " + cameraMin);
			//Camera.main.transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(Camera.main.transform.position.x,transform.position.x,Time.deltaTime*2),cameraMin,cameraMax),Camera.main.transform.position.y,Camera.main.transform.position.z);
			//Camera.main.transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(Camera.main.transform.position.x,Camera.main.transform.position.x+offsetX*5.2f,Time.deltaTime*3f),cameraMin,cameraMax),Camera.main.transform.position.y,Camera.main.transform.position.z);
			cameraParent.position = new Vector3(Mathf.Clamp(Mathf.Lerp(cameraParent.position.x,cameraParent.position.x+offsetX*5.2f,Time.deltaTime*3f),cameraMin,cameraMax),cameraParent.position.y,cameraParent.position.z);
		}
		else if(controlType == 1 && endX - previousEndX != 0)
		{
			cameraParent.position = new Vector3(Mathf.Clamp(Mathf.Lerp(cameraParent.position.x,cameraParent.position.x-(endX-previousEndX)/1.5f,Time.deltaTime*3f),cameraMin,cameraMax),cameraParent.position.y,cameraParent.position.z);
		}
		//		else if(transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize*Camera.main.aspect + 3 && controlType == 1)
		//		else if(transform.position.x < firstQuarter)
		//		{
		//			Debug.Log("123123");
		//			Camera.main.transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(Camera.main.transform.position.x,cameraMin,Time.deltaTime*3f),cameraMin,cameraMax),Camera.main.transform.position.y,Camera.main.transform.position.z);
		//		}
		//else if(transform.position.x > thirdQuarter)
		//{
		//	Camera.main.transform.position = new Vector3(Mathf.Lerp(Camera.main.transform.position.x,cameraMax,Time.deltaTime*2),Camera.main.transform.position.y,Camera.main.transform.position.z);
		//}
		//		else
		//		{
		//			Camera.main.transform.position = new Vector3(Mathf.Lerp(Camera.main.transform.position.x,0,Time.deltaTime*2),Camera.main.transform.position.y,Camera.main.transform.position.z);
		//		}
	}
	
	public void DisablePlayer()
	{
		isShooting = false;
		gameActive = false;
		//BoxCollider2D[] colls = GetComponents<BoxCollider2D>();
		//colls[0].enabled = false;
		//colls[1].enabled = false;
		GetComponent<Collider2D>().enabled = false;
		transform.GetChild(0).Find("PlaneShadow").GetComponent<Renderer>().enabled = false;
	}
	
	public void EnablePlayer()
	{
		transform.GetChild(0).Find("PlaneShadow").GetComponent<Renderer>().enabled = true;
		notControlling = true;
		dontSlowTime = 1;
		StartCoroutine(MoveAndBlink());
		SoundManager.Instance.Play_PlaneResurrect();
	}
	
	IEnumerator MoveAndBlink()
	{
		int i=0;
		bool radi = false;
		transform.position = new Vector3(cameraParent.position.x, cameraParent.position.y - 23, transform.position.z);
		Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
		
		planeChild.gameObject.SetActive(true);
		planeChild.parent.Find("Elipse").gameObject.SetActive(true);
		planeChild.parent.Find("GunsHolder").gameObject.SetActive(true);
		Renderer plane = planeChild.GetComponent<Renderer>();
		GameObject elipse = planeChild.parent.Find("Elipse").gameObject;
		GameObject mainGuns = activeMainGuns.gameObject;
		GameObject wingGuns = null;
		GameObject sideGuns = null;
		if(activeWingGuns != null)
			wingGuns = activeWingGuns.gameObject;
		if(activeSideGuns != null)
			sideGuns = activeSideGuns.gameObject;
		
		float t = 0;
		float distance = 0;
		bool ok = false;
		
		while(t < 1)
		{
			yield return null;
			if(transform.position != targetPos && !ok)
			{
				transform.position = Vector3.MoveTowards(transform.position,targetPos,0.2f);
				distance = targetPos.y - transform.position.y;
				if(transform.position == targetPos)
				{
					//targetPos = new Vector3(transform.position.x, cameraParent, transform.position.z);
					//transform.position = new Vector3(transform.position.x, cameraParent.position.y - distance, transform.position.z);
					ok = true;
				}
			}
			
			plane.enabled = radi;
			elipse.SetActive(radi);
			mainGuns.SetActive(radi);
			if(activeWingGuns != null)
				wingGuns.SetActive(radi);
			if(activeSideGuns != null)
				sideGuns.SetActive(radi);
			if(i==5)
			{
				i=0;
				radi = !radi;
			}
			i++;
			t += Time.deltaTime/3;
		}
		plane.enabled = true;
		elipse.SetActive(true);
		mainGuns.SetActive(true);
		if(activeWingGuns != null)
			wingGuns.SetActive(true);
		if(activeSideGuns != null)
			sideGuns.SetActive(true);
		
		isShooting = true;
		gameActive = true;
		notControlling = false;
		Invoke("CanSlowTime",0.5f);
		normalTime = true;
		yield return new WaitForSeconds(1f);
		GetComponent<Collider2D>().enabled = true;
	}
	
	void FireBullet()
	{
		//		for(int i=0; i<bulletPool.childCount; i++)
		//		{
		//			Bullet tempScript = bulletPool.GetChild(i).GetComponent<Bullet>();
		//			if(tempScript.available)
		//			{
		//				tempScript.initialized = true;
		//				break;
		//			}
		//		}
		if(bulletIndex == bulletPool.childCount)
			bulletIndex = 0;
		
		Tut_Bullet tempScript = bulletPool.GetChild(bulletIndex).GetComponent<Tut_Bullet>();
		if(tempScript.available)
		{
			tempScript.initialized = true;
			SoundManager.Instance.Play_FireBullet();
			//break;
		}
		bulletIndex++;
	}
	
	void FireWingBullet()
	{
		if(wingBulletIndex == wingBulletPool.childCount)
			wingBulletIndex = 0;
		
		Tut_Bullet tempScript = wingBulletPool.GetChild(wingBulletIndex).GetComponent<Tut_Bullet>();
		if(tempScript.available)
		{
			tempScript.initialized = true;
			//break;
		}
		wingBulletIndex++;
	}
	
	void FireSideBullet()
	{
		if(sideBulletIndex == sideBulletPool.childCount)
			sideBulletIndex = 0;
		
		Tut_SideBullet tempScript1 = sideBulletPool.GetChild(sideBulletIndex).GetComponent<Tut_SideBullet>();
		if(tempScript1.available)
		{
			tempScript1.initialized = true;
			//break;
		}
		sideBulletIndex++;
	}
	
	//	void HideBossText()
	//	{
	//		GameObject.Find("BossTime").renderer.enabled = false;
	//	}
	
	string RaycastFunction(Vector3 pos)
	{
		Ray ray = guiCamera.ScreenPointToRay(pos);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 35))
		{
			return hit.collider.name;
		}
		return System.String.Empty;
	}
	
	public void StartLaser()
	{
		dontSlowTime = 1;
		isShooting = false;
		activeWeapon = weaponLaser;
		activeWeapon.SetActive(true);
		SoundManager.Instance.Play_LaunchLaser();
		StartCoroutine(TurnOffWeapon(PandaPlane.laserDuration));
	}
	
	public void StartTesla()
	{
		dontSlowTime = 1;
		isShooting = false;
		activeWeapon = weaponTesla;
		activeWeapon.SetActive(true);
		SoundManager.Instance.Play_LaunchTesla();
		StartCoroutine(TurnOffWeapon(PandaPlane.teslaDuration));
	}
	
	public void StartBlades()
	{
		dontSlowTime = 1;
		activeWeapon = weaponBlades;
		activeWeapon.SetActive(true);
		activeWeapon.SendMessage("Activate");
		SoundManager.Instance.Play_LaunchBlades();
		StartCoroutine(TurnOffWeapon(PandaPlane.bladesDuration+1.5f));
	}
	
	public void StartBomb()
	{
		dontSlowTime = 1;
		isShooting = false;
		activeWeapon = weaponBomb;
		weaponBomb.transform.position = transform.position;
		activeWeapon.SetActive(true);
		SoundManager.Instance.Play_LaunchBomb();
		StartCoroutine(TurnOffWeapon(1f));
		Camera.main.GetComponent<Animation>().Play();
	}
	
}
