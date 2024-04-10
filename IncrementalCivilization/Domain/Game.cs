using IncrementalCivilization.Utils;
using NLog;

namespace IncrementalCivilization.Domain;

public class Game : IGame
{
    public static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public Bundle<ResourceType, Resource> Resources { get; private set; }

    public Game()
    {
        logger.Debug("Creating all resources");
        Resources = ResourceManager.AllResources();
    }
}
