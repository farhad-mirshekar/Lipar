using Lipar.Core;
using Lipar.Entities.Domain.Organization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lipar.Services.Organization.Contracts
{
    public interface IPositionService
    {
        Position GetById(Guid Id);
        void Add(Position model);
        void Edit(Position model);
        void Edit(IEnumerable<Position> positions);
        IPagedList<Position> List(PositionListVM listVM);
        void Delete(Position model);

        /// <summary>
        /// get active position
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns>position</returns>
        Position GetActivePosition(Guid userId);
    }
}
