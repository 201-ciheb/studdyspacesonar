USE [Aims]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [indicator].[sp_get_hh_eligibility_indicator_summary] 
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
    JOIN [capi_hh].[dbo].[heligility] heligility 
    ON heligility.[level_1_id] = hh_level.[level-1-id];';

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql;

    -- Another dynamic query if needed, using the same parameter
    SET @sql = '
    SELECT ea_details.yregionn as [Region], ' + @selectVariables + '
    FROM [capi_hh].[dbo].[level-1] hh_level 
    JOIN [capi_hh].[dbo].[heligility] heligility 
    ON heligility.[level_1_id] = hh_level.[level-1-id]
    CROSS APPLY dbo.get_ea_details_table(hh_level.hea) ea_details
    GROUP BY ea_details.yregionn;';  

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql;
END
