using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Pointers
{
  public class Pointers : MonoBehaviour
  {
    protected Outline lastOutline = null;
    protected RaycastHit MousePointer()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      Physics.Raycast(ray, out RaycastHit hit);
      return hit;
    }

    protected RaycastHit CameraPointer(float distance)
    {
      Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, distance);
      return hit;
    }

    protected void CheckOutlines(RaycastHit typeOfRaycast)
    {
      RaycastHit currentObject = typeOfRaycast;
      if (currentObject.transform != null)
      {
        if (currentObject.transform.TryGetComponent(out Outline outline))
        {
          if (lastOutline != null)
          {
            if (lastOutline.outlineObject != null && lastOutline != outline)
              lastOutline.outlineObject.SetActive(false);

            lastOutline = outline;
            if (lastOutline.outlineObject != null)
              outline.outlineObject.SetActive(true);
          }
          else
          {
            lastOutline = outline;
            outline.outlineObject.SetActive(true);
          }

        }
        else
        {
          if (lastOutline != null)
            if (lastOutline.outlineObject != null)
              lastOutline.outlineObject.SetActive(false);
        }
      }
      else
      {
        if (lastOutline != null)
          if (lastOutline.outlineObject != null)
            lastOutline.outlineObject.SetActive(false);
      }

    }
  }
}

