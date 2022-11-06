using AutomicObjectDesigner.Models.Objects;
using AutomicObjectDesignerBack.Models.Objects;
using AutomicObjectDesignerBack.Repository;

namespace AutomicObjectDesignerBack.Models.List;

public class ListObjects
{
    private readonly IWindowsGeneralRepository _windowsGeneralRepository;
    private readonly IUnixGeneralRepository _unixGeneralRepository;
    private readonly ISapSimpleRepository _sapSimpleRepository;
    private readonly ISapJobBwChainRepository _sapJobBwChainRepository;
    public List<WindowsGeneral>? WindowsGenerals { get; set; }
    public List<SapJobBwChain>? SapJobBwChains { get; set; }
    public List<UnixGeneral>? UnixGenerals { get; set; }
    public List<SapSimple>? SapSimples { get; set; }

    public ListObjects(List<WindowsGeneral> windowsGenerals, List<SapJobBwChain> sapJobBwChains, List<UnixGeneral> unixGenerals, List<SapSimple> sapSimples)
    {
        WindowsGenerals = windowsGenerals;
        SapJobBwChains = sapJobBwChains;
        UnixGenerals = unixGenerals;
        SapSimples = sapSimples;
    }

}