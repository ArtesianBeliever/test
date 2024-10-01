using System;
namespace WebApplication1.Entities;

public class Team
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Tag { get; set; } = String.Empty;
    public Region Region { get; set; } = 0;
}