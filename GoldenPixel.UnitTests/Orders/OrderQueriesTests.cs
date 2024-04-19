﻿using GoldenPixel.Core.Orders;
using GoldenPixel.CQRS.Handlers.Queries;
using GoldenPixel.TestInfrastructure.Attributes;

namespace GoldenPixel.UnitTests.Orders;

[TestFixture]
internal class OrderQueriesTests
{
	[Test]
	[ExpectedException(typeof(ArgumentNullException))]
	public async Task HandleGetOrderById_NullConnection_ThrowExANE()
	{
		var request = new GetOrderByIdRequest(Guid.NewGuid());

		await OrderQueryHandler.HandleGetOrderById(null, request);
	}

	[Test]
	[ExpectedException(typeof(ArgumentNullException))]
	public async Task HandleGetOrders_NullConnection_ThrowExANE()
	{
		var request = new GetOrdersRequest();

		await OrderQueryHandler.HandleGetOrders(null, request);
	}
}