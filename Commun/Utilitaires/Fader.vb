Imports System.Windows.Forms

Public Class Fader
    Public Event atEnd As EventHandler
    Private Shared _intInterval As Int32 = 120
    Private _dblAddValue As Double
    Private _tmrLoopTimer As Timer
    Private _tmrEndTimer As Timer

#Region " Property "
    Private _frm As Form
    Public Property frm() As Form
        Get
            Return _frm
        End Get
        Set(ByVal value As Form)
            _frm = value
        End Set
    End Property

    Private _intDuree As Int32 = 2000
    Public Property Duree() As Int32
        Get
            Return _intDuree
        End Get
        Set(ByVal value As Int32)
            _intDuree = value
        End Set
    End Property

    Private _dblStartingOpacity As Double = 0.2
    Public Property StartingOpacity() As Double
        Get
            Return _dblStartingOpacity
        End Get
        Set(ByVal value As Double)
            _dblStartingOpacity = value
        End Set
    End Property

    Private _dblEndingOpacity As Double = 1
    Public Property EndingOpacity() As Double
        Get
            Return _dblEndingOpacity
        End Get
        Set(ByVal value As Double)
            _dblEndingOpacity = value
        End Set
    End Property

    Private _intTimeToWait As Int32 = 0
    Public Property TimeToWait() As Int32
        Get
            Return _intTimeToWait
        End Get
        Set(ByVal value As Int32)
            _intTimeToWait = value
        End Set
    End Property
#End Region

    Public Sub New(ByVal p_frm As Form)
        _frm = p_frm
    End Sub

    Public Sub New(ByVal p_frm As Form, ByVal p_intDuree As Int32)
        _frm = p_frm
        _intDuree = p_intDuree
    End Sub

    Public Sub start()
        _frm.Opacity = startingOpacity
        _frm.Show()
        _frm.Activate()
        _tmrLoopTimer = New Timer()
        _tmrLoopTimer.Interval = _intInterval

        AddHandler _tmrLoopTimer.Tick, AddressOf Boucle

        _tmrLoopTimer.Enabled = True
        _dblAddValue = (EndingOpacity - StartingOpacity) / _intDuree * _intInterval
    End Sub

    Private Sub Boucle(ByVal sender As Object, ByVal e As System.EventArgs)
        _frm.Opacity += _dblAddValue
        If (_frm.Opacity >= endingOpacity) Then
            _tmrLoopTimer.Dispose()

            _tmrEndTimer = New Timer()
            _tmrEndTimer.Interval = TimeToWait

            AddHandler _tmrEndTimer.Tick, AddressOf stopEndTimer

            _tmrEndTimer.Start()
        End If
    End Sub

    Private Sub stopEndTimer(ByVal sender As Object, ByVal e As System.EventArgs)
        _tmrEndTimer.Dispose()

        RaiseEvent atEnd(sender, e)
    End Sub


End Class
