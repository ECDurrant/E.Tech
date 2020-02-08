<%@ Page Title="" Language="VB" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="BlogPost.aspx.vb" Inherits="EricTech.BlogPost" %>

<%@ Register Src="~/UserControls/BlogNavigation.ascx" TagPrefix="dx" TagName="BlogNavigation" %>
<%@ Register Src="~/UserControls/AddCommentForm.ascx" TagPrefix="dx" TagName="AddCommentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="container">
        <div class="row">
            <article class="col-sm-9">
                <div class="page-header">
                    <h1 id="Subject" runat="server"></h1>
          
                </div>
           
                <p id="Body" runat="server" class="marginTop20"></p>
                <hr />
            </article>
            <nav class="col-sm-3">
                <dx:BlogNavigation runat="server" />
            </nav>
        </div>
        <div class="row">
            <div class="col-md-9">
                <h4>Leave a Comment:</h4>
                <dx:AddCommentForm runat="server" />
                <hr />

                <div class="col-md-3"></div>
            </div>
        </div>
    </div>
</asp:Content>
