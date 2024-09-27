using System;
using System.Reflection;
using PackIT.Domain.Consts;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands
{
    public record CreatePackingListWithItems(Guid Id, string Name, int Days, Gender Gender,
        LocalizationWriteModel Localization) : ICommand;

    public record LocalizationWriteModel(string City, string Country);
}