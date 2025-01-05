using CSharpFunctionalExtensions;
using PetHelp.Domain.SpeciesManagement.Entities;

namespace PetHelp.Domain.SpeciesManagement.AgregateRoot;

public class Species : Entity<Guid>
{
    public Species(
        Guid id,
        string title,
        List<Breed> breeds)  : base(id)
    {
        Title = title;
        Breeds = breeds;
    }

    public string Title { get;  } 
    public List<Breed>? Breeds { get;  }  
   
}