using CourseChecker.Items;

namespace CourseChecker;
internal class Parser
{
    public static IItem Parse(string text)
    {
        return text[0] switch
        {
            '[' => ParseAnyOf(text),
            '<' => ParseSequence(text),
            _ => new ControlPoint(text)
        };
    }

    private static IItem ParseAnyOf(string text)
    {
        text = text.TrimStart('[').TrimEnd(']').Trim();
        var items = new List<IItem>();
        var numberInString = text[0..(text.IndexOf(' '))];
        var amount = int.Parse(numberInString);
        text = text[(numberInString.Length + 1)..];
        while (text != "")
        {
            var trimmed = GetTrimmed(text);
            var item = Parse(trimmed);
            items.Add(item);
            text = text[trimmed.Length..].Trim();
        }
        return new AnyOfBlock(amount, items.ToArray());

    }

    private static IItem ParseSequence(string text)
    {
        text = text.TrimStart('<').TrimEnd('>').Trim();
        var items = new List<IItem>();

        while (text != "")
        {
            var trimmed = GetTrimmed(text);
            var item = Parse(trimmed);
            items.Add(item);
            text = text[trimmed.Length..].Trim();
        }
        return new SequenceBlock(items.ToArray());

    }

    private static string GetTrimmed(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }
        return text[0] switch
        {
            '[' => text[0..(text.IndexOf(']') + 1)],
            '<' => text[0..(text.IndexOf('>') + 1)],
            _ => TrimDefault(text)
        };
    }

    private static string TrimDefault(string text)
    {
        var index = text.IndexOf(' ');
        if (index == -1)
        {
            return text;
        }
        return text[0..index];
    }
}
