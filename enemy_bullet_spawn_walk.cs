using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet_spawn_walk : MonoBehaviour
{
    private bool canShoot;
    public GameObject bala;
    private float time;
    public float waitTime;
    public Transform target;
    public Animator anim;





    private enemy_walk_movement dead;
    private GameObject Dead;

    public bool aim;


    private float distance;

    void Start()
    {
        waitTime = Random.Range(4,10);
        Dead = GameObject.Find("Skeleton_walking");
        dead = Dead.GetComponent<enemy_walk_movement>();
    }




   // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(target.transform.position, transform.position);
        if (dead.health > 0)
        {
            if (distance <=100f) {
                transform.LookAt(target);
                time += Time.deltaTime;

                if (time > waitTime)
                {
                    aim = true;
                    anim.SetBool("seen", true);
                    Invoke("shoot", 1);
                    time = 0;
                }
            }
        }
    }





    void shoot()
    {
        Instantiate(bala, transform.position, transform.rotation);
        anim.SetBool("seen", false);
        aim = false;
    }
}
