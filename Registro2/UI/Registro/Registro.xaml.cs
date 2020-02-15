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
using Registro2.UI.Consulta;

namespace Registro2.UI.Registro
{
    /// <summary>
    /// Interaction logic for Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Registro()
        {
            InitializeComponent();
            PersonaIdTextBox.Text = "0";
            BalanceTextBlock.Text = "0";
        }

        private void Limpiar()
        {
            PersonaIdTextBox.Text = "0";
            NombresTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty; ;
            DireccionTextBox.Text = string.Empty;
            BalanceTextBlock.Text = "0";
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private Personas LlenaClase()
        {
            Personas personas = new Personas();

            personas.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text);
            personas.Nombres = NombresTextBox.Text;
            personas.Telefono = TelefonoTextBox.Text;
            personas.Cedula = CedulaTextBox.Text;
            personas.Direccion = DireccionTextBox.Text;
            personas.FechaNacimiento = Convert.ToDateTime(FechaDatePicker.SelectedDate);

            return personas;
        }

        private void LlenaCampo(Personas personas)
        {
            PersonaIdTextBox.Text = Convert.ToString(personas.PersonaId);
            NombresTextBox.Text = personas.Nombres;
            TelefonoTextBox.Text = personas.Telefono;
            CedulaTextBox.Text = personas.Cedula;
            DireccionTextBox.Text = personas.Direccion;
            FechaDatePicker.SelectedDate = personas.FechaNacimiento;
            BalanceTextBlock.Text = Convert.ToString(personas.Balance);
        }

        private bool Validar()
        {
            bool paso = true;

            if(string.IsNullOrWhiteSpace(NombresTextBox.Text))
            {
                MessageBox.Show("No se Permite dejar Campos Vacíos");
                NombresTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text))
            {
                MessageBox.Show("No se Permite dejar Campos Vacíos");
                TelefonoTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text))
            {
                MessageBox.Show("No se Permite dejar Campos Vacíos");
                CedulaTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
            {
                MessageBox.Show("No se Permite dejar Campos Vacíos");
                DireccionTextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Personas personas = PersonasBLL.Buscar(Convert.ToInt32(PersonaIdTextBox));

            return personas != null;
        }

        private bool IdentificarInscripcion(int IdInscripcion)
        {
            bool paso = false;
            Inscripciones inscripcion;
            var listado = new List<Inscripciones>();
            listado = InscripcionesBLL.GetList(p => true);
            int cantidad = listado.Count;

            for (int i = 1; i <= cantidad; i++)
            {
                inscripcion = InscripcionesBLL.Buscar(i);
                if (inscripcion.PersonaId == IdInscripcion)
                {
                    return paso = true;
                }
            }
            return paso;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas personas;
            bool paso = false;

            if (!Validar())
                return;

            personas = LlenaClase();

            if (PersonaIdTextBox.Text == "0")
                paso = PersonasBLL.Guardar(personas);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar porque no existe en la base de datos");
                    return;
                }
                paso = PersonasBLL.Modificar(personas);
            }


            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!");

            }
            else
                MessageBox.Show("No se Pudo Guardar");
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(PersonaIdTextBox.Text, out id);

            Limpiar();

            if (IdentificarInscripcion(id) == true)
            {
                MessageBox.Show("No se puede eliminar este Estudiante porque tiene una inscripción creada.");
            }
            else
            {
                if (PersonasBLL.Eliminar(id))
                    MessageBox.Show("Estudiante Eliminado");
                else
                    MessageBox.Show("No se puede eliminar, porque no existe.");
            }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas personas = new Personas();
            int.TryParse(PersonaIdTextBox.Text, out id);

            Limpiar();

            personas = PersonasBLL.Buscar(id);
            if (personas != null)
            {
                LlenaCampo(personas);
            }
            else
            {
                MessageBox.Show("Estudiante no encontrado");
            }
        }
    }
}
