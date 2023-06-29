﻿using Otus._2.DAL.Model;
using Otus._2.Dto;
using Riok.Mapperly.Abstractions;

namespace Otus._2.Mapping;

[Mapper]
public static partial class UserMapper
{
    public static partial User ToDbModel(this CreateUserDto user);

    public static partial UserDto ToDto(this User user);

    [MapperIgnoreSource(nameof(UpdateUserDto.Id))]
    public static partial void UpdateDbModel(this UpdateUserDto userDto, User user);
}