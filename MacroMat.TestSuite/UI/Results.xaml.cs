using System.Collections.Generic;
using System.Windows.Controls;

namespace MacroMat.TestSuite.UI;

public partial class Results : UserControl
{
    public Results(IDictionary<TestAnswer, int> frequencies)
    {
        InitializeComponent();
    }
}