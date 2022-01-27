using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthPoints = 10;
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.GetComponent<PlayerHealth>().health < 200)
            {
                other.GetComponent<PlayerHealth>().TakeDamage(-HealthPoints);
                Destroy(gameObject);
            }
        }
        catch
        {

        }
    }
}
