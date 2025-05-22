<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="wpMenuEstudiante.aspx.cs" Inherits="Asesorias.wpMenuEstudiante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="server">
    <li class="nav-item">
        <asp:HyperLink ID="hlSolicitarAsesoria" runat="server" CssClass="nav-link" NavigateUrl="~/wpSolicitarAsesoria.aspx">Solicitar Asesoría</asp:HyperLink>
    </li>
    <li class="nav-item">
        <asp:HyperLink ID="hlMisAsesorias" runat="server" CssClass="nav-link" NavigateUrl="~/wpMisAsesorias.aspx">Tablero de asesorias</asp:HyperLink>
    </li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h1 class="display-4">Bienvenido Estudiante</h1>
        <p class="lead">Sistema de gestión de asesorías académicas</p>
        <hr class="my-4">
        <p>Desde aquí puedes solicitar asesorías con tus docentes y ver el estado de tus solicitudes.</p>
    </div>
</asp:Content>