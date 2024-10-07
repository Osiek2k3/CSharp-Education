﻿using Blazored.LocalStorage;
using uiii.Contracts;
using uiii.Services.Base;

namespace uiii.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
        {
        }

        public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<Guid>();
                CreateLeaveAllocationCommand createLeaveAllocation = new() { LeaveTypeId = leaveTypeId };

                await _client.LeaveAllocationsPOSTAsync(createLeaveAllocation);
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
