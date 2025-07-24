namespace PetHelp.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return Error.NotFound("record.not.found", $"record not found{forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid{label}length)");
        }
    }

    public static class Volunteer
    {
        public static ErrorList AlreadyExist()
        {
            return Error.Validation("record.already.exist", "Volunteer already exist");
        }
    }
    
    public static class FileProvider
    {
        public static Error NotFound(string? objectName = null)
        {
            var label = objectName == null ? "" : " " + objectName + " ";
            return Error.NotFound("file.not_found", $"{objectName} not found");
        }
    }
}