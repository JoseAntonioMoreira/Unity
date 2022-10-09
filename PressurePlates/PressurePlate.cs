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
    sequence.slabsPressed.Add(this);
    sequence.CheckSequence();
    animator.SetBool("isUp", false);
  }

  private void OnTriggerExit(Collider other)
  {
    if(!isSolution)
    ResetPlates();
  }

  public void ResetPlates()
  {
    animator.SetBool("isUp", true);
  }
}
