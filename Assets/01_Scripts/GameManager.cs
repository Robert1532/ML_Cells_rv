using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Text timerText;
    public Text scoreText;
    public int maxCells = 10;

    private float roundTime = 10f;
    private float timeLeft;
    private int score = 0;

    private List<GameObject> activeCells = new List<GameObject>();
    private List<CellData> memory = new List<CellData>();

    void Start()
    {
        timeLeft = roundTime;
        StartRound();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Tiempo: " + Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            EndRound();
            StartRound();
        }
    }

    void StartRound()
    {
        timeLeft = roundTime;
        ClearCells();
        for (int i = 0; i < maxCells; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
            GameObject cell = Instantiate(cellPrefab, spawnPos, Quaternion.identity);
            CellController ctrl = cell.GetComponent<CellController>();
            ctrl.manager = this;
            ctrl.memory = memory;
            activeCells.Add(cell);
        }
    }

    void EndRound()
    {
        foreach (GameObject cell in activeCells)
        {
            if (cell != null)
            {
                CellController ctrl = cell.GetComponent<CellController>();
                ctrl.ReportSurvival(false); // No fueron eliminadas
            }
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Puntaje: " + score;
    }

    public void RegisterMemory(CellData data)
    {
        memory.Add(data);
    }

    void ClearCells()
    {
        foreach (GameObject cell in activeCells)
        {
            if (cell != null)
                Destroy(cell);
        }
        activeCells.Clear();
    }
}
