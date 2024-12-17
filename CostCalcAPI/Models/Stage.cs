namespace CostCalcAPI.Models
{
    public class Stage
    {
        public int Id { get; set; } // Уникальный идентификатор этапа
        public string Name { get; set; } // Название этапа (например, "Системный анализ")
        public int DurationWeeks { get; set; } // Длительность этапа в неделях
        public List<Role> Roles { get; set; } = new List<Role>(); // Роли на этапе
    }
}
