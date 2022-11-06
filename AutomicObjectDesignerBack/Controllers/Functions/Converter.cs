using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AutomicObjectDesignerBack.Controllers.Functions;

public class Converter
{
    public string text { get; set; }
    public static string TextKetteConverter(string text)
    {
        Regex reg = new Regex("[!\"#$%&'()*+,./:;<=>?@^`{|}~]");
        text = reg.Replace(text, "-");

        return text;
    }

    public static string ConvertText(Regex reg, string text, string replacmentSign)
    {
        text = reg.Replace(text, replacmentSign);

        return text;
    }

    public static string TextReportConverter(string text)
    {
        Regex reg = new Regex("[!\"%&'()*+,./:;<=>?^`{|}~]");
        text = reg.Replace(text, "-");

        return text;
    }
    public static string TextVariantConverter(string text)
    {
        Regex reg = new Regex("[!\"#$%&'()*+_,./:;<=>?@^`{|}~]");
        text = reg.Replace(text, "-");

        return text;
    }

    //TODO: Need to check if this works
    public static string DocumentationSplitNewLine(string text)
    {
        int maxLineLenght = 254;
        return String.Concat(text.Select((c, i) => i > 0 && (i % maxLineLenght) == 0 ? c.ToString() + Environment.NewLine : c.ToString()));

    }
}