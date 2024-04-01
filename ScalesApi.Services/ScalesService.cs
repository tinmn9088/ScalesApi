using ScalesApi.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ScalesApi.Services;

public partial class ScalesService : IScalesService
{
    private readonly ISerialPortService _serialPortService;

    public ScalesService(ISerialPortService serialPortService)
    {
        _serialPortService = serialPortService;
    }

    public WeightData GetWeight()
    {
        string input = _serialPortService.ReadLine();
        bool isParsed = TryParseWeightData(input, out double weight, out string unit);

        if (!isParsed)
        {
            throw new IOException($"Invalid input: {input}");
        }

        return new WeightData(weight, unit);
    }

    public static bool TryParseWeightData(string input, out double weight, out string unit)
    {
        weight = double.NaN;
        unit = string.Empty;
        Match match = GetWeightDataLineRegex().Match(input);

        if (!match.Success)
        {
            return false;
        }

        weight = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        unit = match.Groups[2].Value;

        return true;
    }

    [GeneratedRegex("wn(\\d+\\.?\\d+)(kg)")]
    private static partial Regex GetWeightDataLineRegex();
}
