using UnityEngine;
using System.Collections;
using UnityEditor;


public class EnemyScriptEditor : Editor  {

	static int FireRateReference;
	static Enemy.EnemyType EnemyTypeReferenca;
	static Enemy.AirBasic AirBasicReferenca;
	static Enemy.AirIntermediate AirIntermediateReferenca;
	static Enemy.AirAdvanced AirAdvancedReferenca;
	static Enemy.GroundAndWaterStatic GroundAndWaterStaticReferenca;
	static Enemy.GroundAndWaterMobile GroundAndWaterMobileReferenca;

	

	[CustomEditor(typeof(Enemy))]
	public class LevelScriptEditor : Editor 
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update ();

			DrawDefaultInspector();

			Enemy myTarget = (Enemy)target;



			AirBasicReferenca = myTarget.AirBasicReference;
			AirIntermediateReferenca = myTarget.AirIntermediateReference;
			AirAdvancedReferenca = myTarget.AirAdvancedReference;
			GroundAndWaterStaticReferenca = myTarget.GroundAndWaterStaticReference;
			GroundAndWaterMobileReferenca = myTarget.GroundAndWaterMobileReference;
			EnemyTypeReferenca=myTarget.TypeOfEnemy;
//			FireRateReference=myTarget.fireRate;


			if(EnemyTypeReferenca==Enemy.EnemyType.AirBasic)
			{
				myTarget.HasGun=false;
				AirBasicReferenca = (Enemy.AirBasic)EditorGUILayout.EnumPopup("Choose Type Of Air Enemy", myTarget.AirBasicReference);
				myTarget.AirBasicReference=AirBasicReferenca;
				if(myTarget.HasGun)
				{
					myTarget.fireRate = EditorGUILayout.FloatField("Fire Rate", myTarget.fireRate);
				}
				if(Enemy.AirBasic.AirBasic1StraighForward==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 1 Straigh Forward Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic2UpperLeftLowerRight==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 2 Upper Left Lower Right Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic3UpperRightLowerLeft==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 3 Upper Right Lower Left Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic4Upper180RightToLeft==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 4 Upper 180 Right To Left Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic5LeftMiddleRightMiddlePlusOffset==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 5 Left Middle Right Middle Plus Offset Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic6RightMiddleLeftMiddlePlusOffset==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 6 Right Middle Left Middle Plus Offset Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic7MiddleLeftToRightWithJump==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 7 Middle Left To Right With Jump Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic8LeftToRightMiddleDown==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 8 Left To Right Middle Down Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic9TwoEnemiesMiddleThenLeftRight==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 9 Two Enemies Middle Then Left Right Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic10FromRightAndLeftToMiddle==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 10 From Right And Left To Middle Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic11UpperLeftMiddleRightTwoWaves==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 11 Upper Left Middle Right Two Waves Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic12MiddleToRightTurnAndBackStraightLine==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 12 Middle To Right Turn And Back Straight Line Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic13CikCak==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 13 Cik-Cak Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic14TwoWavesStraightForwardAndLeftToRight==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 14 Two Waves Straight Forward And Left To Right Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic15TwoWavesStraightForwardLeftAndRight==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 15 Two Waves Straight Forward Left And Right Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic16Loop==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 16 Loop Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic17Fireballs==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 17 Fireballs Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic18AirMines==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 18 Air Mines Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic19Rockets==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 19 Rockets Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic20JetPackOne==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 20 One Jet Pack Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic21JetPackTwo==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 21 Two Jet Pack Chosen!", MessageType.Info);
				}
				else if(Enemy.AirBasic.AirBasic22Tesla==myTarget.AirBasicReference)
				{
					EditorGUILayout.HelpBox("AirBasic Type - 22 Tesla Chosen!", MessageType.Info);
				}

			}
			else if(myTarget.TypeOfEnemy==Enemy.EnemyType.AirIntermediate)
			{
				AirIntermediateReferenca = (Enemy.AirIntermediate)EditorGUILayout.EnumPopup("Choose Type Of AirWithGun Enemy", myTarget.AirIntermediateReference);
				myTarget.AirIntermediateReference = AirIntermediateReferenca;
				if(myTarget.HasGun)
				{
					myTarget.fireRate = EditorGUILayout.FloatField("Fire Rate", myTarget.fireRate);
				}
				if(Enemy.AirIntermediate.AirIntermediate1Fireballs==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 1 Fireballs Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate2AirMines==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 2 Air Mines Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate3AirLavirint==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 3 Air Lavirint Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate4GroundThunder==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 4 Ground Thunder Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate5DumbSentinels==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 5 Dumb Sentinels Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate6OneAndFire==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 6 Two And Fire Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate7TwoAndFire==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 7 Two And Fire Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate8BottomLeftThenBottomRight==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 8 Bottom Left Then Bottom Right Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate9AirplaneBombKamikaza==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 9 Airplane Bomb Kamikaza Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate10CikCakWithPauseAndFire==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 10 Cik-Cak With Pause And Fire Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate11Rockets==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 11 Rocket Fire Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate12UpperRightToMiddleWithFire==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 12 Upper Right To Middle With Fire Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate13Helicopter==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 13 Intermediate Helicopter Chosen!", MessageType.Info);
				}

				else if(Enemy.AirIntermediate.AirIntermediate14RightUpToCenterPauseFireToLeftDown==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 14 Intermediate Right Up To Center Pause Fire To Left Down Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate15JetPackOne==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 15 Intermediate One Jet Pack Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate16JetPackTwo==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 16 Intermediate Two Jet Pack Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate17Kamikaza==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 17 Intermediate Kamikaza Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate18CountdownMine==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 18 Intermediate Countdown Mine Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate19Tesla==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 19 Intermediate Tesla Chosen!", MessageType.Info);
				}
				else if(Enemy.AirIntermediate.AirIntermediate20MaceGoblin==myTarget.AirIntermediateReference)
				{
					EditorGUILayout.HelpBox("AirIntermediate Type - 20 Intermediate Mace Goblin Chosen!", MessageType.Info);
				}

			}
			else if(myTarget.TypeOfEnemy==Enemy.EnemyType.AirAdvanced)
			{
				AirAdvancedReferenca = (Enemy.AirAdvanced)EditorGUILayout.EnumPopup("Choose Type Of GroundMobile Enemy", myTarget.AirAdvancedReference);
				myTarget.AirAdvancedReference = AirAdvancedReferenca;
				if(myTarget.HasGun)
				{
					myTarget.fireRate = EditorGUILayout.FloatField("Fire Rate", myTarget.fireRate);
				}
				if(Enemy.AirAdvanced.AirAdvanced1Tesla==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 1 Tesla Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced2OneToMany==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 2 One To Many Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced3WithDestructableObjectShield==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 3 Destructable Object Shield Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced4WithBlades==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 4 With Blades Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced5FireWithRotation==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 5 Fire With Rotation Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced6FireForwardAllDirections==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 6 Fire Forward All Directions Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced7AirplaneWithLaser==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 7 Airplane With Laser Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced8MaceGoblin==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 8 Airplane Mace Goblin Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced9AirMines==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 9 Airplane Air Mines Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced10Kamikaza==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 10 Airplane With Laser Chosen!", MessageType.Info);
				}
				else if(Enemy.AirAdvanced.AirAdvanced11CountdownMine==myTarget.AirAdvancedReference)
				{
					EditorGUILayout.HelpBox("AirAdvanced Type - 11 Airplane Countdown Mine Chosen!", MessageType.Info);
				}


			}
			else if(myTarget.TypeOfEnemy==Enemy.EnemyType.GroundAndWaterStatic)
			{
				GroundAndWaterStaticReferenca = (Enemy.GroundAndWaterStatic)EditorGUILayout.EnumPopup("Choose Type Of GroundStatic Enemy", myTarget.GroundAndWaterStaticReference);
				myTarget.GroundAndWaterStaticReference = GroundAndWaterStaticReferenca;
				if(myTarget.HasGun)
				{
					myTarget.fireRate = EditorGUILayout.FloatField("Fire Rate", myTarget.fireRate);
				}
				if(Enemy.GroundAndWaterStatic.TankStatic==myTarget.GroundAndWaterStaticReference)
				{
					EditorGUILayout.HelpBox("GroundAndWaterStatic Type - TankStatic Chosen!", MessageType.Info);
				}
				else if(Enemy.GroundAndWaterStatic.Turret1==myTarget.GroundAndWaterStaticReference)
				{
					EditorGUILayout.HelpBox("GroundAndWaterStatic Type - Turret1 Chosen!", MessageType.Info);
				}
				else if(Enemy.GroundAndWaterStatic.Turret2==myTarget.GroundAndWaterStaticReference)
				{
					EditorGUILayout.HelpBox("GroundAndWaterStatic Type - Turret2 Chosen!", MessageType.Info);
				}
			}
			else if(myTarget.TypeOfEnemy==Enemy.EnemyType.GroundAndWaterMobile)
			{
				GroundAndWaterMobileReferenca = (Enemy.GroundAndWaterMobile)EditorGUILayout.EnumPopup("Choose Type Of MobileObstacle Enemy", myTarget.GroundAndWaterMobileReference);
				myTarget.GroundAndWaterMobileReference=GroundAndWaterMobileReferenca;
				if(myTarget.HasGun)
				{
					myTarget.fireRate = EditorGUILayout.FloatField("Fire Rate", myTarget.fireRate);
				}
				if(Enemy.GroundAndWaterMobile.TankMobile==myTarget.GroundAndWaterMobileReference)
				{
					EditorGUILayout.HelpBox("GroundAndWaterMobile Type - TankMobile Chosen!", MessageType.Info);
				}
				else if(Enemy.GroundAndWaterMobile.ShipMobile==myTarget.GroundAndWaterMobileReference)
				{
					EditorGUILayout.HelpBox("GroundAndWaterMobile Type - ShipMobile Chosen!", MessageType.Info);
				}
			}




			EditorUtility.SetDirty(target);
		}

	}
}
