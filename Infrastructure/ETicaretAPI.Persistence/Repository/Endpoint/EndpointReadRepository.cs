﻿using ETicaretAPI.Application.Repositories.Endpoint;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repository.Endpoint
{
    public class EndpointReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
