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
    if (hasAir && hasFire && hasEarth && hasWater)
    {
      GetComponent<Animator>().SetBool("Opened", true);
      OnComplete?.Invoke();
    }
    else
      GetComponent<Animator>().SetBool("Opened", false);

  }
}
