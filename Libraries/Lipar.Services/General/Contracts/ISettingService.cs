using Lipar.Entities.Configuration;
using Lipar.Entities.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lipar.Services.General.Contracts
{
    public interface ISettingService
    {
        Setting GetById(Guid Id);
        void Delete(Setting model);
        Setting GetSetting(string Name);
        T GetSettingByKey<T>(string Key, T DefaultValue = default);
        void SetSetting<T>(string Name, T Key);
        IEnumerable<Setting> GetAllSettings();
        T LoadSettings<T>() where T : ISettings, new();
        void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> expression) where T : ISettings, new();
    }
}
