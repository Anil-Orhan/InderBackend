
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Posts.Queries
{
    public class GetPostQuery : IRequest<IDataResult<Post>>
    {
        public int Id { get; set; }

        public class GetPostQueryHandler : IRequestHandler<GetPostQuery, IDataResult<Post>>
        {
            private readonly IPostRepository _postRepository;
            private readonly IMediator _mediator;

            public GetPostQueryHandler(IPostRepository postRepository, IMediator mediator)
            {
                _postRepository = postRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Post>> Handle(GetPostQuery request, CancellationToken cancellationToken)
            {
                var post = await _postRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Post>(post);
            }
        }
    }
}
