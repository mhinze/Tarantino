using System;
using System.Collections.Generic;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.Infrastructure.DatabaseManager.DataAccess;

namespace Tarantino.DatabaseManager.Core
{
    public class Factory
    {
        public static ISqlDatabaseManager Create()
        {
            return new SqlDatabaseManager();
        }
    }
}