using AutomicObjectDesigner.Models.Objects;
using AutomicObjectDesignerBack.Data;
using AutomicObjectDesignerBack.Models.Objects;

namespace AutomicObjectDesignerBack.Repository.Implementations;

public class WindowsGeneralRepository : RepositoryBase<WindowsGeneral>, IWindowsGeneralRepository
{
    public WindowsGeneralRepository(AppDatabaseContext databaseContext) : base(databaseContext)
    {

    }
}