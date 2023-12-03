using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HrManegment.Application.Contracts.Persistence;
using HrManegment.Application.Features.LeaveType.Commands.CreateLeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }


        public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, Guid>
        {
            private readonly IMapper _mapper;
            private readonly ILeaveTypeRepository _leaveTypeRepository;

            public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
            {
                _mapper = mapper;
                _leaveTypeRepository = leaveTypeRepository;
            }

            public async Task<Guid> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
            {
                // Validate incoming data
                var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.Errors.Any())
                    throw new BadRequestException("Invalid Leave type", validationResult);

                // convert to domain entity object
                var leaveTypeToCreate = _mapper.Map<HR.LeaveManagement.Domain.LeaveType>(request);

                // add to database
                await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

                // retun record id
                return leaveTypeToCreate.Id;
            }
        }
    }


}
