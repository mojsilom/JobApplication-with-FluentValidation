using System.ComponentModel.DataAnnotations;

namespace JobApplicationFluentValidation.Models
{
    public class JobApplication
    {
            public int ApplicationID { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? ConfirmEmail { get; set; }
            [DataType(DataType.Date)]
            public DateTime? DateOfBirth { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? CountryCode { get; set; }
            public string? Phone { get; set; }
            public string? HomePhone { get; set; }
            public string? WorkingPreferences { get; set; }
            public string? Position { get; set; }
            public string? CoverLetter { get; set; }
            public FileModel? FileUpload { get; set; }
            public int? FileUploadFileModelId { get; set; }
        }
    }
