Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.UI.Utilities

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class ViewInvoice
        Inherits ModuleBase

#Region " Private Members "

        Private _receiptID As Integer = Null.NullInteger

#End Region

#Region " Private Methods "

        Private Sub ReadQueryString()

            If Not (Request("ReceiptID") Is Nothing) Then
                _receiptID = Convert.ToInt32(Request("ReceiptID"))
            End If

        End Sub

        Private Sub BindCrumbs()

            Dim crumbs As New ArrayList

            Dim objCrumbMain As New CrumbInfo
            objCrumbMain.Caption = Localization.GetString("Main", LocalResourceFile)
            objCrumbMain.Url = NavigateURL()
            crumbs.Add(objCrumbMain)

            Dim objViewInvoice As New CrumbInfo
            objViewInvoice.Caption = Localization.GetString("ViewInvoice", LocalResourceFile)
            objViewInvoice.Url = Me.Request.RawUrl
            crumbs.Add(objViewInvoice)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindInvoice()

            Dim objReceiptController As New ReceiptController
            Dim objReceipt As ReceiptInfo = objReceiptController.Get(_receiptID)

            If (objReceipt Is Nothing) Then
                Response.Redirect(NavigateURL, True)
            End If

            Dim objUserController As New UserController
            Dim objUser As UserInfo = objUserController.GetUser(Me.PortalId, objReceipt.UserID)

            If (objUser Is Nothing) Then
                Response.Redirect(NavigateURL, True)
            End If

            Dim symbol As String = "$"
            Dim currency As String = Me.Currency
            If (objReceipt.Currency <> "") Then
                currency = objReceipt.Currency
            End If
            Select Case currency.ToUpper
                Case "USD"
                    symbol = "$"

                Case "GBP"
                    symbol = "£"

                Case "EUR"
                    symbol = "€"

                Case "CAD"
                    symbol = "$"

                Case "JPY"
                    symbol = "¥"
            End Select

            Dim invoice As String = Me.InvoiceTemplate

            invoice = invoice.Replace("[CITY]", objUser.Profile.City)
            invoice = invoice.Replace("[COUNTRY]", objUser.Profile.Country)
            invoice = invoice.Replace("[DATE]", objReceipt.DateCreated.ToString("MMMM, dd yyyy"))
            If (objReceipt.DateEnd <> Null.NullDate) Then
                invoice = invoice.Replace("[EXPIRYDATE]", objReceipt.DateEnd.ToString("MMMM, dd yyyy"))
            Else
                invoice = invoice.Replace("[EXPIRYDATE]", "")
            End If
            invoice = invoice.Replace("[FIRSTNAME]", objReceipt.FirstName)
            invoice = invoice.Replace("[FULLNAME]", objReceipt.FirstName & " " & objReceipt.LastName)
            invoice = invoice.Replace("[LASTNAME]", objReceipt.LastName)
            invoice = invoice.Replace("[DISPLAYNAME]", objReceipt.LastName)
            invoice = invoice.Replace("[PORTALNAME]", Me.PortalSettings.PortalName)
            invoice = invoice.Replace("[POSTALCODE]", objUser.Profile.PostalCode)
            invoice = invoice.Replace("[QTY]", "")
            invoice = invoice.Replace("[RECEIPTID]", objReceipt.ReceiptID.ToString())
            invoice = invoice.Replace("[REGION]", objUser.Profile.Region)
            invoice = invoice.Replace("[STATUS]", objReceipt.Status)
            invoice = invoice.Replace("[STREET]", objUser.Profile.Street)
            invoice = invoice.Replace("[SUBSCRIPTIONDESCRIPTION]", objReceipt.Description)
            invoice = invoice.Replace("[SUBSCRIPTIONNAME]", objReceipt.Name)
            invoice = invoice.Replace("[SUBSCRIPTIONPRICE]", symbol & objReceipt.ServiceFee.ToString("##0.00"))
            invoice = invoice.Replace("[USERNAME]", objUser.Username)

            lblInvoice.Text = "<pre>" & invoice & "</pre>"

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                ReadQueryString()
                BindCrumbs()
                BindInvoice()

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region

    End Class

End Namespace