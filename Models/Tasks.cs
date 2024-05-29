using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using TaskManager_Graf.Classes;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager_Graf.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                Match match = Regex.Match(value, "^.{1,50}$");
                if (!match.Success)
                    MessageBox.Show("Наименование не должно быть пустым, и не более 50 символов.", "Не корректный ввод значения.");
                else
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string priority;
        // <summary> Сокрито приоритет </summary>
        public string Priority
        {
            get { return priority; } // Акксессор чтения
            set
            {
                // проверяем входящее значение, на регулярное выражение
                Match match = Regex.Match(value, "^[a-z]{1,30}$");
                if (!match.Success)
                {
                    MessageBox.Show("Приоритет не должен быть пустым, и не более 30 символов.", "Не корректный вод значения."); // выводим сообщение
                }
                else
                {
                    priority = value; // запоминаем введенное значение
                    OnPropertyChanged("Priority"); // сообщаем об изменении свойства
                }
            }
        }

        // <summary> Поле даты выполнения </summary>
        private DateTime dateExecute;
        // <summary> Сокрито дата выполнения </summary>
        public DateTime DateExecute
        {
            get { return dateExecute; } // Акксессор чтения
            set
            {
                // Проверяем что указанная дата не менее текущей
                if (value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Дата выполнения не может быть менее текущей.", "Не корректный вод значения."); // выводим сообщение
                }
                else
                {
                    dateExecute = value; // запоминаем введенное значение
                    OnPropertyChanged("DateExecute"); // сообщаем об изменении свойства
                }
            }
        }

        // <summary> Комментарий </summary>
        private string comment;
        // <summary> Сокрито комментарий </summary>
        public string Comment
        {
            get { return comment; } // Акксессор чтения
            set
            {
                // проверяем входящее значение, на регулярное выражение
                Match match = Regex.Match(value, "^[a-z]{1,1000}$");
                if (!match.Success)
                {
                    MessageBox.Show("Комментарий не должен быть пустым, и не более 1000 символов.", "Не корректный вод значения."); // выводим сообщение
                }
                else
                {
                    comment = value; // запоминаем введенное значение
                    OnPropertyChanged("Comment"); // сообщаем об изменении свойства
                }
            }
        }

        // <summary> Выполнено </summary>
        private bool done;
        // <summary> Сокрито для выполнения </summary>
        public bool Done
        {
            get { return done; } // Акксессор чтения
            set
            {
                done = value; // запоминаем введенное значение
                OnPropertyChanged("Done"); // сообщаем об изменении свойства
                OnPropertyChanged("IsDoneText"); // сообщаем об изменении свойства
            }
        }

        // <summary> Видимость элемента </summary>
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        private bool isEnable;
        // <summary> Сокрито для видимости элемента </summary>
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        public bool IsEnable
        {
            get { return isEnable; } // Акксессор чтения
            set
            {
                isEnable = value; // запоминаем введенное значение
                OnPropertyChanged("IsEnable"); // сообщаем об изменении свойства
                OnPropertyChanged("IsEnableText"); // сообщаем об изменении свойства
            }
        }

        // <summary> Текст для кнопки изменения </summary>
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить"; // Если изменение возможно, возвращаем одно значение
                else return "Удалить"; // Иначе другое
            }
        }

        // <summary> Текст для кнопки выполнения </summary>
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        public string IsDoneText
        {
            get
            {
                if (Done) return "Не выполнено"; // Если выполнено, возвращаем одно значение
                else return "Выполнено"; // Иначе другое
            }
        }

        // <summary> Команда для изменения </summary>
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj => { // Выполняем команду
                    IsEnable = !IsEnable; // Изменяем состояние изменения представления
                    if (IsEnable) // Если состояние не активно
                        // Возвращаем данные в контексте TaskContext
                        (MainWindow.init.DataContext as ViewModels.VM.Pages).vm_tasks.tasksContext.SaveChanges();
                });
            }
        }

        // <summary> Команда для удаления </summary>
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj => { // Выполняем команду
                    if (MessageBox.Show("Вы хотите удалить задачу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.init.DataContext as ViewModels.VM.Pages).vm_tasks.Tasks.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM.Pages).vm_tasks.TasksContext.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM.Pages).vm_tasks.tasksContext.SaveChanges();
                    }
                });
            }
        }

        // <summary> Команда выполнения </summary>
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        public RealyCommand OnDone
        {
            get
            {
                return new RealyCommand(obj => { // Выполняем команду
                    Done = !Done; // Изменяем состояние
                });
            }
        }
    }
}
