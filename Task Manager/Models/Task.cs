using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public Boolean IsCompleted { get; set; }
        public TimeSpan Timer { get; set; }
        public TaskCategory TaskCategory { get; set; }
        public TaskImportance TaskImportance { get; set; }
        public TaskState TaskState { get; set; }

        public ObservableCollection<TaskChecklist>  TaskChecklist {get;set;}
    }
    public enum TaskState
    {
        InProgress,
        Complete,
        NotStarted,
        Late,
        Archived,
        Deleted
    }

    public enum TaskCategory
    {
        Work,
        Personal,
        Home,
        HealthBeing,
        Finance,
        Shopping,
        SocialFamily,
        Education,
        Travel,
        Errands,
        HobbiesLeisure,
        VolunteeringCommunity,
        BirthdaysAnniversaries,
        Projects,
        LongTermGoal
    }

    public enum TaskImportance
    {
        Low,
        Medium,
        High,
        Critical
    }
}
