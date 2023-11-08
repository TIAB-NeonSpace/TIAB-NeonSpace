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
        // Vector3 objectPosition = new Vector3(438,78,0); 
        Vector3 objectPosition = new Vector3(368,67,0); 
        // Debug.Log("mouse position : " + mousePosition); 
        // Debug.Log("object position : " + objectPosition);
        float directionY = mousePosition.y - objectPosition.y;
        float directionX = mousePosition.x - objectPosition.x;
        // Debug.Log("X : " + directionX  + " Y : " + directionY);

        float rotateDegree =  Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;
        if (rotateDegree < 1) return ; // 각도가 작으면 걍 움직이지마
		//Debug.Log("rotateDegree : "+ rotateDegree );
        // m_transform.transform.LookAt(new Vector3(0,0,rotateDegree));
		m_transform.rotation = Quaternion.Euler (0f,0f, rotateDegree-90);
		// Debug.Log("Rocket Rotate  : " + m_transform.rotation);
    }
}
