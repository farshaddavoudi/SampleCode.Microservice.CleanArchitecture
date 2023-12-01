namespace SampleMicroserviceApp.Identity.Application.Common.Extensions;

public static class UnitsConvertExtensions
{
    private const decimal ConversionRateFromKgToLb = 2.20462m;

    /// <summary>
    /// Kilogram To Pound
    /// </summary>
    public static decimal KgToLb(this decimal kg)
    {
        return kg * ConversionRateFromKgToLb;
    }

    /// <summary>
    /// Pound To Kilogram
    /// </summary>
    public static decimal LbToKg(this decimal lb)
    {
        return lb / ConversionRateFromKgToLb;
    }

    /// <summary>
    /// Fuel Kilogram To Liter
    /// </summary>
    public static decimal KgToLiter(this decimal kgFuel, int degreeCelsius)
    {
        return kgFuel / (decimal)(0.8 - degreeCelsius * 0.000746);
    }

    /// <summary>
    /// Fuel Liter To Kilogram
    /// </summary>
    public static decimal LbToLiter(this decimal lbFuel, int degreeCelsius)
    {
        var kgFuel = lbFuel.LbToKg();

        return kgFuel.KgToLiter(degreeCelsius);
    }
}