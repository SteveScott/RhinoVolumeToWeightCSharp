using System;
using System.Collections.Generic;

public static class RingSizes
{
    public static Dictionary<double, double> ringSizeDict = new Dictionary<double, double>()
    {
        {1.0, 10.5 },
        {1.5, 11.5 },
        {2.0, 12.37 },
        {2.5, 13.41 },
        {3.0, 14.07 },
        {3.5, 14.48 },
        {4.0, 14.88 },
        {4.5, 15.29 },
        {5.0, 15.7 },
        {5.5, 16.1},
        {6.0, 16.51},
        {6.5, 16.92},
        {7.0, 17.32},
        {7.5, 17.73},
        {8.0, 18.1},
        {8.5, 18.54},
        {9.0, 18.95},
        {9.5, 19.35},
        {10.0, 19.76},
        {10.5, 20.17},
        {11.0, 20.57},
        {11.5, 20.98},
        {12.0, 21.59},
        {12.5, 22},
        /// chatGPT below. Really they don't get this big.
    };
    public static string[] ringSizeArray = new string[]
    {
        "1",
        "1.5",
        "2",
        "2.5",
        "3",
        "3.5",
        "4",
        "4.5",
        "5",
        "5.5",
        "6",
        "6.5",
        "7",
        "7.5",
        "8",
        "8.5",
        "9",
        "9.5",
        "10",
        "10.5",
        "11",
        "11.5",
        "12",
        "12.5",
    };
}
