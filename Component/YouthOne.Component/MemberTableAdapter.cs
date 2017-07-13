using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class MemberTableAdapter
    {
        #region 插入团员信息
        public int MyInsert(
            string OID,
            string EmpName,
            string HrCode,
            string Sex,
            string Dept,
            string WorkGroup,
            string Post,
            string ParttimeName,
            string Politics,
            System.DateTime PartyDate,
            string Wedding,
            string Nation,
            string NativePlace,
            string Mobile,
            string EmpID,
            string House,
            System.DateTime Birthday,
            System.DateTime JobDateTime,
            System.DateTime ComDateTime,
            string FstSchoolExp,
            string FstDegree,
            string LstSchoolExp,
            string LstDegree,
            string SkillLevel,
            string VolunteerInfo,
            string SpecialSkill,
            string ApplyParty,
            System.DateTime ApplyPartyDate,
            string FstSchool,
            string FstProfession,
            System.DateTime FstGraduateDate,
            string LstSchool,
            string LstProfession,
            System.DateTime LstGraduateDate,
            string HrType,
            string HrStatus,
            int YouthChargeStd,
            string FK_YouthGroup_FZ,
            string Email,
            string FK_YouthGroup
            )
        {

            FstSchoolExp = FstSchoolExp?? string.Empty;
            FstDegree = FstDegree?? string.Empty;
            LstSchoolExp = LstSchoolExp?? string.Empty;
            LstDegree = LstDegree?? string.Empty;
            SkillLevel = SkillLevel?? string.Empty;
            VolunteerInfo = VolunteerInfo ?? string.Empty;
            ApplyParty = ApplyParty ?? string.Empty;
            LstSchool = LstSchool ?? string.Empty;
            LstProfession = LstProfession ?? string.Empty;
            FstSchool = FstSchool ?? string.Empty;
            FstProfession = FstProfession ?? string.Empty;
            WorkGroup = WorkGroup ?? string.Empty;
            Email = Email ?? string.Empty;
            Post = Post ?? string.Empty;
            ParttimeName = ParttimeName ?? string.Empty;
            NativePlace = NativePlace ?? string.Empty;
            Mobile = Mobile ?? string.Empty;
            EmpID = EmpID ?? string.Empty;

            //return Insert(Guid.NewGuid().ToString(), FK_YouthGroup, Dept, WorkGroup, Post, HrCode, HrType,
            //    HrStatus, EmpName, Sex, EmpID, Birthday, 0, ParttimeName,
            //    JobDateTime, ComDateTime, Wedding, NativePlace, Politics,
            //    PartyDate, Nation, House, SkillLevel, FstSchoolExp, FstDegree,
            //    LstSchoolExp, LstDegree, FstSchool, FstProfession, FstGraduateDate,
            //    LstSchool, LstProfession, LstGraduateDate, ApplyParty, ApplyPartyDate,
            //    VolunteerInfo, Mobile, Email, SpecialSkill, YouthChargeStd);

            return Insert(OID, FK_YouthGroup, FK_YouthGroup_FZ, Dept, WorkGroup, Post, HrCode, HrType,
                HrStatus, EmpName, EmpID, Sex, Birthday, 0, ParttimeName,
                JobDateTime, ComDateTime, Wedding, NativePlace, Politics,
                PartyDate, Nation, House, SkillLevel, FstSchoolExp, FstDegree,
                LstSchoolExp, LstDegree, FstSchool, FstProfession, FstGraduateDate,
                LstSchool, LstProfession, LstGraduateDate, ApplyParty, ApplyPartyDate,
                VolunteerInfo, Mobile, Email, SpecialSkill, YouthChargeStd);
        }
        #endregion

        #region 插入团员信息
        private SqlCommand _MyUpdateSqlComand;
        private SqlCommand MyUpdateSqlComand
        {
            get
            {
                if (_MyUpdateSqlComand == null)
                {
                    _MyUpdateSqlComand = new SqlCommand();
                    _MyUpdateSqlComand.Connection = this.Connection;
                    _MyUpdateSqlComand.CommandText = @"UPDATE [dbo].[Member]
                       SET [FK_YouthGroup_FZ] = @FK_YouthGroup_FZ
                          ,[Dept] = @Dept
                          ,[WorkGroup] = @WorkGroup
                          ,[Post] = @Post
                          ,[HrCode] = @HrCode
                          ,[HrType] = @HrType
                          ,[HrStatus] = @HrStatus
                          ,[EmpName] = @EmpName
                          ,[EmpID] = @EmpID
                          ,[Age] = @Age
                          ,[Sex] = @Sex
                          ,[Birthday] = @Birthday
                          ,[ParttimeName] = @ParttimeName
                          ,[JobDateTime] = @JobDateTime
                          ,[ComDateTime] = @ComDateTime
                          ,[Wedding] = @Wedding
                          ,[NativePlace] = @NativePlace
                          ,[Politics] = @Politics
                          ,[PartyDate] = @PartyDate
                          ,[Nation] = @Nation
                          ,[House] = @House
                          ,[SkillLevel] = @SkillLevel
                          ,[FstSchoolExp] = @FstSchoolExp
                          ,[FstDegree] = @FstDegree
                          ,[LstSchoolExp] = @LstSchoolExp
                          ,[LstDegree] = @LstDegree
                          ,[FstSchool] = @FstSchool
                          ,[FstProfession] = @FstProfession
                          ,[FstGraduateDate] = @FstGraduateDate
                          ,[LstSchool] = @LstSchool
                          ,[LstProfession] = @LstProfession
                          ,[LstGraduateDate] = @LstGraduateDate
                          ,[ApplyParty] = @ApplyParty
                          ,[ApplyPartyDate] = @ApplyPartyDate
                          ,[VolunteerInfo] = @VolunteerInfo
                          ,[Mobile] = @Mobile
                          ,[Email] = @Email
                          ,[SpecialSkill] = @SpecialSkill
                          ,[YouthChargeStd] = @YouthChargeStd
                     WHERE [OID] = @OID";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FK_YouthGroup_FZ", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FK_YouthGroup_FZ", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Dept", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Dept", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@WorkGroup", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "WorkGroup", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Post", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Post", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HrCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HrCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HrType", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HrType", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HrStatus", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HrStatus", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EmpName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EmpName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Sex", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Sex", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EmpID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EmpID", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Age", global::System.Data.SqlDbType.Int));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Birthday", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Birthday", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ParttimeName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ParttimeName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@JobDateTime", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "JobDateTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComDateTime", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComDateTime", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Wedding", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Wedding", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NativePlace", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NativePlace", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Politics", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Politics", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PartyDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PartyDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Nation", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Nation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@House", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "House", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SkillLevel", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SkillLevel", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FstSchoolExp", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FstSchoolExp", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FstDegree", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FstDegree", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LstSchoolExp", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LstSchoolExp", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LstDegree", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LstDegree", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FstSchool", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FstSchool", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FstProfession", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FstProfession", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FstGraduateDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FstGraduateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LstSchool", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LstSchool", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LstProfession", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LstProfession", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LstGraduateDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LstGraduateDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ApplyParty", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ApplyParty", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ApplyPartyDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ApplyPartyDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@VolunteerInfo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "VolunteerInfo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Mobile", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Mobile", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Email", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Email", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SpecialSkill", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SpecialSkill", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@YouthChargeStd", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "YouthChargeStd", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
                }

                return _MyUpdateSqlComand;
            }
        }


        public int MyUpdate(
            string EmpName,
            string HrCode,
            string Sex,
            string Dept,
            string WorkGroup,
            string Post,
            string ParttimeName,
            string Politics,
            System.DateTime PartyDate,
            string Wedding,
            string Nation,
            string NativePlace,
            string Mobile,
            string EmpID,
            string House,
            System.DateTime Birthday,
            System.DateTime JobDateTime,
            System.DateTime ComDateTime,
            string FstSchoolExp,
            string FstDegree,
            string LstSchoolExp,
            string LstDegree,
            string SkillLevel,
            string VolunteerInfo,
            string SpecialSkill,
            string ApplyParty,
            System.DateTime ApplyPartyDate,
            string FstSchool,
            string FstProfession,
            System.DateTime FstGraduateDate,
            string LstSchool,
            string LstProfession,
            System.DateTime LstGraduateDate,
            string HrType,
            string HrStatus,
            int YouthChargeStd,
            string Email,
            string FK_YouthGroup,
            string FK_YouthGroup_FZ,
            string OID
            )
        {
            if ((OID == null))
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[0].Value = ((string)(OID));
            }
            if ((FK_YouthGroup_FZ == null))
            {
                MyUpdateSqlComand.Parameters[1].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[1].Value = ((string)(FK_YouthGroup_FZ));
            }
            if ((Dept == null))
            {
                throw new global::System.ArgumentNullException("Dept");
            }
            else
            {
                MyUpdateSqlComand.Parameters[2].Value = ((string)(Dept));
            }
            if ((WorkGroup == null))
            {
                MyUpdateSqlComand.Parameters[3].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[3].Value = ((string)(WorkGroup));
            }
            if ((Post == null))
            {
                MyUpdateSqlComand.Parameters[4].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[4].Value = ((string)(Post));
            }
            if ((HrCode == null))
            {
                MyUpdateSqlComand.Parameters[5].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[5].Value = ((string)(HrCode));
            }
            if ((HrType == null))
            {
                throw new global::System.ArgumentNullException("HrType");
            }
            else
            {
                MyUpdateSqlComand.Parameters[6].Value = ((string)(HrType));
            }
            if ((HrStatus == null))
            {
                throw new global::System.ArgumentNullException("HrStatus");
            }
            else
            {
                MyUpdateSqlComand.Parameters[7].Value = ((string)(HrStatus));
            }
            if ((EmpName == null))
            {
                throw new global::System.ArgumentNullException("EmpName");
            }
            else
            {
                MyUpdateSqlComand.Parameters[8].Value = ((string)(EmpName));
            }
            if ((Sex == null))
            {
                throw new global::System.ArgumentNullException("Sex");
            }
            else
            {
                MyUpdateSqlComand.Parameters[9].Value = ((string)(Sex));
            }
            if ((EmpID == null))
            {
                throw new global::System.ArgumentNullException("EmpID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[10].Value = ((string)(EmpID));
            }
            MyUpdateSqlComand.Parameters[11].Value = 0;
            MyUpdateSqlComand.Parameters[12].Value = ((System.DateTime)(Birthday));
            if ((ParttimeName == null))
            {
                MyUpdateSqlComand.Parameters[13].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[13].Value = ((string)(ParttimeName));
            }
            MyUpdateSqlComand.Parameters[14].Value = ((System.DateTime)(JobDateTime));
            MyUpdateSqlComand.Parameters[15].Value = ((System.DateTime)(ComDateTime));
            if ((Wedding == null))
            {
                throw new global::System.ArgumentNullException("Wedding");
            }
            else
            {
                MyUpdateSqlComand.Parameters[16].Value = ((string)(Wedding));
            }
            if ((NativePlace == null))
            {
                MyUpdateSqlComand.Parameters[17].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[17].Value = ((string)(NativePlace));
            }
            if ((Politics == null))
            {
                throw new global::System.ArgumentNullException("Politics");
            }
            else
            {
                MyUpdateSqlComand.Parameters[18].Value = ((string)(Politics));
            }
            MyUpdateSqlComand.Parameters[19].Value = ((System.DateTime)(PartyDate));
            if ((Nation == null))
            {
                throw new global::System.ArgumentNullException("Nation");
            }
            else
            {
                MyUpdateSqlComand.Parameters[20].Value = ((string)(Nation));
            }
            if ((House == null))
            {
                throw new global::System.ArgumentNullException("House");
            }
            else
            {
                MyUpdateSqlComand.Parameters[21].Value = ((string)(House));
            }
            if ((SkillLevel == null))
            {
                MyUpdateSqlComand.Parameters[22].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[22].Value = ((string)(SkillLevel));
            }
            if ((FstSchoolExp == null))
            {
                MyUpdateSqlComand.Parameters[23].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[23].Value = ((string)(FstSchoolExp));
            }
            if ((FstDegree == null))
            {
                MyUpdateSqlComand.Parameters[24].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[24].Value = ((string)(FstDegree));
            }
            if ((LstSchoolExp == null))
            {
                MyUpdateSqlComand.Parameters[25].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[25].Value = ((string)(LstSchoolExp));
            }
            if ((LstDegree == null))
            {
                MyUpdateSqlComand.Parameters[26].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[26].Value = ((string)(LstDegree));
            }
            if ((FstSchool == null))
            {
                MyUpdateSqlComand.Parameters[27].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[27].Value = ((string)(FstSchool));
            }
            if ((FstProfession == null))
            {
                MyUpdateSqlComand.Parameters[28].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[28].Value = ((string)(FstProfession));
            }
            MyUpdateSqlComand.Parameters[29].Value = ((System.DateTime)(FstGraduateDate));
            if ((LstSchool == null))
            {
                MyUpdateSqlComand.Parameters[30].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[30].Value = ((string)(LstSchool));
            }
            if ((LstProfession == null))
            {
                MyUpdateSqlComand.Parameters[31].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[31].Value = ((string)(LstProfession));
            }
            MyUpdateSqlComand.Parameters[32].Value = ((System.DateTime)(LstGraduateDate));
            if ((ApplyParty == null))
            {
                MyUpdateSqlComand.Parameters[33].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[33].Value = ((string)(ApplyParty));
            }
            MyUpdateSqlComand.Parameters[34].Value = ((System.DateTime)(ApplyPartyDate));
            if ((VolunteerInfo == null))
            {
                MyUpdateSqlComand.Parameters[35].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[35].Value = ((string)(VolunteerInfo));
            }
            if ((Mobile == null))
            {
                MyUpdateSqlComand.Parameters[36].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[36].Value = ((string)(Mobile));
            }
            if ((Email == null))
            {
                MyUpdateSqlComand.Parameters[37].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[37].Value = ((string)(Email));
            }
            if ((SpecialSkill == null))
            {
                MyUpdateSqlComand.Parameters[38].Value = String.Empty;
            }
            else
            {
                MyUpdateSqlComand.Parameters[38].Value = ((string)(SpecialSkill));
            }
            MyUpdateSqlComand.Parameters[39].Value = ((int)(YouthChargeStd));

            return AdapterHelper.ExecuteCommand(MyUpdateSqlComand);
        }
        #endregion

        #region 删除导入数据
        private SqlCommand _MyDeleteSqlComand;
        private SqlCommand MyDeleteSqlComand
        {
            get
            {
                if (_MyDeleteSqlComand == null)
                {
                    _MyDeleteSqlComand = new SqlCommand();
                    _MyDeleteSqlComand.Connection = this.Connection;
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[Member] WHERE [OID] = @OID";
                    _MyDeleteSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyDeleteSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyDeleteSqlComand;
            }
        }

        public int MyDelete(string OID)
        {
            if ((OID == null))
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyDeleteSqlComand.Parameters[0].Value = OID;
            }

            return AdapterHelper.ExecuteCommand(MyDeleteSqlComand);
        }
        #endregion

        #region 获取单个团员
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "SELECT * FROM dbo.Member WHERE OID = @OID";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.Text;
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MySelectSqlComand;
            }
        }

        public int Fill(YouthOneDS.MemberDataTable dataTable, string memberOID)
        {
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = memberOID;

            if ((this.ClearBeforeFill == true))
            {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        #endregion

        #region 团员转出
        private SqlCommand _ChangeYGComand;
        private SqlCommand ChangeYGComand
        {
            get
            {
                if (_ChangeYGComand == null)
                {
                    _ChangeYGComand = new SqlCommand();
                    _ChangeYGComand.Connection = this.Connection;
                    _ChangeYGComand.CommandText = "ChangeYouthGroup";
                    _ChangeYGComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _ChangeYGComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MM_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MM_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _ChangeYGComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@YG_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "YG_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _ChangeYGComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OP_ADMIN", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OP_ADMIN", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _ChangeYGComand;
            }
        }

        /// <summary>
        /// 团员转出
        /// </summary>
        /// <param name="MM_OID">团员OID</param>
        /// <param name="CYG_OID">转入团支部OID</param>
        /// <returns></returns>
        public int ChangeYG(string MM_OID, string YG_OID, string OP_ADMIN)
        {
            ChangeYGComand.Parameters[0].Value = MM_OID;
            ChangeYGComand.Parameters[1].Value = YG_OID;
            ChangeYGComand.Parameters[2].Value = OP_ADMIN;

            NotificationTableAdapter.ClearNfCache();

            return AdapterHelper.ExecuteCommand(ChangeYGComand);
        }
        #endregion

        #region 状态改变：内退、离职
        public int ChangeStatus(string MM_OID, string HrStatus)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = this.Connection;
            comm.CommandText = "UPDATE dbo.Member SET HrStatus = @HrStatus WHERE OID = @MM_OID";
            comm.CommandType = System.Data.CommandType.Text;

            comm.Parameters.Add("@HrStatus", System.Data.SqlDbType.NVarChar, 50);
            comm.Parameters.Add("@MM_OID", System.Data.SqlDbType.NVarChar, 50);
            comm.Parameters[0].Value = HrStatus;
            comm.Parameters[1].Value = MM_OID;

            NotificationTableAdapter.ClearNfCache();

            return AdapterHelper.ExecuteCommand(comm);
        }


        #endregion
        #region 获取分支下的团员
        private SqlCommand _GetFZMemberComand;
        private SqlCommand GetFZMmeberComand
        {
            get
            {
                if (_GetFZMemberComand == null)
                {
                    _GetFZMemberComand = new SqlCommand();
                    _GetFZMemberComand.Connection = this.Connection;
                    _GetFZMemberComand.CommandText = "SELECT * FROM dbo.Member WHERE FK_YouthGroup_FZ = @OID";
                    _GetFZMemberComand.CommandType = System.Data.CommandType.Text;
                    _GetFZMemberComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _GetFZMemberComand;
            }
        }
        /// <summary>
        /// 获取分支下的团员
        /// </summary>
        /// <param name="FZ_OID"></param>
        /// <returns></returns>
        public YouthOneDS.MemberDataTable GetMemberFZ(string FZ_OID)
        {
            YouthOneDS.MemberDataTable dataTable = new YouthOneDS.MemberDataTable();

            this.Adapter.SelectCommand = GetFZMmeberComand;
            this.Adapter.SelectCommand.Parameters[0].Value = FZ_OID;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }
        #endregion

        #region 获取总支下的团员
        private SqlCommand _GetYGMemberComand;
        private SqlCommand GetYGMmeberComand
        {
            get
            {
                if (_GetYGMemberComand == null)
                {
                    _GetYGMemberComand = new SqlCommand();
                    _GetYGMemberComand.Connection = this.Connection;
                    _GetYGMemberComand.CommandText = "SELECT * FROM dbo.Member WHERE FK_YouthGroup = @OID";
                    _GetYGMemberComand.CommandType = System.Data.CommandType.Text;
                    _GetYGMemberComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _GetYGMemberComand;
            }
        }
        /// <summary>
        /// 获取支部下的团员
        /// </summary>
        /// <param name="FZ_OID"></param>
        /// <returns></returns>
        public YouthOneDS.MemberDataTable GetMemberYG(string YG_OID)
        {
            YouthOneDS.MemberDataTable dataTable = new YouthOneDS.MemberDataTable();

            this.Adapter.SelectCommand = GetYGMmeberComand;
            this.Adapter.SelectCommand.Parameters[0].Value = YG_OID;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }
        #endregion

        /// <summary>
        /// 获取支部或分支下的团员
        /// </summary>
        /// <param name="YG_OID"></param>
        /// <param name="FZ_OID"></param>
        /// <returns></returns>
        public YouthOneDS.MemberDataTable GetMember(string YG_OID, string FZ_OID)
        {
            if (String.IsNullOrEmpty(FZ_OID))
                return GetMemberYG(YG_OID);
            else
                return GetMemberFZ(FZ_OID);
        }

        /// <summary>
        /// 根据OID获取到团员信息
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public YouthOneDS.MemberDataTable GetMemberByOID(string OID)
        {
            YouthOneDS.MemberDataTable dataTable = new YouthOneDS.MemberDataTable();

            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = OID;
            int returnValue = this.Adapter.Fill(dataTable);

            return dataTable;
        }

        #region 根据工号获取到团员信息
        private SqlCommand _GetMemberHrComand;
        private SqlCommand GetMemberHrComand
        {
            get
            {
                if (_GetMemberHrComand == null)
                {
                    _GetMemberHrComand = new SqlCommand();
                    _GetMemberHrComand.Connection = this.Connection;
                    _GetMemberHrComand.CommandText = "SELECT * FROM dbo.Member WHERE HrCode = @HrCode";
                    _GetMemberHrComand.CommandType = System.Data.CommandType.Text;
                    _GetMemberHrComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@HrCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "HrCode", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _GetMemberHrComand;
            }
        }


        /// <summary>
        /// 根据工号获取到团员信息
        /// </summary>
        /// <param name="HrCode"></param>
        /// <returns></returns>
        public YouthOneDS.MemberDataTable GetMember(string HrCode)
        {
            YouthOneDS.MemberDataTable dataTable = new YouthOneDS.MemberDataTable();

            this.Adapter.SelectCommand = GetMemberHrComand;
            this.Adapter.SelectCommand.Parameters[0].Value = HrCode;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        #endregion


        #region 随机取得一个员工
        private SqlCommand _GetMemberRandomComand;
        private SqlCommand GetMemberRandomComand
        {
            get
            {
                if (_GetMemberRandomComand == null)
                {
                    _GetMemberRandomComand = new SqlCommand();
                    _GetMemberRandomComand.Connection = this.Connection;
                    _GetMemberRandomComand.CommandText = "SELECT TOP 1 * FROM dbo.Member WHERE HrStatus <> '离职'";
                    _GetMemberRandomComand.CommandType = System.Data.CommandType.Text;
                }

                return _GetMemberRandomComand;
            }
        }


        /// <summary>
        /// 根据工号获取到团员信息
        /// </summary>
        /// <param name="HrCode"></param>
        /// <returns></returns>
        public YouthOneDS.MemberDataTable GetMemberRandom()
        {
            YouthOneDS.MemberDataTable dataTable = new YouthOneDS.MemberDataTable();

            this.Adapter.SelectCommand = GetMemberRandomComand;
            this.Adapter.Fill(dataTable);
            return dataTable;
        }

        #endregion

        #region 随机取得一个员工
        private SqlCommand _GetSearchMemberComand;
        private SqlCommand GetSearchMemberComand
        {
            get
            {
                if (_GetSearchMemberComand == null)
                {
                    _GetSearchMemberComand = new SqlCommand();
                    _GetSearchMemberComand.Connection = this.Connection;
                    _GetSearchMemberComand.CommandText = "SearchMember";
                    _GetSearchMemberComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _GetSearchMemberComand.Parameters.Add("YG_OID", System.Data.SqlDbType.NVarChar);
                    _GetSearchMemberComand.Parameters.Add("FZ_OID", System.Data.SqlDbType.NVarChar);
                    _GetSearchMemberComand.Parameters.Add("SH_TEXT", System.Data.SqlDbType.NVarChar);
                    _GetSearchMemberComand.Parameters.Add("SH_LB", System.Data.SqlDbType.NVarChar);
                }

                return _GetSearchMemberComand;
            }
        }


        /// <summary>
        /// 查询员工
        /// </summary>
        /// <param name="HrCode"></param>
        /// <returns></returns>
        public YouthOneDS.MemberDataTable SearchMember(string YG_OID, string FZ_OID, string SH_TEXT, string SH_LB)
        {
            FZ_OID = FZ_OID ?? string.Empty;
            SH_TEXT = SH_TEXT ?? string.Empty;

            YouthOneDS.MemberDataTable dataTable = new YouthOneDS.MemberDataTable();

            GetSearchMemberComand.Parameters[0].Value = YG_OID;
            GetSearchMemberComand.Parameters[1].Value = FZ_OID;
            GetSearchMemberComand.Parameters[2].Value = SH_TEXT;
            GetSearchMemberComand.Parameters[3].Value = SH_LB;
            this.Adapter.SelectCommand = GetSearchMemberComand;
            this.Adapter.Fill(dataTable);
            return dataTable;
        }

        #endregion
    }
}
