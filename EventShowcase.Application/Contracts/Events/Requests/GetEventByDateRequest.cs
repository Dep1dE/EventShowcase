using EventShowcase.API.Contracts.Events.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events.Requests
{
    public record GetEventByDateRequest(DateTime Date) : IRequest<EventResponse>;
}
