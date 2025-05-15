using UnityEngine;

public class CellController : MonoBehaviour
{
    public EvolutionManager evolutionManager;
    public Genome genome;

    private SpriteRenderer sr;
    private bool clicked = false;

    private float speed = 0f;
    private float maxSpeed = 0f;
    private Vector2 velocity;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ApplyGenome();
    }

    public void ApplyGenome()
    {
        float[] outputs = genome.net.FeedForward(new float[] { 1f });

        // Color
        float r = Mathf.Clamp01((outputs[0] + 1f) / 2f);
        float g = Mathf.Clamp01((outputs[1] + 1f) / 2f);
        float b = Mathf.Clamp01((outputs[2] + 1f) / 2f);
        sr.color = new Color(r, g, b);

        // Size (0.3 a 1.5)
        float size = Mathf.Lerp(0.3f, 1.5f, (outputs[3] + 1f) / 2f);
        transform.localScale = Vector3.one * size;

        // Máxima velocidad (0 a 3)
        maxSpeed = Mathf.Lerp(0f, 3f, (outputs[4] + 1f) / 2f);

        speed = 0f; // Arrancan quietos
        velocity = Vector2.zero;
    }

    void Update()
    {
        if (clicked) return;

        // Huir del mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dirAwayFromMouse = ((Vector2)transform.position - (Vector2)mouseWorldPos).normalized;

        // Acelerar gradualmente hasta maxSpeed
        speed = Mathf.MoveTowards(speed, maxSpeed, 1f * Time.deltaTime);

        // Cambiar dirección suavemente hacia huida
        velocity = Vector2.Lerp(velocity, dirAwayFromMouse * speed, 0.1f);

        transform.Translate(velocity * Time.deltaTime);
    }

    void OnMouseDown()
    {
        if (clicked) return;

        clicked = true;
        genome.fitness -= 1f; // Penaliza ser clickeada
        evolutionManager.gameManager.AddScore();
        Destroy(gameObject);
    }

    public void Survived()
    {
        if (!clicked)
        {
            genome.fitness += 1f;
        }
    }
}
