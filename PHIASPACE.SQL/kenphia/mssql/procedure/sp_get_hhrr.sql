USE [Aims]
GO
/****** Object:  StoredProcedure [report].[sp_get_hhrr]    Script Date: 27/08/2024 08:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [report].[sp_get_hhrr]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		cluster.yregionn as [Region],
		(case when max(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)) > '2024-08-01' -- GETDATE() this is used for now and would be passed as a parameter with the survey date
		then 'Active' else 'Inactive' end) as [Status],
		min(datefromparts(hsecover.hvyear_1, hsecover.hvmonth_1, hsecover.hvday_1)) as [Date Started],
		count(cluster_level.ycluster) as [Expected EAs],
		count(hh_level.hea) as [Eas Visited],
		sum(samrec.xtotal) as Households_Sampled,
		sum(case when concat(hh_level.hea, hh_level.hnum) != concat(hh_level.hea, '') then 1 else 0 end) as Households_Visited, -- this is basically counting 
		--dintinct hh where ea,hh is not ea only, this is becuase groupby is by region
		count(distinct case when hsecover.hconsent = 1 then concat(hh_level.hea, hh_level.hnum) else null end) as Households_Consented,
		count(distinct case when hsecover.hresult = 5 then hh_level.hnum else null end) as Households_Refused,
		count(distinct case when hsecover.hresult = 2 then hh_level.hnum else null end) as Households_Not_At_Home,
		count(distinct case when hsecover.hresult = 3 then hh_level.hnum else null end) as Households_Absent_For_Long,
		count(distinct case when hsecover.hresult = 4 then hh_level.hnum else null end) as Households_Postponed,
		count(distinct case when hsecover.hresult = 6 then hh_level.hnum else null end) as Households_Vacant,
		count(distinct case when hsecover.hresult = 8 then hh_level.hnum else null end) as Households_Destroyed, --destroyed=8
		count(distinct case when hsecover.hresult = 9 then hh_level.hnum else null end) as Households_Not_Found, --notfound-9
		count(distinct case when hsecover.hresult in (99) then hh_level.hnum else null end) as Households_Others, --other=99
		0 as Eligible_15_64,
		0 as Consented_15_64,
		0 as Completed_Intv_15_64,
		0 as Consented_For_Draw_15_64,
		0 as Draws_15_64

		--,hsecover.*
	  FROM [capi_hh].[dbo].[level-1] hh_level 
		JOIN [capi_hh].[dbo].[hsecover] hsecover ON hsecover.[level-1-id] = hh_level.[level-1-id]
		RIGHT JOIN [capi_cluster].[dbo].[level_1] cluster_level ON cluster_level.ycluster = hh_level.hea -- Right join was used to include all clusters in the EA
		JOIN [capi_cluster].[dbo].[clstrec] cluster ON cluster.level_1_id = cluster_level.level_1_id
		RIGHT JOIN [capi_sampleselection].[dbo].[level_1] samsel ON samsel.xcluster = cluster_level.ycluster
		JOIN [capi_sampleselection].[dbo].[samrec1] samrec ON samrec.level_1_id = samsel.level_1_id -- Right join was used to include all sample selections

	group by cluster.yregionn

	order by cluster.yregionn
END
