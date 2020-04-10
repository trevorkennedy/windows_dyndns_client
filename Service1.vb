Imports System.ServiceProcess
Imports System.Net
Imports System.IO
Imports System.Net.Sockets
Imports System.Xml
Imports System.Reflection.Assembly
Imports System.Text, System.Security.Cryptography

Public Class Service1
    Inherits System.ServiceProcess.ServiceBase

#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()

        ' This call is required by the Component Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call

    End Sub

    'UserService overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    Friend WithEvents Timer1 As System.Timers.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Timer1 = New System.Timers.Timer
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1800000
        '
        'Service1
        '
        Me.ServiceName = "DotNet DynDNS Service"
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

#End Region

    Dim Username As String
    Dim Password As String
    Dim Hostname As String
    Dim Wildcard As String
    Dim MXRecord As String
    Dim BackupMX As String
    'Dim manualIP As String
    Dim manualIP, PostURL, beginIP As String

    Protected Overrides Sub OnStart(ByVal args() As String)
        Dim strFile As String = System.IO.Path.GetDirectoryName(GetExecutingAssembly.Location) & "\Config.xml"
        Try
            Timer1.Enabled = True
            Dim XmlDoc As New XmlDocument
            XmlDoc.Load(strFile)
            Dim Node As XmlNode
            For Each Node In XmlDoc.Item("configuration").Item("appSettings")
                If Node.Name = "add" Then
                    Select Case Node.Attributes.GetNamedItem("key").Value
                        Case "username"
                            Username = Node.Attributes.GetNamedItem("value").Value
                        Case "password"
                            'Password = Node.Attributes.GetNamedItem("value").Value
                            Password = DeCifra(Node.Attributes.GetNamedItem("value").Value)
                        Case "hostname"
                            Hostname = Node.Attributes.GetNamedItem("value").Value
                        Case "wildcard"
                            Wildcard = Node.Attributes.GetNamedItem("value").Value
                        Case "mxrecord"
                            MXRecord = Node.Attributes.GetNamedItem("value").Value
                        Case "backupmx"
                            BackupMX = Node.Attributes.GetNamedItem("value").Value
                        Case "manualip"
                            manualIP = Node.Attributes.GetNamedItem("value").Value
                        Case "posturl"
                            PostURL = Node.Attributes.GetNamedItem("value").Value
                        Case "beginip"
                            beginIP = Node.Attributes.GetNamedItem("value").Value
                    End Select
                End If
            Next Node
            'UpdateLog(UpdateDNS())
            Timer1_Elapsed(Me, Nothing)
        Catch err As Exception
            Dim MyLog As New EventLog
            MyLog.Source = "Application"
            MyLog.WriteEntry("DotNet DynDNS Service", "Configuration file could not be loaded" & vbNewLine & _
            strFile, EventLogEntryType.Error)
            'MsgBox(err.Message.ToString)
            'Me.CanStop = True
        End Try
    End Sub

    Protected Overrides Sub OnStop()
        Timer1.Enabled = False
    End Sub

    Private Sub Timer1_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer1.Elapsed
        'Dim MyLog As New EventLog
        'MyLog.Source = "Application"
        'MyLog.WriteEntry("DotNet DynDNS Service", "NSLookup: " & NSLookup() & vbNewLine & "PublicIP: " & GetPublicIP(), EventLogEntryType.Information)
        If NSLookup() <> GetPublicIP() Then
            UpdateLog(UpdateDNS())
        End If
    End Sub
    Private Function GetPublicIP() As String
        If manualIP = "" Then
            Dim publicip As String
            'Dim PostURL As String = "http://checkip.dyndns.org/"
            Dim HttpWReq As HttpWebRequest = CType(WebRequest.Create(PostURL), HttpWebRequest)
            HttpWReq.Method = "GET"
            Dim HttpWResp As HttpWebResponse = CType(HttpWReq.GetResponse(), HttpWebResponse)
            Dim receiveStream As Stream = HttpWResp.GetResponseStream()
            Dim readStream As New StreamReader(receiveStream)
            Dim strIP As String = readStream.ReadToEnd
            readStream.Close()
            HttpWResp.Close()
            Dim intStartPos As Integer = Strings.InStr(strIP, beginIP) + Len(beginIP)
            'Dim intEndPos As Integer = Strings.InStr(strIP, "</body>")
            Dim i As Integer
            'publicIP = Strings.Mid(strIP, intStartPos, intEndPos - intStartPos)
            publicip = Strings.Mid(strIP, intStartPos, 8)
            For i = 8 To 17
                Dim s As String = Strings.Mid(strIP, intStartPos + i, 1)
                If IsNumeric(s) = True Or s = "." Then
                    publicip = publicip & s
                End If
            Next
            Return publicip
        Else
            Return manualIP
        End If
    End Function
    Private Function NSLookup() As String
        Try
            Dim DNSLookup As Dns
            Dim IPResult As New IPHostEntry
            IPResult = DNSLookup.GetHostByName(Hostname)
            Return IPResult.AddressList(0).ToString
        Catch err As Exception
            Dim MyLog As New EventLog
            MyLog.Source = "Application"
            MyLog.WriteEntry("DotNet DynDNS Service", "Could not query " & Hostname, EventLogEntryType.Error)
            'Me.CanStop = True
        End Try
    End Function
    Private Function UpdateDNS() As String
        Dim PostURL As String = "https://members.dyndns.org/nic/update?system=dyndns&hostname=" & Hostname & "&myip="
        If manualIP <> "" Then
            PostURL = PostURL & manualIP & "&wildcard=" & Wildcard
        Else
            PostURL = PostURL & GetPublicIP() & "&wildcard=" & Wildcard
        End If
        If MXRecord <> "" Then
            PostURL = PostURL & "&mx=" & MXRecord & "&backmx=" & BackupMX
        End If
        Try
            Dim HttpWReq As HttpWebRequest = CType(WebRequest.Create(PostURL), HttpWebRequest)
            HttpWReq.Method = "GET"
            Dim strAuth() As Byte = System.Text.Encoding.UTF8.GetBytes(Username & ":" & Password)
            HttpWReq.Headers.Set("Authorization", "Basic " & System.Convert.ToBase64String(strAuth))
            'HttpWReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.5; Windows NT 5.0)"
            HttpWReq.UserAgent = "DotNet DynDNS Service v1.0"
            Dim HttpWResp As HttpWebResponse = CType(HttpWReq.GetResponse(), HttpWebResponse)
            Dim receiveStream As Stream = HttpWResp.GetResponseStream()
            Dim readStream As New StreamReader(receiveStream)
            Dim strStatus As String = readStream.ReadToEnd
            readStream.Close()
            HttpWResp.Close()
            Return UCase(strStatus)
        Catch err As Exception
            Dim MyLog As New EventLog
            MyLog.Source = "Application"
            MyLog.WriteEntry("DotNet DynDNS Service", "Could not access server. Invalid username or password" & vbNewLine & _
            "Run the configuration client" & vbNewLine & PostURL, EventLogEntryType.Error)
            'Me.CanStop = True
        End Try
    End Function
    Private Sub UpdateLog(ByVal strStatus As String)
        Dim MyLog As New EventLog
        MyLog.Source = "Application"
        If InStr(strStatus, "GOOD") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "IP Address successfully changed." & vbNewLine & _
            strStatus, EventLogEntryType.Information)
        ElseIf InStr(strStatus, "NOCHG") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "No changes, update considered abusive." & vbNewLine & strStatus, EventLogEntryType.Warning)
        ElseIf InStr(strStatus, "BADAUTH") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "Bad username or password." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "BADSYS") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "The system parameter given was not valid." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "BADAGENT") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "DotNet DynDNS Service has been banned by DynDNS.org" & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "!YOURS") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "The hostname specified exists, but not under the username currently being used." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "ABUSE") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "The hostname specified is blocked for abuse; contact support to be unblocked." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "NOTFQDN") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "A Fully-Qualified hostname was not provided." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "NOHOST") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "The hostname specified does not exist." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "!DONATOR") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "The offline setting was set, when the user is not a donator." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "ACTIVE") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "The hostname specified is in a Custom DNS domain which has not yet been activated." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "NUMHOST") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "Too many or too few hosts found." & vbNewLine & strStatus, EventLogEntryType.Error)
        ElseIf InStr(strStatus, "DNSERR") Then
            MyLog.WriteEntry("DotNet DynDNS Service", "DNS error encountered." & vbNewLine & strStatus, EventLogEntryType.Error)
        End If
    End Sub
    Dim myKey As String = "GareWEBNetPlatformbyDedalusSPA"
    Dim des As New TripleDESCryptoServiceProvider
    Dim hashmd5 As New MD5CryptoServiceProvider
    Private Function DeCifra(ByVal pass As String) As String
        des.Key = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        des.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = des.CreateDecryptor()
        Dim buff() As Byte = Convert.FromBase64String(pass)
        Return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    End Function

    Private Function Cifra(ByVal pass As String) As String
        des.Key = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        des.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = des.CreateEncryptor()
        Dim MyASCIIEncoding = New ASCIIEncoding
        Dim buff() As Byte = ASCIIEncoding.ASCII.GetBytes(pass)
        Return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    End Function
End Class
