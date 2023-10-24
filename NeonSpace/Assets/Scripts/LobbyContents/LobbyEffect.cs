using UnityEngine;
using System.Collections;

public class LobbyEffect : MonoBehaviour
{
    [SerializeField]
    GameObject[] effect;

    public void ShowEffect(int idx)
    {
        for (int i = 0; i < effect.Length; ++i)
        {
            effect[i].SetActive(false);
        }
        if (idx < 13) return;
      
        effect[idx - 13].SetActive(true);
    }
}
