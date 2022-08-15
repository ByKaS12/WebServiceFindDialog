using Microsoft.AspNetCore.Mvc;
using WebServiceFindDialog.Models;

namespace WebServiceFindDialog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RGDialogsClientsController : ControllerBase
    {

        private readonly ILogger<RGDialogsClientsController> _logger;

        public RGDialogsClientsController(ILogger<RGDialogsClientsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Guid FindDialog([FromQuery] IEnumerable<Guid> IDClients)
        {
            if (IDClients.Count() == 0)
                 throw new Exception("No users entered");
            List<RGDialogsClients> InitListDialogs = new RGDialogsClients().Init();
            var Answer = InitListDialogs.GroupBy(u => u.IDRGDialog).Where(dialog => IDClients.All(condition => dialog.Any(client => client.IDClient == condition)));
            //Sorting and solving a logical equation type A * (B + C)
            
            if (Answer.Count() == 0)
                return Guid.Empty;
            else
                return Answer.First().Key;
            
           
        }
    }
}