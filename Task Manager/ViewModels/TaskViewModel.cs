using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Task_Manager.DataService;
using Task_Manager.Models;
using Task = Task_Manager.Models.Task;
namespace Task_Manager.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
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

        public ObservableCollection<TaskChecklist> TaskChecklist { get; set; }
        public ICommand IAddNewTask => new RelayCommand(AddNewTask);
        private readonly TaskDataService _taskDataService;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Task> _tasks;

        

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        private void LoadTasks()
        {
            var TaskList = _taskDataService.LoadTasks();
            Tasks=new ObservableCollection<Task>(TaskList);
        }

        public void AddNewTask()
        {
            Task newTask = new Task
            {
                Title=this.Title,
                Description=this.Description,
                Id=_taskDataService.GenerateNewTaskId(),
                IsCompleted=false,
                StartDate= DateTime.Now,
                TaskCategory=TaskCategory.Education,
                TaskImportance=this.TaskImportance,
                TaskState=TaskState.Late,
                Timer=new TimeSpan(0)
            };
            _taskDataService.AddTask(newTask);
            LoadTasks();

            ClearFields();
        }
        public void UpdateTask(Task updateTask)
        {
            _taskDataService.UpdateTask(updateTask);
            LoadTasks();
        }
        public void DeleteTask(int taskId)
        {
            _taskDataService.DeleteTasks(taskId);
            LoadTasks();
        }

        private void ClearFields()
        {
            Title = "";
            OnPropertyChanged(Title);
            Description="";
            OnPropertyChanged(Description);
            DueDate = DateTime.Now;
            OnPropertyChanged(nameof(DueDate));
            TaskChecklist.Clear();
            OnPropertyChanged(nameof(TaskChecklist));
        }
        public TaskViewModel()
        {
            

            _taskDataService = new TaskDataService(); //initializing the TaskDataService
            TaskChecklist = new ObservableCollection<TaskChecklist>();
            DueDate=DateTime.Now;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
