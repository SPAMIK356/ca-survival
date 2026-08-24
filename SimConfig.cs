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
            public CoverType[] suitableSurfaces { get; private set; }
            public  float baseSpreadChance {get; private set;}
        }
        public struct LandSettings
        {
            public float growModifier {get; private set;}
        }
        public float fireSpreadChance {get; private set;}
        public float fireExtinguishChance {get; private set;}
        public GrowSettigns grassSettings {get; private set;}
        public GrowSettigns treeSettings {get; private set;}

        public Dictionary<LandType, LandSettings> landSettings {get; private set;}  
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
