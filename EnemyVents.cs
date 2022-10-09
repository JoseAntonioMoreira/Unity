using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
  public class EnemyVents : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] _vents;

    private GameObject _vent;

    private bool _ChangedVent = false;
    public bool ChangedVent { get => _ChangedVent; set => _ChangedVent = value; }

    public void VentManager(NavMeshAgent agent)
    {
      if (null == _vent)
      {
        _ChangedVent = false;
        _vent = CalculateClosestVent();
      }

      if (agent.destination != _vent.transform.position)
      {
        agent.SetDestination(_vent.transform.position);
        agent.isStopped = false;
      }

      if (1f > Vector3.Distance(agent.transform.position, _vent.transform.position))
      {
        SpawnOnVent(agent,ChooseNextVent(_vent), 2f);
      }
    }

    private GameObject CalculateClosestVent()
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

    private GameObject ChooseNextVent(GameObject enteredVent)
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




    private void SpawnOnVent(NavMeshAgent agent,GameObject leaveVent, float timeToLeave)
    {
      IEnumerator enumerator = SpawnOnVents(agent, leaveVent, timeToLeave);
      StartCoroutine(enumerator);
    }

    IEnumerator SpawnOnVents(NavMeshAgent agent, GameObject leaveVent, float timeToLeave)
    {
      yield return new WaitForSeconds(timeToLeave);
      agent.transform.position = leaveVent.transform.position;
      _vent = null;
      _ChangedVent = true;
    }
  }
}
