using System;
using System.Collections.Generic;
using Tarantino.Core.Commons.Model;

using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Repositories
{
	public interface IPersistentObjectRepository
	{
		IEnumerable<T> GetAll<T>();
		T GetById<T>(Guid id) where T : PersistentObject;
		void Save(PersistentObject persistentObject);
		void Revert(PersistentObject persistentObject);
		void Delete(PersistentObject persistentObject);
		IEnumerable<T> FindAll<T>(CriterionSet criterionSet);
		T FindFirst<T>(CriterionSet set) where T : class;

		string ConfigurationFile { get; set; }
		T GetByIdWithoutClosingSession<T>(Guid id) where T : PersistentObject;
	}
}