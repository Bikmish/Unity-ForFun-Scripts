using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAnims : MonoBehaviour
{
    public string AnimationName = "WeaponBobbing";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            if (gameObject.GetComponent<Animation>().isPlaying == false && gameObject.GetComponent<PickUpController>().equipped)
                gameObject.GetComponent<Animation>().Play(AnimationName);
    }
}
