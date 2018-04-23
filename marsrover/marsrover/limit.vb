<Serializable>
Public Class limit

    Public Property limitX() As String
        Get
            Return m_limitX
        End Get
        Set
            m_limitX = Value
        End Set
    End Property
    Private m_limitX As String

    Public Property limitY() As String
        Get
            Return m_limitY
        End Get
        Set
            m_limitY = Value
        End Set
    End Property
    Private m_limitY As String


End Class
