using System;
using System.Collections.Generic;
using System.Text;
using Registro2.Entidades;
using Registro2.DLL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Registro2.BLL
{
    public class InscripcionesBLL
    {
        private static bool AfectarBalancePersona(Inscripciones inscripciones)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                inscripciones.Balance = db.Inscripciones.Find(inscripciones.InscripcionId).Monto - db.Inscripciones.Find(inscripciones.InscripcionId).Deposito;

                db.Inscripciones.Find(inscripciones.InscripcionId).Balance += inscripciones.Balance;
                db.Personas.Find(inscripciones.PersonaId).Balance += inscripciones.Balance;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Guardar(Inscripciones inscripciones)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Inscripciones.Add(inscripciones) != null)
                    paso = db.SaveChanges() > 0 && AfectarBalancePersona(inscripciones);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        private static bool AfectarBalancePersonasAlModificar(Inscripciones inscripciones)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if(db.Personas.Find(inscripciones.PersonaId).Balance == 0)
                {
                    inscripciones.Balance = db.Inscripciones.Find(inscripciones.InscripcionId).Monto - db.Inscripciones.Find(inscripciones.InscripcionId).Deposito;

                    db.Inscripciones.Find(inscripciones.InscripcionId).Balance += inscripciones.Balance;
                    db.Personas.Find(inscripciones.PersonaId).Balance += inscripciones.Balance;
                    paso = db.SaveChanges() > 0;
                }
                else
                {
                    db.Inscripciones.Find(inscripciones.InscripcionId).Balance = inscripciones.Monto - inscripciones.Deposito;
                    db.Personas.Find(inscripciones.PersonaId).Balance -= inscripciones.Deposito;
                    paso = db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Inscripciones inscripciones)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
               db.Entry(inscripciones).State = EntityState.Modified;
               paso = db.SaveChanges() > 0 && AfectarBalancePersonasAlModificar(inscripciones);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static bool Eliminar(int id, int personaId)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Inscripciones.Find(id).Balance = 0;
                var Eliminar = db.Personas.Find(personaId).Balance = 0;
                //este se encarga de eliminar el balance

                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static Inscripciones Buscar(int InscripcionId)
        {
            Contexto db = new Contexto();
            Inscripciones inscripcion = new Inscripciones();

            try
            {
                inscripcion = db.Inscripciones.Find(InscripcionId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return inscripcion;
        }
        public static List<Inscripciones> GetList(Expression<Func<Inscripciones, bool>> inscripciones)
        {
            List<Inscripciones> Lista = new List<Inscripciones>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Inscripciones.Where(inscripciones).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }
    }
}
