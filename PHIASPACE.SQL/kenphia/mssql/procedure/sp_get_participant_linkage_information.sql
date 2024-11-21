USE [Aims]
GO
/****** Object:  StoredProcedure [linkage].[sp_get_participant_linkage_information]    Script Date: 17/09/2024 10:21:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

ALTER PROCEDURE [linkage].[sp_get_participant_linkage_information] 
	@ParticipantId nvarchar(20) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	Select
		lab_level.lea, lab_level.lnum, lab_level.lline, lab_level.liteam, lab_level.lintverv,
		lab_identify.lbagey, lab_identify.lsex,
		lab_identify.lptid, lab_identify.lhivf as 'hiv_result', datefromparts(lab_identify.lsyear, lab_identify.lsmonth, lab_identify.lsday) as 'test_date',
		pima.ResultDate as 'cd4_test_date', pima.ResultValue as 'cd4_test_result'
		--lab_identify.hbfresult as 'hep_b_result', lab_identify.sfresult as 'syphilis_result'
	From [capi_lab].[dbo].[level_1] lab_level
	JOIN [capi_lab].[dbo].[identify] lab_identify On lab_level.level_1_id = lab_identify.level_1_id
	JOIN [DNAS].[dbo].[PIMAraw] pima On pima.[Sample] = lab_identify.lptid
	Where lptid = @ParticipantId and lab_identify.lhivf = 2
END
