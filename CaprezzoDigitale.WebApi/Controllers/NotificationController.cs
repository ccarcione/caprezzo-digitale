using CaprezzoDigitale.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebPush;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CaprezzoDigitale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly ILogger<NotificationController> _logger;
        private readonly Options _options;

        public NotificationController(ApplicationDbContext context,
            ILogger<NotificationController> logger,
            Options options)
        {
            _context = context;
            _logger = logger;
            _options = options;
        }
        
        [HttpPost("save-subscription")]
        public async Task<IActionResult> SaveSubscriptionPost(Subscription subscription)
        {
            if (subscription == null)
            {
                return new ForbidResult("Subscription can't be empty.");
            }

            Subscription dbSubscription = _context.Subscriptions.Where(s =>
                    s.Endpoint == subscription.Endpoint
                    && s.Keys.P256dh == subscription.Keys.P256dh
                    && s.Keys.Auth == subscription.Keys.Auth)
                .FirstOrDefault();
            if (dbSubscription == null)
            {
                _context.Subscriptions.Add(subscription);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("send-notification-sample")]
        public async Task<IActionResult> SendNotificationPost(Subscription subscription)
        {
            var subject = _options.WebApiOptions["notification_subject"];
            var publicKey = _options.WebApiOptions["notification_publicKey"];
            var privateKey = _options.WebApiOptions["notification_privateKey"];

            var pushsubscription = new PushSubscription(subscription.Endpoint, subscription.Keys.P256dh, subscription.Keys.Auth);
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);

            // https://developer.chrome.com/blog/notifying-you-of-changes-to-notifications/
            string payload = JsonSerializer.Serialize(new
            {
                notification = new
                {
                    data = new
                    {
                        url = "http://www.google.it"
                    },
                    title = "Fun Fun Fun",
                    vibrate = new int[] { 100, 50, 100 }
                }
            });

            var webPushClient = new WebPushClient();
            try
            {
                await webPushClient.SendNotificationAsync(pushsubscription, payload, vapidDetails);
                return Ok();
            }
            catch (WebPushException exception)
            {
                _logger.LogError("Http STATUS code" + exception.StatusCode);
                throw exception;
            }
        }

        [HttpDelete("delete-subscription")]
        public async Task<IActionResult> Delete(Subscription subscription)
        {
            if (subscription == null)
            {
                return new ForbidResult("Subscription can't be empty.");
            }

            Subscription dbSubscription = _context.Subscriptions.Where(s =>
                    s.Endpoint == subscription.Endpoint
                    && s.Keys.P256dh == subscription.Keys.P256dh
                    && s.Keys.Auth == subscription.Keys.Auth)
                .FirstOrDefault();
            if (dbSubscription == null)
            {
                return Ok("Subscription Not Found.");
            }

            _context.Remove(dbSubscription);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
