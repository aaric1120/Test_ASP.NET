using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class MyData 
{
    public string? Name { get; set; }
    [Key]
    public int? ID {get; set;}
}