using System.Collections.Generic;
namespace RhinoVolumeToWeight
{
    public static class Densities
    {
        public static Dictionary<string, double> metalDensitites = new Dictionary<string, double>() {
            //grams per mm^3
            { "aluminum", 0.0027 },
            { "steel", 0.0078 },
            { "sterling_silver", 0.0104 },
            { "gold_14K", 0.0130 },
            { "gold_18K", 0.0150 },
            { "gold_24K", 0.01932 },
            { "platinum",  0.02125 },
            { "paladium", 0.01202 },
            { "titanium",  0.00451 },
            //brass
            //bronze
        };

        public static string[] metals = new string[] {
            "aluminum",
            "steel",
            "sterling_silver",
            "gold_14K",
            "gold_18K",
            "gold_24K",
            "platinum",
            "paladium",
            "titanium"
        };
    }
}
