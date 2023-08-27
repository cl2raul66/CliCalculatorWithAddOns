using System.Composition;

namespace Plugin.Multiply;

public interface ICalculator
{
    int Multiply(int x, int y);
}

[Export(typeof(ICalculator))]
public class Multiplier : ICalculator
{
    public int Multiply(int x, int y)
    {
        return x * y;
    }
}