using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectElemental : MonoBehaviour
{
    private enum Elementals
    {
        air,
        fire,
        earth,
        water
    }
    public CheckCombination checkCombination;

    [SerializeField]
    private Elementals elementals;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(elementals.ToString()))
        {
            switch (elementals)
            {
                case Elementals.air:
                    checkCombination.hasAir = true;
                    break;

                case Elementals.fire:
                    checkCombination.hasFire = true;
                    break;

                case Elementals.earth:
                    checkCombination.hasEarth = true;
                    break;

                case Elementals.water:
                    checkCombination.hasWater = true;
                    break;
            }
            GetComponent<Collider>().enabled = false;
            checkCombination.TryToOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(elementals.ToString()))
        {
            switch (elementals)
            {
                case Elementals.air:
                    checkCombination.hasAir = false;
                    break;

                case Elementals.fire:
                    checkCombination.hasFire = false;
                    break;

                case Elementals.earth:
                    checkCombination.hasEarth = false;
                    break;

                case Elementals.water:
                    checkCombination.hasWater = false;
                    break;
            }
            checkCombination.TryToOpen();
        }
    }
}
