using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryReponse
    {

        public object Users { get; set; }
        public int TotalUserCount { get; set; }


    }
}
