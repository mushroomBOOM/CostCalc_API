namespace CostCalcAPI.Models
{
    public class Role
    {
        public int Id { get; set; } // Уникальный идентификатор роли
        public string Name { get; set; } // Название роли (например, "Разработчик")
        public decimal WeeklySalary { get; set; } // Зарплата за неделю
    }
}
