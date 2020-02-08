Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI

Partial Public Class Layout
    Inherits System.Web.UI.MasterPage
    Private m_showSearch As Boolean = True
    Public Property ShowSearch() As Boolean
        Get
            Return m_showSearch
        End Get
        Set
            m_showSearch = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs)




        If IsPostBack Then


        End If



    End Sub

    Protected Sub HeadLoginStatus_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut()
    End Sub
End Class