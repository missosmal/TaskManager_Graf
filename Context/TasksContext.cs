using Microsoft.EntityFrameworkCore;
using TaskManager_Graf.Classes.Database;
using TaskManager_Graf.Models;

namespace TaskManager_Graf.Context
{
    public class TasksContext : DbContext
    {
        // <summary> Данные из базы данных </summary>
        public DbSet<Tasks> Tasks { get; set; }

        // <summary> Конструктор для контекста </summary>
        public TasksContext()
        {
            // Проверяем создано ли подключение, если нет то создаём
            Database.EnsureCreated();
            Tasks.Load(); // выполняем загрузку данных
        }

        // <summary> Переопределённый метод конфигурации </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            // Подключаемся к MySql, со следующими настройками из Config
            optionsBuilder.UseMySql(Config.connection, Config.version);
    }
}
