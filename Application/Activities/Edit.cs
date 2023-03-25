using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set;}   //get from database

        }
        public class Handler: IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
            _mapper = mapper;
            _context = context;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id );       
                
                _mapper.Map(request.Activity, activity); //automapper takes all properties in request.activity and update to activity

                await _context.SaveChangesAsync(); //saves back to databases

                return Unit.Value; //indicate sucessful completion of activity

            }

        }
    }
}