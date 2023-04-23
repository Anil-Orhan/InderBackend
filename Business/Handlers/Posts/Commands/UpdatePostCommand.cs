
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Posts.ValidationRules;


namespace Business.Handlers.Posts.Commands
{


    public class UpdatePostCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }

        public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, IResult>
        {
            private readonly IPostRepository _postRepository;
            private readonly IMediator _mediator;

            public UpdatePostCommandHandler(IPostRepository postRepository, IMediator mediator)
            {
                _postRepository = postRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePostValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
            {
                var isTherePostRecord = await _postRepository.GetAsync(u => u.Id == request.Id);


                isTherePostRecord.Title = request.Title;
                isTherePostRecord.Text = request.Text;
                isTherePostRecord.Description = request.Description;
                isTherePostRecord.Image = request.Image;
                isTherePostRecord.CategoryId = request.CategoryId;
                isTherePostRecord.CreateDate = request.CreateDate;
                isTherePostRecord.ModifiedDate = request.ModifiedDate;
                isTherePostRecord.Status = request.Status;


                _postRepository.Update(isTherePostRecord);
                await _postRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

