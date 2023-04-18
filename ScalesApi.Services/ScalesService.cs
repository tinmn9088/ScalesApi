using ScalesApi.Contracts;
using System.Text.RegularExpressions;

namespace ScalesApi.Services;

public class ScalesService : IScalesService
{
    private readonly ISerialPortService _serialPortService;

    public ScalesService(ISerialPortService serialPortService)
    {
        _serialPortService = serialPortService;
    }

    public WeightData GetWeight()
    {
        string input = _serialPortService.ReadLine();
        var (weight, unit) = ParseWeightData(input);
        return new WeightData(weight, unit);
    }

    /// <returns>
    /// Вес, единица измерения.
    /// </returns>
    private static (double, string) ParseWeightData(string input)
    {
        string numberPattern = @"^[0-9]+[,.][0-9]+|[0-9]+"; // Целое или дробное число с точкой или запятой в качестве разделителя
        Match match = Regex.Match(input, numberPattern);
        return (double.Parse(match.Value), input[match.Length..].Trim());
    }
}
