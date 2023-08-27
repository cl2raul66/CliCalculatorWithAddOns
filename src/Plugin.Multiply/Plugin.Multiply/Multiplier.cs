using System.Composition;

namespace Plugin.Multiply;

public interface IMultiplier
{
    int Multiply(int x, int y);
}

[Export(typeof(IMultiplier))]
public class Multiplier : IMultiplier
{
    public int Multiply(int x, int y)
    {
        return x * y;
    }
}