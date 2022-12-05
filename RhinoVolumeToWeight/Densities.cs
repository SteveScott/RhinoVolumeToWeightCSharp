using System.Collections.Generic;
namespace RhinoVolumeToWeight
{
    public class Densities
    {
        public Dictionary<string, double> metalDensitites = new Dictionary<string, double>() {
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
            
        /*
        pine = 0.0005,
        oak = 0.0007,
        brick = 0.0018,
        concrete = 0.0023,
        diamond = 0.00351,
        sapphire = 0.00398,
        ruby = 0.00398,
        emerald = 0.0027,
        glass = 0.0024,
        */
        
    }
}
