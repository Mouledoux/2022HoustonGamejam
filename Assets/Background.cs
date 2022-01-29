using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    private Color current_color = Color.black;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        current_color = UniverseToggle.universe_toggle ? Color.white : Color.black;
        this.GetComponent<Image>().color = current_color;
    }
}
