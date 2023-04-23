
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Informations.Queries
{

    public class GetInformationsQuery : IRequest<IDataResult<IEnumerable<Information>>>
    {
        public class GetInformationsQueryHandler : IRequestHandler<GetInformationsQuery, IDataResult<IEnumerable<Information>>>
        {
            private readonly IInformationRepository _informationRepository;
            private readonly IMediator _mediator;

            public GetInformationsQueryHandler(IInformationRepository informationRepository, IMediator mediator)
            {
                _informationRepository = informationRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Information>>> Handle(GetInformationsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Information>>(await _informationRepository.GetListAsync());
            }
        }
    }
}