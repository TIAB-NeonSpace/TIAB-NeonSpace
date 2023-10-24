using UnityEngine;
using System.Collections;

public class GaChaWall : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D go)
    {
        GaChaManager.instance.GaChaGet(go.gameObject);
    }
}
