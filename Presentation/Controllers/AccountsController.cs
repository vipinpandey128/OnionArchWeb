﻿using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/owners/{ownerId:guid}/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AccountsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAccounts(Guid ownerId, CancellationToken cancellationToken)
        {
            var accountsDto = await _serviceManager.AccountService.GetAllByOwnerIdAsync(cancellationToken);

            return Ok(accountsDto);
        }

        [HttpGet("{accountId:guid}")]
        public async Task<IActionResult> GetAccountById(Guid accountId, CancellationToken cancellationToken)
        {
            var accountDto = await _serviceManager.AccountService.GetByIdAsync(accountId, cancellationToken);

            return Ok(accountDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountForCreationDto accountForCreationDto, CancellationToken cancellationToken)
        {
            var response = await _serviceManager.AccountService.CreateAsync(accountForCreationDto, cancellationToken);

            return CreatedAtAction(nameof(GetAccountById), new { ownerId = response.OwnerId, accountId = response.Id }, response);
        }

        [HttpDelete("{accountId:guid}")]
        public async Task<IActionResult> DeleteAccount(Guid ownerId, Guid accountId, CancellationToken cancellationToken)
        {
            await _serviceManager.AccountService.DeleteAsync(accountId, cancellationToken);

            return NoContent();
        }
    }
}
