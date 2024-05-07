using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adoProject.Models;

public enum Gender{
  Male = 0,
  Female = 1
};

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public String? FirstName { get; set; }
    [Required]
    public String? LastName { get; set; }
    public Gender Sex{get; set;}
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set;}
    public String? Address {get; set;}
    public DateTime CreatedDate {get; set;}
}
