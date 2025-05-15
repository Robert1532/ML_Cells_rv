using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Text timerText;
    public Text scoreText;
    public EvolutionManager evolutionManager;

    public int maxCells = 20;
    public float roundDuration = 10f;

    private float timeLeft;
    private int score = 0;
    private List<GameObject> activeCells = new List<GameObject>();

    void Start()
    {
        evolutionManager.gameManager = this;
        timeLeft = roundDuration;
        SpawnCells();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Tiempo: " + Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            EndRound();
            evolutionManager.Evolve();
            timeLeft = roundDuration;
            SpawnCells();
        }
    }

    void SpawnCells()
    {
        ClearCells();

        score = 0;
        scoreText.text = "Puntaje: " + score;

        for (int i = 0; i < maxCells; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
            GameObject cellObj = Instantiate(cellPrefab, pos, Quaternion.identity);
            CellController cell = cellObj.GetComponent<CellController>();

            cell.genome = evolutionManager.population[i];
            cell.evolutionManager = evolutionManager;

            activeCells.Add(cellObj);
        }
    }

    void EndRound()
    {
        foreach (var cellObj in activeCells)
        {
            if (cellObj != null)
            {
                CellController cell = cellObj.GetComponent<CellController>();
                if (cell != null)
                    cell.Survived();
            }
        }

        ClearCells();
    }

    void ClearCells()
    {
        foreach (var obj in activeCells)
            if (obj != null)
                Destroy(obj);

        activeCells.Clear();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Puntaje: " + score;
    }
}
