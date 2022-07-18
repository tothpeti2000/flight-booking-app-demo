using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Tax
{
    public class UpdateTaxValidator : AbstractValidator<UpdateTaxCommand>
    {
        public UpdateTaxValidator()
        {
            RuleFor(tax => tax.Name).NotEmpty();
            RuleFor(tax => tax.Value).GreaterThan(0);
        }
    }

    public class UpdateTaxCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class UpdateTaxCommandHandler : IRequestHandler<UpdateTaxCommand, Unit>
    {
        private readonly ITaxRepository taxRepository;

        public UpdateTaxCommandHandler(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
        }

        public async Task<Unit> Handle(UpdateTaxCommand request, CancellationToken cancellationToken)
        {
            var tax = await taxRepository.GetAsync(request.Id, cancellationToken);

            tax.Name = request.Name;
            tax.Value = request.Value;

            taxRepository.Update(tax);
            return Unit.Value;
        }
    }
}
