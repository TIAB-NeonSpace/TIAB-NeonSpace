using UnityEngine;
using System.Collections;

public class GameContents : MonoBehaviour
{
    public static GameContents instance;

    [SerializeField]
    UILabel scoreCount; //, countDown = null;
    [SerializeField]
    UISprite[] item;
    public int  bally;
    void Awake()
    {
        instance = this;
    }

    public void gameitem(Vector2 pos, int idx)
    {
        item[idx].transform.position = new Vector2(pos.x, pos.y);
        StartCoroutine(gameitemoff(idx));
    }
    IEnumerator gameitemoff(int id)
    {
        yield return new WaitForSeconds(0.05f);
        item[id].gameObject.transform.localPosition = new Vector2(10000, 10000);
    }

    public void pingpongItem(GameObject cd) // 컬라이더를 인자값으로 넘기는 것은 비용이 비싸기 때문에 하지 않는다. 그래서 게임 오브젝트로 받았다.
    {
        int forcex = Random.Range(-60, 60);
        cd.transform.localEulerAngles = new Vector3(0, 0, forcex);
        cd.GetComponent<BallMove>().rd.velocity = cd.transform.up * BallManager.instance.force;
        cd.transform.localEulerAngles = Vector3.zero; // 축이 틀어져 있는 상태에서 하면 안되기 때문에 다시 바꿔준다.
    }
}

