﻿using IPlugins;

namespace Sample.Plugin;

public class Operation : IPlugin
{
    public string Name => "Add";

    public double Calculate(double a, double b)
    {
        return a + b;
    }
}
