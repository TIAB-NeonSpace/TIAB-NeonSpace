using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//여기서는 업그레이 비용 및 업그레이 요소 그리고 상단의 비용을 관장한다.
public class GroupContents : MonoBehaviour 
{
	[SerializeField] UILabel[] showUpgradeLabels;
	[SerializeField] UILabel[] upgradeCostLabels;
	[SerializeField] UISprite ballSprite;

    [SerializeField] CurrentShipShow planeChange;

	//밑에 함수는 애니메이션에서 호출한다.
	public void SetAnim() 
	{
		LobbyController.instance.SetLabel(EnumBase.UIState.Coin , string.Format("{0:N0}", DataManager.instance.GetCoin()));

		if(DataManager.instance.FirePower >= 100)
		{
			showUpgradeLabels[1].text = "MAX";
			upgradeCostLabels[1].text = "MAX";
		}
		else
		{
			showUpgradeLabels[1].text = string.Format("{0:N0}", DataManager.instance.FirePower);
			upgradeCostLabels[1].text = string.Format("{0:N0}", DataManager.instance.ReturenFireBallCoin);
		}
		if(DataManager.instance.StartBall >= 100)
		{
			showUpgradeLabels[0].text = "MAX";
			upgradeCostLabels[0].text = "MAX";
		}
		else
		{
			showUpgradeLabels[0].text = string.Format("{0:N0}", DataManager.instance.StartBall);		
			upgradeCostLabels[0].text = string.Format("{0:N0}", DataManager.instance.ReturenStartBallCoin);
		}
		ballSprite.spriteName = DataManager.instance.GetBall();
	}

	//민진 - 공 업그레이드 버튼 클릭시 실행
	public void UpStartBall()
	{
		//민진 - 만약 공의 업그레이드가 최대라면, 종료
		if(DataManager.instance.StartBall >= 100) return;

		//민진 - 만약 소지 코인의 수가 업그레이드에 필요한 코인 수보다 많거나 같다면, 업그레이드 실행
		if(DataManager.instance.GetCoin() >=  DataManager.instance.ReturenStartBallCoin)
		{
			DataManager.instance.SetCoin(-DataManager.instance.ReturenStartBallCoin);	//코인 차감
			DataManager.instance.StartBall += 1;
			DataManager.instance.CurrentCoin += DataManager.instance.ReturenStartBallCurCoin;
				//시작 공 갯수 증가
			
/*			//!!공 스프라이트 변경 코드
			if(DataManager.instance.StartBall % 10 == 1)	//1의 자리가 1일때 공의 스프라이트 정수값을 1 증가 시킴
			{
				DataManager.instance.BallSprite += 1;
				ballSprite.spriteName = DataManager.instance.GetBall();
			}*/

			//공의 업그레이드가 100이라면, 텍스트 변경
			if(DataManager.instance.StartBall >= 100)
			{
				showUpgradeLabels[0].text = "MAX";
				upgradeCostLabels[0].text = "MAX";
			}
			//아니라면, 해당하는 업그레이드 숫자/금액으로 텍스트 변경
			else
			{
				showUpgradeLabels[0].text = string.Format("{0:N0}", DataManager.instance.StartBall);
				upgradeCostLabels[0].text = string.Format("{0:N0}", DataManager.instance.ReturenStartBallCoin);
			}
			
			LobbyController.instance.SetLabel(EnumBase.UIState.Coin , string.Format("{0:N0}", DataManager.instance.GetCoin()));
		}
		//코인이 부족하다면, 코인 충전 팝업 on
		else
		{
			LobbyController.instance.SetTween(EnumBase.UIState.ChargeMoney , true);
		}
	}

	//민진 - 화력 업그레이드 버튼 클릭 시 실행
	public void UpFirePower()
	{
		if(DataManager.instance.FirePower >= 100) return;
		if(DataManager.instance.GetCoin() >=  DataManager.instance.ReturenFireBallCoin)
		{
			DataManager.instance.SetCoin(-DataManager.instance.ReturenFireBallCoin);
			DataManager.instance.FirePower += 1;
			DataManager.instance.CurrentCoin += DataManager.instance.ReturenFireBallCurCoin;

/*			//!!---우주선 스프라이트 변경 코드---!!
			if(DataManager.instance.FirePower % 10 == 1)
			{
			
				DataManager.instance.PlaneSprite += 1;
				planeChange.ChangeShip();
			}*/

			if(DataManager.instance.FirePower >= 100)
			{
				showUpgradeLabels[1].text = "MAX";
				upgradeCostLabels[1].text = "MAX";
			}
			else
			{
				showUpgradeLabels[1].text = string.Format("{0:N0}", DataManager.instance.FirePower);
				upgradeCostLabels[1].text = string.Format("{0:N0}", DataManager.instance.ReturenFireBallCoin);
			}
		
			LobbyController.instance.SetLabel(EnumBase.UIState.Coin , string.Format("{0:N0}", DataManager.instance.GetCoin()));
		}
		else
		{
			LobbyController.instance.SetTween(EnumBase.UIState.ChargeMoney , true);
		}
	}

	public void ResetUpgrade()
	{
		planeChange.ChangeShip();
		ballSprite.spriteName = DataManager.instance.GetBall();

		DataManager.instance.PlaneSprite = 0;
		DataManager.instance.BallSprite = 0;
		DataManager.instance.StartBall = 1;
		DataManager.instance.FirePower = 1;

		DataManager.instance.SetCoin(+DataManager.instance.CurrentCoin);
		DataManager.instance.CurrentCoin = 0;
		SetAnim();
	}


}
