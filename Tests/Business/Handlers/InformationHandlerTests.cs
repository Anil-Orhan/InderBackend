
using Business.Handlers.Informations.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Informations.Queries.GetInformationQuery;
using Entities.Concrete;
using static Business.Handlers.Informations.Queries.GetInformationsQuery;
using static Business.Handlers.Informations.Commands.CreateInformationCommand;
using Business.Handlers.Informations.Commands;
using Business.Constants;
using static Business.Handlers.Informations.Commands.UpdateInformationCommand;
using static Business.Handlers.Informations.Commands.DeleteInformationCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class InformationHandlerTests
    {
        Mock<IInformationRepository> _informationRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _informationRepository = new Mock<IInformationRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Information_GetQuery_Success()
        {
            //Arrange
            var query = new GetInformationQuery();

            _informationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Information, bool>>>())).ReturnsAsync(new Information()
//propertyler buraya yazılacak
//{																		
//InformationId = 1,
//InformationName = "Test"
//}
);

            var handler = new GetInformationQueryHandler(_informationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.InformationId.Should().Be(1);

        }

        [Test]
        public async Task Information_GetQueries_Success()
        {
            //Arrange
            var query = new GetInformationsQuery();

            _informationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Information, bool>>>()))
                        .ReturnsAsync(new List<Information> { new Information() { /*TODO:propertyler buraya yazılacak InformationId = 1, InformationName = "test"*/ } });

            var handler = new GetInformationsQueryHandler(_informationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Information>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Information_CreateCommand_Success()
        {
            Information rt = null;
            //Arrange
            var command = new CreateInformationCommand();
            //propertyler buraya yazılacak
            //command.InformationName = "deneme";

            _informationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Information, bool>>>()))
                        .ReturnsAsync(rt);

            _informationRepository.Setup(x => x.Add(It.IsAny<Information>())).Returns(new Information());

            var handler = new CreateInformationCommandHandler(_informationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _informationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Information_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateInformationCommand();
            //propertyler buraya yazılacak 
            //command.InformationName = "test";

            _informationRepository.Setup(x => x.Query())
                                           .Returns(new List<Information> { new Information() { /*TODO:propertyler buraya yazılacak InformationId = 1, InformationName = "test"*/ } }.AsQueryable());

            _informationRepository.Setup(x => x.Add(It.IsAny<Information>())).Returns(new Information());

            var handler = new CreateInformationCommandHandler(_informationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Information_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateInformationCommand();
            //command.InformationName = "test";

            _informationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Information, bool>>>()))
                        .ReturnsAsync(new Information() { /*TODO:propertyler buraya yazılacak InformationId = 1, InformationName = "deneme"*/ });

            _informationRepository.Setup(x => x.Update(It.IsAny<Information>())).Returns(new Information());

            var handler = new UpdateInformationCommandHandler(_informationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _informationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Information_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteInformationCommand();

            _informationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Information, bool>>>()))
                        .ReturnsAsync(new Information() { /*TODO:propertyler buraya yazılacak InformationId = 1, InformationName = "deneme"*/});

            _informationRepository.Setup(x => x.Delete(It.IsAny<Information>()));

            var handler = new DeleteInformationCommandHandler(_informationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _informationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

