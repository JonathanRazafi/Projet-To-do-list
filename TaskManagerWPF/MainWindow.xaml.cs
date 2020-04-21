using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace TaskManagerWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    
    //Cette classe modélise l'interface de l'application
    public partial class MainWindow : Window
    {
        TaskManagerDatabase db = new TaskManagerDatabase();
        int Taskid = 0;

        public MainWindow()
        {
            InitializeComponent();
            Dataview.ItemsSource = db.ShowData(); //Affichage des données au lancement de l'application
        }

        //Méthode liée au clic du bouton Save
        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int progress = 0;
                if (Progresstext.Text != "") { progress = Convert.ToInt32(Progresstext.Text); }
                db.Save(Taskid, Categorytext.Text, Titletext.Text, Startdatetext.Text, Deadlinetext.Text, progress); //Appel de la méthode Save avec les données des zones de texte en paramètre
                Dataview.ItemsSource = db.ShowData(); //Mise à jour de l'affichage à la fin du processus
            }
            catch (System.FormatException er) { MessageBox.Show(er.Message); }
            Clear();
        }

        //Méthode liée au clic du bouton delete
        private void Deletebutton_Click(object sender, RoutedEventArgs e)
        {
            db.Delete(Taskid); //Appel de la méthode Delete pour la tâche correspondant au Taskid sélectionné sur le tableau de l'interface
            Dataview.ItemsSource = db.ShowData(); //Mise à jour de l'affichage à la fin du processus
            Clear();
        }

        //Méthode liée au clic du bouton Cancel qui permet d'annuler la sélection d'une tâche
        private void Cancelbutton_Click(object sender, RoutedEventArgs e) {Clear();}

        //Méthode liée au tableau Dataview qui permet de sélectionner une tâche de la liste par un double clic
        private void Dataview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<TaskTable> T = db.ShowData(); //Elle utilise la fonction ShowData() car la récupération directe des données du tableau est complexe
            int Taskindex = Dataview.SelectedIndex;
            Categorytext.Text = T[Taskindex].Category;
            Titletext.Text = T[Taskindex].Title;
            Startdatetext.Text = T[Taskindex].Startdate.ToString("dd/MM/yyyy");
            Deadlinetext.Text = T[Taskindex].Deadline.ToString("dd/MM/yyyy");
            Progresstext.Text = Convert.ToString(T[Taskindex].Progress);
            Taskid = T[Taskindex].Taskid;
        }

        //Méthode qui perment de vider les zones de texte de l'interface et de remettre à zéro la valeur de Taskid
        private void Clear()
        {
            Categorytext.Text = Titletext.Text = Startdatetext.Text = Deadlinetext.Text = Progresstext.Text = "";
            Taskid = 0;
        }
    }
}
