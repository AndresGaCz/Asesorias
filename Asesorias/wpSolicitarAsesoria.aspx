<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="wpSolicitarAsesoria.aspx.cs" Inherits="Asesorias.wpSolicitarAsesoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f8f9fa;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2 class="text-center mb-4">Solicitar Asesoría</h2>
        
        <div class="form-group">
            <label for="ddlDocente">Docente:</label>
            <asp:DropDownList ID="ddlDocente" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDocente_SelectedIndexChanged"></asp:DropDownList>
        </div>
        
        <div class="form-group">
            <label for="ddlMateria">Materia:</label>
            <asp:DropDownList ID="ddlMateria" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        
        <div class="form-group">
            <label for="txtFecha">Fecha:</label>
            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtHoraInicio">Hora Inicio:</label>
                    <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtHoraFin">Hora Fin:</label>
                    <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                </div>
            </div>
        </div>
        
        
        <div class="text-center">
            <asp:Button ID="btnSolicitar" runat="server" Text="Solicitar Asesoría" CssClass="btn btn-primary" OnClick="btnSolicitar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary ml-2" OnClick="btnCancelar_Click" />
        </div>
        
        <div class="mt-3">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </div>
</asp:Content>