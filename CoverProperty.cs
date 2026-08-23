using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace CASurvive
{
    enum CoverType
    {
        Empty,
        Grass,
        Tree
    }
    internal class CoverProperty
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CoverType type { get; init; }
        public bool burnable { get; init; }
        public float fireKeepChance { get; init; }
        public float lightingChance { get; init; }
    }
}
