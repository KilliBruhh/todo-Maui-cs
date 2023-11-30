using Microsoft.VisualBasic;
using static MauiTodo.MainPage;

namespace MauiTodo;

public partial class AddTodo : ContentPage
{

	private MainPage _MainPage;

	public AddTodo(MainPage mainPage)
	{
		InitializeComponent();
		_MainPage = mainPage;
    }

	public async void onNavigateBack(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
	}

	public async void onAddTodo(object sender, EventArgs e)
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

		_MainPage.AddItemToList(toDoItem);

		await Task.Delay(2000);
		Navigation.PopAsync();
	}
}