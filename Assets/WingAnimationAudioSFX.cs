using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event WingFlap_00_SFX;
    
    // Use this for initialization.
    public void PlayWingSound()
    {
        WingFlap_00_SFX.Post(gameObject);
    }
}