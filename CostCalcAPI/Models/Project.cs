namespace CostCalcAPI.Models
{
    public class Project
    {
        public int Id { get; set; } // Уникальный идентификатор проекта
        public string Name { get; set; } // Название проекта
        public List<Stage> Stages { get; set; } = new List<Stage>(); // Этапы проекта
        public List<OverheadCost> OverheadCosts { get; set; } = new List<OverheadCost>(); // Накладные расходы
    }
}
