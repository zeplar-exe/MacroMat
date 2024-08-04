using System;

namespace MacroMat.TestSuite.Tests;

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : Attribute
{
    
}

[AttributeUsage(AttributeTargets.Class)]
public class TestContainerAttribute : Attribute
{
    
}