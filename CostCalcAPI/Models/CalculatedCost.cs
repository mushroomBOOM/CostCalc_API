namespace CostCalcAPI.Models
{
    public class CalculatedCost
    {
        public decimal TotalCost { get; set; } // Общая стоимость проекта
        public decimal TotalStageCost { get; set; } // Общая стоимость этапов проекта
        public decimal OverheadCosts { get; set; } // Стоимость накладных расходов
        public string Breakdown { get; set; } // Детализация расчета в виде строки
    }
}
