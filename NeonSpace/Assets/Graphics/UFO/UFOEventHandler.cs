using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOEventHandler : MonoBehaviour
{
    Animator animator;
    [SerializeField] UFOItem ufoItem;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UFOAppear()
    {
        Debug.Log("ufo appear");
        ufoItem.UFOAttack();
        animator.SetTrigger("End");
    }

    public void UFODisppear()
    {
        ufoItem.UFOEnd();
    }
}
