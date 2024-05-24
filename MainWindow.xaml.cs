using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManager_Graf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Ссылка на главное окно </summary>
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this; // Запоминаем главное окно
            DataContext = new VM_Pages(); // В качестве контекста, указываем модель ViewModelPages
        }
    }
}
