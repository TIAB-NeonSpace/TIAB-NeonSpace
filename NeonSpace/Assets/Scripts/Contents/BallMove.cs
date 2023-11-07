using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour
{
    public Vector3 ball_Pos_;
    int ballIdx;
    public Rigidbody2D rd;
    [SerializeField] UISprite sprite_ = null;
    [SerializeField] TweenPosition t_pos;
    public bool isMove;
    public bool isReflect;

    public Collider2D collider;


    void Start()
    {
        BallManager.instance.ballList.Add(this);
        collider.enabled = false;
    }

    public void SetMove(float forceX, float force)
    {
        collider.enabled = true;
        isReflect = false;
        isMove = true;
        sprite_.enabled = true;
        sprite_.spriteName = DataManager.instance.GetBall();
        rd.isKinematic = false;
        transform.localEulerAngles = new Vector3(0, 0, forceX);
        rd.velocity = transform.up * force;
        transform.localEulerAngles = Vector2.zero;
        ball_Pos_ = transform.position;
        //ShowEffect(ballIdx);
    }

    void SetBall() // 볼 스프라이트 체인지, 나중에 볼 바꿔주는 곳에서 호출
    {
        //BallManager.instance.ballHit = 2;
    }

    public void SetResetState()
    {

    }

    public void FalseSprite()
    {
        sprite_.enabled = false;
    }

    public void StopBall(bool isShow = false)
    {
        isMove = false;
        rd.velocity = Vector2.zero;
        rd.isKinematic = true;
        float forceX = Mathf.Clamp(transform.localPosition.x, -300f, 300f);
        t_pos.from.x = forceX;
        if (isShow) t_pos.from.y = transform.localPosition.y;
        else t_pos.from.y = -393;
        t_pos.ResetToBeginning();
        t_pos.PlayForward();
        collider.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        gameObject.layer = 8;
        if (coll.gameObject.CompareTag("Wall"))
            isReflect = true;
    }
}