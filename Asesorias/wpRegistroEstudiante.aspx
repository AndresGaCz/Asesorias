<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="wpRegistroEstudiante.aspx.cs" Inherits="Asesorias.wpRegistroEstudiante" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Estudiante</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .register-container {
            margin-top: 50px;
            max-width: 600px;
        }
        .register-header {
            background-color: #2c3e50;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-8 register-container">
                    <div class="card">
                        <div class="card-header register-header text-center">
                            <h3>Registro de Nuevo Estudiante</h3>
                        </div>
                        <div class="card-body">
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="txtNumControl">Número de Control:</label>
                                    <asp:TextBox ID="txtNumControl" runat="server" CssClass="form-control" placeholder="Ej. 12345678"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="txtCarrera">Carrera:</label>
                                    <asp:TextBox ID="txtCarrera" runat="server" CssClass="form-control" placeholder="Ingrese su carrera"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                <div class="form-group col-md-4">
                                    <label for="txtNombre">Nombre(s):</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre(s)"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="txtApellidoPaterno">Apellido Paterno:</label>
                                    <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control" placeholder="Apellido paterno"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="txtApellidoMaterno">Apellido Materno:</label>
                                    <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control" placeholder="Apellido materno"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="ddlSemestre">Semestre:</label>
                                    <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">1° Semestre</asp:ListItem>
                                        <asp:ListItem Value="2">2° Semestre</asp:ListItem>
                                        <asp:ListItem Value="3">3° Semestre</asp:ListItem>
                                        <asp:ListItem Value="4">4° Semestre</asp:ListItem>
                                        <asp:ListItem Value="5">5° Semestre</asp:ListItem>
                                        <asp:ListItem Value="6">6° Semestre</asp:ListItem>
                                        <asp:ListItem Value="7">7° Semestre</asp:ListItem>
                                        <asp:ListItem Value="8">8° Semestre</asp:ListItem>
                                        <asp:ListItem Value="9">9° Semestre</asp:ListItem>
                                        <asp:ListItem Value="10">10° Semestre</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="txtUsuario">Usuario:</label>
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Nombre de usuario"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="txtContrasena">Contraseña:</label>
                                    <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group text-center">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary ml-2" OnClick="btnCancelar_Click" />
                            </div>
                            
                            <div class="form-group text-center">
                                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
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