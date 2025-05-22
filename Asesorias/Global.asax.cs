using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Asesorias
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["nombreAplicacion"] = "Sistema de Asesorías ITP";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["numControl"] = null;
            Session["claveDocente"] = null;
            Session["nombreCompleto"] = null;
            Session["carrera"] = null;
            Session["semestre"] = null;
            Session["tipoUsuario"] = null;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            // Registrar el error
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}