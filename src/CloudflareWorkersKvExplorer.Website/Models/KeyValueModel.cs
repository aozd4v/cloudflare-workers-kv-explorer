namespace CloudflareWorkersKvExplorer.Website.Models
{
    public class KeyValueModel
    {
        public string Key { get; set; }
        public string Json { get; set; }

        public KeyValueModel() { }

        public KeyValueModel(string key, string json)
        {
            Key = key;
            Json = json;
        }
    }
}
