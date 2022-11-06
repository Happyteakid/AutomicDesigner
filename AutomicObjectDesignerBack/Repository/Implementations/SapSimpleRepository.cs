using AutomicObjectDesigner.Models.Objects;
using AutomicObjectDesignerBack.Data;
using AutomicObjectDesignerBack.Models.Objects;

namespace AutomicObjectDesignerBack.Repository.Implementations;

public class SapSimpleRepository : RepositoryBase<SapSimple>, ISapSimpleRepository
{
    public SapSimpleRepository(AppDatabaseContext databaseContext) : base(databaseContext)
    {
    }
}