using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.General.Implementations
{
    public class GenericAttributeService : IGenericAttributeService
    {
        #region Ctor
        public GenericAttributeService(IRepository<GenericAttribute> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Fields
        private readonly IRepository<GenericAttribute> _repository;
        #endregion

        #region Methods
        public T GetAttribute<T>(BaseEntity entity, string key, T defaultValue = default)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var keyGroup = entity.GetType().Name;

            var genericAttributes = GetAttributesForEntity(entity.Id, keyGroup);

            if(genericAttributes == null)
            {
                return defaultValue;
            }

            var prop = genericAttributes.FirstOrDefault(ga =>
                                                        ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return defaultValue;

            return CommonHelper.To<T>(prop.Value);
        }

        public void SaveAttribute<T>(BaseEntity entity, string key, T value)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var keyGroup = entity.GetType().Name;

            var genericAttributes = GetAttributesForEntity(entity.Id, keyGroup);

            var prop = genericAttributes.FirstOrDefault(ga =>
                                        ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    //delete
                    Delete(prop);
                }
                else
                {
                    //update
                    prop.Value = valueStr;
                    prop.CreationDate = CommonHelper.GetCurrentDateTime();
                    Edit(prop);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                    return;

                //insert
                prop = new GenericAttribute
                {
                    EntityId = entity.Id,
                    Key = key,
                    KeyGroup = keyGroup,
                    Value = valueStr,
                    CreationDate = CommonHelper.GetCurrentDateTime()
                };

                Add(prop);
            }
        }

        public IList<GenericAttribute> GetAttributesForEntity(int entityId , string keyGroup , bool noTracking = false)
        {
            var query = _repository.Table;

            if (noTracking)
            {
                query = _repository.TableNoTracking;
            }

            var genericAttributes = query.Where(g => g.EntityId == entityId &&
                                                   g.KeyGroup == keyGroup).ToList();

            return genericAttributes;
        }

        public void Delete(GenericAttribute model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Delete(model);
        }

        public void Edit(GenericAttribute model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Update(model);
        }

        public void Add(GenericAttribute model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _repository.Insert(model);
        }
        #endregion
    }
}
