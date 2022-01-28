using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool InfitnitySpawn = false;
    public float Frequency = 1f;
    public int Count = 1;
    public GameObject obj;
    private float startingFrequency;
    // Start is called before the first frame update
    void Start()
    {
        startingFrequency = Frequency;
    }

    // Update is called once per frame
    void Update()
    {
        Frequency -= Time.deltaTime;
        if (Count > 0 && Frequency <= 0 || InfitnitySpawn && Frequency <= 0)
        {
            spawnObject(obj);
            if(!InfitnitySpawn) --Count;
            Frequency = startingFrequency;
        }
        if (Count <= 0 && !InfitnitySpawn)
            Destroy(gameObject);
    }
    private void spawnObject(GameObject obj)
    {
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
