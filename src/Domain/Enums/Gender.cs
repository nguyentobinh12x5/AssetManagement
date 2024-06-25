using System.Text.Json.Serialization;

namespace AssetManagement.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male = 0,
    Female = 1,
}