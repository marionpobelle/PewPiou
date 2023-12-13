using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Awake()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        Destroy(gameObject, 0.2f);
    }

}
