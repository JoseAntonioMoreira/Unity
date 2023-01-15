using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Pointers
{
  public class CameraOutlineChecker : Pointers
  {
    public float outlineDistance = 5;
    private void Update()
    {
      CheckOutlines(CameraPointer(outlineDistance));
    }
  }
}

