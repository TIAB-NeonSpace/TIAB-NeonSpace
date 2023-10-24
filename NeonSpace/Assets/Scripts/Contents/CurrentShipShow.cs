using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentShipShow : MonoBehaviour 
{
	[SerializeField] GameObject[] shipBundle;

	private void OnEnable() 
	{
		for(int i = 0; i < shipBundle.Length; ++i)
		{
			shipBundle[i].SetActive(false);
		}
		shipBundle[DataManager.instance.PlaneSprite].SetActive(true);
	}

	public void ChangeShip() 
	{
		for(int i = 0; i < shipBundle.Length; ++i)
		{
			shipBundle[i].SetActive(false);
		}
		shipBundle[DataManager.instance.PlaneSprite].SetActive(true);
	}
}
