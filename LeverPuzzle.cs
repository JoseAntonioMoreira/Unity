using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    private Animator gate;

    private bool isOn = false;

    [SerializeField]
    private ChangeState[] changeStates;

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("On", isOn);
    }

    public void ChangeLever()
    {
        if (gameObject.name != "Cube.027") 
        {
            changeStates[0].ChangeInputState();
            changeStates[1].ChangeInputState();
        }
        else
            changeStates[0].ChangeInputState();
        ChangeLeverState();
        
        CheckIfResolved();
    }

    void CheckIfResolved()
    {
        for (int i = 0; i < changeStates.Length; i++)
        {
            if (!changeStates[i].isOn)
            {
                return;
            }
        }

        gate.SetBool("Opened", true);
    }

    private void ChangeLeverState()
    {
        isOn = !isOn;

        anim.SetBool("On", isOn);
    }
}