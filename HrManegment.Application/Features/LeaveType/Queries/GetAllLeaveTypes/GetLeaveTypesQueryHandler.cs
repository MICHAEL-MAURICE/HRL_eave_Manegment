using AutoMapper;
using HrManegment.Application.Contracts.Logging;
using HrManegment.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, IEnumerable<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
      private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

        public GetLeaveTypesQueryHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
           IAppLogger<GetLeaveTypesQueryHandler> logger
            )
        {
              _mapper = mapper;
              _leaveTypeRepository = leaveTypeRepository;
              _logger = logger;
        }

        public async Task<IEnumerable<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {


            // Query the database
            _logger.LogInformation($"request={@request}");
            var leaveTypes = await _leaveTypeRepository.GetAsync(request.PageNumber,request.Count);
            if(leaveTypes == null )
            {
                return Enumerable.Empty<LeaveTypeDto>();
            }

            // convert data objects to DTO objects
            var data = _mapper.Map<IEnumerable<LeaveTypeDto>>(leaveTypes);

            // return list of DTO object
            _logger.LogInformation("Leave types were retrieved successfully");
            return data.ToList();
        }
    }
}
