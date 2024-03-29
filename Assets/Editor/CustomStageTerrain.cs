using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MoveBg))]
public class CustomStageTerrain : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		MoveBg terrain = (MoveBg)target;

//		if(terrain.stageSelected == MoveBg.stage.woods)
//		{
//			EditorGUILayout.HelpBox("Air Type - FirstAir Chosen!", MessageType.Info);
//		}
		GUIStyle style = GUI.skin.GetStyle("HelpBox");
		style.fontSize = 15;

		switch(terrain.stageSelected)
		{
		case MoveBg.stage.woods: EditorGUILayout.HelpBox("Legend:\n0 - Only Ground ---------- (available: 0, 1, 5)\n" +
																	"1 - Ground To Water ------- (available: 2, 3, 4)\n" +
																	"2 - Only Water ----------- (available: 2, 3, 4)\n" +
			                                                 		"3 - Water To Ground ------- (available: 0, 1, 5)\n" +
			                                                 		"4 - Water With Island ----- (available: 2, 3, 4)\n" +
			                                                 		"5 - Ground With Stuff ----- (available: 0, 1, 5)", MessageType.None); break;

		case MoveBg.stage.darkWoods: EditorGUILayout.HelpBox("Legend:\n0 - Only Ground ---------- (available: 0, 1)\n" +
			                                                 "1 - Ground To Water ------- (available: 2, 3, 4)\n" +
			                                                 "2 - Only Water ----------- (available: 2, 3, 4)\n" +
			                                                 "3 - Water To Ground ------- (available: 0, 1)\n" +
			                                                 "4 - Water With Island ----- (available: 2, 3, 4)", MessageType.None); break;

		case MoveBg.stage.ice: EditorGUILayout.HelpBox("0 - Only Ground\n1 - Ground To Ice\n2 - Only Ice\n3 - Ice To Ground", MessageType.None); break;
		case MoveBg.stage.jungle: EditorGUILayout.HelpBox("0 - Only Ground\n1 - Ground To Trees\n2 - Only Trees\n3 - Trees To Ground", MessageType.None); break;
		case MoveBg.stage.lava: EditorGUILayout.HelpBox("0 - Only Ground\n1 - Ground To Lava\n2 - Only Lava\n3 - Lava To Ground", MessageType.None); break;
		case MoveBg.stage.sea: EditorGUILayout.HelpBox("0 - Only Sea\n1 - Sea To Island\n2 - Only Island\n3 - Island To Sea", MessageType.None); break;
		case MoveBg.stage.tropic: EditorGUILayout.HelpBox("0 - Only Ground\n1 - Ground To Woods\n2 - Only Woods\n3 - Woods To Ground", MessageType.None); break;
		case MoveBg.stage.wasteland: EditorGUILayout.HelpBox("0 - Only Ground\n1 - Ground To Mount\n2 - Only Mount\n3 - Mount To Ground", MessageType.None); break;
		}
	}
}
