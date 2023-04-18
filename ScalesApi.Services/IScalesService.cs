using ScalesApi.Contracts;

namespace ScalesApi.Services;

public interface IScalesService
{
    WeightData GetWeight();
}
