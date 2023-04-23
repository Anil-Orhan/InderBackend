
using Business.Handlers.AssociationMembers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.AssociationMembers.Queries.GetAssociationMemberQuery;
using Entities.Concrete;
using static Business.Handlers.AssociationMembers.Queries.GetAssociationMembersQuery;
using static Business.Handlers.AssociationMembers.Commands.CreateAssociationMemberCommand;
using Business.Handlers.AssociationMembers.Commands;
using Business.Constants;
using static Business.Handlers.AssociationMembers.Commands.UpdateAssociationMemberCommand;
using static Business.Handlers.AssociationMembers.Commands.DeleteAssociationMemberCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AssociationMemberHandlerTests
    {
        Mock<IAssociationMemberRepository> _associationMemberRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _associationMemberRepository = new Mock<IAssociationMemberRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task AssociationMember_GetQuery_Success()
        {
            //Arrange
            var query = new GetAssociationMemberQuery();

            _associationMemberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AssociationMember, bool>>>())).ReturnsAsync(new AssociationMember()
//propertyler buraya yazılacak
//{																		
//AssociationMemberId = 1,
//AssociationMemberName = "Test"
//}
);

            var handler = new GetAssociationMemberQueryHandler(_associationMemberRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.AssociationMemberId.Should().Be(1);

        }

        [Test]
        public async Task AssociationMember_GetQueries_Success()
        {
            //Arrange
            var query = new GetAssociationMembersQuery();

            _associationMemberRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AssociationMember, bool>>>()))
                        .ReturnsAsync(new List<AssociationMember> { new AssociationMember() { /*TODO:propertyler buraya yazılacak AssociationMemberId = 1, AssociationMemberName = "test"*/ } });

            var handler = new GetAssociationMembersQueryHandler(_associationMemberRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<AssociationMember>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task AssociationMember_CreateCommand_Success()
        {
            AssociationMember rt = null;
            //Arrange
            var command = new CreateAssociationMemberCommand();
            //propertyler buraya yazılacak
            //command.AssociationMemberName = "deneme";

            _associationMemberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AssociationMember, bool>>>()))
                        .ReturnsAsync(rt);

            _associationMemberRepository.Setup(x => x.Add(It.IsAny<AssociationMember>())).Returns(new AssociationMember());

            var handler = new CreateAssociationMemberCommandHandler(_associationMemberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _associationMemberRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task AssociationMember_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateAssociationMemberCommand();
            //propertyler buraya yazılacak 
            //command.AssociationMemberName = "test";

            _associationMemberRepository.Setup(x => x.Query())
                                           .Returns(new List<AssociationMember> { new AssociationMember() { /*TODO:propertyler buraya yazılacak AssociationMemberId = 1, AssociationMemberName = "test"*/ } }.AsQueryable());

            _associationMemberRepository.Setup(x => x.Add(It.IsAny<AssociationMember>())).Returns(new AssociationMember());

            var handler = new CreateAssociationMemberCommandHandler(_associationMemberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task AssociationMember_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateAssociationMemberCommand();
            //command.AssociationMemberName = "test";

            _associationMemberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AssociationMember, bool>>>()))
                        .ReturnsAsync(new AssociationMember() { /*TODO:propertyler buraya yazılacak AssociationMemberId = 1, AssociationMemberName = "deneme"*/ });

            _associationMemberRepository.Setup(x => x.Update(It.IsAny<AssociationMember>())).Returns(new AssociationMember());

            var handler = new UpdateAssociationMemberCommandHandler(_associationMemberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _associationMemberRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task AssociationMember_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteAssociationMemberCommand();

            _associationMemberRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AssociationMember, bool>>>()))
                        .ReturnsAsync(new AssociationMember() { /*TODO:propertyler buraya yazılacak AssociationMemberId = 1, AssociationMemberName = "deneme"*/});

            _associationMemberRepository.Setup(x => x.Delete(It.IsAny<AssociationMember>()));

            var handler = new DeleteAssociationMemberCommandHandler(_associationMemberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _associationMemberRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

