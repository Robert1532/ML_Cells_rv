using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    public int populationSize = 20;
    public List<Genome> population = new List<Genome>();
    public GameManager gameManager;

    void Start()
    {
        InitializePopulation();
    }

    public void InitializePopulation()
    {
        population.Clear();
        for (int i = 0; i < populationSize; i++)
        {
            Genome genome = new Genome();
            genome.net.InitializeRandom();  // Inicializa pesos aleatorios aqu�
            population.Add(genome);
        }
    }

    public void Evolve()
    {
        // Ordena la poblaci�n por fitness descendente
        population.Sort((a, b) => b.fitness.CompareTo(a.fitness));

        List<Genome> newPop = new List<Genome>();

        // El mejor 50% se mantiene y genera hijos mutados para completar la poblaci�n
        for (int i = 0; i < populationSize / 2; i++)
        {
            Genome parent = population[i];
            newPop.Add(parent.Clone());

            Genome child = parent.Clone();
            child.net.Mutate(0.1f);
            newPop.Add(child);
        }

        // Si populationSize es impar, ajusta el tama�o
        if (newPop.Count > populationSize)
            newPop.RemoveAt(newPop.Count - 1);

        population = newPop;
    }
}
