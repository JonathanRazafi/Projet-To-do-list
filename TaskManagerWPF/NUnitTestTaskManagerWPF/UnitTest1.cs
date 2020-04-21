using NUnit.Framework;
using System;
using TaskManagerWPF;

namespace NUnitTestTaskManagerWPF
{
    public class TaskmanagerTests
    {
        //Tests unitaires réalisés sur des cas d'utilisation de la méthode UpdateTask
        TaskTable Task = new TaskTable();
        TaskManagerDatabase db = new TaskManagerDatabase();

        [Test]
        public void Updatetask_Updatestatus_intime()
        {
            string Statevalue = "In time";
            Task.Startdate = DateTime.Today.AddDays(-1);
            Task.Deadline = DateTime.Today.AddDays(2);
            Task.Progress = 50;
            db.UpdateTask(Task);
            Assert.AreEqual(Statevalue, Task.Status);

        }
        
        [Test]
        public void Updatetask_Updatestatus_Delayed()
        {
            string Statevalue = "Delayed";
            Task.Startdate = DateTime.Today.AddDays(-1);
            Task.Deadline = DateTime.Today.AddDays(2);
            Task.Progress = 0;
            db.UpdateTask(Task);
            Assert.AreEqual(Statevalue, Task.Status);
        }

        [Test]
        public void Updatetask_Updatedaysleft_Daysleft()
        {
            Task.Startdate = DateTime.Today.AddDays(-1);
            Task.Deadline = DateTime.Today.AddDays(2);
            Task.Daysleft = 0;
            db.UpdateTask(Task);
            Assert.AreEqual(2, Task.Daysleft);
            Assert.Pass();
        }


    }
}