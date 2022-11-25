﻿using App.Application.Services;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces.IRepositories
{
    public interface IUserRepository : IGenericRepository<Customer>
    {

    }
}
