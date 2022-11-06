using AutomicObjectDesigner.Models.Objects;

namespace AutomicObjectDesignerBack.Models.Objects
{
    public class WindowsGeneral : Job
    {
        //Entry sequence:
        // step 1 and 2
        public string SapSid { get; set; }
        public string SapClient { get; set; }
        public string WinServer { get; set; }
        public string WinLogin { get; set; }
        public bool RoutineJob { get; set; }
        public string? ProcessName { get; set; }
        public string? NameSuffix { get; set; }
        public string? ObjectName { get; set; }


        //step 3
        public string? Process { get; set; }

        //step 4
        public string? Documentation { get; set; }

        //step 5

        public string? Title { get; set; }
        public string? Archive1 { get; set; }
        public string? Archive2 { get; set; }
        public string? InternalAccount { get; set; }
        public string? Folder { get; set; }
        //<folder>

        ////step 6 optional
        ////up to 5 set's of variables
        public string? VariableKey1 { get; set; }
        public string? VariableValue1 { get; set; }
        public string? VariableKey2 { get; set; }
        public string? VariableValue2 { get; set; }
        public string? VariableKey3 { get; set; }
        public string? VariableValue3 { get; set; }
        public string? VariableKey4 { get; set; }
        public string? VariableValue4 { get; set; }
        public string? VariableKey5 { get; set; }
        public string? VariableValue5 { get; set; }

        //outcome
        //Fixed predefined fields:

        public string Template = "JOBS.WIN";
        public string PreProcess = ":INC XXX.XXX#ZZZ#WIN_PRE#GENERAL.JOBI";
        public string PostProcess = ":INC XXX.XXX#ZZZ#WIN_POST#GENERAL.JOBI";

        //Calculated fields:
        public string? Queue { get; set; }

    }
}
