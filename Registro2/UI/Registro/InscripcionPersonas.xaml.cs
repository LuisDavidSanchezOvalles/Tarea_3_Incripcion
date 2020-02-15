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
        public InscripcionPersonas()
        {
            InitializeComponent();
            InscripcionIdTextBox.Text = "0";
            PersonaIdTextBox.Text = "0";
            FechaDatePicker.SelectedDate = DateTime.Now;
            DepositoTextBox.Text = "0";
        }

        private void Limpiar()
        {
            InscripcionIdTextBox.Text = "0";
            PersonaIdTextBox.IsEnabled = true;
            PersonaIdTextBox.Text = "0";
            ComentariosTextBox.Text = string.Empty;
            DepositoTextBox.Text = "0";
            MontoTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            BalanceTextBlock.Text = "0";
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            PersonaIdTextBox.IsEnabled = true;
            Limpiar();
        }


        private Inscripciones LlenaClase()
        {
            Inscripciones inscripciones = new Inscripciones();

            inscripciones.InscripcionId = Convert.ToInt32(InscripcionIdTextBox.Text);
            inscripciones.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text);
            inscripciones.Fecha = Convert.ToDateTime(FechaDatePicker.SelectedDate);
            inscripciones.Comentarios = ComentariosTextBox.Text;
            inscripciones.Monto = Convert.ToDecimal(MontoTextBox.Text);
            inscripciones.Deposito = Convert.ToDecimal(DepositoTextBox.Text);
            inscripciones.Balance = inscripciones.Monto - inscripciones.Deposito;

            return inscripciones;
        }

        private void LlenaCampo(Inscripciones inscripciones)
        {
            InscripcionIdTextBox.Text = Convert.ToString(inscripciones.InscripcionId);
            PersonaIdTextBox.Text = Convert.ToString(inscripciones.PersonaId);
            FechaDatePicker.SelectedDate = inscripciones.Fecha;
            ComentariosTextBox.Text = inscripciones.Comentarios;
            MontoTextBox.Text = Convert.ToString(inscripciones.Balance);//esto es porque es lo que debe la persona
            DepositoTextBox.Text = Convert.ToString(inscripciones.Deposito);
            BalanceTextBlock.Text = Convert.ToString(inscripciones.Balance);
        }

        private bool Validar()
        {
            bool paso = true;

            if(string.IsNullOrWhiteSpace(InscripcionIdTextBox.Text))
            {
                MessageBox.Show("EL Campo Inscripción ID No Puede Estar Vacío");
                ComentariosTextBox.Focus();
                paso = false;
            }
            else
            {
                try
                {
                    int i = Convert.ToInt32(InscripcionIdTextBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("El Campo Inscripción ID debe tener Numeros");
                    InscripcionIdTextBox.Focus();
                    paso = false;
                }
            }

            if (string.IsNullOrWhiteSpace(ComentariosTextBox.Text))
            {
                MessageBox.Show("EL Campo Comentario No Puede Estar Vacío");
                ComentariosTextBox.Focus();
                paso = false;
            }

            //para que lea solo en numeros y que no este en blanco
            if (string.IsNullOrWhiteSpace(DepositoTextBox.Text))
            {
                MessageBox.Show("EL Campo Deposito No Puede Estar Vacío");
                DepositoTextBox.Focus();
                paso = false;
            }
            else
            {
                try
                {
                    decimal d = Convert.ToDecimal(DepositoTextBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("El Campo deposito debe tener Numeros");
                    DepositoTextBox.Focus();
                    paso = false;
                }
            }

            if (string.IsNullOrWhiteSpace(MontoTextBox.Text))
            {
                MessageBox.Show("EL Campo Monto No Puede Estar Vacío");
                MontoTextBox.Focus();
                paso = false;
            }
            else
            {
                try
                {
                    decimal d = Convert.ToDecimal(MontoTextBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("El Campo deposito debe tener Numeros");
                    MontoTextBox.Focus();
                    paso = false;
                }
            }

            return paso;
        }

        private bool ExisteEnLaBaseDeDatosIncripciones()
        {
            Inscripciones inscripciones = InscripcionesBLL.Buscar(Convert.ToInt32(InscripcionIdTextBox.Text));

            return inscripciones != null;
        }

        private bool ExisteEnLaBaseDeDatosPersonas()
        {
            Personas personas = PersonasBLL.Buscar(Convert.ToInt32(PersonaIdTextBox.Text));

            return (personas != null);
        }

        //Para Verificar que existe el PersonaID en la la Inscripcion
        private bool PersonaIdExisteEnInscripcion()
        {
            bool paso = false;
            Inscripciones inscripciones;
            var listado = new List<Inscripciones>();
            listado = InscripcionesBLL.GetList(p => true);
            int cantidad = listado.Count;

            for (int i = 1; i <= cantidad; i++)
            {
                inscripciones = InscripcionesBLL.Buscar(i);
                if (inscripciones.PersonaId == Convert.ToInt32(PersonaIdTextBox.Text))
                {
                    paso = true;
                }
            }
            return paso;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Inscripciones inscripciones;
            bool paso = false;

            if (!Validar())
                return;

            inscripciones = LlenaClase();

            //aca determina ademas de guardar o modificar si tambien existe en la Base de datos
            if (InscripcionIdTextBox.Text == "0" && ExisteEnLaBaseDeDatosPersonas() == true)
            {
                paso = InscripcionesBLL.Guardar(inscripciones);
            }
            else
            {

                if (!ExisteEnLaBaseDeDatosIncripciones())
                {
                    MessageBox.Show("No se puede modificar porque no existe en la base de datos Inscripción o Persona");
                    return;
                }

                paso = InscripcionesBLL.Modificar(inscripciones);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!");
            }
            else
                MessageBox.Show("No fue posible guardar!!");
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(InscripcionIdTextBox.Text, out id);
            int PersonaId;
            int.TryParse(PersonaIdTextBox.Text, out PersonaId);

            Limpiar();

            if (InscripcionesBLL.Eliminar(id, PersonaId))
                MessageBox.Show("Balance de Inscripción Eliminado");
            else
                MessageBox.Show("No se puede eliminar, porque no existe.");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(InscripcionIdTextBox.Text, out id);
            Inscripciones inscripciones = new Inscripciones();

            Limpiar();

            inscripciones = InscripcionesBLL.Buscar(id);

            if (inscripciones != null)
            {
                PersonaIdTextBox.IsEnabled = false;
                LlenaCampo(inscripciones);
            }
            else
                MessageBox.Show("Incripcion no Encontrada");
        }
    }
}
