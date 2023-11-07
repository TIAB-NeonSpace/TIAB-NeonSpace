using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RocketRoateManager : MonoBehaviour
{
    private Transform m_transform;

    public static RocketRoateManager instance ; 
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_transform = this.transform;
    }

/// <summary>
/// Using tan^-1 function with mousePosition and objectPosition
/// </summary> 
    public void RotateRocket()
    {
        Vector3 mousePosition = UICamera.lastEventPosition ;
        // 원래는 m_transform.position으로 값 받아왔지만
        // 마우스가 탐지하는 좌표랑 해당 좌표의 괴리값이 있어서 그냥 고정값으로 넣음
        // Vector3 objectPosition = new Vector3(438,78,0); //1차 
        // Vector3 objectPosition = new Vector3(623,106,0); //2차 왜 출발값이 바뀌는거지 
        Vector3 objectPosition = new Vector3(365,78,0); //3차 왜 출발값이 바뀌는거지 
        // 공의 출발위치 = oobjectPosition.y + 43;
        float directionY = mousePosition.y - objectPosition.y;
        float directionX = mousePosition.x - objectPosition.x;
        
        // if (mousePosition.y < 200) return; // 일정 방향이상으로 안돌리게하기  근데 이게 y값만으로 생각하는거라 그냥 각도로 보는게 편함

        float rotateDegree =  Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;
        if (rotateDegree < 1) return ; // 각도가 작으면 걍 움직이지마

        // m_transform.transform.LookAt(new Vector3(0,0,rotateDegree));
        // Debug.Log ("Euler : " + Quaternion.Euler (0f,0f, rotateDegree-90));
		m_transform.rotation = Quaternion.Euler (0f,0f, rotateDegree-90);
		

        //Debug
        // Debug.Log("mouse position : " + mousePosition); 
        // Debug.Log("object position : " + objectPosition);
        // Debug.Log("X : " + directionX  + " Y : " + directionY);
        // Debug.Log("Rocket Rotate  : " + m_transform.rotation);

        
    }
}
