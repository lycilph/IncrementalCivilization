using NLog;

namespace IncrementalCivilization.Domain;

public class Effects
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public double FarmerEffieciency { get; set; } = 1.0;
    public double WoodCutterEffieciency { get; set; } = 1.0;

    public void Reset()
    {
        logger.Debug("Resetting effects");

        FarmerEffieciency = 1.0;
        WoodCutterEffieciency = 1.0;
    }
}
