using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Artefactos
{
  public class Artifact : MonoBehaviour
  {

    [HideInInspector]
    public Outline outline;


    [SerializeField]
    private Artifacts _artifact;

    [HideInInspector]
    public GameObject spawner;

    [SerializeField]
    private string _poligonoBase;
    [SerializeField]
    private int _arestasBase, _facesLaterais, _faces, _vertices, _arestas;

    public string PoligonoBase { get => _poligonoBase; }
    public int ArestasBase { get => _arestasBase; }
    public int FacesLaterais { get => _facesLaterais; }
    public int Faces { get => _faces; }
    public int Vertices { get => _vertices; }
    public int Arestas { get => _arestas; }
    public Artifacts Artifacts { get => _artifact; }

    private void Start()
    {
      outline = GetComponent<Outline>();
    }
  }

}
