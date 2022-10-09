//Script to make a combination of pressure plates

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSequence : MonoBehaviour
{
  public GameObject[] sequence;//Add here the combination of pressure plates that you need 
  public List<PressurePlate> slabsPressed = new List<PressurePlate>();

  public void CheckSequence() //This function is called on the PressurePlate script after adding a new pressure plate to check if the combination is the right one
  {
    if (slabsPressed.Count == sequence.Length)
    {
      if (slabsPressed[slabsPressed.Count - 1].gameObject == sequence[slabsPressed.Count - 1].gameObject) //This condiction is made when the puzzle is solved
      {
        slabsPressed[slabsPressed.Count - 1].isSolution = true; ////Instruction to give the reward of for solving the puzzle
        GetComponent<Animator>().SetBool("Opened", true);       ////
        return;
      }
    }
    else if (slabsPressed.Count > sequence.Length)
      return;

    if (slabsPressed[slabsPressed.Count - 1].gameObject != sequence[slabsPressed.Count - 1].gameObject)//This condition checks if the player is solving the puzzle right
    {                                                                                                  
      foreach (PressurePlate pressurePlate in slabsPressed)//if the player makes a mistake the puzzle resets
      {
        pressurePlate.isSolution = false;
        pressurePlate.ResetPlates();
      }
      slabsPressed.Clear();
    }
    else
    {
      slabsPressed[slabsPressed.Count - 1].isSolution = true;
    }
  }
}
