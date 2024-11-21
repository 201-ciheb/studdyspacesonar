USE [Aims]
GO
/****** Object:  StoredProcedure [indicator].[sp_get_hh_consent_indicator_summary]    Script Date: 01/09/2024 02:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [indicator].[sp_get_hh_consent_indicator_summary] 
    @selectVariables NVARCHAR(MAX) 
AS
BEGIN
    SET NOCOUNT ON;

    -- Define the dynamic SQL
    DECLARE @sql NVARCHAR(MAX);

    -- Construct the query by concatenating the input parameter
    SET @sql = '
    SELECT ' + @selectVariables + '
    FROM [capi_hh].[dbo].[level-1] hh_level 
    JOIN [capi_hh].[dbo].[hsecover] hsecover 
    ON hsecover.[level-1-id] = hh_level.[level-1-id];';

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql;

    -- Another dynamic query if needed, using the same parameter
    SET @sql = '
    SELECT ea_details.yregionn as [Region], ' + @selectVariables + '
    FROM [capi_hh].[dbo].[level-1] hh_level 
    JOIN [capi_hh].[dbo].[hsecover] hsecover 
    ON hsecover.[level-1-id] = hh_level.[level-1-id]
    CROSS APPLY dbo.get_ea_details_table(hh_level.hea) ea_details
    GROUP BY ea_details.yregionn;';  

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql;
END
