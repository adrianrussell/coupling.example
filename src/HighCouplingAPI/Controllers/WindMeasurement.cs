namespace HighCouplingAPI.Controllers;

public class WindMeasurement
{
    public long Id { get; set; }
    public int WindSpeedMph{ get; set; }

    public string WindDirection { get; set; } = null!;

    public string MeasurementLocation { get; set; } = null!;
}