using System;
namespace SQS.Publisher.Messaging
{
	public record QueueSettings
	{
		public const string Key = "Queue";
		public required string Name  { get; init; }
	}
}

