using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mouledoux.Node;

public class ProceduralPrefabBuilder : MonoBehaviour, NodeComponent
{
    public float m_biomeValue;
    public float m_elevationValue;
    public float m_tempValue;
    public ProceduralPrefab m_proceduralPrefab;

    private Node node;

    static Texture2D tex;

    void Start()
    {
        Vector3 pos = transform.position;
        int size = 128;

        if(tex == null)
        {
            tex = Perlin.GeneratePerlinTexture("123123", size, size, 0, 0);
        }

        Color color = tex.GetPixel((int)(pos.x / size), (int)(pos.z / size));
        Color.RGBToHSV(color, out pos.x, out pos.y, out pos.z);

        m_biomeValue = pos.x;
        m_elevationValue = pos.y;
        m_tempValue = pos.z;
        
        BuildProInstance(pos.x, pos.y, pos.z);

        transform.localPosition += Vector3.up * pos.y;
    }

    public void BuildProInstance(float a_biome, float a_elevation, float a_temperature)
    {
        GameObject proInstance = m_proceduralPrefab.GetPrefab(a_biome, a_elevation, a_temperature);
        Instantiate(proInstance, transform);
    }

    public Node GetNode()
    {
        if(node == null)
        {
            node = new Node();
        }

        return node;
    }
}
