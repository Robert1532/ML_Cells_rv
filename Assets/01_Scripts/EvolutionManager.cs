using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    public int populationSize = 20;
    public List<Genome> population = new List<Genome>();
    public GameManager gameManager;

    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            population.Add(new Genome());
        }
    }

    public void Evolve()
    {
        population.Sort((a, b) => b.fitness.CompareTo(a.fitness));

        List<Genome> newPop = new List<Genome>();

        // El mejor 50% se mantiene y genera hijos mutados
        for (int i = 0; i < populationSize / 2; i++)
        {
            Genome parent = population[i];
            newPop.Add(parent.Clone());

            Genome child = parent.Clone();
            child.net.Mutate(0.1f);
            newPop.Add(child);
        }

        population = newPop;
    }
}
