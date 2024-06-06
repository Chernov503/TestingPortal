namespace Front.DTOs
{
    public record RegisterUserRequest(string password,
                                      string firstName,
                                      string surName,
                                      string email,
                                      string company);

}
