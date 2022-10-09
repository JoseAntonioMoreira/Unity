////Lever Puzzle that turns lights ON or OFF depending on the previous state of the light
////If all lights are on the puzzle is resolved
////The lights use the ChangeState script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    private Animator gate;

    private bool isOn = false;

    [SerializeField]
    private ChangeState[] changeStates;//Array of the lights, making possible to have as many levers and lights as needed

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("On", isOn);
    }

    public void ChangeLever()
    {
        if (gameObject.name != "Cube.027")          ////    this condition can be removed if all the levers will have the same behaviour
        {                                           ////    
            changeStates[0].ChangeInputState();     ////    
            changeStates[1].ChangeInputState();     ////    this is changing only the first 2 lights of the array
        }                                           ////    but a loop can be made for all the lights to switch
        else        	                            ////    or more can be added for the needed puzzle
            changeStates[0].ChangeInputState();     ////
            
            
        ChangeLeverState();
        CheckIfResolved();
    }

    void CheckIfResolved()  //Looks at the array of lights and if all are ON the puzzle will play the reward
    {
        for (int i = 0; i < changeStates.Length; i++)   ////
        {                                               ////    This loop will check if all the lights are ON
            if (!changeStates[i].isOn)                  ////    If even one of them is OFF the function will
            {                                           ////    stop working so the reward can't be played
                return;                                 ////
            }                                           ////
        }                                               ////

        gate.SetBool("Opened", true);//This part is the reward for solving the puzzle
    }

    private void ChangeLeverState()//Animation to move the lever so the player has some feedback
    {
        isOn = !isOn;

        anim.SetBool("On", isOn);
    }
}
