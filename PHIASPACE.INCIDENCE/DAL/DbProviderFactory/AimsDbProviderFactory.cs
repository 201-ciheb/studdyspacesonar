using PHIASPACE.INCIDENCE.DAL.MssqlDbService;
using PHIASPACE.INCIDENCE.DAL.MysqlDbService;
//using PHIASPACE.INCIDENCE.DAL.MssqlDbService;

namespace PHIASPACE.INCIDENCE.DAL.DbProviderFactory;

// This class is sorely to provide a dynamic way of switchin databases.
public class AimsDbProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AimsDbProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAimsDbProvider GetProvider(string dbType)
    {
        return dbType switch
        {
            "MSSQL" => _serviceProvider.GetService<MssqlDbProvider>(),
            "MySQL" => _serviceProvider.GetService<MysqlDbProvider>(),
            _ => throw new NotImplementedException()
        };
    }
}
