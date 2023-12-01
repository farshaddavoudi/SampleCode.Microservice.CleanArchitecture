namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.DbConstants;

public class SqlQueryConst
{
    public const string AddUsersView = $@"CREATE OR ALTER VIEW [dbo].[{ViewNameConst.UsersRahkaranView}]

        AS
        SELECT A.[UserId]
			  ,A.[PersonnelCode]
			  ,A.[Username]
			  ,A.[RahkaranId]
			  ,A.[FirstName]
			  ,A.[LastName]
			  ,A.[FullName]
			  ,A.[FatherName]
			  ,A.[LastHokmExecutionDate]
			  ,A.[LastContractStartJalaliDate]
			  ,A.[LastContractEndJalaliDate]
			  ,A.[LastHokmEndDate]
			  ,A.[Gender]
			  ,A.[GenderDisplay]
			  ,A.[Mobile]
			  ,A.[WorkLocationCode]
			  ,A.[WorkLocation]
			  ,A.[NationalCode]
			  ,A.[BirthDate]
			  ,A.[MaritalStatus]
			  ,A.[EmploymentDate]
			  ,A.[EmploymentStatusId]
			  ,A.[Dismissed]
			  ,A.[EmploymentStatusTitle]
			  ,A.[PostId]
			  ,A.[JobTitle]
			  ,A.[PostTitle]
			  ,A.[BoxId]
			  ,A.[ParentBoxId]
			  ,A.[UnitName]
			  ,A.[UnitDivisionId]
			  ,A.[UnitDivisionName]
			  ,A.[Address]
			  ,A.[EducationLevel]
			  ,A.[EducationField]
			  ,A.[ChildrenNo]
			  ,A.[InsuranceCode]
			  ,A.[MellatAccountNo]
			  ,A.[SaderatAccountNo]
			  ,A.[MellatShebaNo]
			  ,A.[SaderatShebaNo]
              ,B.ENFirstName
	          ,B.ENLastName
	          ,B.Pos
	          ,B.JobPosition
	          ,B.ScheduleName
	          ,B.Active
	          ,B.CrewCode
	          ,B.ACType
	          ,B.LicenceNo
	          ,B.PassportNo
	          ,B.MedicalExpire
	          ,B.LicenceExpire
	          ,B.CMCExpire
	          ,B.BaseStation
	          ,B.L4Expire
	          ,B.PassportExpire
	          ,B.PassportExpireDaysLeft
	          ,B.MedicalExpireDaysLeft
	          ,B.LicenceExpireDaysLeft
	          ,B.CMCExpireDaysLeft
	          ,B.L4ExpireLeft
	          ,B.MultiType
        FROM [ATAUsers].[dbo].[Users] A LEFT JOIN [dbo].[CrewsView] B ON A.PersonnelCode = B.PersonnelCode

        GO";
}