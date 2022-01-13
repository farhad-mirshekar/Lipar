using Lipar.Entities.Domain.Financial;
using Lipar.Services.Financial.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.Financial;
using Lipar.Web.Framework.Models;
using System;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.Financial
{
    public class BankModelFactory : IBankModelFactory
    {
        #region Ctor
        public BankModelFactory(IBankService bankService
                              , IBaseAdminModelFactory baseAdminModelFactory
                              , IBankPortService bankPortService)
        {
            _bankService = bankService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _bankPortService = bankPortService;
        }
        #endregion

        #region Fields
        private readonly IBankService _bankService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IBankPortService _bankPortService;
        #endregion

        #region Methods
        public BankListModel PrepareBankListModel(BankSearchModel searchModel)
        {
            var bankList = _bankService.List(new BankListVM
            {
                Name = searchModel.Name,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize
            });

            if(bankList == null)
            {
                return null;
            }

            var models = new BankListModel().PrepareToGrid(searchModel, bankList, () =>
            {
                return bankList.Select(bank =>
                {
                    var bankModel = bank.ToModel<BankModel>();

                    return bankModel;
                });
            });

            return models;
        }

        public BankModel PrepareBankModel(BankModel model, Bank bank)
        {
            if(bank != null)
            {
                model = bank.ToModel<BankModel>();
                //prepare bank port
                PrepareBankPortSearchModel(model.BankPortSearchModel, bank);
            }

            _baseAdminModelFactory.PrepareEnabledType(model.AvailableEnableTypes);

            return model;
        }

        public BankPortListModel PrepareBankPortListModel(BankPortSearchModel searchModel)
        {
            var bankPorts = _bankPortService.List(new BankPortListVM
            {
                BankId = searchModel.BankId,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize
            });

            if(bankPorts == null)
            {
                return null;
            }

            var models = new BankPortListModel().PrepareToGrid(searchModel, bankPorts, () =>
            {
                return bankPorts.Select(bankPort =>
                {
                    var bankPortModel = bankPort.ToModel<BankPortModel>();

                    return bankPortModel;
                });
            });

            return models;
        }

        public BankPortModel PrepareBankPortModel(BankPortModel model, BankPort bankPort)
        {
            if(bankPort != null)
            {
                model = bankPort.ToModel<BankPortModel>();
            }

            //prepare enable type select
            _baseAdminModelFactory.PrepareEnabledType(model.AvailableEnableTypes);

            return model;
        }
        #endregion

        protected BankPortSearchModel PrepareBankPortSearchModel(BankPortSearchModel searchModel , Bank bank)
        {
            if (searchModel == null)
                throw new Exception(nameof(searchModel));

            if (bank == null)
                throw new Exception(nameof(bank));

            searchModel.BankId = bank.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }
    }
}
