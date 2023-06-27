using System.Text.Json.Serialization;

namespace JobApplicationFluentValidation.Models
{
    public class FileModel
    {
        public int FileModelId { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte[] Data { get; set; }
        [JsonIgnore]
        public virtual ICollection<JobApplication> Applicants { get; } = new List<JobApplication>();
    }
}
