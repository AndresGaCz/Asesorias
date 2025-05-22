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
    public partial class wpAsesoriasPendientes : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["claveDocente"] == null)
                {
                    Response.Redirect("wpAcceso.aspx");
                }

                await CargarAsesoriasPendientes();
            }
        }

        private async Task CargarAsesoriasPendientes(string filtro = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string claveDocente = Session["claveDocente"].ToString();
                    string url = $"https://localhost:44331/api/asesoria/reporte-global?estado=Pendiente&docente={claveDocente}&soloMateriasDocente=true";

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        url += $"&filtro={filtro}";
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
                MostrarAlerta($"Error al cargar asesorías: {ex.Message}");
            }
        }
        protected async void gvAsesorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAsesorias.PageIndex = e.NewPageIndex;
            await CargarAsesoriasPendientes(txtBusqueda.Text);
        }

        protected async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarAsesoriasPendientes(txtBusqueda.Text);
        }

        protected async void btnAceptar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idAsesoria = btn.CommandArgument;
            await CambiarEstadoAsesoria(idAsesoria, "Aceptada");
        }

        protected async void btnRechazar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idAsesoria = btn.CommandArgument;
            await CambiarEstadoAsesoria(idAsesoria, "Rechazada");
        }

        private async Task CambiarEstadoAsesoria(string idAsesoria, string nuevoEstado)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = new
                    {
                        id = int.Parse(idAsesoria),
                        estado = nuevoEstado
                    };

                    string json = JsonConvert.SerializeObject(data);
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync("https://localhost:44331/api/asesoria/cambiar-estado", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban == 0)
                        {
                            MostrarAlerta($"Asesoría {nuevoEstado.ToLower()} correctamente");
                            await CargarAsesoriasPendientes(txtBusqueda.Text);
                        }
                        else
                        {
                            MostrarAlerta($"Error al cambiar estado: {apiResponse.msg}");
                        }
                    }
                    else
                    {
                        MostrarAlerta("Error al conectar con el servidor");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error: {ex.Message}");
            }
        }

        private void MostrarAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                $"alert('{mensaje}');", true);
        }
    }
}