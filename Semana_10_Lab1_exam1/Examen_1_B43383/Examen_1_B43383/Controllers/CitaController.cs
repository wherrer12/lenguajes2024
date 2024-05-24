using Microsoft.AspNetCore.Mvc;
using Examen_1_B43383.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Build.Framework;

namespace Examen_1_B43383.Controllers
{
    public class CitaController : Controller
    {
        private readonly DbContextCita _context = null;

        public CitaController(DbContextCita context)
        {

            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var listado = await _context.Citas.ToListAsync();

            return View(listado);
        }

        private Cita ValidarUsuario(Cita temp)
        {
            Cita autorizado = null;
            var user = _context.Citas.FirstOrDefault(u => u.Email == temp.Email);

            if (user != null)
            {
                if (user.Placa.Equals(temp.Placa))
                {
                    autorizado = user;
                }
            }

            return autorizado;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Cita citas)
        {
            if (citas == null)//Validacion de datos
            {
                return View();
            }
            else
            {
                citas.Id=0;

                //Validacion de cita
                var cita = await _context.Citas.FirstOrDefaultAsync(r => r.FechaHoraRevision == citas.FechaHoraRevision);
                if (cita != null)
                {
                    ModelState.AddModelError("Fecha", "Ya existe una cita para esta fecha");
                    return View();
                }

                _context.Citas.Add(citas);
                await _context.SaveChangesAsync();
                EnviarEmail(citas);
                return RedirectToAction("Index","Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);
            return View(temp);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(int id,[Bind] Cita citas)
        //{
        //    if (citas != null)
        //    {
        //        return RedirectToAction("Index", "Cita");
        //    }
        //    else
        //    {
        //        var cita = await _context.Citas.FirstOrDefaultAsync(r => r.Id == citas.Id);
        //        if (citas.Observacion == null && citas.Mantenimiento == null && citas.Precio == 0)
        //        {
        //            cita.Placa = citas.Placa;
        //            cita.Cedula = citas.Cedula;
        //            cita.NombreCompleto = citas.NombreCompleto;
        //            cita.Email = citas.Email;
        //            cita.TipoRevision = citas.TipoRevision;
        //            cita.FechaHoraRevision = citas.FechaHoraRevision;
        //            cita.Estado = 'A';
        //            _context.Citas.Update(cita);
        //            await _context.SaveChangesAsync();
        //            ConfirmacionRevision(citas);
        //            return RedirectToAction("Index", "Cita");

        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Cita");
        //        }
        //    }

        //}

        // -------------------- ALTERNATIVA CLASICA EDIT --------------------
        //Actualizar cita con los datos estado, observacion, mantenimiento y precio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,[Bind] Cita citas)
        {

            if (id == citas.Id)
            {
                var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);
                _context.Citas.Remove(temp);
                citas.Estado = 'A';
                _context.Citas.Update(citas);
                await _context.SaveChangesAsync();
                ConfirmacionRevision(citas);
                return RedirectToAction("Index","Cita");

            }
            else
            {
                return NotFound();

            }

            //    ////Generar codigo de editar cita
            //    //if (citas.Id != null)
            //    //{
            //    //    var cita = await _context.Citas.FirstOrDefaultAsync(r => r.Id == citas.Id);
            //    //    if (cita != null)
            //    //    {

            //    //        cita.Estado = 'A';
            //    //        _context.Citas.Update(cita);
            //    //        await _context.SaveChangesAsync();
            //    //        Email email = new Email();
            //    //        email.EnviarObservacion(citas);
            //    //        return RedirectToAction("Index", "Cita");

            //    //    }
            //    //    else
            //    //    {
            //    //        return NotFound();
            //    //    }

            //    //}
            //    //else
            //    //{
            //    //    return View();
            //    //}   

            //    //if (citas == null)
            //    //{
            //    //    return NotFound();
            //    //}
            //    //else
            //    //{
            //    //    var cita = await _context.Citas.FirstOrDefaultAsync(r => r.Id == citas.Id);
            //    //    if (citas.Observacion == null && citas.Mantenimiento == null && citas.Precio == 0 )
            //    //    {
            //    //        //return NotFound();
            //    //        cita.Placa = citas.Placa;
            //    //        cita.Cedula = citas.Cedula;
            //    //        cita.NombreCompleto = citas.NombreCompleto;
            //    //        cita.Email = citas.Email;
            //    //        cita.TipoRevision = citas.TipoRevision;
            //    //        cita.FechaHoraRevision = citas.FechaHoraRevision;
            //    //        cita.Estado = 'A';
            //    //        _context.Citas.Update(cita);
            //    //        await _context.SaveChangesAsync();
            //    //        Email email = new Email();
            //    //        email.EnviarObservacion(citas);
            //    //        return RedirectToAction("Index", "Cita");

            //    //    }
            //    //    else
            //    //    {
            //    //        return RedirectToAction("Index", "Cita");
            //    //    }
            //    //}

            //    //citas.Estado = 'A';
            //    //citas.Observacion = citas.Observacion;
            //    //citas.Mantenimiento = citas.Mantenimiento;
            //    //citas.Precio = citas.Precio;
            //    //_context.Citas.Update(citas);
            //    //await _context.SaveChangesAsync();
            //    //Email email = new Email();
            //    //email.EnviarObservacion(citas);
            //    //return RedirectToAction("Index", "Home");

            }

            // -------------------- ALTERNATIVA PRINCIPAL EDIT --------------------

            //[HttpPost]
            //[ValidateAntiForgeryToken]

            //public async Task<IActionResult> Edit(int id, [Bind] Cita cCitas/*,Diagnostico resultado*/)
            //{

            //    if (cCitas != null)
            //    {
            //        var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);

            //        cCitas.Estado = 'A';
            //        _context.Citas.Update(cCitas);
            //        await _context.SaveChangesAsync();
            //        Email email = new Email();
            //        email.EnviarObservacion(cCitas/*, resultado*/);
            //        return RedirectToAction("Index", "Home");

            //        //cCitas.Estado = 'A';
            //        //_context.Citas.Update(cCitas);
            //        //await _context.SaveChangesAsync();
            //        //Email email = new Email();
            //        //email.EnviarObservacion(cCitas);
            //        //return RedirectToAction("Index");
            //    }
            //    else
            //    {

            //        return NotFound();
            //    }

            //}//Fin metodo Edit httpPost

            // ------------------------------------------------------------------------------------------------

            // -------------------- ALTERNATIVA EDIT --------------------
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Edit([Bind] Cita citas)
            //{
            //    if (citas == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        var cita = await _context.Citas.FirstOrDefaultAsync(r => r.Id == citas.Id);
            //        if (cita == null)
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            cita.Estado = 'A';
            //            _context.Citas.Update(cita);
            //            await _context.SaveChangesAsync();
            //            Email email = new Email();
            //            email.EnviarObservacion(citas);
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //}

            //Metodos de la clase
            //Envio de email inicial

            public bool EnviarEmail(Cita citas)
        {
            try
            {
                bool enviar = true;
                Email email = new Email();
                email.Enviar(citas);
                enviar = true;
                return enviar;

            }
            catch (Exception ex)
            {
                return false;

            }

        }//Fin metodo enviar email

        public bool ConfirmacionRevision(Cita citas)
        {
            try
            {
                bool enviar = true;
                Email email = new Email();
                email.EnviarObservacion(citas);
                enviar = true;
                return enviar;

            }
            catch (Exception ex)
            {
                return false;

            }

        }//Fin metodo enviar email

    }//Fin clase

}//Fin namespace
