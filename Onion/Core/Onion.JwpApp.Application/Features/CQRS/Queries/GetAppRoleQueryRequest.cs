using MediatR;
using Onion.JwpApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwpApp.Application.Features.CQRS.Queries
{
    public class GetAppRoleQueryRequest : IRequest<List<AppRoleListDto>>
    {
    }
}
