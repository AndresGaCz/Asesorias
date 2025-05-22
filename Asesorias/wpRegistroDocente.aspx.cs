using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json;

namespace Asesorias
{
    public partial class wpRegistroDocente : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                await RegistrarDocente();
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                lblMensaje.Text = "Ingrese la clave del docente";
                return false;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblMensaje.Text = "Ingrese el nombre";
                return false;
            }

            if (string.IsNullOrEmpty(txtApellidoPaterno.Text))
            {
                lblMensaje.Text = "Ingrese el apellido paterno";
                return false;
            }

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                lblMensaje.Text = "Ingrese un nombre de usuario";
                return false;
            }

            if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                lblMensaje.Text = "Ingrese una contraseña";
                return false;
            }

            if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                lblMensaje.Text = "Las contraseñas no coinciden";
                return false;
            }

            return true;
        }

        private async Task RegistrarDocente()
        {
            try
            {
                var docente = new
                {
                    clave = txtClave.Text,
                    nombre = txtNombre.Text,
                    apellidoPaterno = txtApellidoPaterno.Text,
                    apellidoMaterno = txtApellidoMaterno.Text,
                    usuario = txtUsuario.Text,
                    contrasena = txtContrasena.Text
                };

                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(docente);
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:44331/api/docente/registrar", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<clsApiStatus>(result);

                        if (apiResponse.ban == 0)
                        {
                            lblMensaje.Text = "Docente registrado correctamente";
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
            txtClave.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtUsuario.Text = "";
            txtContrasena.Text = "";
            txtConfirmarContrasena.Text = "";
        }
    }
}