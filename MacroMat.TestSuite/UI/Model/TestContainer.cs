using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MacroMat.TestSuite.Tests;

namespace MacroMat.TestSuite.UI;

public class TestContainer
{
    public string Name { get; }
    public string FullName { get; }
    public IReadOnlyCollection<TestInfo> Tests { get; }
    public IReadOnlyCollection<TestContainer> NestedContainers { get; }

    private TestContainer(string name, string fullName, IEnumerable<TestInfo> tests, IEnumerable<TestContainer> nestedContainers)
    {
        Name = name;
        FullName = fullName;
        NestedContainers = nestedContainers.ToArray().AsReadOnly();
        Tests = tests.ToArray().AsReadOnly();
    }

    public static TestContainer? From(Type type)
    {
        var containers = new List<TestContainer>();
        
        foreach (var nested in type.GetNestedTypes())
        {
            var container = From(nested);
            
            if (container != null)
                containers.Add(container);
        }
        
        var tests = new List<TestInfo>();

        foreach (var method in type.GetMethods())
        {
            if (method.GetCustomAttribute<TestAttribute>() != null)
            {
                tests.Add(new TestInfo(method));
            }
        }

        if (tests.Count == 0)
            return null;

        return new TestContainer(type.Name, type.FullName!, tests, containers);
    }
}