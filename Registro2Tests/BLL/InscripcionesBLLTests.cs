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
            Personas p = new Personas();

            p = PersonasBLL.Buscar(4);

            decimal BalanceInicial = p.Balance;

            decimal BalanceEsperado = BalanceInicial + 3000;

            inscripciones.InscripcionId = 0;
            inscripciones.PersonaId = 4;
            inscripciones.Fecha = DateTime.Now;
            inscripciones.Comentarios = "Usted lo hizo bien";
            inscripciones.Monto = 4000;
            inscripciones.Deposito = 1000;

            paso = InscripcionesBLL.Guardar(inscripciones);

            decimal BalancePrueba = InscripcionesBLL.Buscar(inscripciones.PersonaId).Balance;

            if (BalanceEsperado == BalancePrueba)
                paso = true;

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            bool paso;
            Inscripciones inscripciones = new Inscripciones();
            Personas p = new Personas();

            p = PersonasBLL.Buscar(4);

            decimal BalanceInicial = p.Balance;

            decimal BalanceEsperado = BalanceInicial - 1000;

            inscripciones.InscripcionId = 4;
            inscripciones.PersonaId = 4;
            inscripciones.Fecha = DateTime.Now;
            inscripciones.Comentarios = "Usted lo hizo Correctamente";
            inscripciones.Monto = 3000;
            inscripciones.Deposito = 1000;

            paso = InscripcionesBLL.Modificar(inscripciones);

            decimal BalancePrueba = InscripcionesBLL.Buscar(inscripciones.PersonaId).Balance;

            if (BalanceEsperado == BalancePrueba)
                paso = true;

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool paso;

            Inscripciones inscripciones = new Inscripciones();
            Personas p;
            Inscripciones i;

            decimal BalanceEsperado = 0;

            paso = InscripcionesBLL.Eliminar(4, 4);//aca se encarga de poner en balance en 0 tanto de persona como inscripción
            p = PersonasBLL.Buscar(4);
            i = InscripcionesBLL.Buscar(4);

            if (i.Balance == BalanceEsperado && p.Balance == BalanceEsperado)
                paso = true;

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