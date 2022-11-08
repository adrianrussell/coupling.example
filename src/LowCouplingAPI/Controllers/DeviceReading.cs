namespace LowCouplingAPI.Controllers;

public class DeviceReading
{
    public long Id { get; set; }
    public string Device { get; set; } = string.Empty;

    public List<Measurement> Measurements { get; set; } = new();
}

