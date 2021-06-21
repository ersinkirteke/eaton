using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public static class TelemetryDataAccessCollectionExtensions
    {
        public static IServiceCollection AddTelemetryAccessData(this IServiceCollection collection,
            Action<TelemetryDataAccessOptions> setupAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);
            return collection.AddTransient<TelemetryDataAccess>();
        }
    }
}
