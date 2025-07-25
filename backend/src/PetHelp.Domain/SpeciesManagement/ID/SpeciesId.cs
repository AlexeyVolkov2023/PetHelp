﻿using CSharpFunctionalExtensions;

namespace PetHelp.Domain.SpeciesManagement.ID;

public class SpeciesId : ComparableValueObject
{
    public SpeciesId()
    {
        
    }
    private SpeciesId(Guid value) 
    {
        Value = value;
    }

    public Guid Value { get;  }

    public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());

    public static SpeciesId Empty() => new(Guid.Empty);

    public static SpeciesId Create(Guid id) => new(id);
    public static implicit operator Guid(SpeciesId speciesId) => speciesId.Value;
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}