using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TaskManager_Graf.Classes;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager_Graf.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }
    }
}
