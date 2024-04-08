namespace IncrementalCivilization.Domain;

public class ProgessEvent
{
    public Action Effect { get; set; } = () => { };
    public Func<bool> Trigger { get; set; } = () => false;
}

public static class ProgressEvents
{
    public static List<ProgessEvent> Initialize(Game game)
    {
        return
        [
            new()
            {
                Effect = () => 
                {
                    game.Resources.Population.Active = true; 
                    game.Jobs.WoodCutter.Active = true;
                },
                Trigger = () => game.Resources.Population.Value > 0
            },
            new()
            {
                Effect = () => {
                    game.Resources.Food.Active = true;
                    game.AddMessage("You found some food in the forrest");
                },
                Trigger = () => game.Resources.Food.Value > 0
            },
            new()
            {
                Effect = () => 
                {
                    game.Resources.Wood.Active = true;
                    game.AddMessage("Try collecting a little more wood");
                },
                Trigger = () => game.Resources.Wood.Value > 0
            },
            new()
            {
                Effect = () => game.Resources.Science.Active = true,
                Trigger = () => game.Resources.Science.Value > 0
            },
            new()
            {
                Effect = () =>
                {
                    game.Buildings.Field.Active = true;
                    game.AddMessage("While gathering food, you wonder if there is an easier way");
                },
                Trigger = () => game.Resources.Food.Value >= 5
            },
            new()
            {
                Effect = () =>
                {
                    game.Effects.RefineFoodEnabled = true;
                    game.AddMessage("Maybe food can be used for other things?");
                },
                Trigger = () => game.Resources.Food.Value >= 25
            },
            new()
            {
                Effect = () =>
                {
                    game.Buildings.Hut.Active = true;
                    game.AddMessage("You wonder if this wood cannot be used for something, if only you had a little more");
                },
                Trigger = () => game.Resources.Wood.Value >= 3
            },
            new()
            {
                Effect = () =>
                {
                    game.Buildings.Library.Active = true;
                    game.AddMessage("The world is large and there are many things to investigate");
                },
                Trigger = () => game.Resources.Wood.Value >= 15
            },
            new()
            {
                Effect = () => game.AddMessage("Over time people will join your village, be patient"),
                Trigger = () => game.Buildings.Hut.Count > 0,
            },
            new()
            {
                Effect = () => 
                {
                    game.Jobs.Scholar.Active = true; 
                    game.AddMessage("What wonders to discover...");
                },
                Trigger = () => game.Buildings.Library.Count > 0,
            },
            new()
            {
                Effect = () =>
                { 
                    game.Effects.ResearchPageEnabled = true;
                    game.AddMessage("The first idea presents itself");
                    
                    var agriculture = new ResearchItem("Agriculture", "Enables the farming job");
                    agriculture.Cost.Add(new CostItem(game.Resources.Science, 50));
                    agriculture.BuyAction = () => game.Jobs.Farmer.Active = true;
                    game.Research.Add(agriculture);
                },
                Trigger = () => game.Jobs.Scholar.Count > 0,
            },
            new()
            {
                Effect = () =>
                {
                    game.Effects.TimePageEnabled = true;
                    game.AddMessage("You can now reset the game");
                },
                Trigger = () => game.Resources.Population.Value >= 2
            },
        ];
    }
}
