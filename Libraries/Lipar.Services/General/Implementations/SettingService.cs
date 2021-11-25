using Lipar.Core.Caching;
using Lipar.Core.Common;
using Lipar.Data;
using Lipar.Entities.Configuration;
using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lipar.Services.General.Implementations
{
    public class SettingService : ISettingService
    {
        #region Fields
        private readonly IRepository<Setting> _repository;
        private readonly IStaticCacheManager _staticCacheManager;
        #endregion

        #region Ctor
        public SettingService(IRepository<Setting> repository
                            , IStaticCacheManager staticCacheManager)
        {
            _repository = repository;
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods
        public void Delete(Setting model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Setting> GetAllSettings()
        {
            var query = _repository.TableNoTracking;

            return query;
        }

        public Setting GetById(int Id)
        {
            if (Id == 0)
                return null;

            return _repository.GetById(Id);
        }

        public Setting GetSetting(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return null;

            var query = _repository.TableNoTracking;
            var setting = query.Where(s => s.Name.Contains(Name.Trim())).FirstOrDefault();

            return setting;
        }

        public T GetSettingByKey<T>(string Key, T DefaultValue = default)
        {
            if (string.IsNullOrEmpty(Key))
                return DefaultValue;

            var settings = GetAllSettingsDictionary();
            Key = Key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(Key))
                return DefaultValue;

            var settingsByKey = settings[Key];
            var setting = settingsByKey.FirstOrDefault();

            return setting != null ? CommonHelper.To<T>(setting.Value) : DefaultValue;
        }

        public T LoadSettings<T>() where T : ISettings, new()
        {
            return (T)LoadSettings(typeof(T));
        }

        public ISettings LoadSettings(Type type)
        {
            var settings = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = type.Name + "." + prop.Name;
                //load by store
                var setting = GetSettingByKey<string>(key);
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings as ISettings;
        }

        public void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> expression) where T : ISettings, new()
        {
            if (!(expression.Body is MemberExpression member))
            {
                throw new ArgumentException($"Expression '{expression}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{expression}' refers to a field, not a property.");
            }

            var key = GetSettingKey(settings, expression);
            var value = (TPropType)propInfo.GetValue(settings, null);

            if (value != null)
                SetSetting(key, value);
            else
                SetSetting(key, string.Empty);

            _staticCacheManager.Remove(LiparPublicDefaults.Settings_All_As_Dictionary);

        }

        public void SetSetting<T>(string Name, T Key)
        {
            SetSetting(typeof(T), Name, Key);
        }
        #endregion

        #region Utilities
        protected IDictionary<string, IList<Setting>> GetAllSettingsDictionary()
        {
            return _staticCacheManager.Get(LiparPublicDefaults.Settings_All_As_Dictionary, () =>
            {
                var settigs = GetAllSettings();

                var dictionary = new Dictionary<string, IList<Setting>>();

                foreach (var item in settigs)
                {
                    var resourceName = item.Name.ToLowerInvariant();
                    var settingForCaching = new Setting
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Value = item.Value,
                    };

                    if (!dictionary.ContainsKey(resourceName))
                    {
                        //first setting
                        dictionary.Add(resourceName, new List<Setting>
                        {
                            settingForCaching
                        });
                    }
                    else
                    {
                        dictionary[resourceName].Add(settingForCaching);
                    }
                }

                return dictionary;
            });
        }
        protected void Add(Setting model)
        => _repository.Insert(model);
        protected void Edit(Setting model)
        => _repository.Update(model);
        private string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector) where TSettings : ISettings, new()
        {
            if (!(keySelector.Body is MemberExpression member))
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

            if (!(member.Member is PropertyInfo propInfo))
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

            var key = $"{typeof(TSettings).Name}.{propInfo.Name}";

            return key;
        }
        protected void SetSetting(Type type, string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsDictionary();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetById(settingForCaching.Id);
                setting.Value = valueStr;
                Edit(setting);
            }
            else
            {
                //insert
                var setting = new Setting
                {
                    Name = key,
                    Value = valueStr
                };
                Add(setting);
            }
        }
        #endregion
    }
}
