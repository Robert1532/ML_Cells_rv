using UnityEngine;

public class CellController : MonoBehaviour
{
    public GameManager manager;
    public System.Collections.Generic.List<CellData> memory;

    private SpriteRenderer sr;
    private Color color;
    private float size;

    private bool clicked = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Machine learning básico: intentar colores que sobrevivieron
        CellData best = GetBestMemory();
        if (best != null)
        {
            color = new Color(best.r, best.g, best.b);
            size = best.size;
        }
        else
        {
            color = new Color(Random.value, Random.value, Random.value);
            size = Random.Range(0.5f, 1.5f);
        }

        sr.color = color;
        transform.localScale = Vector3.one * size;
    }

    void OnMouseDown()
    {
        clicked = true;
        manager.AddScore();
        ReportSurvival(true); // Eliminado
        Destroy(gameObject);
    }

    public void ReportSurvival(bool eliminated)
    {
        manager.RegisterMemory(new CellData
        {
            r = color.r,
            g = color.g,
            b = color.b,
            size = size,
            survived = !eliminated
        });
    }

    CellData GetBestMemory()
    {
        CellData best = null;
        float highestScore = -1f;

        foreach (var data in memory)
        {
            if (data.survived)
            {
                float score = 1f - Mathf.Abs(data.r - Random.value) - Mathf.Abs(data.g - Random.value) - Mathf.Abs(data.b - Random.value);
                if (score > highestScore)
                {
                    best = data;
                    highestScore = score;
                }
            }
        }
        return best;
    }
}
