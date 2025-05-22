using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Asesorias
{
    public partial class wpHistorialAsesorias : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["claveDocente"] == null)
                {
                    Response.Redirect("wpAcceso.aspx");
                }

                await CargarHistorialAsesorias();
            }
        }

        private async Task CargarHistorialAsesorias(string filtro = "", string estado = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string claveDocente = Session["claveDocente"].ToString();
                    string url = $"https://localhost:44331/api/asesoria/reporte-global?docente={claveDocente}&soloMateriasDocente=true";

                    if (!string.IsNullOrEmpty(estado))
                    {
                        url += $"&estado={estado}";
                    }

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban > 0)
                        {
                            JArray asesorias = (JArray)apiResponse.datos["asesorias"];
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(asesorias.ToString());

                            if (!string.IsNullOrEmpty(filtro))
                            {
                                DataView dv = dt.DefaultView;
                                dv.RowFilter = $"estudiante_nombre LIKE '%{filtro}%' OR materia_nombre LIKE '%{filtro}%'";
                                dt = dv.ToTable();
                            }

                            gvAsesorias.DataSource = dt;
                            gvAsesorias.DataBind();
                        }
                        else
                        {
                            gvAsesorias.DataSource = null;
                            gvAsesorias.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error al cargar historial: {ex.Message}");
            }
        }

        protected async void gvAsesorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAsesorias.PageIndex = e.NewPageIndex;
            await CargarHistorialAsesorias(txtBusqueda.Text, ddlEstado.SelectedValue);
        }

        protected async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarHistorialAsesorias(txtBusqueda.Text, ddlEstado.SelectedValue);
        }

        protected async void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CargarHistorialAsesorias(txtBusqueda.Text, ddlEstado.SelectedValue);
        }

        private void MostrarAlerta(string mensaje)
        {
            string scriptMensaje = mensaje.Replace("'", "\\'").Replace("\r\n", "\\n");
            string script = $"alert('{scriptMensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
        }
    }
}