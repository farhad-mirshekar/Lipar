﻿using Lipar.Web.Models.Organization;

namespace Lipar.Web.Factories.Organization
{
    /// <summary>
    /// user model factory
    /// </summary>
    public interface IUserModelFactory
    {
        /// <summary>
        /// prepare passowrd recovery model
        /// <param name="PasswordRecoveryModel">password recovery model</param>
        /// </summary>
        /// <returns></returns>
        PasswordRecoveryModel PreparePasswordRecoveryModel(PasswordRecoveryModel model);
    }
}