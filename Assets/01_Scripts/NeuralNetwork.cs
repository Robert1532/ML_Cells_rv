using UnityEngine;

public class NeuralNetwork
{
    private int inputCount = 1;
    private int outputCount = 5; // R, G, B, Tamaño, Velocidad (máx)

    private float[,] weights;

    public NeuralNetwork()
    {
        weights = new float[inputCount, outputCount];
        Randomize();
    }

    public void Randomize()
    {
        for (int i = 0; i < inputCount; i++)
            for (int j = 0; j < outputCount; j++)
                weights[i, j] = Random.Range(-1f, 1f);
    }

    private float Tanh(float x)
    {
        float ePos = Mathf.Exp(x);
        float eNeg = Mathf.Exp(-x);
        return (ePos - eNeg) / (ePos + eNeg);
    }

    public float[] FeedForward(float[] inputs)
    {
        float[] outputs = new float[outputCount];
        for (int i = 0; i < outputCount; i++)
        {
            float sum = 0f;
            for (int j = 0; j < inputCount; j++)
                sum += inputs[j] * weights[j, i];
            outputs[i] = Tanh(sum);
        }
        return outputs;
    }

    public NeuralNetwork Clone()
    {
        NeuralNetwork clone = new NeuralNetwork();
        for (int i = 0; i < inputCount; i++)
            for (int j = 0; j < outputCount; j++)
                clone.weights[i, j] = weights[i, j];
        return clone;
    }

    public void Mutate(float mutationRate = 0.1f)
    {
        for (int i = 0; i < inputCount; i++)
            for (int j = 0; j < outputCount; j++)
                if (Random.value < mutationRate)
                    weights[i, j] += Random.Range(-0.5f, 0.5f);
    }
}
