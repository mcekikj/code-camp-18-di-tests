using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EncryptorLoggerDemoTesting.Tests
{
	[TestClass]
	public class MessengerTests
	{
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void DispatchMessage_MessageIsScripted_ShouldThrowApplicationException()
		{
			var messenger = new Messenger(new MockEncryptor(), new MockLogger());

			var message = new MessageContent
			{
				Content = "Test message",
				IsScripted = true
			};

			messenger.DispatchMessage(message);
		}

		[TestMethod]
		public void DispatchMessage_HighPriorityMessage_ShouldEnableMessageDetails()
		{
			var messenger = new Messenger(new MockEncryptor(), new MockLogger());

			var messageContent = new MessageContent
			{
				Content = "Test high priority message",
				IsScripted = false,
				Priority = MessagePriorityEnum.High
			};

			messenger.DispatchMessage(messageContent);

			Assert.IsNotNull(messageContent.MessagePriorityDetails);
		}

		public class MockLogger : ILogger
		{
			public void Log(string message)
			{

			}
		}

		public class MockEncryptor : IEncryptor
		{
			string IEncryptor.Encrypt(string message)
			{
				return string.Empty;
			}
		}
	}
}