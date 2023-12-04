using System.Collections.ObjectModel;
using System.Windows.Input;
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
        // Replace Item in the list
        public void EditItemInList(ToDoItem replacement, ToDoItem original)
        {
            var indexOriginal = ToDoItems.IndexOf(original);
            ToDoItems[indexOriginal] = replacement;
        }

        // Delete and Edit function
        public void OnDeleteItemClicked(object sender, EventArgs e)
        {
            var item = (ToDoItem)((Button)sender).CommandParameter;
            ToDoItems.Remove(item);
        }
        public async void OnEditItemClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var itemSelected = button.CommandParameter as ToDoItem;
            await Navigation.PushAsync(new EditPage(this,itemSelected));
            
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