using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuitText : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
    }

    public void PointerEnter()
    {
        textMesh.text = "Quit?";
    }

    public void PointerExit()
    {
        textMesh.text = "Quit";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
