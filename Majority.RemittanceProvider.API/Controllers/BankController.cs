﻿using Majority.RemittanceProvider.API.Common;
using Majority.RemittanceProvider.API.Models;
using Majority.RemittanceProvider.API.Services;
using Majority.RemittanceProvider.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Controllers
{
    [Route("[controller]")]
    [ServiceFilter(typeof(TokenAuthenticationFilter))]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IIdentityServerService _iIdentityServerService;
        private readonly ILogger<BankController> _logger;
        private readonly ApplicationConfigurations _appConfiguration;
        private readonly ICountryRepository _countryRepository;
        public BankController(ILogger<BankController> logger, IIdentityServerService IidentityServerService, ApplicationConfigurations appConfiguration, ICountryRepository countryRepository)
        {
            _logger = logger;
            _iIdentityServerService = IidentityServerService;
            _appConfiguration = appConfiguration;
            _countryRepository = countryRepository;
        }


        [HttpPost]
        [Route("get-bank-list")]

        public async Task<GenericUseCaseResult> GetAllBanks()
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {
                var countryList = await _countryRepository.GetAllAsync();
                response.HttpStatusCode = Convert.ToInt32(ResponseCode.Success);
                response.IsSuccess = true;
              
                response.Result = (from country in countryList
                              select new { country.Name, country.Code }).ToArray();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Bank Controller:" + ex.InnerException);
            }
            return response;
        }


        [HttpPost]
        [Route("get-beneficiary-name")]
        public async Task<GenericUseCaseResult> GetBeneficiaryName(string Scope)
        {
            GenericUseCaseResult response = new GenericUseCaseResult();
            try
            {

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Result = new { Error = ex.Message };
                _logger.LogError("Error Bank Controller:" + ex.InnerException);

            }
            return response;
        }

    }
}
