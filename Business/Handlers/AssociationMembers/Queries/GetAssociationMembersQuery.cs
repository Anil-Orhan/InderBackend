
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

namespace Business.Handlers.AssociationMembers.Queries
{

    public class GetAssociationMembersQuery : IRequest<IDataResult<IEnumerable<AssociationMember>>>
    {
        public class GetAssociationMembersQueryHandler : IRequestHandler<GetAssociationMembersQuery, IDataResult<IEnumerable<AssociationMember>>>
        {
            private readonly IAssociationMemberRepository _associationMemberRepository;
            private readonly IMediator _mediator;

            public GetAssociationMembersQueryHandler(IAssociationMemberRepository associationMemberRepository, IMediator mediator)
            {
                _associationMemberRepository = associationMemberRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AssociationMember>>> Handle(GetAssociationMembersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AssociationMember>>(await _associationMemberRepository.GetListAsync());
            }
        }
    }
}