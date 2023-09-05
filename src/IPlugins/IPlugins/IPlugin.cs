namespace IPlugins;

public interface IPlugin
{
    string Name { get; }
    double Calculate(double a, double b);
}