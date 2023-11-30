using System.Collections.ObjectModel;
using MauiTodo;

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

        // Navigate to the add page
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTodo(this));
        }

        // Function to add item to list
        public void AddItemToList(ToDoItem newItem)
        {
            ToDoItems.Add(newItem);
            todoListView.ItemsSource = null; // refresh
            todoListView.ItemsSource = ToDoItems;
        }

        // Class for the data int he ToDo
        public class ToDoItem
        {
            public string TaskName { get; set; }
            public DateTime DueDate { get; set; }
            public int Priority { get; set; }
        }

    }
}