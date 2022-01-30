using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mouledoux.Node;

public class ProceduralPrefabBuilder : MonoBehaviour
{
    public float m_biomeValue;
    public float m_elevationValue;
    public float m_tempValue;
    public GameObject m_proceduralPrefab;


    static Texture2D tex;

    void Start()
    {
        Vector3 pos = transform.position;
        float size = 256f;

        if(tex == null)
        {
            tex = Perlin.GeneratePerlinTexture("123123", (int)size, (int)size, 0, 0, 10f, 20f, 10f);
        }

        int x = (int)((pos.x % size) / size * size);
        int y = (int)((pos.z % size) / size * size);

        Color color = tex.GetPixel(x, y);
        Color.RGBToHSV(color, out pos.x, out pos.y, out pos.z);

        m_biomeValue = pos.x;
        m_elevationValue = pos.y;
        m_tempValue = pos.z;

        BuildProInstance(pos.x, pos.z, pos.y);
    }

    public void BuildProInstance(float a_biome, float a_elevation, float a_temperature)
    {
        GameObject proInstance = m_proceduralPrefab;
        proInstance = Instantiate(proInstance, transform);

        proInstance.transform.localScale = Vector3.one + (Vector3.up * a_elevation) * 8f;
    }
}
