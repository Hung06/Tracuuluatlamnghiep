using net04_btl_nhom1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace net04_btl_nhom1.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string title;
        private string content;
        private string createTime;
        private string updateTime;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(content);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        public string CreateTime
        {
            get => createTime;
            set => SetProperty(ref createTime, value);
        }

        public string UpdateTime
        {
            get => updateTime;
            set => SetProperty (ref updateTime, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Chapters newItem = new Chapters()
            {
                
                Title = Title,
                Content = Content,
                CreateTime = CreateTime,
                UpdateTime = UpdateTime,
            };

            await ChaptersData.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
