using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.ViewModel.AttachedTreeView;
using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace File_manager.FileManager.ViewModel
{
    public class FileAttachedTreeViewModel: NotifyViewModel
    {
        public ObservableCollection<AttachedItem> AttachedItems { get; private set; }

        public FileAttachedTreeViewModel()
        {
            AttachedItems = new ObservableCollection<AttachedItem>();

            var favorites = FileManagerViewModel.Instance.Model.Favorites;

            foreach (var favorite in favorites)
            {
                AttachedItems.Add(AttachedItem.GetInstance(favorite));
            }
        }

        public void Attach(string name)
        {
            if (FileManagerViewModel.Instance.Model.Favorites.Contains(name)) return;

            FileManagerViewModel.Instance.Model.Favorites.Add(name);
            AttachedItems.Add(AttachedItem.GetInstance(name));
        }

        public void UnAttach(AttachedItem item)
        {
            FileManagerViewModel.Instance.Model.Favorites.Remove(item.Path);
            AttachedItems.Remove(item);
        }

    }
}
