using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyPlus.Data.Entities;


public class Active
{ 

    public int Id { get; set; }

    [MaxLength(30)]
    [DisplayName("Description Active")]
    public string Description { get; set; }

    [DisplayName("Active Value")]
    [DataType(DataType.Currency)]
    public double? Value { get; set; }

    public string UserId { get; internal set; }
}
