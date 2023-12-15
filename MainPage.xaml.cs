using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Input;
using MauiTodo;
using Newtonsoft.Json;

namespace MauiTodo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public ObservableCollection<ToDoItem> ToDoItems { get; set; } = new ObservableCollection<ToDoItem>();
        
        
        public MainPage()
        {
            InitializeComponent();     
            todoListView.ItemsSource = ToDoItems;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ToDoItems.Clear();
            var todoItems = await GetAllTodo();
            foreach (var item in todoItems) 
            {
                ToDoItems.Add(item);
            }
        }

        // Navigate to the add page
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTodo(this));
        }

        // Function to add item to list
        public void AddItemToList(ToDoItem newItem) 
        {
            AddTodo(newItem);
            /*
                todoListView.ItemsSource = null; // refresh
                todoListView.ItemsSource = ToDoItems;
            */

        }
        // Replace Item in the list
        public void EditItemInList(ToDoItem replacement, ToDoItem original)
        {
            UpdateTodo(replacement);
        }

        // Delete and Edit function
        public void OnDeleteItemClicked(object sender, EventArgs e)
        {
            var item = (ToDoItem)((Button)sender).CommandParameter;
            DeleteTodo(item);
        }
        public async void OnEditItemClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var itemSelected = button.CommandParameter as ToDoItem;
            await Navigation.PushAsync(new EditPage(this, itemSelected, itemSelected.Id));
            
        }

        // API Functions
        public async Task<List<ToDoItem>> GetAllTodo()
        {
            HttpClient client = new HttpClient();
            String response = await client.GetStringAsync("http://10.0.2.2:5031/api/todo");

            return JsonConvert.DeserializeObject<List<ToDoItem>>(response);
        }
        public async void DeleteTodo(ToDoItem t)
        {
            int id = t.Id;
            ToDoItems.Remove(t);

            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync($"http://10.0.2.2:5031/api/todo/{id}");
            
        }
        public async void AddTodo(ToDoItem t)
        {
            var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://10.0.2.2:5031/api/todo", content);
        }
        public async void UpdateTodo(ToDoItem t)
        {
            int id = t.Id;
            var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var response = await client.PutAsync($"http://10.0.2.2:5031/api/todo/{id}", content);
        }

        // Class for the data int he ToDo
        public class ToDoItem
        { 
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime DueDate { get; set; }
            public int Priority { get; set; }
        }

    }
}