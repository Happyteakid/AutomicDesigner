using AutomicObjectDesignerBack.Models.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutomicObjectDesignerBack.Models.Objects
{
    public class WindowsGeneralStep1Dto
    {
        public string WinServer { get; set; }
        public string WinLogin { get; set; }
        public bool RoutineJob { get; set; }
        public string? ProcessName { get; set; }
        public string? NameSuffix { get; set; }
        public string? ObjectName { get; set; }
        public string SapSid { get; set; }
        public string SapClient { get; set; }
    }

    public class WindowsGeneralStep2Dto
    {
        public string? ObjectName { get; set; }
        public int? Id { get; set; }
    }

    public class WindowsGeneralStep3Dto
    {
        public string? Process { get; set; }
        public int? Id { get; set; }
    }

    public class WindowsGeneralStep4Dto
    {
        public string? Documentation { get; set; }
        public int? Id { get; set; }
    }

    public class WindowsGeneralStep5Dto
    {
        public string? Title { get; set; }
        public string? Archive1 { get; set; }
        public string? Archive2 { get; set; }
        public string? InternalAccount { get; set; }
        public string? Folder { get; set; }
        public int? Id { get; set; }
    }

    public class WindowsGeneralStep6Dto
    {
        public int? Id { get; set; }
        public string? VariableKey { get; set; }
        public string? VariableValue { get; set; }

    }
}
