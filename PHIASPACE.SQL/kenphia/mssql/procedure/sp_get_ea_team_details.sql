USE [Aims]
GO
/****** Object:  StoredProcedure [monitor].[sp_get_ea_team_details]    Script Date: 04/10/2024 12:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [monitor].[sp_get_ea_team_details]
	-- Add the parameters for the stored procedure here
	@cluster_number int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT user_rec.uname 'Username', user_rec.urole 'Role', '' 'Email', '' 'PhoneNumber', user_rec.usex 'Sex'
	  FROM [capi_clusterteam].[dbo].[level_1] cluster_team_level
	  JOIN [capi_user].[dbo].[user_rec] user_rec ON user_rec.uteam = cluster_team_level.tteam_id
	  where cluster_team_level.tcluster = @cluster_number
END
