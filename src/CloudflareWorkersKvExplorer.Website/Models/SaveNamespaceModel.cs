namespace CloudflareWorkersKvExplorer.Website.Models
{
    public class SaveNamespaceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public SaveNamespaceModel() { }

        public SaveNamespaceModel(string id,
                                  string name)
        {
            Id = id;
            Name = name;
        }
    }
}
