using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallBoom : MonoBehaviour
{
    public float Timer = 3f, explosionRadius = 3f, EnemyDistance = 1.5f, explosionForce = 10f;
    public int explosionDamage = 35;
    public GameObject BoomEffect;
    public LayerMask EnemyMask, PlayerMask, ProjectileMask;
    bool isEnemy = false;
    public bool AllLayers = false, friendlyFire = false;
    //public Material material;
    //Color oldColor;

    // Start is called before the first frame update
    void Start()
    {
        //oldColor = material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        isEnemy = AllLayers ? Physics.CheckSphere(gameObject.transform.position, EnemyDistance, Physics.AllLayers - ProjectileMask) : Physics.CheckSphere(gameObject.transform.position, EnemyDistance, EnemyMask);
        Timer -= Time.deltaTime;
        if (Timer <= 0.15f)
            GetComponent<Renderer>().material.color = new Color(0.15f, 0, 0.1f);
        //    GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b);
        //    material.SetColor("_Color",Color.red);
        if (Timer<=0 || isEnemy)
        {
            //making damage
            Explode();

            //creating exlosion effect
            if (BoomEffect != null)
                Instantiate(BoomEffect, gameObject.transform.position, Quaternion.identity);
            
            //destroying the projectile 
            Destroy(gameObject);
            //material.SetColor("_Color", oldColor);
        }
    }
    private void Explode()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, EnemyMask);
        Collider[] players = Physics.OverlapSphere(transform.position, explosionRadius, PlayerMask);
        Collider[] objs = Physics.OverlapSphere(transform.position, explosionRadius, Physics.AllLayers);
        for (int i = 0; i<enemies.Length; ++i)
        {
            EnemyAI curEnemy = enemies[i].GetComponent<EnemyAI>();
            if (curEnemy != null)
                curEnemy.TakeDamage(explosionDamage);
            else
                print("Ooops! There is no EnemyAI script!");
        }
        for(int j = 0; j<players.Length; ++j)
            if (friendlyFire)
            {
                PlayerHealth curPlayer = players[j].GetComponent<PlayerHealth>();
                if (curPlayer != null)
                    curPlayer.TakeDamage(explosionDamage);
                else
                    print("Ooops! There is no PlayerHealth script!");
            }
        for (int k = 0; k < objs.Length; ++k)
            if (objs[k].GetComponent<Rigidbody>())
                objs[k].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
    }
}
