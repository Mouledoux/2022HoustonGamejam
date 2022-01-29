using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mouledoux.Node;

public class ProceduralPrefabBuilder : MonoBehaviour, ITraversable, NodeComponent
{
    public ProceduralPrefab m_proceduralPrefab;

    private Node node;

    public ITraversable origin { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float[] coordinates { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float fVal => throw new System.NotImplementedException();

    public float gVal { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float hVal { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float[] pathingValues { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool isOccupied { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool isTraversable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        node.AddInformation(m_proceduralPrefab);
        Vector3 perlinVals = GetNode().GetInformation<Vector3>()[0];

        BuildProInstance(perlinVals.x, perlinVals.y, perlinVals.z);

    }

    public void BuildProInstance(float a_biome, float a_elevation, float a_temperature)
    {
        GameObject proInstance = m_proceduralPrefab.BuildProPrefabInstance(a_biome, a_elevation, a_temperature);
    }

    public Node GetNode()
    {
        if(node == null)
        {
            node = new Node();
        }

        return node;
    }

    public ITraversable[] GetConnectedTraversables()
    {
        throw new System.NotImplementedException();
    }

    public int CompareTo(ITraversable other)
    {
        throw new System.NotImplementedException();
    }
}
