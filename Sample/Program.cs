using System.Reflection;
using System.Composition.Hosting;
using Sample.Plugin;

var configuration = new ContainerConfiguration()
                .WithAssembly(typeof(Program).GetTypeInfo().Assembly);

// Load all DLLs in the current directory
var dlls = Directory.GetFiles(Directory.GetCurrentDirectory(), $"Plugin.*.dll");
foreach (var dll in dlls)
{
    var assembly = Assembly.LoadFile(dll);
    configuration.WithAssembly(assembly);
}

using (var container = configuration.CreateContainer())
{
    Console.WriteLine("Choose an operation:");
    Console.WriteLine("1. Add");

    // Get all exported types that implement ICalculator
    var calculatorTypes = container.GetExports<ICalculator>().Select(c => c.GetType()).Distinct().ToList();

    // Display a menu option for each exported type
    int i = 2;
    foreach (var calculatorType in calculatorTypes)
    {
        var methods = calculatorType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var method in methods)
        {
            if (method.Name != "Add" && method.GetParameters().Length == 2 && method.GetParameters()[0].ParameterType == typeof(int) && method.GetParameters()[1].ParameterType == typeof(int))
            {
                Console.WriteLine(i + ". " + method.Name);
                i++;
            }
        }
    }

    int choice = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter first number: ");
    int x = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter second number: ");
    int y = Convert.ToInt32(Console.ReadLine());

    if (choice == 1)
    {
        var calculator = container.GetExport<ICalculator>();
        Console.WriteLine("Result: " + calculator.Add(x, y));
    }
    else
    {
        // Find the selected operation
        string operation = null;
        i = 2;
        foreach (var calculatorType in calculatorTypes)
        {
            var methods = calculatorType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                if (method.Name != "Add" && method.GetParameters().Length == 2 && method.GetParameters()[0].ParameterType == typeof(int) && method.GetParameters()[1].ParameterType == typeof(int))
                {
                    if (i == choice)
                    {
                        operation = method.Name;
                        break;
                    }
                    i++;
                }
            }
            if (operation != null) break;
        }

        if (operation == null)
        {
            Console.WriteLine("Invalid choice.");
        }
        else
        {
            // Find the calculator that implements the selected operation
            object calculator = null;
            foreach (var calculatorType in calculatorTypes)
            {
                var method = calculatorType.GetMethod(operation);
                if (method != null)
                {
                    calculator = container.GetExports<ICalculator>().First(c => c.GetType() == calculatorType);
                    break;
                }
            }

            if (calculator == null)
            {
                Console.WriteLine(operation + " functionality is not available.");
            }
            else
            {
                // Invoke the selected operation
                var method = calculator.GetType().GetMethod(operation);
                double result = (double)method.Invoke(calculator, new object[] { x, y });
                Console.WriteLine("Result: " + result);
            }
        }
    }
}