
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Informations.Queries
{
    public class GetInformationQuery : IRequest<IDataResult<Information>>
    {
        public int Id { get; set; }

        public class GetInformationQueryHandler : IRequestHandler<GetInformationQuery, IDataResult<Information>>
        {
            private readonly IInformationRepository _informationRepository;
            private readonly IMediator _mediator;

            public GetInformationQueryHandler(IInformationRepository informationRepository, IMediator mediator)
            {
                _informationRepository = informationRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Information>> Handle(GetInformationQuery request, CancellationToken cancellationToken)
            {
                var information = await _informationRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Information>(information);
            }
        }
    }
}
