using AutomicObjectDesigner.Models.Objects;

namespace AutomicObjectDesignerBack.Models.Objects
{
    public class SapJobBwChain : Job
    {
        // step 1

        public string SapClient { get; set; }
        public string SapSid { get; set; }
        public string Kette { get; set; }
        public bool RoutineJob { get; set; }
        public string ProcessName { get; set; }
        public string SapJobName { get; set; }
        public bool DeleteSapJob { get; set; }
        public string ObjectName { get; set; }
        public string SapReport { get; set; }
        public string SapVariant { get; set; }

        ////step 3
        public string? Documentation { get; set; }

        ////step 4
        public string? Title { get; set; }
        public string? Archive1 { get; set; }
        public string? Archive2 { get; set; }
        public string? InternalAccount { get; set; }
        public string? Folder { get; set; }
        //// <folder>

        ////outcome
        ////Fixed predefined fields:

        public string Template = "JOBS.SAP_ABAP";
        public string Process = "BW_ACTIVATE_CHAIN ID=&SAP_Kette#";
        public string PreProcess = ":INC XXX.XXX#ZZZ#SAP_PRE#GENERAL.JOBI";
        public string PostProcess = ":INC XXX.XXX#ZZZ#SAP_POST#GENERAL.JOBI";

        ////Calculated fields:
        public string? Queue { get; set; }
        public string? Agent { get; set; }
        public string? Login { get; set; }

    }
}
