
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Business.Handlers.Posts.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePostCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, IResult>
        {
            private readonly IPostRepository _postRepository;
            private readonly IMediator _mediator;

            public DeletePostCommandHandler(IPostRepository postRepository, IMediator mediator)
            {
                _postRepository = postRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {
                var postToDelete = _postRepository.Get(p => p.Id == request.Id);
                postToDelete.Status = false;
                postToDelete.ModifiedDate = DateTime.Now;
                _postRepository.Update(postToDelete);
                await _postRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

