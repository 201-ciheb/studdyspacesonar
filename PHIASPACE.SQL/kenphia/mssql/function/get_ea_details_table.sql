USE [Aims]
GO

/****** Object:  UserDefinedFunction [dbo].[get_ea_details_table]    Script Date: 27/08/2024 08:36:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[get_ea_details_table] 
(	
	@ea_code int 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT
		ycluster, yregionn, ystaten, ydistrictn, ycityn
	FROM [capi_cluster].[dbo].[level_1] cluster_level
		JOIN [capi_cluster].[dbo].[clstrec] cluster ON cluster.level_1_id = cluster_level.level_1_id
	WHERE cluster_level.ycluster = @ea_code
)
GO
