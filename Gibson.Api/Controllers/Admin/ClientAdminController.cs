using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gibson.Clients;
using Gibson.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gibson.Api.Controllers
{
    [Route("clients")]
    [Authorize(Policy = "TechDevsDataPolicy")]
    public class ClientAdminController : Controller
    {
        private readonly IClientService _clientService;

        public ClientAdminController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [AllowAnonymous]
        [HttpGet("/key/{clientKey}")]
        public async Task<ActionResult<Client>> GetClientByShortKey([FromRoute] string clientKey)
        {
            var res = await _clientService.GetClientByShortKey(clientKey);
            if(res == null) return new NotFoundResult();
            return new OkObjectResult(res);
        }
        
        [AllowAnonymous]
        [HttpGet("{clientId}")]
        public async Task<ActionResult<Client>> GetClient([FromRoute] Guid clientId)
        {
            var res = await _clientService.GetClient(clientId.ToString());
            if (res == null) return new NotFoundResult();
            return new OkObjectResult(res);
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetClients() => new OkObjectResult(await _clientService.GetClients());

        [HttpGet("customer")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PublicClient>>> GetClientsByCustomerEmail([FromQuery] string customerEmail) => new OkObjectResult(await _clientService.GetClientsByCustomer(customerEmail));

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient([FromBody] ClientRegistration client)
            => new OkObjectResult(await _clientService.CreateClient(client));

        [HttpPut("{clientId}")]
        public async Task<ActionResult<Client>> UpdateClient(string clientId, [FromBody] Client client) => new OkObjectResult(await _clientService.UpdateClient(clientId, client));

        [HttpDelete("{clientId}")]
        public async Task<ActionResult<Client>> DeleteClient([FromRoute] string clientId) => new OkObjectResult(await _clientService.DeleteClient(clientId));
    }
}