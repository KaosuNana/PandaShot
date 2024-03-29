using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
    public Button playGame;
    public GameObject circle;
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

    static PlaneManager instance;
    public static PlaneManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(PlaneManager)) as PlaneManager;

            return instance;
        }
    }

    void Awake()
    {
        planeChild = transform.GetChild(0).Find("PandaPlane");
    }

    // Use this for initialization
    void Start()
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
        firstQuarter = Camera.main.ViewportToWorldPoint(Vector3.one * 0.25f).x;
        thirdQuarter = Camera.main.ViewportToWorldPoint(Vector3.one * 0.75f).x;
        cameraMin = boundLeft + Camera.main.orthographicSize * Camera.main.aspect;//-2
        cameraMax = boundRight - Camera.main.orthographicSize * Camera.main.aspect;//+2
        planeBoundsLeft = boundLeft + Camera.main.aspect * 4;
        planeBoundsRight = boundRight - Camera.main.aspect * 4;
        //cameraMin = boundLeft + Camera.main.orthographicSize*0.625f;
        //cameraMax = boundRight - Camera.main.orthographicSize*0.625f;
        fireRateCounter = fireRate;
        //firePosition = planeChild.Find("FirePosition").transform;
        firePosition = transform.GetChild(0).Find("FirePosition").transform;
        wingFirePosition = transform.GetChild(0).Find("WingFirePosition").transform;
        sideFirePosition = transform.GetChild(0).Find("SideFirePosition").transform;
        gameActive = false;
        dontSlowTime = 1;

        if (PlayerPrefs.HasKey("ControlType"))
            controlType = PlayerPrefs.GetInt("ControlType");

        //SoundManager.Instance.Play_CloudsPassing();
        playGame.onClick.AddListener(BeginGame);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playGame.gameObject);
    }

    void BeginGame()
    {
        SoundManager.Instance.Stop_CloudsPassing();
        LevelGenerator.checkpoint = false;
        //GameObject circle = GameObject.Find(clickedItem); todo play
        circle.GetComponent<Collider>().enabled = false;
        //circle.GetComponent<Animation>().Play("StartCircleDisappear_Animation");
        if (circle.transform.GetChild(0).gameObject.activeSelf)
            circle.transform.GetChild(0).gameObject.SetActive(false);
        PopUpHandler.Instance.ResumeGame();
        Invoke("StartCircleDissapear", 1.6f);
        StartPlaying();
        playGame.gameObject.SetActive(false);
    }
    void CanSlowTime()
    {
        if (dontSlowTime == 1)
            dontSlowTime = 0;
    }

    public void DetermineGunsAndArmorLevel()
    {
        bulletPool = (Instantiate(Resources.Load("Bullets/BulletPool" + PandaPlane.mainGunLvl.ToString())) as GameObject).transform;
        if (PandaPlane.mainGunLvl > 7)
        {
            activeMainGuns = transform.GetChild(0).Find("GunsHolder/MainGuns/Level4");
            activeMainGuns.gameObject.SetActive(true);
        }
        else if (PandaPlane.mainGunLvl > 3)
        {
            activeMainGuns = transform.GetChild(0).Find("GunsHolder/MainGuns/Level3");
            activeMainGuns.gameObject.SetActive(true);
        }
        else if (PandaPlane.mainGunLvl > 0)
        {
            activeMainGuns = transform.GetChild(0).Find("GunsHolder/MainGuns/Level2");
            activeMainGuns.gameObject.SetActive(true);
        }
        else
        {
            activeMainGuns = transform.GetChild(0).Find("GunsHolder/MainGuns/Level1");
            activeMainGuns.gameObject.SetActive(true);
        }

        if (PandaPlane.wingGunLvl > 0)
        {
            wingBulletPool = (Instantiate(Resources.Load("Bullets/WingBulletPool" + PandaPlane.wingGunLvl.ToString())) as GameObject).transform;
            if (PandaPlane.wingGunLvl > 7)
            {
                activeWingGuns = transform.GetChild(0).Find("GunsHolder/WingGuns/Level3");
                activeWingGuns.gameObject.SetActive(true);
            }
            else if (PandaPlane.wingGunLvl > 3)
            {
                activeWingGuns = transform.GetChild(0).Find("GunsHolder/WingGuns/Level2");
                activeWingGuns.gameObject.SetActive(true);
            }
            else
            {
                activeWingGuns = transform.GetChild(0).Find("GunsHolder/WingGuns/Level1");
                activeWingGuns.gameObject.SetActive(true);
            }
        }

        if (PandaPlane.sideGunLvl > 0)
        {
            sideBulletPool = (Instantiate(Resources.Load("Bullets/SideBulletPool" + PandaPlane.sideGunLvl.ToString())) as GameObject).transform;
            activeSideGuns = transform.GetChild(0).Find("GunsHolder/SideGuns");
            activeSideGuns.gameObject.SetActive(true);
        }

        fireRate -= PandaPlane.mainGunLvl * 0.025f;
        wingFireRate -= PandaPlane.wingGunLvl * 0.025f;

        if (PandaPlane.armorLvl > 7)
        {
            planeChild.GetComponent<SpriteRenderer>().sprite = GameObject.Find("PlaneReferences/PandaPlaneArmor3Gameplay").GetComponent<SpriteRenderer>().sprite;
            planeChild.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameObject.Find("PlaneReferences/PandaPlaneArmor3HIT").GetComponent<SpriteRenderer>().sprite;
        }
        else if (PandaPlane.armorLvl > 3)
        {
            //"ISPOD JE PRIJAVIO NULL REFERENCE EXCEPTION!!!!!" +
            //"JAVLJA SE KAD SE NABUDZI ARMOR NA LEVEL 2 NA NPR. ICE I WASTELAND STAGE!!!!!"
            planeChild.GetComponent<SpriteRenderer>().sprite = GameObject.Find("PlaneReferences/PandaPlaneArmor2Gameplay").GetComponent<SpriteRenderer>().sprite;
            planeChild.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameObject.Find("PlaneReferences/PandaPlaneArmor2HIT").GetComponent<SpriteRenderer>().sprite;
        }
    }

    public IEnumerator TurnOffWeapon(float time)
    {
        yield return new WaitForSeconds(time);
        if (activeWeapon != null)
        {
            if (activeWeapon.name.Contains("Laser"))
            {
                activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].normalizedTime = 1;
                activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].speed = -1;
                activeWeapon.transform.GetChild(0).GetComponent<Animation>().Play();
                yield return new WaitForSeconds(0.6f);
                activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].normalizedTime = 0;
                activeWeapon.transform.GetChild(0).GetComponent<Animation>()["LaserLaunch"].speed = 1;
            }
            //  else if(activeWeapon.name.Contains("Tesla"))
            //  {
            //   activeWeapon.GetComponents<BoxCollider2D>()[0].enabled = false;
            //   yield return new WaitForSeconds(0.5f);
            //   activeWeapon.SetActive(false);
            //   activeWeapon.GetComponents<BoxCollider2D>()[0].enabled = true;
            //   //activeWeapon.GetComponents<BoxCollider2D>()[1].enabled = false;
            //   //activeWeapon.collider2D.enabled = false;
            //   activeWeapon.layer = LayerMask.NameToLayer("Default");
            //   activeWeapon.transform.GetChild(0).gameObject.SetActive(false);
            //   activeWeapon.transform.GetChild(1).particleSystem.Play();
            //   yield return new WaitForSeconds(1f);
            //   activeWeapon.SetActive(false);
            //   //activeWeapon.collider2D.enabled = true;
            //   activeWeapon.layer = LayerMask.NameToLayer("PlayerBullet");
            //   activeWeapon.transform.GetChild(0).gameObject.SetActive(true);
            //  }
            if (PandaPlane.health > 0)
                isShooting = true;
            yield return new WaitForSeconds(0.25f);
            if (activeWeapon.activeSelf)
                activeWeapon.SetActive(false);
            activeWeapon = null;
            if (dontSlowTime == 1)
                dontSlowTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        boundLeft = bounds.Find("BoundUpLeft").position.x;
        boundUp = bounds.Find("BoundUpLeft").position.y;
        boundRight = bounds.Find("BoundDownRight").position.x;
        boundDown = bounds.Find("BoundDownRight").position.y;
        //		firstQuarter = Camera.main.ViewportToWorldPoint(Vector3.one*0.25f).x;
        //		thirdQuarter = Camera.main.ViewportToWorldPoint(Vector3.one*0.75f).x;
        //		cameraMin = boundLeft + Camera.main.orthographicSize*Camera.main.aspect-3;
        //		cameraMax = boundRight - Camera.main.orthographicSize*Camera.main.aspect+3;

        if (Input.GetKeyUp(KeyCode.Escape) && PlayerPrefs.GetInt("OpenEnough") != 1)
        {
            if (!bossTimePart && !PopUpHandler.Instance.bossDestroyed)
            {
                //Interface.Instance.PauseGame();
                if (PopUpHandler.popupType == 0) // regular game
                {
                    if (dontSlowTime == 0)
                    {
                        Time.timeScale = 0;
                        menuIsActive = true;
                        PopUpHandler.Instance.ShowPauseScreen();
                    }
                }
                else if (PopUpHandler.popupType == 1) // slow time screen
                {
                    if (Time.timeScale == 1f)
                    {
                        menuIsActive = true;
                        PopUpHandler.Instance.ShowPauseScreen();
                    }

                }
                else if (PopUpHandler.popupType == 2) // paused game
                {
                    PopUpHandler.Instance.ResumeGameFromPause();
                }
                else if (PopUpHandler.popupType == 3) // keep playing screen
                {
                    PopUpHandler.Instance.ShowGameOverScreen();
                }
                else if (PopUpHandler.popupType == 4) // game over
                {
                    AdsManager.Instance.ShowInterstitial();
                    PopUpHandler.Instance.LoadMainScene();
                }
                else if (PopUpHandler.popupType == 5) // confirmation message
                {
                    PopUpHandler.Instance.CloseConfirmationMessagePopup();
                }
                else if (PopUpHandler.popupType == 6) // no video available
                {
                    PopUpHandler.Instance.CloseNoVideoAvailablePopUp();
                }
            }
        }
        //if(Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.B))
        {
            if (bossTimePart)
            {
                //speedUpTime = true;
                gameActive = false;
                isShooting = false;
                pressedAndHold = true;
                PopUpHandler.Instance.SpeedUpDialog();
                if (controlType == 1)
                {
                    //startX = endX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                    //startY = endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                }
                else
                {
                    startX = endX = Input.mousePosition.x;
                    startY = endY = Input.mousePosition.y;
                }
                //Time.timeScale = 5f;
                //GameManager.Instance.speed /= 5f;
                //transform.GetChild(0).Find("Elipse").animation["PandaPlaneElipseRotationAnimation"].speed = 1/5f;
            }
            else
            {
                clickedItem = RaycastFunction(Input.mousePosition);
                if (clickedItem == "")
                {
                    clickedItem = "StartCircleHolder";

                }
                //				Debug.Log("OVDE SE POZIVA KLIK 1 "+gameActive);
                if (gameActive)
                {
                    //					Debug.Log("OVDE SE POZIVA KLIK 2 "+gameActive);

                    if (activeWeapon == null)
                        isShooting = true;
                    notControlling = false;
                    if (!Interface.Instance.normalTime)
                    {
                        //						Debug.Log("OVDE SE POZIVA KLIK 3 "+Interface.Instance.normalTime);
                        if (!menuIsActive)
                        {
                            Interface.Instance.normalTime = true;
                        }

                        //StopCoroutine(Interface.Instance.SlowTime());
                        //if(!menuIsActive)
                        //	StartCoroutine(Interface.Instance.NormalTime());
                        //PopUpHandler.Instance.ResumeGame();

                        pressedAndHold = true;
                        //						Debug.Log("OVDE SE POZIVA KLIK 4 "+menuIsActive);
                        if (!normalTime && !menuIsActive)
                        {
                            //							Debug.Log("OVDE SE POZIVA NormalTime");
                            //StartCoroutine("NormalTime",0.075f);
                            StartCoroutine("NormalTime", 0.01f);
                        }
                    }

                    if (controlType == 1)
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

                if (clickedItem.Equals("WeaponLaser"))
                {
                    dontSlowTime = 1;
                    isShooting = false;
                    activeWeapon = weaponLaser;
                    activeWeapon.SetActive(true);
                    StartCoroutine(TurnOffWeapon(2f));
                }
                else if (clickedItem.Equals("WeaponTesla"))
                {
                    dontSlowTime = 1;
                    isShooting = false;
                    activeWeapon = weaponTesla;
                    activeWeapon.SetActive(true);
                    StartCoroutine(TurnOffWeapon(4f));
                }
                else if (clickedItem.Equals("WeaponBlades"))
                {
                    dontSlowTime = 1;
                    activeWeapon = weaponBlades;
                    activeWeapon.SetActive(true);
                    activeWeapon.SendMessage("Activate");
                    StartCoroutine(TurnOffWeapon(PandaPlane.bladesDuration + 1.5f));
                }
                else if (clickedItem.Equals("WeaponBomb"))
                {
                    dontSlowTime = 1;
                    isShooting = false;
                    activeWeapon = weaponBomb;
                    weaponBomb.transform.position = transform.position;
                    activeWeapon.SetActive(true);
                    StartCoroutine(TurnOffWeapon(1f));
                    Camera.main.GetComponent<Animation>().Play();
                }
                else if (clickedItem.Equals("StartCircleHolder"))
                {
                    //SoundManager.Instance.Stop_CloudsPassing();
                    //LevelGenerator.checkpoint = false;
                    ////GameObject circle = GameObject.Find(clickedItem); todo play
                    //circle.GetComponent<Collider>().enabled = false;
                    ////circle.GetComponent<Animation>().Play("StartCircleDisappear_Animation");
                    //if (circle.transform.GetChild(0).gameObject.activeSelf)
                    //    circle.transform.GetChild(0).gameObject.SetActive(false);
                    //PopUpHandler.Instance.ResumeGame();
                    //Invoke("StartCircleDissapear", 1.6f);
                    //StartPlaying();
                    //SoundManager.Instance.Play_GameplayMusic();
                }
            }
        }

        //else if(Input.GetMouseButton(0) && gameActive && normalTime)
        else if (Input.GetKeyUp(KeyCode.A))
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

            if (controlType == 1)
            {
                endX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

                offsetX = endX - startX;
                offsetY = endY - startY;

                //endX = Camera.main.ScreenToWorldPoint(new Vector3(endX,0,0)).x;
                transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(endX, planeBoundsLeft, planeBoundsRight), Mathf.Clamp(endY + 4.75f, boundDown, boundUp), transform.position.z), Time.deltaTime * 8);//0.175 -- Time.deltaTime*12
                previousEndX = transform.position.x;
                //	Debug.Log("endX: " + endX);
                //animator.SetFloat("Speed",(endX-previousEndX));
                planeChild.rotation = Quaternion.Lerp(planeChild.rotation, Quaternion.Euler(0, -offsetX * 45, 0), 0.5f);
                if (planeChild.eulerAngles.y > 35 && planeChild.eulerAngles.y < 180)
                    planeChild.eulerAngles = new Vector3(0, 35, 0);
                else if (planeChild.eulerAngles.y > 180 && planeChild.eulerAngles.y < 325)
                    planeChild.eulerAngles = new Vector3(0, 325, 0);
                //Debug.Log("OFFSET X:  " + offsetX + ", pevX: " + (endX - previousEndX));
            }

            else if (controlType == 2)
            {
                endX = Input.mousePosition.x;
                endY = Input.mousePosition.y;
                offsetX = (endX - startX) / Screen.height * 45;
                offsetY = (endY - startY) / Screen.height * 45;
                //transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(transform.position.x + offsetX*10.4f,boundLeft,boundRight), Mathf.Clamp(transform.position.y + offsetY*10.4f,boundDown,boundUp), transform.position.z), Time.deltaTime*4);//0.9 -- Time.deltaTime*12
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Clamp(transform.position.x + offsetX,boundLeft,boundRight), Mathf.Clamp(transform.position.y + offsetY,boundDown,boundUp), transform.position.z),1);//Time.deltaTime*4);

                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Clamp(transform.position.x + GameObject.Find("Buljavost").transform.position.x,boundLeft,boundRight), Mathf.Clamp(transform.position.y + GameObject.Find("Buljavost").transform.position.y,boundDown,boundUp), transform.position.z),1);//Time.deltaTime*4);
                //transform.position = new Vector3(Mathf.Clamp(Mathf.MoveTowards(transform.position.x, transform.position.x + offsetX, 0.93f),boundLeft,boundRight), Mathf.Clamp(Mathf.MoveTowards(transform.position.y, transform.position.y + offsetY, 0.99f),boundDown,boundUp), transform.position.z);
                transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(transform.position.x, transform.position.x + offsetX, 0.97f), planeBoundsLeft, planeBoundsRight), Mathf.Clamp(Mathf.Lerp(transform.position.y, transform.position.y + offsetY, 1f), boundDown, boundUp), transform.position.z);
                //transform.position = new Vector3(Mathf.Clamp(transform.position.x + offsetX,boundLeft,boundRight), Mathf.Clamp(transform.position.y + offsetY,boundDown,boundUp), transform.position.z);

                //transform.Translate(new Vector3(offsetX,offsetY,0));
                //	Debug.Log("offsetX: " + offsetX + ", ad: " + Time.deltaTime);
                //transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);//0.9
                //animator.SetFloat("Speed",offsetX);
                planeChild.rotation = Quaternion.Lerp(planeChild.rotation, Quaternion.Euler(0, -offsetX * 45, 0), 0.5f);
                if (planeChild.eulerAngles.y > 35 && planeChild.eulerAngles.y < 180)
                    planeChild.eulerAngles = new Vector3(0, 35, 0);
                else if (planeChild.eulerAngles.y > 180 && planeChild.eulerAngles.y < 325)
                    planeChild.eulerAngles = new Vector3(0, 325, 0);
            }

            startX = endX;
            startY = endY;
            previousEndX = endX;
        }

        //else if(Input.GetMouseButtonUp(0))
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (bossTimePart)
            {
                //Time.timeScale = 1;
                PopUpHandler.Instance.NormalDialog();
            }
            else
            {
                releasedItem = RaycastFunction(Input.mousePosition);
                pressedAndHold = false;
                //if(!dontSlowTime && !notControlling)
                if (dontSlowTime == 0)
                {
                    normalTime = false;
                    StopCoroutine("NormalTime");
                }

                if (clickedItem.Equals(releasedItem))
                {
                    if (releasedItem.Equals("ControlsType1"))
                    {
                        controlType = 1;
                        Interface.Instance.transform.Find("MenuHolder/Menu/ControlsSelection").localPosition = new Vector3(-0.25f, 0, -0.1f);
                    }
                    else if (releasedItem.Equals("ControlsType2"))
                    {
                        controlType = 2;
                        Interface.Instance.transform.Find("MenuHolder/Menu/ControlsSelection").localPosition = new Vector3(0.25f, 0, -0.1f);
                    }
                    //					else if(releasedItem.Equals("StartCircleHolder"))
                    //					{
                    //						SoundManager.Instance.Stop_CloudsPassing();
                    //						LevelGenerator.checkpoint = false;
                    //						GameObject.Find(releasedItem).animation.Play("StartCircleDisappear_Animation");
                    //						PopUpHandler.Instance.ResumeGame();
                    //						Invoke("StartCircleDissapear",0.6f);
                    //						StartPlaying();
                    //						SoundManager.Instance.Play_GameplayMusic();
                    //					}

                    if (planeChild.eulerAngles.y != 0)
                        planeChild.rotation = Quaternion.Lerp(planeChild.rotation, Quaternion.Euler(0, 0, 0), 0.5f);
                }

                if (gameActive)
                {
                    startX = endX = startY = endY = 0;
                    previousEndX = endX = 0;
                    if (isShooting && dontSlowTime == 0 && !menuIsActive)
                    {
                        //isShooting = false;
                        Interface.Instance.normalTime = false;
                        menuIsActive = false;
                        //StopCoroutine(Interface.Instance.NormalTime()); @@@@@@@@
                        //StartCoroutine(Interface.Instance.SlowTime());
                        Time.timeScale = 0.1f;
                        //PopUpHandler.Instance.transform.Find("SlowTimeScreen").gameObject.SetActive(true); @@@@@@@@
                        PopUpHandler.Instance.ShowSlowTimeScreen();
                        //fireRateCounter = fireRate;
                    }
                }
            }
        }

        if (isShooting)
        {
            if (fireRateCounter >= fireRate)
            {
                FireBullet();
                fireRateCounter = 0;
            }
            else
            {
                //fireRateCounter++;
                fireRateCounter += Time.deltaTime;
            }
            if (wingBulletPool != null)
            {
                if (wingFireRateCounter >= wingFireRate)
                {
                    FireWingBullet();
                    wingFireRateCounter = 0;
                }
                else
                {
                    wingFireRateCounter += Time.deltaTime;
                }
            }
            if (sideBulletPool != null)
            {
                if (sideFireRateCounter >= sideFireRate)
                {
                    FireSideBullet();
                    sideFireRateCounter = 0;
                }
                else
                {
                    sideFireRateCounter += Time.deltaTime;
                }
            }
            if (Input.GetAxis("Horizontal") < 0 || Input.GetKey(KeyCode.LeftArrow))
                this.gameObject.transform.Translate(Vector3.left * 12 * Time.deltaTime);
            if (Input.GetAxis("Horizontal") > 0 || Input.GetKey(KeyCode.RightArrow))
                this.gameObject.transform.Translate(Vector3.right * 12 * Time.deltaTime);
            if (Input.GetAxis("Vertical") > 0 || Input.GetKey(KeyCode.UpArrow))
                this.gameObject.transform.Translate(Vector3.up * 12 * Time.deltaTime);
            if (Input.GetAxis("Vertical") < 0 || Input.GetKey(KeyCode.DownArrow))
                this.gameObject.transform.Translate(Vector3.down * 12 * Time.deltaTime);
            if (this.gameObject.transform.localPosition.x > 12)
                this.gameObject.transform.localPosition = new Vector3(12, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
            if (this.gameObject.transform.localPosition.x < -12)
                this.gameObject.transform.localPosition = new Vector3(-12, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
            if (this.gameObject.transform.localPosition.y < -20)
                this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, -20, this.gameObject.transform.localPosition.z);
            if (this.gameObject.transform.localPosition.y > 20)
                this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, 20, this.gameObject.transform.localPosition.z);
        }
    }

    void NormalTime2()
    {
        if (!normalTime)
        {
            normalTime = true;
        }
    }

    void StartCircleDissapear()
    {
        //GameObject.Find("StartCircleHolder").SetActive(false);
    }

    public GameObject clouds;
    public void StartPlaying()
    {
        if (controlType == 1)
        {
            //startX = endX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            //startY = endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        }
        else
        {
            startX = endX = Input.mousePosition.x;
            startY = endY = Input.mousePosition.y;
        }
        SoundManager.Instance.Play_GameplayMusic();
        //GameObject clouds = GameObject.Find("ShopAndClouds");
        gameActive = true;
        MoveBg.canMove = true;
        GameManager.canMove = true;
        //GameObject.Find("ShopAndClouds").SetActive(false);
        clouds.transform.GetChild(0).GetComponent<Animation>().Play();
        Destroy(clouds, 2f);
        isShooting = true;
        PopUpHandler.popupType = 0;
        Invoke("CanSlowTime", 0.5f);
    }

    public IEnumerator NormalTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (pressedAndHold)
        {
            normalTime = true;
            //StopCoroutine(Interface.Instance.SlowTime()); @@@@@@@@
            Time.timeScale = 1;
            //StartCoroutine(Interface.Instance.NormalTime()); @@@@@@@@
            //PopUpHandler.Instance.transform.Find("SlowTimeScreen").gameObject.SetActive(true); @@@@@@@@
            PopUpHandler.Instance.ShowMenu();
        }
    }

    public void ForceGameActive()
    {
        gameActive = true;
        normalTime = true;
        menuIsActive = false;
    }

    public bool ReturnGameActive()
    {
        return gameActive;
    }

    public bool ReturnNormalTime()
    {
        return normalTime;
    }

    public bool ReturnMenuIsActive()
    {
        return menuIsActive;
    }

    public int ReturnDontSlowTime()
    {
        return dontSlowTime;
    }

    public void NormalTimeAfterWeaponUse()
    {
        normalTime = true;
        StopCoroutine(Interface.Instance.SlowTime());
        StartCoroutine(Interface.Instance.NormalTime());
        Time.timeScale = 1;
        menuIsActive = false;
        PopUpHandler.Instance.ShowMenu();
    }

    void LateUpdate()
    {
        //if(transform.position.x < firstQuarter || offsetX != 0)
        //Debug.Log("Avion: " + transform.position.x + ", ivica kamere: " + (Camera.main.transform.position.x - Camera.main.orthographicSize*Camera.main.aspect + 1));
        //Debug.Log("MRDAAAAK: " + (endX-previousEndX));
        if (offsetX != 0)
        {
            //Debug.Log("111");
            //Debug.Log("ALO BRE: " + cameraMax + " " + cameraMin);
            //Camera.main.transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(Camera.main.transform.position.x,transform.position.x,Time.deltaTime*2),cameraMin,cameraMax),Camera.main.transform.position.y,Camera.main.transform.position.z);
            //Camera.main.transform.position = new Vector3(Mathf.Clamp(Mathf.Lerp(Camera.main.transform.position.x,Camera.main.transform.position.x+offsetX*5.2f,Time.deltaTime*3f),cameraMin,cameraMax),Camera.main.transform.position.y,Camera.main.transform.position.z);

            //cameraParent.position = new Vector3(Mathf.Clamp(Mathf.Lerp(cameraParent.position.x,cameraParent.position.x+offsetX*5.2f,Time.deltaTime*3f),cameraMin,cameraMax),cameraParent.position.y,cameraParent.position.z);
        }
        else if (controlType == 1 && endX - previousEndX != 0)
        {

            //cameraParent.position = new Vector3(Mathf.Clamp(Mathf.Lerp(cameraParent.position.x,cameraParent.position.x-(endX-previousEndX)/1.5f,Time.deltaTime*3f),cameraMin,cameraMax),cameraParent.position.y,cameraParent.position.z);
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
        int i = 0;
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
        if (activeWingGuns != null)
            wingGuns = activeWingGuns.gameObject;
        if (activeSideGuns != null)
            sideGuns = activeSideGuns.gameObject;

        float t = 0;
        float distance = 0;
        bool ok = false;

        while (t < 1)
        {
            yield return null;
            if (transform.position != targetPos && !ok)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.2f);
                distance = targetPos.y - transform.position.y;
                if (transform.position == targetPos)
                {
                    //targetPos = new Vector3(transform.position.x, cameraParent, transform.position.z);
                    //transform.position = new Vector3(transform.position.x, cameraParent.position.y - distance, transform.position.z);
                    ok = true;
                }
            }

            plane.enabled = radi;
            elipse.SetActive(radi);
            mainGuns.SetActive(radi);
            if (activeWingGuns != null)
                wingGuns.SetActive(radi);
            if (activeSideGuns != null)
                sideGuns.SetActive(radi);
            if (i == 5)
            {
                i = 0;
                radi = !radi;
            }
            i++;
            t += Time.deltaTime / 3;
        }
        plane.enabled = true;
        elipse.SetActive(true);
        mainGuns.SetActive(true);
        if (activeWingGuns != null)
            wingGuns.SetActive(true);
        if (activeSideGuns != null)
            sideGuns.SetActive(true);

        isShooting = true;
        gameActive = true;
        notControlling = false;
        menuIsActive = false;
        Invoke("CanSlowTime", 0.5f);
        normalTime = true;
        if (controlType == 1)
        {
            //startX = endX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            //startY = endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        }
        else
        {
            startX = endX = Input.mousePosition.x;
            startY = endY = Input.mousePosition.y;
        }
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
        if (bulletIndex == bulletPool.childCount)
            bulletIndex = 0;

        Bullet tempScript = bulletPool.GetChild(bulletIndex).GetComponent<Bullet>();
        if (tempScript.available)
        {
            tempScript.initialized = true;
            SoundManager.Instance.Play_FireBullet();
            //break;
        }
        bulletIndex++;
    }

    void FireWingBullet()
    {
        if (wingBulletIndex == wingBulletPool.childCount)
            wingBulletIndex = 0;

        Bullet tempScript = wingBulletPool.GetChild(wingBulletIndex).GetComponent<Bullet>();
        if (tempScript.available)
        {
            tempScript.initialized = true;
            //break;
        }
        wingBulletIndex++;
    }

    void FireSideBullet()
    {
        if (sideBulletIndex == sideBulletPool.childCount)
            sideBulletIndex = 0;

        SideBullet tempScript1 = sideBulletPool.GetChild(sideBulletIndex).GetComponent<SideBullet>();
        if (tempScript1.available)
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
        if (Physics.Raycast(ray, out hit, 35))
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
        StartCoroutine(TurnOffWeapon(PandaPlane.bladesDuration + 1.5f));
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

    public void ResetCoordinates()
    {
        if (controlType == 1)
        {
            //startX = endX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            //startY = endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        }
        else
        {
            startX = endX = Input.mousePosition.x;
            startY = endY = Input.mousePosition.y;
        }
    }

}
