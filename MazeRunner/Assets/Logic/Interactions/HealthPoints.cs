using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{

    public float HealPoints
    {
        get { return healthPoints; }
        set
        {
            healthPoints = value;//check health
            if (healthPoints <= 0)
                Destroy(gameObject);
        }
    }
    [SerializeField]//allows private field to be visible in inspector
    private float healthPoints = 100f;
}
