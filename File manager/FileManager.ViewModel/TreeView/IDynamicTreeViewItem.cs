namespace File_manager.FileManager.ViewModel.TreeView
{
    /// <summary>
    /// TreeView items with this interface can change by runtime
    /// </summary>
    public interface IDynamicTreeViewItem
    {
        void Update();
    }
}
