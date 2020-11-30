using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    public Text hpText;
    public float HealPoints
    {
        get { return healthPoints; }
        set
        {
            healthPoints = value;//check health
            hpText.text = HealPoints.ToString();
            if (healthPoints <= 0)
                Destroy(gameObject);
        }
    }
    [SerializeField]//allows private field to be visible in inspector
    private float healthPoints = 100f;

    private void Awake()
    {
        Text hpText = GetComponent<Text>();
    }

    private void Update()
    {
        
    }
}
