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

namespace Registro2.UI.Registro
{
    /// <summary>
    /// Interaction logic for InscripcionPersonas.xaml
    /// </summary>
    public partial class InscripcionPersonas : Window
    {
        const int COSTO = 2600;
        public InscripcionPersonas()
        {
            InitializeComponent();
            FechaDatePicker.SelectedDate = DateTime.Now;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(BalanceTextBox.Text) <= 0)
                return;

            Inscripciones inscripcion;
            bool paso = false;

            if (!validar())
                return;

            if (existeEnLaBaseDeDatos())
            {
                actualizarBalance();
                inscripcion = llenaClaseI();
                paso = InscripcionesBLL.Modificar(inscripcion);
            }
            else
            {
                MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
            Inscripciones inscripcion = new Inscripciones();
            int.TryParse(IdInscripcionTextBox.Text, out id);

            limpiar();

            inscripcion = InscripcionesBLL.Buscar(id);

            if (inscripcion != null)
            {
                MessageBox.Show("Persona Encontrada");
                llenaCampo(inscripcion);
            }
            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IdInscripcionTextBox.Text, out id);

            if (eliminarInscripcion(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se puede eliminar una persona que no existe");

            limpiar();
        }

        private void limpiar()
        {
            IdInscripcionTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            PersonaIdTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            BalanceTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
        }

        private void llenaCampo(Inscripciones inscripcion)
        {
            IdInscripcionTextBox.Text = Convert.ToString(inscripcion.InscripcionId);
            FechaDatePicker.SelectedDate = inscripcion.Fecha;
            PersonaIdTextBox.Text = Convert.ToString(inscripcion.PersonaId);
            NombreTextBox.Text = nombrePersona(inscripcion.PersonaId);
            BalanceTextBox.Text = Convert.ToString(inscripcion.Balance);
            ComentariosTextBox.Text = inscripcion.Comentarios;
        }

        private Inscripciones llenaClaseI()
        {
            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = Convert.ToInt32(IdInscripcionTextBox.Text);
            inscripcion.Fecha = DateTime.Now;
            inscripcion.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text); //posible
            inscripcion.Balance = Convert.ToInt32(BalanceTextBox.Text) - Convert.ToInt32(MontoTextBox.Text);
            inscripcion.Monto = aumentarMonto();
            inscripcion.Comentarios = ComentariosTextBox.Text;

            return inscripcion;
        }

        public string nombrePersona(int PersonaId)
        {
            Personas persona = PersonasBLL.Buscar(PersonaId);

            return persona.Nombres;
        }

        public int aumentarMonto()
        {
            Inscripciones montoViejo = InscripcionesBLL.Buscar(Convert.ToInt32(IdInscripcionTextBox.Text));
            int monto = Convert.ToInt32(MontoTextBox.Text);
            monto += montoViejo.Monto;

            return monto;
        }

        public void actualizarBalance()
        {
            Personas persona = PersonasBLL.Buscar(Convert.ToInt32(PersonaIdTextBox.Text));
            persona.Balance -= Convert.ToInt32(MontoTextBox.Text);
            PersonasBLL.Modificar(persona);
        }

        public bool eliminarInscripcion(int id)
        {
            Inscripciones inscripcion = InscripcionesBLL.Buscar(id);
            Personas persona = PersonasBLL.Buscar(inscripcion.PersonaId);

            persona.Balance -= inscripcion.Balance;

            PersonasBLL.Modificar(persona);

            return InscripcionesBLL.Eliminar(id);
        }

        private bool existeEnLaBaseDeDatos()
        {

            Inscripciones inscripcion = InscripcionesBLL.Buscar(Convert.ToInt32(IdInscripcionTextBox.Text));
            return (inscripcion != null);
        }

        private bool validar()
        { //como en los campos numericos no se usan decimales no se aceptan los puntos

            bool paso = true;

            //InscripcionId
            if (string.IsNullOrWhiteSpace(IdInscripcionTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < IdInscripcionTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(IdInscripcionTextBox.Text[i]) || Convert.ToInt32(IdInscripcionTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            //Monto
            if (string.IsNullOrWhiteSpace(MontoTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < MontoTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(MontoTextBox.Text[i]) || Convert.ToInt32(MontoTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            if (paso == false)
                MessageBox.Show("Datos invalidos");

            return paso;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
    }
}
