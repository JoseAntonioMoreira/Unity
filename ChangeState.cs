using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public Material offMaterial;
    public Material onMaterial;
    public bool isOn = false;

    Renderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        if (isOn)
            meshRenderer.material = onMaterial;
        else
            meshRenderer.material = offMaterial;
    }

    public void ChangeInputState()
    {
        if (isOn)
        {
            meshRenderer.material = offMaterial;
            isOn = false;
        }
        else
        {
            meshRenderer.material = onMaterial;
            isOn = true;
        }
    }
}
