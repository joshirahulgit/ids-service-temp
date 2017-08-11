using Scheduler.Data.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.DBEntity;
using System.Data;
using Scheduler.Core;
using System.Data.SqlClient;
using eSquared.Core;

namespace Scheduler.Data.Implementation
{
    internal class AccountRepository : DatabaseRepository, IAccountRepository
    {
        public List<TechCompleteSuggestionList> UpdateTechCompleteSuggestionList(List<TechCompleteSuggestionList> toS)
        {
            foreach (TechCompleteSuggestionList list in toS)
            {
                if (list.IsDeleted)
                    DeleteTechCompleteSuggestItem(list);
                else if (list.Id == 0)
                    CreateTechCompleteSuggestItem(list);
                else
                    UpdateTechCompleteSuggestItem(list);
            }
            return GetTechCompleteSuggestItems();
        }
        public void DeactivateLocalCpt(long id)
        {
            SetConnection2Account();
            string sql = "UPDATE CodeReferences set isActive = 0 where CodeReferenceId = @id";
            ExecuteNonQuery(sql, id);
        }

        public void UpdateFiltersConfiguration(Account acct)
        {
            SetConnection2Global();
            string sql = @"UPDATE       SchedulerConfigurations
                         SET IsLocationFilterVis = @isLocationVis , IsModalityFilterVis = @isModVis , IsRoomFilterVis = @isRoomVis , IsRoleFilterVis = @isRoleVis , 
                         IsProviderFilterVis = @isproviderVis , IsApptStatusFilterVis = @isAppVis , IsDaysFilterVis = @isDaysVis , IsPhyGroupVis = @isPhysGroupVis , 
                         IsWtGroupVis = @isWTVis
                         Where AccountId = @accid";

            ExecuteNonQuery(sql, false, acct.IsLocationFilterVis, acct.IsModalityFilterVis, acct.IsRoomFilterVis, acct.IsRoleFilterVis, acct.IsProviderFilterVis,
                acct.IsApptStatusFilterVis, acct.IsDaysFilterVis, acct.IsPhyGroupVis, acct.IsWtGroupVis, GlobalContext.RequestContext.Account);

        }

        public int CreatePatientVisit(Appointment appointment)
        {
            AppointmentResourcePhysician prov = appointment.Sources.OfType<AppointmentResourcePhysician>().FirstOrDefault();
            long? providerId = null;
            if (prov != null)
                providerId = prov.Id;

            SetConnection2Account();

            String sql = string.Format(@"INSERT INTO [Visits]
                               ([PatientId]
                               ,[DateofService]
                               ,[VistReason]
                               ,[HealthProviderId]
                               ,[CreateUser]
                               ,[CreateDate]
                                )
                         VALUES
                               (@PatientId
                               ,@DateofService
                               ,@VistReason
                               ,@hpid
                               ,(SELECT TOP 1 ID from Global.dbo.Users	WHERE UserId = @createuser)
                                ,dateadd(minute, {0}, getdate())
                               );
                                SELECT scope_identity()", GetTimeZonesDiff);

            int newID = Convert.ToInt32(ExecuteScalar(sql,
                appointment.PatientID,
                appointment.StartTime,
                appointment.Visit.VisitReason,
                providerId,
                GlobalContext.RequestContext.UserName
                ));

            return newID;
        }

        public void MarkVisitAsSchedulerCreated(long id, int paitentVisitid)
        {
            SetConnection2Account();
            const string sql = @"INSERT INTO SchedulerAppointmentPatientVisits (AppointmentId, VisitId)
	                VALUES (@appId, @visitId)";

            ExecuteNonQuery(sql, id, paitentVisitid);
        }

        public int AttachProcedureToPatientVisit(int patientVisitId, Procedure entity)
        {
            String sql = string.Format(@"
IF not EXISTS(SELECT * FROM VisitProcedures vp WHERE vp.VisitId = @visId AND vp.ProcedureCode = @procCode)
BEGIN
INSERT INTO [VisitProcedures]
                               ([VisitId]
                               ,[ProcedureCode]
                               ,[ProcedureDescription]
                               ,[OnsetDate]
                               ,[StatusId]
                               ,[isDeleted]
                               ,[CreateUser]
                               ,[CreateDate],
							   ModifierCode1,
							   ModifierCode2,
							   ModifierCode3,
							   ModifierCode4
                                )
                         VALUES
                               (@VisitId
                               ,@ProcedureCode
                               ,@ProcedureDescription
                               ,dateadd(minute, {0}, getdate())
                               ,@StatusId
                               ,@isDeleted
                               ,(SELECT TOP 1 ID from Global.dbo.Users	WHERE UserId = @createuser)
                               ,dateadd(minute, {0}, getdate()),
							   @md1,
							   @md2,
							   @md3,
							   @md4
                               );
SELECT
	SCOPE_IDENTITY()
END", GetTimeZonesDiff);

            string[] mods = new string[] { null, null, null, null };
            int c = 0;
            foreach (CPTModifier m in entity.Modifiers)
            {
                mods[c] = m.Code;
                ++c;
                if (c == 4)
                    break;
            }

            int dbRes = Convert.ToInt32(ExecuteScalar(sql, patientVisitId, entity.Code, patientVisitId, entity.Code, entity.CommonDescription, 1, 0, GlobalContext.RequestContext.UserName, mods[0], mods[1], mods[2], mods[3]));

            //hook up two entites
            sql = @"INSERT INTO SchedulerAppointmentsPVData
                         (VisitId, CptType, CptId)
                        VALUES        (@vId,@cptType,@cpiId)";

            if (dbRes != 0)
                ExecuteNonQuery(sql, patientVisitId, 1 /*Procedure*/, dbRes);
            return dbRes;
        }

        public int GetPatientVisitIdByAppointmentId(long appId)
        {
            SetConnection2Account();
            string sql = @"SELECT top 1 sav.VisitId FROM SchedulerAppointmentPatientVisits sav WHERE sav.AppointmentId = @appid";
            int visitId = Convert.ToInt32(ExecuteScalar(sql, appId));
            return visitId;
        }

        public int AttachDiagnosisToPatientVisit(int patientVisitId, Diagnosis entity)
        {
            SetConnection2Account();


            String sql = string.Format(@"IF not EXISTS(SELECT * FROM VisitDiagnoses vd where vd.VisitId =@visId and vd.DiagnosisCode=@diagcode)
BEGIN
	

INSERT INTO [VisitDiagnoses]
                           ([VisitId]
                           ,[DiagnosisType]
                           ,[DiagnosisCode]
                           ,[DiagnosisDescription]
                           ,[OnsetDate]
                           ,[UpdateDate]
                           ,[StatusId]
                           ,[isDeleted]
                           ,[CreateUser]
                           ,[CreateDate]
                           )
                     VALUES
                           (@VisitId
                           ,@DiagnosisType
                           ,@DiagnosisCode
                           ,@DiagnosisDescription
                           ,dateadd(minute, {0}, getdate())
                           ,@UpdateDate
                           ,@StatusId
                           ,@isDeleted
                           ,(SELECT TOP 1 ID from Global.dbo.Users	WHERE UserId = @createuser)
                           ,dateadd(minute, {0}, getdate())
                           );
SELECT
	SCOPE_IDENTITY()

						   END", GetTimeZonesDiff);

            int dbRes = Convert.ToInt32(ExecuteScalar(sql, patientVisitId, entity.Code, patientVisitId, 0, entity.Code, entity.CommonDescription, null, 1, 0, GlobalContext.RequestContext.UserName));


            sql = @"INSERT INTO SchedulerAppointmentsPVData
                         (VisitId, CptType, CptId)
                        VALUES        (@vId,@cptType,@cpiId)";

            if (dbRes != 0)
                ExecuteNonQuery(sql, patientVisitId, 2 /*Diagnosis*/, dbRes);

            return dbRes;
        }

        public void RemoveExistingCptsInPatientVisit(int patientVisitId)
        {
            SetConnection2Account();

            string sql = @"DELETE vp FROM VisitProcedures vp
                            JOIN SchedulerAppointmentPatientVisits sapv ON vp.VisitId = sapv.VisitId
                            JOIN SchedulerAppointmentsPVData sap ON vp.VisitId = sap.VisitId AND sap.CptType=1 AND sap.CptId = vp.VisitProcedureId
                            WHERE sap.VisitId = @vid
                            ";
            ExecuteNonQuery(sql, patientVisitId);

            sql = @"DELETE vp FROM VisitDiagnoses vp
                    JOIN SchedulerAppointmentPatientVisits sapv ON vp.VisitId = sapv.VisitId
                    JOIN SchedulerAppointmentsPVData sap ON vp.VisitId = sap.VisitId AND sap.CptType=2 AND sap.CptId = vp.VisitDiagnosisId
                    WHERE sap.VisitId = @vid";
            ExecuteNonQuery(sql, patientVisitId);


            sql = @"DELETE from SchedulerAppointmentsPVData where VisitId = @vid";
            ExecuteNonQuery(sql, patientVisitId);
        }

        public List<Procedure> GetPatientVisitProceduresBytVisitId(int patientVisitId)
        {
            List<Procedure> result = new List<Procedure>();

            const string sql = @"SELECT vp.ProcedureCode FROM VisitProcedures vp WHERE vp.VisitId = @visitId";

            using (IDataReader reader = ExecuteReader(sql, patientVisitId))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        Procedure p = new Procedure();
                        string code = sr.GetString("ProcedureCode");
                        p.SetCode(code);
                        result.Add(p);
                    }
                }
            }

            return result;
        }

        public List<Diagnosis> GetPatientVisitDiagnosesBytVisitId(int patientVisitId)
        {

            List<Diagnosis> result = new List<Diagnosis>();
            SetConnection2Account();
            const string sql = @"SELECT vd.DiagnosisCode FROM VisitDiagnoses vd where vd.VisitId =  @vid";

            using (IDataReader reader = ExecuteReader(sql, patientVisitId))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        Diagnosis p = new Diagnosis();
                        string code = sr.GetString("DiagnosisCode");
                        p.SetCode(code);
                        result.Add(p);
                    }
                }
            }

            return result;
        }

        public bool CheckIfPaitentVivistExists(long id)
        {
            bool r = false;

            SetConnection2Account();
            string sql = @"SELECT TOP 1 sapv.VisitId FROM SchedulerAppointmentPatientVisits sapv where sapv.AppointmentId = @appid";

            r = Convert.ToInt32(ExecuteScalar(sql, id)) > 0;

            return r;

        }

        public List<NotificationSlotComment> GetNotificationSlotComments()
        {

            SetConnection2Account();

            List<NotificationSlotComment> resultList = new List<NotificationSlotComment>();
            string sql = @"Select 
                            Id
                            ,Comment 
                            ,IsActive 
                            ,Color 
                            From SchedulerNotificationSlotComment
                            Where IsActive = 1";

            using (var reader = ExecuteReader(sql))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        NotificationSlotComment nsComment = sr.ToNotificationSlotComment();
                        if (nsComment != null)
                        {
                            resultList.Add(nsComment);
                        }
                    }
                }
            }

            return resultList;
        }

        public List<NotificationSlot> FindAllNotificationSlots(bool initiateConnection)
        {
            if (initiateConnection)
                SetConnection2Account();

            List<NotificationSlot> r = new List<NotificationSlot>();
            const string sql = @"SELECT
	                        sns.Id,
	                        sns.DayOfWeek,
	                        sns.StartDate,
	                        CAST(sns.StartTime as datetime) AS StartTime,
	                        CAST (sns.EndTime as datetime) AS EndTime,
	                        sns.ModalityId,
	                        sns.Comment,
	                        sns.IsActive,
	                        sns.Color,
	                        sns.EndDate,
                            sns.IsActive,
							snstc.CPTCode
                        FROM SchedulerNotificationSlots sns
						LEFT JOIN SchedulerNotificationSlotToCpt snstc ON snstc.NotificationSlotId = sns.Id and snstc.IsActive=1
                        ";

            NotificationSlot lastSlot = null;
            using (var reader = ExecuteReader(sql))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        int id = sr.GetInt32("Id");
                        string cptCode = sr.GetNullableString("CPTCode");
                        if (lastSlot == null || lastSlot.Id != id)
                        {
                            lastSlot = sr.ToNotificationSlot();
                            r.Add(lastSlot);
                        }
                        if (!string.IsNullOrEmpty(cptCode))
                            lastSlot.AttachAllowedCpt(cptCode);
                    }
                }
            }
            return r;
        }

        public Diagnosis GetDiagnosisById(long id)
        {
            SetConnection2Account();
            Diagnosis result = new Diagnosis();
            string sql = GetAccountDiagnosesQuery(0);//
            sql += " and cr.CodeReferenceId = @id ";
            using (IDataReader reader = ExecuteReader(sql, id))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    if (sr.Read())
                    {
                        result = sr.ToDiagnosis();
                    }
                }
            }
            return result;
        }

        public Procedure GetProcedureById(long id)
        {
            SetConnection2Account();
            Procedure result = new Procedure();
            string sql = GetAccountProceduresQuery(0);//0 == no limit
            sql += " and cr.CodeReferenceId = @id";
            using (IDataReader reader = ExecuteReader(sql, id))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    if (sr.Read())
                    {
                        result = sr.ToProcedure();
                    }
                }
            }
            return result;
        }


        public WorkingSchedule GetAccountWorkingSchedule()
        {
            SetConnection2Account();
            return ReadAccountWorkingSchedule();
        }

        private void CreateTechCompleteSuggestItem(TechCompleteSuggestionList list)
        {
            SetConnection2Account();
            string sql = @"INSERT INTO [SchedulerTCSuggestionList]
                               ([DisplayName]
                               ,[IsVisible])
                         VALUES
                               (@dn
                               ,@isv)";
            ExecuteNonQuery(sql, list.DisplayName, list.IsVisible);
        }

        private void UpdateTechCompleteSuggestItem(TechCompleteSuggestionList list)
        {
            SetConnection2Account();
            string sql = @"UPDATE [SchedulerTCSuggestionList]
                           SET [DisplayName] = @dn
                              ,[IsVisible] = @isv
                         WHERE Id=@id";
            ExecuteNonQuery(sql, list.DisplayName, list.IsVisible, list.Id);
        }

        private void DeleteTechCompleteSuggestItem(TechCompleteSuggestionList list)
        {
            SetConnection2Account();
            string sql = @"DELETE FROM [SchedulerTCSuggestionList]
                            WHERE Id= @id";
            ExecuteNonQuery(sql, list.Id);
        }

        private List<TechCompleteSuggestionList> GetTechCompleteSuggestItems()
        {
            List<TechCompleteSuggestionList> result = new List<TechCompleteSuggestionList>();
            SetConnection2Account();

            string sql = @"SELECT [Id]
                              ,[DisplayName]
                              ,[IsVisible]
                          FROM [SchedulerTCSuggestionList] (nolock)";

            using (IDataReader reader = ExecuteReader(sql))
            using (SafeDataReader sr = new SafeDataReader(reader))
                while (sr.Read())
                    result.Add(sr.ToTechCompleteSuggestionList());

            return result;
        }

        public string GetAppointmentStatusById(long statusId)
        {
            SetConnection2Account();
            string sql = @"Select Id From SchedulerAppointmentStatuses Where Id = @id";
            return ExecuteScalar(sql, statusId).ToString();
        }

        public List<PhysicianSpeciality> UpdateReferralSpecialities(List<PhysicianSpeciality> phBusiness)
        {
            foreach (PhysicianSpeciality pSpeciality in phBusiness)
            {
                if (pSpeciality.IsDeleted)
                    DeleteReferralSpeciality(pSpeciality);
                else if (pSpeciality.Id == 0)
                    CreateReferralSpeciality(pSpeciality);
                else
                    UpdateReferralSpeciality(pSpeciality);
            }


            return GetReferralSpecialities(GlobalContext.RequestContext.AccountId);
        }

        public AppointmentResourcePhysician GetDefaultProvider(string patientLocation, bool userIsDictator)
        {
            SetConnection2Insurance();
            string sql = @"EXEC GetDefaultProviderDetails @Account, @UserID, @PatientLocation,@IsDictator";

            using (IDataReader reader = ExecuteReader(sql, CurrentAccountName, GlobalContext.RequestContext.UserName, patientLocation, userIsDictator))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                    if (sr.Read())
                        return sr.ToAppointmentResourcePhysician(CurrentAccountId);
            }
            return null;
        }


        public AppointmentResourcePhysician GetProviderBySignature(string dictatorSignature)
        {
            SetConnection2Account();
            string sql = @"select d.ID,d.DictatorId,d.IsVisibleInScheduler,d.Name,d.Tag,d.TypeId,d.LastName,d.FirstName,d.Account,d.Email,d.SendTo,d.EmailCopy,d.NPI, d.Location, d.UserId, d.Color from Dictators d where d.Signature = @signature";

            using (
                IDataReader reader = ExecuteReader(sql, dictatorSignature))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                    if (sr.Read())
                        return sr.ToAppointmentResourcePhysician(CurrentAccountId);
            }
            return null;
        }

        public Referral GetReferralByReferringId(string toString)
        {
            SetConnection2Account();
            Referral result = null;
            String sql = @"SELECT
	CAST(ID AS bigint) AS ID,
	[FirstName],
	LastName,
	Address,
	Address2,
	City,
	Zipcode,
	State,
	ReferringId,
	phone 'phone',
	fax 'fax',
	email 'email',
	TypeCode 'Type',
	[Faxing Enabled] 'FaxEnabled',
	Speciality,
    IsEmailEnabled,
    IsAutoPrintEnabled
FROM RefPhysician (nolock)
WHERE ReferringId = @ID";

            using (IDataReader reader = ExecuteReader(sql, toString))
            {
                using (SafeDataReader sReader = new SafeDataReader(reader))
                {
                    while (sReader.Read())
                    {
                        // Referral r = Referral.ExtractFromReader(sReader);
                        result = sReader.ToReferral();
                    }
                }
            }

            //            if(result !=null)
            //                result.ReferringNotes = GetReferringNotes(result.ReferralId);

            return result;
        }

        public List<CrosswalkPayer> GetCrossWalkPayers()
        {
            List<CrosswalkPayer> result = new List<CrosswalkPayer>();

            string sql = @"SELECT
	cpcw.ClientPayerCrossWalkId,
	cpcw.ClientPayerID,
	cpcw.PayerID
FROM InsuranceVerification.dbo.ClientPayersCrossWalk (NOLOCK) cpcw
WHERE cpcw.Account = @acc";

            Dictionary<int, int> payerIds = new Dictionary<int, int>();

            using (IDataReader reader = ExecuteReader(sql, GlobalContext.RequestContext.Account))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        int localId;
                        int? globalId;
                        int id = sr.GetInt32("ClientPayerCrossWalkId");
                        if (int.TryParse(sr.GetString("ClientPayerID"), out localId))
                        {
                            globalId = sr.GetNullableInt32("PayerID");
                            if (globalId.HasValue)
                            {
                                CrosswalkPayer p = new CrosswalkPayer(id, localId, globalId.Value);
                                result.Add(p);
                            }
                        }
                    }
                }
            }

            foreach (CrosswalkPayer payer in result)
            {
                var local = FindCustomPayer(payer.LocalPayerId, PayerSearchMode.Account);
                var global = FindCustomPayer(payer.GlobalPayerId, PayerSearchMode.Global);
                payer.SetPayers(local, global);
            }

            return result;
        }

        public void DeletePayerCrosswalk(int crosswalkId)
        {
            string sql = @"delete FROM InsuranceVerification.dbo.ClientPayersCrossWalk where ClientPayerCrossWalkId =@id";
            ExecuteNonQuery(sql, crosswalkId);
        }

        public void UpdatePayerCrosswalk(CrosswalkPayer crosswalk)
        {
            int userId;

            string sql = @"SELECT Top 1 ID FROM Global.dbo.Users (nolock) u WHERE u.UserId=@uId";
            userId = Convert.ToInt32(ExecuteScalar(sql, GlobalContext.RequestContext.UserName));
            try
            {
                UpdateClientCode(crosswalk);

                sql = string.Format(@"update InsuranceVerification.dbo.ClientPayersCrossWalk set ClientPayerID=@cpid, ClientPayerName=@cpname,
                        PayerID=@globalId, ModifiedDate=dateadd(minute, {0}, getdate()), ModifiedUser=@username where ClientPayerCrossWalkId = @id",
                        GetTimeZonesDiff);

                ExecuteNonQuery(sql, crosswalk.LocalPayer.VendorPayerId// PayerId
                    , crosswalk.LocalPayer.Name, crosswalk.GlobalPayer.VendorPayerId// PayerId
                    , userId, crosswalk.Id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2106)
                    throw new SchedulerException(SchedulerExceptionType.UniqueUIDViolation, "Global Payer is already assigned");
                if (ex.Number == 2601)
                    throw new SchedulerException(SchedulerExceptionType.UniqueUIDViolation, "Local payer is already assigned");
            }
        }

        public void CreateCrosswalk(CrosswalkPayer crosswalk)
        {
            int userId;

            string sql = @"SELECT Top 1 ID FROM Global.dbo.Users (nolock) u WHERE u.UserId=@uId";
            userId = Convert.ToInt32(ExecuteScalar(sql, GlobalContext.RequestContext.UserName));
            try
            {
                UpdateClientCode(crosswalk);

                sql = @"INSERT INTO InsuranceVerification.dbo.ClientPayersCrossWalk
                         (Account, ClientPayerID, ClientPayerName, PayerID,ModifiedUser)
                        VALUES        (@acc,@cid,@cName,@gId,@userid)";


                ExecuteNonQuery(sql, GlobalContext.RequestContext.Account, crosswalk.LocalPayer.VendorPayerId// PayerId
                    , crosswalk.LocalPayer.Name, crosswalk.GlobalPayer.VendorPayerId// PayerId
                    , userId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2106)
                    throw new SchedulerException(SchedulerExceptionType.UniqueUIDViolation, "Global payer is already assigned");
                if (ex.Number == 2601)
                    throw new SchedulerException(SchedulerExceptionType.UniqueUIDViolation, "Local payer is already assigned");
            }
        }

        private void UpdateClientCode(CrosswalkPayer crosswalk)
        {
            SetConnection2Account();
            string sql;
            if (!string.IsNullOrEmpty(crosswalk.LocalPayer.ClientCode))
            {
                sql = "UPDATE Payers SET ClientCode=@cc WHERE PayerID = @id";
                ExecuteNonQuery(sql, crosswalk.LocalPayer.ClientCode, crosswalk.LocalPayer.VendorPayerId// PayerId
                    );
            }
        }

        private CustomPayer FindCustomPayer(int id, PayerSearchMode mode)
        {
            SetConnection2Account();
            List<CustomPayer> results = new List<CustomPayer>();
            StringBuilder queryToExecute = new StringBuilder();
            string globalPayersQuery =
                @"
                       SELECT TOP 1
	p.PayerID,
	NULL AS VendorPayerID,
	p.IsEligible,
	p.Name,
	p.WebSite,
	pa.PayerAddressID,
	pa.PayerID AS Expr1,
	pa.Address1,
	pa.Address2,
	pa.City,
	pa.State,
	pa.Zipcode,
	pa.Phone,
	pa.Fax,
	p.IsEligible,
	CAST(0 AS bit) AS IsGlobal
FROM InsuranceVerification.dbo.Payers (NOLOCK) AS p
LEFT JOIN InsuranceVerification.dbo.PayerAddresses AS pa
	ON pa.PayerID = p.PayerID
WHERE p.PayerID = @id";

            string localPayersQuery =
                @"
                           SELECT TOP 1
	p.PayerID,
	NULL AS VendorPayerID,
	p.IsEligible,
	p.Name,
	p.WebSite,
	pa.PayerAddressID,
	pa.PayerID AS Expr1,
	pa.Address1,
	pa.Address2,
	pa.City,
	pa.State,
	pa.ZipCode,
	pa.Phone,
	pa.Fax,
	p.IsEligible,
	CAST(0 AS bit) AS IsGlobal,
	ClientCode
FROM Payers (NOLOCK) AS p
LEFT JOIN PayerAddresses AS pa
	ON pa.PayerID = p.PayerID
WHERE p.PayerID = @id"
                //WHERE     (p.Name LIKE @search) or (pa.ZipCode LIKE @search) or (pa.Address1 LIKE @search) or (pa.Address2 LIKE @search)
                //                                    ORDER BY p.Name"

                ;


            if (mode == PayerSearchMode.Account)
                queryToExecute = new StringBuilder(localPayersQuery);


            if (mode == PayerSearchMode.Global)
                queryToExecute = new StringBuilder(globalPayersQuery);

            Dictionary<int, List<Address>> addresses = new Dictionary<int, List<Address>>();

            using (IDataReader reader = ExecuteReader(queryToExecute.ToString(), id))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    if (sr.Read())
                    {
                        ReadPayerMultipleAddresses(sr, addresses, results);
                    }
                }
            }
            foreach (CustomPayer payer in results)
            {
                if (addresses.ContainsKey((int)payer.Id))
                    payer.Addresses.AddRange(addresses[(int)payer.Id]);
            }

            if (results.Count > 0)
                return results.First();
            return null;
        }

        private static void ReadPayerMultipleAddresses(SafeDataReader sr, Dictionary<int, List<Address>> addresses, List<CustomPayer> results)
        {
            var payerid = sr.GetInt32("PayerID");
            if (!addresses.ContainsKey(payerid))
                addresses[payerid] = new List<Address>();
            Address res = new Address(sr.GetInt32("PayerAddressID"),
                sr.GetNullableString("Address1") ?? string.Empty,
                sr.GetNullableString("Address2") ?? string.Empty,
                sr.GetNullableString("City") ?? string.Empty,
                sr.GetNullableString("State") ?? string.Empty,
                sr.GetNullableString("ZipCode") ?? string.Empty,
                string.Empty,
                sr.GetNullableString("Phone") ?? string.Empty, sr.GetNullableString("Fax") ?? string.Empty,
                string.Empty,
                string.Empty);

            addresses[payerid].Add(res);
            CustomPayer p = sr.ToCustomPayer();
            if (results.FirstOrDefault(o => o.Id == p.Id) == null)
                results.Add(p);
        }

        public string CheckExistProcedure(List<Procedure> procedures, DateTime appointEndDate, long patientId, long appointmentID)
        {
            const int MaxCount = 3;
            StringBuilder result = new StringBuilder();
            Procedure firstProc = procedures[0];
            SetConnection2Account();
            string sql =
                @"
SELECT sa.AppointmentID FROM SchedulerAppointments (nolock) sa Join 
SchedulerAppointmentProcedures (nolock) sap on sa.AppointmentID = sap.AppointmentID join 
SchedulerResources (nolock) sr on sa.AppointmentID = sr.AppointmentID join
PatientInfo (nolock) pinfo on sr.ResourceID = pinfo.AutoCount
where sap.CPTCode = @CPTCode and sa.EndTime > @EndTime 
and sr.ResourceType = 2 
and pinfo.AutoCount = @PtientId and sa.IsDeleted=0 and sa.Status <> 4
";
            List<long> listAppointmentIDs = new List<long>();
            using (IDataReader reader = ExecuteReader(sql, firstProc.Code, appointEndDate == DateTime.MinValue ? (object)null : appointEndDate, patientId))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        listAppointmentIDs.Add(sr.GetInt64("AppointmentID"));
                    }
                }
            }

            if (listAppointmentIDs.Count == 0)
                return String.Empty;

            int count = 1;
            foreach (long appointmentIDforSearch in listAppointmentIDs)
            {
                sql =
@"Select schr.AppointmentID,sa.StartTime,sm.Name from SchedulerResources schr join
SchedulerAppointments sa on sa.AppointmentID = schr.AppointmentID join
SchedulerModalities sm on schr.ResourceID = sm.ModalityID
where schr.ResourceID =
(Select top 1 sr.ResourceID from  SchedulerResources sr
where sr.AppointmentID = @appointmentID and sr.ResourceType = 3)
AND schr.AppointmentID = @appointmentIDforSearch";

                using (IDataReader reader = ExecuteReader(sql, appointmentID, appointmentIDforSearch))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        if (sr.Read())
                        {
                            result.AppendLine(String.Format("{0}. Date/Time: \"{1} {2}\"; Room: \"{3}\"",
                                count, sr.GetDateTime("StartTime").ToShortDateString(), sr.GetDateTime("StartTime").ToShortTimeString(), sr.GetString("Name")));
                            count++;
                            if (count >= MaxCount)
                            {
                                result.AppendLine("...");
                                return result.ToString();
                            }
                        }
                    }
                }

            }
            return result.ToString();
            //return Convert.ToInt32(ExecuteScalar(sql, firstProc.Code, appointEndDate, patientId)) > 0;

        }

        private void UpdateReferralSpeciality(PhysicianSpeciality p)
        {
            SetConnection2Account();
            string sql = @"UPDATE    RefPhysicianSpecialities
                           SET SpecialityName =@sn,
                               IsVisible =@isVis
                                Where Id = @id";

            ExecuteNonQuery(sql, p.Name, p.IsVisible, p.Id);
        }

        private void CreateReferralSpeciality(PhysicianSpeciality pSpeciality)
        {
            SetConnection2Account();
            string sql = @"	INSERT INTO RefPhysicianSpecialities
                      (SpecialityName, IsVisible)
                        VALUES     (@sn,@isv)";
            ExecuteNonQuery(sql, pSpeciality.Name, pSpeciality.IsVisible);
        }

        private void DeleteReferralSpeciality(PhysicianSpeciality p)
        {
            SetConnection2Account();
            string sql = "DELETE FROM RefPhysicianSpecialities Where Id = @id";
            ExecuteNonQuery(sql, p.Id);
        }

        private List<PhysicianSpeciality> GetReferralSpecialities(long id)
        {
            List<PhysicianSpeciality> result = new List<PhysicianSpeciality>();

            SetConnection2Account(id);

            string query = @"SELECT
	                            ID,
	                            SpecialityName,
	                            IsVisible
                            FROM RefPhysicianSpecialities (NOLOCK)
                            ORDER BY SpecialityName ASC";
            using (IDataReader reader = ExecuteReader(query))
            using (SafeDataReader sr = new SafeDataReader(reader))
                while (sr.Read())
                    result.Add(sr.ToPhysicianSpeciality());

            return result;
        }

        public List<VolumeUnit> UpdateVolumeUnits(List<VolumeUnit> serverObj)
        {
            foreach (VolumeUnit unit in serverObj)
            {
                if (unit.IsDeleted)
                    DeleteVolumeUnit(unit);
                else if (unit.Id == 0)
                    CreateVolumeUnit(unit);
                else
                    UpdateVolumeUnit(unit);
            }

            return GetVolumeUnits(GlobalContext.RequestContext.AccountId);
        }

        private void UpdateVolumeUnit(VolumeUnit unit)
        {
            SetConnection2Account();
            string sql = @"UPDATE    SchedulerVolumeUnits
                           SET DisplayName = @dn, IsVisible = @isv
                           WHERE ID=@id";

            ExecuteNonQuery(sql, unit.DisplayName, unit.IsVisible, unit.Id);
        }

        private void CreateVolumeUnit(VolumeUnit unit)
        {
            SetConnection2Account();
            string sql = @"INSERT INTO SchedulerVolumeUnits
                           (DisplayName, IsVisible)
                           VALUES
                           (@dn,@isVis)";
            ExecuteNonQuery(sql, unit.DisplayName, unit.IsVisible);
        }

        private void DeleteVolumeUnit(VolumeUnit unit)
        {
            SetConnection2Account();
            string sql = "DELETE FROM SchedulerVolumeUnits WHERE ID=@id";
            ExecuteNonQuery(sql, unit.Id);
        }


        public List<SnomedProcedure> GetSnomedSuggestionList(string searchString)
        {
            List<SnomedProcedure> result = new List<SnomedProcedure>();
            SetConnection2Global();

            string sql = @"SELECT TOP 100 Id
	                     , SnomedCode
	                     , ShortDescription + ISNULL(' (' + ConceptID + ')', '') ShortDescription
	                     , MediumDescription
	                     , LongDescription
	                     , IsEncounterCode FROM SnomedCodes (nolock)
	                     WHERE    SnomedCode LIKE @search 
                                  OR ShortDescription LIKE @search 
                                  OR ConceptID LIKE @search 

order by LongDescription
                                 ";

            using (IDataReader reader = ExecuteReader(sql, "%" + searchString + "%"))
            using (SafeDataReader sr = new SafeDataReader(reader))
                while (sr.Read())
                    result.Add(sr.ToSnomedProcedure());

            return result;
        }

        private const string E2SourceApplicationIdFromConfigFile = "E2SourceApplicationId";
        public List<AppointmentStatus> UpdateAppointmentStatuses(List<AppointmentStatus> appointmentStatuses)
        {
            foreach (AppointmentStatus status in appointmentStatuses)
                UpdateAppointmentStatus(status);

            return GetAccountAppointmentStatuses(true);
        }

        public List<CommentType> UpdateCommentTypes(List<CommentType> commentTypes)
        {
            foreach (CommentType type in commentTypes)
            {
                if (type.IsDeleted)
                {
                    DeleteCommentType(type);
                }
                else
                if (type.Id != 0)
                    UpdateCommentType(type);
                else
                {
                    CreateCommentType(type);
                }
            }

            return GetCommentTypes();
        }

        private void DeleteCommentType(CommentType type)
        {
            SetConnection2Account();
            string sql = @"Delete from SchedulerCommentTypes where Id=@id";
            ExecuteNonQuery(sql, type.Id);
        }

        private void CreateCommentType(CommentType type)
        {
            SetConnection2Account();

            int newCustomStatusId = Convert.ToInt32(ExecuteScalar("select min(Id)-1 from [SchedulerCommentTypes]"));
            if (newCustomStatusId == 0) newCustomStatusId--;

            string sql = @"INSERT INTO SchedulerCommentTypes
                      (Id,DisplayName, IsVisible)
                      VALUES     (@id,@dn,@isvis)";
            ExecuteNonQuery(sql, newCustomStatusId, type.Name, type.IsVisible);
        }

        private void UpdateCommentType(CommentType type)
        {
            SetConnection2Account();
            string sql = @"UPDATE SchedulerCommentTypes SET DisplayName = @dn,IsVisible =@vis, IsSystem=@issystem where Id = @id ";
            ExecuteNonQuery(sql, type.Name, type.IsVisible, type.IsSystem, type.Id);
        }

        public List<CommentType> GetCommentTypes()
        {
            SetConnection2Account();
            List<CommentType> result = new List<CommentType>();
            string query = @"SELECT
	                        Id,
	                        DisplayName,
	                        IsVisible,
	                        IsSystem,
                            CannedCommentEnumType
                        FROM SchedulerCommentTypes (NOLOCK)
                        ORDER BY DisplayName ASC";

            using (IDataReader reader = ExecuteReader(query))
            using (SafeDataReader sr = new SafeDataReader(reader))
                while (sr.Read())
                    result.Add(sr.ToCommentType());

            return result;
        }

        private const string PaymentFeeScheduleSql = @"   SELECT DISTINCT
	                                                           pfs.[Id]
                                                              ,pfs.[FeeScheduleName]
                                                              ,pfs.[FeeScheduleType]
                                                              ,pfs.[EffectiveDate]
                                                              ,pfs.[ExpirationDate]
                                                              ,pfs.[FeeScheduleConfigId]
                                                              ,pfs.[Amount]
                                                              ,pfs.[CreatedBy]
                                                              ,pfs.[CreatedOn]
                                                              ,pfs.[ModifiedBy]
                                                              ,pfs.[ModifiedOn]
                                                              ,pfs.[IsActive]
                                                              ,pfs.[IsDeleted]
	                                                          ,pfsc.[SchedulerLocationIds]
                                                              ,pfsc.[CodeReferenceIds]
                                                              ,pfsc.[ProcedureCodes]
                                                              ,pfsc.[StateCodes]
                                                              ,pfsc.[ZipCodes]
                                                              ,pfsc.[LocalPayerIds]
                                                         FROM [PaymentFeeSchedule] pfs
                                                         JOIN [PaymentFeeScheduleConfig] pfsc ON pfsc.[Id]=pfs.[FeeScheduleConfigId] 
