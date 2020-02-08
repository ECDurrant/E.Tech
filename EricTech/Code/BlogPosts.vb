Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Xml.Serialization

Namespace Code
<XmlRoot("BlogPosts")> _
Public Class BlogPostsProvider
    Private blogPostsValue As List(Of BlogPost)
    <XmlElement("BlogPost")> _
    Public Property BlogPosts() As List(Of BlogPost)
        Get
            Return blogPostsValue
        End Get
        Set(ByVal value As List(Of BlogPost))
            blogPostsValue = value
        End Set
    End Property

    Private Shared currentValue As BlogPostsProvider
    Shared ReadOnly Property Current() As BlogPostsProvider
        Get
            If currentValue Is Nothing Then
                Dim filePath As String = HttpContext.Current.Server.MapPath("~/App_Data/BlogPosts.xml")
                Using reader As New StreamReader(filePath)
                    Dim serializer As New XmlSerializer(GetType(BlogPostsProvider))
                    currentValue = CType(serializer.Deserialize(reader), BlogPostsProvider)
                End Using
            End If
            Return currentValue
        End Get
    End Property

    Public Shared Function GetBlogPost(ByVal id? As Integer) As BlogPost
        If (Not id.HasValue) Then
            Return Nothing
        End If
        Return Current.BlogPosts.FirstOrDefault(Function(p) p.Id.Equals(id))
    End Function

    Public Shared Function GetBlogPosts() As List(Of BlogPost)
        Return GetBlogPosts(Nothing, Nothing)
    End Function
    Public Shared Function GetBlogPosts(ByVal year? As Integer, ByVal month? As Integer) As List(Of BlogPost)
        Dim blogPosts As List(Of BlogPost) = Current.BlogPosts
        If blogPosts Is Nothing Then
            Return Nothing
        End If

        If year.HasValue Then
            blogPosts = blogPosts.FindAll(Function(p) p.Date.Year.Equals(year))
        End If
        If month.HasValue Then
            blogPosts = blogPosts.FindAll(Function(p) p.Date.Month.Equals(month))
        End If
        Return blogPosts
    End Function

    Public Shared Function GetBlogsByCategories() As Dictionary(Of Integer, Dictionary(Of Integer, IEnumerable(Of BlogPost)))
        Dim result = New Dictionary(Of Integer, Dictionary(Of Integer, IEnumerable(Of BlogPost)))()
        Dim yearsGroup = Current.BlogPosts.GroupBy(Function(p) p.Date.Year).OrderByDescending(Function(g) g.Key)
        For Each yearGroup In yearsGroup
            result(yearGroup.Key) = New Dictionary(Of Integer, IEnumerable(Of BlogPost))()
            Dim monthGroups = yearGroup.GroupBy(Function(p) p.Date.Month).OrderByDescending(Function(g) g.Key)
            For Each monthGroup In monthGroups
                result(yearGroup.Key)(monthGroup.Key) = monthGroup.ToList()
            Next monthGroup
        Next yearGroup
        Return result
    End Function
End Class

Public Class BlogPost
    Private idValue As Integer
    Private categoryValue As String
    Private subjectValue As String
    Private bodyValue As String
    Private imageUrlValue As String
    Private dateValue As DateTime

    Public Property Id() As Integer
        Get
            Return idValue
        End Get
        Set(ByVal value As Integer)
            idValue = value
        End Set
    End Property

    Public Property Category() As String
        Get
            Return categoryValue
        End Get
        Set(ByVal value As String)
            categoryValue = value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return subjectValue
        End Get
        Set(ByVal value As String)
            subjectValue = value
        End Set
    End Property

    Public Property Body() As String
        Get
            Return bodyValue
        End Get
        Set(ByVal value As String)
            bodyValue = value
        End Set
    End Property

    Public Property ImageUrl() As String
        Get
            Return imageUrlValue
        End Get
        Set(ByVal value As String)
            imageUrlValue = value
        End Set
    End Property

    <XmlIgnore> _
    Public Property [Date]() As DateTime
        Get
            Return dateValue
        End Get
        Set(ByVal value As DateTime)
            dateValue = value
        End Set
    End Property

    Public Property DateString() As String
        Get
            Return Me.Date.ToString("yyyy-MM-dd HH:mm:ss")
        End Get
        Set(ByVal value As String)
            Me.Date = DateTime.Parse(value)
        End Set
    End Property
End Class
End Namespace