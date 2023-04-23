
using Business.Handlers.Posts.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Posts.Queries.GetPostQuery;
using Entities.Concrete;
using static Business.Handlers.Posts.Queries.GetPostsQuery;
using static Business.Handlers.Posts.Commands.CreatePostCommand;
using Business.Handlers.Posts.Commands;
using Business.Constants;
using static Business.Handlers.Posts.Commands.UpdatePostCommand;
using static Business.Handlers.Posts.Commands.DeletePostCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.Handlers
{
    [TestFixture]
    public class PostHandlerTests
    {
        Mock<IPostRepository> _postRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _postRepository = new Mock<IPostRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Post_GetQuery_Success()
        {
            //Arrange
            var query = new GetPostQuery();

            _postRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Post, bool>>>())).ReturnsAsync(new Post()
//propertyler buraya yazılacak
//{																		
//PostId = 1,
//PostName = "Test"
//}
);

            var handler = new GetPostQueryHandler(_postRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PostId.Should().Be(1);

        }

        [Test]
        public async Task Post_GetQueries_Success()
        {
            //Arrange
            var query = new GetPostsQuery();

            _postRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Post, bool>>>()))
                        .ReturnsAsync(new List<Post> { new Post() { /*TODO:propertyler buraya yazılacak PostId = 1, PostName = "test"*/ } });

            var handler = new GetPostsQueryHandler(_postRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Post>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Post_CreateCommand_Success()
        {
            Post rt = null;
            //Arrange
            var command = new CreatePostCommand();
            //propertyler buraya yazılacak
            //command.PostName = "deneme";

            _postRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Post, bool>>>()))
                        .ReturnsAsync(rt);

            _postRepository.Setup(x => x.Add(It.IsAny<Post>())).Returns(new Post());

            var handler = new CreatePostCommandHandler(_postRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _postRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Post_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePostCommand();
            //propertyler buraya yazılacak 
            //command.PostName = "test";

            _postRepository.Setup(x => x.Query())
                                           .Returns(new List<Post> { new Post() { /*TODO:propertyler buraya yazılacak PostId = 1, PostName = "test"*/ } }.AsQueryable());

            _postRepository.Setup(x => x.Add(It.IsAny<Post>())).Returns(new Post());

            var handler = new CreatePostCommandHandler(_postRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Post_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePostCommand();
            //command.PostName = "test";

            _postRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Post, bool>>>()))
                        .ReturnsAsync(new Post() { /*TODO:propertyler buraya yazılacak PostId = 1, PostName = "deneme"*/ });

            _postRepository.Setup(x => x.Update(It.IsAny<Post>())).Returns(new Post());

            var handler = new UpdatePostCommandHandler(_postRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _postRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Post_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePostCommand();

            _postRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Post, bool>>>()))
                        .ReturnsAsync(new Post() { /*TODO:propertyler buraya yazılacak PostId = 1, PostName = "deneme"*/});

            _postRepository.Setup(x => x.Delete(It.IsAny<Post>()));

            var handler = new DeletePostCommandHandler(_postRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _postRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

