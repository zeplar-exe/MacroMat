using System.Windows;
using System.Windows.Controls;
using MacroMat.TestSuite.UI;

namespace MacroMat.TestSuite.Core;

public class TestScreen
{
    private TestScreenControl Control { get; }
    
    public TestScreen(TestScreenControl control)
    {
        Control = control;
    }

    public void SetInstructions(string instructions)
    {
        
    }

    public void CompleteActStage()
    {
        
    }

    public Button CreateButton(string text)
    {
        return new Button();
    }

    public TextBox CreateTextBox(string? label = null)
    {
        return new TextBox();
    }

    public Point GetGlobalElementPosition(FrameworkElement element)
    {
        return element.PointToScreen(new Point(0, 0));
    }
}