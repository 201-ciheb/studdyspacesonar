using System.Data;

namespace PHIASPACE.RTDMS.DAL.IService
{
    public interface ITeamService
    {
        // //void AddPersonsToTeam(List<aims_lnk_team_person> team_personos);
        // AimsTeam AddTeam(AimsTeam team);
        // void AddTeamToEas(List<AimsLnkTeamEa> eas);
        // void AddTeamToState(AimsLnkTeamState team_State);
        // IQueryable<AimsLnkTeamPerson> GetPersonTeams();
        // AimsTeam GetTeam(int team_id);
        // IQueryable<AimsLnkTeamEa> GetTeamEas();
        // AimsPerson GetTeamLeadEa(int ea_id);
        // IQueryable<AimsTeam> GetTeams();
        // AimsTeam GetTeamState(string state_name, int code);
        // void RemoveEaFromTeam(int link_id);
        // void RemovePersonFromTeam(int person_id, int team_id);
        // void UpdatePersonTeam(AimsLnkTeamPerson person);
        // void UpdateTeam(AimsTeam team);
        // void UpdateTeamEa(AimsLnkTeamEa team_Ea);
        // void UpdateTeamState(AimsLnkTeamState team_State);


        // Task<DataTable> GetTeamEaStatAsync(int eaId);
        // Task<DataSet> GetHhLineAsync(int page, string level, string locationId);
        Task<DataTable> GetEaConfirmationSummaryAsync(int eaId);
    }
}
