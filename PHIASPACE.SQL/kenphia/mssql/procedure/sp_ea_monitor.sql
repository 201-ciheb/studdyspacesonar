-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [report].[sp_ea_monitor] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @start_date nvarchar(15) = '2024-08-01'
	
	Select 
		ea_details.ycluster, ea_details.yregionn, ea_details.ydistrictn,
		(case when max(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)) > @start_date -- GETDATE() this is used for now and would be passed as a parameter with the survey date
			then 'Active' else 'Inactive' end) as [Status],
		min(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)) as [Date Started],
		'' as [Date Completed field], '' as [Date completed DM],
		datediff(day, min(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)), 
			max(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1))) as [Duration (Days)],
		sum(cluster.ytothh) as [HH Selected],
		sum(case when concat(hh_level.hea, hh_level.hnum) != concat(hh_level.hea, '') then 1 else 0 end) as Households_Visited, -- this is basically counting 
		count(distinct case when hsecover.hresult = 1 then hh_level.hnum else null end) as [Completed Household],
		count(distinct case when hsecover.hresult = 4 then hh_level.hnum else null end) as [Households Postponed],
		count(distinct case when hsecover.hresult = 5 then hh_level.hnum else null end) as [Household Refused],
		count(distinct case when hsecover.hresult not in (1, 5, 4) then hh_level.hnum else null end) as [Household Others],
		sum(cluster.ytothh) - sum(case when concat(hh_level.hea, hh_level.hnum) != concat(hh_level.hea, '') then 1 else 0 end) as [HH Incomplete],
		cast(round((sum(case when concat(hh_level.hea, hh_level.hnum) != concat(hh_level.hea, '') then 1 else 0 end) * 100.0) / nullif(sum(cluster.ytothh), 0), 2) as decimal(10,2)) as [% Completion],
		0 as [HH Status],
		0 as [15-64 Eligible for Interview]
		  ,0 as [15-64 interviews completed]
		  ,0 as [15-64 interviews Postponed]
		  ,0 as [15-64 interviews Refused]
		  ,0 as [15-64 interviews other reasons]
		  ,0 as [15-64 incomplete interviews]
		  ,0 as [15-64 Interview Completed?]

		  ,0 as [15-64 consented to blood draw]
		  ,0 as [15-64 blood draw count]
		  ,0 as [15-64 refused blood draw]
		  ,0 as [10-14 Eligible for Interview]
		  ,0 as [10-14 interviews completed]
		  ,0 as [10-14 interviews Postponed]
		  ,0 as [10-14 interviews Refused]
		  ,0 as [10-14 interviews other reasons]
		,0 as [10-14 incomplete interviews]
		,0 as [Adolescent Interview Completed?]
		,0 as [10-14 consented to blood draw]
		,0 as [10-14 blood draw count]
		,0 as [10-14 refused blood draw]
		,0 as [Children Eligible for blood draw]
		,0 as [Children completed blood draw]
		,0 as [Children refused blood draw],
		 0 as [Children survey Completed?]

	FROM [capi_hh].[dbo].[level-1] hh_level 
			JOIN [capi_hh].[dbo].[hsecover] hsecover ON hsecover.[level-1-id] = hh_level.[level-1-id]
			RIGHT JOIN [capi_cluster].[dbo].[level_1] cluster_level ON cluster_level.ycluster = hh_level.hea
			JOIN [capi_cluster].[dbo].[clstrec] cluster ON cluster.level_1_id = cluster_level.level_1_id
			CROSS APPLY dbo.get_ea_details_table(cluster_level.ycluster) ea_details
	group by ea_details.ycluster, ea_details.yregionn, ea_details.ydistrictn

END
GO
