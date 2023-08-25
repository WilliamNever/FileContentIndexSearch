namespace FileSearchByIndex.Core.Enums
{
    [Flags]
    public enum EnKeyWordsType
    {
        None = 0,
        Comment = 1,
        MethodOrClassName = 2,
        CommandName = 4,
        FlatText = 8,
    }
}
