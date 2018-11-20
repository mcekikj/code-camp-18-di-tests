﻿using System;

namespace EncryptorLoggerDemoTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			var messenger = new Messenger(new HeadEncryptor(), new ConsoleLogger());

			var message = new MessageContent()
			{
				Content = "Test message",
				IsScripted = false,
				Priority = MessagePriorityEnum.High
			};

			messenger.DispatchMessage(message);

			Console.ReadLine();
		}
	}

	public class Messenger
	{
		private readonly IEncryptor _encryptor;
		private readonly ILogger _logger;

		public Messenger(IEncryptor encryptor, ILogger logger)
		{
			_encryptor = encryptor;
			_logger = logger;
		}

		public void DispatchMessage(MessageContent message)
		{
			if (message.IsScripted)
				throw new ApplicationException("Message content is scripted, no encryption needed!");

			if (message.Priority.Equals(MessagePriorityEnum.High))
				message.MessagePriorityDetails = new MessagePriorityDetails()
				{
					TimeStamp = DateTime.UtcNow
				};

			var encryptedMessage = _encryptor.Encrypt(message.Content);

			//Logic for sending encrypted message

			_logger.Log("Encrypted message: " + encryptedMessage);
		}
	}

	public class MessagePriorityDetails
	{
		public DateTime TimeStamp { get; set; }
	}

	public enum MessagePriorityEnum
	{
		Low = 1,
		Intermediate = 2,
		High = 3
	}

	public class MessageContent
	{
		public string Content { get; set; }
		public bool IsScripted { get; set; }
		public MessagePriorityEnum Priority { get; set; }
		public MessagePriorityDetails MessagePriorityDetails { get; set; }
	}

	public interface IEncryptor
	{
		string Encrypt(string message);
	}

	public class HeadEncryptor : IEncryptor
	{
		public string Encrypt(string message)
		{
			Console.WriteLine("Encrypting message...");

			//Encryption logic

			return $"{message}_{DateTime.UtcNow.Date}";
		}
	}

	public interface ILogger
	{
		void Log(string message);
	}

	public class ConsoleLogger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine($"Console Logger: INFO-{message}");
		}
	}
}
