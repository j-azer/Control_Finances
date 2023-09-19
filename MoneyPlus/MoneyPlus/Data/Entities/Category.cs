using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace MoneyPlus.Data.Entities;


public class Category
{
    public int Id { get; set; }


    [MaxLength(50)]
    [DisplayName("Category")]
    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
    public string Name { get; set; }


    public List<Subcategory> Subcategories { get; set; }
}