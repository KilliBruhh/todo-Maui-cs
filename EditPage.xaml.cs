namespace MauiTodo;
using static MauiTodo.MainPage;


public partial class EditPage : ContentPage
{
    private MainPage _MainPage;
    private ToDoItem item;

    public EditPage(MainPage mainPage, ToDoItem itemSelected)
	{
		InitializeComponent();
        _MainPage = mainPage;
        this.item = itemSelected;
        SetFormData();
    }



    public void SetFormData()
    {
        taskName.Text = item.TaskName;
        taskExpDate.Date = item.DueDate;
        taskPriority.SelectedIndex = item.Priority - 1;
    }

    public async void onNavigateBack(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    public async void onEditTodo(object sender, EventArgs e)
    {
        string tn = taskName.Text;
        DateTime td = taskExpDate.Date;
        int tp = (int)taskPriority.SelectedItem;

        ToDoItem toDoItem = new ToDoItem
        {
            TaskName = tn,
            DueDate = td,
            Priority = tp,
        };

        _MainPage.EditItemInList(toDoItem, this.item);

        Navigation.PopAsync();
    }
}