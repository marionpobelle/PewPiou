using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event WingFlap_00_SFX;
    
    
    public void PlayWingSound()
    {
        WingFlap_00_SFX.Post(gameObject);
    }
}