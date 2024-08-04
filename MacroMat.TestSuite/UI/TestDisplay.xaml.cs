using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MacroMat.TestSuite.UI;

public partial class TestDisplay : Window
{
    private Dictionary<TestAnswer, int> Answers { get; }
    private Queue<TestInfo> Tests { get; }
    private Macro Macro { get; }
    
    public TestDisplay(IEnumerable<TestInfo> tests)
    {
        Answers = new Dictionary<TestAnswer, int>();
        Tests = new Queue<TestInfo>(tests);
        Macro = new Macro();
        
        InitializeComponent();
        
        ShowNext();
    }

    private void ShowNext(TestAnswer answer = TestAnswer.None)
    {
        if (!Tests.TryDequeue(out var test))
        {
            Content = new Results(Answers);
            
            return;
        }

        if (answer != TestAnswer.None)
        {
            if (!Answers.ContainsKey(answer))
                Answers[answer] = 0;
            
            Answers[answer] = 1;
        }
        
        v_ContentControl.Content = new TestScreenControl(test, Macro);
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Pass(object sender, RoutedEventArgs e)
    {
        ShowNext(TestAnswer.Pass);
    }

    private void Fail(object sender, RoutedEventArgs e)
    {
        ShowNext(TestAnswer.Fail);
    }

    private void Skip(object sender, RoutedEventArgs e)
    {
        ShowNext(TestAnswer.Skip);
    }
}