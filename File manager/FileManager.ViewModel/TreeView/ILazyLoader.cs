namespace File_manager.FileManager.ViewModel.TreeView
{

    public interface ILazyLoader
    {
        void Load();

        void Unload();
    }
}
