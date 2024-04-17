namespace IncrementalCivilization.Domain;

public class ProgessEvent
{
    public Action Effect { get; set; } = () => { };
    public Func<bool> Trigger { get; set; } = () => false;
}