namespace MoneyPlus.Models;

public class MonthModel
{
    public int Number { get; set; }
    public string Name { get; set; }

    public static List<MonthModel> GetMonths()
    {
        return new List<MonthModel>()
        {
            new MonthModel {Number = 1,  Name = "Jan"},
            new MonthModel {Number = 2,  Name = "Feb"},
            new MonthModel {Number = 3,  Name = "Mar"},
            new MonthModel {Number = 4,  Name = "Apr"},
            new MonthModel {Number = 5,  Name = "May"},
            new MonthModel {Number = 6,  Name = "Jun"},
            new MonthModel {Number = 7,  Name = "Jul"},
            new MonthModel {Number = 8,  Name = "Aug"},
            new MonthModel {Number = 9,  Name = "Sep"},
            new MonthModel {Number = 10,  Name = "Oct"},
            new MonthModel {Number = 11,  Name = "Nob"},
            new MonthModel {Number = 12,  Name = "Dec"},
        };
    }
}
