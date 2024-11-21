using System.Collections.Generic;
using AutoMapper;
using PHIASPACE.INTERFACE.DAL.Model.Data;
using PHIASPACE.INTERFACE.Models;
public class PhiaSpaceMapper{
    public List<SatLabAssessment> MapSatLabObj(List<SatLabApiResponse> _tobemapped)
    {
        var config = new MapperConfiguration(m =>
        {
            m.CreateMap<SatLabApiResponse, SatLabAssessment>()
                .ForMember(d => d.FormId, a => a.MapFrom(s => s._id))
                .ForMember(d => d.FormUuid, a => a.MapFrom(s => s.formhubuuid))
                .ForMember(d => d.AssessmentDate, a => a.MapFrom(s => s.Page1assessment_date))
                .ForMember(d => d.EntryDate, a => a.MapFrom(s => s.EntryDate))
                .ForMember(d => d.StartDate, a => a.MapFrom(s => s.start))
                .ForMember(d => d.EndDate, a => a.MapFrom(s => s.end))
                .ForMember(d => d.Country, a => a.MapFrom(s => s.Page1county))
                .ForMember(d => d.SubCounty, a => a.MapFrom(s => s.Page1subcounty))
                .ForMember(d => d.Team, a => a.MapFrom(s => s.Page1team))
                .ForMember(d => d.Interviewer, a => a.MapFrom(s => s.Page1interviewer))
                .ForMember(d => d.Kmfl, a => a.MapFrom(s => s.Page1kmfl))
                .ForMember(d => d.Facility, a => a.MapFrom(s => s.Page1facility))
                .ForMember(d => d.Geocord1, a => a.MapFrom(s => s._geolocation[0]))
                .ForMember(d => d.Geocord2, a => a.MapFrom(s => s._geolocation[1]))
                //.ForMember(d => d.DrawDateTime , a => a.MapFrom(s => s.DRAW_DATE_TIME))
                .ForMember(d => d.Score , a => a.MapFrom(s => s.TotalScore))
                ;
        });
        var mapper = config.CreateMapper();
         return mapper.Map<List<SatLabApiResponse>, List<SatLabAssessment>>(_tobemapped);
    }
}
