using NLog;

namespace IncrementalCivilization.Domain;

public class Game
{
    public static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public Capabilities Capabilities { get; private set; }
    public ResourceBundle Resources { get; private set; }

    public Game()
    {
        logger.Debug("Creating game object");
     
        Capabilities = new Capabilities();
        Resources = new ResourceBundle();
    }
}
