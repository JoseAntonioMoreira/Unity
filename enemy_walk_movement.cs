//This is a script for AI walking of a skeleton, the script creates a sphere around the GameObject and selects a random location.
//After that the GameObject follows and when a timer reaches 0 it gives a new location again so the GameObject doesn't stop walking
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_walk_movement : MonoBehaviour
{

    public AudioSource sons;
    public AudioClip damage_taken;
    public AudioClip fala1;
    public AudioClip fala2;
    public AudioClip fala3;
    public AudioClip fala4;


    private int escolha;
    private float tempo;
    private float waitTime=8;


    public float health;

    public Transform target2;

    public float wanderTimer;
    public float wanderRadius;
    public Animator anim;




    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;


    public GameObject coin;
    private float drop;

    private enemy_bullet_spawn_walk mira;
    public GameObject player;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        health = 100;
        drop = 100;
        mira = player.GetComponent<enemy_bullet_spawn_walk>();

        
    }









    void OnEnable()
    {
        timer = wanderTimer;
    }










    
    void Update()
    {

        if (drop <= 0)
        {
            coin_drop();
            drop = 100;
        }
        if (mira.aim)
        {
            transform.LookAt(target2);
                   
        }



        if (health > 0)
        {
            anim.SetBool("revive", false);
            agent.enabled = true;

            timer += Time.deltaTime;
        }
        else if (health <= 0)
        {
            anim.SetBool("Dead", true);
            agent.enabled = false;
            Invoke("Rise_up", 120);
        }


        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }


    }




    void Rise_up()
    {
        health = 100;
        anim.SetBool("Dead", false);
        anim.SetBool("revive", true);
    }



    void coin_drop()
    {
        Vector3 moeda = new Vector3(transform.position.x+2,transform.position.y+1,transform.position.z);
        Instantiate(coin,moeda, transform.rotation);
    }



    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {

        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Sphere(Clone)")
        {
            health -= 100;
            drop -= 100;
            sons.PlayOneShot(damage_taken, 0.7f);
        }
    }


}
