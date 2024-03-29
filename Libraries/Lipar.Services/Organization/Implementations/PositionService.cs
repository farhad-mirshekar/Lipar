﻿using Lipar.Core;
using Lipar.Core.Common;
using Lipar.Core.Infrastructure;
using Lipar.Data;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Organization.Implementations
{
    public class PositionService : IPositionService
    {
        #region Fields
        private readonly IRepository<Position> _repository;
        #endregion

        #region Ctor
        public PositionService(IRepository<Position> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public void Add(Position model)
        {
            var _workContext = EngineContext.Current.Resolve<IWorkContext>();
            model.CenterId = _workContext.CurrentCenter.Id;

            _repository.Insert(model);
        }

        public void Delete(Position model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var _workContext = EngineContext.Current.Resolve<IWorkContext>();

            model = GetById(model.Id);
            model.RemoveDate = CommonHelper.GetCurrentDateTime();
            model.RemoverId = _workContext.CurrentUser.Id;
            model.EnabledTypeId = (int)EnabledTypeEnum.Active;
            model.Default = false;

            Edit(model);
        }

        public void Edit(Position model)
        {
            var _workContext = EngineContext.Current.Resolve<IWorkContext>();
            model.CenterId = _workContext.CurrentCenter.Id;

            _repository.Update(model);
        }

        public void Edit(IEnumerable<Position> positions)
        {
            if (positions == null)
                throw new ArgumentNullException(nameof(positions));

            foreach (var position in positions)
            {
                var _workContext = EngineContext.Current.Resolve<IWorkContext>();
                position.CenterId = _workContext.CurrentCenter.Id;
            }

            _repository.Update(positions);
        }

        public Position GetActivePosition(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var position = _repository.TableNoTracking
                                      .Include(p => p.PositionRoles)
                                      .Where(p => p.UserId == userId &&
                                                p.EnabledTypeId == (int)EnabledTypeEnum.Active &&
                                                p.Default).FirstOrDefault();

            return position;
        }

        public Position GetById(Guid Id)
        {
            var query = _repository.Table;
            query = query.Include(position => position.User)
                         .Include(position => position.PositionRoles);

            var position = query.Where(p => p.Id.Equals(Id) &&
                                       p.RemoverId == null &&
                                       p.RemoveDate == null).FirstOrDefault();
            return position;
        }

        public Position GetPosition(int positionTypeId, int? enableTypeId, int? departmentTypeId)
        {
            var positions = _repository.TableNoTracking;

            positions = positions.Where(p => p.PositionTypeId == positionTypeId);
            
            if(enableTypeId.HasValue && enableTypeId.Value != 0)
            {
                positions = positions.Where(p => p.EnabledTypeId == enableTypeId);
            }

            if (departmentTypeId.HasValue && departmentTypeId.Value != 0)
            {
                positions = positions.Where(p => p.Department.DepartmentTypeId == departmentTypeId);
            }

            var position = positions.FirstOrDefault();

            return position;
        }

        public IPagedList<Position> List(PositionListVM listVM)
        {
            var query = _repository.Table.Include(p => p.User)
                                         .Include(p => p.Center)
                                         .Include(p => p.PositionRoles)
                                         .Include(p => p.PositionType)
                                         .Include(p => p.Department).AsQueryable();

            switch (listVM.EnabledTypeId)
            {
                case (int)EnabledTypeEnum.InActive:
                    query = query.Where(p => p.RemoverId != null && p.RemoveDate != null && p.EnabledTypeId == listVM.EnabledTypeId);
                    break;
                case (int)EnabledTypeEnum.Active:
                    query = query.Where(p => p.RemoverId == null && p.RemoveDate == null && p.EnabledTypeId == listVM.EnabledTypeId);
                    break;
            }

            if (!string.IsNullOrEmpty(listVM.UserListVM.FirstName))
                query = query.Where(p => p.User.FirstName.Contains(listVM.UserListVM.FirstName.Trim()));

            if (!string.IsNullOrEmpty(listVM.UserListVM.LastName))
                query = query.Where(p => p.User.LastName.Contains(listVM.UserListVM.LastName.Trim()));

            if (!string.IsNullOrEmpty(listVM.UserListVM.NationalCode))
                query = query.Where(p => p.User.NationalCode.Contains(listVM.UserListVM.NationalCode.Trim()));

            if (listVM.UserId.HasValue && listVM.UserId.Value != Guid.Empty)
                query = query.Where(p => p.UserId == listVM.UserId.Value);


            var models = new PagedList<Position>(query, listVM.PageIndex, listVM.PageSize);
            return models;
        }
    }
    #endregion
}
