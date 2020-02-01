using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Registro2.BLL;
using Registro2.DLL;
using Registro2.Entidades;
using System.Linq;

namespace Registro2.UI.Consulta
{
    /// <summary>
    /// Interaction logic for ConsultarInscripcion.xaml
    /// </summary>
    public partial class ConsultarInscripcion : Window
    {
        public ConsultarInscripcion()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Inscripciones>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //todo
                        listado = InscripcionesBLL.GetList(p => true);
                        MessageBox.Show("Todo");
                        break;
                    case 1: //ID
                        try
                        {
                            int id = Convert.ToInt32(CriterioTextBox.Text);
                            listado = InscripcionesBLL.GetList(p => p.InscripcionId == id);
                            MessageBox.Show("ID");
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    default:
                        MessageBox.Show("No existe esa opción en el Filtro");
                        break;

                }
                //fecha
                listado = listado.Where(p => p.Fecha.Date >= DesdeDatePicker.SelectedDate.Value && p.Fecha.Date <= HastaDatePicker.SelectedDate.Value).ToList();
            }
            else
            {
                listado = InscripcionesBLL.GetList(p => true);
            }
            ConsultaDataGrid.ItemsSource= null;
            ConsultaDataGrid.ItemsSource = listado;
        }
    }
}