JOIN #TempCodeRefIds tcri ON  tcri.CodeRefId  IN(SELECT Value FROM Global.dbo.fn_ParseTextToTable(pfsc.CodeReferenceIds,','))
JOIN #TempLocationIds tli ON tli.LocId IN(SELECT Value FROM Global.dbo.fn_ParseTextToTable(pfsc.SchedulerLocationIds,','))";

        private const string PaymentFeeScheduleSqlNew = @"   SELECT DISTINCT
	                                                           pfs.[Id]
                                                              ,pfs.[FeeScheduleName]
                                                              ,pfs.[FeeScheduleType]
                                                              ,pfs.[EffectiveDate]
                                                              ,pfs.[ExpirationDate]
                                                              ,pfs.[FeeScheduleConfigId]
                                                              ,pfs.[Amount]
                                                              ,pfs.[CreatedBy]
                                                              ,pfs.[CreatedOn]
                                                              ,pfs.[ModifiedBy]
                                                              ,pfs.[ModifiedOn]
                                                              ,pfs.[IsActive]
                                                              ,pfs.[IsDeleted]
	                                                          ,pfsc.[SchedulerLocationIds]
                                                              ,pfsc.[CodeReferenceIds]
                                                              ,pfsc.[ProcedureCodes]
                                                              ,pfsc.[StateCodes]
                                                              ,pfsc.[ZipCodes]
                                                              ,pfsc.[LocalPayerIds]
                                                         FROM [PaymentFeeSchedule] pfs
                                                         JOIN [PaymentFeeScheduleConfig] pfsc ON pfsc.[Id]=pfs.[FeeScheduleConfigId] 
                        OUTER APPLY
                        (
                                SELECT Value pCodeRefId  FROM Global.dbo.fn_ParseTextToTable(pfsc.CodeReferenceIds,',')
                        ) AS CodeRefs                                                                                               
                            OUTER APPLY
                        (
                                SELECT Value pSchLocId FROM Global.dbo.fn_ParseTextToTable(pfsc.SchedulerLocationIds,',')
                        ) AS SchLocs";


        public List<PaymentFee> GetPaymentFees(List<int> locationIds, List<int> codeReferenceIds, DateTime? date)
        {
            if ((locationIds != null && locationIds.Count == 0) || (codeReferenceIds != null && codeReferenceIds.Count == 0))
                return new List<PaymentFee>();

            SetConnection2Account();
            List<PaymentFee> result = new List<PaymentFee>();
            List<object> prms = new List<object>();


            StringBuilder query = new StringBuilder();
            query.Append(PaymentFeeScheduleSqlNew + @" WHERE pfs.IsActive = 1 AND pfs.IsDeleted = 0 AND CodeRefs.pCodeRefId IN (SELECT CodeRefId FROM #TempCodeRefIds)
            AND SchLocs.pSchLocId IN (SELECT LocId FROM #TempLocationIds) ");

            if (codeReferenceIds != null)
            {
                query.Append(" AND (");
                var c = 1;
                foreach (var cr in codeReferenceIds)
                {
                    var p = (c > 1 ? " OR" : "") +
                            string.Format(
                                "  (pfsc.CodeReferenceIds = '{0}' OR ',' + pfsc.CodeReferenceIds + ',' LIKE '%,{0},%') ",
                                cr);
                    query.Append(p);
                    c++;
                }
                query.Append(" )");
            }

            if (locationIds != null)
            {
                query.Append(" AND (");
                var c = 1;
                foreach (var l in locationIds)
                {
                    var p = (c > 1 ? " OR" : "") +
                            string.Format(
                                " (pfsc.SchedulerLocationIds = '{0}' OR ',' + pfsc.SchedulerLocationIds + ',' LIKE '%,{0},%') ",
                                l);
                    query.Append(p);
                    c++;
                }
                query.Append(" )");
            }

            if (date.HasValue)
            {
                query.Append(" AND pfs.EffectiveDate < @Date AND @Date <= CASE WHEN pfs.ExpirationDate < '01/01/1800' THEN @Date ELSE pfs.ExpirationDate END ");
                prms.Add(date.Value);
            }
            ExecuteNonQuery(@"CREATE TABLE #TempLocationIds(LocId INT PRIMARY KEY)");
            ExecuteNonQuery(@"CREATE TABLE #TempCodeRefIds(CodeRefId INT PRIMARY KEY)");

            if (locationIds != null)
                InsertBulkData(@"#TempLocationIds", new Dictionary<string, Type>() { { "LocId", typeof(int) } }, locationIds.Distinct().ToList(), (row, i) => row["LocId"] = i);
            if (codeReferenceIds != null)
                InsertBulkData(@"#TempCodeRefIds", new Dictionary<string, Type>() { { "CodeRefId", typeof(int) } }, codeReferenceIds.Distinct().ToList(), (row, i) => row["CodeRefId"] = i);

            /*      if (locationIds != null && locationIds.Count > 0 && codeReferenceIds != null && codeReferenceIds.Count > 0)
            {
                List<string> ls = new List<string>();
                List<string> cs = new List<string>();
                for (int i = 0; i < locationIds.Count; i++)
                {
                    ls.Add(
                        string.Format(
                            " (pfsc.SchedulerLocationIds = '{0}' OR pfsc.SchedulerLocationIds LIKE   +'%{0},' OR pfsc.SchedulerLocationIds LIKE '%,{0},%' OR pfsc.SchedulerLocationIds LIKE '%,{0}') ",
                            locationIds[i]));
                }

                for (int i = 0; i < codeReferenceIds.Count; i++)
                {
                    cs.Add(
                        string.Format(
                            " (pfsc.CodeReferenceIds = '{0}' OR pfsc.CodeReferenceIds LIKE   +'%{0},' OR pfsc.CodeReferenceIds LIKE '%,{0},%' OR pfsc.CodeReferenceIds LIKE '%,{0}') ",
                            codeReferenceIds[i]));
                }

                query.Append(string.Format(" AND (({0}) AND( {1})) ", string.Join(" OR ", ls), string.Join(" OR ", cs)));
            }
*/

            using (IDataReader reader = ExecuteReader(query.ToString(), prms.ToArray()))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                    while (sr.Read())
                    {
                        result.Add(sr.ToPaymentFee());
                    }
            }
            ExecuteNonQuery(@"DROP TABLE #TempLocationIds");
            ExecuteNonQuery(@"DROP TABLE #TempCodeRefIds");

            return result;
        }

        private List<AppointmentStatus> GetAccountAppointmentStatuses(bool initiateConnection)
        {
            List<AppointmentStatus> res = new List<AppointmentStatus>();
            if (initiateConnection)
                SetConnection2Account();
            string query = @"SELECT
	                            sas.Id,
	                            DisplayName,
	                            AppliedDisplayName,
	                            SortIndex,
	                            IsVisible,
	                            Color,
	                            IsSystemStatus,
								sst.AvailableAppointmentStatusId,
                                sst.PatientViewSpecific
                            FROM SchedulerAppointmentStatuses sas (NOLOCK)
							LEFT JOIN SchedulerStatusesTransition sst ON sas.Id = sst.BaseAppointmentStatus
                            ORDER BY SortIndex ASC";

            AppointmentStatus lastStatus = null;
            using (IDataReader reader = ExecuteReader(query))
            using (SafeDataReader sr = new SafeDataReader(reader))
                while (sr.Read())
                {
                    int statusid = sr.GetInt32("Id");
                    int? attachedStatusId = sr.GetNullableInt32("AvailableAppointmentStatusId");
                    bool? isPatientViewSpecific = sr.GetNullableBoolean("PatientViewSpecific");
                    //                  AppointmentStatus status = AppointmentStatus.ExtractFromReader(sr);
                    if (lastStatus == null || lastStatus.Id != statusid)
                    {
                        lastStatus = sr.ToAppointmentStatus();
                        res.Add(lastStatus);
                    }
                    if (attachedStatusId.HasValue)
                        lastStatus.AllowedTransition.Add(new AppointmentStatusTransition(attachedStatusId.Value, isPatientViewSpecific ?? false));
                }

            return res;
        }

        private void UpdateAppointmentStatus(AppointmentStatus status)
        {
            SetConnection2Account();

            if (status.IsSystemStatus)
            {

                string sql = @"UPDATE    SchedulerAppointmentStatuses SET
                DisplayName =@dn,
                IsVisible =@vis,
                SortIndex =@sin,
                AppliedDisplayName =@apdn,
                Color =@color
                WHERE Id=@id";

                ExecuteNonQuery(sql, status.StatusName, status.IsVisible, status.SortIndex, status.AppliedStatusName, status.Color,
                       status.Id);
            }
            else
            {
                if (status.IsDeleted)
                {
                    string sql = @"delete from SchedulerAppointmentStatuses where Id=@id";
                    ExecuteNonQuery(sql, status.Id);
                }
                else
                {

                    int newCustomStatusId = Convert.ToInt32(ExecuteScalar("select min(Id)-1 from [SchedulerAppointmentStatuses]"));
                    if (newCustomStatusId == 0) newCustomStatusId--;
                    if (status.Id == 0)
                    {
                        ExecuteNonQuery("SET identity_insert [SchedulerAppointmentStatuses] ON");
                        string sql = @" 
                        INSERT INTO [SchedulerAppointmentStatuses]
                               ([Id]
                               ,[DisplayName]
                               ,[IsVisible]
                               ,[SortIndex]
                               ,[AppliedDisplayName]
                               ,[Color]
                               ,[IsSystemStatus])
                         VALUES
                               (@id
                               ,@dn
                               ,@isVis
                               ,@sortIndex
                               ,@appliedDn
                               ,@color
                               ,@isSystem)
";
                        ExecuteNonQuery(sql, newCustomStatusId, status.StatusName, status.IsVisible, status.SortIndex,
                                        status.AppliedStatusName, status.Color,
                                        false);
                        ExecuteNonQuery("SET identity_insert [SchedulerAppointmentStatuses] OFF");
                    }
                    else
                    {
                        string sql = @"UPDATE    SchedulerAppointmentStatuses SET
                DisplayName =@dn,
                IsVisible =@vis,
                SortIndex =@sin,
                AppliedDisplayName =@apdn,
                Color =@color
                WHERE Id=@id";

                        ExecuteNonQuery(sql, status.StatusName, status.IsVisible, status.SortIndex, status.AppliedStatusName, status.Color,
                               status.Id);
                    }
                }
            }
        }

        public List<AuthorizationAlert> UpdateAuthorizationAlerts(List<AuthorizationAlert> serverAlerts)
        {
            SetConnection2Account();

            //            foreach (AuthorizationAlert alert in serverAlerts)
            //            {
            //                MarkAlertAsDeleted(alert.Id);
            //                //foreach (AuthorizationProcedure proc in alert.AuthorizationProcedures)
            //                MarkProcedureAsDeleted(alert.Id);
            //            }
            //
            //            foreach (AuthorizationAlert alert in serverAlerts)
            //            {
            //                int newAlertId = AddNewAlert(alert);
            //                foreach (AuthorizationProcedure proc in alert.AuthorizationProcedures)
            //                {
            //                    AddNewAuthorizationProcedure(proc, newAlertId);
            //                }
            //            }
            foreach (AuthorizationAlert alert in serverAlerts)
            {
                if (alert.IsDeleted)
                    DeleteAlert(alert);
                else if (alert.Id == 0)
                {
                    int newAlertId = AddNewAlert(alert);
                    foreach (AuthorizationProcedure proc in alert.AuthorizationProcedures)
                        AddNewAuthorizationProcedure(proc, newAlertId);
                }
                else
                {
                    UpdateAlert(alert);
                    foreach (AuthorizationProcedure proc in alert.AuthorizationProcedures)
                    {
                        if (proc.IsDeleted)
                            DeleteAuthorizationProcedure(proc.Id);
                        else if (proc.Id == 0)
                            AddNewAuthorizationProcedure(proc, (int)alert.Id);
                        else
                            UpdateAuthorizationProcedure(proc);
                    }
                }
            }

            return GetAuthorizationAlerts();
        }

        private void DeleteAuthorizationProcedure(long id)
        {
            SetConnection2Account();

            string sql = @"DELETE FROM SchedulerAuthorizationProcedures where Id =@id";
            ExecuteNonQuery(sql, id);
        }

        private void UpdateAuthorizationProcedure(AuthorizationProcedure proc)
        {
            SetConnection2Account();
            string sql = @"UPDATE    SchedulerAuthorizationProcedures
SET              PayerId = @PayerId, Description = @desc, Code = @code, ProcedureAmount = @procAmount, ProcedureUnit = @procUnit where ID =@id";

            ExecuteNonQuery(sql, proc.PayerId, proc.Description, proc.Code, proc.ProcedureAmount, proc.ProcedureUnit,
                            proc.Id);
        }

        private void UpdateAlert(AuthorizationAlert alert)
        {
            SetConnection2Account();
            string sql = @"UPDATE    SchedulerAuthorizationAlerts
SET              PayerId = @payerId, PayerName = @payerName WHERE Id=@id";

            ExecuteNonQuery(sql, alert.PayerId, alert.PayerName, alert.Id);
        }

        private void DeleteAlert(AuthorizationAlert alert)
        {
            SetConnection2Account();
            string sql = @"DELETE FROM SchedulerAuthorizationAlerts where Id=@id";
            ExecuteNonQuery(sql, alert.Id);
            sql = "DELETE FROM SchedulerAuthorizationProcedures WHERE PayerId=@id";
            ExecuteNonQuery(sql, alert.Id);
        }

        private void AddNewAuthorizationProcedure(AuthorizationProcedure proc, int newAlertId)
        {
            SetConnection2Account();

            string sql = @"INSERT INTO SchedulerAuthorizationProcedures(
                        PayerId
                        , Description
                        , Code
                        , ProcedureAmount
                        , ProcedureUnit
                        , IsDeleted)
                        VALUES     (
                            @payerId
                            ,@desc
                            ,@code
                            ,@procAmount
                            ,@procUnit
                            ,@isDeleted)";

            ExecuteNonQuery(sql, newAlertId, proc.Description, proc.Code, proc.ProcedureAmount, proc.ProcedureUnit, 0);
        }

        private int AddNewAlert(AuthorizationAlert alert)
        {
            SetConnection2Account();

            string sql = @"INSERT INTO SchedulerAuthorizationAlerts
                      (PayerId
                        , PayerName
                        , IsDeleted)
                    VALUES
                        (@payerId
                        ,@payerName
                        ,@isDeleted); SELECT @@IDENTITY";
            return Convert.ToInt32(ExecuteScalar(sql, alert.PayerId, alert.PayerName, 0));
        }

        /*
                private void MarkProcedureAsDeleted(long id)
                {
                    SetConnection2Account();

                    string sql = @"Update SchedulerAuthorizationProcedures set IsDeleted =1 where PayerId=@id";
                    ExecuteNonQuery(sql, id);
                }
        */

        /*
                private void MarkAlertAsDeleted(long id)
                {
                    SetConnection2Account();

                    string sql = @"Update SchedulerAuthorizationAlerts set IsDeleted =1 where Id=@id";
                    ExecuteNonQuery(sql, id);
                }
        */

        public List<AuthorizationAlert> GetAuthorizationAlerts()
        {
            SetConnection2Account();

            List<AuthorizationAlert> result = new List<AuthorizationAlert>();

            string sql = @"SELECT
	                    Id,
	                    PayerId,
	                    PayerName
                    FROM SchedulerAuthorizationAlerts (NOLOCK)
                    WHERE IsDeleted = 0";

            using (IDataReader reader = ExecuteReader(sql))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                        result.Add(sr.ToAuthorizationAlert());
                }
            }

            foreach (AuthorizationAlert alert in result)
            {
                alert.LoadProcedures(GetAuthorizationProcedures(alert.Id));
            }

            return result;
        }

        public List<AuthorizationAlert> GetAuthorizationAlerts(long accountId)
        {
            SetConnection2Account(accountId);

            List<AuthorizationAlert> result = new List<AuthorizationAlert>();

            string sql = @"SELECT
	                        Id,
	                        PayerId,
	                        PayerName
                        FROM SchedulerAuthorizationAlerts (NOLOCK)
                        WHERE IsDeleted = 0";

            using (IDataReader reader = ExecuteReader(sql))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                        result.Add(sr.ToAuthorizationAlert());
                }
            }

            foreach (AuthorizationAlert alert in result)
            {
                alert.LoadProcedures(GetAuthorizationProcedures(alert.Id));
            }

            return result;
        }

        private List<AuthorizationProcedure> GetAuthorizationProcedures(long payerId)
        {
            List<AuthorizationProcedure> result = new List<AuthorizationProcedure>();
            string sql = @"SELECT
	                        Id,
	                        PayerId,
	                        Description,
	                        Code,
	                        ProcedureAmount,
	                        ProcedureUnit
                        FROM SchedulerAuthorizationProcedures (NOLOCK)
                        WHERE PayerId = @payerId
                        AND IsDeleted = 0";

            using (IDataReader reader = ExecuteReader(sql, payerId))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                        result.Add(sr.ToAuthorizationProcedure());
                }
            }
            return result;
        }

        public List<AccountGenerateIDconfig> FindAllGenerateIdConfiguration()
        {
            SetConnection2Global();
            List<AccountGenerateIDconfig> res = new List<AccountGenerateIDconfig>();

            string sql = @"SELECT
	                        *
                        FROM AccountGenerateIDconfigs (NOLOCK)
                        WHERE AccountId = @accId";

            using (IDataReader reader = ExecuteReader(sql, ResolveNameByAccountId()))
            {
                while (reader.Read())
                    res.Add(reader.ToAccountGenerateIDconfig());
            }

            return res;
        }

        private AccountGenerateIDconfig GetGenerateIdConfigurationById(long id)
        {
            //            SetConnection2Global();

            string sql = @"SELECT
	                        *
                        FROM AccountGenerateIDconfigs (NOLOCK)
                        WHERE AccountGenerateIDconfigId = @id";

            using (IDataReader reader = ExecuteReader(sql, id))
            {
                if (reader.Read())
                    return reader.ToAccountGenerateIDconfig();
            }

            return null;
        }

        public void DeleteAccountIdConfiguration(AccountGenerateIDconfig genId)
        {
            SetConnection2Global();
            string sql = @"DELETE FROM AccountGenerateIDconfigs WHERE AccountGenerateIDconfigId = @accid";
            ExecuteNonQuery(sql, genId.Id);
        }

        public AccountGenerateIDconfig CreateAccountIdConfiguration(AccountGenerateIDconfig created)
        {
            SetConnection2Global();
            string sql = @"INSERT INTO AccountGenerateIDconfigs
                       (AccountId, 
                        Location, 
                        CustomLocationCode, 
                        IdTypeName, 
                        IDFormatString, 
                        StartingSeq, 
                        NextAvailableSeq, 
                        UseGuid,
                        PreFix,
                        PostFix,
                        IsSeqPadded,
                        SeqPaddingLen,
                        SeqPaddingChar,
                        SeqPaddingDir,
                        GuidLen
                        )
                    VALUES(
                        @accId,
                        @loc,
                        @customLoc,
                        @idTypeName,
                        @idFormat,
                        @startingSeq,
                        @nextava,
                        @useGuid,
                        @PreFix,
                        @PostFix,
                        @IsSeqPadded,
                        @SeqPaddingLen,
                        @SeqPaddingChar,
                        @SeqPaddingDir,
                        @GuidLen
                    );  
            select SCOPE_IDENTITY() ";

            object res = ExecuteScalar(sql,
                ResolveNameByAccountId(),
                created.Location,
                created.CustomLocationCode,
                created.IdTypeName,
                created.IDFormatString,
                created.StartingSeq,
                created.NextAvailableSeq,
                created.UseGuid,
                created.PreFix,
                created.PostFix,
                created.IsSeqPadded,
                created.SeqPaddingLen,
                created.SeqPaddingChar,
                created.SeqPaddingDir,
                created.GuidLen
                );
            long id = Convert.ToInt64(res);
            return GetGenerateIdConfigurationById(id);
        }

        public AccountGenerateIDconfig UpdateAccountIdConfiguration(AccountGenerateIDconfig updatedType)
        {
            SetConnection2Global();

            String sql = @"UPDATE [AccountGenerateIDconfigs]
            SET              
AccountId = @accId, 
Location = @loc, 
CustomLocationCode = @cli, 
IdTypeName = @idtypname, 
IDFormatString = @idstringFormat, 
StartingSeq = @starseq, 
NextAvailableSeq = @nextav, 
UseGuid = @UseGuid,
PreFix = @PreFix,
PostFix = @PostFix,
IsSeqPadded = @IsSeqPadded,
SeqPaddingLen = @SeqPaddingLen,
SeqPaddingChar = @SeqPaddingChar,
SeqPaddingDir = @SeqPaddingDir,
GuidLen = @GuidLen

where AccountGenerateIDconfigId = @agidCon";

            ExecuteNonQuery(sql,
                ResolveNameByAccountId(),
                updatedType.Location,
                updatedType.CustomLocationCode,
                updatedType.IdTypeName,
                updatedType.IDFormatString,
                updatedType.StartingSeq,
                updatedType.NextAvailableSeq,
                updatedType.UseGuid,
                updatedType.PreFix,
                updatedType.PostFix,
                updatedType.IsSeqPadded,
                updatedType.SeqPaddingLen,
                updatedType.SeqPaddingChar,
                updatedType.SeqPaddingDir,
                updatedType.GuidLen,
                updatedType.Id
                );
            return GetGenerateIdConfigurationById(updatedType.Id);
        }

        protected const int UNIQUE_IDX_VIOLATION_ERROR_CODE = 2627;

        #region Construction

        public AccountRepository(SchedulerDatabaseConnection dbConnection)
            : base(dbConnection)
        {
        }

        #endregion

        #region IAccountRepository Members

        public String FindWorkTypeDescription(string accountName, string workType, long locationId)
        {
            SetConnection2Account(accountName);

            String sql = @"SELECT TOP 1 wt.[Description] FROM 
                WorkType (NOLOCK) wt
                LEFT JOIN Location (NOLOCK) l ON wt.Location = l.Location 
                LEFT JOIN SchedulerLocation (NOLOCK) sl on sl.LocationName = l.Name
                WHERE 
                      wt.Wt      = @wtName  AND 
	                  wt.Account = @accName";
            //AND sl.LocationId = @locID";

            return ExecuteScalar(sql, workType, accountName) as String;
        }

        private string GetUniqueReferringId(string first, string last)
        {
            String accountName = ResolveNameByAccountId();
            SetConnection2Account(accountName);

            string sql = @"select ReferringId from RefPhysician where ReferringId like @name " + "+'%'";
            string name = String.Format("{0}.{1}", first, last);
            using (IDataReader reader = ExecuteReader(sql, name))
            {
                List<string> result = new List<string>();
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
                if (result.Count == 0)
                    return name;

                int maxNumber = 0;
                string lastItem = result.Last();
                var str = lastItem.Substring(name.Length, lastItem.Length - name.Length);
                if (int.TryParse(str, out maxNumber))
                {
                    name = name + (maxNumber + 1).ToString();
                }
                else
                {
                    name = GenerateUniqueName(result, name);
                }

                return name;
            }

        }

        private string GenerateUniqueName(List<string> result, string name)
        {
            Random rnd = new Random();
            name += rnd.Next(1, 1);
            if (!result.Contains(name))
            {
                return name;
            }
            return GenerateUniqueName(result, name);
        }
        // TODO: Extend method to accept account
        public Referral CreateReferral(Referral newReferral)
        {
            String accountName = ResolveNameByAccountId();

            SetConnection2Account(accountName);

            String sql = @"INSERT INTO [RefPhysician]
                   ([ReferringId]
                   ,[Account]
                   ,[Password]
                   ,[LastName]
                   ,[FirstName]
                   ,[Signature]
                   ,[Address]
                   ,[Address2]
                   ,[City]
                   ,[State]
                   ,[Zipcode]
                   ,[Phone]
                   ,[Fax]
                   ,[Email]
                   ,[EmailCopy]
                   ,[Faxing Enabled]
                   ,IsAutoPrintEnabled
                   ,[GroupId]
                   ,[IsEmailEnabled]
                   ,[EmailAttachmentsPassword]
                   ,[IsFaxCoversheetRequired]
                   ,[TypeCode]
                   ,[NPI]
                   ,[TaxId]
                   ,[AcceptedPhysicianOrderTerms]
                   ,[SignatureFile],
                    Country,
                    Speciality,
                    RefGroup,
                    OfficePhone,
                    OfficeFax,
                    MobilePhone,
                    IsActive,
                    SSN,
                    FirstLanguage,
                    SecondLanguage,
                    RefINOutStatus, 
                    ExternalID, 
                    MiddleName, 
                    Credentials)
             VALUES
                   (@ReferringId
                   ,@Account
                   ,@Password
                   ,@LastName
                   ,@FirstName
                   ,@Signature
                   ,@Address
                   ,@Address2
                   ,@City
                   ,@State
                   ,@Zipcode
                   ,@Phone
                   ,@Fax
                   ,@Email
                   ,@EmailCopy
                   ,@Faxing 
                   ,@IsAutoPrintEnabled
                   ,@GroupId
                   ,@IsEmailEnabled
                   ,@EmailAttachmentsPassword
                   ,@IsFaxCoversheetRequired
                   ,@TypeCode
                   ,@NPI
                   ,@TaxId
                   ,@AcceptedPhysicianOrderTerms
                   ,@SignatureFile,
                    @Country,
                    @Speciality,
                    @RefGroup,
                    @OfficePhone,
                    @OfficeFax,
                    @MobilePhone,
                    @IsActive,
                    @SSN,
                    @FirstLanguage,
                    @SecondLanguage,
                    @RefINOutStatus,
                    @ExternalID,
                    @MiddleName,
                    @Credentials
);SELECT SCOPE_IDENTITY() ";

            var newReferringId = string.IsNullOrEmpty(newReferral.ReferralId)
                ? GetUniqueReferringId(newReferral.FirstName, newReferral.LastName)
                : newReferral.ReferralId;
            object dbRes = ExecuteScalar(sql,
                                         newReferringId,
                                         accountName,
                                         null, //Password
                                         newReferral.LastName,
                                         newReferral.FirstName,
                                         string.IsNullOrEmpty(newReferral.Signature) ? String.Concat(newReferral.LastName, " ", newReferral.FirstName) : newReferral.Signature, //Signature
                                         String.IsNullOrEmpty(newReferral.Address) ? null : newReferral.Address,
                                         //Address
                                         String.IsNullOrEmpty(newReferral.Address2) ? null : newReferral.Address2,
                                         //Address2
                                         String.IsNullOrEmpty(newReferral.City) ? null : newReferral.City, //City
                                         String.IsNullOrEmpty(newReferral.State) ? null : newReferral.State, //State
                                         String.IsNullOrEmpty(newReferral.ZipCode) ? null : newReferral.ZipCode,
                                         //ZipCode
                                         String.IsNullOrEmpty(newReferral.Phone) ? null : newReferral.Phone,
                                         String.IsNullOrEmpty(newReferral.Fax) ? null : newReferral.Fax,
                                         String.IsNullOrEmpty(newReferral.Email) ? null : newReferral.Email,
                                         null, //EmailCopy
                                         newReferral.IsFaxingEnabled, //Faxing
                                         newReferral.IsAutoPrintEnabled, //IsAutoPrintEnabled
                                         null, //GroupId
                                         newReferral.IsEmailEnabled, //IsEmailEnabled
                                         null, //EmailAttachmentPassword
                                         false, //IsFaxCoverSheetRequired
                                         newReferral.Type,
                                         String.IsNullOrEmpty(newReferral.NPI) ? null : newReferral.NPI, //NPI
                                         String.IsNullOrEmpty(newReferral.TaxId) ? null : newReferral.TaxId,
                                         false, //AcceptedPhysicianOrderTerms
                                         null, //SignatureFile
                                         newReferral.Country,
                                         newReferral.Speciality,
                                         newReferral.Group,
                                         newReferral.OfficePhone,
                                         newReferral.OfficeFax,
                                         newReferral.MobilePhone,
                                         newReferral.IsActive,
                                         newReferral.SSN,
                                         newReferral.FirstLanguage,
                                         newReferral.SecondLanguage,
                                         newReferral.RefINOutStatus,
                                         newReferral.ExternalID,
                                         newReferral.MiddleName,
                                         newReferral.Credentials);

            //            ExecuteNonQuery("DELETE from Referrals2MarketingReps where ReferralId=@refId",);


            //            Referral ires = GetReferralById(Convert.ToInt64(dbRes));
            SetNewId(typeof(Referral), newReferral, Convert.ToInt64(dbRes));


            sql = @"INSERT INTO Referrals2MarketingReps (ReferralId, UserId)
	                VALUES (@refId, @userId);";
            //            foreach (MarketingRep rep in newReferral.MarketingReps)
            ExecuteNonQuery(sql, newReferral.Id, newReferral.PrimaryMarketerId);


            newReferral.SetNewReferringId(newReferringId);
            //Send message2E2
            if (AccountHasCRMdb(accountName))
                SendMessageToE2(accountName, newReferringId, (int)Event.EventTypes.AccountPhysicianCreated, null);

            return newReferral;
        }

        public List<String> FindRefTypesForOrderPhysicianID(string accountName)
        {
            SetConnection2Global();
            List<String> result = new List<string>();
            using (IDataReader reader = ExecuteReader(@"SELECT 
                            RefferalType 
                            FROM SchedulerOrderRefTypeMapping (nolock)
                WHERE AccountName = @account AND IsDictator = 1", accountName))
            {
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
            }
            return result;

        }

        public List<String> FindRefTypesForOrderСС(string accountName)
        {
            SetConnection2Global();
            List<String> result = new List<string>();
            using (IDataReader reader = ExecuteReader(@"SELECT 
                            RefferalType 
                            FROM SchedulerOrderRefTypeMapping  (nolock) 
                WHERE AccountName = @account AND IsCC = 1", accountName))
            {
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
            }
            return result;
        }

        public void RemoveOrder(long orderId)
        {
            SetConnection2Account();

            String sql = "UPDATE SchedulerAppointmentOrders SET isDeleted = 1 WHERE SchedulerOrderId = @ID";
            ExecuteNonQuery(sql, orderId);
            sql = string.Format("UPDATE OrderSchedule SET Deleted = 1, LastModifiedDate = dateadd(minute, {0}, getdate()) WHERE OrderID = (select top 1 OrderID from SchedulerAppointmentOrders WHERE SchedulerOrderId = @ID)", GetTimeZonesDiff);
            ExecuteNonQuery(sql, orderId);
            SendMessageToE2(GlobalContext.RequestContext.Account, orderId.ToString(), (int)Event.EventTypes.OrderScheduleUpdated, null);
        }

        public void UpdateOrderLinkedItem(long orderId, long? newAppointmentItemType, String newAppointmentItemId)
        {
            SetConnection2Account();
            BackupSchedulerAppointmentOrders(orderId);
            String sql = "UPDATE SchedulerAppointmentOrders SET AppointmentItemType = @newItemType,AppointmentItemId = @newId WHERE SchedulerOrderId = @id";

            ExecuteNonQuery(sql, newAppointmentItemType, newAppointmentItemId, orderId);
        }

        public void UpdateProcedureGuid(String oldGuid, String newGuid)
        {
            SetConnection2Account();

            try
            {
                String sql = "UPDATE SchedulerAppointmentProcedures SET ProcedureGlobalID = @NewID WHERE ProcedureGlobalID = @oldID";

                ExecuteNonQuery(sql, newGuid, oldGuid);
            }
            catch (SqlException sql)
            {
                if (sql.Number == UNIQUE_IDX_VIOLATION_ERROR_CODE)
                    throw new SchedulerException(SchedulerExceptionType.UniqueUIDViolation, "Provided identifier already exists");
                else
                    throw new SchedulerException(SchedulerExceptionType.UnknownDALError, sql.Message);
            }
            catch (Exception e)
            {
                throw new SchedulerException(SchedulerExceptionType.UnknownDALError, e.Message);
            }
        }

        public void UpdateDiagnosisGuid(String oldGuid, String newGuid)
        {
            try
            {
                SetConnection2Account();

                String sql = "UPDATE SchedulerAppointmentDiagnoses SET DiagnosGlobalID = @NewID WHERE DiagnosGlobalID = @oldID";

                ExecuteNonQuery(sql, newGuid, oldGuid);
            }
            catch (SqlException sql)
            {
                if (sql.Number == UNIQUE_IDX_VIOLATION_ERROR_CODE)
                    throw new SchedulerException(SchedulerExceptionType.UniqueUIDViolation, "Provided identifier already exists");
                else
                    throw new SchedulerException(SchedulerExceptionType.UnknownDALError, sql.Message);
            }
            catch (Exception e)
            {
                throw new SchedulerException(SchedulerExceptionType.UnknownDALError, e.Message);
            }
        }
        //
        //        public String GetWTValueByCptCode(String cptCode,long accountID)
        //        {
        //            if (String.IsNullOrEmpty(cptCode))
        //                return null;
        //
        //            String accountName = ResolveNameByAccountId(accountID);
        //            SetConnection2Global();
        //
        //            String sql   = @"SELECT AccountWtValue FROM SchedulerOrderTransforms sot
        //                            LEFT JOIN SchedulerConfigurations sc 
        //                            ON sot.SchedulerConfigurationId = sc.SchedulerConfigurationId
        //                            WHERE AccountId=@accName AND MapFieldValue= @val";
        //
        //            Object dbRes = ExecuteScalar(sql, accountName, cptCode);
        //
        //            return dbRes == null || dbRes == DBNull.Value ?  null : Convert.ToString(dbRes);
        //        }

        public List<OrderTransformParameter> FindOrderTransformParams(String mapFieldValue, long accountId)
        {
            List<OrderTransformParameter> result = new List<OrderTransformParameter>();
            String accountName = ResolveNameByAccountId(accountId);
            SetConnection2Global();
            String sql = @"                   
                SELECT sot.[TransformId]
                      ,sot.[SchedulerConfigurationId]
                      ,sot.[MapFieldValue]
                      ,sot.[AccountWtValue]
                      ,sot.[MapFieldGroup]
                      ,sot.[IsGroupPrompt]
                      ,sot.[OverrideCreationMode]
                FROM [SchedulerOrderTransforms]  (nolock) sot
                LEFT JOIN SchedulerConfigurations  (nolock) sconfig on (sconfig.SchedulerConfigurationId = sot.SchedulerConfigurationId)
                WHERE sconfig.AccountId = @accName AND  sot.MapFieldValue <> @val AND sot.[MapFieldGroup] IN         
                (SELECT MapFieldGroup FROM SchedulerOrderTransforms  (nolock) WHERE MapFieldValue = @val AND IsGroupPrompt = 1)";

            using (IDataReader reader = ExecuteReader(sql, accountName, mapFieldValue))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Add(sr.ToOrderTransformParameter());
                    }
                }
            }
            // Search Procedure after date
            //            query = @"SELECT CodeCategoryId,CodeCategory,ParentCodeCategoryId,isActive FROM CodeCategories where IsActive=1 order by CodeCategory asc";
            //try
            //{
            //    using (IDataReader reader = ExecuteReader(query))
            //    {
            //        while (reader.Read())
            //        {
            //            res.CodeCategories.Add(CodeCategory.ExtractFromReader(reader));
            //        }
            //    }
            //}
            //catch
            //{
            //    Container.RequestContext.Notifier.AddWarnings(SchedulerWarningType.CodeCategoriesAreNotAvailable, "Code categories are not presented in the database");
            //}
            return result;
        }

        public OrderTransformParameter GetOrderTransformParamForProcCode(string mapFieldValue, long accountId)
        {
            OrderTransformParameter result = new OrderTransformParameter();
            String accountName = ResolveNameByAccountId(accountId);
            SetConnection2Global();
            String sql = @"                   
                SELECT sot.* FROM SchedulerOrderTransforms  (nolock) sot INNER JOIN SchedulerConfigurations   (nolock) 
                sc ON sc.SchedulerConfigurationId = sot.SchedulerConfigurationId
                AND sc.AccountId = @accName AND sot.MapFieldValue = @val";

            using (IDataReader reader = ExecuteReader(sql, accountName, mapFieldValue))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result = sr.ToOrderTransformParameter();
                        break;
                    }
                }
            }
            return result;
        }

        public Procedure AddProcedure2LocalStorage(Procedure procedure)
        {
            return AddCpt2Local(new List<string>() { "'CPT'", "'ICD9 Proc'" }, procedure) as Procedure;
        }

        public Diagnosis AddDiagnosis2LocalStorage(Diagnosis diagnosis)
        {
            return AddCpt2Local(new List<string>() { "'ICD9 Diag'" }, diagnosis) as Diagnosis;
        }

        private CptBase AddCpt2Local(List<string> codeTypes, CptBase entity)
        {
            SetConnection2Global();
            int userId = ResolveUserIdByName(GlobalContext.RequestContext.UserName);
            String sql = "SELECT TOP 1 CodeTypeID FROM CodeTypes  (nolock) WHERE CodeTypeName in (" + string.Join(",", codeTypes) + ")";
            object dbRes = ExecuteScalar(sql);

            if (dbRes == null || dbRes == DBNull.Value)
                throw new SchedulerException(SchedulerExceptionType.LocalCPTStorageError, "Local storage is not configured properly");
            try
            {
                int codeTypeId = Convert.ToInt32(dbRes);

                SetConnection2Account();
                sql = "select CodeReferenceId from CodeReferences where CodeReferenceId = @codeRef";
                object qres = ExecuteScalar(sql, entity.Id);
                if (qres == null)
                {
                    sql = "select CodeReferenceId from CodeReferences where CodeReference = @code and isActive = 1";
                    qres = ExecuteScalar(sql, entity.Code);
                    if (qres != null) return null;

                    sql = @"INSERT INTO [CodeReferences]
                           ([ExternalCode]
                           ,[Description]
                           ,[CodeReference]
                           ,[CodeTypeId]
                           ,[isActive]
                           ,DefaultOverhead
                           ,AlertText
                           ,DefaultAmount
                           ,DefaultVolume
                           ,DefaultHCPCS
                            )
                     VALUES
                           (
                            @extCode
                           ,@desc
                           ,@codeReference
                           ,@codeTypeID
                           ,1
                           ,@defaultOverhead
                           ,@alert
                           ,@DefaultAmount
                           ,@DefaultVolume
                           ,@DefaultHCPCS);
                SELECT SCOPE_IDENTITY() ";

                    int codeReferenceCategoryId = Convert.ToInt32(ExecuteScalar(sql, entity.Code,
                        entity.ShortDescription ?? entity.LongDescription, entity.Code, codeTypeId,
                        (entity is Procedure) ? (entity as Procedure).TimeOverheadMinutes : null, entity.AlertText,
                         (entity is Procedure) ? (entity as Procedure).Amount : null,
                         (entity is Procedure) ? (entity as Procedure).Volume : null,
                         (entity is Procedure) ? (entity as Procedure).HCPCScodeName : null));

                    sql = string.Format(
                        @"INSERT INTO CodeReferenceCategories
                        (CodeReferenceId
                        ,CodeCategoryId
                        ,isActive
                        ,LastUser
                        ,LastDate
                    )
                    VALUES
                        (@CodeReferenceId, 
                        @CodeCategory, 
                        @isActive, 
                        @LastUser, 
                    dateadd(minute, {0}, getdate()))", GetTimeZonesDiff);
                    ExecuteNonQuery(sql, codeReferenceCategoryId, entity.Category, true, userId);
                }
                else
                {
                    sql = @"UPDATE [CodeReferences]
                        SET [ExternalCode] = @excode
                            ,[Description] = @desc
                            ,[CodeReference] = @codeRef
                            ,[CodeTypeId] = @code
                            ,[DefaultOverhead]=  @defOverhead
                            ,[AlertText] = @alert
                            ,[DefaultAmount] = @DefaultAmount
                            ,[DefaultVolume] = @DefaultVolume
                            ,[DefaultHCPCS] = @DefaultHCPCS
                        WHERE CodeReferenceId =@codeRefId";

                    ExecuteNonQuery(sql, entity.Code, entity.ShortDescription ?? entity.LongDescription,
                                    entity.Code, codeTypeId,
                                    (entity is Procedure) ? (entity as Procedure).TimeOverheadMinutes : null, entity.AlertText,
                                       (entity is Procedure) ? (entity as Procedure).Amount : null,
                                         (entity is Procedure) ? (entity as Procedure).Volume : null,
                                           (entity is Procedure) ? (entity as Procedure).HCPCScodeName : null, qres);

                    sql = "select top 1 [CodeReferenceCategoryId] from [CodeReferenceCategories] where [CodeReferenceId] = @CodeRefId";
                    object codeCategoryId = ExecuteScalar(sql, entity.Id);
                    if (codeCategoryId == null && !string.IsNullOrEmpty(entity.Category))
                    {
                        sql = string.Format(
                            @"INSERT INTO CodeReferenceCategories
                            (CodeReferenceId
                            ,CodeCategoryId
                            ,isActive
                            ,LastUser
                            ,LastDate
                            )
                            VALUES
                            (@CodeReferenceId, 
                            @CodeCategory, 
                            @isActive, 
                            @LastUser, 
                            dateadd(minute, {0}, getdate()))", GetTimeZonesDiff);
                        ExecuteNonQuery(sql, entity.Id, entity.Category, true, userId);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(entity.Category))
                        {
                            sql = @"DELETE FROM CodeReferenceCategories
                            WHERE CodeReferenceCategoryId = @categoryId";
                            ExecuteNonQuery(sql, Convert.ToInt32(codeCategoryId));
                        }
                        else
                        {
                            sql = string.Format(@"UPDATE CodeReferenceCategories
                            set 
                               CodeCategoryId = @category
                               ,isActive = 1
                               ,LastUser = @lastUser
                               ,LastDate = dateadd(minute, {0}, getdate())
                            WHERE CodeReferenceCategoryId = @categoryId", GetTimeZonesDiff);
                            ExecuteNonQuery(sql, entity.Category, userId, Convert.ToInt32(codeCategoryId));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SchedulerException(SchedulerExceptionType.LocalCPTStorageError, e.Message);
            }
            return entity;
        }

        public void UpdatePhysicianType(PhysicianType type)
        {
            SetConnection2Account();

            String sql = "UPDATE SchedulerPhysicianTypes SET TypeName = @Name,TypeColor = @TypeColor WHERE TypeId = @ID";

            ExecuteNonQuery(sql, type.Name, type.Color, type.Id);
        }

        public Dictionary<string, string> GetApplicationConfig(string appName, string accountName, string userName)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            SetConnection2Global();

            string sql = "execute GetApplicationConfigurations @appName,@accName,@userName";

            using (IDataReader reader = ExecuteReader(sql, appName, accountName, userName))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        string key = sr.GetNullableString("SettingName");
                        if (String.IsNullOrEmpty(key) || result.ContainsKey(key))
                            continue;

                        result.Add(key, sr.GetNullableString("SettingValue"));
                    }
                }
            }
            return result;
        }

        public List<Account> FindAllAccounts()
        {
            SetConnection2Global();

            //List<long> accountIds  = new List<long>();
            List<Account> accounts = new List<Account>();
            String sql = @"select id,Account from ValidAccounts (nolock) where UserId = @user /*and Admin = 1*/ AND Account in (SELECT name FROM sys.databases (NOLOCK))";
            using (IDataReader reader = ExecuteReader(sql, GlobalContext.RequestContext.UserName))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        accounts.Add(new Account(Convert.ToInt64(sr.GetDecimal(0)), sr.GetNullableString(1)));
                    }
                }
            }
            return accounts;
            //using (IDataReader reader = ExecuteReader(sql, Container.RequestContext.UserName))
            //{
            //    using (SafeDataReader sr = new SafeDataReader(reader))
            //    {
            //        while (sr.Read())
            //        {
            //            accountIds.Add(Convert.ToInt64(sr.GetDecimal(0)));
            //        }
            //    }
            //}

            //List<Account> results = new List<Account>(accountIds.Count);
            //foreach (long id in accountIds)
            //{
            //    try
            //    {
            //        results.Add(GetById(id));
            //    }
            //    catch (Exception e)
            //    {
            //        Container.RequestContext.Notifier.AddWarnings(SchedulerWarningType.AccountIsBroken, e.Message);
            //    }
            //}
            //return results;
        }

        private int ResolveUserIdByName(string userName)
        {
            SetConnection2Global();
            string sql = @"SELECT top 1 ID FROM ValidAccounts (NOLOCK) va WHERE va.UserId = @userId AND Account = @account";

            return
                Convert.ToInt32(ExecuteScalar(sql, userName, ResolveNameByAccountId(GlobalContext.RequestContext.AccountId)));

        }
        public string GetEmailTemplate(int emailType)
        {
            SetConnection2Account();

            String sql = "select Template from SchedulerEmailTemplates where typeID = @id";

            object dbRes = ExecuteScalar(sql, emailType);

            if (dbRes != null && dbRes != DBNull.Value)
                return dbRes as String;

            return null;
        }

        public Referral GetReferralById(long refId)
        {
            SetConnection2Account();
            Referral result = null;
            String sql = @"SELECT
	                CAST(ID AS bigint) AS ID,
	                [FirstName],
	                LastName,
	                Address,
	                Address2,
	                City,
	                Zipcode,
	                State,
	                ReferringId,
	                phone 'phone',
	                fax 'fax',
	                email 'email',
	                TypeCode 'Type',
	                [Faxing Enabled] 'FaxEnabled',
	                Country,
	                Speciality,
	                RefGroup,
	                OfficePhone,
	                OfficeFax,
	                MobilePhone,
	                IsActive,
	                SSN,
	                FirstLanguage,
	                SecondLanguage,
	                RefINOutStatus,
                    IsEmailEnabled,
                    IsAutoPrintEnabled
                FROM RefPhysician (NOLOCK)
                WHERE [ID] = @ID";

            using (IDataReader reader = ExecuteReader(sql, refId))
            {
                using (SafeDataReader sReader = new SafeDataReader(reader))
                {
                    if (sReader.Read())
                    {
                        // Referral r = Referral.ExtractFromReader(sReader);
                        result = sReader.ToReferral();
                    }
                }
            }

            //            if(result !=null)
            //                result.ReferringNotes = GetReferringNotes(result.ReferralId);

            return result;
        }

        public Referral UpdateReferral(Referral updatedReferring)
        {
            SetConnection2Account();
            String accountName = ResolveNameByAccountId();

            string sql = @"UPDATE RefPhysician
                          SET
	                        LastName = @lastName
	                        , FirstName = @fisrtName
	                        , Signature = @signature
	                        , Address = @address
	                        , Address2 = @address2
	                        , State = @state
	                        , Zipcode = @zip
	                        , City = @city
	                        , Phone = @phone
	                        , Fax = @fax
	                        , Email = @email
	                        , [Faxing Enabled] = @faxEnabled
                            , IsAutoPrintEnabled = @ape
                            , IsEmailEnabled = @ee
                            , NPI =  @npi
                            , TaxId = @taxid
                            , Country = @Country
                            , Speciality = @Speciality
                            , RefGroup = @RefGroup
                            , OfficePhone = @OfficePhone
                            , OfficeFax = @OfficeFax
                            , MobilePhone = @MobilePhone
                            , IsActive = @isActive
                            , SSN = @SSN
                            , FirstLanguage = @FirstLanguage
                            , SecondLanguage = @SecondLanguage
                            , RefINOutStatus = @RefINOutStatus
                            , ExternalID = @ExternalID
                            , MiddleName = @MiddleName
                            , Credentials = @Credentials
                            WHERE ID = @id";

            ExecuteNonQuery(sql,
                            updatedReferring.LastName
                            , updatedReferring.FirstName
                            , updatedReferring.Signature
                            , updatedReferring.Address
                            , updatedReferring.Address2
                            , updatedReferring.State
                            , updatedReferring.ZipCode
                            , updatedReferring.City
                            , updatedReferring.Phone
                            , updatedReferring.Fax
                            , updatedReferring.Email
                            , updatedReferring.IsFaxingEnabled
                            , updatedReferring.IsAutoPrintEnabled
                            , updatedReferring.IsEmailEnabled
                            , updatedReferring.NPI
                            , updatedReferring.TaxId
                            , updatedReferring.Country
                            , updatedReferring.Speciality
                            , updatedReferring.Group
                            , updatedReferring.OfficePhone
                            , updatedReferring.OfficeFax
                            , updatedReferring.MobilePhone
                            , updatedReferring.IsActive
                            , updatedReferring.SSN
                            , updatedReferring.FirstLanguage
                            , updatedReferring.SecondLanguage
                            , updatedReferring.RefINOutStatus
                            , updatedReferring.ExternalID
                            , updatedReferring.MiddleName
                            , updatedReferring.Credentials
                            , updatedReferring.Id);


            /*
                        ExecuteNonQuery("DELETE from Referrals2MarketingReps where ReferralId=@refId", updatedReferring.Id);
                        sql = @"INSERT INTO Referrals2MarketingReps (ReferralId, UserId)
                                VALUES (@refId, @userId)";
            //            foreach (MarketingRep rep in updatedReferring.MarketingReps)
                            ExecuteNonQuery(sql, updatedReferring.Id, updatedReferring.PrimaryMarketerId);*/



            Referral ires = GetReferralById(updatedReferring.Id);

            //Send message2E2 if CRM db exists
            if (AccountHasCRMdb(accountName))
                SendMessageToE2(accountName, ires.ReferralId, (int)Event.EventTypes.AccountPhysicianUpdated, null);

            return ires;
        }

        private bool AccountHasCRMdb(string accName)
        {
            //Sunil 10/16/2015: take into account new crm only accounts
            string sql = @"SELECT SUM(num) Num FROM ( SELECT count(1) AS Num FROM Global.dbo.Accounts (NOLOCK) a WHERE AccountId = @acc AND RelatedCRMDBName IS NOT NULL
                        AND len(RelatedCRMDBName) > 1 AND RelatedCRMDBName <> ''
                        UNION
                        SELECT CASE WHEN COUNT(1) > 0 THEN 1 ELSE 0 END AS Num
                        FROM Global.dbo.CrmUsersInAccounts cuia WHERE cuia.Account = @accN ) X ";
            int res = Convert.ToInt32(ExecuteScalar(sql, accName, accName).ToString());
            return res > 0;
        }

        public List<Referral> FindReferrals(string searchString)
        {
            SetConnection2Account();

            List<Referral> result = new List<Referral>();
            //Sunil: enable find only active refphysicians - patch 81 takes care of column and defaults
            //Sunil: 11/29/2012 - add scoring to results to make the results more organized
            String sql = @"SELECT
	                CAST(ID AS bigint) AS ID,
	                FirstName,
	                LastName,
	                Address,
	                Address2,
	                City,
	                Zipcode,
	                State,
	                ReferringId,
	                phone AS 'phone',
	                fax AS 'fax',
	                email AS 'email',
	                TypeCode AS 'Type',
	                [Faxing Enabled] AS 'FaxEnabled',
	                Speciality,
	                IsEmailEnabled,
	                NPI,
	                TaxId,
	                CASE
		                WHEN FirstName = @request THEN 100
		                WHEN FirstName LIKE @request + '%' THEN 90
		                WHEN LastName = @request THEN 80
		                WHEN LastName LIKE @request + '%' THEN 70
		                WHEN [Signature] LIKE @request + '%' THEN 60
		                WHEN [Signature] LIKE '%' + @request + '%' THEN 50
		                ELSE 0
	                END AS Score,
                    IsAutoPrintEnabled
                FROM RefPhysician (NOLOCK)
                WHERE IsActive = 1
                AND [Signature] LIKE ('%' + @request + '%')
                ORDER BY Score DESC, FirstName, LastName, Zipcode";

            using (IDataReader reader = ExecuteReader(sql, searchString))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Add(sr.ToReferral());
                    }
                }
            }

            return result;
        }

        private void GenerateSearchParam(String columnName, String filterValue, StringBuilder sql, List<string> parameters, ref bool filterAdded, bool startsFromMatch)
        {
            if (String.IsNullOrEmpty(filterValue))
                return;

            string varName = columnName.Replace('.', '_').Replace(' ', '_').Replace('[', '_').Replace(']', '_');
            if (filterAdded)
                sql.Append(" AND ");
            else
                sql.Append(" WHERE ");

            if (columnName.ToLower().Contains("pt dob"))
            {
                sql.Append(string.Format(@"(case (ISDATE({0})) when 1 then convert(varchar(50), convert(date, {0})) else {0} end = 
                                            case (ISDATE(@{1})) when 1 then convert(varchar(50), convert(date, @{1})) else @{1} end )", columnName, varName));
            }
            else
            {
                sql.Append(columnName);
                if (startsFromMatch)
                {
                    sql.Append(@" LIKE( ");
                }
                else
                {
                    sql.Append(@" LIKE('%' + ");
                }
                sql.Append(" @").Append(varName);
                sql.Append(@" + '%')");
            }


            //sql.Append(" = ");
            //sql.Append(" @").Append(columnName.Replace('.', '_').Replace(' ', '_').Replace('[', '_').Replace(']', '_'));
            parameters.Add(filterValue);
        }

        public void SendMessageToE2(string accountName, string key, int evtTypId, string additionalInfoString)
        {
            //SetConnection2Esquared();

            int E2SourceApplicationId = GlobalContext.ApplicationSetting.E2SourceApplicationId;
            //Int32.Parse(WebConfigurationManager.AppSettings[E2SourceApplicationIdFromConfigFile].ToString());
            using (SqlConnection sqlConnection = new SqlConnection(_databaseConnection.ConnectionString))
            {
                if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                sqlConnection.ChangeDatabase("esquared");

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = string.Format(" INSERT INTO [Events] " +
                                                           "([AccountName],[JobID],[EventTypeID],[SourceApplicationID],[CreateDateTime],[CurrentStateID], AdditionalInfoInt, AdditionalInfoString) " +
                                                           "VALUES(@account,@jobId,@eventTypeId,@E2SourceApplicationID,dateadd(minute, {0}, getdate()), 1,@additionalInfoInt,@additionalInfoString )",
                        GetTimeZonesDiff);

                    sqlCommand.Parameters.AddWithValue("@account", accountName);
                    sqlCommand.Parameters.AddWithValue("@jobId", key);
                    sqlCommand.Parameters.AddWithValue("@eventTypeId", evtTypId);
                    sqlCommand.Parameters.AddWithValue("@E2SourceApplicationID", E2SourceApplicationId);
                    sqlCommand.Parameters.AddWithValue("@additionalInfoInt", 0);
                    sqlCommand.Parameters.AddWithValue("@additionalInfoString",
                        additionalInfoString ?? GlobalContext.RequestContext.UserName);


                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }



            /* string sqlE2 = string.Format(" INSERT INTO [Events] " +
                            "([AccountName],[JobID],[EventTypeID],[SourceApplicationID],[CreateDateTime],[CurrentStateID], AdditionalInfoInt, AdditionalInfoString) " +
                            "VALUES(@account,@jobId,@eventTypeId,@E2SourceApplicationID,dateadd(minute, {0}, getdate()), 1,@additionalInfoInt,@additionalInfoString )",GetTimeZonesDiff);

             ExecuteNonQuery(sqlE2,
                             accountName,
                             key,
                             evtTypId,
                             E2SourceApplicationId,
                             0,
                             Container.RequestContext.UserName);*/
        }

        public List<Procedure> GetProcedureSuggestionListWithRoom(string searchString, int category, CPTCodeSearchMode mode, bool exactMatch,
            long? roomId)
        {
            List<Procedure> result = new List<Procedure>();
            SetConnection2Account(true);

            if (mode == CPTCodeSearchMode.Both || mode == CPTCodeSearchMode.Account)
            {

                List<object> sqlParams = new List<object>();
                string sql = GetAccountProceduresQueryWithRooms(100, roomId);

                if (roomId.HasValue)
                    sqlParams.Add(roomId);

                if (!string.IsNullOrEmpty(searchString.Trim()))
                {
                    if (exactMatch)
                    {
                        sql += (@"AND (cr.ExternalCode LIKE @search OR cr.Description = @search OR CodeReference = @search)");
                        sqlParams.Add(searchString);
                    }
                    else
                    {
                        sql += (@"AND (cr.ExternalCode LIKE @search OR cr.Description LIKE @search OR CodeReference LIKE @search)");
                        sqlParams.Add("%" + searchString + "%");
                    }
                }
                if (category > 0)
                {
                    //Select all child categories 
                    String sql2 = @"WITH C (CodeCategoryId,CodeCategory,ParentCodeCategoryId) AS 
                                    (
	                                    SELECT B.CodeCategoryId,B.CodeCategory,B.ParentCodeCategoryId FROM 
	                                    CodeCategories  (NOLOCK) AS B WHERE B.CodeCategoryId = @catID
	                                    UNION ALL
	                                    SELECT B.CodeCategoryId, B.CodeCategory, B.ParentCodeCategoryId FROM CodeCategories  (NOLOCK) AS B
		                                    INNER JOIN C ON C.CodeCategoryId = B.ParentCodeCategoryId
                                    )
                                    SELECT CodeCategoryId FROM C";
                    StringBuilder catIds = new StringBuilder(" AND crc.CodeCategoryId IN (");
                    using (IDataReader reader = ExecuteReader(sql2, category))
                    {
                        int counter = 0;

                        while (reader.Read())
                        {
                            catIds.Append(String.Format("@p{0},", ++counter));
                            sqlParams.Add(reader.GetInt32(0));
                        }
                        catIds.Remove(catIds.Length - 1, 1);
                        catIds.Append(")");
                    }
                    sql += catIds.ToString();
                }

                sql += "   ORDER BY ShortDesc ASC";

                using (IDataReader reader = ExecuteReader(sql, sqlParams.ToArray()))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            Procedure proc = sr.ToProcedure();
                            //Sunil: 2015/11/10 this suggestion list does not get the val from db so its always null
                            // causing the grouped procedures to report a false IsOrderRequired true value
                            //todo: Sunil: this is a workaround need to fix
                            proc.OverrideCreationMode = 0;
                            if (result.Count(a => a.Code == proc.Code) == 0)
                                result.Add(proc);
                        }
                    }
                }
            }

            if (mode == CPTCodeSearchMode.Both || mode == CPTCodeSearchMode.Global)
            {
                //                SetConnection2Global();

                String sql = @"
                        SELECT 
                            TOP 100 * 
                            FROM( 
                                    SELECT 
                                       -1 'ID',
                                       [CPTCode]          'ICD9Code',
                                       [ShortDescription] 'ShortDesc',
                                       [MediumDescription] 'Mediumdesc',
                                       [LongDescription]   'LongDesc',
                                       '' as ProcedureGlobalID ,
                                       1  as IsGlobal,
                                       NULL 'ModalityId',
									   mtc.MammogramType,
                                       0 as TimeOverHead
                                    FROM [Global].dbo.[CPTCodesFull] (NOLOCK) c
                                        left join MammogramTypesToCpt (NOLOCK) mtc on mtc.Cpt = c.CPTCode
                                    WHERE 
                                        CPTCode LIKE @search 
                                        OR ShortDescription LIKE @search 
                                  ) AS FullList 
                        ORDER BY ShortDesc ASC";

                using (IDataReader reader = ExecuteReader(sql, "%" + searchString + "%"))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            Procedure proc = sr.ToProcedure();
                            if (result.Count(a => a.Code == proc.Code) == 0)
                                result.Add(proc);
                        }
                    }
                }
            }

            return result;
        }


        public List<ModalityType> FindModalityTypesByModality(int modalityId)
        {
            SetConnection2Account();
            List<ModalityType> res = new List<ModalityType>();
            string query =
                    @"select distinct 
                            smt.TypeId,
                            smt.Name, 
                            smt.AllowComparision, 
                            smsr.ModalityID,
                            smsr.Estimate, 
                            smsr.IsMammographyResource,
                            smsr.IsActive
                        FROM SchedulerModalityTypes smt  
						LEFT JOIN SchedulerMultipleServiceResources smsr ON smt.TypeId = smsr.Type
                        where smsr.ModalityId=@modiD
                    ORDER BY Name";

            using (IDataReader reader = ExecuteReader(query, modalityId))
                while (reader.Read())
                    res.Add(reader.ToModalityType());

            return res;
        }

        public void UpdateNotificationSlots(List<NotificationSlot> sts)
        {
            SetConnection2Account();
            ExecuteNonQuery(@"CREATE TABLE #TempSNS (
	            Id INT CONSTRAINT PK_12qwedfvgbhnjmki8uyhgfvcdxsd PRIMARY KEY CLUSTERED
	            ,DayWeek VARCHAR(15),StartTime DATETIME,EndTime DATETIME,ModalityId INT
	            ,Comment VARCHAR(100),IsActive BIT,Color VARCHAR(50),StartDate DATETIME,EndDate DATETIME
            )");
            var cols = new Dictionary<string, Type>()
            {
                {"Id", typeof(int) },
                {"DayWeek", typeof(string) },
                {"StartTime", typeof(DateTime) },
                {"EndTime", typeof(DateTime) },
                {"ModalityId", typeof(int) },
                {"Comment", typeof(string) },
                {"IsActive", typeof(bool) },
                {"Color", typeof(string) },
                {"StartDate", typeof(DateTime) },
                {"EndDate", typeof(DateTime) },
            };

            InsertBulkData(@"#TempSNS", cols, sts.ToList(), (r, ns) =>
            {
                r["Id"] = ns.Id;
                r["DayWeek"] = (string.IsNullOrEmpty(ns.DayOfWeek) ? (object)DBNull.Value : ns.DayOfWeek);
                r["StartTime"] = ns.StartTime;
                r["EndTime"] = ns.EndTime;
                r["ModalityId"] = ns.ModalityId;
                r["Comment"] = (string.IsNullOrEmpty(ns.Comment) ? (object)DBNull.Value : ns.Comment);
                r["IsActive"] = ns.IsActive;
                r["Color"] = (string.IsNullOrEmpty(ns.Color) ? (object)DBNull.Value : ns.Color);
                r["StartDate"] = ns.StartDate ?? (object)DBNull.Value;
                r["EndDate"] = ns.EndDate ?? (object)DBNull.Value;
            });

            string sql = @"INSERT INTO SchedulerNotificationSlots_Audit (SnsId, DayOfWeek, StartTime, EndTime, 
			ModalityId, Comment, IsActive, Color, StartDate, EndDate, CreateUser, CreateDate)
		        SELECT
			        sns.Id
			        ,sns.DayOfWeek
			        ,CAST(sns.StartTime AS TIME) StartTime
			        ,CAST(sns.EndTime AS TIME) EndTime
			        ,sns.ModalityId
			        ,sns.Comment
			        ,sns.IsActive
			        ,sns.Color
			        ,sns.StartDate
			        ,sns.EndDate
                    ,@usr
                    ,GETDATE()
		        FROM #TempSNS tsns
		        INNER JOIN SchedulerNotificationSlots sns ON tsns.Id = sns.Id";
            ExecuteNonQuery(sql, GlobalContext.RequestContext.UserName);

            sql = @"INSERT INTO SchedulerNotificationSlots (DayOfWeek, StartTime, EndTime, ModalityId, Comment, IsActive, Color, StartDate, EndDate)
		        SELECT
			        DayWeek
			        ,CAST(StartTime AS TIME) StartTime
			        ,CAST(EndTime AS TIME) EndTime
			        ,ModalityId
			        ,Comment
			        ,IsActive
			        ,Color
			        ,StartDate
			        ,EndDate
		        FROM #TempSNS tsns
		        WHERE tsns.Id < 0";
            ExecuteNonQuery(sql);

            sql = @"UPDATE sns SET
	            [DayOfWeek] = tsns.DayWeek
	            ,StartTime = CAST(tsns.StartTime AS TIME)
	            ,EndTime = CAST(tsns.EndTime AS TIME)
	            ,ModalityId = tsns.ModalityId
	            ,Comment = tsns.Comment
	            ,IsActive = tsns.IsActive
	            ,Color= tsns.Color
	            ,StartDate = tsns.StartDate
	            ,EndDate = tsns.EndDate
            FROM #TempSNS tsns
            INNER JOIN SchedulerNotificationSlots sns
	            ON tsns.Id = sns.Id
            WHERE tsns.Id > 0";
            ExecuteNonQuery(sql);
            ExecuteNonQuery(@"DROP TABLE #TempSNS");
        }

        public void DeleteNotificationSlots(List<NotificationSlot> sts)
        {
            SetConnection2Account();
            ExecuteNonQuery($"Delete SchedulerNotificationSlotToCpt WHERE NotificationSlotId IN ({string.Join(",", sts.Select(s => s.Id))})");
            ExecuteNonQuery($"Delete SchedulerNotificationSlots WHERE Id IN ({string.Join(",", sts.Select(s => s.Id))})");
        }

        #region Referring Notes

        public ReferringNote CreateReferringNote(string referringID, ReferringNote note)
        {

            if (String.IsNullOrEmpty(note.Text))
                throw new SchedulerException(SchedulerExceptionType.CannotCreateEmptyComment, "System doesn't allow creation of empty comments");

            SetConnection2Account();

            string sql = @" INSERT INTO RefPhysicianNotes
                            (
                                ReferringId
                                ,Note
                                ,isAlert
                                ,OrderId
                                ,AppointmentId
                                ,CreateUserID
                            )
                            VALUES
                            (
                                @ReferringId
                                ,@Note
                                ,@isAlert
                                ,@OrderId
                                ,@AppointmentId
                                ,@CreateUserID
                            ); SELECT SCOPE_IDENTITY() ";

            int newID = Convert.ToInt32(ExecuteScalar(sql, referringID, note.Text, note.isAlert, note.OrderId, note.AppointmentId, GlobalContext.RequestContext.UserName));
            SetNewId(typeof(ReferringNote), note, newID);
            return note;
        }

        public List<ReferringNote> GetReferringNotes(string referringID)
        {
            SetConnection2Account();

            List<ReferringNote> results = new List<ReferringNote>();

            try
            {
                String sql = @"SELECT RefPhysicianNoteId ID
	                             , Note CommentText
	                             , isAlert
	                             , OrderId
	                             , AppointmentId
	                             , CreateUserID UserID
	                             , CreateDate LastDate FROM RefPhysicianNotes (NOLOCK) rpn
                                WHERE isDeleted = 0 AND ReferringId = @rfid ORDER BY CreateDate DESC";
                using (IDataReader r = ExecuteReader(sql, referringID))
                {
                    using (SafeDataReader sr = new SafeDataReader(r))
                    {
                        while (sr.Read())
                        {
                            results.Add(sr.ToReferringNote());
                        }
                    }
                }
            }
            catch
            {
                //CP: Fix, Not sure about the functionality
                //GlobalContext.RequestContext.Notifier.AddWarnings(Common.Messages.SchedulerWarningType.PatientCommentsStorageIsBroken, "Can not read patient comments");
            }

            return results;
        }

        public string CheckExistReferral(Referral referral)
        {
            SetConnection2Account();
            int count = 0;
            StringBuilder result = new StringBuilder();
            String sql;
            if (!String.IsNullOrEmpty(referral.NPI))
            {
                sql =
@"SELECT COUNT ([ID]) 
  FROM [RefPhysician] (NOLOCK) 
  where NPI = @NPI AND IsActive=1";
                count = (int)ExecuteScalar(sql, referral.NPI);
                if (count > 0)
                {
                    result.Append("NPI");
                    count = 0;
                }
            }

            if (!String.IsNullOrEmpty(referral.TaxId))
            {
                sql =
@"SELECT COUNT ([ID]) 
  FROM [RefPhysician]  (NOLOCK) 
  where TaxId = @TaxId AND IsActive=1";
                count = (int)ExecuteScalar(sql, referral.TaxId);
                if (count > 0)
                {
                    if (result.Length > 0)
                        result.Append(", TaxId");
                    else
                        result.Append("TaxId");
                    count = 0;
                }
            }

            sql =
@"SELECT COUNT ([ID]) 
  FROM [RefPhysician]  (NOLOCK) 
  where LastName = @LastName and FirstName = @FirstName AND IsActive=1";

            if (!String.IsNullOrEmpty(referral.ZipCode))
            {
                sql += String.Format(" and ZipCode = '{0}'", referral.ZipCode);
            }
            if (!String.IsNullOrEmpty(referral.Phone))
            {
                sql += String.Format(" and Phone = '{0}'", referral.Phone);
            }
            count = (int)ExecuteScalar(sql, referral.LastName, referral.FirstName);
            if (count > 0)
            {

                if (result.Length > 0)
                    result.Append(String.Format(", Last Name, First Name{0}{1}", !String.IsNullOrEmpty(referral.ZipCode) ? ", Zip" : "", !String.IsNullOrEmpty(referral.Phone) ? ", Phone" : ""));
                else
                    result.Append(String.Format("Last Name, First Name{0}{1}", !String.IsNullOrEmpty(referral.ZipCode) ? ", Zip" : "", !String.IsNullOrEmpty(referral.Phone) ? ", Phone" : ""));
                count = 0;
            }
            return result.ToString();
        }

        #endregion


        public String CreateOrderEx(long appId, int? appItemType, String appItemId, OrderCreationValues values)
        {
            String orderID = values.GetSQLParam(OrderCreationParameter.ORDER_ID) as String;
            String accountName = values.GetSQLParam(OrderCreationParameter.ACCOUNT_NAME_PARAM) as String;
            if (!String.IsNullOrEmpty(accountName))
                SetConnection2Account(accountName);
            else
                SetConnection2Account();

            //Here we convert scheduler location id to abbadox location id and name 
            object locationId = values.GetSQLParam(OrderCreationParameter.LOCATION_ID_PARAM);
            int abbadoxLocationId = 0;
            string abbadoxLocationName = String.Empty;
            if (locationId != null)
            {
                //Sunil: 2013/07/19 Defect #1765: Locations are not being added properly to the order. (CDI new location orders are disappearing)
                // the scheduler location name is not the same as the one in the abbadox locations (could happen although not suggested)

                // tie to actual location code that exists between these two tables instead of name
                using (IDataReader reader = ExecuteReader(
                                @"SELECT TOP 1
	                                AbbadoxLocation.id,
	                                AbbadoxLocation.name
                                FROM dbo.location  (NOLOCK)  AbbadoxLocation
                                INNER JOIN SchedulerLocation (NOLOCK) 
	                                ON SchedulerLocation.location = AbbadoxLocation.location
                                WHERE SchedulerLocation.LocationId=@locationID", locationId))
                {
                    using (SafeDataReader safeReader = new SafeDataReader(reader))
                    {
                        while (safeReader.Read())
                        {
                            abbadoxLocationId = reader.GetInt32(0);
                            abbadoxLocationName = reader.GetString(1);
                        }
                    }
                }
            }
            String resolvedDictatorID = null;
            OrderCreationValue resolvedDictatorIDVal = values.GetParam(OrderCreationParameter.DICTATOR_ID_PARAM);
            if (resolvedDictatorIDVal != null)
            {
                if (resolvedDictatorIDVal.IsDefaultValue)
                {
                    resolvedDictatorID = resolvedDictatorIDVal.Value as String;
                }
                else
                {
                    resolvedDictatorID =
                        ExecuteScalar("SELECT DictatorID FROM Dictators (NOLOCK) WHERE Id=@dictatorID",
                            resolvedDictatorIDVal.Value) as String;
                }
            }

            String resolvedRefPhysicianName = null;
            String resolvedRefPhysicianId = string.Empty;
            OrderCreationValue resolvedRefPhysicianNameVal = values.GetParam(OrderCreationParameter.PHYS_ID_PARAM);

            if (resolvedRefPhysicianNameVal != null)
            {
                if (resolvedRefPhysicianNameVal.IsDefaultValue)
                {
                    resolvedRefPhysicianName = resolvedRefPhysicianNameVal.Value as String;
                    resolvedRefPhysicianId = resolvedRefPhysicianNameVal.Value as String;
                }
                else
                {
                    resolvedRefPhysicianName =
                        ExecuteScalar(
                            "SELECT LastName+' '+FirstName FROM RefPhysician  (NOLOCK)  WHERE ReferringId=@physicianID",
                            resolvedRefPhysicianNameVal.Value) as String;
                    resolvedRefPhysicianId = resolvedRefPhysicianNameVal.Value as String;
                }
            }

            String sql = string.Format(
                @"INSERT INTO OrderSchedule(
                        OrderID, 
                        [Order Date], 
                        PatientID, 
                        Location, 
                        Facility,
                        Modality, 
                        ReasonForExam, 
                        VisitType, 
                        IsManualOrder, 
                        Dictator, 
                        DictatorID, 
                        Physician,
                        PhysicianId,
                        Priority, 
                        ExamCode, 
                        DateReceive, 
                        ExamDescription,
                        Resource) 
                  VALUES (
                        @orderID, 
                        @DOS, 
                        @patientId,
                        @locationId, 
                        @location, 
                        @workTypeID, 
                        @reason, 
                        @ExamDescription,
                        1, 
                        @dictator,
                        @dictatorID, 
                        @physicianName, 
                        @physicianID, 
                        @priority, 
                        @examCode, 
                        dateadd(minute, {0}, getdate()), 
                        @ExamDescription,
                        @roomName)", GetTimeZonesDiff);
            try
            {
                ExecuteNonQuery(sql,
                    values.GetSQLParam(OrderCreationParameter.ORDER_ID),
                    values.GetSQLParam(OrderCreationParameter.DOS_PARAM),
                    values.GetSQLParam(OrderCreationParameter.PATIENT_ID_PARAM),
                    abbadoxLocationId,
                    abbadoxLocationName,
                    values.GetSQLParam(OrderCreationParameter.WORKTYPE_PARAM),
                    values.GetSQLParam(OrderCreationParameter.REASON_PARAM),
                    values.GetSQLParam(OrderCreationParameter.EXAMDESC_PARAM),
                    values.GetSQLParam(OrderCreationParameter.DICTATOR_NAME_PARAM),
                    resolvedDictatorID,
                    resolvedRefPhysicianName,
                    resolvedRefPhysicianId,
                    values.GetSQLParam(OrderCreationParameter.PRIORITY_PARAM),
                    values.GetSQLParam(OrderCreationParameter.EXAMCODE_PARAM),
                    values.GetSQLParam(OrderCreationParameter.ROOM_NAME) ?? values.GetSQLParam(OrderCreationParameter.MODALITY_PARAM));

            }
            catch (SqlException ex)
            {
                if (ex.Number == UNIQUE_IDX_VIOLATION_ERROR_CODE)
                    throw new SchedulerException(SchedulerExceptionType.UniqueUIDViolation, "Provided identifier already exists");
                else throw;
            }

            sql = "INSERT INTO JobFax (Accession,refID) VALUES (@orderID,@name)";
            if (!String.IsNullOrEmpty(resolvedRefPhysicianId))
            {
                ExecuteNonQuery(sql, values.GetSQLParam(OrderCreationParameter.ORDER_ID), resolvedRefPhysicianId);
            }
            String cc = values.GetSQLParam(OrderCreationParameter.CC_PARAM) as String;
            if (!String.IsNullOrEmpty(cc))
            {
                ExecuteNonQuery(sql, values.GetSQLParam(OrderCreationParameter.ORDER_ID), cc);
            }
            //Send message2E2
            //            SetConnection2Esquared();
            //
            //            int E2SourceApplicationId =
            //                Int32.Parse(WebConfigurationManager.AppSettings[E2SourceApplicationIdFromConfigFile].ToString());
            //
            //            string sqlE2 = " INSERT INTO [Events] " +
            //                           "([AccountName],[JobID],[EventTypeID],[SourceApplicationID],[CreateDateTime],[CurrentStateID], AdditionalInfoInt, AdditionalInfoString) " +
            //                           "VALUES(@account,@jobId,@eventTypeId,@E2SourceApplicationID,getDate(), 1,@additionalInfoInt,@additionalInfoString )";
            //            ExecuteNonQuery(sqlE2,
            //                            values.GetSQLParam(OrderCreationParameter.ACCOUNT_NAME_PARAM),
            //                            values.GetSQLParam(OrderCreationParameter.ORDER_ID),
            //                            (int)Event.EventTypes.PendingOrderRelay,
            //                            E2SourceApplicationId,
            //                            0,
            //                            Container.RequestContext.UserName);
            //            try
            //            {
            //                // fire event for order status update
            //                Event objEvent = new Event(CurrentAccountName,
            //                                           values.GetSQLParam(OrderCreationParameter.ORDER_ID).ToString(),
            //                                           Event.EventTypes.OrderScheduleUpdated, 23, 0, 0, "NW", "");
            //                EventDbLayer.SubmitEventToEsquaredEngine(objEvent);
            //            }
            //            catch (Exception e) { }

            //Create Reference between Abbadox order and appointment reference
            SetConnection2Account();

            sql = @"INSERT INTO SchedulerAppointmentOrders
                    ([AppointmentId]
                   ,[AppointmentItemType]
                   ,[AppointmentItemId]
                   ,[PatientId]
                   ,[LocationName]
                   ,[CPTCode]
                   ,[WorktypeDescription]
                   ,[ExamDescription]
                   ,[PhysicianId]
                   ,[Reason]
                   ,[Dictator]
                   ,[Priority]
                   ,[MultipleOrderId]
                   ,[OrderId]
                   ,[DOS]
                   ,[AccountName]
                   ,[RecurringSeriesID]
                   ,[CC]
                   ,[Modality])
             VALUES
                   (@AppointmentId
                   ,@AppointmentItemType
                   ,@AppointmentItemId
                   ,@PatientId
                   ,@LocationName
                   ,@CPTCode
                   ,@WorktypeDescription
                   ,@ExamDescription
                   ,@PhysicianId
                   ,@Reason
                   ,@Dictator
                   ,@Priority
                   ,@MultipleOrderId
                   ,@OrderId
                   ,@DOS
                   ,@AccountName
                   ,@RecurringSeriesID
                   ,@CC
                   ,@Modality);SELECT SCOPE_IDENTITY() ";

            object dbRes = ExecuteScalar(sql, appId, appItemType, appItemId,
                values.GetSQLParam(OrderCreationParameter.PATIENT_ID_PARAM),
                values.GetSQLParam(OrderCreationParameter.LOCATION_ID_PARAM),
                values.GetSQLParam(OrderCreationParameter.EXAMCODE_PARAM),
                values.GetSQLParam(OrderCreationParameter.WORKTYPE_PARAM),
                values.GetSQLParam(OrderCreationParameter.EXAMDESC_PARAM),
                values.GetSQLParam(OrderCreationParameter.PHYS_ID_PARAM),
                values.GetSQLParam(OrderCreationParameter.REASON_PARAM),
                values.GetSQLParam(OrderCreationParameter.DICTATOR_ID_PARAM),
                values.GetSQLParam(OrderCreationParameter.PRIORITY_PARAM),
                values.GetSQLParam(OrderCreationParameter.MULTY_ORDER_ID_PARAM),
                values.GetSQLParam(OrderCreationParameter.ORDER_ID),
                values.GetSQLParam(OrderCreationParameter.DOS_PARAM),
                values.GetSQLParam(OrderCreationParameter.ACCOUNT_NAME_PARAM),
                values.GetSQLParam(OrderCreationParameter.RECCURING_SERIES_PARAM),
                values.GetSQLParam(OrderCreationParameter.CC_PARAM),
                values.GetSQLParam(OrderCreationParameter.MODALITY_PARAM));

            SendMessageToE2(GlobalContext.RequestContext.Account, orderID, (int)Event.EventTypes.OrderScheduleUpdated, "NW");

            return orderID;
        }

        public AppointmentOrder UpdateOrder(AppointmentOrder order)
        {
            SetConnection2Account();
            BackupSchedulerAppointmentOrders(order.Id);
            String sql = "UPDATE SchedulerAppointmentOrders SET Priority = @Priority WHERE SchedulerOrderId = @id";
            ExecuteNonQuery(sql, order.Priority, order.Id);

            sql = string.Format("UPDATE OrderSchedule SET Priority = @Priority, LastModifiedDate = dateadd(minute, {0}, getdate()) WHERE OrderId = @id", GetTimeZonesDiff);
            ExecuteNonQuery(sql, order.Priority, order.OrderId);

            SendMessageToE2(GlobalContext.RequestContext.Account, order.OrderId, (int)Event.EventTypes.PendingOrderRelay, null);
            return order;
        }

        public void UpdateOrderForProcedure(long orderId, long appointmentId, Procedure procedure)
        {
            SetConnection2Account();
            String sql = string.Format(@"
UPDATE 
	OrderSchedule 
SET [Order date]       = sa.StartTime,
    ExamCode       = @ExamCode,
    ExamDescription = @ExamDescription, 
    VisitType = @visitType, 
    LastModifiedDate = dateadd(minute, {0}, getdate())
from SchedulerAppointments (nolock) sa 
	join SchedulerAppointmentOrders (nolock) sao on sao.AppointmentId = sa.AppointmentID
	join OrderSchedule (nolock) os on os.OrderID = sao.OrderId
where sao.SchedulerOrderId = @SchedulerOrderId and sa.IsDeleted=0
", GetTimeZonesDiff);

            ExecuteNonQuery(sql, procedure.Code, procedure.ShortDescription, procedure.ShortDescription, orderId);
            SendMessageToE2(GlobalContext.RequestContext.Account, orderId.ToString(), (int)Event.EventTypes.PendingOrderRelay, null);

            sql = @"
UPDATE 
	SchedulerAppointmentOrders 
SET CptCode       = @ExamCode,
    ExamDescription = @ExamDescription
from SchedulerAppointments (nolock) sa 
	join SchedulerAppointmentOrders (nolock) sao on sao.AppointmentId = sa.AppointmentID
where sao.SchedulerOrderId = @SchedulerOrderId and sa.IsDeleted=0
";

            ExecuteNonQuery(sql, procedure.Code, procedure.ShortDescription, orderId);
        }

        public void UpdateOrderForRoom(long orderId, long appointmentId, AppointmentResourceModality room)
        {
            SetConnection2Account();
            String sql = string.Format(@"
UPDATE 
	OrderSchedule 
SET [Order date]       = sa.StartTime,
    Modality       = smt.Name,
    Location = sl.LocationId,
    Facility = sl.LocationName, 
    LastModifiedDate = dateadd(minute, {0}, getdate())
from SchedulerAppointments (nolock) sa 
	join SchedulerAppointmentOrders (nolock) sao on sao.AppointmentId = sa.AppointmentID
	join OrderSchedule (nolock) os on os.OrderID = sao.OrderId
    left join SchedulerModalityTypes (nolock) smt on smt.TypeId = @ModalityId
    left join SchedulerLocation (nolock) sl on sl.LocationId = @Location
where sao.SchedulerOrderId = @SchedulerOrderId and sa.IsDeleted=0
", GetTimeZonesDiff);

            ExecuteNonQuery(sql, room.ModalityType.Id, room.Location.Id, orderId);

            sql = @"
UPDATE 
	SchedulerAppointmentOrders  
SET Modality       = smt.Name,
    WorkTypeDescription    = smt.Name,
    LocationName = sl.LocationName
from SchedulerAppointments (nolock) sa 
	join SchedulerAppointmentOrders (nolock) sao on sao.AppointmentId = sa.AppointmentID
    left join SchedulerModalityTypes (nolock) smt on smt.TypeId = @ModalityId
    left join SchedulerLocation (nolock) sl on sl.LocationId = @Location
where sao.SchedulerOrderId = @SchedulerOrderId and sa.IsDeleted=0
";

            ExecuteNonQuery(sql, room.ModalityType.Id, room.Location.Id, orderId);



        }


        public void ChangeOrderMapping(long orderId, long newAppId, long? newAppItemType, String newAppItemId)
        {
            SetConnection2Account();
            BackupSchedulerAppointmentOrders(orderId);
            String sql = @"
UPDATE 
                            SchedulerAppointmentOrders 
SET DOS       = sa.StartTime,
                        AppointmentItemType = @AppointmentItemType,
                        AppointmentItemId   = @AppointmentItemId
from SchedulerAppointments (nolock) sa 
	join SchedulerAppointmentOrders (nolock) sao on sao.AppointmentId = sa.AppointmentID
	join OrderSchedule (nolock) os on os.OrderID = sao.OrderId
where sao.SchedulerOrderId = @SchedulerOrderId and sa.IsDeleted=0
";

            ExecuteNonQuery(sql, newAppItemType, newAppItemId, orderId);
            //Sunil: ensure that orderschedule is in synch
            try
            {
                UpdateOrderScheduleWhenNecessary(orderId);
            }
            catch { }
        }

        public void UpdateOrderScheduleWhenNecessary(long schedulerappointmentorderId)
        {
            //sunil:02/20/2015 - some reschedule cases the process would reach here with the connection to esquared and throw OrderSchedule invalid object error
            SetConnection2Account();
            string sql = string.Format(@"
UPDATE OrderSchedule 
SET 
    [Order Date] = app.StartTime,
    Modality = app.Modality, 
    Location = app.Location, 
    Facility = app.LocationName, 
    LastModifiedDate = dateadd(minute, {0}, getdate()), 
    ReasonForExam = app.VisitReason
FROM 
    OrderSchedule os,
    (SELECT top 1 
        sao.SchedulerOrderId, 
        sao.OrderId, 
        sa.StartTime, 
        smt.Name Modality, 
		sa.LocationId Location,
	    sl.LocationName,
        sav.VisitReason  
    FROM 
        SchedulerAppointments  (NOLOCK)  sa 
        INNER JOIN SchedulerAppointmentOrders  (NOLOCK)  sao ON sao.AppointmentId = sa.AppointmentId
        INNER JOIN SchedulerResources (NOLOCK)  sr ON sr.AppointmentID = sao.AppointmentID
		INNER JOIN SchedulerAppointmentVisit (NOLOCK)  sav ON sa.AppointmentID = sav.AppointmentId
        INNER JOIN SchedulerModalities (NOLOCK)  sm ON sr.ResourceID = sm.ModalityID AND sr.ResourceType = 3
        INNER JOIN SchedulerModalityTypes (NOLOCK)  smt ON sm.Type = smt.TypeId
        inner join SchedulerLocation sl on sl.LocationId = sa.LocationId
    where sao.SchedulerOrderId = @soid) as app
WHERE  os.OrderID = app.OrderId
", GetTimeZonesDiff);
            ExecuteNonQuery(sql, schedulerappointmentorderId);
            BackupSchedulerAppointmentOrders(schedulerappointmentorderId);
            sql = @"
UPDATE SchedulerAppointmentOrders
SET	[DOS] = app.StartTime,
	Modality = app.Modality,
	WorktypeDescription = app.Modality,
    LocationName = app.LocationId,
	Reason = app.VisitReason
FROM SchedulerAppointmentOrders sao1, 
	(SELECT TOP 1
		sao.SchedulerOrderId,
		sao.OrderId,
		sa.StartTime,
		smt.name Modality,
	    sl.LocationId,
		sav.VisitReason
	FROM SchedulerAppointments (NOLOCK) sa
	INNER JOIN SchedulerAppointmentOrders (NOLOCK) sao ON sao.AppointmentId = sa.AppointmentId
	INNER JOIN SchedulerResources (NOLOCK) sr ON sr.AppointmentId = sao.AppointmentId
	INNER JOIN SchedulerAppointmentVisit (NOLOCK) sav ON sa.AppointmentId = sav.AppointmentId
	INNER JOIN SchedulerModalities (NOLOCK) sm ON sr.ResourceID = sm.ModalityID AND sr.ResourceType = 3
	INNER JOIN SchedulerModalityTypes (NOLOCK) smt ON sm.type = smt.TypeId
    inner join SchedulerLocation sl on sl.LocationId = sa.LocationId
	WHERE sao.SchedulerOrderId = @soid AND sa.IsDeleted=0)
	AS app
WHERE sao1.OrderId = app.OrderId
";

            ExecuteNonQuery(sql, schedulerappointmentorderId);

            string orderid = Convert.ToString(ExecuteScalar("SELECT top 1 OrderId FROM SchedulerAppointmentOrders where SchedulerOrderId = @id", schedulerappointmentorderId));

            if (!string.IsNullOrEmpty(orderid))
                SendMessageToE2(GlobalContext.RequestContext.Account, orderid, (int)Event.EventTypes.PendingOrderRelay, null);
        }

        //       public string CreateOrder(OrderCreateParametersDto orderCreateParameters)
        //       {
        //           try
        //           {
        //               int abbadoxLocationId = 0;

        //               SetConnection2Account(orderCreateParameters.Account);


        //               string sql =
        //               @"SELECT TOP 1
        //            AbbadoxLocation.Id
        //           FROM dbo.Location (NOLOCK) AbbadoxLocation
        //           INNER JOIN SchedulerLocation (NOLOCK)
        //            ON SchedulerLocation.LocationName = AbbadoxLocation.Name
        //           WHERE SchedulerLocation.LocationId = @locationID";
        //               //Checking for location in the table Location (for abbadox)
        //               using (IDataReader reader = ExecuteReader(sql, orderCreateParameters.LocationId))
        //               {
        //                   using (SafeDataReader sr = new SafeDataReader(reader))
        //                   {
        //                       while (sr.Read())
        //                       {
        //                           int? locationIdN = sr.GetNullableInt32("Id");
        //                           if (locationIdN == null)
        //                           {
        //                               throw new SchedulerException(SchedulerExceptionType.OrderCreation, "");
        //                           }
        //                           abbadoxLocationId = (int)locationIdN;
        //                       }
        //                   }
        //               }

        //               //Add Order
        //               sql = string.Format(
        //                   "INSERT INTO OrderSchedule (OrderID, [Order Date], PatientID, Location, Facility,Modality, ReasonForExam, VisitType, IsManualOrder, Dictator, DictatorID, Physician,PhysicianId,Priority, ExamCode, DateReceive, RecurringSeriesID) " +
        //                   "VALUES (@orderID, @DOS, @patientId,@locationId, @location, @workTypeID, @reason, @visitType, 1, @dictator," +
        //                   " (SELECT DictatorID FROM Dictators (NOLOCK) WHERE Id=@dictatorID)," +
        //                   "(SELECT LastName+' '+FirstName FROM RefPhysician (NOLOCK) WHERE ID=@physicianID), " +
        //                   "(SELECT ReferringId FROM RefPhysician (NOLOCK) WHERE ID=@physicianID), " +
        //                   " @priority, @examCode, dateadd(minute, {0}, getdate()), @recurringSeriesID)", GetTimeZonesDiff);

        //               ExecuteNonQuery(sql,
        //                               orderCreateParameters.OrderId,
        //                               orderCreateParameters.DateOfService,
        //                               orderCreateParameters.PatientId,
        //                               abbadoxLocationId,
        //                               orderCreateParameters.Location,
        //                               orderCreateParameters.WorkTypeId,
        //                               orderCreateParameters.Reason,
        //                               orderCreateParameters.VisitType,
        //                               orderCreateParameters.Dictator,
        //                               orderCreateParameters.DictatorId,
        //                               //orderCreateParameters.Physician,
        //                               orderCreateParameters.PhysicianId,
        //                               orderCreateParameters.Priority,
        //                               orderCreateParameters.ExamCode,
        //                               DBNull.Value);

        //               //Send message2E2
        //               //SetConnection2Esquared();

        //               int E2SourceApplicationId =
        //                   Int32.Parse(WebConfigurationManager.AppSettings[E2SourceApplicationIdFromConfigFile].ToString());

        //               /* string sqlE2 = string.Format(" INSERT INTO [Events] " +
        //                               "([AccountName],[JobID],[EventTypeID],[SourceApplicationID],[CreateDateTime],[CurrentStateID], AdditionalInfoInt, AdditionalInfoString) " +
        //                               "VALUES(@account,@jobId,@eventTypeId,@E2SourceApplicationID,dateadd(minute, {0}, getdate()), 1,@additionalInfoInt,@additionalInfoString )",GetTimeZonesDiff);
        //                ExecuteNonQuery(sqlE2,
        //                                orderCreateParameters.Account,
        //                                orderCreateParameters.OrderId,
        //                                (int) Event.EventTypes.PendingOrderRelay,
        //                                E2SourceApplicationId,
        //                                0,
        //                                orderCreateParameters.UserId);
        //*/
        //               using (SqlConnection sqlConnection = new SqlConnection(_databaseConnection.ConnectionString))
        //               {
        //                   if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
        //                   sqlConnection.ChangeDatabase("esquared");

        //                   using (SqlCommand sqlCommand = new SqlCommand())
        //                   {
        //                       sqlCommand.Connection = sqlConnection;
        //                       sqlCommand.CommandType = CommandType.Text;
        //                       sqlCommand.CommandText = string.Format(" INSERT INTO [Events] " +
        //                                                              "([AccountName],[JobID],[EventTypeID],[SourceApplicationID],[CreateDateTime],[CurrentStateID], AdditionalInfoInt, AdditionalInfoString) " +
        //                                                              "VALUES(@account,@jobId,@eventTypeId,@E2SourceApplicationID,dateadd(minute, {0}, getdate()), 1,@additionalInfoInt,@additionalInfoString )",
        //                           GetTimeZonesDiff);

        //                       sqlCommand.Parameters.AddWithValue("@account", orderCreateParameters.Account);
        //                       sqlCommand.Parameters.AddWithValue("@jobId", orderCreateParameters.OrderId);
        //                       sqlCommand.Parameters.AddWithValue("@eventTypeId", (int)Event.EventTypes.PendingOrderRelay);
        //                       sqlCommand.Parameters.AddWithValue("@E2SourceApplicationID", E2SourceApplicationId);
        //                       sqlCommand.Parameters.AddWithValue("@additionalInfoInt", 0);
        //                       sqlCommand.Parameters.AddWithValue("@additionalInfoString", orderCreateParameters.UserId);

        //                       sqlCommand.ExecuteNonQuery();
        //                       sqlConnection.Close();
        //                   }
        //               }

        //               //todo:sunil need to change above e2 to use common method

        //               // fire event for order status update
        //               Event objEvent = new Event(CurrentAccountName, orderCreateParameters.OrderId,
        //                   Event.EventTypes.OrderScheduleUpdated, 23, 0, 0, "NW", "");
        //               EventDbLayer.SubmitEventToEsquaredEngine(objEvent);


        //               return orderCreateParameters.OrderId;
        //           }
        //           catch (Exception e)
        //           {
        //               throw new SchedulerException(SchedulerExceptionType.OrderCreation, e.Message);
        //           }
        //       }


        public List<Procedure> GetProcedureSuggestionList(string searchString, int categoryFilter, CPTCodeSearchMode mode, bool exactMatch)
        {
            List<Procedure> result = new List<Procedure>();
            SetConnection2Account(true);

            if (mode == CPTCodeSearchMode.Both || mode == CPTCodeSearchMode.Account)
            {

                List<object> sqlParams = new List<object>();
                string sql = GetAccountProceduresQuery(100);

                if (!string.IsNullOrEmpty(searchString.Trim()))
                {
                    if (exactMatch)
                    {
                        sql += (@"AND (cr.ExternalCode LIKE @search OR cr.Description = @search OR CodeReference = @search)");
                        sqlParams.Add(searchString);
                    }
                    else
                    {
                        sql += (@"AND (cr.ExternalCode LIKE @search OR cr.Description LIKE @search OR CodeReference LIKE @search)");
                        sqlParams.Add("%" + searchString + "%");
                    }
                }
                if (categoryFilter > 0)
                {
                    //Select all child categories 
                    String sql2 = @"WITH C (CodeCategoryId,CodeCategory,ParentCodeCategoryId) AS 
                                    (
	                                    SELECT B.CodeCategoryId,B.CodeCategory,B.ParentCodeCategoryId FROM 
	                                    CodeCategories  (NOLOCK) AS B WHERE B.CodeCategoryId = @catID
	                                    UNION ALL
	                                    SELECT B.CodeCategoryId, B.CodeCategory, B.ParentCodeCategoryId FROM CodeCategories  (NOLOCK) AS B
		                                    INNER JOIN C ON C.CodeCategoryId = B.ParentCodeCategoryId
                                    )
                                    SELECT CodeCategoryId FROM C";
                    StringBuilder catIds = new StringBuilder(" AND crc.CodeCategoryId IN (");
                    using (IDataReader reader = ExecuteReader(sql2, categoryFilter))
                    {
                        int counter = 0;

                        while (reader.Read())
                        {
                            catIds.Append(String.Format("@p{0},", ++counter));
                            sqlParams.Add(reader.GetInt32(0));
                        }
                        catIds.Remove(catIds.Length - 1, 1);
                        catIds.Append(")");
                    }
                    sql += catIds.ToString();
                }


                sql += "   ORDER BY ShortDesc ASC";

                using (IDataReader reader = ExecuteReader(sql, sqlParams.ToArray()))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            Procedure proc = sr.ToProcedure();
                            //Sunil: 2015/11/10 this suggestion list does not get the val from db so its always null
                            // causing the grouped procedures to report a false IsOrderRequired true value
                            //todo: Sunil: this is a workaround need to fix
                            proc.OverrideCreationMode = 0;

                            if (result.Count(a => a.Code == proc.Code) == 0)
                                result.Add(proc);
                        }
                    }
                }
            }

            if (mode == CPTCodeSearchMode.Both || mode == CPTCodeSearchMode.Global)
            {
                //                SetConnection2Global();

                String sql = @"
                        SELECT 
                            TOP 100 * 
                            FROM( 
                                    SELECT 
                                       -1 'ID',
                                       [CPTCode]          'ICD9Code',
                                       [ShortDescription] 'ShortDesc',
                                       [MediumDescription] 'Mediumdesc',
                                       [LongDescription]   'LongDesc',
                                       '' as ProcedureGlobalID ,
                                       1  as IsGlobal,
                                       NULL 'ModalityId',
									   mtc.MammogramType,
                                       0 as TimeOverHead,
                                       NULL as PatientInsuranceId,
                                       NULL as PatientGuarantorId
                                    FROM [Global].dbo.[CPTCodesFull] (NOLOCK) c
                                        left join MammogramTypesToCpt (NOLOCK) mtc on mtc.Cpt = c.CPTCode
                                    WHERE 
                                        CPTCode LIKE @search 
                                        OR ShortDescription LIKE @search 
                                  ) AS FullList 
                        ORDER BY ShortDesc ASC";

                using (IDataReader reader = ExecuteReader(sql, "%" + searchString + "%"))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            Procedure proc = sr.ToProcedure();
                            //Sunil: 2015/11/10 this suggestion list does not get the val from db so its always null
                            // causing the grouped procedures to report a false IsOrderRequired true value
                            //todo: Sunil: this is a workaround need to fix
                            proc.OverrideCreationMode = 0;
                            if (result.Count(a => a.Code == proc.Code) == 0)
                                result.Add(proc);
                        }
                    }
                }
            }

            return result;
        }

        private string GetAccountProceduresQuery(int recordLimit)
        {
            return string.Format(@"SELECT {0}
                            cr.CodeReferenceId as 'ID',
                           '' as ProcedureGlobalID,
                            convert(varchar(250),cr.ExternalCode) as ICD9Code,
                            convert(varchar(250),cr.[Description])    as Mediumdesc,
                            convert(varchar(250),cr.[Description])   as LongDesc,
                            convert(varchar(250),cr.[Description])   as ShortDesc,
                            ''                                        as DiagnosGlobalID,
                            0                  as IsGlobal,
                            NULL 'ModalityId',
                            cr.DefaultOverhead as TimeOverHead,
                            crc.CodeCategoryId as CodeCategoryId,cr.AlertText 'AlertText',
                            cr.DefaultOverhead, cr.DefaultAmount, cr.DefaultVolume, cr.DefaultHCPCS,
		                    mtc.MammogramType,
                            srdobp.ID ResourceDurationOverrideId,
		                    srdobp.ActualDuration,
		                    srdobp.SedationTime,
		                    srdobp.AddLeadTime,
                            NULL as PatientInsuranceId,
                            NULL as PatientGuarantorId
                    FROM CodeReferences (NOLOCK) cr 
                        inner join [Global].dbo.CodeTypes (NOLOCK) ct on ct.CodeTypeID = cr.CodeTypeId
                        left join CodeReferenceCategories (NOLOCK) crc on crc.CodeReferenceId = cr.CodeReferenceId 
                        left join MammogramTypesToCpt (NOLOCK) mtc on mtc.Cpt = cr.CodeReference
                        LEFT JOIN SchedulerResourceDurationOverrideByProcedures (NOLOCK) srdobp ON cr.CodeReferenceId = srdobp.CodeReferenceId
                        LEFT JOIN SchedulerModalities (NOLOCK) sm ON sm.ModalityID = srdobp.SchedulerModalityId
                    where 
                        cr.isActive = 1 AND 
                        (ct.CodeTypeName= 'ICD9Proc' OR ct.CodeTypeName='CPT')", recordLimit == 0 ? string.Empty : "TOP " + recordLimit);

        }

        private string GetAccountProceduresQueryWithRooms(int recordLimit, long? roomId)
        {
            return string.Format(@"SELECT {0}
                            cr.CodeReferenceId as 'ID',
                           '' as ProcedureGlobalID,
                            convert(varchar(250),cr.ExternalCode) as ICD9Code,
                            convert(varchar(250),cr.[Description])    as Mediumdesc,
                            convert(varchar(250),cr.[Description])   as LongDesc,
                            convert(varchar(250),cr.[Description])   as ShortDesc,
                            ''                                        as DiagnosGlobalID,
                            0                  as IsGlobal,
                            NULL 'ModalityId',
                            0 as TimeOverHead,
                            crc.CodeCategoryId as CodeCategoryId,cr.AlertText 'AlertText',
                            cr.DefaultOverhead, cr.DefaultAmount, cr.DefaultVolume, cr.DefaultHCPCS,
		                    mtc.MammogramType
                    FROM CodeReferences (NOLOCK) cr 
                        inner join [Global].dbo.CodeTypes (NOLOCK) ct on ct.CodeTypeID = cr.CodeTypeId
                        left join CodeReferenceCategories (NOLOCK) crc on crc.CodeReferenceId = cr.CodeReferenceId 
                        left join MammogramTypesToCpt (NOLOCK) mtc on mtc.Cpt = cr.CodeReference
                        {1}
                    where 
                        cr.isActive = 1 AND 
                        (ct.CodeTypeName= 'ICD9Proc' OR ct.CodeTypeName='CPT')
                        ",
                recordLimit == 0 ? string.Empty : "TOP " + recordLimit,
                roomId.HasValue
                    ? "JOIN SchedulerCodeReferenceToModality scctm ON scctm.CodeReferenceId = cr.CodeReferenceId AND scctm.ModalityId=@modid "
                    : "");

        }

        private string GetAccountDiagnosesQuery(int recordLimit)
        {
            return string.Format(@"SELECT {0}
                            cr.CodeReferenceId as 'ID',
                            convert(varchar(250),cr.ExternalCode) as CPTCode,
                            convert(varchar(250),cr.[Description])    as Mediumdesc,
                            convert(varchar(250),cr.[Description])   as LongDesc,
                            convert(varchar(250),cr.[Description])   as ShortDesc,
                            ''                                        as DiagnosGlobalID,
                            0                  as IsGlobal,
                            crc.CodeCategoryId   as CodeCategoryId,cr.AlertText 'AlertText'
                    FROM CodeReferences (NOLOCK) cr 
                    inner join [Global].dbo.CodeTypes (NOLOCK) ct on ct.CodeTypeID = cr.CodeTypeId
                    left join CodeReferenceCategories (NOLOCK) crc on crc.CodeReferenceId = cr.CodeReferenceId 
                    where 
                        cr.isActive = 1 AND 
                        ct.CodeTypeName= 'ICD9 Diag' ", recordLimit == 0 ? string.Empty : "TOP " + recordLimit);
        }

        public List<Procedure> GetLocalProcedureAdminList()
        {
            List<Procedure> result = new List<Procedure>();

            SetConnection2Account();

            List<object> sqlParams = new List<object>();
            string sql = GetAccountProceduresQuery(0);//no limit

            using (IDataReader reader = ExecuteReader(sql + " ORDER BY cr.ExternalCode DESC", sqlParams.ToArray()))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        Procedure proc = sr.ToProcedure();
                        if (result.Count(a => a.Code == proc.Code) == 0)
                            result.Add(proc);
                    }
                }
            }

            return result;
        }

        public List<Diagnosis> GetLocalDiagnosesAdminList()
        {
            List<Diagnosis> result = new List<Diagnosis>();

            SetConnection2Account();

            List<object> sqlParams = new List<object>();
            string sql = GetAccountDiagnosesQuery(0);//no limit

            using (IDataReader reader = ExecuteReader(sql + " ORDER BY cr.ExternalCode DESC", sqlParams.ToArray()))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        Diagnosis proc = sr.ToDiagnosis();
                        if (result.Count(a => a.Code == proc.Code) == 0)
                            result.Add(proc);
                    }
                }
            }

            return result;
        }

        public List<CPTModifier> GetModifierSuggestionList(string searchString)
        {
            return GetModifierList(searchString, false);
        }

        public CPTModifier GetModifierByCode(string searchString)
        {
            List<CPTModifier> lm = GetModifierList(searchString, true);
            if (lm != null && lm.Count > 0) return lm[0];
            return null;
        }

        private List<CPTModifier> GetModifierList(string searchString, bool isEqual)
        {
            List<CPTModifier> result = new List<CPTModifier>();
            SetConnection2Account();

            String sql = @"SELECT TOP 100 ID
	                     , ExternalCode
	                     , Code
	                     , Description
	                     , IsGlobal
	                     , CASE 
		                       WHEN Code LIKE @search THEN 2
		                       WHEN DESCRIPTION LIKE @search THEN 1
		                    ELSE 0
                           END Ranking
                    FROM
	                    (SELECT ID
		                      , '' ExternalCode
		                      , Code
		                      , Description
		                      , cast(1 AS BIT) IsGlobal
	                     FROM
		                     Global.dbo.CPTModifiers (NOLOCK) c
	                     WHERE
		                     isActive = 1
		                     AND Code NOT IN (SELECT CodeReference Code
						                      FROM
							                      CodeReferences (NOLOCK) cr
						                      WHERE
							                      isActive = 1
							                      AND CodeTypeId = (SELECT TOP 1 ct.CodeTypeID
												                    FROM
													                    Global.dbo.CodeTypes ct
												                    WHERE
													                    ct.CodeTypeName = 'CPTMods'))
	                     UNION
	                     SELECT CodeReferenceId ID
		                      , ExternalCode
		                      , CodeReference Code
		                      , Description
		                      , cast(0 AS BIT) IsGlobal
	                     FROM
		                     CodeReferences (NOLOCK) cr
	                     WHERE
		                     isActive = 1
		                     AND CodeTypeId = (SELECT TOP 1 ct.CodeTypeID
						                       FROM
							                       Global.dbo.CodeTypes (NOLOCK) ct
						                       WHERE
							                       ct.CodeTypeName = 'CPTMods')) Modifiers
                    WHERE"
                + (isEqual ? " Code = @search" : " Code LIKE @search OR DESCRIPTION LIKE @search")
                + " ORDER BY Ranking DESC, Code ASC";
            if (!isEqual)
                searchString = "%" + searchString.Replace(' ', '%') + "%";
            using (IDataReader reader = ExecuteReader(sql, searchString))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Add(sr.ToCPTModifier());
                    }
                }
            }
            return result;
        }

        public List<Diagnosis> GetDiagnosisSuggestionListCommand(string searchString, int categoryFilter, CPTCodeSearchMode mode)
        {
            List<Diagnosis> result = new List<Diagnosis>();

            if (mode == CPTCodeSearchMode.Both || mode == CPTCodeSearchMode.Global)
            {
                SetConnection2Global(true);

                String sql = @"SELECT TOP 100
	                *
                FROM (SELECT
	                -1 'ID',
	                [ICD9Code] 'CPTCode',
	                [ShortDesc],
	                [Mediumdesc],
	                [LongDesc],
	                '' AS DiagnosGlobalID,
	                1 'IsGlobal',
	                (CASE
		                WHEN hccc.ID IS NULL THEN CAST(0 AS bit)
		                ELSE CAST(1 AS bit)
	                END) AS IsChronic
                FROM [ICD9ProcCodes](NOLOCK)
                left JOIN HierarchicalConditionCategoryCodes hccc
	                ON hccc.DiagnosisCode = ICD9Code
                WHERE ICD9Code LIKE @search
                OR ShortDesc LIKE @search
                UNION
                SELECT
	                -1 'ID',
	                [ICD9Code] 'CPTCode',
	                [ShortDesc],
	                [Mediumdesc],
	                [LongDesc],
	                '' AS DiagnosGlobalID,
	                1 'IsGlobal',
	                (CASE
		                WHEN hccc.ID IS NULL THEN CAST(0 AS bit)
		                ELSE CAST(1 AS bit)
	                END) AS IsChronic
                FROM [ICD9DiagCodes](NOLOCK)
                left JOIN HierarchicalConditionCategoryCodes hccc
	                ON hccc.DiagnosisCode = ICD9Code
                WHERE ICD9Code LIKE @search
                OR ShortDesc LIKE @search) AS FullList
                ORDER BY ShortDesc ASC";

                using (IDataReader reader = ExecuteReader(sql, "%" + searchString + "%"))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            result.Add(sr.ToDiagnosis());
                        }
                    }
                }
            }

            if (mode == CPTCodeSearchMode.Account || mode == CPTCodeSearchMode.Both)
            {
                SetConnection2Account(true);

                StringBuilder sql = new StringBuilder(GetAccountDiagnosesQuery(100));

                List<object> sqlParams = new List<object>();
                if (!string.IsNullOrEmpty(searchString.Trim()))
                {
                    sql.Append(" AND (cr.ExternalCode LIKE @search OR cr.Description LIKE @search OR CodeReference LIKE @search)");
                    sqlParams.Add("%" + searchString + "%");
                }

                if (categoryFilter > 0)
                {
                    //Select all child categories 
                    String sql2 = @"WITH C (CodeCategoryId,CodeCategory,ParentCodeCategoryId) AS 
                                    (
	                                    SELECT B.CodeCategoryId,B.CodeCategory,B.ParentCodeCategoryId FROM 
	                                    CodeCategories (NOLOCK) AS B WHERE B.CodeCategoryId = @catID
	                                    UNION ALL
	                                    SELECT B.CodeCategoryId, B.CodeCategory, B.ParentCodeCategoryId FROM CodeCategories (NOLOCK) AS B
		                                    INNER JOIN C ON C.CodeCategoryId = B.ParentCodeCategoryId
                                    )
                                    SELECT CodeCategoryId FROM C";
                    StringBuilder catIds = new StringBuilder();
                    using (IDataReader reader = ExecuteReader(sql2, categoryFilter))
                    {
                        catIds.Append(" AND crc.CodeCategoryId IN (");
                        int counter = 0;

                        while (reader.Read())
                        {
                            catIds.Append(String.Format("@p{0},", ++counter));
                            sqlParams.Add(reader.GetInt32(0));
                        }
                        catIds.Remove(catIds.Length - 1, 1);
                        catIds.Append(")");
                    }
                    sql.Append(catIds);
                }
                sql.Append(" ORDER BY ShortDesc ASC");
                using (IDataReader reader = ExecuteReader(sql.ToString(), sqlParams.ToArray()))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            result.Add(sr.ToDiagnosis());
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region ICanGetById<Account,long> Members

        private void ReadOrdersConfiguration(Account res)
        {
            SetConnection2Global();

            String query = @"
                            SELECT 
	                            p.*
                            FROM dbo.vSchedulerAccountConfigOrderParams (NOLOCK) p
                            WHERE p.AccountId = @accountName";

            using (IDataReader reader = ExecuteReader(query, res.Name))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        res.OrderCreationParameters.Add(sr.ToOrderCreationParameter());
                    }
                }
            }
        }


        public Account GetById(long id)
        {
            SetConnection2Global();

            string query = @"SELECT a.ID as 'AccountID',
                                    a.Account as 'AccountName', 
                                    acc.State as 'AccountState',
                                    acc.ViewPatientsInValidLocationsOnly,
                                    a.AccessValidLocationsOnly,
                                    config.*,
                                    s.Logo AS 'AccountLogo',
                                    acc.Address as 'AccountAddress',
                                    acc.Address2  as 'AccountAddress2',
                                    acc.City  as 'AccountCity', 
                                    acc.Zipcode  as 'AccountZipcode',
                                    acc.Phone  as 'AccountPhone',
                                    a.IsWorkWithPatientVisitAllowed,
                                    a.SchedulerUserLvl
                            FROM ValidAccounts (NOLOCK) a 
                            LEFT JOIN  Accounts (NOLOCK) acc ON a.Account = acc.AccountId
                            LEFT JOIN  SchedulerConfigurations (NOLOCK) config on config.AccountId = a.Account
							LEFT JOIN Skins (NOLOCK) s ON s.SkinsName = acc.AccountId
                where a.ID = @accountID";

            Account res = null;
            using (IDataReader reader = ExecuteReader(query, id))
            {
                while (reader.Read())
                {
                    res = reader.ToAccount();
                    break;
                }
            }

            if (res != null)
            {
                res.IsCrmEnabled = AccountHasCRMdb(res.Name);
                res.InitDefaultData();

                SetConnection2Account(res.Name);

                res.LoadOrderCreationTriggers(GetOrderCreationTriggers());
                res.LoadVisitCreationTriggers(GetVisitCreationTriggers());

                object proceduresAreAvailable = ExecuteScalar(@"SELECT COUNT(1)
                                                FROM CodeReferences (NOLOCK) cr 
                                                inner join [Global].dbo.CodeTypes (NOLOCK) ct on ct.CodeTypeID = cr.CodeTypeId
                                                where 
                                                cr.isActive = 1 AND 
                                                (ct.CodeTypeName= 'ICD9Proc' OR ct.CodeTypeName='CPT')");

                object diagnosesAreAvailable = ExecuteScalar(@"SELECT COUNT(1)
                                                FROM CodeReferences (NOLOCK) cr 
                                                inner join [Global].dbo.CodeTypes (NOLOCK) ct on ct.CodeTypeID = cr.CodeTypeId
                                                where 
                                                cr.isActive = 1 AND 
                                                ct.CodeTypeName= 'ICD9 Diag'");

                res.SetAccountSourcesAvailability(proceduresAreAvailable, diagnosesAreAvailable);

                query = @"SELECT CodeCategoryId,CodeCategory,ParentCodeCategoryId,isActive FROM CodeCategories (NOLOCK) where IsActive=1 order by CodeCategory asc";
                try
                {
                    using (IDataReader reader = ExecuteReader(query))
                    {
                        while (reader.Read())
                        {
                            res.CodeCategories.Add(reader.ToCodeCategory());
                        }
                    }
                }
                catch
                {
                    //CP: Fix
                    //GlobalContext.RequestContext.Notifier.AddWarnings(SchedulerWarningType.CodeCategoriesAreNotAvailable, "Code categories are not presented in the database");
                }

                query = @"SELECT Id, DisplayName, IsVisible, IsSystem, CannedCommentEnumType FROM SchedulerCommentTypes (NOLOCK) order by DisplayName asc";

                using (IDataReader reader = ExecuteReader(query))
                {
                    res.CommentTypes = new List<CommentType>();
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            res.CommentTypes.Add(sr.ToCommentType());
                        }
                    }
                }

                query = @"SELECT Id, DisplayName, IsVisible FROM SchedulerTCSuggestionList (NOLOCK) order by DisplayName asc";

                using (IDataReader reader = ExecuteReader(query))
                {
                    res.TechCompleteSuggestionList = new List<TechCompleteSuggestionList>();
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                            res.TechCompleteSuggestionList.Add(new TechCompleteSuggestionList(sr.GetInt64("Id"),
                                                                                          sr.GetString("DisplayName"),
                                                                                          sr.GetBoolean("IsVisible")));
                    }
                }

                res.VisitCategories = LoadAccountEnumsByType(AccountEnumTypes.VisitCategory);
                res.HCPCScodes = LoadAccountEnumsByType(AccountEnumTypes.HCPCS);
                res.EnumsScheduledBy = LoadAccountEnumsByType(AccountEnumTypes.ScheduledBy);
                res.EnumsHeardOfUs = LoadAccountEnumsByType(AccountEnumTypes.HeardOfUs);
                res.EnumPriority = LoadAccountEnumsByType(AccountEnumTypes.OrderPriority);
                res.EnumPendingReason = LoadAccountEnumsByType(AccountEnumTypes.PendingReason);
                res.EnumAuthorizationUserStatuses = LoadAccountEnumsByType(AccountEnumTypes.AuthorizationUserStatuses);
                res.EnumPatientAilment = LoadAccountEnumsByType(AccountEnumTypes.PatientAilment);
                res.GuarantorRelationShip = LoadAccountEnumsByType(AccountEnumTypes.GuarantorRelationship);
                res.EnumMaritalStatus = LoadAccountEnumsByType(AccountEnumTypes.MaritalStatus);
                res.EnumContactRelation = LoadAccountEnumsByType(AccountEnumTypes.ContactRelation);
                res.EnumContactType = LoadAccountEnumsByType(AccountEnumTypes.ContactType);
                res.EnumEmploymentStatus = LoadAccountEnumsByType(AccountEnumTypes.EmploymentStatus);
                res.EnumDiagnosisFlags = LoadAccountEnumsByType(AccountEnumTypes.DiagnosisFlags);
                res.EnumReferralGroups = LoadAccountEnumsByType(AccountEnumTypes.ReferralGroups);
                res.EnumReferralSpecialities = LoadAccountEnumsByType(AccountEnumTypes.ReferralSpecialities);
                res.EnumCreditCardTypes = LoadAccountEnumsByType(AccountEnumTypes.CreditCardTypes);
                res.EnumPaymentStatuses = LoadAccountEnumsByType(AccountEnumTypes.PaymentStatuses);
                res.EnumFiltersConfiguration = LoadAccountEnumsByType(AccountEnumTypes.EnumFiltersConfiguration);
                res.EnumMultipleIdentifierSource = LoadAccountEnumsByType(AccountEnumTypes.EnumMultipleIdentifierSource);
                res.EnumPatientCommentTransferredTo = LoadAccountEnumsByType(AccountEnumTypes.EnumPatientCommentTransferredTo);

                res.AllGenders = LoadAccountEnumsByType(AccountEnumTypes.AllGenders);
                res.AllRaces = LoadAllRaces();
                res.AllEthnicity = LoadEthnicities();
                //                res.AllSmoking = LoadAccountEnumsByType(AccountEnumTypes.AllSmoking);
                res.AllSmoking = LoadAllSmoking();
                res.AllPatientStatuses = LoadAccountEnumsByType(AccountEnumTypes.AllPatientStatuses);
                res.AllSpecialNeeds = LoadAccountEnumsByType(AccountEnumTypes.AllSpecialNeeds);
                res.AllRelationships = LoadAccountEnumsByType(AccountEnumTypes.AllRelationships);
                //                res.AllEhrSystems = LoadAccountEnumsByType(AccountEnumTypes.CRMEhrSystems);

                res.WorkingSchedule = ReadAccountWorkingSchedule();
                /*
                                query = @"SELECT
                                                sas.Id,
                                                DisplayName,
                                                AppliedDisplayName,
                                                SortIndex,
                                                IsVisible,
                                                Color,
                                                IsSystemStatus,
                                                sst.AvailableAppointmentStatusId
                                            FROM SchedulerAppointmentStatuses sas (NOLOCK)
                                            LEFT JOIN SchedulerStatusesTransition sst ON sas.Id = sst.BaseAppointmentStatus
                                            ORDER BY SortIndex ASC";

                                using (IDataReader reader = ExecuteReader(query))
                                {
                                    res.LoadAppointmentStatuses(reader);
                                }*/
                res.AppointmentStatuses.AddRange(GetAccountAppointmentStatuses(false));

                query = @"SELECT *
                              FROM [SchedulerAccountConfig] (NOLOCK)";

                using (IDataReader reader = ExecuteReader(query))
                {
                    //res.ExtractConfigFromReader(reader);
                    //res.WorkingDays = new List<DayOfWeek>();
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        if (sr.Read())
                        {
                            res.WorkingDays = new List<DayOfWeek>();

                            if (sr.GetBoolean("MonIsWorking"))
                                res.WorkingDays.Add(DayOfWeek.Monday);
                            if (sr.GetBoolean("TueIsWorking"))
                                res.WorkingDays.Add(DayOfWeek.Tuesday);
                            if (sr.GetBoolean("WenIsWorkgin"))
                                res.WorkingDays.Add(DayOfWeek.Wednesday);
                            if (sr.GetBoolean("ThrIsWorking"))
                                res.WorkingDays.Add(DayOfWeek.Thursday);
                            if (sr.GetBoolean("SatIsWorking"))
                                res.WorkingDays.Add(DayOfWeek.Saturday);
                            if (sr.GetBoolean("SunIsWorking"))
                                res.WorkingDays.Add(DayOfWeek.Sunday);
                            if (sr.GetBoolean("FriIsWorking"))
                                res.WorkingDays.Add(DayOfWeek.Friday);

                            res.StartWeekOn = sr.GetInt32("StartWeekOn");
                            res.StartWorkingHour = sr.GetInt32("StartWorkingHour");
                            res.StartWorkingMinute = sr.GetInt32("StartWorkingMinute");
                            res.EndWorkingHour = sr.GetInt32("EndWorkingHour");
                            res.EndWorkingMinute = sr.GetInt32("EndWorkingMinute");
                            res.NumberOfVisibleHours = sr.GetInt32("DefaultViewSize");

                            res.DefaultViewMode = sr.GetInt32("DefaultViewMode");
                            res.ScheduleMode = sr.GetInt32("DefaultScheduleMode");
                            res.IsReferralRequired = (sr.GetNullableBoolean("IsReferralRequired") ?? true);
                            res.IsBillingNoteRequired = (sr.GetNullableBoolean("IsBillingNoteRequired") ?? true);
                            res.IsCreateOrderRequired = (sr.GetNullableBoolean("IsCreateOrderRequired") ?? true);
                            res.IsVisitReasonRequired = (sr.GetNullableBoolean("IsVisitReasonRequired") ?? true);
                            res.ProcedureExpansionMode = (ProcedureExpansionMode)(sr.GetNullableInt32("ProcedureAutoExpansionMode") ?? 0);
                            res.PayersSearchMode = (PayersSearchMode)(sr.GetNullableInt32("PayersSearchMode") ?? 0);
                            res.PreselectProcedureTypes = sr.GetNullableBoolean("PreselectProcedureTypes") ?? false;
                            res.IsPendingEnabled = sr.GetBoolean("IsPendingEnabled");
                            res.PatientCategoryRequired = sr.GetBoolean("PatientCategoryRequired");
                            res.IsProcedureGlobalSearchEnabled = sr.GetBoolean("IsProcedureGlobalSearchEnabled");
                            res.IsWarningMessagesEnabled = sr.GetBoolean("IsWarningMessagesEnabled");
                            res.IsCommentForBlockingRequired = sr.GetBoolean("IsCommentForBlockingRequired");
                            res.IsPatientDOBMandatory = sr.GetBoolean("PatientDOBMandatory");
                            res.SendEmailFromAddress = sr.GetNullableString("EmailAddress");
                            res.IsPaymentsEnabled = sr.GetBoolean("IsPaymentsEnabled");
                            res.IsProcessPaymentsEnabled = sr.GetBoolean("IsProcessPaymentsEnabled");
                            res.IsScheduleAppointmentByEstimationSlots = sr.GetBoolean("IsScheduleAppointmentByEstimationSlots");
                            res.IsStateOfServiceEnabled = sr.GetBoolean("IsStateOfServiceEnabled");
                            res.IsProcedureRequired = sr.GetBoolean("IsProcedureRequired");
                            res.MRNReadOnly = sr.GetBoolean("MRNReadOnly");
                            res.IsMammographyActive = sr.GetBoolean("IsMammographyActive");
                        }
                    }
                }

                if (res.IsMammographyActive)
                {
                    res.EnumMammoLaterality = LoadAccountEnumsByType(AccountEnumTypes.MammoLaterality);
                    res.EnumMammoMammogramType = LoadAccountEnumsByType(AccountEnumTypes.MammoMammogramType);
                    res.EnumMammoMammogramSubType = LoadAccountEnumsByType(AccountEnumTypes.MammoMammogramSubType);
                    res.EnumMammoNodalStatus = LoadAccountEnumsByType(AccountEnumTypes.MammoNodalStatus);
                    res.EnumMammoTumorSize = LoadAccountEnumsByType(AccountEnumTypes.MammoTumorSize);
                    res.EnumMammoBiopsyType = LoadAccountEnumsByType(AccountEnumTypes.MammoBiopsyType);
                    res.EnumMammoBirads = LoadAccountEnumsByType(AccountEnumTypes.MammoBirads);
                    res.EnumMammoBreastDensity = LoadAccountEnumsByType(AccountEnumTypes.MammoBreastDensity);
                    res.EnumTestResultStatus = LoadAccountEnumsByType(AccountEnumTypes.TestResultStatus);
                }

                query =

                     @"SELECT 	smt.TypeId, smt.Name, l.Id, l.Location,smt.AllowComparision FROM SchedulerModalityTypes smt
                        INNER JOIN WorkType wt ON smt.Name = wt.Wt
                        INNER JOIN Location l ON wt.Location = l.Location
                        WHERE l.IsActive = 1 AND wt.IsActive = 1
                        ORDER BY smt.Name";


                //nik mod: in previous statement we won't see untied modality types
                /*                 query =
                                    @"
                                    select  smt.TypeId,smt.Name, smsr.ModalityID
                                        from SchedulerModalityTypes smt
                                        JOIN SchedulerModalities smsr ON smsr.Type = smt.TypeId";*/
                using (IDataReader reader = ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        res.ModalityTypes.Add(reader.ToModalityType());
                    }
                }

                GetLocationsForAccount(res);

                foreach (ResourceLocation location in res.ResourceLocations)
                {
                    if (res.WorkTypes.ContainsKey(location.LocationName))
                    {
                        continue;
                    }

                    Dictionary<String, String> workTypes = new Dictionary<string, string>();

                    query = "SELECT WorkType.Wt, WorkType.Description FROM WorkType (NOLOCK) " +
                            " INNER JOIN Location (NOLOCK) AbbadoxLocation ON AbbadoxLocation.Location = WorkType.Location " +
                            " INNER JOIN SchedulerLocation (NOLOCK) ON SchedulerLocation.LocationName = AbbadoxLocation.[Name] " +
                            " WHERE SchedulerLocation.LocationId = @locationID AND WorkType.Type IN (1, 3)";
                    using (IDataReader reader = ExecuteReader(query, location.Id))
                    {
                        using (SafeDataReader sr = new SafeDataReader(reader))
                        {
                            while (sr.Read())
                            {
                                string wt = sr.GetNullableString("Wt");
                                string description = sr.GetNullableString("Description");

                                if (String.IsNullOrEmpty(wt) || String.IsNullOrEmpty(description))
                                {
                                    continue;
                                }

                                if (!workTypes.ContainsKey(wt))
                                {
                                    workTypes.Add(wt, description);
                                }
                            }
                        }
                    }
                    res.WorkTypes.Add(location.LocationName, workTypes);
                }


                query = @"SELECT DISTINCT Signature,(CAST(ID as varchar(50)) + '`' + ISNULL(NPI,'')+ '`' + ISNULL(ProviderNumber,'')) as DICNPI FROM dbo.Dictators (NOLOCK) d";
                using (IDataReader reader = ExecuteReader(query))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            try
                            {
                                string key = sr.GetNullableString("DICNPI");
                                if (String.IsNullOrEmpty(key))
                                    continue;

                                if (!res.AvailableProviders.ContainsKey(key))
                                    res.AvailableProviders.Add(key, sr.GetString("Signature"));
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                query = @"SELECT TypeId,TypeName,TypeColor FROM SchedulerPhysicianTypes (NOLOCK)";
                using (IDataReader reader = ExecuteReader(query))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            res.PhysicianTypes.Add(sr.ToPhysicianType());
                        }
                    }
                }

                query = @"SELECT acli.[Id] 'ItemId' ,acli.[Name] 'ItemName' ,ett.[Id] 'TemplateId', ett.TemplateName FROM [dbo].[AppointmentCheckListItems] acli LEFT JOIN EhrTaskTemplates ett ON acli.TaskTemplateId = ett.Id WHERE acli.IsDeleted = 0 ORDER BY acli.[Name]";
                using (IDataReader reader = ExecuteReader(query))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            res.AppointmentCheckListItems.Add(sr.ToAppointmentCheckListItem());
                        }
                    }
                }

                try
                {
                    SetConnection2Global();
                    query = @"select LanguageId,LanguageName FROM Languages";
                    res.AvailableLanguages.Add(" ", " ");
                    using (IDataReader reader = ExecuteReader(query))
                    {
                        using (SafeDataReader sr = new SafeDataReader(reader))
                        {
                            while (sr.Read())
                            {
                                string key = sr.GetInt32("LanguageId").ToString();
                                if (!res.AvailableLanguages.ContainsKey(key))
                                    res.AvailableLanguages.Add(key, sr.GetString("LanguageName"));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //CP: Fix
                    //Container.RequestContext.Notifier.AddWarnings(
                    //    SchedulerWarningType.LanguagesReadingError, String.Format("Language reading for account #{0} failed. {1}", res.Name, e.Message));
                }

                try
                {
                    query = @"SELECT stateCode, StateName FROM Global.dbo.States (NOLOCK) s order by StateName, stateCode";
                    //res.UsaStates.Add(String.Empty, String.Empty);
                    using (IDataReader reader = ExecuteReader(query))
                    {
                        using (SafeDataReader sr = new SafeDataReader(reader))
                        {
                            while (sr.Read())
                            {
                                string key = sr.GetString("stateCode");
                                if (!res.UsaStates.ContainsKey(key))
                                {
                                    string name = sr.GetNullableString("StateName");
                                    if (string.IsNullOrEmpty(name)) name = key;
                                    res.UsaStates.Add(key, name);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //CP: Fix
                    //Container.RequestContext.Notifier.AddWarnings(
                    //    SchedulerWarningType.LanguagesReadingError, String.Format("USA states reading for account #{0} failed. {1}", res.Name, e.Message));
                }

                ReadOrdersConfiguration(res);

                //                SetConnection2Global();

                //                query = @"
                //                            SELECT op.OrderParameterId,
                //	                               op.ParameterName,
                //	                               op.ParameterType,
                //	                               op.IsSystemRequired,
                //                                   sop.isRequired,
                //	                               sop.ParameterDefaultValue FROM SchedulerConfigurations sc
                //                            RIGHT JOIN SchedulerOrderParameters sop 
                //                            ON sc.SchedulerConfigurationId = sop.SchedulerConfigurationId 
                //                            RIGHT JOIN OrderParameters op 
                //                            ON op.OrderParameterId = sop.OrderParameterId 
                //                            WHERE sc.AccountId IS NULL OR sc.AccountId = @accName";

                //                using (IDataReader reader = ExecuteReader(query, res.Name))
                //                {
                //                    using (SafeDataReader sr = new SafeDataReader(reader))
                //                    {
                //                        while (sr.Read())
                //                        {
                //                            res.OrderCreationParameters.Add(OrderCreationParameter.ExtractFromReader(sr));
                //                        }
                //                    }
                //                }

                SetConnection2Insurance();
                query = @"SELECT 
    distinct pa.State
FROM Payers (NOLOCK) p join PayerAddresses (NOLOCK) pa on p.PayerID = pa.PayerID
where pa.State is not null and pa.State <> 'all'
ORDER BY pa.State";

                res.PayerStates.Add("ALL", " ");
                using (IDataReader reader = ExecuteReader(query))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            string state = sr.GetNullableString("State");
                            string name = state;

                            if (state == null || name == null)
                                continue;

                            if (!res.PayerStates.ContainsKey(state))
                                res.PayerStates.Add(state, name);
                        }
                    }
                }


                //                SetConnection2Insurance();
                //                query = @"SELECT 
                //                            (cast(p.PayerID AS varchar) + cast(isnull(p.VendorPayerID,'') AS varchar) + cast(isnull(p.IsEligible,'') AS varchar) + '`' + isnull(pa.Address1,'')+ '`' + isnull(pa.Address2,'') + '`' + isnull(pa.City,'') + '`' + isnull(pa.[State],'') + '`' + isnull(pa.Country,'')  + '`' + isnull(pa.ZipCode,'')  + '`' + isnull(p.WebSite,'')   + '`' + isnull(pa.Phone,'')  + '`' + isnull(pa.Fax,'') + '`' + (p.Name +' ~ '+ pa.[State]) ) as PayerDetails,
                //                            (p.Name +' ~ '+ pa.[State]) as name   FROM PayerAddresses pa   JOIN Payers p ON p.PayerID = pa.PayerID  ORDER BY p.Name";
                //
                //                using (IDataReader reader = ExecuteReader(query))
                //                {
                //                    using (SafeDataReader sr = new SafeDataReader(reader))
                //                    {
                //                        while (sr.Read())
                //                        {
                //                            string pId  = sr.GetNullableString("PayerDetails");
                //                            string name = sr.GetNullableString("Name");
                //
                //                            if (pId == null || name == null)
                //                                continue;
                //
                //                            if (!res.AvailablePayers.ContainsKey(pId))
                //                                res.AvailablePayers.Add(pId, name);
                //                        }
                //                    }
                //                }
                //
                query = @"SELECT 'Self' as Code, 'Self' as CodeDesc UNION SELECT Code,CodeDesc FROM dbo.DependentRelationshipCodes (NOLOCK) order by CodeDesc";
                using (IDataReader reader = ExecuteReader(query))
                {
                    using (SafeDataReader sr = new SafeDataReader(reader))
                    {
                        while (sr.Read())
                        {
                            string key = sr.GetString("Code");
                            if (!res.AvailableInsRelationships.ContainsKey(key))
                                res.AvailableInsRelationships.Add(key, sr.GetString("CodeDesc"));
                        }
                    }
                }
                res.LoadAuthorizationAlerts(GetAuthorizationAlerts(id));
                res.LoadVolumeUnits(GetVolumeUnits(id));
                res.LoadNotificationSlots(FindAllNotificationSlots(false));
                //  res.LoadReferralSpecialities(GetReferralSpecialities(id));
                res.LoadAllMarketingReps(GetAllMarketingReps());
            }
            return res;
        }

        private List<AccountEnum> LoadAllSmoking()
        {
            List<AccountEnum> result = new List<AccountEnum>();

            const string sql = @"SELECT 	vss.RecodeVal 'Name', vss.StatusDesc 'Value' FROM Global.dbo.VisitSmokingStatuses vss";


            using (IDataReader reader = ExecuteReader(sql))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Add(sr.ToAccountEnum());
                    }
                }
            }
            return result;
        }

        private List<Ethnicity> LoadEthnicities()
        {
            List<Ethnicity> r = new List<Ethnicity>();
            const string sql = @"SELECT 	e.EthnicityID,
		                    e.Description,
		                    e.HL7Code FROM Global.dbo.Ethnicity e";

            using (IDataReader reader = ExecuteReader(sql))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        Ethnicity entry = sr.ToEthnicity();
                        r.Add(entry);
                    }
                }
            }
            return r;
        }

        private List<MarketingRep> GetAllMarketingReps()
        {
            List<MarketingRep> res = new List<MarketingRep>();

            string sql = @"IF EXISTS (SELECT
		                        *
	                        FROM sys.columns c
	                        WHERE c.object_id = OBJECT_ID(N'[dbo].[CrmUsers]'))
                        BEGIN
	                        SELECT
		                        [cu].[UserId],
		                        [cu].[UserLastName],
		                        [cu].[UserFirstName]
	                        FROM [CrmUsers] cu
                        END";

            using (IDataReader reader = ExecuteReader(sql))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        int userId = sr.GetInt32("UserId");
                        string firstName = sr.GetString("UserFirstName");
                        string lastName = sr.GetString("UserLastName");
                        MarketingRep rep = new MarketingRep(userId, firstName, lastName);
                        res.Add(rep);
                    }
                }
            }

            return res;
        }

        private List<int> GetVisitCreationTriggers()
        {
            List<int> res = new List<int>();
            const string sql = @"SELECT svct.SchedulerAppointmentStatus FROM SchedulerVisitCreationTriggers svct";

            using (IDataReader reader = ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    res.Add(id);
                }
            }

            return res;
        }

        private List<int> GetOrderCreationTriggers()
        {
            List<int> res = new List<int>();
            const string sql = @"SELECT 	soct.SchedulerAppointmentStatus, soct.SchedulerAppointmentStatus FROM SchedulerOrderCreationTriggers soct";

            using (IDataReader reader = ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    res.Add(id);
                }
            }

            return res;
        }


        private List<Race> LoadAllRaces()
        {
            List<Race> result = new List<Race>();
            const string sql = @"SELECT r.RaceID,
		                        r.Description,
		                        r.HL7Code FROM Global.dbo.Race r";

            using (IDataReader reader = ExecuteReader(sql))
            using (SafeDataReader sr = new SafeDataReader(reader))
                while (sr.Read())
                {
                    Race r = new Race();
                    r.Id = sr.GetInt32("RaceID");
                    r.Description = sr.GetNullableString("Description");
                    r.HL7Code = sr.GetNullableString("HL7Code");
                    result.Add(r);
                }

            return result;
        }

        private WorkingSchedule ReadAccountWorkingSchedule()
        {
            WorkingSchedule result = new WorkingSchedule();
            string query = @"select saw.SaWhId
                             , saw.WeekDayName 'WeekDayName'
	                         , cast(saw.StartTime as Datetime) 'StartTime'
	                         , cast(saw.EndTime as Datetime) 'EndTime'
	                         , cast(saw.BreakFrom as Datetime) as 'BreakFrom'
	                         , cast(saw.BreakTo as Datetime) as 'BreakTo'
	                         , saw.isActive from [SchedulerAccountWorkingHours] as saw where saw.IsActive = 1";

            using (IDataReader reader = ExecuteReader(query))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                    while (sr.Read())
                    {
                        //WorkingScheduleItem item = WorkingScheduleItem.ExtractFromReader(sr);
                        result.Items.Add(sr.ToWorkingScheduleItem());
                    }
            }

            query = @"SELECT [Id]
                              ,[Name]
                              ,[StartDate]
                              ,[EndTime]
                              ,[AllDay]
                          FROM [SchedulerHolidays] (NOLOCK)";

            using (IDataReader reader = ExecuteReader(query))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Holidays.Add(sr.ToHoliday());
                    }
                }
            }

            return result;
        }

        private void GetLocationsForAccount(Account res)
        {
            //todo:10/14/2016 sunil, what was i trying to do here?????
            //StringBuilder sb = new StringBuilder("SELECT LocationId,LocationName,location as 'AbbadoxLocation',Address,Country,State,City from SchedulerLocation (NOLOCK) sl");

            //sb.Append(" ORDER BY LocationName");
            if (res.AccessValidLocationsOnly)
            {
                string query =
                    @"
SELECT 
    LocationId,
    LocationName,
    COALESCE(sl.location, l.location) as 'AbbadoxLocation',
    Country,    
    COALESCE(L.Address, a.Address) + ISNULL(' ' + COALESCE(L.Address2, a.Address2), '') Address, 
    COALESCE(L.City, a.City) City, 
    COALESCE(l.State, a.State) State, 
    COALESCE(l.Zipcode, a.Zipcode) Zip,
    sl.IsForceStateMatch,
    sl.LocationAlert, 
    sl.PathToImage
from 
    SchedulerLocation (NOLOCK) sl
    JOIN Global.dbo.ValidLocations (NOLOCK) vl ON vl.Location = sl.Location
    left join Global.dbo.Accounts (NOLOCK) a on a.AccountId = @account 
    LEFT JOIN Location (NOLOCK) L ON l.Account = @account and l.Location = sl.Location
WHERE UserId=@uid AND vl.Account=@account
ORDER BY LocationName";

                using (IDataReader reader = ExecuteReader(query, res.Name, GlobalContext.RequestContext.UserName))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            long locId = reader.GetInt64(0);
                            if (!res.ResourceLocations.Any(p => p.Id == locId))
                                res.ResourceLocations.Add(reader.ToResourceLocation());
                        }
                        catch
                        {
                        }
                    }
                }
            }
            else
            {
                string query = @"
SELECT 
    LocationId,
    LocationName,
    COALESCE(sl.location, l.location) as 'AbbadoxLocation',
    Country,
    COALESCE(L.Address, a.Address) + ISNULL(' ' + COALESCE(L.Address2, a.Address2), '') Address, 
    COALESCE(L.City, a.City) City, 
    COALESCE(l.State, a.State) State, 
    COALESCE(l.Zipcode, a.Zipcode) Zip,
    sl.IsForceStateMatch,
    sl.LocationAlert, 
    sl.PathToImage
from 
    SchedulerLocation (NOLOCK) sl
    left join Global.dbo.Accounts (NOLOCK) a on a.AccountId = @account 
    LEFT JOIN Location (NOLOCK) L ON l.Account = @account and l.Location = sl.Location
ORDER BY LocationName";
                using (IDataReader reader = ExecuteReader(query, res.Name))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            long locId = reader.GetInt64(0);
                            if (!res.ResourceLocations.Any(p => p.Id == locId))
                                res.ResourceLocations.Add(reader.ToResourceLocation());
                        }
                        catch
                        {
                        }
                    }

                }
            }
        }

        private List<VolumeUnit> GetVolumeUnits(long id)
        {
            SetConnection2Account(id);
            List<VolumeUnit> result = new List<VolumeUnit>();
            string query =
                    @"SELECT ID
                      ,DisplayName, IsVisible
                  FROM SchedulerVolumeUnits  (NOLOCK) order by DisplayName asc";

            using (IDataReader reader = ExecuteReader(query))
            using (SafeDataReader sr = new SafeDataReader(reader))
                while (sr.Read())
                    result.Add(sr.ToVolumeUnit());

            return result;
        }

        public List<AccountEnum> GetAccountEnumsByType(string enumType)
        {
            SetConnection2Account();

            return LoadAccountEnumsByType(enumType);
        }

        private List<AccountEnum> LoadAccountEnumsByType(string enumType)
        {
            List<AccountEnum> result = new List<AccountEnum>();

            String sql =
                @"SELECT  ae.[id],ae.[Name],ae.[Value],ae.[EnumType],ae.[IsVisible], cast(case ISNULL(aed.[Name], 0) when '0' then 0 else 1 end as bit) as IsDefault,
ae.UserCanEdit, ae.UserCanDelete
FROM [AccountEnums] (NOLOCK) ae LEFT JOIN AccountEnumDefaults (NOLOCK) aed on
ae.EnumType = aed.EnumType and aed.Name = ae.Name
WHERE [IsVisible]=1 AND ae.EnumType =@enumType ORDER BY ae.DisplaySequence ASC, ae.Value ASC";

            using (IDataReader reader = ExecuteReader(sql, enumType))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Add(sr.ToAccountEnum());
                    }
                }
            }
            return result;
        }

        public List<AccountEnum> InsertUpdateAccountEnum(List<AccountEnum> accEnums)
        {
            SetConnection2Account();
            String sql;
            foreach (AccountEnum accEnum in accEnums)
            {
                if (accEnum.Id == -1)
                {
                    sql = @"
INSERT INTO [AccountEnums]
           ([Name]
           ,[Value]
           ,[EnumType]
           ,[IsVisible]
           ,[UserCanEdit]
           ,[UserCanDelete])
     VALUES
           (@Name
           ,@Value
           ,@EnumType
           ,@IsVisible
           ,@UserCanEdit
           ,@UserCanDelete)";
                    ExecuteNonQuery(sql, accEnum.Name, accEnum.Value, accEnum.EnumType, accEnum.IsVisible, accEnum.UserCanEdit, accEnum.UserCanDelete);
                }
                else
                {
                    sql =
                        @"
UPDATE [AccountEnums]
   SET [Name] = @Name
      ,[Value] = @Value
      ,[IsVisible] = @IsVisible
      ,[UserCanEdit] = @UserCanEdit
      ,[UserCanDelete] = @UserCanDelete
 WHERE Id = @Id";
                    ExecuteNonQuery(sql, accEnum.Name, accEnum.Value, accEnum.IsVisible, accEnum.UserCanEdit, accEnum.UserCanDelete, accEnum.Id);

                }
                if (accEnum.IsDefault)
                {
                    sql =
                        @"
If((Select COUNT (aed.EnumType) from [AccountEnumDefaults] aed where aed.EnumType = @EnumType) = 0)
BEGIN
INSERT INTO [AccountEnumDefaults]
           ([EnumType]
           ,[Name])
     VALUES
           (@EnumType
           ,@Name)
END
ELSE
BEGIN
UPDATE [AccountEnumDefaults]
   SET [Name] = @Name
 WHERE EnumType = @EnumType
END
";
                    ExecuteNonQuery(sql, accEnum.EnumType, accEnum.Name);

                }
            }

            List<AccountEnum> result = new List<AccountEnum>();

            sql = @"SELECT  ae.[id],ae.[Name],ae.[Value],ae.[EnumType],ae.[IsVisible], cast(case ISNULL(aed.[Name], 0) when '0' then 0 else 1 end as bit) as IsDefault,
ae.UserCanEdit, ae.UserCanDelete
FROM [AccountEnums] (NOLOCK) ae LEFT JOIN AccountEnumDefaults (NOLOCK) aed on
ae.EnumType = aed.EnumType and aed.Name = ae.Name
                    WHERE ae.[IsVisible]=1 AND ae.EnumType =@enumType order by Name asc";

            using (IDataReader reader = ExecuteReader(sql, accEnums[0].EnumType))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Add(sr.ToAccountEnum());
                    }
                }
            }
            return result;

        }

        private void ManageAccountWorkingSchedule(WorkingSchedule workingSchedule)
        {
            for (DayOfWeek i = DayOfWeek.Sunday; i <= DayOfWeek.Saturday; i++)
            {
                WorkingScheduleItem item = workingSchedule.GetScheduleForDay(i);
                if (item == null)
                    DeleteAccountWorkingHours(Enum.GetName(typeof(DayOfWeek), i));
                else if (item.Id == 0)
                    CreateAccountWorkingHours(item);
                else
                    UpdateAccountWorkingHours(item);
            }
        }

        private void DeleteAccountWorkingHours(string day)
        {
            string sql = @"delete SchedulerAccountWorkingHours
                      Where WeekDayName = @wName";

            ExecuteNonQuery(sql, day);
        }

        private void CreateAccountWorkingHours(WorkingScheduleItem item)
        {
            string sql = @"INSERT INTO SchedulerAccountWorkingHours
                      (WeekDayName, StartTime, EndTime, BreakFrom, BreakTo, isActive)
                        VALUES     (@wName,cast(@sTime as TIME),cast(@eTime as TIME),cast(@bsTime as TIME),cast(@beTime as TIME),@isActive)";

            ExecuteNonQuery(sql, item.WeekDay, item.StartTime, item.EndTime, item.BreakFrom, item.BreakTo, item.IsActive);
        }

        private void UpdateAccountWorkingHours(WorkingScheduleItem item)
        {
            string sql = @"
update SchedulerAccountWorkingHours 
set 
    StartTime=cast(@st as Time), 
    EndTime=cast(@et as Time), 
    BreakFrom=cast(@bst as Time), 
    BreakTo=cast(@bet as Time), 
    isActive=@ise 
where 
    SaWhId=@id";
            ExecuteNonQuery(sql, item.StartTime, item.EndTime, item.BreakFrom, item.BreakTo, item.IsActive, item.Id);
        }

        #endregion

        #region ICanCreate<Account> Members

        public Account Create(Account entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICanRemove<Account> Members

        public void Remove(Account entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICanUpdate<Account> Members

        public void Update(Account entity)
        {
            SetConnection2Account(entity.Id);

            //Old sql
            //            String sql = @"UPDATE [SchedulerAccountConfig]
            //                           SET [MonIsWorking] = @mon
            //                              ,[TueIsWorking] = @tue
            //                              ,[WenIsWorkgin] = @wen
            //                              ,[ThrIsWorking] = @thr
            //                              ,[FriIsWorking] = @fri
            //                              ,[SatIsWorking] = @sat
            //                              ,[SunIsWorking] = @sun
            //                              ,[StartWeekOn] = @startWeekOn
            //                              ,[StartWorkingHour] = @startWHour
            //                              ,[StartWorkingMinute] = @startWMinute
            //                              ,[EndWorkingHour] = @endWHour
            //                              ,[EndWorkingMinute] = @endWMinute
            //                              ,[DefaultViewSize] = @defViewSize
            //                              ,[DefaultViewMode] = @defViewMode
            //
            //                              ,[IsBillingNoteRequired] = @IsBillingNoteRequired
            //                              ,[IsVisitReasonRequired] = @isvisitReasonRequired
            //                              ,[DefaultScheduleMode] = @defScheduleMode
            //                              ,[ProcedureAutoExpansionMode]   = @ProcedureAutoExpansionMode
            //                              ,[PreselectProcedureTypes]   = @PreselectProcedureTypes
            //                              ,[PatientCategoryRequired]   = @PatientCategoryRequired
            //                              ,[IsProcedureGlobalSearchEnabled]   = @isProcedureGlobalSearchEnabled
            //                              ,[IsCreateOrderRequired]   = @isCreateOrderRequired
            //";

            String sql = @"UPDATE [SchedulerAccountConfig]
                           SET
                               [StartWeekOn] = @startWeekOn
                              ,[DefaultViewSize] = @defViewSize
                              ,[DefaultViewMode] = @defViewMode
                              ,[IsBillingNoteRequired] = @IsBillingNoteRequired
                              ,[IsVisitReasonRequired] = @isvisitReasonRequired
                              ,[DefaultScheduleMode] = @defScheduleMode
                              ,[ProcedureAutoExpansionMode]   = @ProcedureAutoExpansionMode
                              ,[PreselectProcedureTypes]   = @PreselectProcedureTypes
                              ,[PatientCategoryRequired]   = @PatientCategoryRequired
                              ,[IsProcedureGlobalSearchEnabled]   = @isProcedureGlobalSearchEnabled
                              ,[IsCreateOrderRequired]   = @isCreateOrderRequired
                              ,[IsPendingEnabled] = @IsPendingEnabled
                              ,[IsWarningMessagesEnabled] = @IsWarningMessagesEnabled
                              ,[IsCommentForBlockingRequired] = @IsCommentForBlockingRequired
                              ,[EmailAddress] = @EmailAddress
                              ,[IsPaymentsEnabled] = @IsPaymentsEnabled
                              ,[IsProcessPaymentsEnabled] = @IsProcessPaymentsEnabled
                              ,[PayersSearchMode] = @PayersSearchMode
                              ,[IsScheduleAppointmentByEstimationSlots] = @IsScheduleAppointmentByEstimationSlots
                              ,[IsStateOfServiceEnabled] = @IsStateOfServiceEnabled
                              ,[IsProcedureRequired] = @IsProcedureRequired
                              ,[MRNReadOnly] = @MRNReadOnly
                              ,[PatientDOBMandatory] = @PatientDOBMandatory
                              ,[IsMammographyActive] = @IsMammographyActive
";
            //                              --,[IsReferralRequired] = @IsReferralRequired

            ExecuteNonQuery(sql, false,//parameterLessQuery = false
                                       //entity.WorkingDays.Contains(DayOfWeek.Monday),
                                       //entity.WorkingDays.Contains(DayOfWeek.Tuesday),
                                       //entity.WorkingDays.Contains(DayOfWeek.Wednesday),
                                       //entity.WorkingDays.Contains(DayOfWeek.Thursday),
                                       //entity.WorkingDays.Contains(DayOfWeek.Friday),
                                       //entity.WorkingDays.Contains(DayOfWeek.Saturday),
                                       //entity.WorkingDays.Contains(DayOfWeek.Sunday),
                                entity.StartWeekOn,
                                //entity.StartWorkingHour,
                                //entity.StartWorkingMinute,
                                //entity.EndWorkingHour,
                                //entity.EndWorkingMinute,
                                entity.NumberOfVisibleHours,
                                entity.DefaultViewMode,
                                //entity.IsReferralRequired,
                                entity.IsBillingNoteRequired,
                                entity.IsVisitReasonRequired,

                                entity.ScheduleMode,
                                (int)entity.ProcedureExpansionMode,
                                entity.PreselectProcedureTypes,
                                entity.PatientCategoryRequired,
                                entity.IsProcedureGlobalSearchEnabled,
                                entity.IsCreateOrderRequired,
                                entity.IsPendingEnabled,
                                entity.IsWarningMessagesEnabled,
                                entity.IsCommentForBlockingRequired,
                                entity.SendEmailFromAddress,
                                entity.IsPaymentsEnabled,
                                entity.IsProcessPaymentsEnabled,
                                (int)entity.PayersSearchMode,
                                entity.IsScheduleAppointmentByEstimationSlots,
                                entity.IsStateOfServiceEnabled,
                                entity.IsProcedureRequired,
                                entity.MRNReadOnly,
                                entity.IsPatientDOBMandatory,
                                entity.IsMammographyActive


);

            sql = "DELETE FROM SchedulerHolidays";
            ExecuteNonQuery(sql);

            sql = @"INSERT INTO [SchedulerHolidays]
                           ([Name]
                           ,[StartDate]
                           ,[EndTime]
                           ,[AllDay])
                     VALUES
                           (@name,@start,@end,@allDay); SELECT SCOPE_IDENTITY() ";


            foreach (Holiday holiday in entity.WorkingSchedule.Holidays)
            {
                long newId = Convert.ToInt64(ExecuteScalar(sql, holiday.Name, holiday.StartTime, holiday.EndTime, holiday.Repeat));
                SetNewId(typeof(Holiday), holiday, newId);
            }

            ManageAccountWorkingSchedule(entity.WorkingSchedule);

            sql = @"delete from SchedulerOrderCreationTriggers"; // clear prevStatuses
            ExecuteNonQuery(sql);
            sql = @"INSERT INTO	SchedulerOrderCreationTriggers (SchedulerAppointmentStatus)
	            VALUES (@id)";
            foreach (int i in entity.OrderCreationTrigger)
                ExecuteNonQuery(sql, i);

            sql = @"delete from SchedulerVisitCreationTriggers"; // clear prevStatuses
            ExecuteNonQuery(sql);
            sql = @"INSERT INTO SchedulerVisitCreationTriggers (SchedulerAppointmentStatus)
                    VALUES(@id)";
            foreach (int i in entity.VisitCreationTrigger)
                ExecuteNonQuery(sql, i);

            //Order Creation mode update
            SetConnection2Global();
            String accountName = ResolveNameByAccountId(entity.Id);

            sql = "UPDATE Accounts SET ViewPatientsInValidLocationsOnly =@vpvl where AccountId = @accId";
            ExecuteNonQuery(sql, entity.ViewPatientsInValidLocationsOnly, accountName);

            sql = @"SELECT SchedulerConfigurationId FROM SchedulerConfigurations (NOLOCK) WHERE AccountId = @accountName";
            object dbRes = ExecuteScalar(sql, accountName);

            if (dbRes == null || dbRes == DBNull.Value)
            {
                //Configuration doesn't exist. It is needed to be created
                sql = @"INSERT INTO [SchedulerConfigurations]
                               ([AccountId]
                               ,[SchedulerOrderCreationModeId]
                               ,[MapToWorkTypeSourceTable]
                               ,[MapToWorkTypeField]
                                )
                         VALUES
                               (@AccountId
                               ,@SchedulerOrderCreationModeId
                               ,@MapToWorkTypeSourceTable
                               ,@MapToWorkTypeField)";

                ExecuteScalar(sql, accountName, entity.OrderCreationMode, /*entity.OrderCreationTrigger, entity.VisitCreationTrigger,*/
                              @"$G.[CPTCodesFull];$A.[CodeReferences]",
                              @"CPTCode;CodeReferenceId");
            }
            else
            {
                //Existing configuration should be updated
                sql = @"UPDATE [SchedulerConfigurations]
                           SET 
	                           [SchedulerOrderCreationModeId] = @SchedulerOrderCreationModeId
                         WHERE AccountId = @AccountId";

                ExecuteNonQuery(sql, entity.OrderCreationMode, accountName);

                OrderCreationParameter referringParam = entity.OrderCreationParameters.FirstOrDefault(a => a.ParamName == OrderCreationParameter.PHYS_ID_PARAM);
                if (referringParam != null)
                {
                    sql =
                        @"UPDATE [SchedulerOrderParameters]
                           SET 
	                           [isRequired] = @isRequired,
                               [ParameterDefaultValue]   = @ParameterDefaultValue
                         WHERE SchedulerConfigurationId = @SchedulerConfigurationId and
                                OrderParameterId = @OrderParameterId";

                    ExecuteNonQuery(sql, false,//parameterlessQueery = false
                        referringParam.IsRequired, referringParam.DefaultValue ?? (object)DBNull.Value, (int)dbRes, referringParam.Id);
                }
            }
        }

        #endregion


        #region AccountGenerateIDconfig related code

        public List<AccountGenerateIDconfig> GetAllAccountGenerateIDconfigs(string location)
        {
            string accountName = ResolveNameByAccountId();
            SetConnection2Global();
            List<AccountGenerateIDconfig> agic = new List<AccountGenerateIDconfig>();
            String sql = @"SELECT * FROM AccountGenerateIDconfigs (NOLOCK) agi WHERE AccountId = @Account AND (Location = '*' OR Location IS NULL OR Location = @Location)";
            using (IDataReader reader = ExecuteReader(sql, accountName, (string.IsNullOrEmpty(location) ? "" : location)))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        agic.Add(sr.ToAccountGenerateIDconfig());
                    }
                }
            }
            return agic;
        }

        public AccountGenerateIDconfig GetIdGenerator(string locationId, IdGenerationTypeName typeName)
        {
            string accountName = ResolveNameByAccountId();
            SetConnection2Global();
            AccountGenerateIDconfig result = null;
            //if exists in Cache return cached object
            //else read from DB (following code) 

            // sql will ensure that if there is a definite config that is returned in favor of a general config "*"
            string sql = @"
                SELECT top 1 * FROM (
                SELECT AccountGenerateIDconfigId
	                 , AccountId
	                 , Location
	                 , CustomLocationCode
	                 , IdTypeName
	                 , IDFormatString
	                 , PreFix
	                 , PostFix
	                 , StartingSeq
	                 , NextAvailableSeq
	                 , IsSeqPadded
	                 , SeqPaddingLen
	                 , SeqPaddingChar
	                 , SeqPaddingDir
	                 , UseGuid
	                 , GuidLen, 1 AS Lvl
                FROM
                    AccountGenerateIDconfigs (NOLOCK) agi
                WHERE
                    AccountId=@accountId AND Location = @Location AND IdTypeName=@idTypename
                UNION
                SELECT AccountGenerateIDconfigId
	                 , AccountId
	                 , @Location
	                 , CustomLocationCode
	                 , IdTypeName
	                 , IDFormatString
	                 , PreFix
	                 , PostFix
	                 , StartingSeq
	                 , NextAvailableSeq
	                 , IsSeqPadded
	                 , SeqPaddingLen
	                 , SeqPaddingChar
	                 , SeqPaddingDir
	                 , UseGuid
	                 , GuidLen, 2 AS Lvl
                FROM
                    AccountGenerateIDconfigs (NOLOCK) agi
                WHERE
                    AccountId=@accountId AND (Location = '*' OR Location IS NULL) AND IdTypeName=@idTypename
                )C order by Lvl ASC
            ";
            using (IDataReader dr = ExecuteReader(sql, accountName, locationId, typeName.ToString()))
            {
                using (SafeDataReader sr = new SafeDataReader(dr))
                {
                    if (sr.Read())
                    {
                        result = sr.ToAccountGenerateIDconfig();
                        return result;
                    }
                }
            }

            //Add to cache
            // if we have come this far, there is no configuration and so we will set to a default guid based configuration
            result = new AccountGenerateIDconfig(accountName, string.Empty);
            result.SetDefaultConfiguration(typeName.ToString());
            return result;
        }

        public int ReserveNewSeqForId(long idGeneratorId)
        {
            /* this function must be called each time a new id is required and the setting is not guid based */
            int newId = -1;
            SetConnection2Global();

            string sql = @"
                SELECT coalesce(NextAvailableSeq, StartingSeq)
                FROM
	                AccountGenerateIDconfigs (NOLOCK) agi
                WHERE
	                AccountGenerateIDconfigId = @idGeneratorId";

            object res = ExecuteScalar(sql, idGeneratorId);

            int r = Convert.ToInt32(res);
            if (r > 0)
            {
                sql = @"UPDATE AccountGenerateIDconfigs
	                SET
		                NextAvailableSeq = @res 
	                WHERE
		                AccountGenerateIDconfigId = @idGeneratorId";
                ExecuteNonQuery(sql, r + 1, idGeneratorId);
            }
            if (res != null) int.TryParse(res.ToString(), out newId);
            return newId;
        }

        public bool CheckIfNewIdExistsInAccount(string newId, string idType)
        {
            string sql = string.Empty;
            SetConnection2Account();
            switch (idType.ToLower())
            {
                case "mrn":
                    sql = @"SELECT Req FROM PatientInfo (NOLOCK) pat WHERE Req = @id";
                    break;
                case "order":
                    sql = @"SELECT OrderID FROM OrderSchedule (NOLOCK) os WHERE OrderID = @id";
                    break;
                case "job":
                    sql = @"SELECT JobID FROM OrderSchedule (NOLOCK) os WHERE JobID = @id";
                    break;
                case "payerext":
                    sql = @"SELECT ExternalID FROM Payers (NOLOCK) WHERE ExternalID = @id";
                    break;
                case "referringid":
                    sql = @"SELECT rp.ReferringId FROM RefPhysician rp WHERE rp.ReferringId = @id";
                    break;
            }
            if (!string.IsNullOrEmpty(sql))
            {
                object res = ExecuteScalar(sql, newId);
                return (res != null && !string.IsNullOrEmpty(res.ToString()));
            }

            throw new SchedulerException(SchedulerExceptionType.UndefinedIdGenerationType, idType);
        }

        #endregion // end region -- AccountGenerateIDconfig related code



        public void GetAdditionalUserInformation(IRequestContext rc)
        {
            SetConnection2Global();
            const string sql = @"SELECT
	                u.ID UserIdInt,
	                u.FirstName + ' ' + u.LastName UserFullName,
	                va.ID ValidAccountId,
	                a.Name AccountFullName
                FROM Users u
                INNER JOIN ValidAccounts va ON u.UserId = va.UserId
                INNER JOIN Accounts a ON a.AccountId = va.Account
                WHERE u.UserId = @u AND va.Account = @a";
            using (var dr = ExecuteReader(sql, rc.UserName, rc.Account))
            {
                while (dr.Read())
                {
                    //CP: Fix, not sure, how to handle
                    //rc.UserId = dr.GetInt32(0);
                    //rc.UserFullName = dr.GetString(1);
                    //rc.ValidAccountId = (int)dr.GetDecimal(2);
                    //rc.AccountFullName = dr.GetString(3);
                    break;
                }
            }
        }

        public void AttachVitalsToPatientVisit(int patientVisitId, Visit visit)
        {
            SetConnection2Account();
            string sql = string.Format(@"INSERT INTO VisitPatientVitals
                         (VisitID, Height, Weight, BMI, CreateUser, CreateDate, isMarried)
                         VALUES        (@visitId, @height, @weight, @bmi, @createuser, dateadd(minute, {0}, getdate()), DEFAULT)", GetTimeZonesDiff);

            ExecuteNonQuery(sql, patientVisitId, visit.PatientHeight, visit.PatientWeight, Visit.CalculateBMI(visit.PatientWeight, visit.PatientHeight), GlobalContext.RequestContext.UserName);

            ExecuteNonQuery(@"
                            INSERT INTO VisitVitals (PatientVisitID,  BloodPressureSystolic, BloodPressureDiastolic, EnteredBy, LastUser, LastDate )
	                            VALUES (@ivistid,@bpFrom,@bpTo,@enteredBy,@lastUser,@lastDate);", patientVisitId, visit.BPFrom, visit.BPTo, GlobalContext.RequestContext.UserName, GlobalContext.RequestContext.UserName, DateTime.Now);

        }

        public void UpdateVitalsToPatientVisit(int patientVisitId, Visit visit)
        {
            SetConnection2Account();

            const string sql = @"
                                UPDATE VisitPatientVitals
                                SET Height = @height, Weight=@weight,BMI=@bmi
                                WHERE VisitID=@visitId";

            ExecuteNonQuery(sql, visit.PatientHeight, visit.PatientWeight, Visit.CalculateBMI(visit.PatientWeight, visit.PatientHeight), patientVisitId);
        }

        public bool AllowAppointmentForOutofNetworkPrimaryPayer(long schedulerLocationId, string patientMrn)
        {
            SetConnection2Account();
            const string sql = @"SELECT slpe.IsHardStop FROM (
                SELECT TOP 1 pins.LocalPayerId FROM PatientInsurances pins 
                WHERE  pins.Deleted = 0 AND pins.InsuranceLevel = 1 AND pins.PatientID = @mrn
                ORDER BY pins.CreatedDate DESC) prim
                INNER JOIN SchedulerLocationPayerExceptions slpe ON prim.LocalPayerId = slpe.PayerId
                WHERE slpe.IsDeleted = 0 AND slpe.IsHardStop = 1 AND slpe.SchedulerLocationId = @sloc";
            var res = ExecuteScalar(sql, patientMrn, schedulerLocationId);
            return res == null;
        }

        public void CreatePatientMruEntry(int userIdInt, string userName, long patientAutocount, string patientreq)
        {
            SetConnection2Account();
            const string sql =
                @"IF EXISTS (SELECT * FROM SchedulerPatientMruList spml WHERE spml.UserIntId = @UserIntId AND spml.PatAutoCount = @PatAutoCount)
	            UPDATE SchedulerPatientMruList SET DateAccessed = GETDATE() WHERE UserIntId = @UserIntId AND PatAutoCount = @PatAutoCount
            ELSE
	            INSERT INTO SchedulerPatientMruList (UserIntId, UserLogin, PatAutoCount, PatientReq)
	            VALUES (@UserIntId, @UserLogin, @PatAutoCount, @PatientReq)";
            var res = ExecuteScalar(sql, userIdInt, patientAutocount, userName, patientreq);
        }

        public List<NotificationSlotCpt> FindNotificationSlotsCptsById(int notificationSlotId)
        {
            List<NotificationSlotCpt> r = new List<NotificationSlotCpt>();
            SetConnection2Account();
            const string sql = @"SELECT 	snstc.Id,
		                            snstc.NotificationSlotId,
		                            snstc.CPTCode,
		                            snstc.IsActive FROM SchedulerNotificationSlotToCpt snstc
                                    WHERE snstc.NotificationSlotId=@id";

            using (IDataReader reader = ExecuteReader(sql, notificationSlotId))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        int id = sr.GetInt32("Id");
                        int nsid = sr.GetInt32("NotificationSlotId");
                        string cpt = sr.GetString("CPTCode");
                        bool isActive = sr.GetBoolean("IsActive");
                        r.Add(new NotificationSlotCpt(id, nsid, cpt, isActive));
                    }
                }
            }

            return r;
        }

        public void UpdateNotificationSlotsCpts(List<NotificationSlotCpt> slots)
        {
            SetConnection2Account();
            NotificationSlotCpt element = slots.FirstOrDefault();

            if (element != null)
            {
                ExecuteNonQuery(@"
						DELETE from SchedulerNotificationSlotToCpt
						WHERE NotificationSlotId=@slotId", element.NotificationSlotId);
            }


            foreach (NotificationSlotCpt cpt in slots)
            {
                if (string.IsNullOrEmpty(cpt.CptCode)) continue;
                ExecuteNonQuery(@"INSERT INTO SchedulerNotificationSlotToCpt (NotificationSlotId, CPTCode, IsActive)
	                                VALUES (@nslotiD, @cpt, @isActive);", cpt.NotificationSlotId, cpt.CptCode, cpt.IsActive);
            }
        }

        public void UpdateEhrMaritalStatus(bool isActive, int patientVisitId)
        {
            SetConnection2Account();
            const string sql = @"UPDATE VisitPatientVitals
                                    SET isMarried=@isMarried
                                    WHERE VisitID=@visitId";

            ExecuteNonQuery(sql, false, isActive, patientVisitId);
        }

        public void UpdateEhrProvider(AppointmentResourcePhysician phys, int patientVisitId)
        {
            SetConnection2Account();
            const string sql = @"
                                    UPDATE Visits
                                    SET HealthProviderId= @provId
                                    WHERE VisitId=@visitId";

            ExecuteNonQuery(sql, phys == null ? (object)null : phys.Id, patientVisitId);
        }

        public void UpdateEhrLocation(long? locationId, int patientVisitId)
        {
            if (locationId.HasValue)
            {
                SetConnection2Account();
                ExecuteNonQuery(@"UPDATE Visits
                                    SET LocationId = (SELECT top 1 l.Id FROM Location l WHERE l.Id=@locId)
                                    WHERE VisitId = @visitId", locationId.Value, patientVisitId);
            }
        }

        public void UpdateEhrChiefComplaint(string visitVisitReason, int patientVisitId)
        {
            SetConnection2Account();
            ExecuteNonQuery(@"
                                UPDATE VisitPatientVitals
                                SET ChiefComplaint = @text
                                WHERE VisitID = @vId", visitVisitReason, patientVisitId);
        }

        public void UpdateEhrIsPregnant(bool visitIsPregnant, int patientVisitId)
        {
            SetConnection2Account();
            ExecuteNonQuery(@"UPDATE VisitPatientVitals
                                SET isPregnant = @preg
                                WHERE VisitID = @vId", false, visitIsPregnant ? (object)DateTime.Now : DBNull.Value, patientVisitId);
        }

        public bool EsquaredSendEmail(long appointmentId, string email, string subject, string htmlBody)
        {
            SetConnection2Account();
            var tosubj = "|" + email + " | " + subject;
            const string sql =
                @"INSERT INTO esquared.dbo.Events (AccountName, JobID, EventTypeID, SourceApplicationID, CreateDateTime, 
LastUpdateDateTime, CurrentStateID, AdditionalInfoString, AdditionalInfoString2)
SELECT DB_NAME(), @JobID, 1650, 99, GETDATE(), GETDATE(), 1, ast.Value + @tosubject, @htmlbody 
FROM AccountSettings ast WHERE ast.Name = 'AccountDefaultEmailId' AND ast.IsActive = 1";

            ExecuteNonQuery(sql, appointmentId, tosubj, htmlBody);
            return true;
        }
    }
}
