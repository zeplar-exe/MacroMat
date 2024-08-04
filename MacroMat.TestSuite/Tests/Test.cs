using System;
using MacroMat.Extensions;
using MacroMat.TestSuite.Core;

namespace MacroMat.TestSuite.Tests;

[TestContainer]
public static class Test
{
    [Test]
    public static void Test1(TestScreen screen, Macro macro)
    {
        screen.SetInstructions("The test will begin in 2 seconds. " +
                               "Do not manually move your mouse. " +
                               "Pass if the mouse flickers in the top left of the screen.");

        macro.Wait(2000)
             .Action(_ => throw new Exception())
             .MoveMouse((0, 0), (10, 10), (15, 15), (10, 10), (5, 5), (0, 0), (10, 10));

        screen.CompleteActStage();
    }
}