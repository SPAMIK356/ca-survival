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
            [JsonInclude]
            public LandType[] suitableSurfaces { get; private set; }
            [JsonInclude]
            public  float baseSpreadChance {get; private set;}
        }
        public struct LandSettings
        {
            [JsonInclude]
            public float growModifier {get; private set;}
        }
        [JsonInclude]
        public float fireSpreadChance {get; private set;}
        [JsonInclude]
        public float fireExtinguishChance {get; private set;}
        [JsonInclude]
        public GrowSettigns grassSettings {get; private set;}
        [JsonInclude]
        public GrowSettigns treeSettings {get; private set;}
        [JsonInclude]
        public Dictionary<LandType, LandSettings> landSettings {get; private set;}  
    }
    
    internal static class SimConfig
    {
        private static readonly JsonSerializerOptions Options = new() { 
            Converters = {new  JsonStringEnumConverter()}
        
        };

        public static SimSettings settings { get; private set; }

        public static Dictionary<CoverType, CoverProperty> properties;
        public static Dictionary<CoverType, CoverLogic> logic;


        public static void Initialize()
        {
            string jsonTemplates = File.ReadAllText("./cover_templates.json");
            properties = JsonSerializer.Deserialize<Dictionary<CoverType, CoverProperty>>(jsonTemplates, Options);

            string jsonSettings = File.ReadAllText("./settings.json");
            settings = JsonSerializer.Deserialize<SimSettings>(jsonSettings, Options);
            
        }
    }
}
