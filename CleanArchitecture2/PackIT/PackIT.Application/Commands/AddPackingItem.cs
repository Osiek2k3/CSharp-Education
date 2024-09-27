using System;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands
{
    public record AddPackingItem(Guid PackingListId, string Name, int Quantity) : ICommand;
}