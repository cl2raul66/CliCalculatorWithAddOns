using Sample.Plugin;
using Sample;
using IPlugins;

var Assemblys = LoadPlugins.Plugins.ToList();

IPlugin? InternalPlugin = new Operation();

if (InternalPlugin is null)
{
    Console.WriteLine("Not modules loaded");
    return;
}

Console.WriteLine("Choose an operation:");
Assemblys.Insert(0, InternalPlugin);
int i = 1;

foreach (var item in Assemblys)
{
    Console.WriteLine(i + ". " + item.Name);
    i++;
}

int? choice = null;

bool choiceEnable = false;
while (!choiceEnable)
{
    choice = Convert.ToInt32(Console.ReadLine());
    if (choice is null || choice < 1 || choice > i)
    {
        Console.WriteLine("Invalid choice.");
    }
    else
    {
        choiceEnable = true;
    }
}

double? x = null;
while (x is null)
{
    Console.WriteLine("Enter first number: ");
    x = Convert.ToInt32(Console.ReadLine());
    if (x is null)
    {
        Console.WriteLine("The first number is not number");
    }
}

double? y = null;
while (y is null)
{
    Console.WriteLine("Enter second number: ");
    y = Convert.ToInt32(Console.ReadLine());
    if (y is null)
    {
        Console.WriteLine("The second number is not number");
    }
}

Console.WriteLine($"Result: {Assemblys[(int)choice! - 1].Calculate((double)x!, (double)y!)}");