using System.ComponentModel.DataAnnotations;

namespace AutomicObjectDesigner.Models.Objects;

public abstract class Job : AutomicObject
{
    public bool? Active { get; set; }
    public int? MaxParallelTasks { get; set; }
    public string? Process { get; set; }
    public string? PreProcess { get; set; }
    public string? PostProcess { get; set; }
    public string? Queue { get; set; }
    public string? Agent { get; set; }
    public string? Login { get; set; }
}