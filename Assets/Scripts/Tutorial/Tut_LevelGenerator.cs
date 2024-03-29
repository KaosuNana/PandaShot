using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Tut_LevelGenerator : MonoBehaviour {
	
	static Tut_LevelGenerator instance;
	public static Tut_LevelGenerator Instance
	{
		get
		{
			if(instance == null)
				instance = GameObject.FindObjectOfType(typeof(Tut_LevelGenerator)) as Tut_LevelGenerator;
			
			return instance;
		}
	}
	
	[HideInInspector] public Transform activeTerrain;
	[HideInInspector] public Transform snapObject;
	List<int> availableTerrainIndexes; 
	public int terenaUPocetku = 8;
	public static int terrainsPassed = 0;
	
	MoveBg activeTerrainProperties;
	MoveBg newTerrainProperties;
	[HideInInspector] public static int currentStage = 1;
	
	public static int currentBossPlaneLevel = 1;
	public static int currentBossShipLevel = 1;
	public static int currentBossTankLevel = 1;
	GameObject currentBoss;
	[HideInInspector] public int currentBossAvailableTerrainType;
	[HideInInspector] public int currentBossCameraOffsetY;
	public GameObject warpZonePrefab;
	public static List<int> levels;
	static int minimalLevel = 2;
	public static bool checkpoint = false;
	
	
	// Use this for initialization
	void Start () 
	{
		availableTerrainIndexes = new List<int>();
		activeTerrain = transform.Find("TerrainPreset3");
		activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
		snapObject = activeTerrain.Find("SnapObject");
		
		//		for(int i=0;i<transform.childCount;i++)
		//		{
		//			newTerrainProperties = transform.GetChild(i).GetComponent<MoveBg>();
		//			if(newTerrainProperties.minimumStage <= currentStage && newTerrainProperties.isAvailable)
		//			{
		//				if(System.Array.IndexOf(newTerrainProperties.availableTerrainTypes, newTerrainProperties.TerrainType) > -1)
		//					availableTerrainIndexes.Add(i);
		//			}
		//		}

		//@@@@@@@@@@@@@@@ POTENCIJALNI VISAK START
//		for(int i=0;i<terenaUPocetku-1;i++)
//		{
//			availableTerrainIndexes.Clear();
//			
//			for(int ii=0;ii<transform.childCount;ii++)
//			{
//				newTerrainProperties = transform.GetChild(ii).GetComponent<MoveBg>();
//				if(newTerrainProperties.minimumStage <= currentStage && newTerrainProperties.isAvailable && !newTerrainProperties.notAvailableUntilBoss)
//				{
//					if(System.Array.IndexOf(activeTerrainProperties.availableTerrainTypes, newTerrainProperties.TerrainType) > -1)
//						availableTerrainIndexes.Add(ii);
//				}
//			}
//			
//			if(availableTerrainIndexes.Count == 0)
//			{
//				GameObject InstantiatedTerrain;
//				for(int j=0;j<transform.childCount;j++)
//				{
//					newTerrainProperties = transform.GetChild(j).GetComponent<MoveBg>();
//					if(newTerrainProperties.minimumStage <= currentStage && !newTerrainProperties.notAvailableUntilBoss)
//					{
//						if(System.Array.IndexOf(activeTerrainProperties.availableTerrainTypes, newTerrainProperties.TerrainType) > -1)
//						{
//							availableTerrainIndexes.Add(j);
//						}
//						
//					}
//				}
//				int randomTerrainClone = Random.Range(0,availableTerrainIndexes.Count);
//				InstantiatedTerrain = Instantiate(transform.GetChild(availableTerrainIndexes[randomTerrainClone]).gameObject) as GameObject;
//				activeTerrain = InstantiatedTerrain.transform;
//				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
//				activeTerrain.position = snapObject.position;
//				snapObject = activeTerrain.Find("SnapObject");
//				activeTerrainProperties.isAvailable = false;
//				
//			}
//			else
//			{
//				int randomTerrain = Random.Range(0,availableTerrainIndexes.Count);
//				//Debug.Log("AVAILAHEL TERERAIN INDEXES COUNT: " + availableTerrainIndexes.Count + ", index izvucen: " + randomTerrain);
//				
//				activeTerrain = transform.GetChild(availableTerrainIndexes[randomTerrain]);
//				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
//				activeTerrain.position = snapObject.position;
//				snapObject = activeTerrain.Find("SnapObject");
//				activeTerrainProperties.isAvailable = false;
//				//Debug.Log("IZVUKOJE: " + randomTerrain + ", dok je teren recimo: " + transform.GetChild(availableTerrainIndexes[randomTerrain]));
//				availableTerrainIndexes.RemoveAt(randomTerrain);
//			}
//		}
//		SummoningBoss();
		//@@@@@@@@@@@@@@@ POTENCIJALNI VISAK END
		
	}
	
	//	void Update()
	//	{
	//
	//		Debug.Log("Terrains passed: " + terrainsPassed);
	//	}
	
	public void RepositionTerrain()
	{
		for(int i=0;i<terenaUPocetku-1;i++)
		{
			availableTerrainIndexes.Clear();
			
			for(int ii=0;ii<transform.childCount;ii++)
			{
				newTerrainProperties = transform.GetChild(ii).GetComponent<MoveBg>();
				if(newTerrainProperties.isAvailable)
					if(newTerrainProperties.minimumStage <= currentStage && newTerrainProperties.isAvailable && !newTerrainProperties.notAvailableUntilBoss)
				{
					if(System.Array.IndexOf(activeTerrainProperties.availableTerrainTypes, newTerrainProperties.TerrainType) > -1)
						availableTerrainIndexes.Add(ii);
				}
			}
			
			//"AKO NEMA SLOBODNIH TERENA DA INSTANCIRA NEKI KOJI PASUJE!!!!!"
			if(availableTerrainIndexes.Count == 0)
			{
				GameObject InstantiatedTerrain;
				for(int j=0;j<transform.childCount;j++)
				{
					newTerrainProperties = transform.GetChild(j).GetComponent<MoveBg>();
					if(newTerrainProperties.minimumStage <= currentStage && !newTerrainProperties.notAvailableUntilBoss)
					{
						if(System.Array.IndexOf(activeTerrainProperties.availableTerrainTypes, newTerrainProperties.TerrainType) > -1)
						{
							availableTerrainIndexes.Add(j);
						}
						
					}
				}
				int randomTerrainClone = Random.Range(0,availableTerrainIndexes.Count);
				InstantiatedTerrain = Instantiate(transform.GetChild(availableTerrainIndexes[randomTerrainClone]).gameObject) as GameObject;
				activeTerrain = InstantiatedTerrain.transform;
				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
				activeTerrain.position = snapObject.position;
				snapObject = activeTerrain.Find("SnapObject");
				activeTerrainProperties.isAvailable = false;
				
			}
			else
			{
				int randomTerrain = Random.Range(0,availableTerrainIndexes.Count);
				activeTerrain = transform.GetChild(availableTerrainIndexes[randomTerrain]);
				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
				activeTerrain.position = snapObject.position;
				snapObject = activeTerrain.Find("SnapObject");
				activeTerrainProperties.isAvailable = false;
				//Debug.Log("IZVUKOJE: " + randomTerrain + ", dok je teren recimo: " + transform.GetChild(availableTerrainIndexes[randomTerrain]));
				availableTerrainIndexes.RemoveAt(randomTerrain);
			}
		}
	}
	
	public void RepositionSingleTerrainForBoss()
	{
		availableTerrainIndexes.Clear();
		for(int ii=0;ii<transform.childCount;ii++)
		{
			newTerrainProperties = transform.GetChild(ii).GetComponent<MoveBg>();
			if(newTerrainProperties.minimumStage <= currentStage && newTerrainProperties.isAvailable)
			{
				
				//for(int q=0; q<Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes.Length;q++)
				{
					if(currentBossAvailableTerrainType == -1)
					{
						if(System.Array.IndexOf(activeTerrainProperties.availableTerrainTypes, newTerrainProperties.TerrainType) > -1)
						{
							availableTerrainIndexes.Add(ii);
						}
					}
					else
					{
						if(currentBossAvailableTerrainType.Equals(newTerrainProperties.TerrainType))
						{
							availableTerrainIndexes.Add(ii);
						}
					}
				}
				//if(System.Array.IndexOf((int[])Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes, (int)newTerrainProperties.TerrainType) > -1 || Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes.Length == 0)
				//	availableTerrainIndexes.Add(ii);
			}
		}
		
		
		int randomTerrain = Random.Range(0,availableTerrainIndexes.Count);
		activeTerrain = transform.GetChild(availableTerrainIndexes[randomTerrain]);
		activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
		activeTerrain.position = snapObject.position;
		snapObject = activeTerrain.Find("SnapObject");
		activeTerrainProperties.isAvailable = false;
		availableTerrainIndexes.RemoveAt(randomTerrain);
		
	}
	
	public void RepositionTerrainForBoss(MoveBg.stage currentStage)
	{
		switch(currentStage)
		{
		case MoveBg.stage.woods:
			if((activeTerrainProperties.TerrainType == 0 || activeTerrainProperties.TerrainType == 3 || activeTerrainProperties.TerrainType == 5) && currentBossAvailableTerrainType == 2)
			{
				activeTerrain = transform.Find("TerrainBossBridge");
				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
				activeTerrain.position = snapObject.position;
				snapObject = activeTerrain.Find("SnapObject");
				activeTerrainProperties.isAvailable = false;
				MoveBg.hasBridge = true;
			}
			else if((activeTerrainProperties.TerrainType == 1 || activeTerrainProperties.TerrainType == 2 || activeTerrainProperties.TerrainType == 4) && currentBossAvailableTerrainType == 0)
			{
				activeTerrain = transform.Find("TerrainBossBridge2");
				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
				activeTerrain.position = snapObject.position;
				snapObject = activeTerrain.Find("SnapObject");
				activeTerrainProperties.isAvailable = false;
				MoveBg.hasBridge = true;
			}
			break;
			
		case MoveBg.stage.darkWoods:
			if((activeTerrainProperties.TerrainType == 0 || activeTerrainProperties.TerrainType == 3) && currentBossAvailableTerrainType == 2)
			{
				//Debug.Log("OVDE DA SE UBACI DA DODA TEREN KOJI TREBA!!!!!");
				activeTerrain = transform.Find("TerrainBossBridge");
				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
				activeTerrain.position = snapObject.position;
				snapObject = activeTerrain.Find("SnapObject");
				activeTerrainProperties.isAvailable = false;
				MoveBg.hasBridge = true;
			}
			else if((activeTerrainProperties.TerrainType == 1 || activeTerrainProperties.TerrainType == 2) && currentBossAvailableTerrainType == 0)
			{
				//Debug.Log("I OVDE ISTO!!!!!");
			}
			break;
		}
		
		
		//		for(int i=0;i<2;i++)
		//		{
		//			availableTerrainIndexes.Clear();
		//
		//			for(int ii=0;ii<transform.childCount;ii++)
		//			{
		//				newTerrainProperties = transform.GetChild(ii).GetComponent<MoveBg>();
		//				if(newTerrainProperties.minimumStage <= currentStage && newTerrainProperties.isAvailable)
		//				{
		//
		//					for(int q=0; q<Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes.Length;q++)
		//					{
		//						Debug.Log("AVAJLIBLE TERAJN TUPE: " + Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes[q] + "TERAJIN TUPE: " + newTerrainProperties.TerrainType);
		//						if(((int)(Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes[q])).Equals((int)newTerrainProperties.TerrainType))
		//						{
		//							Debug.Log("IDEMO BRE!!!!!");
		//							availableTerrainIndexes.Add(ii);
		//						}
		//					}
		//					Debug.Log("============================================================");
		//					//if(System.Array.IndexOf((int[])Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes, (int)newTerrainProperties.TerrainType) > -1 || Camera.main.transform.Find("BossTepach").GetComponent<Boss>().AvailableTerrainTypes.Length == 0)
		//					//	availableTerrainIndexes.Add(ii);
		//				}
		//			}
		//
		//			int randomTerrain = Random.Range(0,availableTerrainIndexes.Count);
		//			Debug.Log("AVAILAHEL TERERAIN INDEXES COUNT: " + availableTerrainIndexes.Count + ", index izvucen: " + randomTerrain);
		//			activeTerrain = transform.GetChild(availableTerrainIndexes[randomTerrain]);
		//			activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
		//			activeTerrain.position = snapObject.position;
		//			snapObject = activeTerrain.Find("SnapObject");
		//			activeTerrainProperties.isAvailable = false;
		//			Debug.Log("IZVUKOJE: " + randomTerrain + ", dok je teren recimo: " + transform.GetChild(availableTerrainIndexes[randomTerrain]));
		//			availableTerrainIndexes.RemoveAt(randomTerrain);
		//		}
	}
	
	public void DestroyTerrains()
	{
		StartCoroutine(DestroyTerrainCoroutine());
	}
	
	IEnumerator DestroyTerrainCoroutine()
	{
		yield return new WaitForSeconds(2f);
		Tut_PlaneManager.Instance.guiCamera.transform.Find("LoadingHolder 1").GetChild(0).GetComponent<Animation>().Play("LoadingArrival2New");
		yield return new WaitForSeconds(2f);
		
		currentStage++;
		
		if(levels == null)
		{
			minimalLevel = 2;
			levels = new List<int>();
			for(int i=minimalLevel;i<=7;i++)
			{
				levels.Add(i);
			}
		}
		else 
		{
			if(levels.Count==0)
			{
				minimalLevel = 1;
				for(int i=minimalLevel;i<=7;i++)
				{
					if(i != Application.loadedLevel)
						levels.Add(i);
				}
			}
		}
		
		//int randomStage = Random.Range(minimalLevel,levels.Count+1);
		int randomStage = Random.Range(0,levels.Count);
		//		while(Application.loadedLevel == randomStage)
		//		{
		//			randomStage = Random.Range(minimalLevel,levels.Count+1);
		//			Debug.Log("§§§§§§§§§§-- CURRENT STAGE: " + Application.loadedLevel + ", new level: " + randomStage + " --§§§§§§§§§§");
		//		}
		int levelToLoad = levels[randomStage];
		levels.RemoveAt(randomStage);
		
		//		string stageType = System.String.Empty;
		//		if(Application.loadedLevelName.Equals("Woods"))
		//			stageType = "Ice";
		//		else if(Application.loadedLevelName.Equals("Ice"))
		//			stageType = "Wasteland";
		//		else if(Application.loadedLevelName.Equals("Wasteland"))
		//			stageType = "Woods";
		
		terrainsPassed = 0;
		MoveBg.hasBridge = false;
		//		Application.LoadLevel(stageType);
		Application.LoadLevel(levelToLoad);
		//		int count = transform.childCount;
		//
		//		//for(int i=0;i<transform.childCount;i++)
		//		for(int i=transform.childCount-1;i>=0;i--)
		//		{
		//			//Transform obj = transform.GetChild(i);
		//			//obj.parent = null;
		//			Destroy(transform.GetChild(i).gameObject);
		//			yield return new WaitForSeconds(0.02f);
		//			//"PROBAJ LOOP UNAZAD SA DELAY-OM!!!!!"
		//		}
		//		yield return null;
		//		yield return new WaitForSeconds(3f);
		//		transform.DetachChildren();
		//		StartCoroutine(LoadNewTerrains());
	}
	
	IEnumerator LoadNewTerrains()
	{
		//"DA SE POPRAVI MAIN GUN SHOOTER I DA SE VIDI KAKO CE I STA DA PUCA KAD ON DODJE NA RED!!!!!"
		string stageType = System.String.Empty;
		if(Application.loadedLevelName.Equals("Woods"))
			stageType = "Ice";
		else if(Application.loadedLevelName.Equals("Ice"))
			stageType = "Wasteland";
		else if(Application.loadedLevelName.Equals("Wasteland"))
			stageType = "Woods";
		currentStage++;
		int pomeraj = -100;
		for(int i=1;i<17;i++)
		{
			GameObject terrain = Instantiate(Resources.Load("Terrain/"+stageType+"/TerrainPreset"+i)) as GameObject;
			yield return new WaitForSeconds(0.01f);
			terrain.transform.parent = this.transform; //VRATI
			terrain.name = terrain.name.Substring(0,terrain.name.Length-7);
			//yield return new WaitForSeconds(0.25f);
			terrain.transform.localPosition = new Vector3(pomeraj, terrain.transform.localPosition.y, terrain.transform.localPosition.z); //VRATI
			pomeraj -= 35;
			//			if(!terrain.GetComponent<MoveBg>().isAvailable)
			//			{
			//				Debug.Log("ULETEO OVDE");
			//				activeTerrain = terrain.transform;
			//				activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
			//				snapObject = activeTerrain.Find("SnapObject");
			//			}
		}
		yield return new WaitForSeconds(3f);
		//@@@@@@@@ IZBRISI
		//GameObject warpZone1 = GameObject.FindGameObjectWithTag("WarpZone");
		//Destroy(warpZone1);
		//"TRIGGER OBJEKAT U ENEMIES OBJEKTU U TERENIMA NEMA SKRIPTU, ZBOG NJEGA IZBACUJE WARNING!!!!!"
		//"DA SE PROBA GORE BEZ PAUZE INSTANTIATE!!!!!"
		//"DA SE VRATI PARENTOVANJE U TERRAIN POOL ILI DA SE ODRADI NAKNADNO!!!!!"
		
		int startTerrain = Random.Range(0,transform.childCount);
		activeTerrain = transform.GetChild(startTerrain);
		activeTerrain.position = new Vector3(0,-22.2f,0);
		activeTerrainProperties = activeTerrain.GetComponent<MoveBg>();
		activeTerrainProperties.isAvailable = false;
		snapObject = activeTerrain.Find("SnapObject");
		GameObject terrain1 = Instantiate(Resources.Load("Terrain/"+stageType+"/TerrainBossBridge")) as GameObject;
		terrain1.transform.parent = this.transform;
		terrain1.name = terrain1.name.Substring(0,terrain1.name.Length-7);
		terrain1 = Instantiate(Resources.Load("Terrain/"+stageType+"/TerrainBossBridge2")) as GameObject;
		terrain1.transform.parent = this.transform;
		terrain1.name = terrain1.name.Substring(0,terrain1.name.Length-7);
		RepositionTerrain();
		GameObject warpZone = GameObject.FindGameObjectWithTag("WarpZone");
		
		//warpZone.renderer.enabled = false;
		warpZone.transform.GetChild(0).GetComponent<Animation>().Play("CloudsGoAway_Animation");
		Destroy(warpZone,2f);
		//warpZone.GetComponent<OffsetTexture>().enabled = false;
		GameManager.Instance.speed = 30;
		terrainsPassed = 0;
		MoveBg.hasBridge = false;
		SummoningBoss();
		//"DRUGI BOSS NECE DA SE POJAVI!!!!!"
	}
	
	//	public void SummonBoss()
	//	{
	//		GameObject boss = Instantiate(Resources.Load("Boss/BossPlaneHOLDER"), new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+5, -18), Quaternion.identity) as GameObject;
	//		boss.transform.parent = Camera.main.transform;
	//	}
	
	public void SummonBoss()
	{
		//		GameObject.Find("BossTime").renderer.enabled = true;
		//		Invoke("HideBossText",1.5f);
		//		//Invoke("PositionBoss",2.2f);
		//		currentBoss.SetActive(true);
		//		currentBoss.SendMessage("AdjustSpeed",SendMessageOptions.DontRequireReceiver);
		SoundManager.Instance.Stop_GameplayMusic();
		//SoundManager.Instance.Play_BossTime();
		GameObject.Find("BossTime").transform.GetChild(0).GetComponent<Animation>().Play();
		Debug.Log("AHAHAHAHAHAHAHHABAFPID");
		if(Time.timeScale < 1)
		{
			Debug.Log("KOJI TI JE PROBLEM BRE?");
			Tut_PlaneManager.pressedAndHold = true;
			Interface.Instance.normalTime = true;
			Tut_PopUpHandler.Instance.ResumeGame();
			StartCoroutine(Tut_PlaneManager.Instance.NormalTime(0.02f));
		}
		Tut_PlaneManager.Instance.dontSlowTime = 1;
		Tut_PlaneManager.Instance.bossTimePart = true;
	}
	
	public void CallBoss()
	{
		currentBoss.SetActive(true);
		currentBoss.SendMessage("AdjustSpeed",SendMessageOptions.DontRequireReceiver);
	}
	
	void SummoningBoss()
	{
		if(currentStage % 3 == 1)//da se vrati na 1
		{
			currentBoss = Instantiate(Resources.Load("Boss/BossPlaneHOLDER"+currentBossPlaneLevel.ToString()), new Vector3(0, -50, -30), Quaternion.identity) as GameObject;
			currentBoss.SendMessage("GetAvailableTerrainType",SendMessageOptions.DontRequireReceiver);
			currentBoss.SetActive(false);
			if(currentBossPlaneLevel < 3)
				currentBossPlaneLevel++;
		}
		else if(currentStage % 3 == 2)
		{
			currentBoss = Instantiate(Resources.Load("Boss/BossShipHOLDER"+currentBossShipLevel.ToString()), new Vector3(0, -50, -30), Quaternion.identity) as GameObject;
			currentBoss.SendMessage("GetAvailableTerrainType",SendMessageOptions.DontRequireReceiver);
			currentBoss.SetActive(false);
			if(currentBossShipLevel < 3)
				currentBossShipLevel++;
		}
		else
		{
			currentBoss = Instantiate(Resources.Load("Boss/BossTankHOLDER"+currentBossTankLevel.ToString()), new Vector3(0, -50, -30), Quaternion.identity) as GameObject;
			currentBoss.SendMessage("GetAvailableTerrainType",SendMessageOptions.DontRequireReceiver);
			currentBoss.SetActive(false);
			if(currentBossTankLevel < 3)
				currentBossTankLevel++;
		}
	}
	
	void PositionBoss()
	{
		if(currentStage % 3 == 1)
		{
			currentBossCameraOffsetY = 7;
			currentBoss.transform.position = new Vector3(0, Camera.main.transform.position.y+currentBossCameraOffsetY, -22);//-18
			currentBoss.SendMessage("SetCameraOffsetY",SendMessageOptions.DontRequireReceiver);
		}
		else if(currentStage % 3 == 2)
		{
			currentBossCameraOffsetY = 7;
			currentBoss.transform.position = new Vector3(0, Camera.main.transform.position.y+currentBossCameraOffsetY, -21);
			currentBoss.SendMessage("SetCameraOffsetY",SendMessageOptions.DontRequireReceiver);
		}
		//currentBoss.transform.parent = Camera.main.transform;
		else if(currentStage % 3 == 0)
		{
			currentBossCameraOffsetY = 7;
			currentBoss.transform.position = new Vector3(0, Camera.main.transform.position.y+currentBossCameraOffsetY, -30);//-18
			currentBoss.SendMessage("SetCameraOffsetY",SendMessageOptions.DontRequireReceiver);
		}
		
	}
	
	void HideBossText()
	{
		GameObject.Find("BossTime").GetComponent<Renderer>().enabled = false;
	}
	
	public void StageClear()
	{
		SoundManager.Instance.Play_StageClear();
		int currentMaxStage;
		if(PlayerPrefs.HasKey("MaxStage"))
		{
			int cms = PlayerPrefs.GetInt("MaxStage");
			int ghe = PlayerPrefs.GetInt("ghE67+=as23")-5;
			int fsd = PlayerPrefs.GetInt("Fsdfs+=as23")-11;
			
			if(cms==ghe)
			{
				if(cms==fsd)
				{
					currentMaxStage = fsd;
				}
				else
				{
					currentMaxStage = 1;
				}
				
			}
			else
			{
				currentMaxStage = 1;
			}
		}
		else
		{
			currentMaxStage = 1;
		}
		
		switch(currentStage)
		{
		case 3: case 6: case 9: case 12: case 15:
			if(currentStage<currentMaxStage)
			{
				GameObject.Find("CheckpointHolder").SetActive(false);
			}
			
			break;
		default:
			GameObject.Find("CheckpointHolder").SetActive(false); //"OVDE JE IZBACIO NULL REFERENCE EXCEPTION!!!!!"
			break;
		}
		
		if(currentStage>currentMaxStage)
		{
			PlayerPrefs.SetInt("MaxStage",currentStage);
			PlayerPrefs.SetInt("ghE67+=as23",currentStage+5);
			PlayerPrefs.SetInt("Fsdfs+=as23",currentStage+11);
		}
		
		Tut_PandaPlane.Instance.AddScore(200*currentStage);
		string uzengije = (Tut_PandaPlane.stars+10) + "#" + (Tut_PandaPlane.highScore-20) + "#" + (Tut_PandaPlane.laserWeaponNumber+5) + "#" + (Tut_PandaPlane.teslaWeaponNumber+5) + "#" + (Tut_PandaPlane.bladesWeaponNumber+5) + "#" + (Tut_PandaPlane.bombWeaponNumber+5);
		PlayerPrefs.SetString("Uzengije",uzengije);
		PlayerPrefs.Save();
		GameObject.Find("StageText").GetComponent<Text>().text = "STAGE " + currentStage.ToString();
		GameObject.Find("StageClearHolder/AnimationHolder").GetComponent<Animation>().Play();
	}
}
