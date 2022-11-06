using AutomicObjectDesigner.Models.Objects;
using AutomicObjectDesignerBack.Data;
using AutomicObjectDesignerBack.Models.Objects;

namespace AutomicObjectDesignerBack.Repository.Implementations;

public class UnixGeneralRepository : RepositoryBase<UnixGeneral>, IUnixGeneralRepository
{
    public UnixGeneralRepository(AppDatabaseContext databaseContext) : base(databaseContext)
    {

    }
}