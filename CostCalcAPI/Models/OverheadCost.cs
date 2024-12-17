namespace CostCalcAPI.Models
{
    public class OverheadCost
    {
        public int Id { get; set; } // Уникальный идентификатор накладного расхода
        public string Description { get; set; } // Описание расхода (например, "Аренда офиса")
        public decimal Amount { get; set; } // Сумма расхода
    }
}
