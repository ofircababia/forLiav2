namespace Web_Api.DTO
{
    public class EmployeeSignInDTOcs
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? PasswordKey { get; set; }

        bool isOkay { get; set; }
    }
}
