using System.Reflection;

Console.WriteLine("Bienvenido a CliCalculadora!");
while (true)
{
    Console.WriteLine("Selecciona una opción:");
    Console.WriteLine("1. Sumar");
    string[] dlls = Directory.GetFiles(".", "Plugin.*.dll");
    for (int i = 0; i < dlls.Length; i++)
    {
        Console.WriteLine($"{i + 2}. {Path.GetFileNameWithoutExtension(dlls[i])}");
    }
    Console.WriteLine($"{dlls.Length + 2}. Salir");
    string? opcion = Console.ReadLine();
    if (opcion is null || opcion == (dlls.Length + 2).ToString())
    {
        break;
    }
    Console.Write("Ingresa el primer operador: ");
    double a = double.Parse(Console.ReadLine() ?? "0");
    Console.Write("Ingresa el segundo operador: ");
    double b = double.Parse(Console.ReadLine() ?? "0");
    double resultado = 0;
    if (opcion == "1")
    {
        resultado = Sumar(a, b);
    }
    else
    {
        int index = int.Parse(opcion!) - 2;
        if (index >= 0 && index < dlls.Length)
        {
            resultado = CargarOperacion(dlls[index], a, b);
        }
        else
        {
            Console.WriteLine("Opción inválida.");
            continue;
        }
    }
    Console.WriteLine();
    Console.WriteLine($"El resultado es: {resultado}");
}


static double Sumar(double a, double b)
{
    return a + b;
}

static double CargarOperacion(string dll, double a, double b)
{
    Assembly assembly = Assembly.LoadFrom(dll);
    Type? type = assembly.GetType(Path.GetFileNameWithoutExtension(dll) + ".Operacion");
    MethodInfo? methodInfo = type?.GetMethod("Calcular");
    object instance = Activator.CreateInstance(type);
    return (double)methodInfo.Invoke(instance, new object[] { a, b });
}