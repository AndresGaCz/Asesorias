using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Asesorias
{
    public partial class wpSolicitarAsesoria : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["numControl"] == null)
                {
                    Response.Redirect("wpAcceso.aspx");
                }

                await CargarDocentes();
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private async Task CargarDocentes()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:44331/api/docentes/listar");

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban > 0)
                        {
                            JArray docentes = (JArray)apiResponse.datos["docentes"];
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(docentes.ToString());

                            ddlDocente.DataSource = dt;
                            ddlDocente.DataTextField = "docente_nombre";
                            ddlDocente.DataValueField = "docente_clave";
                            ddlDocente.DataBind();
                            ddlDocente.Items.Insert(0, new ListItem("Seleccione un docente", ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al cargar docentes: {ex.Message}";
            }
        }

        protected async void ddlDocente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocente.SelectedValue != "")
            {
                await CargarMateriasDocente(ddlDocente.SelectedValue);
            }
            else
            {
                ddlMateria.Items.Clear();
            }
        }

        private async Task CargarMateriasDocente(string claveDocente)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44331/api/docente/{claveDocente}/materias");

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban > 0)
                        {
                            JArray materias = (JArray)apiResponse.datos["materias"];
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(materias.ToString());

                            ddlMateria.DataSource = dt;
                            ddlMateria.DataTextField = "nombre";
                            ddlMateria.DataValueField = "id";
                            ddlMateria.DataBind();
                            ddlMateria.Items.Insert(0, new ListItem("Seleccione una materia", ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al cargar materias: {ex.Message}";
            }
        }

        protected async void btnSolicitar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                await EnviarSolicitud();
            }
        }

        private bool ValidarFormulario()
        {
            if (ddlDocente.SelectedValue == "")
            {
                lblMensaje.Text = "Seleccione un docente";
                return false;
            }

            if (ddlMateria.SelectedValue == "")
            {
                lblMensaje.Text = "Seleccione una materia";
                return false;
            }

            if (string.IsNullOrEmpty(txtFecha.Text))
            {
                lblMensaje.Text = "Ingrese una fecha";
                return false;
            }

            if (string.IsNullOrEmpty(txtHoraInicio.Text) || string.IsNullOrEmpty(txtHoraFin.Text))
            {
                lblMensaje.Text = "Ingrese hora de inicio y fin";
                return false;
            }

            return true;
        }

        private async Task EnviarSolicitud()
        {
            try
            {
                var asesoria = new
                {
                    estudianteNumControl = Session["numControl"].ToString(),
                    docenteClave = ddlDocente.SelectedValue,
                    materia_id = int.Parse(ddlMateria.SelectedValue),
                    fecha = txtFecha.Text,
                    horaInicio = txtHoraInicio.Text,
                    horaFin = txtHoraFin.Text,
                    
                };

                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(asesoria);
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:44331/api/asesoria/solicitar", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban == 0)
                        {
                            lblMensaje.Text = "Asesoría solicitada correctamente";
                            LimpiarFormulario();
                        }
                        else
                        {
                            lblMensaje.Text = $"Error al solicitar asesoría: {apiResponse.msg}";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Error al conectar con el servidor";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            ddlDocente.SelectedIndex = 0;
            ddlMateria.Items.Clear();
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtHoraInicio.Text = "";
            txtHoraFin.Text = "";
            
        }
    }
}