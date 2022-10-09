//Interacable script for player to pick up certain GameObject
//Uses the unity input system
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [CreateAssetMenu(menuName = "Player System/ Player Components/ Interact")]//This scripts belongs to a scriptable object class that isn't mine 
    public class Interact : PlayerComponent                                   //I just did the interaction mechanic part
    {
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 3f))//3 is the distance of the interactable can be changed according to the needs
                {
                    if (hit.transform.tag != "Pillar") //This conditions are for putting certain GameObjects is there respective places
                    {
                        if (hit.transform.tag == "fire" || hit.transform.tag == "air" || hit.transform.tag == "water" || hit.transform.tag == "earth")
                        {
                            if (Camera.main.transform.childCount > 0)
                                if (Camera.main.transform.GetChild(0).tag == "Elementals")
                                {
                                    Camera.main.transform.GetChild(0).transform.parent = hit.transform;
                                    hit.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
                                    hit.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                                }
                        }

                        if (Camera.main.transform.childCount > 0)//This condition is to empty the player hand
                        {
                            if (Camera.main.transform.GetChild(0).tag == "Totems" || Camera.main.transform.GetChild(0).tag == "Elementals")
                                Camera.main.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                            Camera.main.transform.DetachChildren();
                        }
                    }

                    switch (hit.transform.tag)//Switch to handle the different types of gameObjects
                    {
                        case "Levers":
                            hit.transform.GetComponent<LeverPuzzle>().ChangeLever(); //To activate levers
                            break;

                        case "Elementals":
                            PickObjects(hit.transform.gameObject, new Vector3(-1.5f, 0, 1)); //To pick up the orbs
                            break;
                        case "Totems":
                            PickObjects(hit.transform.gameObject, new Vector3(-1, 0, 1)); //To pick up totems
                            break;

                        case "Pillar": //To place the totem parts in the respective way
                            if (Camera.main.transform.childCount != 0)
                            {
                                switch (CheckPiece.CountConfirm)//This switch is to force the player to put the totems by order
                                {
                                    case 0:
                                        if (Camera.main.transform.GetChild(0).name != "Buffalo_Head")
                                            return;
                                        break;
                                    case 1:
                                        if (Camera.main.transform.GetChild(0).name != "Fox_Head")
                                            return;
                                        break;
                                    case 2:
                                        if (Camera.main.transform.GetChild(0).name != "Eagle_Body")
                                            return;
                                        break;
                                }

                                if (Camera.main.transform.GetChild(0).name == hit.transform.name)//Adjusts the totem in the correct position
                                    ReplaceTotem(hit.transform.gameObject);
                            }
                            break;

                        case "Button"://For the player to press buttons
                            if (Vector3.Distance(Camera.main.transform.position, hit.transform.position) <= 1.5f)
                            {
                                Interection interection = hit.transform.GetComponent<Interection>();
                                if (interection)
                                    interection.DoInterection();
                            }
                            break;
                    }
                }
                else//This condition is so the player can drop the object is holding without the raycast hitting something
                {
                    if (Camera.main.transform.childCount > 0)
                        if (Camera.main.transform.GetChild(0).tag == "Totems" || Camera.main.transform.GetChild(0).tag == "Elementals")
                            Camera.main.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                    Camera.main.transform.DetachChildren();
                }

            }
        }
        
        public void PickObjects(GameObject hit, Vector3 offSet)//Function to pick up orbs and the totem
        {
            if (Camera.main.transform.childCount == 0)
            {
                hit.transform.parent = Camera.main.transform;
                Camera.main.transform.GetChild(0).transform.localPosition = offSet;
                Camera.main.transform.GetChild(0).GetComponent<SphereCollider>().enabled = false;
            }
            else
                Camera.main.transform.DetachChildren();
        }

        public void ReplaceTotem(GameObject hit)//Function to replace the outline totem for the real one
        {
            Camera.main.transform.GetChild(0).transform.position = hit.transform.position;
            Camera.main.transform.GetChild(0).transform.rotation = hit.transform.rotation;
            Camera.main.transform.GetChild(0).transform.tag = "Untagged";
            Camera.main.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            Camera.main.transform.DetachChildren();
            CheckPiece.CountConfirm++;
            CheckPiece.CheckCombination();
            Destroy(hit.gameObject);
        }
    }
}
