using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudflareWorkersKvExplorer.Website.Models
{
    public class CloudflareCredentialsModel
    {
        [DisplayName("Email"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("API Key")]
        public string ApiKey { get; set; }
        [DisplayName("Account ID")]
        public string AccountId { get; set; }
    }
}
