using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class akeventtest : MonoBehaviour
{
    // Start is called before the first frame update
    public AK.Wwise.Event P1ShieldGet1_00_SFX;
    void Start()
    {
        P1ShieldGet1_00_SFX.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
