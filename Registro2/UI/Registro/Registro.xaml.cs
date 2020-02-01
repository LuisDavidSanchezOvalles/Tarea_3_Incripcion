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

namespace Registro2.UI.Registro
{
    /// <summary>
    /// Interaction logic for Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        const int COSTO = 4600;
        public Registro()
        {
            InitializeComponent();
            BalanceTextBox.Text = "4600";
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas persona = new Personas();
            Inscripciones inscripcion = new Inscripciones();
            bool paso = false;

            if (!validar())
                return;

            persona = llenaClaseP();
            inscripcion = llenaClaseI();


            if (Convert.ToInt32(IdTextBox.Text) == 0)
            {
                paso = PersonasBLL.Guardar(persona);
                inscripcion = EnlazarLlave(inscripcion);
                paso = InscripcionesBLL.Guardar(inscripcion);
            }
            else
            {
                if (!existeEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (InscripcionCheckBox.IsChecked == true)
                {
                    persona = aumentarBalance(persona);
                    paso = PersonasBLL.Modificar(persona);
                    paso = InscripcionesBLL.Guardar(inscripcion);
                }
                else
                    paso = PersonasBLL.Modificar(persona);
            }

            if (paso)
            {
                limpiar();
                MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas persona = new Personas();
            int.TryParse(IdTextBox.Text, out id);

            limpiar();

            persona = PersonasBLL.Buscar(id);

            if (persona != null)
            {
                MessageBox.Show("Persona Encontrada");
                llenaCampo(persona);
            }
            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IdTextBox.Text, out id);

            limpiar();

            if (PersonasBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se puede eliminar una persona que no existe");
        }

        private bool existeEnLaBaseDeDatos()
        {

            Personas persona = PersonasBLL.Buscar(Convert.ToInt32(IdTextBox.Text));
            return (persona != null);
        }

        private void limpiar()
        {
            IdTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            BalanceTextBox.Text = Convert.ToString(COSTO);
        }

        private void llenaCampo(Personas persona)
        {
            IdTextBox.Text = Convert.ToString(persona.PersonaId);
            NombreTextBox.Text = persona.Nombres;
            TelefonoTextBox.Text = persona.Telefono;
            CedulaTextBox.Text = persona.Cedula;
            DireccionTextBox.Text = persona.Direccion;
            FechaDatePicker.SelectedDate = persona.FechaNacimiento;
            BalanceTextBox.Text = Convert.ToString(persona.Balance);
        }

        private Personas llenaClaseP()
        {
            Personas persona = new Personas();
            persona.PersonaId = Convert.ToInt32(IdTextBox.Text);
            persona.Nombres = NombreTextBox.Text;
            persona.Telefono = TelefonoTextBox.Text;
            persona.Cedula = CedulaTextBox.Text;
            persona.Direccion = DireccionTextBox.Text;
            persona.FechaNacimiento = (DateTime)FechaDatePicker.SelectedDate;
            persona.Balance = Convert.ToInt32(BalanceTextBox.Text);

            return persona;
        }

        private Inscripciones llenaClaseI()
        {
            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = 0;
            inscripcion.Fecha = DateTime.Now;
            inscripcion.PersonaId = Convert.ToInt32(IdTextBox.Text); //posible
            inscripcion.Comentarios = "";
            inscripcion.Monto = 0;
            inscripcion.Balance = COSTO;

            return inscripcion;
        }

        private Inscripciones EnlazarLlave(Inscripciones inscripcion)
        {
            inscripcion.PersonaId = PersonasBLL.UltimoRegistro();

            return inscripcion;
        }

        private bool validar()
        { //como en los campos numericos no se usan decimales no se aceptan los puntos

            bool paso = true;

            //PersonaId
            if (string.IsNullOrWhiteSpace(IdTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < IdTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(IdTextBox.Text[i]) || Convert.ToInt32(IdTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            //Nombre
            if (NombreTextBox.Text == string.Empty)
                paso = false;

            //Telefono
            if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text.Replace("-", "")))
                paso = false;
            else
            {
                for (int i = 0; i < TelefonoTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(TelefonoTextBox.Text[i]) || Convert.ToInt32(TelefonoTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            //Direccion
            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
                paso = false;

            //Cedula
            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text.Replace("-", "")))
                paso = false;
            else
            {
                for (int i = 0; i < CedulaTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(CedulaTextBox.Text[i]))
                        paso = false;
                }
            }

            if (paso == false)
                MessageBox.Show("Datos invalidos");

            return paso;
        }

        private Personas aumentarBalance(Personas persona)
        {
            persona.Balance += 4600;

            return persona;
        }
    }
}
