﻿namespace GoldenPixel.Core.Orders;

public enum OrdersErrorCode
{
    BadOrderId = 1,
    ManyRecordsByOrderId = 2,
    OrdersIsNull = 3,
}

public readonly record struct OrderError(OrdersErrorCode ErrorCode, string ErrorMessage);

public static class Errors
{
    private static readonly string BadOrderIdMessage = "Записи о запросе с данным Id не существует";
    private static readonly string ManyRecordsByIdMessage = "По введенному Id существует несколько записей о запросах";
    private static readonly string OrdersIsNullMessage = "Выборка заказов равна null";

    public static readonly OrderError BadOrderIdError = new(OrdersErrorCode.BadOrderId, BadOrderIdMessage);
    public static readonly OrderError ManyRecordsByOrderId = new(OrdersErrorCode.ManyRecordsByOrderId, ManyRecordsByIdMessage);
    public static readonly OrderError OrderIsNull = new(OrdersErrorCode.OrdersIsNull, OrdersIsNullMessage);
}
