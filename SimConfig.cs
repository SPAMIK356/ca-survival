using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;
namespace CASurvive
{
    struct SimSettings
    {
        public struct GrowSettigns
        {
            public readonly CoverType[] suitableSurfaces;
            public readonly float baseSpreadChance;
        }

        public readonly float fireSpreadChance;
        public readonly float fireExtinguishChance;
        public readonly GrowSettigns grassSettings;
        public readonly GrowSettigns treeSettings;
    }
    internal static class SimConfig
    {
        public readonly static float fireSpreadChance = 0.3f;
        public readonly static float fireExtinguishChance = 0.3f;

        

        public static Dictionary<CoverType, CoverProperty> properties;
        public static Dictionary<CoverType, CoverLogic> logic;


        public static void Initialize()
        {
            string jsonTemplates = File.ReadAllText("./CoverTemplates.json");
            properties = JsonSerializer.Deserialize<Dictionary<CoverType, CoverProperty>>(jsonTemplates);
            
        }
    }
}
