﻿namespace Front.DTOs
{
    public record UserResponse(
        Guid Id,
        string? Password,
        string FirstName,
        string SurName,
        string Email,
        string Company,
        int Status);
}



