using System;
using System.Reflection;
using MacroMat.TestSuite.Core;

namespace MacroMat.TestSuite.UI;

public class TestInfo
{
    public string Name { get; }
    public string FullName { get; }
    public Action<TestScreen, Macro> Callable { get; }
    public bool IsSelected { get; set; }

    public TestInfo(MethodInfo methodInfo)
    {
        if (methodInfo.ReflectedType == null)
            throw new Exception();

        var parameters = methodInfo.GetParameters();

        if (parameters.Length != 2)
            throw new Exception();

        if (parameters[0].ParameterType != typeof(TestScreen))
            throw new Exception();
        
        if (parameters[1].ParameterType != typeof(Macro))
            throw new Exception();

        if (!(methodInfo.ReflectedType.IsAbstract && methodInfo.ReflectedType.IsSealed)) // if not static
            throw new Exception();
        
        Name = methodInfo.Name;
        FullName = $"{methodInfo.ReflectedType.FullName}.{methodInfo.Name}";
        Callable = (screen, macro) =>
        {
            methodInfo.Invoke(null, new object?[] { screen, macro });
        };
    }
}