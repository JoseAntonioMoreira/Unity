using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Vector3 targetpoint;
    public Camera Camera;


    public GameObject rock;
    public GameObject flame;
    public GameObject acid;


    float cooldown = 0.3f;
    float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = cooldown;
    }

    void Shoot()
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = ray.GetPoint(1000);
        }

        //uncomment the element you want to use

        if (Input.GetKeyUp(KeyCode.Mouse0))//rock and acid
        {
            //Rock();
            Acid();
        }


        /*if (Input.GetKey(KeyCode.Mouse0))//fire
        {
            
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }


            if (currentTime <= 0)
            {
                Fire();
                currentTime = cooldown;
            }
        } */
    }







    void Fire()
    {
        var fogo = Instantiate(flame, transform.position, transform.rotation);
        fogo.transform.parent = gameObject.transform;
        fogo.GetComponent<Rigidbody>().velocity = (targetpoint - transform.position).normalized * 1000 * Time.deltaTime;
      

    }

    void Rock()
    {
        var pedra = Instantiate(rock, transform.position, transform.rotation);
        pedra.GetComponent<Rigidbody>().velocity = (targetpoint - transform.position).normalized * 1000 * Time.deltaTime;
    }

    void Acid()
    {
        var acido = Instantiate(acid, transform.position, transform.rotation);
        acido.GetComponent<Rigidbody>().velocity = (targetpoint - transform.position).normalized * 1000 * Time.deltaTime;

    }












    void Update()
    {
        Shoot();
       
    }
}
