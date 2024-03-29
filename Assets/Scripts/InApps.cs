using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InApps : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	IEnumerator MoneyCounter(int kolicina)
	{
		Text moneyText = GameObject.Find("StarsNumberInAppText").GetComponent<Text>();
		int current = int.Parse(moneyText.text);
		int suma = current + kolicina;
		int korak = (suma - current)/10;
		ShopNotificationClass.numberNotification = GameObject.Find("Canvas").transform.Find("ShopMenu").GetComponent<Shop>().ShopNotification();
		
		while(current != suma)
		{
			current += korak;
			moneyText.text = current.ToString();
			yield return new WaitForSeconds(0.07f);
		}
		moneyText.text = Shop.stars.ToString();
	}
	
}
