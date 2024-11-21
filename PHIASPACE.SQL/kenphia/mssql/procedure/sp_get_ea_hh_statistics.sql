USE [Aims]
GO
/****** Object:  StoredProcedure [monitor].[get_ea_hh_statistics]    Script Date: 04/10/2024 12:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [monitor].[sp_get_ea_hh_statistics] 
    @cluster_number INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        samp_rec_2.xnumber AS 'HHNumber',
        samp_rec_2.xname AS 'HHName',
        COUNT(DISTINCT CASE WHEN hsec01x.hdel <> 1 THEN hsec01x.hline ELSE NULL END) AS 'Rostered',
        COUNT(DISTINCT CASE WHEN hsec01x.hdel <> 1 THEN hsec01x.hline ELSE NULL END) AS 'Eligible', 
        0 AS 'ConsentedInterview',
        0 AS 'RefusedInterview',
        0 AS 'Interviewed',
        0 AS 'PTIDs',
        0 AS 'ConsentedBloodDraw',
        0 AS 'BloodDrawn',
        0 AS 'ConsentedToBeContacted',
        0 AS 'ContactInfoAvailable',
        0 AS 'ChildreEligible',
        0 AS 'ChidrenWithBloodDraw',
        '2024/09/01' AS 'DateOpened',
        GETDATE() AS 'Date Uploaded'
    FROM [capi_sampleselection].[dbo].[level_1] samp_sel_level
    JOIN [capi_sampleselection].[dbo].[samrec2] samp_rec_2 ON samp_sel_level.level_1_id = samp_rec_2.level_1_id
    JOIN [capi_hh].[dbo].[level-1] hh_level ON hh_level.hea = samp_sel_level.xcluster AND hh_level.hnum = samp_rec_2.xnumber
    JOIN [capi_hh].[dbo].[hsec01x] hsec01x ON hsec01x.level_1_id = hh_level.[level-1-id]
    CROSS APPLY [dbo].[get_ea_details_table](samp_sel_level.xcluster) ea_details 
    WHERE 
        (@cluster_number <= 0 OR samp_sel_level.xcluster = @cluster_number)
    GROUP BY 
        samp_sel_level.xcluster, samp_rec_2.xnumber, samp_rec_2.xname;
END
