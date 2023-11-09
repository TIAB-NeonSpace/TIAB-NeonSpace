using UnityEngine;
using System.Collections;


public class BrickCollider : MonoBehaviour
{
    public Brick brick_;
    public EnumBase.Special_Brick special_Brick;

    private BallMove ball;

    void Start()
    {
        if (special_Brick == EnumBase.Special_Brick.sheld)
            brick_ = transform.parent.parent.GetComponent<Brick>();
        else
            brick_ = transform.parent.GetComponent<Brick>();
    }

    void OnCollisionEnter2D(Collision2D cd)
    {
        if (!cd.gameObject.CompareTag("Ball"))
            return;

        ball = cd.gameObject.GetComponent<BallMove>();
        if (special_Brick == EnumBase.Special_Brick.reflect && !ball.isReflect)
            return;
        if (special_Brick == EnumBase.Special_Brick.direct && ball.isReflect)
            return;

        ball.isReflect = true;
        brick_.SetCollider(cd.gameObject);
    }
}
