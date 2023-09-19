using MoneyPlus.Data.Entities;

namespace MoneyPlus.Models;

public class AggregateExepenseCategoryModel
{
    public Category Category { get; set; }

    public int Year { get; set; }
    public int Month { get; set; }
    public double Total { get; set; }
}

public class AggregateExepenseSubcategoryModel
{
    public Subcategory Subcategory { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public double Total { get; set; }
}
