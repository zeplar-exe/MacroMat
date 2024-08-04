using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
using MacroMat.TestSuite.Core;
using MacroMat.TestSuite.Tests;

namespace MacroMat.TestSuite.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TestContainer> DiscoveredTests { get; set; }
        
        public MainWindow()
        {
            DiscoveredTests = new ObservableCollection<TestContainer>();
            DataContext = this;
        
            LoadTests();
        
            InitializeComponent();
        }
        
        public void RunSelectedTests(object? sender, RoutedEventArgs e)
        {
            var display = new TestDisplay(GetSelectedTests());
            
            display.Show();
        }

        private void LoadTests()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                         .Where(t => t.GetCustomAttribute<TestContainerAttribute>() != null))
            {
                var container = TestContainer.From(type);
            
                if (container != null)
                    DiscoveredTests.Add(container);
            }
        }

        private IEnumerable<TestInfo> GetSelectedTests()
        {
            IEnumerable<TestInfo> Recurse(TestContainer container)
            {
                foreach (var test in container.Tests)
                    yield return test;

                foreach (var nested in container.NestedContainers)
                {
                    foreach (var test in Recurse(nested))
                        yield return test;
                }
            }

            foreach (var container in DiscoveredTests)
            {
                foreach (var test in Recurse(container).Where(t => t.IsSelected))
                    yield return test;
            }
        }
    }
}