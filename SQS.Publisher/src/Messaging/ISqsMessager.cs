using System;
using Amazon.SQS.Model;

namespace SQS.Publisher.Messaging
{

	public interface ISqsMessage
	{

		Task<SendMessageResponse> SendMessageAsync<T>(T message);
	}

    
}

