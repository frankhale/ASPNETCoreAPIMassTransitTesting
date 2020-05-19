using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefaultController : ControllerBase
    {
        private readonly IBus _bus;

        public DefaultController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _bus.Publish<MyMessage>(new MyMessage()
            {
                Value = "Message sent from API1"
            });

            return Ok("API1");
        }
    }
}
