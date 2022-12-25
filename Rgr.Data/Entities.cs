using System.ComponentModel.DataAnnotations;

namespace Rgr.Data
{
    public class Client
    {
        [Required] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string Name { get; set; } = String.Empty;
        [Required] public string Surname { get; set; } = String.Empty;
        [Required] public string MiddleName { get; set; } = String.Empty;
        [Required] public int RegistryNumber { get; set; }
        [Required] public string Address { get; set; } = String.Empty;
        [Required] public string Gender { get; set; } = String.Empty;
        [Required] public string ReceiptNumber { get; set; } = String.Empty;
        [Required] public string Phone { get; set; } = String.Empty;

        public List<JobRequest> ClientsRequests { get; set; } = new List<JobRequest>();
    }

    public class ModifyClientDto
    {
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string MiddleName { get; set; }
        [Required] public int RegistryNumber { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string ReceiptNumber { get; set; }
        [Required] public string Phone { get; set; }
    }

    public class JobRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string PositionName { get; set; }

        [Required] public int Salary { get; set; }
    }

    public class ModifyJobRequestDto
    {
        [Required] public string PositionName { get; set; }

        [Required] public int Salary { get; set; }
    }

    public enum OwnerShipType
    {
        OOO,
        ZAO,
        IP
    }

    public class Employer
    {
        [Required] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string Name { get; set; } = String.Empty;
        [Required] public OwnerShipType OwnerShipType { get; set; }
        [Required] public int RegistryNumber { get; set; }
        [Required] public string Address { get; set; } = String.Empty;
        [Required] public string Phone { get; set; } = String.Empty;

        public List<Offer> EmployerOffers { get; set; } = new List<Offer>();
    }

    public class ModifyEmployerDto
    {
        [Required] public string Name { get; set; } = String.Empty;
        [Required] public OwnerShipType OwnerShipType { get; set; }
        [Required] public int RegistryNumber { get; set; }
        [Required] public string Address { get; set; } = String.Empty;
        [Required] public string Phone { get; set; } = String.Empty;
    }

    public class Offer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string PositionName { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public int Salary { get; set; }
    }

    public class ModifyOfferDto
    {
        [Required] public string PositionName { get; set; }
        [Required] public int Salary { get; set; }
        [Required] public string Gender { get; set; }
    }
}