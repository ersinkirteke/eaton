using FluentValidation;
using kafka.eaton.common.domain.models;

namespace kafka.eaton.producer.api.validators
{
    public class TelemetryValidator : AbstractValidator<TelemetryDto>
	{
		public TelemetryValidator()
		{
			RuleFor(x => x.DeviceName).NotEmpty();
			RuleFor(x => x.DeviceName).Length(0, 40);
		}
	}
}
