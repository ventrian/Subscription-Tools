Imports Ventrian.SubscriptionTools.Data

Namespace Ventrian.SubscriptionTools.Entities

    Public Class ReportController

#Region " Public Methods "

        Public Function List(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal reportType As ReportType, ByVal dateStart As DateTime, ByVal dateEnd As DateTime) As IDataReader

            Return DataProvider.Instance().ListReport(portalID, moduleID, reportType, dateStart, dateEnd)

        End Function

#End Region

    End Class

End Namespace
