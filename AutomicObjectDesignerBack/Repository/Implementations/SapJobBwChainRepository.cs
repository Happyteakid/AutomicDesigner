using AutomicObjectDesigner.Models.Objects;
using AutomicObjectDesignerBack.Data;
using AutomicObjectDesignerBack.Models.Objects;

namespace AutomicObjectDesignerBack.Repository.Implementations;

public class SapJobBwChainRepository : RepositoryBase<SapJobBwChain>, ISapJobBwChainRepository
{
    public SapJobBwChainRepository(AppDatabaseContext databaseContext) : base(databaseContext)
    {

    }
}