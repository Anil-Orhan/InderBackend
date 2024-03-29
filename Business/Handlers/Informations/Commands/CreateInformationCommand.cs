﻿
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
using Business.Handlers.Informations.ValidationRules;

namespace Business.Handlers.Informations.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateInformationCommand : IRequest<IResult>
    {

        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Priority { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }


        public class CreateInformationCommandHandler : IRequestHandler<CreateInformationCommand, IResult>
        {
            private readonly IInformationRepository _informationRepository;
            private readonly IMediator _mediator;
            public CreateInformationCommandHandler(IInformationRepository informationRepository, IMediator mediator)
            {
                _informationRepository = informationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateInformationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateInformationCommand request, CancellationToken cancellationToken)
            {
                var isThereInformationRecord = _informationRepository.Query().Any(u => u.Title == request.Title);

                if (isThereInformationRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedInformation = new Information
                {
                    Title = request.Title,
                    Text = request.Text,
                    Description = request.Description,
                    Image = request.Image,
                    Priority = request.Priority,
                    CreateDate = request.CreateDate,
                    ModifiedDate = request.ModifiedDate,
                    Status = request.Status,

                };

                _informationRepository.Add(addedInformation);
                await _informationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}