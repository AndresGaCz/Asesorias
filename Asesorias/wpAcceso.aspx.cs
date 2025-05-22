using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Asesorias
{
    public partial class wpAcceso : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnAcceder_Click(object sender, EventArgs e)
        {
            if (rblTipoUsuario.SelectedValue == "estudiante")
            {
                await ValidarEstudiante();
            }
            else
            {
                await ValidarDocente();
            }
        }

        private async Task ValidarEstudiante()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var estudiante = new
                    {
                        usuario = txtUsuario.Text,
                        contrasena = txtContrasena.Text
                    };

                    string json = JsonConvert.SerializeObject(estudiante);
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:44331/api/cred/login/estudiante", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban == 1)
                        {
                            Session["numControl"] = apiResponse.datos["num_control"].ToString();
                            Session["nombreCompleto"] = apiResponse.datos["nombre_completo"].ToString();
                            Session["carrera"] = apiResponse.datos["carrera"].ToString();
                            Session["semestre"] = apiResponse.datos["semestre"].ToString();
                            Session["tipoUsuario"] = "estudiante";

                            Response.Redirect("wpMenuEstudiante.aspx");
                        }
                        else
                        {
                            MostrarAlerta("Credenciales incorrectas");
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

        private async Task ValidarDocente()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var docente = new
                    {
                        usuario = txtUsuario.Text,
                        contrasena = txtContrasena.Text
                    };

                    string json = JsonConvert.SerializeObject(docente);
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:44331/api/cred/login/docente", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban == 1)
                        {
                            Session["claveDocente"] = apiResponse.datos["clave"].ToString();
                            Session["nombreCompleto"] = apiResponse.datos["nombre_completo"].ToString();
                            Session["tipoUsuario"] = "docente";

                            Response.Redirect("wpMenuDocente.aspx");
                        }
                        else
                        {
                            MostrarAlerta("Credenciales incorrectas");
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtContrasena.Text = "";
        }

        private void MostrarAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                $"alert('{mensaje}');", true);
        }
    }

    public class clsApiStatus
    {
        public bool statusExec { get; set; }
        public int ban { get; set; }
        public string msg { get; set; }
        public JObject datos { get; set; }
    }
}