﻿namespace MotoShare.Domain.Messaging;

public interface IBusPublisher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
}
