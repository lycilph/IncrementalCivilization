namespace IncrementalCivilization.Domain;

public class ProgessEvent
{
    public string Name { get; set; } = string.Empty;
    public Action Effect { get; set; } = () => { };
    public Func<bool> Trigger { get; set; } = () => false;
}