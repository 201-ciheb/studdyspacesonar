USE [Aims]
GO
/****** Object:  StoredProcedure [report].[sp_get_state_performance_trend]    Script Date: 31/08/2024 03:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [report].[sp_get_state_performance_trend]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT count(hh_level.hnum) as [HHs],
	datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1) as [Date],
	ea_details.yregionn as [State] 
	FROM [capi_hh].[dbo].[level-1] hh_level 
		JOIN [capi_hh].[dbo].[hsecover] hsecover ON hsecover.[level-1-id] = hh_level.[level-1-id]
		CROSS APPLY dbo.get_ea_details_table(hh_level.hea) ea_details
		
		GROUP BY
		ea_details.yregionn,
		datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)
--(
--	SELECT count(DATEFROMPARTS([AHINTY],[AHINTM],[AHINTD])) [No of HH],
--	DATEFROMPARTS([AHINTY],[AHINTM],[AHINTD]) as [Date],
--	state_name as [State] 
--	from [dbo].[CAPI_AHSECOVER] ah 
--	join [dbo].[aims_ea] ea on ah.A1QCLUST=ea.id where [AHINTY] <> '-1'  
--	and A1QCLUST in (select id from [dbo].[aims_ea] where 
--	state_id=(select top 1 state_id from [dbo].[aims_ea] where id=ah.A1QCLUST)
--)  
--	group by [AHINTY],[AHINTM],[AHINTD],state_name --order by [AHINTY],[AHINTM],[AHINTD] desc
--	)rs group by [Date],[State] order by Date asc
	
END
