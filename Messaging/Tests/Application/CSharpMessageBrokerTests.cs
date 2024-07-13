using Messaging.Api;
using Messaging.Application;
using Messaging.Domain;
using Moq;

namespace Messaging.Tests.Application;

public class CSharpMessageBrokerTests
{
    private readonly IMessageBroker _messageBroker = new CSharpMessageBroker();

    [Fact]
    private void Publish_GivenThatYouAreSubscribedToTheTopic_ShouldRunTheHandler()
    {
        var mockedMethod = new Mock<Action<Message>>();
        var topic = "test_topic";
        _messageBroker.Subscribe(topic, mockedMethod.Object);
        _messageBroker.Publish(topic, new Message(topic, null));
        mockedMethod.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Once());
    }
    
    [Fact]
    private void Publish_GivenThatYouAreNotSubscribedToTheTopic_ShouldNotRunTheHandler()
    {
        var mockedMethod = new Mock<Action<Message>>();
        var topic = "test_topic";
        _messageBroker.Publish(topic, new Message(topic, null));
        mockedMethod.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Never());
    }
    
    [Fact]
    private void Publish_GivenTwoSubscribers_ShouldRunBothHandlers()
    {
        var mockedMethod1 = new Mock<Action<Message>>();
        var mockedMethod2 = new Mock<Action<Message>>();
        var mockedMethod3 = new Mock<Action<Message>>();
        var topic = "test_topic";
        _messageBroker.Subscribe(topic, mockedMethod1.Object);
        _messageBroker.Subscribe(topic, mockedMethod3.Object);
        _messageBroker.Publish(topic, new Message(topic, null));
        mockedMethod1.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Once);
        mockedMethod2.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Never);
        mockedMethod3.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Once);
    }
    
    [Fact]
    private void Unsubscribe_GivenTwoSubscribersAndYouUnsubscribeBoth_ShouldNotRunHandlersAfterUnsubscribe()
    {
        var mockedMethod1 = new Mock<Action<Message>>();
        var mockedMethod2 = new Mock<Action<Message>>();
        var topic = "test_topic";
        _messageBroker.Subscribe("test_topic", mockedMethod1.Object);
        _messageBroker.Subscribe("test_topic", mockedMethod2.Object);
        _messageBroker.Publish(topic, new Message(topic, null));
        mockedMethod1.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Exactly(1));
        mockedMethod2.Verify(x => x.Invoke(It.IsAny<Message>()),Times.Exactly(1));
        
        _messageBroker.Unsubscribe("test_topic", mockedMethod1.Object); // +1 to handlerCalls
        _messageBroker.Publish(topic, new Message(topic, null));
        mockedMethod1.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Exactly(1));
        mockedMethod2.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Exactly(2));
        
        _messageBroker.Unsubscribe("test_topic", mockedMethod2.Object); // +2 to handlerCalls
        _messageBroker.Publish(topic, new Message(topic, null));
        mockedMethod1.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Exactly(1));
        mockedMethod2.Verify(x => x.Invoke(It.IsAny<Message>()), Times.Exactly(2));
    }
}