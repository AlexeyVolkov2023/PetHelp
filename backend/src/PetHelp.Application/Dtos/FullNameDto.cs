﻿namespace PetHelp.Application.Dtos;

public record FullNameDto(
    string Name,
    string Surname,
    string? Patronymic);