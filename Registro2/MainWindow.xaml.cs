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
using Registro2.UI.Registro;
using Registro2.UI.Consulta;

namespace Registro2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistroButton_Click(object sender, RoutedEventArgs e)
        {
            Registro r = new Registro();
            r.Show();
        }

        private void ConsultaButton_Click(object sender, RoutedEventArgs e)
        {
            Consulta c = new Consulta();
            c.Show();
        }

        private void ConsultarInscripcion_Click(object sender, RoutedEventArgs e)
        {
            ConsultarInscripcion c = new ConsultarInscripcion();
            c.Show();
        }

        private void InscripcionButton_Click(object sender, RoutedEventArgs e)
        {
            InscripcionPersonas i = new InscripcionPersonas();
            i.Show();
        }
    }
}
