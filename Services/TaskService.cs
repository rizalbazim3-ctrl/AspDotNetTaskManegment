using TaskList.Models;

namespace TaskList.Services
{
    public class TaskService
    {
        public static List<TaskItems> taskItems = new List<TaskItems>();

        public void addTask(TaskItems data)
        {
            taskItems.Add(data);
        }
        public void editTask(TaskItems data, int id)
        {
            var item = taskItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Id = data.Id;
                item.Title = data.Title;
                item.Description = data.Description;
                item.Status = data.Status;

            }
        }
        public void deleteTask(int id)
        {
            taskItems.RemoveAll(x => x.Id == id);
        }
    }
}