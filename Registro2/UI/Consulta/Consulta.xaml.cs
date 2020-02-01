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
using Registro2.Entidades;
using System.Linq;

namespace Registro2.UI.Consulta
{
    /// <summary>
    /// Interaction logic for Consulta.xaml
    /// </summary>
    public partial class Consulta : Window
    {
        public Consulta()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var Listado = new List<Personas>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0://Todo
                        Listado = PersonasBLL.GetList(p => true);
                        break;
                    case 1://Id
                        int Id = Convert.ToInt32(CriterioTextBox.Text);
                        Listado = PersonasBLL.GetList(p => p.PersonaId == Id);
                        break;
                    case 2://Nombre
                        Listado = PersonasBLL.GetList(p => p.Nombres.Contains(CriterioTextBox.Text));
                        break;
                    case 3://Cedula
                        Listado = PersonasBLL.GetList(p => p.Cedula.Contains(CriterioTextBox.Text));
                        break;
                    case 4://Direccion
                        Listado = PersonasBLL.GetList(p => p.Direccion.Contains(CriterioTextBox.Text));
                        break;
                }

                Listado = Listado.Where(c => c.FechaNacimiento.Date >= DesdeDataPicker.SelectedDate.Value && c.FechaNacimiento.Date <= HastaDataPicker.SelectedDate.Value).ToList();
            }
            else
            {
                Listado = PersonasBLL.GetList(p => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = Listado;
        }
    }


}
