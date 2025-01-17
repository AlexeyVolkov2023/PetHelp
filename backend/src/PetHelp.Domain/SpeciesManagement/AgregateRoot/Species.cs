﻿using CSharpFunctionalExtensions;
using PetHelp.Domain.SpeciesManagement.Entities;
using PetHelp.Domain.SpeciesManagement.ID;

namespace PetHelp.Domain.SpeciesManagement.AgregateRoot;

public class Species : Entity<SpeciesId>
{
    private Species(SpeciesId id) : base(id)
    {
        
    }
    private Species(
        SpeciesId id,
        string title,
        IEnumerable<Breed>? breeds)  : base(id)
    {
        Title = title;
        Breeds = breeds?.ToList() ?? [];
    }

    public string Title { get;  } 
    public IReadOnlyList<Breed>? Breeds { get;  }  
   
}