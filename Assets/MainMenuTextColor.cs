using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuTextColor : MonoBehaviour
{
    TextMeshProUGUI m_textMesh;
    Color current_face_color = Color.red;
    Color current_underlay_color = Color.blue;

    // Start is called before the first frame update
    void Start()
    {
        m_textMesh = this.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        current_face_color = UniverseToggle.universe_toggle ? Color.blue : Color.red;
        current_underlay_color = UniverseToggle.universe_toggle ? Color.red : Color.blue;
        m_textMesh.fontSharedMaterial.SetColor(ShaderUtilities.ID_FaceColor, current_face_color);
        m_textMesh.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, current_underlay_color);
    }
}
