using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Perlin
{
    public static Texture2D GeneratePerlinTexture(string a_seed, int a_cols, int a_rows, int a_xOffset, int a_yOffset, float a_scale1 = 1f, float a_scale2 = 1f, float a_scale3 = 1f)
    {
        Texture2D perlinTexture = new Texture2D(a_cols, a_rows);
        Color[] pixels = new Color[perlinTexture.width * perlinTexture.height];

        float sampleH, sampleS, sampleV;
        int seedLen;

        a_seed = a_seed.GetHashCode().ToString();
        seedLen = a_seed.Length;

        for(int i = 0; i < perlinTexture.height; i++)
        {
            for(int j = 0; j < perlinTexture.width; j++)
            {
                string seed1 = a_seed.Substring(          0, seedLen / 2);
                string seed2 = a_seed.Substring(seedLen / 4, seedLen / 2);
                string seed3 = a_seed.Substring(seedLen / 2, seedLen / 2);

                int x = j + a_xOffset;
                int y = i + a_yOffset;

                sampleH = GetPerlinNoiseValue(seed1, a_cols, a_rows, x, y, a_scale1, 1f);
                sampleS = GetPerlinNoiseValue(seed2, a_cols, a_rows, x, y, a_scale2, 1f);
                sampleV = GetPerlinNoiseValue(seed3, a_cols, a_rows, x, y, a_scale3, 1f);

                pixels[(i * perlinTexture.width) + j] = Color.HSVToRGB(sampleH, sampleS, sampleV);
            }
        }

        perlinTexture.SetPixels(pixels);
        perlinTexture.Apply();

        return perlinTexture;
    }



    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    public static float GetPerlinNoiseValue(string a_seed, int a_cols, int a_rows, float a_xCoord, float a_yCoord, float a_scale1 = 1f, float a_valueMod = 1f)
    {   
        float seedHash = (float)(a_seed.GetHashCode() % (a_seed.Length));

        a_xCoord /= a_cols;
        a_yCoord /= a_rows;

        a_xCoord *= a_scale1;
        a_yCoord *= a_scale1;

        a_xCoord += seedHash;
        a_yCoord += seedHash;

        return Mathf.Clamp01(Mathf.PerlinNoise(a_xCoord, a_yCoord)) * a_valueMod;
    }
}
