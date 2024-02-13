namespace AirCheap.Server.DataTransferObjects;

public record UserAuthenticationDto
(
    string Username,
    string Password
);

public record UserInsertDto
(
    string Username,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    string Email
);

public record UserUpdateDto
(
    string Username,
    string FirstName,
    string LastName,
    string Email
);

public record UserUpdateUsernameDto
(
    string Username,
    string NewUsername
);

public record UserUpdatePasswordDto
(
    string Username,
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword
);