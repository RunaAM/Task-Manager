using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Task_Manager.Models;
using Task = Task_Manager.Models.Task;

namespace Task_Manager.DataService
{
    public class TaskDataService
    {

        private readonly string _filePath;
        private readonly string folderName = "TaskManager";
        private readonly string filename = "tasks.json";
        public TaskDataService()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //Get the folder of the application in roaming
            string appFolder = Path.Combine(appDataPath, folderName);

            string dataFolder = Path.Combine(appFolder, "data");

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            _filePath= Path.Combine(dataFolder, filename);

            InitializeFile();
        }
        private void InitializeFile()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(new List<Task>()));
            }

            //Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folderName));
        }

        public List<Task> LoadTasks()
        {
            string fileContent = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Task>>(fileContent);
        }

        public void SaveTasks(List<Task> tasks)
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        public void AddTask(Task newTask)
        {
            newTask.Id=GenerateNewTaskId();

            var task = LoadTasks();
            task.Add(newTask);
            SaveTasks(task);
        }

        public void UpdateTask(Task updateTask)
        {
            var tasks = LoadTasks();
            var taskIndex = tasks.FindIndex(t => t.Id==updateTask.Id);
            if (taskIndex!=-1)
            {
                tasks[taskIndex]=updateTask;
                SaveTasks(tasks);
            }
        }

        public void DeleteTasks(int taskId)
        {
            var tasks = LoadTasks();
            tasks.RemoveAll(t => t.Id==taskId);
            SaveTasks(tasks);
        }
        public int GenerateNewTaskId()
        {
            var tasks = LoadTasks();
            if (!tasks.Any())
            {
                return 1;
            }
            int maxID = tasks.Max(t => t.Id);
            return maxID +1;
        }
    }
}
