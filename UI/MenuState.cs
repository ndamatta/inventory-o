namespace InventoryApp.UI;

public class MenuState
{
    public int SelectedIndex { get; set; } = 0;
    public int CurrentPage { get; set; } = 0;
    public const int PageSize = 15;

    public bool HasUnsavedChanges { get; set; } = false;
    public string? SearchQuery { get; set; } = null;
}
