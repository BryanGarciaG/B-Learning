using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using BLearning.Models.Negocio;

namespace BLearning.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            List<string> tiposUser = new List<string>();
            tiposUser.Add("Teacher");
            tiposUser.Add("Administrator");
            ViewData["tiposUser"] = new SelectList(tiposUser, "tipoUser");
            return View();
        }

        LogicPersona objPersonaEntidad = new LogicPersona();

        public ActionResult  loginEstudiantes()
        {
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();

            if (Request.Cookies["ups"] != null)
            {
                string usuario = Server.HtmlEncode(Request.Cookies["ups"]["xx1"]);
                string contraseña =Server.HtmlEncode(Request.Cookies["ups"]["xx2"]);
                string rol = Server.HtmlEncode(Request.Cookies["ups"]["rol"]);
                string contrax = _objSeguridad.Decrypt(contraseña);
                string usuax = _objSeguridad.Decrypt(usuario);
                string rolx = _objSeguridad.Decrypt(rol);
               
                try
                {
                    return valiLoginEstudiantes(usuax, contrax, rolx);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                string op = _objSeguridad.Encrypt("b");
                return RedirectToAction("Oops", "Login", new { @area = "", N= op });
            }
 

        }

        public ActionResult valiLoginEstudiantes(string usuario, string contra, string tipoUsu)
        {
            PersonaEntidad objPersona = new PersonaEntidad();
            switch (tipoUsu)
            {
                case "Estudiantes":
                    objPersona = objPersonaEntidad.loginAlumno(usuario);
                    break;
            }
            if (objPersona != null && objPersona.idPersona != 0)
            {
                switch (tipoUsu)
                {
                    case "Estudiantes":
                        Session["personaLogin"] = objPersona;
                        return RedirectToAction("Inicio", "Estudiante", new { @area = "" });
                    default:
                        break;
                }
            }

            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();

            var op = _objSeguridad.Encrypt("a");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });
        }

        public ActionResult Oops(string N)
        {
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Decrypt(N);

            if (op == "a")
            {
                ViewData["nota"] = "Your credentials are incorrect or you may not have access to this system.";
            }
            else if (op == "b")
            {
                ViewData["nota"] = "You are not logged in.";
            }
            else if (op == "c")
            {
                ViewData["nota"] = "Your session has expired.";
            }else
            {
                ViewData["nota"] = "docenteAdministrador";
            }

            return View();
        }


        [HttpPost]
        public ActionResult valiLogin(string usuario, string contra, string tipoUsu)
        {
            PersonaEntidad objPersona = new PersonaEntidad();
            switch (tipoUsu)
            {
                case "Teacher":
                    objPersona = objPersonaEntidad.loginDocente(usuario, contra, "Docente");
                    break;
                case "Administrator":
                    objPersona = objPersonaEntidad.loginAdministrador(usuario, contra, "Administradores");
                    break;
                default:
                    break;
            }
            if (objPersona != null && objPersona.idPersona != 0)
            {
                switch (tipoUsu)
                {
                    case "Teacher":
                        Session["personaLogin"] = objPersona;
                        return Json(tipoUsu);
                    case "Administrator":
                        Session["personaLogin"] = objPersona;
                        return Json(tipoUsu);
                    default:
                        break;
                }
            }
            return Json("ERROR");
        }
        
        public ActionResult logOut()
        {
            if (Session["personaLogin"] == null)
            {
                return RedirectToAction("Index", "Login");

            }
            PersonaEntidad objPersona = (PersonaEntidad)Session["personaLogin"];
            LogicAcceso _objLogicAcceso = new LogicAcceso();
            _objLogicAcceso.ingresarAcceso(0, DateTime.Now, "Salida", objPersona.idPersona, 3);
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Index", "Login");
        }
	}
}