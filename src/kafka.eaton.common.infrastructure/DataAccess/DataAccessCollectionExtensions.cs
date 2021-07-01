using Microsoft.Extensions.DependencyInjection;
using System;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public static class DataAccessCollectionExtensions
    {
        public static IServiceCollection AddAccessData(this IServiceCollection collection,
            Action<DataAccessOptions> setupAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);
            return collection.AddTransient<TelemetryDataAccess>();
        }
    }
}
