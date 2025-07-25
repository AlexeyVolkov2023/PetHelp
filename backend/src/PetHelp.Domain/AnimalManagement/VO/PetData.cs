using CSharpFunctionalExtensions;
using PetHelp.Domain.Shared;

namespace PetHelp.Domain.AnimalManagement.VO;

public class PetData : ComparableValueObject
{
    private PetData(
        string color,
        string healthInfo,
        double weight,
        double height,
        bool isNeutered,
        bool isVaccinated)
    {
        Color = color;
        HealthInfo = healthInfo;
        Weight = weight;
        Height = height;
        IsNeutered = isNeutered;
        IsVaccinated = isVaccinated;
    }

    public string Color { get; }
    public string HealthInfo { get; }
    public double Weight { get; }
    public double Height { get; }
    public bool IsNeutered { get; }
    public bool IsVaccinated { get; }

    public static Result<PetData, Error> Create(
        string color,
        string healthInfo,
        double weight,
        double height,
        bool isNeutered,
        bool isVaccinated
    )
    {
        if (string.IsNullOrWhiteSpace(color) || color.Length > Constants.COLOR_MAX_LENGTH)
            return Errors.General.ValueIsInvalid("color");

        if (string.IsNullOrWhiteSpace(healthInfo) || healthInfo.Length > Constants.HEALTH_INFO_MAX_LENGTH)
            return Errors.General.ValueIsInvalid("health info");

        if (weight <= 0 || weight > Constants.WEIGHT_MAX_VALUE)
            return Errors.General.ValueIsInvalid("weight");

        if (height <= 0 || height > Constants.HEIGHT_MAX_VALUE)
            return Errors.General.ValueIsInvalid("height");

        return new PetData(color, healthInfo, weight, height, isNeutered, isVaccinated);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Color;
        yield return HealthInfo;
        yield return Weight;
        yield return Height;
        yield return IsNeutered;
        yield return IsVaccinated;
    }
}