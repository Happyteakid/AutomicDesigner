using AutomicObjectDesigner.Models.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutomicObjectDesigner.Models.Objects;

public class SapSimple : Job
{
    //Entry sequence:
    // step 1

    public string SapClient { get; set; }
    public string SapSid { get; set; }
    public string SapReport { get; set; }

    public string SapVariant { get; set; }
    public bool? RoutineJob { get; set; }
    public string ProcessName { get; set; }
    public string SapJobName { get; set; }
    public bool? DeleteSapJob { get; set; }

    public string ObjectName { get; set; }


    //step 3
    public string? Documentation { get; set; }

    //step 4
    public string? Title { get; set; }
    public string? Archive1 { get; set; }
    public string? Archive2 { get; set; }
    public string? InternalAccount { get; set; }
    public string? Folder { get; set; }

    //outcome
    //Fixed predefined fields:
   
    public string Template = "JOBS.SAP_ABAP";
    public string Process = "R3_ACTIVATE_REPORT REPORT=&SAP_REPORT#,VARIANT=&SAP_VARIANT#";
    public string PreProcess = ":INC XXX.XXX#ZZZ#SAP_PRE#GENERAL.JOBI";
    public string PostProcess = ":INC XXX.XXX#ZZZ#SAP_POST#GENERAL.JOBI";

    //Calculated fields:
    public string? Queue1 { get; set; }
    public string? Agent1 { get; set; }
    public string? Login1 { get; set; }



}

