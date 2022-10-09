using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSequence : MonoBehaviour
{
  public GameObject[] sequence;
  public List<PressurePlate> slabsPressed = new List<PressurePlate>();

  public void CheckSequence()
  {
    if (slabsPressed.Count == sequence.Length)
    {
      if (slabsPressed[slabsPressed.Count - 1].gameObject == sequence[slabsPressed.Count - 1].gameObject)
      {
        slabsPressed[slabsPressed.Count - 1].isSolution = true;
        GetComponent<Animator>().SetBool("Opened", true);
        return;
      }
    }
    else if (slabsPressed.Count > sequence.Length)
      return;

    if (slabsPressed[slabsPressed.Count - 1].gameObject != sequence[slabsPressed.Count - 1].gameObject)
    {
      foreach (PressurePlate pressurePlate in slabsPressed)
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
