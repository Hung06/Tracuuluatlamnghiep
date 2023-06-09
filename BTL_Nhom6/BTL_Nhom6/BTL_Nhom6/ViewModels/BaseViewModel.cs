using BTL_Nhom6.Models;
using BTL_Nhom6.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BTL_Nhom6.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IChapters<Chapters> ChaptersData => DependencyService.Get<IChapters<Chapters>>();
        public IUsers<Users> UsersData => DependencyService.Get<IUsers<Users>>();
        public IArticles<Articles> ArticlesData => DependencyService.Get<IArticles<Articles>>();
        public ISections<Sections> SectionsData => DependencyService.Get<ISections<Sections>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
