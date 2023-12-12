using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField]Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim.SetTrigger("bulletIsHittingEnemy");
        Destroy(gameObject, 2.0f);
    }

}
