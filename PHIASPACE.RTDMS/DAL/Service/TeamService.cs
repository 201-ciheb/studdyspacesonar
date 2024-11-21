using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;
using PHIASPACE.RTDMS.DAL.IService;

namespace PHIASPACE.RTDMS.DAL.Service
{
    public class TeamService : ITeamService
    {

        private readonly IAimsDbProvider _dbProvider;
        private readonly IConfiguration _configuration;


        public TeamService(IAimsDbProvider dbProvider, IConfiguration configuration)
        {
            _dbProvider = dbProvider;
            _configuration = configuration;
        }
        // private readonly RtdmsDbContext _dbcontext;

        // public TeamService(RtdmsDbContext dbcontext)
        // {
        //     _dbcontext = dbcontext;
        // }

        // public AimsTeam AddTeam(AimsTeam team)
        // {
        //     throw new NotImplementedException();
        // }

        // public void AddTeamToEas(List<AimsLnkTeamEa> eas)
        // {
        //     throw new NotImplementedException();
        // }

        // public void AddTeamToState(AimsLnkTeamState team_State)
        // {
        //     throw new NotImplementedException();
        // }

        // public IQueryable<AimsLnkTeamPerson> GetPersonTeams()
        // {
        //     throw new NotImplementedException();
        // }

        // public AimsTeam GetTeam(int team_id)
        // {
        //     return _dbcontext.AimsTeams.FirstOrDefault(e => e.Id == team_id);
        // }

        // public IQueryable<AimsLnkTeamEa> GetTeamEas()
        // {
        //     throw new NotImplementedException();
        // }

        // public AimsPerson GetTeamLeadEa(int ea_id)
        // {
        //     throw new NotImplementedException();
        // }

        // public IQueryable<AimsTeam> GetTeams()
        // {
        //     // create dummy teams data for testing, a team contains id, team_code and status
        //     var teams = new List<AimsTeam>
        //     {
        //         new AimsTeam { Id = 1, TeamCode = 1000, Status = 1 },
        //         new AimsTeam { Id = 2, TeamCode = 2000, Status = 1 },
        //         new AimsTeam { Id = 3, TeamCode = 3000, Status = 1 },
        //         new AimsTeam { Id = 4, TeamCode = 4000, Status = 1 },
        //         new AimsTeam { Id = 5, TeamCode = 5000, Status = 1 }
        //     };
        //     //return _dbcontext.AimsTeams.AsQueryable();
        //     return teams.AsQueryable();
        // }

        // public AimsTeam GetTeamState(string state_name, int code)
        // {
        //     throw new NotImplementedException();
        // }

        public void RemoveEaFromTeam(int link_id)
        {
            throw new NotImplementedException();
        }

        public void RemovePersonFromTeam(int person_id, int team_id)
        {
            throw new NotImplementedException();
        }

        // public void UpdatePersonTeam(AimsLnkTeamPerson person)
        // {
        //     throw new NotImplementedException();
        // }

        // public void UpdateTeam(AimsTeam team)
        // {
        //     throw new NotImplementedException();
        // }

        // public void UpdateTeamEa(AimsLnkTeamEa team_Ea)
        // {
        //     throw new NotImplementedException();
        // }

        // public void UpdateTeamState(AimsLnkTeamState team_State)
        // {
        //     throw new NotImplementedException();
        // }// New methods to execute stored procedures and return results

        // public async Task<DataTable> GetTeamEaStatAsync(int eaId)
        // {
        //     var dataTable = new DataTable();

        //     using (var command = _dbcontext.Database.GetDbConnection().CreateCommand())
        //     {
        //         command.CommandText = "sp_team_ea_stat";
        //         command.CommandType = CommandType.StoredProcedure;
        //         command.Parameters.Add(new SqlParameter("@ea_id", eaId));

        //         using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
        //         {
        //             dataAdapter.Fill(dataTable);
        //         }
        //     }

        //     return dataTable;
        // }

        // public async Task<DataSet> GetHhLineAsync(int page, string level, string locationId)
        // {
        //     var dataSet = new DataSet();

        //     using (var command = _dbcontext.Database.GetDbConnection().CreateCommand())
        //     {
        //         command.CommandText = "sp_get_hh_line";
        //         command.CommandType = CommandType.StoredProcedure;
        //         command.Parameters.Add(new SqlParameter("@page", page));
        //         command.Parameters.Add(new SqlParameter("@level", (object)level ?? DBNull.Value));
        //         command.Parameters.Add(new SqlParameter("@location_id", (object)locationId ?? DBNull.Value));

        //         using (var dataAdapter = new SqlDataAdapter((SqlCommand)command))
        //         {
        //             dataAdapter.Fill(dataSet);
        //         }
        //     }

        //     return dataSet;
        // }



        public async Task<DataTable> GetEaConfirmationSummaryAsync(int eaId)
        {
            var sqlQuery = "sp_get_ea_confirmation_summary";
            var parameters = new[]
            {
        _dbProvider.CreateParameter("@ea_id", eaId, DbType.Int32),
            };

            // Execute the stored procedure and get the DataSet
            var ds = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString(), parameters);

            // Return the first DataTable from the DataSet if it exists, otherwise return an empty DataTable
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

    }
}
