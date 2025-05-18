namespace PetHelp.Domain.Shared;

public interface ISoftDeletable
{
    void SoftDelete();
    void Restore();
}