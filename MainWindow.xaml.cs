using System.Windows;

namespace BeeBreeds
{
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            vm.Reset();
        }
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            vm.Data();
            Calculate.IsEnabled = false;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            vm.addInput();
        }
    }
}
