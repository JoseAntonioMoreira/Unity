//Simple mechanic for an enemy to go towards a point and then chose another point to come out of, after a delay
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
  public class EnemyVents : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] _vents;//point for the enemy to chose from

    private GameObject _vent;//the point from where the enemy entered

    private bool _ChangedVent = false;
    public bool ChangedVent { get => _ChangedVent; set => _ChangedVent = value; }

    public void VentManager(NavMeshAgent agent)//function to chose a point based on the current location
    {
      if (null == _vent)//if the enemy hasn't picked a vent he should pick the nearest point to enter 
      {
        _ChangedVent = false;
        _vent = CalculateClosestVent();
      }

      if (agent.destination != _vent.transform.position)//if the enemy as chosen a point it will walk towards it
      {
        agent.SetDestination(_vent.transform.position);
        agent.isStopped = false;
      }

      if (1f > Vector3.Distance(agent.transform.position, _vent.transform.position))//when the enemy reaches the point it should chose the next point, excluiding the current point
      {
        SpawnOnVent(agent,ChooseNextVent(_vent), 2f);
      }
    }

    private GameObject CalculateClosestVent()//function that calculates the nearest point from the position of the enemy
    {
      GameObject closestVent = _vents[0];
      foreach (GameObject vent in _vents)
      {
        if (Vector3.Distance(transform.position, vent.transform.position) < Vector3.Distance(transform.position, closestVent.transform.position))
        {
          closestVent = vent;
        }
      }
      return closestVent;
    }

    private GameObject ChooseNextVent(GameObject enteredVent)//function to chose the next point, excluding the current point
    {
      int generateNumber;
      while (true)
      {
        generateNumber = Random.Range(0, _vents.Length);

        if (_vents[generateNumber] != enteredVent)
          break;
      }
      return _vents[generateNumber];
    }




    private void SpawnOnVent(NavMeshAgent agent,GameObject leaveVent, float timeToLeave)//function to make the enemy spawn in the selected point
    {
      IEnumerator enumerator = SpawnOnVents(agent, leaveVent, timeToLeave);
      StartCoroutine(enumerator);
    }

    IEnumerator SpawnOnVents(NavMeshAgent agent, GameObject leaveVent, float timeToLeave)//Coroutine to add a delay so the enemy doesn't  leave the point immediately (best to way to do this is adding a animation with a key event, but for purpose of demonstration I made it this way)
    {
      yield return new WaitForSeconds(timeToLeave);
      agent.transform.position = leaveVent.transform.position;
      _vent = null;
      _ChangedVent = true;
    }
  }
}
