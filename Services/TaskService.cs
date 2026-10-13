using TaskList.Models;

namespace TaskList.Services
{
    public class TaskService
    {
        public static List<TaskItems> taskItems = new List<TaskItems>();

        public List<TaskItems> getTask()
        {
            return taskItems;
        }

        public void addTask(TaskItems data)
        {
            taskItems.Add(data);
        }
        public bool editTask(TaskItems data, int id)
        {
            var item = taskItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return false;
            }
            item.Title = data.Title;
            item.Description = data.Description;
            item.Status = data.Status;

            return true;
        }
        public bool deleteTask(int id)
        {
            var item = taskItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                taskItems.Remove(item);
                return true;
            }

            return false;
            
        }
    }
}