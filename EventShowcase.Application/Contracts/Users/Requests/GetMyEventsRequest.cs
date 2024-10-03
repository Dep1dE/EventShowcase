using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.API.Contracts.Users.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Contracts.Users.Requests
{
    public record GetMyEventsRequest(string Token) : IRequest<List<EventResponse>>;
    
}
