using System;
using Tarantino.Core.DatabaseManager.Services.Impl.Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
    public interface IDataBaseActionLocator
    {
        IDatabaseActionExecutor CreateInstance(DatabaseAction databaseAction);
    }

    public class DataBaseActionLocator : IDataBaseActionLocator
    {
        public IDatabaseActionExecutor CreateInstance(DatabaseAction databaseAction)
        {
            if (databaseAction != null)
                if(databaseAction.Equals(DatabaseAction.Create))
                {
                    return new DatabaseCreator();
                }
                else if(databaseAction.Equals(DatabaseAction.Update))
                {
                    return new DatabaseUpdater();
                }
                else if (databaseAction.Equals(DatabaseAction.Drop))
                {
                    return new DatabaseDropper();
                }

            return null;
        }
    }
}