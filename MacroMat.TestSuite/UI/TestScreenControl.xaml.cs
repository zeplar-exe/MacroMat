using System;
using System.Windows.Controls;
using MacroMat.TestSuite.Core;

namespace MacroMat.TestSuite.UI;

public partial class TestScreenControl : UserControl
{
    public TestScreenControl(TestInfo testInfo, Macro macro)
    {
        InitializeComponent();
        
        var screen = new TestScreen(this);

        testInfo.Callable.Invoke(screen, macro);
    }
}