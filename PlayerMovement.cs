using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 6.0f;
    private float jumpspeed = 8.0f;
    private float gravity = 20.0f;





    private float currentDashTime_b;
    private float currentDashTime_l;
    private float currentDashTime_r;
    private float currentDashTime_f;


    private float dashSpeed = 5.0f;
    private float dashBrake = 0.1f;
    private float MaxDashTime = 2.0f;
    private Vector3 dashMovement;
    private float dashCoolDown;





    private Vector3 moveDirection;
    private CharacterController controller;


    void Start()
    {
        controller = GetComponent<CharacterController>();


        currentDashTime_l = MaxDashTime;
        currentDashTime_b = MaxDashTime;
        currentDashTime_r = MaxDashTime;
        currentDashTime_f = MaxDashTime;
    }






    //simple movement control binding
    private void Movement()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection); //moveDirection = local
            moveDirection *= speed;
            



            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpspeed;
            }
            if (Input.GetKeyDown(KeyCode.Space)) //jump
            {
                moveDirection.y = jumpspeed;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))//run
            {
                speed = 8.0f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))//stop run
            {
                speed = 6.0f;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && dashCoolDown <= 1f && Input.GetKey(KeyCode.W))//dash(shift+w)
            {
                currentDashTime_f = 0f;
                dashCoolDown = 1.8f;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && dashCoolDown <= 1f && Input.GetKey(KeyCode.A))//dash(shift+a)
            {
                currentDashTime_l = 0f;
                dashCoolDown = 1.8f;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && dashCoolDown <= 1f && Input.GetKey(KeyCode.D))//dash(shift+d)
            {
                currentDashTime_r = 0f;
                dashCoolDown = 1.8f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl) && dashCoolDown <= 1f && Input.GetKey(KeyCode.S))//dash(shift+s)
            {
                currentDashTime_b = 0f;
                dashCoolDown = 1.8f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl) && dashCoolDown <= 1f)//dash(shift+s)
            {
                currentDashTime_b = 0f;
                dashCoolDown = 1.8f;
            }








            if (currentDashTime_l < MaxDashTime)//dash left
            {

                dashMovement = transform.TransformDirection(new Vector3(-dashSpeed, 0, 0));
                currentDashTime_l += dashBrake;
            }
            else if (currentDashTime_r < MaxDashTime)//dash right
            {

                dashMovement = transform.TransformDirection(new Vector3(dashSpeed, 0, 0));
                currentDashTime_r += dashBrake;
            }
            else if (currentDashTime_b < MaxDashTime)//dash back
            {

                dashMovement = transform.TransformDirection(new Vector3(0, 0, -dashSpeed));
                currentDashTime_b += dashBrake;
            }
            else if (currentDashTime_f < MaxDashTime)//dash front
            {

                dashMovement = transform.TransformDirection(new Vector3(0, 0, dashSpeed));
                currentDashTime_f += dashBrake;
            }
            else //don't dash
            {
                dashMovement = Vector3.zero;
            }
            controller.Move(dashMovement * Time.deltaTime);





        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }



    private void Update()
    {
        if (dashCoolDown > 0)
        {
            dashCoolDown -= Time.deltaTime;
        }
    }






    private void FixedUpdate()
    {
        Movement();
    }

}
