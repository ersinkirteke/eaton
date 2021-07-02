using kafka.eaton.producer.api.settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace kafka.eaton.producer.api.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private IProducerSettings _settings;

        protected IProducerSettings Settings => _settings ?? (_settings = HttpContext?.RequestServices.GetService<IProducerSettings>());

    }
}
