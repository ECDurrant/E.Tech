Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports EricTech.Code

Partial Public Class BlogPost
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)






        Dim id? As Integer = GetBlogPostId()
        Dim blogPost = BlogPostsProvider.GetBlogPost(id)
        If blogPost Is Nothing Then
            Response.Redirect("BlogTimeline.aspx")
        End If
        Subject.InnerText = blogPost.Subject
        Page.Title = Subject.InnerText
        '     [Date].Text = blogPost.Date.ToString("ddd, MMM dd yyyy")
        Body.InnerText = blogPost.Body
        '      BlogImage.Src = blogPost.ImageUrl
    End Sub
    Private Function GetBlogPostId() As Integer?
        Dim id As Integer = Nothing
        Dim strId = Request.Params("id")
        Return If(Integer.TryParse(strId, id), id, DirectCast(Nothing, Integer?))
    End Function
End Class