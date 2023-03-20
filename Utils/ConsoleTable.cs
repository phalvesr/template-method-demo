using System.Reflection;
using NSubstitute;

namespace TemplateMethodDemo.Utils;

public static class ConsoleTable
{
    internal static void Print<T>(IReadOnlyDictionary<Guid, T> dictionary, params string[] exclude)
    {
        var columns = dictionary.First().Value!
            .GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x =>
            {
                if (exclude.Length == 0)
                {
                    return true;
                }

                return !exclude.Contains(x.Name);
            })
            .Select(x => x.Name).ToArray();
        
        var table = new ConsoleTables.ConsoleTable(columns);
        
        foreach (var (key, value) in dictionary)
        {
            var properties = value!.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x =>
                {
                    if (exclude.Length == 0)
                    {
                        return true;
                    }

                    return !exclude.Contains(x.Name);
                })
                .Select(x => value.GetType().GetProperty(x.Name)!.GetValue(value)!)
                .ToArray();

            table.AddRow(properties);
        }
        
        table.Write();
    }
}