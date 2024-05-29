using TaskManager_Graf.Classes;
using TaskManager_Graf.Models;
using System;
using System.Collections.ObjectModel;
using TaskManager_Graf.Context;
using System.Linq;

namespace TaskManager_Graf.ViewModels
{
    public class VM_Tasks : Notification
    {
        // <summary> Контекст данных для модели задач </summary>
        public TasksContext tasksContext = new TasksContext();

        // <summary> Коллекция задач </summary>
        public ObservableCollection<Tasks> Tasks { get; set; }

        // <summary> Конструктор для модели представления </summary>
        public VM_Tasks() =>
            // Получаем данные из контекста данных отсортированно по выполнению
            Tasks = new ObservableCollection<Tasks>(tasksContext.Tasks.OrderBy(x => x.Done));

        // <summary> Метод добавления новой задачи </summary>
        public RealyCommand OnAddTask
        {
            get // акссесор чтения
            {
                return new RealyCommand(obj =>
                {
                    // выполняем команду
                    Tasks NewTask = new Tasks() // Создаём новую задачу
                    {
                        DateExecute = DateTime.Now // устанавливаем текущую дату
                    };
                    Tasks.Add(NewTask); // Добавляем задачу в коллекцию
                    tasksContext.Tasks.Add(NewTask); // Добавляем задачу в контекст данных
                    tasksContext.SaveChanges(); // Сохраняем изменения в БД
                });
            }
        }
    }
}
