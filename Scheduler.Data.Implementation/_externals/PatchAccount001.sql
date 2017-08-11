USE [cdi]
GO

if not Exists(select * from sys.indexes where Object_ID = Object_ID(N'SchedulerAppointmentOrders') and Name = N'IX_SchedulerAppointmentOrders_AppointmentId_incIsDeleted' )
CREATE NONCLUSTERED INDEX [IX_SchedulerAppointmentOrders_AppointmentId_incIsDeleted] ON [dbo].[SchedulerAppointmentOrders] 
(
      [AppointmentId] ASC
)
INCLUDE ( [isDeleted]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

if not Exists(select * from sys.indexes where Object_ID = Object_ID(N'[OrderSchedule]') and Name = N'IX_OrderSchedule_OrderId_incDeleted' )
CREATE NONCLUSTERED INDEX [IX_OrderSchedule_OrderId_incDeleted] ON [dbo].[OrderSchedule] 
(
      [OrderID] ASC
)
INCLUDE ( [Deleted]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

if not Exists(select * from sys.indexes where Object_ID = Object_ID(N'VisitVitals') and Name = N'IX_VisitVitals_PatientVisitID' )
CREATE NONCLUSTERED INDEX IX_VisitVitals_PatientVisitID
ON [dbo].[VisitVitals] ([PatientVisitID])

GO
