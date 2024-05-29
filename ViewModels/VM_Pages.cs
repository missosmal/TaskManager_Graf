using System;
using System.Collections.Generic;
using System.Text;
using TaskManager_Graf.Classes;

namespace TaskManager_Graf.ViewModels
{
    public class VM_Pages : Notification
    {
        public VM_Tasks vm_tasks = new VM_Tasks();
        public VM_Pages()
        {
            MainWindow.init.frame.Navigate(new Views.Main(vm_tasks));
        }
        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.Close();
                });
            }
        }
    }
}
