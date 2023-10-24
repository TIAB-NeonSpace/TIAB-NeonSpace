using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScripts : MonoBehaviour 
{
	Animator anim_;

	private void Start() 
	{
		anim_ = GetComponent<Animator>();
		if(!DondestroyManager.instance.isFirst) 
		{
			DondestroyManager.instance.isFirst = true;
			anim_.SetTrigger("On");
		}
		else
		{
			LobbyController.instance.rocketBundle.SetActive(true);
		}
	}
	public void SetEndAnim()
	{
		LobbyController.instance.rocketBundle.SetActive(true);
		 if(!PlayerPrefsElite.GetBoolean("ShowTutorial"))
        {
            LobbyController.instance.SetTween(EnumBase.UIState.Tutorial , true);
            PlayerPrefsElite.SetBoolean("ShowTutorial" , true);
        }
	}
}
