using AutomicObjectDesignerBack.Models.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutomicObjectDesignerBack.Models.Objects
{
    public class UnixGeneralStep1Dto
    {
        public string UnixServer { get; set; }
        public string UnixLogin { get; set; }
        public string SapSid { get; set; }
        public string SapClient { get; set; }
        public bool RoutineJob { get; set; }
        public string ProcessName { get; set; }
        public string NameSuffix { get; set; }
        public string ObjectName { get; set; }

    }

    public class UnixGeneralStep2Dto
    {
        public string? ObjectName { get; set; }
        public int? Id { get; set; }
    }
    public class UnixGeneralStep3Dto
    {
        public string? Process { get; set; }
        public int? Id { get; set; }
    }
    public class UnixGeneralStep4Dto
    {
        public string? Documentation { get; set; }
        public int? Id { get; set; }
    }
    public class UnixGeneralStep5Dto
    {
        public string? Title { get; set; }
        public string? Archive1 { get; set; }
        public string? Archive2 { get; set; }
        public string? InternalAccount { get; set; }
        public string? Folder { get; set; }
        public int? Id { get; set; }
    }

    public class UnixGeneralStep6Dto
    {
        public int? Id { get; set; }
        public string? VariableKey { get; set; }
        public string? VariableValue { get; set; }

    }
}
