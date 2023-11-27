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
    }
}
