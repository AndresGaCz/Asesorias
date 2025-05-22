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
    public partial class wpMisAsesorias : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["numControl"] == null)
                {
                    Response.Redirect("wpAcceso.aspx");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("Número de control en sesión: " + Session["numControl"].ToString());
                await CargarMisAsesorias();
            }
        }

        private async Task CargarMisAsesorias(string filtro = "", string estado = "")
        {
            try
            {
                string numControl = Session["numControl"]?.ToString();
                if (string.IsNullOrEmpty(numControl))
                {
                    Response.Redirect("wpAcceso.aspx");
                    return;
                }

                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://localhost:44331/api/asesoria/reporte-global?estudianteNumControl={numControl}";

                    if (!string.IsNullOrEmpty(estado) && estado != "Todos los estados")
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

                            // Crear DataTable con las columnas exactas que necesitas mostrar
                            DataTable dt = new DataTable();
                            dt.Columns.Add("id", typeof(int));
                            dt.Columns.Add("docente_nombre", typeof(string));
                            dt.Columns.Add("materia_nombre", typeof(string));
                            dt.Columns.Add("fecha_formateada", typeof(string));
                            dt.Columns.Add("hora_inicio", typeof(string));
                            dt.Columns.Add("hora_fin", typeof(string));
                            dt.Columns.Add("estado", typeof(string));

                            // El campo "tema" no viene en el JSON, así que lo omitimos o ponemos valor por defecto
                            dt.Columns.Add("tema", typeof(string));

                            foreach (JObject asesoria in asesorias)
                            {
                                DataRow row = dt.NewRow();
                                row["id"] = asesoria["id"].Value<int>();
                                row["docente_nombre"] = asesoria["docente_nombre"].Value<string>();
                                row["materia_nombre"] = asesoria["materia_nombre"].Value<string>();
                                row["fecha_formateada"] = asesoria["fecha_formateada"].Value<string>();
                                row["hora_inicio"] = asesoria["hora_inicio"].Value<string>();
                                row["hora_fin"] = asesoria["hora_fin"].Value<string>();
                                row["estado"] = asesoria["estado"].Value<string>();
                                row["tema"] = "Sin tema especificado"; // Valor por defecto

                                dt.Rows.Add(row);
                            }

                            // Depuración: Verificar datos
                            System.Diagnostics.Debug.WriteLine($"Número de filas en DataTable: {dt.Rows.Count}");
                            foreach (DataRow row in dt.Rows)
                            {
                                System.Diagnostics.Debug.WriteLine($"ID: {row["id"]}, Docente: {row["docente_nombre"]}, Materia: {row["materia_nombre"]}");
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
                System.Diagnostics.Debug.WriteLine("Error en CargarMisAsesorias: " + ex.ToString());
                MostrarAlerta($"Error al cargar asesorías: {ex.Message}");
            }
        }
        protected async void gvAsesorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAsesorias.PageIndex = e.NewPageIndex;
            await CargarMisAsesorias(txtBusqueda.Text, ddlEstado.SelectedValue);
        }

        protected async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarMisAsesorias(txtBusqueda.Text, ddlEstado.SelectedValue);
        }

        protected async void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CargarMisAsesorias(txtBusqueda.Text, ddlEstado.SelectedValue);
        }

        private void MostrarAlerta(string mensaje)
        {
            // Escapar comillas y caracteres especiales
            string mensajeEscapado = mensaje.Replace("'", "\\'").Replace("\"", "\\\"");
            mensajeEscapado = mensajeEscapado.Replace(Environment.NewLine, "\\n");

            // Registrar el script con el mensaje escapado
            string script = $"alert('{mensajeEscapado}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
        }
    }
}