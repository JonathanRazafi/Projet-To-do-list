//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManagerWPF
{
    using System;

    //Cette classe modélise une tâche et ses différentes propriétés 
    public partial class TaskTable
    {
        public int Taskid { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public System.DateTime Startdate { get; set; } = DateTime.Today;
        public System.DateTime Deadline { get; set; }
        public int Progress { get; set; } = 0;
        public string Status { get; set; } = "In time";
        public int Daysleft { get; set; }
    }
}

