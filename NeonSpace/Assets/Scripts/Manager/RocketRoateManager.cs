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

    public void RotateRocket()
    {
        Vector3 mousePosition = UICamera.lastEventPosition ;
        // 원래는 m_transform.position으로 값 받아왔지만
        // 마우스가 탐지하는 좌표랑 해당 좌표의 괴리값이 있어서 그냥 고정값으로 넣음
        Vector3 objectPosition = new Vector3(530,128,0); 
        // Debug.Log("mouse position : " + mousePosition); 
        // Debug.Log("object position : " + objectPosition);
        float directionY = mousePosition.y - objectPosition.y;
        float directionX = mousePosition.x - objectPosition.x;
        // Debug.Log("X : " + directionX  + " Y : " + directionY);

        float rotateDegree =  Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;

		// Debug.Log("rotateDegree : "+ rotateDegree );
		m_transform.rotation = Quaternion.Euler (0f,0f, rotateDegree -90);
		// Debug.Log("Rocket Rotate  : " + m_transform.rotation);
    }
}
