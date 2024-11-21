USE [Aims]
GO
/****** Object:  StoredProcedure [report].[sp_get_hh_ea_summary]    Script Date: 28/08/2024 08:01:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [report].[sp_get_hh_ea_summary] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @start_date nvarchar(15) = '2024-08-01'

	Select 
		ea_details.yregionn [State],
		min(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)) as [Date Started],
		(case when max(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)) > @start_date -- GETDATE() this is used for now and would be passed as a parameter with the survey date
			then 'Active' else 'Inactive' end) as [Status],
		datediff(day, min(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)), 
			max(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1))) as [Duration (Days)],
		0 as [Rest days],
		count(distinct hh_level.hteamnum) as [No of Teams],
		0 as [Expected Eas],
		0 as [Eas Visited]
	
	   FROM [capi_hh].[dbo].[level-1] hh_level 
		JOIN [capi_hh].[dbo].[hsecover] hsecover ON hsecover.[level-1-id] = hh_level.[level-1-id]
		CROSS APPLY dbo.get_ea_details_table(hh_level.hea) ea_details
	
		GROUP BY
		ea_details.yregionn, hh_level.hea
END
