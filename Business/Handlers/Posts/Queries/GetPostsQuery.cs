
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

namespace Business.Handlers.Posts.Queries
{

    public class GetPostsQuery : IRequest<IDataResult<IEnumerable<Post>>>
    {
        public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IDataResult<IEnumerable<Post>>>
        {
            private readonly IPostRepository _postRepository;
            private readonly IMediator _mediator;

            public GetPostsQueryHandler(IPostRepository postRepository, IMediator mediator)
            {
                _postRepository = postRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Post>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Post>>(await _postRepository.GetListAsync(p=>p.Status==true));
            }
        }
    }
}