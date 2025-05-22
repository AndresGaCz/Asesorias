using System;
using System.Web.UI;

namespace Asesorias
{
    public partial class mpPrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lbMenuPrincipal_Click(object sender, EventArgs e)
        {
            if (Session["claveDocente"] != null)
            {
                // Usuario es docente
                Response.Redirect("wpmenuDocente.aspx");
            }
            else if (Session["numControl"] != null)
            {
                // Usuario es estudiante
                Response.Redirect("wpMenuEstudiante.aspx");
            }
            else
            {
                // No hay sesión iniciada
                Response.Redirect("wpAcceso.aspx");
            }
        }

        protected void lbCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("wpAcceso.aspx");
        }
    }
}