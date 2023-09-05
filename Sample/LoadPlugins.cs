using IPlugins;
using System.Reflection;
using System.Runtime.Loader;
namespace Sample;

public static class LoadPlugins
{
    public static IEnumerable<IPlugin> Plugins
    {
        get
        {
            List<IPlugin> PluginsList = new();
            foreach (var element in Directory.GetFiles(Directory.GetCurrentDirectory(), "Plugin.*.dll"))
            {
                AssemblyLoadContext assemblyLoadContext = new(element);
                Assembly assembly = assemblyLoadContext.LoadFromAssemblyPath(element);
                IPlugin? plugin = Activator.CreateInstance(assembly.GetExportedTypes()[0]) as IPlugin;
                if (plugin is not null)
                {
                    PluginsList.Add(plugin);
                }
            }
            return PluginsList;
        }
    }
}
