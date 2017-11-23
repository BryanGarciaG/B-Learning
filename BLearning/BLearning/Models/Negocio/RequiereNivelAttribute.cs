using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Negocio;

namespace BLearning.Models.Negocio
{
    public class RequiereNivelAttribute: ActionFilterAttribute
    {
        public RequiereNivelAttribute(Nivel nivel)
        {
            this.nivel = nivel;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            // validamos si la sesion ha sido asignada
            if (context.HttpContext.Session["personaLogin"] != null)
            {
                PersonaEntidad objPersona = context.HttpContext.Session["personaLogin"] as PersonaEntidad;

                if (objPersona.Nivel != this.nivel)
                {
                    // como no es del mismo nivel especificado, lo redireccionamos al index de la app
                    context.Result = new RedirectResult("~/Reportes/cannotAcces");
                }
            }
        }

        public Nivel nivel { get; set; }
    }
}