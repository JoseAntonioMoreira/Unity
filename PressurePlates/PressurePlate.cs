//Add this to the pressure plate gameObject
//References the PressurePlateSequence to checks if the puzzle has been solved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
  public bool isSolution;
  public PressurePlateSequence sequence;

  private float yStart;
  public Animator animator;

  private void Start()
  {
    yStart = transform.position.y;
  }

  private void OnTriggerEnter(Collider other)
  {                             
    sequence.slabsPressed.Add(this);      ////On contact with the player the pressure plate will check is it's one of the solutions
    sequence.CheckSequence();             ////If the pressure plate isn't on the solution sequence 
    animator.SetBool("isUp", false);      ////All the presure plates will reset and the player will have to make the path again
  }

  private void OnTriggerExit(Collider other)//If the pressure plate isn't on the solution sequence  
  {                                         //when the player leaves, the position will reset to it's original state
    if(!isSolution)
    ResetPlates();
  }

  public void ResetPlates()
  {
    animator.SetBool("isUp", true);
  }
}
