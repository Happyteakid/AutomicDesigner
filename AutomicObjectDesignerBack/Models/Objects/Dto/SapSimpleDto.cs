using AutomicObjectDesignerBack.Models.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutomicObjectDesigner.Models.Objects;
public class SapSimpleStep1Dto
{
    public string SapClient { get; set; }
    public string SapSid { get; set; }
    public string SapReport { get; set; }

    public string SapVariant { get; set; }
    public bool RoutineJob { get; set; }
    public string ProcessName { get; set; }
    public string SapJobName { get; set; }
    public bool DeleteSapJob { get; set; }
    public string ObjectName { get; set; }
}

public class SapSimpleStep2Dto
{
    public string ObjectName { get; set; }
    public string SapReport { get; set; }
    public string SapVariant { get; set; }
    public int? Id { get; set; }
}

public class SapSimpleStep3Dto
{
    public string? Documentation { get; set; }
    public int? Id { get; set; }
}

public class SapSimpleStep4Dto
{
    public string? Title { get; set; }
    public string? Archive1 { get; set; }
    public string? Archive2 { get; set; }
    public string? InternalAccount { get; set; }
    public string? Folder { get; set; }
    public int? Id { get; set; }
}