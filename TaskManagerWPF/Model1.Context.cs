﻿//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Windows;

    //Cette classe permet d'avoir un modèle de la base de données SQL manipulable directement avec .NET
    public partial class TaskManagerDatabase : DbContext
    {
        public TaskManagerDatabase()
            : base("name=TaskManagerDatabase")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TaskTable> TaskTable { get; set; }

        TaskTable Task = new TaskTable();
        
        //La fonction principale de cette méthode est de sauvegarder des données mais le processus exact dépend du paramètre Taskid
        public void Save(int Taskid, string Category, string Title, string Startdate, string Deadline, int Progress)
        {
            try 
            {
                if (Title != "")
                {
                    Task.Taskid = Taskid;
                    Task.Category = Category;
                    Task.Title = Title;
                    if (Startdate != "") { Task.Startdate = DateTime.Parse(Startdate); }
                    if (Deadline != "") { Task.Deadline = DateTime.Parse(Deadline); } else { Task.Deadline = Task.Startdate; }
                    Task.Progress = Progress;
                    Task.Daysleft = Convert.ToInt32((Task.Deadline - DateTime.Today).TotalDays);
                    using (TaskManagerDatabase db = new TaskManagerDatabase())
                    {
                        if (Taskid == 0) { db.TaskTable.Add(Task); }//La tâche n'existe pas et est rajouté à la liste
                        else { db.Entry(Task).State = EntityState.Modified; }//La tâche existe et est mise à jour
                        db.SaveChanges();
                    }
                }
                else { MessageBox.Show("Titre obligatoire"); }
            }
            catch (System.FormatException er) { MessageBox.Show(er.Message); }
        }

        //Suppression de la tâche ayant l'identifiant égal au paramètre Taskid
        public void Delete(int Taskid)
        {
            if (Taskid != 0)
            {
                if (MessageBox.Show("Supprimer les données sélectionnées?", "Suppresion des données", MessageBoxButton.YesNo) == MessageBoxResult.Yes) //Boite de dialogue de confirmation
                {
                    using (TaskManagerDatabase db = new TaskManagerDatabase())
                    {
                        Task = db.ShowData().Find(x => x.Taskid == Taskid);
                        if (db.Entry(Task).State == EntityState.Detached)
                        { db.TaskTable.Attach(Task); }
                        db.TaskTable.Remove(Task);
                        db.SaveChanges();
                    }
                }
            }
            else { MessageBox.Show("Aucune tâche sélectionnée"); }
        }

        //Création d'une liste des tâches triées et en cours pour l'affichage
        public List<TaskTable> ShowData()
        {
            using (TaskManagerDatabase db = new TaskManagerDatabase())
            {
                List<TaskTable> Tasks = db.TaskTable.ToList();
                foreach(TaskTable element in Tasks)
                { UpdateTask(element); } //Elle fait appel à la méthode UpdateTask pour mettre à jour à chaque fois l'état des tâches
                return Tasks.OrderBy(x => x.Deadline).Where(x => x.Progress < 100).ToList<TaskTable>();
            }
        }

        //Détermination de l'état d'une tâche en fonction de ses paramètres
        public void UpdateTask(TaskTable Task)
        {
            string State;
            double Total = (Task.Deadline - Task.Startdate).TotalHours;
            double TotalNow = (DateTime.Now - Task.Startdate).TotalHours;
            double Progress = TotalNow / (Total);
            var Userprogress = Task.Progress;
            if (Progress < 1 || Userprogress != 100)
            {
                if (Progress >= 0.25 && Progress < 0.5 && Userprogress >= 25) { State = "In time"; }
                else if (Progress >= 0.5 && Progress < 0.75 && Userprogress >= 50) { State = "In time"; }
                else if (Progress >= 0.75 && Progress < 1 && Userprogress >= 75) { State = "In time"; }
                else if (Progress < 0.25) { State = "In time"; }
                else { State = "Delayed"; }
            }
            else { State = "In time"; }
            Task.Status = State;
            Task.Daysleft = Convert.ToInt32((Task.Deadline - DateTime.Today).TotalDays);
        }
    }
}
