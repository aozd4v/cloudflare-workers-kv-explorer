using System.Collections.Generic;

namespace CloudflareWorkersKvExplorer.Website.Models
{
    public class ViewNamespaceModel
    {
        public string Name { get; set; }
        public IEnumerable<string>  Keys { get; set; }

        public ViewNamespaceModel(string name,
                                  IEnumerable<string> keys)
        {
            Name = name;
            Keys = keys;
        }
    }
}
