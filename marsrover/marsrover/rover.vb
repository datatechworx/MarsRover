<Serializable>
Public Class rover
    '#ROVER PROPERTIES
    Public Property direction() As Char
        Get
            Return m_direction
        End Get
        Set
            m_direction = Value
        End Set
    End Property
    Private m_direction As Char

    Public Property positionX() As Integer
        Get
            Return m_positionX
        End Get
        Set
            m_positionX = Value
        End Set
    End Property
    Private m_positionX As Integer

    Public Property positionY() As Integer
        Get
            Return m_positionY
        End Get
        Set
            m_positionY = Value
        End Set
    End Property
    Private m_positionY As Integer



End Class
