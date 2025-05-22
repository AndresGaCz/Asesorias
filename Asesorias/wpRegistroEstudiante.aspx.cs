using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json;

namespace Asesorias
{
    public partial class wpRegistroEstudiante : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                await RegistrarEstudiante();
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtNumControl.Text))
            {
                lblMensaje.Text = "Ingrese el número de control";
                return false;
            }

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellidoPaterno.Text))
            {
                lblMensaje.Text = "Ingrese nombre completo";
                return false;
            }

            if (string.IsNullOrEmpty(txtCarrera.Text))
            {
                lblMensaje.Text = "Ingrese la carrera";
                return false;
            }

            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text))
            {
                lblMensaje.Text = "Ingrese usuario y contraseña";
                return false;
            }

            return true;
        }

        private async Task RegistrarEstudiante()
        {
            try
            {
                var estudiante = new
                {
                    numControl = txtNumControl.Text,
                    nombre = txtNombre.Text,
                    apellidoPaterno = txtApellidoPaterno.Text,
                    apellidoMaterno = txtApellidoMaterno.Text,
                    carrera = txtCarrera.Text,
                    semestre = int.Parse(ddlSemestre.SelectedValue),
                    usuario = txtUsuario.Text,
                    contrasena = txtContrasena.Text
                };

                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(estudiante);
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:44331/api/estudiante/registrar", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban == 0)
                        {
                            lblMensaje.Text = "Estudiante registrado correctamente";
                            LimpiarFormulario();
                        }
                        else
                        {
                            lblMensaje.Text = $"Error al registrar: {apiResponse.msg}";
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
            txtNumControl.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtCarrera.Text = "";
            txtUsuario.Text = "";
            txtContrasena.Text = "";
            ddlSemestre.SelectedIndex = 0;
        }
    }
}