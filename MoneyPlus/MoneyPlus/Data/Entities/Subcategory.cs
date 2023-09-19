using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace MoneyPlus.Data.Entities;


public class Subcategory
{
    public int Id { get; set; }

    [MaxLength(128)]
    [DisplayName("Subcategory")]
    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    public string Name { get; set; }

    public Category Category { get; set; }

    public int CategoryId { get; set; }
}