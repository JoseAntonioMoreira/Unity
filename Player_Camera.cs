//A basic camera script that makes the camera move closer to the player if there's a wall behind the player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    //mouse (void CamControl)
    public float RotationSpeed;
    public Transform Target, Player;
    private float mouseX, mouseY;



    //zoom (void ViewObstructed)
    public Collider PlayerCollider;
    private bool Parede;
    private float ZoomSpeed = 5f;







    void Start()
    {
        Parede = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }








    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -20, 30);


        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }











    void ViewObstructed()
    {
        RaycastHit hit_wall;
        RaycastHit hit_player;


        PlayerCollider.Raycast(new Ray( transform.position, Target.position - transform.position), out hit_player, 5f);//creates a raycast from the camera that only focuses on the player



        if (Physics.Raycast(Target.position,  -Target.forward, out hit_wall, 4))//creates a raycast from the player that goes backwards 
        {
            Parede = true;
          
        }
        else
        {
            Parede = false;
        }


        if(Parede == true)
        {
            //the camera goes foward if there's a wall behind the player
            if (Vector3.Distance(Target.position, hit_wall.point) <= 4 && Vector3.Distance(transform.position, hit_player.point) >= 2f)//care with the "2f" value, needs to be exact numbers
                transform.Translate(Vector3.forward * ZoomSpeed * Time.deltaTime);
        }
        else
        {
            //the camera goes backwards if there's nothing behind the player
            if (Vector3.Distance(transform.position, hit_player.point) <= 4.5f)
                transform.Translate(Vector3.back * ZoomSpeed * Time.deltaTime);
        }

    }









    void LateUpdate() // if bug try FixedUpdate
    {
        CamControl();
        ViewObstructed();

    }

}
