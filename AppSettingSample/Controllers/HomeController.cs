using AppSettingSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.Extensions.Azure;

namespace AppSettingSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TwilioSettings _twilioSettings;
        private readonly IConfiguration _configuration;
        private readonly IOptions<TwilioSettings> _twilioOptions;

        private SocialLoginSettings _socialLoginSettings;
        private readonly IOptions<SocialLoginSettings> _socialOptions;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptions<TwilioSettings> options,
            TwilioSettings twilioSettings, SocialLoginSettings socialLoginSettings, IOptions<SocialLoginSettings> socialOptions)
        {
            //_configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            _twilioSettings = twilioSettings;

            _twilioOptions = options;

            _logger = logger;
            _configuration = configuration;
            _socialLoginSettings = socialLoginSettings;
            _socialOptions = socialOptions;
            // _twilioSettings = new TwilioSettings();
            // _configuration.GetSection("Twilio").Bind(_twilioSettings);
        }

        public IActionResult Index()
        {
            var connection = _configuration.GetConnectionString("DefaultConnection");
          
            var google = _socialOptions.Value.Google;
            var phone = _twilioSettings.phone;
            var id = _twilioOptions.Value.id;
            var facebook= _socialLoginSettings.Facebook;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
