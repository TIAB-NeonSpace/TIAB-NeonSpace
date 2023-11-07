using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBrickCollider : MonoBehaviour
{
    public Brick Shieldbrick_;
    void Start()
    {
        Shieldbrick_ = transform.parent.parent.GetComponent<Brick>();
    }

    void OnCollisionEnter2D(Collision2D cd)
    {
        if (cd.gameObject.CompareTag("Ball"))
            Shieldbrick_.SetCollider(cd.gameObject);
    }

}
