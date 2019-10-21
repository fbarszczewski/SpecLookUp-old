using System.Windows;
using SpecLookUp.DAL;

namespace SpecLookUp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!MysqlWorker.DatabaseConnection())
                Close();
        }
    }
}