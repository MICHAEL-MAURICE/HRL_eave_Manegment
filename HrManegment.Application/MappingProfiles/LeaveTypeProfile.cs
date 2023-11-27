using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Domain;
using HrManegment.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HrManegment.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.MappingProfiles
{
    public class LeaveTypeProfile:Profile
    {
        public LeaveTypeProfile()
        {
            // ReverseMap allows bidirectional mapping, enabling seamless conversion between LeaveTypeDto and LeaveType objects.
            CreateMap<LeaveTypeDto,LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>();
            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>();
           
        }
    }
}
