namespace RemoteFinder.Models.Shared
{
    public class DataGridModel<T>
    {
        public List<T> Items { get; set; }
        public int Total { get; set; }
    }
}