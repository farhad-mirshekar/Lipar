using Lipar.Entities;
using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;

namespace Lipar.Services.General.Contracts
{
    /// <summary>
    /// generic attribute service
    /// </summary>
    public interface IGenericAttributeService
    {
        /// <summary>
        /// save generic attribute
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="entity">entity</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        void SaveAttribute<T,TProperty>(BaseEntity<TProperty> entity, string key, T value);

        /// <summary>
        /// get attribute
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="entity">entity</param>
        /// <param name="key">key</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetAttribute<T, TProperty>(BaseEntity<TProperty> entity, string key, T defaultValue = default);

        /// <summary>
        /// gets attribute for entity
        /// </summary>
        /// <param name="entityId">entity id</param>
        /// <param name="keyGroup">key group</param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        IList<GenericAttribute> GetAttributesForEntity(string entityId, string keyGroup, bool noTracking = false);

        /// <summary>
        /// delete generic attribute method
        /// </summary>
        /// <param name="model">generic attribute</param>
        void Delete(GenericAttribute model);

        /// <summary>
        /// edit generic attribute method
        /// </summary>
        /// <param name="model">generic attribute</param>
        void Edit(GenericAttribute model);

        /// <summary>
        /// add generic attribute method
        /// </summary>
        /// <param name="model">generic attribute</param>
        void Add(GenericAttribute model);
    }
}
