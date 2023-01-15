using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Artefactos
{
  public class ArtifactsGameManager : MonoBehaviour
  {
    [SerializeField]
    private Levels level;

    [SerializeField]
    private Pointers.MouseOutlineChecker mouseChecker;

    [SerializeField]
    private List<GameObject> artifactsSpawners;

    private readonly List<Artifact> spawnedArtifacts = new();

    public List<GameObject> resetSpawners;

    public Artifact[] artifacts;

    public Ruin[] ruins;

    private void OnEnable()
    {
      mouseChecker.enabled = false;
      RuinSpawner();
      ArtifactsSpawner();
    }

    private void OnDisable()
    {
      mouseChecker.enabled = true;
      ResetGame();
    }

    private void ResetGame()
    {
      foreach (var item in spawnedArtifacts)
      {
        if (item != null)
          Destroy(item.gameObject);
      }
      spawnedArtifacts.Clear();
      artifactsSpawners = new List<GameObject>(resetSpawners);

      //Fazer aqui o que faltar para os resets
    }

    private void RuinSpawner()
    {
      for (int i = 0; i < ruins.Length; i++)
      {
        ruins[i].GetSolution(SpawnSolution());
        GenerateQuestion(ruins[i]);
      }
      artifactsSpawners.Clear();
    }

    private Artifact SpawnSolution()
    {
      int randomNumber = Random.Range(0, artifacts.Length);
      Artifact temp = Instantiate(artifacts[randomNumber]);
      spawnedArtifacts.Add(temp);
      randomNumber = Random.Range(0, artifactsSpawners.Count);
      temp.transform.position = artifactsSpawners[randomNumber].transform.position;
      temp.spawner = artifactsSpawners[randomNumber];
      artifactsSpawners.Remove(artifactsSpawners[randomNumber]);
      return temp;
    }

    private void ArtifactsSpawner()
    {
      foreach (GameObject artifactSpawn in artifactsSpawners)
      {
        int randomNumber = Random.Range(0, artifacts.Length);
        Artifact temp = Instantiate(artifacts[randomNumber]);
        temp.transform.position = artifactSpawn.transform.position;
        temp.spawner = artifactSpawn;
      }
    }

    private void GenerateQuestion(Ruin ruin)
    {
      switch (level)
      {
        case Levels.Level1:
          ruin.question.text = "Seleciona ";
          switch (ruin.artifactSolution.Artifacts)
          {
            case Artifacts.Cubo:
              ruin.question.text += "o cubo";
              break;
            case Artifacts.PrismaTriangular:
              ruin.question.text += "o prisma triangular";
              break;
            case Artifacts.PiramideQuadrangular:
              ruin.question.text += "a pirâmide quadrangular";
              break;
            case Artifacts.Cilindro:
              ruin.question.text += "o cilindro";
              break;
            case Artifacts.Paralelepipedo:
              ruin.question.text += "o paralelepipedo";
              break;
            case Artifacts.PrismaPentagonal:
              ruin.question.text += "o prisma pentagonal";
              break;
            case Artifacts.PrismaHexagonal:
              ruin.question.text += "o prisma hexagonal";
              break;
            case Artifacts.Cone:
              ruin.question.text += "o cone";
              break;
            case Artifacts.Esfera:
              ruin.question.text += "a esfera";
              break;
          }

          //adicionar a forma da base do object
          break;

        case Levels.Level2:
          switch (ruin.artifactSolution.Artifacts)
          {
            case Artifacts.Cubo:
              ruin.question.text = "Seleciona o sólido geométrico com 6 faces, 8 vértices e 12 arestas com o mesmo comprimento";
              break;
            case Artifacts.PrismaTriangular:
              ruin.question.text = "Seleciona o prisma com 5 faces e 9 arestas";
              break;
            case Artifacts.PiramideQuadrangular:
              ruin.question.text = "Seleciona a pirâmide com 8 arestas";
              break;
            case Artifacts.Cilindro:
              ruin.question.text = "Seleciona o sólido geométrico não poliedro que apresenta duas bases circulares";
              break;
            case Artifacts.Paralelepipedo:
              ruin.question.text = "Seleciona o sólido geométrico que não é sólido platónico, com 6 faces, 8 vértices, e 12 arestas";
              break;
            case Artifacts.PrismaPentagonal:
              ruin.question.text = "Seleciona o prisma com 10 vértices";
              break;
            case Artifacts.PrismaHexagonal:
              ruin.question.text = "Seleciona o prisma com 18 arestas";
              break;
            case Artifacts.Cone:
              ruin.question.text = "Seleciona o sólido geométrico não poliedro com uma base circular e um vértice ";
              break;
            case Artifacts.Esfera:
              ruin.question.text = "Seleciona o sólido geométrico não poliedro que não tem arestas, nem vértices, nem bases";
              break;
          }
          break;

        case Levels.Level3:
          switch (ruin.artifactSolution.Artifacts)
          {
            case Artifacts.Cubo:
              ruin.question.text = "Seleciona o sólido geométrico que contem todas as suas faces geometricamente iguais";
              break;
            case Artifacts.PrismaTriangular:
              ruin.question.text = "Seleciona o sólido geométrico com 6 vértices e 9 arestas";
              break;
            case Artifacts.PiramideQuadrangular:
              ruin.question.text = "Seleciona o sólido geométrico com 5 vértices";
              break;
            case Artifacts.Cilindro:
              ruin.question.text = "Seleciona o sólido não poliedro com duas bases";
              break;
            case Artifacts.Paralelepipedo:
              ruin.question.text = "Seleciona o sólido geométrico com 6 faces, e 12 arestas, não tendo todas elas o mesmo comprimento ";
              break;
            case Artifacts.PrismaPentagonal:
              ruin.question.text = "Seleciona o sólido geométrico com 15 arestas";
              break;
            case Artifacts.PrismaHexagonal:
              ruin.question.text = "Seleciona o sólido geométrico com 8 faces";
              break;
            case Artifacts.Cone:
              ruin.question.text = "Seleciona o sólido geométrico que contem apenas um vértice";
              break;
            case Artifacts.Esfera:
              ruin.question.text = "Seleciona o sólido não poliedro que não tem bases";
              break;
          }
          break;
      }
    }
  }
}

