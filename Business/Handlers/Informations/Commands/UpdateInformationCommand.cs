
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
using Business.Handlers.Informations.ValidationRules;


namespace Business.Handlers.Informations.Commands
{


    public class UpdateInformationCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Priority { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }

        public class UpdateInformationCommandHandler : IRequestHandler<UpdateInformationCommand, IResult>
        {
            private readonly IInformationRepository _informationRepository;
            private readonly IMediator _mediator;

            public UpdateInformationCommandHandler(IInformationRepository informationRepository, IMediator mediator)
            {
                _informationRepository = informationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateInformationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateInformationCommand request, CancellationToken cancellationToken)
            {
                var isThereInformationRecord = await _informationRepository.GetAsync(u => u.Id == request.Id);


                isThereInformationRecord.Title = request.Title;
                isThereInformationRecord.Text = request.Text;
                isThereInformationRecord.Description = request.Description;
                isThereInformationRecord.Image = request.Image;
                isThereInformationRecord.Priority = request.Priority;
                isThereInformationRecord.CreateDate = request.CreateDate;
                isThereInformationRecord.ModifiedDate = request.ModifiedDate;
                isThereInformationRecord.Status = request.Status;


                _informationRepository.Update(isThereInformationRecord);
                await _informationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

