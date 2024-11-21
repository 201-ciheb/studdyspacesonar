USE [Aims]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [report].[sp_team_summary]

AS
BEGIN

	SET NOCOUNT ON;

    SELECT 
		concat(hh_level.hteamnum, ' - ', hh_level.hea) as Teams,
		hh_level.hea as [EAs Visited],
		count(distinct hh_level.hnum) [Households]
		,count(distinct case when hsecover.hresult = 1 then hh_level.hnum else null end) as [Completed Household]
		,count(distinct case when hsecover.hresult = 6 then hh_level.hnum else null end) as [Vacant household]
		,count(distinct case when hsecover.hresult = 9 then hh_level.hnum else null end) as [Household not found] --notfound-9
		,count(distinct case when hsecover.hresult = 8 then hh_level.hnum else null end) as [Household Destroyed] --destroyed=8
		,count(distinct case when hsecover.hresult = 5 then hh_level.hnum else null end) as [Household Refused]
		,count(distinct case when hsecover.hresult not in (1, 6, 9, 8, 5) then hh_level.hnum else null end) as [Household Others] --other=99
		,count(distinct case when hsec01.h0005 = 1 then hh_level.hnum else null end) as [No who lives in the house]
		,count(distinct case when hsec01.h0006 = 1 then hh_level.hnum else null end) as [No slept in the house]
		,count(distinct case when (hsec01.h0010 > 0 or hsec01.h0011 > 0) and (hsec01.hageswitch = 0 or hsec01.hageswitch is null) then hh_level.hnum else null end) as [No of eligible participants]
		,count(distinct case when (hsec01.h0010 = 0 or hsec01.h0010 is null or hsec01.h0011 = 0 or hsec01.h0011 is null) and (hsec01.hageswitch = 3) then hh_level.hnum else null end) as [No of ineligible participants]
		,count(distinct case when hsecover.hconsent = 1 then hh_level.hnum else null end) as [No of consents]
		,count(distinct case when (ieligibility.ip015 = 1 or ieligibility.im013 = 1 or ieligibility.ia012 = 1) then concat(ind_level.inum,ind_level.iline) else null end) as [Individuals Eligible for Interview] --I assume all is eligible as I have not found a variable to use for this
		,count(distinct case when isecover.isecover_id is not null then concat(ind_level.inum,ind_level.iline) else null end) as [Individuals interviewed]
		,count(distinct case when isecover.iresult = 1 then concat(ind_level.inum,ind_level.iline) else null end) as [Individual interviews completed]
		,count(distinct case when isecover.iresult = 3 then concat(ind_level.inum,ind_level.iline) else null end) as [Individual interviews Postponed]
		,count(distinct case when isecover.iresult = 4 then concat(ind_level.inum,ind_level.iline) else null end) as [Individual interviews Refused]
		,count(distinct case when isecover.iresult not in (1, 3, 4) then concat(ind_level.inum,ind_level.iline) else null end) as [Individual interviews other reasons]
	  FROM [capi_hh].[dbo].[level-1] hh_level 
	JOIN [capi_hh].[dbo].[hsecover] hsecover ON hsecover.[level-1-id] = hh_level.[level-1-id]
	LEFT JOIN [capi_hh].[dbo].[hsec01] hsec01 ON hsec01.level_1_id = hh_level.[level-1-id]
	LEFT JOIN [capi_ind].[dbo].[level-1] ind_level ON ind_level.iea = hh_level.hea and ind_level.iateam =  hh_level.hteamnum
	LEFT JOIN [capi_ind].[dbo].[isecover] isecover ON isecover.level_1_id = ind_level.[level-1-id]
	LEFT JOIN [capi_ind].[dbo].[ieligibility] ieligibility ON ieligibility.level_1_id = ind_level.[level-1-id]
	group by
	hh_level.hteamnum, hh_level.hea

END
