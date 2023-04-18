using System.IO.Ports;

namespace ScalesApi.Contracts;

public record SerialPortServiceConfiguration
{
    public string? PortName { get; set; }
    public int BaudRate { get; set; }
    public Parity? Parity  { get; set; }
    public int DataBits { get; set; }
    public StopBits? StopBits { get; set; }
    public Handshake? Handshake { get; set; }
    public int WriteTimeout { get; set; }
    public int ReadTimeout { get; set; }
}
