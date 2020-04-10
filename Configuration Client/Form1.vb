Imports System.Net
Imports System.IO
Imports System.Reflection.Assembly
Imports System.Xml
Imports System.Threading
Imports System.Text, System.Security.Cryptography
Public Class Form1
    Inherits System.Windows.Forms.Form
    Dim manualIP, publicIP, PostURL, beginIP, endIP As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ServiceController1 As System.ServiceProcess.ServiceController
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkWildcard As System.Windows.Forms.CheckBox
    Friend WithEvents txtHostname As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkBackupMX As System.Windows.Forms.CheckBox
    Friend WithEvents txtMXRecord As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkManual As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.ServiceController1 = New System.ServiceProcess.ServiceController
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkManual = New System.Windows.Forms.CheckBox
        Me.txtIP = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkWildcard = New System.Windows.Forms.CheckBox
        Me.txtHostname = New System.Windows.Forms.TextBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkBackupMX = New System.Windows.Forms.CheckBox
        Me.txtMXRecord = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ServiceController1
        '
        Me.ServiceController1.ServiceName = "DotNet DynDNS Service"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(88, 336)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Save"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(9, 312)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(232, 16)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "IP Address will be checked every 30 minutes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkManual)
        Me.GroupBox1.Controls.Add(Me.txtIP)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 80)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Network Information"
        '
        'chkManual
        '
        Me.chkManual.Location = New System.Drawing.Point(72, 56)
        Me.chkManual.Name = "chkManual"
        Me.chkManual.Size = New System.Drawing.Size(144, 16)
        Me.chkManual.TabIndex = 17
        Me.chkManual.Text = "Manually Override"
        '
        'txtIP
        '
        Me.txtIP.Enabled = False
        Me.txtIP.Location = New System.Drawing.Point(72, 24)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(144, 20)
        Me.txtIP.TabIndex = 15
        Me.txtIP.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "IP Address:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkWildcard)
        Me.GroupBox2.Controls.Add(Me.txtHostname)
        Me.GroupBox2.Controls.Add(Me.txtPassword)
        Me.GroupBox2.Controls.Add(Me.txtUsername)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 96)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(232, 128)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "DynDNS Information"
        '
        'chkWildcard
        '
        Me.chkWildcard.Location = New System.Drawing.Point(80, 104)
        Me.chkWildcard.Name = "chkWildcard"
        Me.chkWildcard.Size = New System.Drawing.Size(112, 16)
        Me.chkWildcard.TabIndex = 5
        Me.chkWildcard.Text = "Enable Wildcards"
        '
        'txtHostname
        '
        Me.txtHostname.Location = New System.Drawing.Point(80, 80)
        Me.txtHostname.Name = "txtHostname"
        Me.txtHostname.Size = New System.Drawing.Size(136, 20)
        Me.txtHostname.TabIndex = 10
        Me.txtHostname.Text = ""
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(80, 48)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtPassword.TabIndex = 8
        Me.txtPassword.Text = ""
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(80, 24)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(136, 20)
        Me.txtUsername.TabIndex = 6
        Me.txtUsername.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Hostname:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Password:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Username:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkBackupMX)
        Me.GroupBox3.Controls.Add(Me.txtMXRecord)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 232)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(232, 72)
        Me.GroupBox3.TabIndex = 18
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Optional Information"
        '
        'chkBackupMX
        '
        Me.chkBackupMX.Location = New System.Drawing.Point(88, 48)
        Me.chkBackupMX.Name = "chkBackupMX"
        Me.chkBackupMX.Size = New System.Drawing.Size(128, 16)
        Me.chkBackupMX.TabIndex = 6
        Me.chkBackupMX.Text = "Backup MX record"
        '
        'txtMXRecord
        '
        Me.txtMXRecord.Location = New System.Drawing.Point(88, 24)
        Me.txtMXRecord.Name = "txtMXRecord"
        Me.txtMXRecord.Size = New System.Drawing.Size(128, 20)
        Me.txtMXRecord.TabIndex = 7
        Me.txtMXRecord.Text = ""
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(16, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "MX Record:"
        '
        'Form1
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(250, 368)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DotNet DynDNS Service"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ErrorFree() = True Then
            Try
                SaveSettings()
                If ServiceController1.Status <> 1 Then
                    ServiceController1.Stop()
                    ServiceController1.WaitForStatus(ServiceProcess.ServiceControllerStatus.Stopped)
                End If
                ServiceController1.Start()
            Catch err As Exception
                MsgBox("Could not restart the DotNet DynDNS Windows Service", MsgBoxStyle.Critical, "DotNet DynDNS Service")
            Finally
                Application.Exit()
            End Try
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strFile As String = System.IO.Path.GetDirectoryName(GetExecutingAssembly.Location) & "\Config.xml"
        'Dim t As Thread = New Thread(New ThreadStart(AddressOf GetPublicIP))
        't.Start()
        'Dim DNSLookup As Dns
        'Dim IPResult As New IPHostEntry
        'IPResult = DNSLookup.GetHostByName("www.yahoo.com")
        'MsgBox(IPResult.AddressList(0).ToString)
        Try
    Dim XmlDoc As New XmlDocument
            XmlDoc.Load(strFile)
    Dim Node As XmlNode
            For Each Node In XmlDoc.Item("configuration").Item("appSettings")
                If Node.Name = "add" Then
                    Select Case Node.Attributes.GetNamedItem("key").Value
                        Case "username"
                            txtUsername.Text = Node.Attributes.GetNamedItem("value").Value
                        Case "password"
                            txtPassword.Text = DeCifra(Node.Attributes.GetNamedItem("value").Value)
                        Case "hostname"
                            txtHostname.Text = Node.Attributes.GetNamedItem("value").Value
                        Case "wildcard"
                            If Node.Attributes.GetNamedItem("value").Value = "ON" Then chkWildcard.Checked = True
                        Case "mxrecord"
                            txtMXRecord.Text = Node.Attributes.GetNamedItem("value").Value
                        Case "backupmx"
                            If Node.Attributes.GetNamedItem("value").Value = "YES" Then chkBackupMX.Checked = True
                        Case "manualip"
                            manualIP = Node.Attributes.GetNamedItem("value").Value
                            txtIP.Text = manualIP
                        Case "posturl"
                            PostURL = Node.Attributes.GetNamedItem("value").Value
                        Case "beginip"
                            beginIP = Node.Attributes.GetNamedItem("value").Value
                    End Select
                End If
            Next Node
            GetPublicIP()
        Catch err As Exception
            MsgBox("Could not load the configuration file:" & vbNewLine & strFile, MsgBoxStyle.Critical, "DotNet DynDNS Service")
            Application.Exit()
        End Try
    End Sub
    Private Sub GetPublicIP()
        Try
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
            publicIP = Strings.Mid(strIP, intStartPos, 8)
            For i = 8 To 17
                Dim s As String = Strings.Mid(strIP, intStartPos + i, 1)
                If IsNumeric(s) = True Or s = "." Then
                    publicIP = publicIP & s
                End If
            Next
            If manualIP = "" Then
                txtIP.Text = publicIP
            Else
                txtIP.Text = manualIP
                chkManual.Checked = True
            End If
        Catch err As Exception
            MsgBox("Could not obtain your public IP Address", MsgBoxStyle.Critical, "DotNet DynDNS Service")
            'MsgBox(err.Message.ToString)
            'Application.Exit()
        End Try
    End Sub
    Private Function ErrorFree() As Boolean
        Dim blnErrors As Boolean = False
        If Trim(txtUsername.Text) = "" Then
            blnErrors = True
            MsgBox("Username is required", MsgBoxStyle.Exclamation, "DotNet DynDNS Service")
        ElseIf Trim(txtPassword.Text) = "" Then
            blnErrors = True
            MsgBox("Password is required", MsgBoxStyle.Exclamation, "DotNet DynDNS Service")
        ElseIf Trim(txtHostname.Text) = "" Then
            blnErrors = True
            MsgBox("Hostname is required")
        ElseIf Trim(txtMXRecord.Text) = "" And chkBackupMX.Checked = True Then
            blnErrors = True
            MsgBox("If Backup Record is checked, MX record is required", MsgBoxStyle.Exclamation, "DotNet DynDNS Service")
        End If
        Return Not blnErrors
    End Function
    Private Sub SaveSettings()
        Dim strFile As String = System.IO.Path.GetDirectoryName(GetExecutingAssembly.Location) & "\Config.xml"
        Try
            Dim XmlDoc As New XmlDocument
            XmlDoc.Load(strFile)
            Dim Node As XmlNode
            For Each Node In XmlDoc.Item("configuration").Item("appSettings")
                If Node.Name = "add" Then
                    Select Case Node.Attributes.GetNamedItem("key").Value
                        Case "username"
                            Node.Attributes.GetNamedItem("value").Value = txtUsername.Text
                        Case "password"
                            'Node.Attributes.GetNamedItem("value").Value = Encrypt(txtPassword.Text, False)
                            Node.Attributes.GetNamedItem("value").Value = Cifra(txtPassword.Text)
                        Case "hostname"
                            Node.Attributes.GetNamedItem("value").Value = txtHostname.Text
                        Case "wildcard"
                            If chkWildcard.Checked = True Then
                                Node.Attributes.GetNamedItem("value").Value = "ON"
                            Else
                                Node.Attributes.GetNamedItem("value").Value = "OFF"
                            End If
                        Case "mxrecord"
                            Node.Attributes.GetNamedItem("value").Value = txtMXRecord.Text
                        Case "backupmx"
                            If chkBackupMX.Checked = True Then
                                Node.Attributes.GetNamedItem("value").Value = "YES"
                            Else
                                Node.Attributes.GetNamedItem("value").Value = "NO"
                            End If
                        Case "manualip"
                            If chkManual.Checked = True Then
                                Node.Attributes.GetNamedItem("value").Value = txtIP.Text
                            Else
                                Node.Attributes.GetNamedItem("value").Value = ""
                            End If
                    End Select
                End If
            Next Node
            XmlDoc.Save(strFile)
        Catch err As Exception
            MsgBox("Could not save the configuration file:" & vbNewLine & strFile, MsgBoxStyle.Critical, "DotNet DynDNS Service")
            Application.Exit()
        End Try
    End Sub

    Private Sub chkManual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkManual.CheckedChanged
        If chkManual.Checked = True Then
            If manualIP <> "" Then
                txtIP.Text = manualIP
            End If
            txtIP.Enabled = True
        Else
            txtIP.Text = publicIP
            txtIP.Enabled = False
        End If
        'txtIP.Enabled = chkManual.Checked
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
