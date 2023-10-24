using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DondestroyManager : MonoBehaviour
{
	public bool isFirst;
	public static DondestroyManager instance;
	private void Awake() {
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		} 
		else
		{
			Destroy(gameObject);
		}
		
	}
}
