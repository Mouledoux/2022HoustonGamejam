using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProPrefab", menuName = "Perlin/ProPrefab")]
public class ProceduralPrefab : ScriptableObject
{
    public GameObject m_prefab;


    [Space]
    [Range(0f, 1f)]
    public float m_minBiomeVal;
    [Range(0f, 1f)]
    public float m_maxBiomeVal;


    [Space]
    [Range(0f, 1f)]
    public float m_minElevation;
    [Range(0f, 1f)]
    public float m_maxElevation;


    [Space]
    [Range(0f, 1f)]
    public float m_minTemperature;
    [Range(0f, 1f)]
    public float m_maxTemperature;


    public float averageBiome => (m_minBiomeVal + m_maxBiomeVal) / 2f;
    public float averageElevation => (m_minElevation + m_maxElevation) / 2f;
    public float averageTemperature => (m_minTemperature + m_maxTemperature) / 2f;


    [Space, SerializeField]
    private ProceduralPrefab[] m_subPrefabs;


    public ProceduralPrefab(float a_minBiome = 0f, float a_maxBiome = 1f,
        float a_minElevation = 0f, float a_maxElevation = 1f,
        float a_minTemperature = 0f, float a_maxTemperature = 1f)
    {
        m_minBiomeVal = a_minBiome;
        m_maxBiomeVal = a_maxBiome;

        m_minElevation = a_minElevation;
        m_maxElevation = a_maxElevation;

        m_minTemperature = a_minTemperature;
        m_maxTemperature = a_maxTemperature;
    }

    public GameObject GetPrefab(float a_biome, float a_elevation, float a_temperature)
    {
        float bias = float.MaxValue;
        return GetSubPrefabs(a_biome, a_elevation, a_temperature, ref bias, null).m_prefab;
    }

    private ProceduralPrefab GetSubPrefabs(float biome_, float elevation_, float temperature_, ref float bias_, ProceduralPrefab parentBiome = null)
    {
        System.Func<float, float, float, float> getSubOffset = (pMin, pMax, cMod) => (pMin + (pMax - pMin) * cMod);

        float tBiomeMin = m_minBiomeVal;
        float tBiomeMax = m_maxBiomeVal;
        float tElevationMin = m_minElevation;
        float tElevationMax = m_maxElevation;
        float tTemperatureMin = m_minTemperature;
        float tTemperatureMax = m_maxTemperature;

        if(parentBiome != null)
        {
            tBiomeMin = getSubOffset(parentBiome.m_minBiomeVal, parentBiome.m_maxBiomeVal, m_minBiomeVal);
            tBiomeMax = getSubOffset(parentBiome.m_minBiomeVal, parentBiome.m_maxBiomeVal, m_maxBiomeVal);

            tElevationMin = getSubOffset(parentBiome.m_minElevation, parentBiome.m_maxElevation, m_minElevation);
            tElevationMax = getSubOffset(parentBiome.m_minElevation, parentBiome.m_maxElevation, m_maxElevation);

            tTemperatureMin = getSubOffset(parentBiome.m_minTemperature, parentBiome.m_maxTemperature, m_minTemperature);
            tTemperatureMax = getSubOffset(parentBiome.m_minTemperature, parentBiome.m_maxTemperature, m_maxTemperature);
        }

        if(biome_ < tBiomeMin || biome_ > tBiomeMax ||
            elevation_ < tElevationMin || elevation_ > tElevationMax ||
                temperature_ < tTemperatureMin || temperature_ > tTemperatureMax)
                    return null;

        float biomeBias = (tBiomeMax - tBiomeMin);
        float elevationBias = (tElevationMax - tElevationMin);
        float temperatureBias = (tTemperatureMax - tTemperatureMin);
        float bias = biomeBias + elevationBias + temperatureBias;

        if(bias <= bias_)
        {
            ProceduralPrefab sub = null;
            foreach(ProceduralPrefab b in m_subPrefabs)
            {
                ProceduralPrefab temp = b.GetSubPrefabs(biome_, elevation_, temperature_, ref bias, this);
                sub = temp == null ? sub : temp;
            }
            if(sub != null)
            {
                return sub;
            }
        }
        else
        {
            return null;
        }
        
        return this;
    }
}
