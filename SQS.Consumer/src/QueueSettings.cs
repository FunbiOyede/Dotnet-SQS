﻿using System;
namespace SQS.Consumer
{
	public class QueueSettings
	{
        public const string Key = "Queue";
        public required string Name { get; init; }
    }
}

