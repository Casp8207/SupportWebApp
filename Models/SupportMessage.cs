using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace SupportWebApp.Models;

public class SupportMessage
{
    [JsonProperty("id")]
    public string Id { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Phone { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    [JsonProperty("category")]
    public string Category { get; set; } = "";

    public DateTime DateTime { get; set; }
}