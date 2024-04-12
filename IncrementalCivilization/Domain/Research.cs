namespace IncrementalCivilization.Domain;

public class Research
{
    public Improvement Agriculture { get; private set; }

    public Research(Game game)
    {
        Agriculture = new Improvement("Agriculture", "Enables the farming job");
        Agriculture.Cost.Add(new Cost(game.Resources.Science, 50));
        Agriculture.BuyAction = () => game.Jobs.Farmer.Active = true;
    }
}
