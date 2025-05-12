using System;

namespace JoaquinCustomer.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class CreateCustomerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class UpdateCustomerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}