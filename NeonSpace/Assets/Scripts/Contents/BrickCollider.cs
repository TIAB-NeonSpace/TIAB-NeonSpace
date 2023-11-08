using UnityEngine;
using System.Collections;

public class BrickCollider : MonoBehaviour
{
    public Brick brick_;
	void Start ()
    {
        brick_ = transform.parent.GetComponent<Brick>();
	}

    void OnCollisionEnter2D(Collision2D cd)
    {
        brick_.SetCollider(cd.gameObject);
    }
}
