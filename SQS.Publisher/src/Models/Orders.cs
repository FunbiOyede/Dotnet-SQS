using System;
namespace SQS.Publisher.Models
{
	public record Orders
	{
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public double Price { get; init; }
        public string Size { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public string Color { get; init; } = string.Empty;
        public DateTime purchasedDate { get; init; }
    }
}

