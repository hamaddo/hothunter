using System.ComponentModel.DataAnnotations;

namespace Rgr.Data
{
    public class Client
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public int RegistryNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ReceiptNumber { get; set; }
        public string Phone { get; set; }

        public List<JobRequest> ClientsRequests { get; set; } = new List<JobRequest>();
    }
    
    public class ModifyClientDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public int RegistryNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ReceiptNumber { get; set; }
        public string Phone { get; set; }
    }

    public class JobRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string PositionName { get; set; }

        public int Salary { get; set; }
    }
    
    public class ModifyJobRequestDto
    {
        [Required] public string PositionName { get; set; }

        public int Salary { get; set; }
    }
}