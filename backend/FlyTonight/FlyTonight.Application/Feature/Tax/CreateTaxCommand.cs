using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Tax
{
    public class CreateTaxValidator : AbstractValidator<CreateTaxCommand> 
    {
        public CreateTaxValidator()
        {
            RuleFor(tax => tax.Name).NotEmpty();
            RuleFor(tax => tax.Value).GreaterThan(0);
        }
    }

    public class CreateTaxCommand : IRequest
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, Unit>
    {
        private readonly ITaxRepository taxRepository;

        public CreateTaxCommandHandler(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
        }

        public Task<Unit> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            taxRepository.Add(new Domain.Models.Tax
            {
                Name = request.Name,
                Value = request.Value,
            });
            return Unit.Task;
        }
    }
}
