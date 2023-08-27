using System.Composition;

namespace Sample.Plugin;

public interface ICalculator
{
    double Add(double x, double y);
}

[Export(typeof(ICalculator))]
public class Calculator : ICalculator
{
    public double Add(double x, double y)
    {
        return x + y;
    }
}
