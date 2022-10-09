using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [CreateAssetMenu(menuName = "Player System/ Player Components/ Interact")]
    public class Interact : PlayerComponent
    {
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 3f))
                {
                    if (hit.transform.tag != "Pillar")
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

                        if (Camera.main.transform.childCount > 0)
                        {
                            if (Camera.main.transform.GetChild(0).tag == "Totems" || Camera.main.transform.GetChild(0).tag == "Elementals")
                                Camera.main.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                            Camera.main.transform.DetachChildren();
                        }
                    }

                    switch (hit.transform.tag)
                    {
                        case "Levers":
                            hit.transform.GetComponent<LeverPuzzle>().ChangeLever();
                            break;

                        case "Elementals":
                            if (Camera.main.transform.childCount == 0)
                            {
                                hit.transform.parent = Camera.main.transform;
                                Camera.main.transform.GetChild(0).transform.localPosition = new Vector3(-1.5f, 0, 1);
                                Camera.main.transform.GetChild(0).GetComponent<SphereCollider>().enabled = false;
                            }
                            else
                                Camera.main.transform.DetachChildren();
                            break;

                        case "Totems":
                            if (Camera.main.transform.childCount == 0)
                            {
                                hit.transform.parent = Camera.main.transform;
                                Camera.main.transform.GetChild(0).transform.localPosition = new Vector3(-1, 0, 1);
                                Camera.main.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                            }
                            break;

                        case "Pillar":
                            if (Camera.main.transform.childCount != 0)
                            {
                                switch (CheckPiece.CountConfirm)
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
                                if (Camera.main.transform.GetChild(0).name == hit.transform.name)
                                {
                                    Camera.main.transform.GetChild(0).transform.position = hit.transform.position;
                                    Camera.main.transform.GetChild(0).transform.rotation = hit.transform.rotation;
                                    Camera.main.transform.GetChild(0).transform.tag = "Untagged";
                                    Camera.main.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                                    Camera.main.transform.DetachChildren();
                                    CheckPiece.CountConfirm++;
                                    CheckPiece.CheckCombination();
                                    Destroy(hit.transform.gameObject);
                                }
                            }
                            break;

                        case "Button":
                            if (Vector3.Distance(Camera.main.transform.position, hit.transform.position) <= 1.5f)
                            {
                                Interection interection = hit.transform.GetComponent<Interection>();
                                if (interection)
                                    interection.DoInterection();
                            }
                            break;
                    }
                }
                else
                {
                    if (Camera.main.transform.childCount > 0)
                        if (Camera.main.transform.GetChild(0).tag == "Totems" || Camera.main.transform.GetChild(0).tag == "Elementals")
                            Camera.main.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                    Camera.main.transform.DetachChildren();
                }

            }
        }
    }
}
