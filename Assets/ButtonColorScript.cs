using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorScript : MonoBehaviour
{
    Color buttonColor = Color.white;

    Image buttonImage;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        buttonColor = UniverseToggle.universe_toggle ? Color.black : Color.white;
        buttonImage.color = buttonColor;      
    }
}
