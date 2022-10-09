//Checks if all the elements have been placed correctly and gives the reward for solving the combination

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckCombination : MonoBehaviour
{
  public bool hasFire, hasWater, hasAir, hasEarth;

  public UnityEvent OnComplete;

  public void TryToOpen()
  {
    if (hasAir && hasFire && hasEarth && hasWater)          //This is the condiction that checks is the puzzle as been solved
    {
      GetComponent<Animator>().SetBool("Opened", true);
      OnComplete?.Invoke();
    }
    else                                                    //For reseting the reward if some of the keys is removed
      GetComponent<Animator>().SetBool("Opened", false);

  }
}
