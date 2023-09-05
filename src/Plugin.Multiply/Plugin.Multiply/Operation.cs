using IPlugins;

namespace Plugin.Multiply;

public class Operation : IPlugin
{
    public string Name => "Multiply";

    public double Calculate(double a, double b)
    {
        return a * b;
    }
}