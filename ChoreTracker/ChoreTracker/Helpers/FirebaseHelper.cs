using ChoreTracker.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoreTracker.Helpers
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-f2f5c.firebaseio.com/");

        //Method for getting all TASKS Objects from the database
        public async Task<List<Tasks>> GetAllTasks()
        {
            return (await firebase
                .Child("Tasks")
                .OnceAsync<Tasks>()).Select(item => new Tasks
                {
                    TaskTitle = item.Object.TaskTitle,
                    TaskShortDescription = item.Object.TaskShortDescription,
                    TaskLongDescription = item.Object.TaskLongDescription,
                    TaskWorthSwitchTime = item.Object.TaskWorthSwitchTime,
                    TaskWorthDollarValue = item.Object.TaskWorthDollarValue
                }).ToList();

        }


        //Method for adding a single TASKS Object to the database
        public async Task AddTask(string taskTitle, string taskShortDesc, string taskLongDesc, double taskSwitchValue, double taskDollarValue)
        {
            await firebase.Child("Tasks")
                .PostAsync(new Tasks() {TaskTitle = taskTitle, TaskShortDescription = taskShortDesc, TaskLongDescription = taskLongDesc, TaskWorthSwitchTime = taskSwitchValue, TaskWorthDollarValue = taskDollarValue});
        }


        //Method for extracting a singular TASKS Object from the database
        public async Task<Tasks> GetTask(string taskTitle)
        {
            var allTasks = await GetAllTasks();
            await firebase
                .Child("Tasks")
                .OnceAsync<Tasks>();
            return allTasks.Where(a => a.TaskTitle == taskTitle).FirstOrDefault();
        }


        //Method for updating a selected TASKS Object
        public async Task UpdateTasks(string taskTitle, string taskShortDesc, string taskLongDesc, double taskSwitchValue, double taskDollarValue)
        {
            var toUpdateTask = (await firebase
                .Child("Tasks")
                .OnceAsync<Tasks>()).Where(a => a.Object.TaskTitle == taskTitle).FirstOrDefault();

            await firebase
                .Child("Tasks")
                .Child(toUpdateTask.Key)
                .PutAsync(new Tasks() { TaskTitle = taskTitle, TaskShortDescription = taskShortDesc, TaskLongDescription = taskLongDesc, TaskWorthSwitchTime = taskSwitchValue, TaskWorthDollarValue = taskDollarValue });
        }


        //Method for deleting a selected TASKS Object
        public async Task DeleteTask(string taskTitle)
        {
            var toDeleteTask = (await firebase
                .Child("Tasks")
                .OnceAsync<Tasks>()).Where(a => a.Object.TaskTitle == taskTitle).FirstOrDefault();
            await firebase.Child("Tasks").Child(toDeleteTask.Key).DeleteAsync();
        }


        //Method for adding a single CHILD Object to the database
        public async Task AddChild(string childName)
        {
            


            await firebase.Child("Children")
                .PostAsync(new Child() { ChildName = childName, ChildEarnedDollar = 0.00, ChildEarnedTime = 0.00 });
        }


        //Method for updating a selected CHILD Object
        public async Task UpdateChild(string childName, double earnedValue, double earnedTime)
        {
            var toUpdateChild = (await firebase
                .Child("Children")
                .OnceAsync<Child>()).Where(a => a.Object.ChildName == childName).FirstOrDefault();

            var currentValue = toUpdateChild.Object.ChildEarnedDollar;
            var currentTime = toUpdateChild.Object.ChildEarnedTime;

            await firebase
                .Child("Children")
                .Child(toUpdateChild.Key)
                .PutAsync(new Child() { ChildName = childName, ChildEarnedDollar = (currentValue + earnedValue), ChildEarnedTime = (currentTime + earnedTime) });
        }


        //Method for getting all CHILD Objects from the database
        public async Task<List<Child>> GetAllChildren()
        {
            return (await firebase
                .Child("Children")
                .OnceAsync<Child>()).Select(item => new Child
                {
                    ChildName = item.Object.ChildName,
                    ChildEarnedDollar = item.Object.ChildEarnedDollar,
                    ChildEarnedTime = item.Object.ChildEarnedTime
                }).ToList();

        }


        //Method get extracting a singular CHILD Object from the database
        public async Task<Child> GetChild(string childName)
        {
            var allChildren = await GetAllChildren();
            await firebase
                .Child("Children")
                .OnceAsync<Child>();
            return allChildren.Where(a => a.ChildName == childName).FirstOrDefault();
        }
    }
}
