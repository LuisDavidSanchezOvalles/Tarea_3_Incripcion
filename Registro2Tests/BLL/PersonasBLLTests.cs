using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registro2.BLL;
using Registro2.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Registro2.BLL.Tests
{
    [TestClass()]
    public class PersonasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            bool paso;
            Personas personas = new Personas();
            personas.PersonaId = 0;
            personas.Nombres = " Luis David ";
            personas.Telefono = " 829566 ";
            personas.Cedula = " 056 ";
            personas.Direccion = " Duarte ";
            personas.FechaNacimiento = DateTime.Now;
            paso = PersonasBLL.Guardar(personas);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            bool paso;
            Personas personas = new Personas();
            personas.PersonaId = 1;
            personas.Nombres = " Luis David Sánchez";
            personas.Telefono = " 8295660909";
            personas.Cedula = " 056 ";
            personas.Direccion = " Duarte ";
            personas.FechaNacimiento = DateTime.Now;
            paso = PersonasBLL.Modificar(personas);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool paso;
            paso = PersonasBLL.Eliminar(1);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Personas personas = new Personas();
            personas = PersonasBLL.Buscar(1);

            Assert.AreEqual(personas, personas);
        }

        [TestMethod()]
        public void GetListTest()
        {
            var listado = new List<Personas>();
            listado = PersonasBLL.GetList(p => true);
            Assert.AreEqual(listado, listado);
        }
    }
}