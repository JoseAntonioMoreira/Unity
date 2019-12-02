//This script sends a raycast that if hits a gameobject with the tag "pillar" it will create a raycast from there
//It's like a mirror reflect for puzzles
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reflect : MonoBehaviour
{
    private GameObject last_obj_hit;


    public float maxDistance = 50;
    public bool isCasting = false;
    public bool mainRay = false;

    

    public void StartCasting()
    {
        isCasting = true;
    }

    public void StopCasting()
    {
        isCasting = false;
    }
    

    void Update()
    {
        
        if (isCasting || mainRay)
        {
            Reflecting(transform.position, transform.forward);
        }
        else
        {
            StopHit(last_obj_hit);
        }
    }

   

   
    void Reflecting(Vector3 position, Vector3 direction)
    {
       
        Vector3 startingPosition = position;
        Ray ray = new Ray(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.tag == "pillar")
            {
                last_obj_hit = hit.collider.gameObject;
                position = hit.point;
                HasHit(hit.collider.gameObject);
                Debug.DrawLine(startingPosition, position, Color.blue);

            }
        }
        else
        {
            position += direction * maxDistance;
            Debug.DrawLine(startingPosition, position, Color.black);
            StopHit(last_obj_hit);
        }
    }


    public void HasHit(GameObject obj)
    {
        obj.SendMessage("StartCasting");
        Debug.Log("NOME do objecto: " + obj.name);
        
    } 

    public void StopHit(GameObject obj)
    {
        obj.SendMessage("StopCasting");
    }
}
