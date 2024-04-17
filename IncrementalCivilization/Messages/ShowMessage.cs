namespace IncrementalCivilization.Messages;

public class ShowMessage(string text)
{
    public string Text { get; private set; } = text;

    public static ShowMessage Create(string text) => new ShowMessage(text);
}
