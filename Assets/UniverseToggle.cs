using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool universe_toggle = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            universe_toggle = !universe_toggle;
        }
        
    }
}
