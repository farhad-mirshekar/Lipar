using Lipar.Data;
using Lipar.Entities.Domain.Core;
using Lipar.Services.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lipar.Services.Core.Implementations
{
    public class GenderService : IGenderService
    {
        #region Ctor
        public GenderService(IRepository<Gender> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<Gender> _repository;
        #endregion

        public IQueryable<Gender> GetGenders()
        {
            var query = _repository.TableNoTracking;

            return query;
        }
    }
}
