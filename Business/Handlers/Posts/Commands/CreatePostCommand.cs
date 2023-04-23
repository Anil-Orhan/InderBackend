
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Posts.ValidationRules;
using System;

namespace Business.Handlers.Posts.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePostCommand : IRequest<IResult>
    {

        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        


        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, IResult>
        {
            private readonly IPostRepository _postRepository;
            private readonly IMediator _mediator;
            public CreatePostCommandHandler(IPostRepository postRepository, IMediator mediator)
            {
                _postRepository = postRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePostValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                var isTherePostRecord = _postRepository.Query().Any(u => u.Title == request.Title);

                if (isTherePostRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPost = new Post
                {
                    Title = request.Title,
                    Text = request.Text,
                    Description = request.Description,
                    Image = request.Image,
                    CategoryId = request.CategoryId,
                    CreateDate =DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = true,

                };

                _postRepository.Add(addedPost);
                await _postRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}