﻿namespace PetHelp.Application.Dto;

public record FullNameDto(
    string Name,
    string Surname,
    string? Patronymic);