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
            string modulesPath = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
            if (!Directory.Exists(modulesPath))
            {
                Directory.CreateDirectory(modulesPath);
            }

            foreach (var element in Directory.GetFiles(modulesPath, "Plugin.*.dll"))
            {
                var loadContext = new PluginLoadContext(element);
                Assembly assembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(element)));
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
