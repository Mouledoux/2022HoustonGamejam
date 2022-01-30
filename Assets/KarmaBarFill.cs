using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarmaBarFill : MonoBehaviour
{
    public bool goodKarma = true;

    Image fillBar;

    // Start is called before the first frame update
    void Start()
    {
        fillBar = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goodKarma)
        {
            fillBar.fillAmount = Mathf.Clamp(EnemyManager.karmaMeter, 0, 1);
        }
        else
        {
            fillBar.fillAmount = -Mathf.Clamp(EnemyManager.karmaMeter, -1, 0);
        }
    }
}
