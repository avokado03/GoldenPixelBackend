﻿namespace GoldenPixel.Db.Entities;

/// <summary>
/// Order db entity
/// </summary>
public class Orders
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Requester { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}
