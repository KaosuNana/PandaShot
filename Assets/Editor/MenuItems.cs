using UnityEngine;
using UnityEditor;

public class MenuItems
{
	[MenuItem("ClearPlayerPrefs/Clear PlayerPrefs %p")]
	private static void NewMenuOption()
	{
		if(EditorUtility.DisplayDialog("Delete PlayerPrefs?", "Are you sure you want to delete PlayerPrefs? This action cannot be undone.", "Ok", "No"))
		{
			PlayerPrefs.DeleteAll();
			if(EditorUtility.DisplayDialog("PlayerPrefs deleted", "Successful", "Ok"))
			{

			}
		}

	
	}
}
