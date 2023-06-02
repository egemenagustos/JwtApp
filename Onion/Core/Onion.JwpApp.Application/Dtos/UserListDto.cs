using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwpApp.Application.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int AppRoleId { get; set; }
    }
}
