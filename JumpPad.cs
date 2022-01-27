using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float JumpForce = 100f;
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.GetComponent<PlayerMovement>().Jump(JumpForce);
        }
        catch
        {
            try
            {
                other.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
            catch
            {

            }
        }
            
    }       
}
