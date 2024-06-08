using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interview.Api.ApplicationCore.Models;

[Owned]
public class Address
{
    [MaxLength(50)] 
    public string Line1 { get; set; } = string.Empty;
    [MaxLength(50)]
    public string? Line2 { get; set; } = string.Empty;
    [MaxLength(30)]
    public string City { get; set; } = string.Empty;
    [MaxLength(30)]
    public string State { get; set; } = string.Empty;
    [MaxLength(5)]
    public string ZipCode { get; set; } = string.Empty;
}
