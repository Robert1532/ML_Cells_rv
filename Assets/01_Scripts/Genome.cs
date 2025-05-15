[System.Serializable]
public class Genome
{
    public NeuralNetwork net;
    public float fitness;

    public Genome()
    {
        net = new NeuralNetwork();
        fitness = 0;
    }

    public Genome Clone()
    {
        Genome clone = new Genome();
        clone.net = net.Clone();
        clone.fitness = 0;
        return clone;
    }
}
