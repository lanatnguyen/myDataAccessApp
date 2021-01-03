using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myDataAccessApp.Persistence;
using Xamarin.Forms;
using SQLite;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace myDataAccessApp
{
    public class Recipe: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string _name;
        [MaxLength(255)]
        public string Name {
            get { return _name; }
            set { if (_name == value) return;
                _name = value;
                //set notification when the name change
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Recipe> _recipes;
        public MainPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _connection.CreateTableAsync<Recipe>();
            recipeListView.ItemsSource = _recipes;
        }
        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Recipe>();
            var listRes = await _connection.Table<Recipe>().ToListAsync();
            _recipes = new ObservableCollection<Recipe>(listRes);
            recipeListView.ItemsSource = listRes;
            base.OnAppearing();
        }
        async void addButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var rec = new Recipe { Name = "Receipe " + DateTime.Now.Ticks };
            await _connection.CreateTableAsync<Recipe>();
            await _connection.InsertAsync(rec);
            _recipes.Add(rec);
            recipeListView.ItemsSource = _recipes;
        }
        async void deleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var delRec = _recipes[0];
            await _connection.DeleteAsync(delRec);
            _recipes.Remove(delRec);
            recipeListView.ItemsSource = _recipes;
        }
        async void updateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var rec = _recipes[0];
            rec.Name += " UPDATED";
            await _connection.UpdateAsync(rec);
            recipeListView.ItemsSource = _recipes;
        }
    }
}
