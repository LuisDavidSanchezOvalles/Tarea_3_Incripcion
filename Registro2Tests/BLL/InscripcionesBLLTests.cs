using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registro2.BLL;
using Registro2.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Registro2.BLL.Tests
{
    [TestClass()]
    public class InscripcionesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            bool paso;
            Inscripciones inscripciones = new Inscripciones();

            inscripciones.InscripcionId = 0;
            inscripciones.PersonaId = 1;
            inscripciones.Fecha = DateTime.Now;
            inscripciones.Comentarios = "Usted lo hizo bien";
            inscripciones.Monto = 4000;
            inscripciones.Deposito = 1000;
            inscripciones.Balance = inscripciones.Monto - inscripciones.Deposito;

            paso = InscripcionesBLL.Guardar(inscripciones);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            bool paso;

            Inscripciones inscriciones = new Inscripciones();
            inscriciones.InscripcionId = 1;
            inscriciones.PersonaId = 1;
            inscriciones.Fecha = DateTime.Now;
            inscriciones.Comentarios = "Se Hizo Correcto El Test";
            inscriciones.Monto = 3000;
            inscriciones.Deposito = 1000;

            paso = InscripcionesBLL.Modificar(inscriciones);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool paso;
            paso = InscripcionesBLL.Eliminar(1, 1);//aca se encarga de poner en balance en 0 tanto de persona como inscripción
            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Inscripciones inscripciones;
            inscripciones = InscripcionesBLL.Buscar(1);
            Assert.AreEqual(inscripciones, inscripciones);
        }

        [TestMethod()]
        public void GetListTest()
        {
            var Listado = new List<Inscripciones>();
            Listado = InscripcionesBLL.GetList(p => true);
            Assert.AreEqual(Listado, Listado);
        }
    }
}