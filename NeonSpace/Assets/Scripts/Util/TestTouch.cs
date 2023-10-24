using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour {
    [SerializeField] UISprite ArrowImg;
    [SerializeField] float forceX;
    [SerializeField] LineRenderer LineR;
    [SerializeField] UISprite BallImg;
    [SerializeField] float BallTerm;
    float forceY, halfWidth;
    Vector3 pos;
    bool isPress;


    void OnPress(bool isBool)
    {
        isPress = isBool;
        LineR.enabled = isBool;
        BallImg.enabled = isBool;
        if (isBool)
        {
            pos = Input.mousePosition;
        }
        else
        {
            ArrowImg.transform.localEulerAngles = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (isPress)
        {
            halfWidth = Input.mousePosition.x;
            forceY = Input.mousePosition.y;
            forceX = Mathf.Clamp(((pos.x - halfWidth) * 0.2f), -75f, 75f);
            if (forceY > 0.6f) SetAngle(forceX);
            if (forceY < 0.5f) SetAngle(forceX);
            BallImg.transform.localPosition = new Vector3(LineR.GetPosition(2).y, LineR.GetPosition(2).x + BallTerm, 0);
            //BallImg.transform.localPosition = new Vector3(lineReflect.CheckReflectPos.y , lineReflect.CheckReflectPos.x + 130,0);//LineR.GetPosition(2);
        }
    }

    void SetAngle(float eulerZ)
    {
        ArrowImg.transform.localEulerAngles = new Vector3(0, 0, eulerZ);
    }
}
