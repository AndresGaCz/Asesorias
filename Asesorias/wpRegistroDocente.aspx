<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="wpRegistroDocente.aspx.cs" Inherits="Asesorias.wpRegistroDocente" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Docente</title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .register-container {
            margin-top: 50px;
            max-width: 600px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-8 register-container">
                    <div class="card">
                        <div class="card-header bg-primary text-white text-center">
                            <h3>Registro de Docente</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtClave">Clave de Docente:</label>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" placeholder="Ej: DOC-001"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtNombre">Nombre:</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtApellidoPaterno">Apellido Paterno:</label>
                                <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtApellidoMaterno">Apellido Materno:</label>
                                <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtUsuario">Usuario:</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtContrasena">Contraseña:</label>
                                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtConfirmarContrasena">Confirmar Contraseña:</label>
                                <asp:TextBox ID="txtConfirmarContrasena" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group text-center">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary ml-2" OnClick="btnCancelar_Click" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="bootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="bootstrap/js/bootstrap.bundle.min.js"></script>
</body>
</html>