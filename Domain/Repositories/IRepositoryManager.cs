﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IAccountRepository AccountRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
