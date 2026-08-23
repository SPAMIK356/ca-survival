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
        public struct LandSettings
        {
            public readonly float growModifier;
        }
        public readonly float fireSpreadChance;
        public readonly float fireExtinguishChance;
        public readonly GrowSettigns grassSettings;
        public readonly GrowSettigns treeSettings;

        public readonly Dictionary<LandType, LandSettings> landSettings;  
    }
    internal static class SimConfig
    {

        public static SimSettings settings { get; private set; }

        public static Dictionary<CoverType, CoverProperty> properties;
        public static Dictionary<CoverType, CoverLogic> logic;


        public static void Initialize()
        {
            string jsonTemplates = File.ReadAllText("./cover_templates.json");
            properties = JsonSerializer.Deserialize<Dictionary<CoverType, CoverProperty>>(jsonTemplates);

            string jsonSettings = File.ReadAllText("./settings.json");
            settings = JsonSerializer.Deserialize<SimSettings>(jsonSettings);
            
        }
    }
}
