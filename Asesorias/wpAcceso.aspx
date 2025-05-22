<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="wpAcceso.aspx.cs" Inherits="Asesorias.wpAcceso" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Acceso al Sistema de Asesorías</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .login-container {
            margin-top: 100px;
            max-width: 500px;
        }
        .login-header {
            background-color: #2c3e50;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6 login-container">
                    <div class="card">
                        <div class="card-header login-header text-center">
                            <h3>Sistema de Asesorías ITP</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtUsuario">Usuario:</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtContrasena">Contraseña:</label>
                                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña"></asp:TextBox>
                            </div>
                            <div class="form-group text-center">
                                <asp:RadioButtonList ID="rblTipoUsuario" runat="server" RepeatDirection="Horizontal" CssClass="form-check-inline">
                                    <asp:ListItem Value="estudiante" Selected="True">Estudiante</asp:ListItem>
                                    <asp:ListItem Value="docente">Docente</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="form-group text-center">
                                <asp:Button ID="btnAcceder" runat="server" Text="Acceder" CssClass="btn btn-primary" OnClick="btnAcceder_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary ml-2" OnClick="btnCancelar_Click" />
                            </div>
      

                            <div class="form-group text-center">
    <asp:HyperLink ID="hlRegistroEstudiante" runat="server" NavigateUrl="~/wpRegistroEstudiante.aspx" CssClass="btn btn-link">Registrarse como estudiante</asp:HyperLink>
    <asp:HyperLink ID="hlRegistroDocente" runat="server" NavigateUrl="~/wpRegistroDocente.aspx" CssClass="btn btn-link">Registrarse como docente</asp:HyperLink>
</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="bootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="bootstrap/js/popper.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
</body>
</html>