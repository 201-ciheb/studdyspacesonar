USE [Aims]
GO
/****** Object:  StoredProcedure [monitor].[sp_get_team_ea]    Script Date: 04/10/2024 12:34:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


ALTER PROCEDURE [monitor].[sp_get_team_ea]
	@team_code int = 0,
    @region NVARCHAR(20) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		Select 
			cluster_team.tteam_id 'Team',
			ea_details.ycluster 'Cluster', ea_details.yregionn 'Region', ea_details.ystaten 'Zone', ea_details.ydistrictn 'District', ea_details.ycityn 'City',
			[dbo].[get_duplicate_ptid](cluster_team.tcluster) 'DuplicatePtids',
			hh_details.HouseHolds_Visited 'HouseHoldsVisited',
			hh_details.HouseHolds_Completed 'HouseHoldsCompleted',
			hh_details.HouseHolds_Refused 'HouseHoldsRefused'
			FROM [capi_clusterteam].[dbo].[level_1] cluster_team
			CROSS APPLY dbo.get_ea_details_table(cluster_team.tcluster) ea_details
			CROSS APPLY dbo.[get_ea_household_summary](cluster_team.tcluster) hh_details
			Where (@team_code <= 0 OR cluster_team.tteam_id = @team_code)
			AND (@region = '' OR ea_details.yregionn = @region)

END
