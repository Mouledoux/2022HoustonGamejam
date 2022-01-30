using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject inherentlyEvilCreature;
    public GameObject inherentlyGoodCreature;

    public static float karmaMeter = 0.0f;

    private List<GameObject> badCreatures = new List<GameObject>();
    private List<GameObject> goodCreatures = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        badCreatures.Add(Instantiate(inherentlyEvilCreature, new Vector3(0, 0, 0), Quaternion.identity));
        badCreatures.Add(Instantiate(inherentlyEvilCreature, new Vector3(1, 0, 0), Quaternion.identity));
        badCreatures.Add(Instantiate(inherentlyEvilCreature, new Vector3(-1, 0, 0), Quaternion.identity));
        badCreatures.Add(Instantiate(inherentlyEvilCreature, new Vector3(0, 0, 1), Quaternion.identity));
        badCreatures.Add(Instantiate(inherentlyEvilCreature, new Vector3(0, 0, -1), Quaternion.identity));


        goodCreatures.Add(Instantiate(inherentlyGoodCreature, new Vector3(-3, 0, -5), Quaternion.identity));
        goodCreatures.Add(Instantiate(inherentlyGoodCreature, new Vector3(-2, 0, -5), Quaternion.identity));
        goodCreatures.Add(Instantiate(inherentlyGoodCreature, new Vector3(-4, 0, -5), Quaternion.identity));
        goodCreatures.Add(Instantiate(inherentlyGoodCreature, new Vector3(-3, 0, -5), Quaternion.identity));
        goodCreatures.Add(Instantiate(inherentlyGoodCreature, new Vector3(-3, 0, -5), Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject creature in badCreatures)
        {
            if (creature == null)
            {
                karmaMeter += 0.2f;
            }
        }
        badCreatures.RemoveAll(creature => creature == null);

        foreach (GameObject creature in goodCreatures)
        {
            if (creature == null)
            {
                karmaMeter -= 0.2f;
            }
        }
        goodCreatures.RemoveAll(creature => creature == null);
        Debug.Log(karmaMeter);
    }
}
