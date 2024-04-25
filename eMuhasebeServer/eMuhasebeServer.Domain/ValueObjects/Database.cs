namespace eMuhasebeServer.Domain.ValueObjects;
public sealed record Database(
    string Server,
    string DatabaseName,
    string UserId,
    string Password);
