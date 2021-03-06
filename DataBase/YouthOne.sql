IF NOT EXISTS (SELECT * FROM systypes WHERE name = N'u001')
EXEC dbo.sp_addtype N'u001', N'float',N'not null'

GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SearchSystemLog]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SearchSystemLog]
--==================================================
--作者：Tony
--时间：2012-11-24
--功能：1）根据查询条件获取到系统日志
--==================================================
(
	@LogType NVARCHAR(50),
	@TimeScope NVARCHAR(50)
)
AS
BEGIN
	DECLARE @SQL_WHERE NVARCHAR(200)
	DECLARE @SQL NVARCHAR(500)
	
	SET @SQL_WHERE = ''''
	SET @SQL = ''SELECT * FROM dbo.SystemLog ''
	
	PRINT @SQL
	
	IF @TimeScope = ''当天''
		SET @SQL_WHERE = @SQL_WHERE + '' DATEDIFF(DAY, OP_DATE, GETDATE()) = 0''
	ELSE IF @TimeScope = ''近一周''
		SET @SQL_WHERE = @SQL_WHERE + '' DATEDIFF(DAY, OP_DATE, GETDATE()) < 7''
	ELSE IF @TimeScope = ''近一月''
		SET @SQL_WHERE = @SQL_WHERE + '' DATEDIFF(DAY, OP_DATE, GETDATE()) < 30''
	ELSE IF @TimeScope = ''近半年''
		SET @SQL_WHERE = @SQL_WHERE + '' DATEDIFF(DAY, OP_DATE, GETDATE()) < 180''
	ELSE
		SET @SQL_WHERE = @SQL_WHERE + '' DATEDIFF(DAY, OP_DATE, GETDATE()) < 360''
	
	IF @LogType <> ''所有日志''
		SET @SQL_WHERE = @SQL_WHERE + '' AND OP_TYPE = '''''' +  @LogType + ''''''''

	PRINT @SQL_WHERE
	
	SET @SQL = @SQL + '' WHERE '' + @SQL_WHERE + '' ORDER BY OP_DATE DESC ''
	PRINT @SQL
	
	EXEC sys.sp_executesql @SQL
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SystemLog]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[SystemLog](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemLog_OID]  DEFAULT (''),
	[SA_OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemLog_SA_OID]  DEFAULT (''),
	[SA_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemLog_SA_NAME]  DEFAULT (''),
	[OP_DATE] [datetime] NOT NULL CONSTRAINT [DF_SystemLog_OP_DATE]  DEFAULT (getdate()),
	[OP_TYPE] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemLog_OP_TYPE]  DEFAULT (''),
	[OP_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemLog_OP_NAME]  DEFAULT (''),
	[OP_DESC] [nvarchar](200) NOT NULL CONSTRAINT [DF_SystemLog_OP_DESC]  DEFAULT (''),
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SystemAdmin]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[SystemAdmin](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemAdmin_OID]  DEFAULT (''),
	[LOG_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemAdmin_LOG_NAME]  DEFAULT (''),
	[PAS_WORD] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemAdmin_PAS_WORD]  DEFAULT (''),
	[ROL_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemAdmin_ROL_NAME]  DEFAULT (''),
	[CRE_DATE] [datetime] NOT NULL CONSTRAINT [DF_SystemAdmin_CRE_DATE]  DEFAULT (getdate()),
	[YG_OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemAdmin_YG_OID]  DEFAULT (''),
 CONSTRAINT [PK_SystemAdmin] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[YouthGroup]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[YouthGroup](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_YouthGroup_OID]  DEFAULT (''),
	[YG_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_YouthGroup_YG_NAME]  DEFAULT (''),
	[YG_LEVEL] [int] NOT NULL CONSTRAINT [DF_YouthGroup_YG_LEVEL]  DEFAULT ((0)),
	[PARENT_OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_YouthGroup_PARENT_OID]  DEFAULT (''),
	[CRE_DATE] [datetime] NOT NULL CONSTRAINT [DF_YouthGroup_CRE_DATE]  DEFAULT (((1000)-(1))-(1)),
	[YG_ORDER] [int] NOT NULL CONSTRAINT [DF__YouthGrou__YG_OR__023D5A04]  DEFAULT ((0)),
 CONSTRAINT [PK_YouthGroup] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Member]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Member](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__OID__5BE2A6F2]  DEFAULT (''),
	[FK_YouthGroup] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__FK_Youth__5CD6CB2B]  DEFAULT (''),
	[Dept] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__Dept__5DCAEF64]  DEFAULT (''),
	[WorkGroup] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__WorkGrou__5EBF139D]  DEFAULT (''),
	[Post] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__Post__5FB337D6]  DEFAULT (''),
	[HrCode] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__HrCode__60A75C0F]  DEFAULT (''),
	[HrType] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__HrType__6D0D32F4]  DEFAULT (''),
	[HrStatus] [nvarchar](20) NOT NULL CONSTRAINT [DF_Member_HrStatus]  DEFAULT (''),
	[EmpName] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__EmpName__619B8048]  DEFAULT (''),
	[EmpID] [nvarchar](30) NOT NULL CONSTRAINT [DF__Member__EmpID__6383C8BA]  DEFAULT (''),
	[Sex] [nvarchar](2) NOT NULL CONSTRAINT [DF__Member__Sex__628FA481]  DEFAULT (''),
	[Birthday] [datetime] NOT NULL CONSTRAINT [DF__Member__Birthday__6477ECF3]  DEFAULT ('1900-1-1'),
	[Age] [int] NOT NULL CONSTRAINT [DF__Member__Age__656C112C]  DEFAULT ((0)),
	[ParttimeName] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__Parttime__66603565]  DEFAULT (''),
	[JobDateTime] [datetime] NOT NULL CONSTRAINT [DF__Member__JobDateT__6754599E]  DEFAULT ('1900-1-1'),
	[ComDateTime] [datetime] NOT NULL CONSTRAINT [DF__Member__ComDateT__68487DD7]  DEFAULT ('1900-1-1'),
	[Wedding] [nvarchar](10) NOT NULL CONSTRAINT [DF__Member__Wedding__693CA210]  DEFAULT (''),
	[NativePlace] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__NativePl__6A30C649]  DEFAULT (''),
	[Politics] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__Politics__6B24EA82]  DEFAULT (''),
	[PartyDate] [datetime] NOT NULL CONSTRAINT [DF__Member__PartyDat__6C190EBB]  DEFAULT ('1900-1-1'),
	[Nation] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__Nation__6E01572D]  DEFAULT (''),
	[House] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__House__6EF57B66]  DEFAULT (''),
	[SkillLevel] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__SkillLev__6FE99F9F]  DEFAULT (''),
	[FstSchoolExp] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__FstSchoo__70DDC3D8]  DEFAULT (''),
	[FstDegree] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__FstDegre__71D1E811]  DEFAULT (''),
	[LstSchoolExp] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__LstSchoo__72C60C4A]  DEFAULT (''),
	[LstDegree] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__LstDegre__73BA3083]  DEFAULT (''),
	[FstSchool] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__FstSchoo__74AE54BC]  DEFAULT (''),
	[FstProfession] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__FstProfe__75A278F5]  DEFAULT (''),
	[FstGraduateDate] [datetime] NOT NULL CONSTRAINT [DF__Member__FstGradu__76969D2E]  DEFAULT ('1900-1-1'),
	[LstSchool] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__LstSchoo__778AC167]  DEFAULT (''),
	[LstProfession] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__LstProfe__787EE5A0]  DEFAULT (''),
	[LstGraduateDate] [datetime] NOT NULL CONSTRAINT [DF__Member__LstGradu__797309D9]  DEFAULT (''),
	[ApplyParty] [nvarchar](2) NOT NULL CONSTRAINT [DF__Member__ApplyPar__7A672E12]  DEFAULT (''),
	[ApplyPartyDate] [datetime] NOT NULL CONSTRAINT [DF__Member__ApplyPar__7B5B524B]  DEFAULT ('1900-1-1'),
	[VolunteerInfo] [nvarchar](100) NOT NULL CONSTRAINT [DF__Member__Voluntee__7C4F7684]  DEFAULT (''),
	[Mobile] [nvarchar](20) NOT NULL CONSTRAINT [DF__Member__Mobile__7D439ABD]  DEFAULT (''),
	[Email] [nvarchar](50) NOT NULL CONSTRAINT [DF__Member__Email__7E37BEF6]  DEFAULT (''),
	[SpecialSkill] [nvarchar](200) NOT NULL CONSTRAINT [DF__Member__SpecialS__7F2BE32F]  DEFAULT (''),
	[YouthChargeStd] [int] NOT NULL CONSTRAINT [DF__Member__YouthCha__00200768]  DEFAULT ((0)),
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Post]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Post](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_Post_OID]  DEFAULT (''),
	[CRE_DATE] [datetime] NOT NULL CONSTRAINT [DF_Post_CRE_DATE]  DEFAULT (getdate()),
	[CAT_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_Post_CAT_NAME]  DEFAULT (''),
	[POST_NAME] [nvarchar](100) NOT NULL CONSTRAINT [DF_Post_POST_NAME]  DEFAULT (''),
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Organize]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Organize](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_Organize_OID]  DEFAULT (''),
	[CRE_DATE] [datetime] NOT NULL CONSTRAINT [DF_Organize_CRE_DATE]  DEFAULT (getdate()),
	[OG_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_Organize_OG_NAME]  DEFAULT (''),
	[OG_LEVEL] [int] NOT NULL CONSTRAINT [DF_Organize_OG_LEVEL]  DEFAULT ((0)),
	[OG_ORDER] [int] NOT NULL CONSTRAINT [DF_Organize_OG_ORDER]  DEFAULT ((0)),
	[PARENT_OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_Organize_PARENT_OID]  DEFAULT (''),
 CONSTRAINT [PK_Organize] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[StandardEnum]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[StandardEnum](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_StandardEnum_OID]  DEFAULT (''),
	[SE_TYPE] [nvarchar](50) NOT NULL CONSTRAINT [DF_StandardEnum_SE_TYPE]  DEFAULT (''),
	[SE_KEY] [nvarchar](50) NOT NULL CONSTRAINT [DF_StandardEnum_SE_KEY]  DEFAULT (''),
	[SE_VALUE] [nvarchar](50) NOT NULL CONSTRAINT [DF_StandardEnum_SE_VALUE]  DEFAULT (''),
	[SE_ORDER] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_StandardEnum] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[_BAK]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[_BAK](
	[C1] [nvarchar](50) NULL,
	[C2] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[_BAK2]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[_BAK2](
	[C1] [nvarchar](50) NULL,
	[C2] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[MemberCharge]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[MemberCharge](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberCharge_OID]  DEFAULT (''),
	[YG_OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberCharge_YG_OID]  DEFAULT (''),
	[MC_CODE] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberCharge_MC_CODE]  DEFAULT (''),
	[YG_MEMBER] [int] NOT NULL CONSTRAINT [DF_MemberCharge_YG_MEMBER]  DEFAULT ((0)),
	[YG_PARTY] [int] NOT NULL,
	[YJ_1] [int] NOT NULL CONSTRAINT [DF_MemberCharge_YJ_1]  DEFAULT ((0)),
	[YJ_2] [int] NOT NULL CONSTRAINT [DF_MemberCharge_YJ_2]  DEFAULT ((0)),
	[YJ_3] [int] NOT NULL CONSTRAINT [DF_MemberCharge_YJ_3]  DEFAULT ((0)),
	[BJ_1] [int] NOT NULL,
	[BJ_2] [int] NOT NULL CONSTRAINT [DF_MemberCharge_BJ_2]  DEFAULT ((0)),
	[BJ_3] [int] NOT NULL CONSTRAINT [DF_MemberCharge_BJ_3]  DEFAULT ((0)),
	[HJ_NUM] [int] NOT NULL CONSTRAINT [DF_MemberCharge_HJ_NUM]  DEFAULT ((0)),
	[HJ_ZW] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberCharge_HJ_ZW]  DEFAULT (''),
	[MC_DATE] [datetime] NOT NULL CONSTRAINT [DF_MemberCharge_MC_DATE]  DEFAULT (getdate()),
	[MC_JKR] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberCharge_MC_JKR]  DEFAULT (''),
	[MC_SKR] [nvarchar](50) NOT NULL,
	[CRE_DATE] [datetime] NOT NULL CONSTRAINT [DF_MemberCharge_CRE_DATE]  DEFAULT (getdate()),
 CONSTRAINT [PK_MemberCharge] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Prise]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[Prise](
	[OID] [nvarchar](50) NOT NULL DEFAULT (''),
	[FK_Member] [nvarchar](50) NOT NULL DEFAULT (''),
	[PR_NAME] [nvarchar](100) NOT NULL DEFAULT (''),
	[PR_UNIT] [nvarchar](100) NOT NULL DEFAULT (''),
	[PR_DATE] [datetime] NOT NULL DEFAULT ('1900-1-1'),
	[PR_VALUE] [numeric](10, 2) NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Prise] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[NewYouthGroup]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[NewYouthGroup]
--==================================================
--作者：Tony
--时间：2012-11-06
--功能：1）增加团支部
--==================================================
(
	@YG_NAME NVARCHAR(50),
	@PARENT_OID NVARCHAR(50)
)
AS
BEGIN
	DECLARE @YG_ORDER INT
	DECLARE @YG_LEVEL INT
	
	SELECT @YG_ORDER = COUNT(*) FROM dbo.YouthGroup WHERE PARENT_OID = @PARENT_OID
	SELECT @YG_LEVEL = YG_LEVEL + 1 FROM dbo.YouthGroup WHERE OID = @PARENT_OID
	
	INSERT dbo.YouthGroup ( OID, YG_NAME, YG_LEVEL, PARENT_OID, CRE_DATE, YG_ORDER )
	VALUES  ( NEWID(), -- OID - nvarchar(50)
	          @YG_NAME, -- YG_NAME - nvarchar(50)
	          @YG_LEVEL, -- YG_LEVEL - int
	          @PARENT_OID, -- PARENT_OID - nvarchar(50)
	          GETDATE(),  -- CRE_DATE - smalldatetime
	          @YG_ORDER
	          )

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[UpdateAdmin]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[UpdateAdmin]
--==================================================
--作者：Tony
--时间：2012-11-07
--功能：1）更新管理员信息
--==================================================
(
	@OID NVARCHAR(50),
	@LOG_NAME NVARCHAR(50),
	@PAS_WORD NVARCHAR(50),
	@ROL_NAME NVARCHAR(50),
	@YG_OID NVARCHAR(50)
)
AS
BEGIN
	IF @PAS_WORD = ''PAS_WORD_TONY''
		UPDATE dbo.SystemAdmin SET LOG_NAME = @LOG_NAME
		,ROL_NAME = @ROL_NAME
		,YG_OID = @YG_OID
		WHERE OID = @OID
	ELSE
		UPDATE dbo.SystemAdmin SET LOG_NAME = @LOG_NAME
		,PAS_WORD = @PAS_WORD
		,ROL_NAME = @ROL_NAME
		,YG_OID = @YG_OID
		WHERE OID = @OID		
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GetAge]') AND xtype in (N'FN', N'IF', N'TF'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetAge](
--========================================================
--作者：Tony
--时间：2012-12-09
--功能：实现年龄(虚岁)计算
--========================================================
	@Birthday DateTime
)
RETURNS INT
AS
BEGIN
	DECLARE @Age INT
	DECLARE @Day INT
	
	SET @Age = DATEDIFF(year, @Birthday, GETDATE())
	SET @Day = DATEDIFF(day, DATEADD(year, @Age, @Birthday), GETDATE())
	
		
	IF @Age < 0
		RETURN @Age
	ELSE
	IF(@Day > 0)
		SET @Age = @Age + 1
	
	RETURN @Age
END	' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FormateMoney]') AND xtype in (N'FN', N'IF', N'TF'))
BEGIN
execute dbo.sp_executesql @statement = N'


CREATE FUNCTION [dbo].[FormateMoney] (@num numeric(14,2))    
     
--==========================================================
--作者：Tony
--时间：2012-12-10
--功能：实现数字到大写中文的装换
--==========================================================
  
RETURNS nvarchar(100)    
    
AS
BEGIN           
      
    DECLARE @n_data nVARCHAR(20),@c_data nVARCHAR(100),@n_str nVARCHAR(10),@i int           
    
    SET @n_data=RIGHT(SPACE(14)+CAST(CAST(ABS(@num*100) AS bigint) AS nvarchar(20)),14)           
    SET @c_data= ''''           
    SET @i=1           
    WHILE @i <=14           
    BEGIN           
            SET @n_str=SUBSTRING(@n_data,@i,1)           
            IF @n_str <> ''''           
            BEGIN           
                    IF not ((SUBSTRING(@n_data,@i,2)= ''00'') or           
                            ((@n_str= ''0'') and ((@i=4) or (@i=8) or (@i=12) or (@i=14))))           
                            SET @c_data=@c_data+SUBSTRING( N''零壹贰叁肆伍陆柒捌玖'',CAST(@n_str AS int)+1,1)           
                    IF not ((@n_str= ''0'') and (@i <> 4) and (@i <> 8) and (@i <> 12))           
                            SET @c_data=@c_data+SUBSTRING( N''仟佰拾亿仟佰拾万仟佰拾圆角分'',@i,1)           
                    IF SUBSTRING(@c_data,LEN(@c_data)-1,2)= N''亿万''           
                            SET @c_data=SUBSTRING(@c_data,1,LEN(@c_data)-1)           
            END           
            SET @i=@i+1           
    END           
    IF @num <0           
            SET @c_data= ''（负数）''+@c_data           
    IF @num=0           
            SET @c_data= ''零圆''           
    IF @n_str= ''0''           
            SET @c_data=@c_data+ ''元整''           
    RETURN(@c_data)           
END
' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[MemberStatic]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[MemberStatic](
	[OID] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberStatic_OID]  DEFAULT (''),
	[MS_YEAR] [int] NOT NULL CONSTRAINT [DF_MemberStatic_CRE_DATE]  DEFAULT ((0)),
	[MS_NUM] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MS_NUM]  DEFAULT ((0)),
	[YG_NAME] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberStatic_YG_NAME]  DEFAULT (''),
	[MM_MALE] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MEM_MALE]  DEFAULT ((0)),
	[MM_FEMALE] [int] NOT NULL CONSTRAINT [DF_MemberStatic_ME_FEMALE]  DEFAULT ((0)),
	[MM_SUM] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_SUM]  DEFAULT ((0)),
	[MM_1428_MALE] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_1428_MALE]  DEFAULT ((0)),
	[MM_1428_FEMALE] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_1428_FEMALE]  DEFAULT ((0)),
	[MM_1428_SUM] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_1428_SUM]  DEFAULT ((0)),
	[MM_35_MALE] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_35_MALE]  DEFAULT ((0)),
	[MM_35_FEMALE] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_35_FEMALE]  DEFAULT ((0)),
	[MM_35_SUM] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_35_SUM]  DEFAULT ((0)),
	[MM_YEAR] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_YEAR]  DEFAULT ((0)),
	[MM_PARTY] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_PARTY]  DEFAULT ((0)),
	[PARTY_APP] [int] NOT NULL CONSTRAINT [DF_MemberStatic_PARTY_APP]  DEFAULT ((0)),
	[MM_PARTY_APP] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_PARTY_APP]  DEFAULT ((0)),
	[MM_CL] [int] NOT NULL CONSTRAINT [DF_MemberStatic_MM_CL]  DEFAULT ((0)),
	[MS_REMARK] [nvarchar](50) NOT NULL CONSTRAINT [DF_MemberStatic_MS_REMARK]  DEFAULT (''),
 CONSTRAINT [PK_MemberStatic] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CreateMemberCharge]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[CreateMemberCharge]
--==================================================
--作者：Tony
--时间：2012-12-09
--功能：1）根据支部名称和缴费季度获取支部的缴费数据
--==================================================
(
	@YG_NAME NVARCHAR(50),
	@MC_CODE NVARCHAR(50),
	@BJ_1 INT,
	@BJ_2 INT,
	@BJ_3 INT
)
AS
BEGIN
	DECLARE @YG_OID NVARCHAR(50)
	DECLARE @YJ_1 INT
	DECLARE @YJ_2 INT
	DECLARE @YJ_3 INT
	DECLARE @YG_PARTY INT
	DECLARE @HJ_NUM INT
	DECLARE @HJ_ZW NVARCHAR(50)
	
	--STEP.1.获取到支部OID
	SELECT @YG_OID = OID FROM dbo.YouthGroup WHERE YG_NAME = @YG_NAME
	
	--STEP.2.取到在岗和外借的共青团员
	SELECT * INTO #Member FROM dbo.Member WHERE FK_YouthGroup = @YG_OID
	AND Politics = ''共青团员'' AND ( HrType=''在岗'' OR HrType=''外借'' ) 
	
	--STEP.3.获取各类团员人数
	SELECT @YJ_1 = COUNT(*) FROM #Member WHERE HrStatus IN (''在册员工'', ''委派员工'', ''商借员工'')
	SELECT @YJ_2 = COUNT(*) FROM #Member WHERE HrStatus IN (''准员工'', ''协力工'')
	SELECT @YJ_3 = COUNT(*) FROM #Member WHERE HrStatus = ''劳务工''
	
	--STEP.4.获取党内团员数
	SELECT @YG_PARTY = COUNT(*) FROM dbo.Member WHERE FK_YouthGroup = @YG_OID
	AND Politics IN( ''中共党员'', ''中共预备党员'') AND ( HrType=''在岗'' OR HrType=''外借'' ) AND dbo.GetAge(Birthday) < 29
	
	SELECT @YG_PARTY = @YG_PARTY + COUNT(*) FROM dbo.Member WHERE FK_YouthGroup = @YG_OID
	AND Politics IN( ''中共党员'', ''中共预备党员'') AND ( HrType=''在岗'' OR HrType=''外借'' )
	AND ParttimeName IN( ''团委副书记'', ''团委委员'', ''团支部书记'', ''团支部委员'') AND dbo.GetAge(Birthday) >= 29
	
	--STEP.5.计算合计金额
	SET @HJ_NUM = (@YJ_1 + @BJ_1) * 10 + (@YJ_2 + @BJ_2) * 8 + (@YJ_3 + @BJ_3) * 3
	SET @HJ_ZW = dbo.FormateMoney(@HJ_NUM) + '' ( ￥'' + CAST(@HJ_NUM AS NVARCHAR(50)) + '' ) ''
	
	--STEP.6.存储计算结果
	DELETE dbo.MemberCharge WHERE YG_OID = @YG_NAME AND MC_CODE = @MC_CODE
	INSERT dbo.MemberCharge ( OID, YG_OID, MC_CODE, YG_MEMBER, YG_PARTY,
	                                YJ_1, YJ_2, YJ_3, BJ_1, BJ_2, BJ_3, HJ_NUM,
	                                HJ_ZW, MC_DATE, MC_JKR, MC_SKR, CRE_DATE )
	VALUES  ( NEWID(), -- OID - nvarchar(50)
	          @YG_NAME, -- YG_OID - nvarchar(50)
	          @MC_CODE, -- MC_CODE - nvarchar(50)
	          @YG_PARTY + @YJ_1 + @YJ_2 + @YJ_3, -- YG_MEMBER - int
	          @YG_PARTY, -- YG_PARTY - int
	          @YJ_1, -- YJ_1 - int
	          @YJ_2, -- YJ_2 - int
	          @YJ_3, -- YJ_3 - int
	          @BJ_1, -- BJ_1 - int
	          @BJ_2, -- BJ_2 - int
	          @BJ_3, -- BJ_3 - int
	          @HJ_NUM, -- HJ_NUM - int
	          @HJ_ZW, -- HJ_ZW - nvarchar(50)
	          GETDATE(), -- MC_DATE - datetime
	          '''', -- MC_JKR - nvarchar(50)
	          '''', -- MC_SKR - nvarchar(50)
	          GETDATE()  -- CRE_DATE - datetime
	          )
	          
	--STEP.7.返回计算好的数据          
	SELECT * FROM dbo.MemberCharge WHERE YG_OID = @YG_NAME AND MC_CODE = @MC_CODE          
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[NewOrganize]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[NewOrganize]
--==================================================
--作者：Tony
--时间：2012-12-03
--功能：1）增加行政机构
--==================================================
(
	@OG_NAME NVARCHAR(50),
	@PARENT_OID NVARCHAR(50)
)
AS
BEGIN
	DECLARE @OG_ORDER INT
	DECLARE @OG_LEVEL INT
	
	SELECT @OG_ORDER = COUNT(*) FROM dbo.Organize WHERE PARENT_OID = @PARENT_OID
	SELECT @OG_LEVEL = OG_LEVEL + 1 FROM dbo.Organize WHERE OID = @PARENT_OID
	          
	INSERT dbo.Organize ( OID, CRE_DATE, OG_NAME, OG_LEVEL, OG_ORDER,
	                            PARENT_OID )
	VALUES  ( NEWID(), -- OID - nvarchar(50)
	          GETDATE(), -- CRE_DATE - datetime
	          @OG_NAME, -- OG_NAME - nvarchar(50)
	          @OG_LEVEL, -- OG_LEVEL - int
	          @OG_ORDER, -- OG_ORDER - int
	          @PARENT_OID  -- PARENT_OID - nvarchar(50)
	          )  

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CreateMemberStatic]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[CreateMemberStatic]
--==================================================
--作者：Tony
--时间：2012-12-11
--功能：1）年份创建团员数据统计表
--==================================================
(
	@MS_YEAR INT
)
AS
BEGIN
	SELECT * FROM dbo.MemberStatic WHERE MS_YEAR = @MS_YEAR   
END' 
END
