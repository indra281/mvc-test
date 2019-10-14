using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nClam;

namespace AVDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            try
            {

           
                var clam = new ClamClient("localhost", 3310);
                var pingResult = await clam.PingAsync();

                if (!pingResult)
                {
                    Console.WriteLine("test failed. Exiting.");
                    return null;
                }

                Console.WriteLine("connected.");
                var scanResult = await clam.ScanFileOnServerAsync("C:\\Users\\Sujit.Kadam\\Documents\\Final Team.txt");  //any file you would like!

                switch (scanResult.Result)
                {
                    case ClamScanResults.Clean:
                        Console.WriteLine("The file is clean!");
                        break;
                    case ClamScanResults.VirusDetected:
                        Console.WriteLine("Virus Found!");
                        Console.WriteLine("Virus name: {0}", scanResult.InfectedFiles.First().VirusName);
                        break;
                    case ClamScanResults.Error:
                        Console.WriteLine("Woah an error occured! Error: {0}", scanResult.RawResult);
                        break;
                }

                return new string[] { "Status", "Success" };
            }
            catch(Exception e)
            {
                Console.WriteLine("not connected.");
                return new string[] { "Status", "Failed" };
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
