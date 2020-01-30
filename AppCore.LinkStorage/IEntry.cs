namespace AppCore.LinkStorage.API
{
    public interface IEntry
    {
        string Id { get; set; }

        StorageURI Uri { get; set; }
    }
}
