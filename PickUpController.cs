using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpController : MonoBehaviour
{
    public ProjectileShooting gunScript;
    public Rigidbody rb;
    public Collider coll;
    public Transform player, gunContainer, fpsCam;

    public float PickUpRange, dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    public TextMeshProUGUI ammunitionDisplay;

    private void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = rb.isKinematic = coll.isTrigger = slotFull = false;
            ammunitionDisplay.SetText("");
        }
        else
            gunScript.enabled = rb.isKinematic = coll.isTrigger = slotFull = true;
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (Input.GetKeyDown(KeyCode.E) && distanceToPlayer.magnitude <= PickUpRange && !equipped && !slotFull)
            PickUp();

        if (Input.GetKeyDown(KeyCode.F) && equipped)
            Drop();
    }
    private void PickUp()
    {
        equipped = slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = coll.isTrigger = gunScript.enabled = true;
    }
    private void Drop()
    {
        equipped = slotFull = false;
        rb.isKinematic = coll.isTrigger = gunScript.enabled = false;
        transform.SetParent(null);
        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random)*10);

        
        ammunitionDisplay.SetText("");
    }
}
