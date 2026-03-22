using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DataRooms.Contracts;
using DataRooms.Entity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DataRooms.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationController : ControllerBase
    {
       
        //private readonly IDataProtector _dataProtector;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        private readonly ISqlRepository<User> _sqlRepository;
        private readonly ISqlRepository<LicenseInfo> _licenseRepository;
        private readonly ISqlRepository<ADInfo> _adinfoRepository;
        //EncryptionHelper encryptionHelper;
        public ActivationController(IWebHostEnvironment hostingEnvironment,
            IConfiguration config,
            ISqlRepository<User> sqlRepository,
            ISqlRepository<LicenseInfo> licenseRepository,
            ISqlRepository<ADInfo> adinfoRepository
            //,IDataProtectionProvider dataProtectionProvider
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _config = config;
            _sqlRepository = sqlRepository;
            _licenseRepository = licenseRepository;
            _adinfoRepository = adinfoRepository;
            //_dataProtector = dataProtectionProvider.CreateProtector(_config["EncryptionKey"]);
            //encryptionHelper = new EncryptionHelper();
        }


        [Route("checkandcreatedatabase")]
        [System.Web.Http.HttpPost]
        public bool CreateDataBase([System.Web.Http.FromBody] DBConfigureData model)
        {
            try
            {
                string scriptPath = _hostingEnvironment.ContentRootPath + "/DBScript/SharBoxScript.sql";
                //string key = _config["EncryptionKey"];
                //model.EncryptedDomainName = _dataProtector.Protect(model.DomainName);
                //model.EncryptedEmailId = _dataProtector.Protect(model.EmailId);
                //model.EncryptedPublicIp = _dataProtector.Protect(model.PublicIp);
                bool isDatabseCreationSuccessful = _sqlRepository.ConfigureDatabase(model, scriptPath, string.Empty);
                if (isDatabseCreationSuccessful)
                {
                    string newConnectionString = "Data Source=" + model.DBHostName + ";Database=DataRoom;User Id=" + model.UserId + ";Password=" + model.Password + ";Connection Timeout=4000;";
                    _config["ConnectionStrings:SqlDataConnection"] = newConnectionString;
                }
                return isDatabseCreationSuccessful;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("gethostdetails")]
        [System.Web.Http.HttpGet]
        public async Task<HostDetails> GetHostInfo()
        {
            HostDetails host = new HostDetails();
            try
            {
                host = await _sqlRepository.GetHostDetailsforActivation();
                //if (!string.IsNullOrEmpty(host.HostName))
                //{
                //    host.HostName = _dataProtector.Unprotect(host.HostName);
                //    host.IpAddress = _dataProtector.Unprotect(host.IpAddress);
                //    host.EmailId = _dataProtector.Unprotect(host.EmailId);
                //}
                
                return host;
            }
            catch (Exception ex)
            {
                host.HostName = ex.Message;
                host.EmailId = ex.StackTrace;
                //throw ex;
            }
            return host;
        }

        //[Route("savelicenseinfo")]
        //[System.Web.Http.HttpPost]
        //public async Task<int> SaveLicenseInfo([System.Web.Http.FromBody]Module model)
        //{
        //    try
        //    {
        //        var existedlicenseinfo = await _licenseRepository.GetSingleAsync();
        //        if (existedlicenseinfo != null)
        //        {
        //            await _licenseRepository.DeleteAsync(existedlicenseinfo);
        //        }

        //        LicenseInfo licenseInfo = new LicenseInfo();
        //        licenseInfo.Column1 = _dataProtector.Protect(model.ModuleName);
        //        licenseInfo.Column2 = _dataProtector.Protect(model.ModuleCount);
        //        licenseInfo.Column3 = _dataProtector.Protect(model.LicenseStatus);
        //        licenseInfo.Column4 = _dataProtector.Protect(model.FromDate);
        //        licenseInfo.Column5 = _dataProtector.Protect(model.ToDate);
        //        return await _licenseRepository.CreateAsync(licenseInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        [Route("savelicenseinfo")]
        [System.Web.Http.HttpPost]
        public async Task<int> SaveLicenseInfo([System.Web.Http.FromBody] LicenseInfo model)
        {
            try
            {
                var existedlicenseinfo = await _licenseRepository.GetSingleAsync();
                if (existedlicenseinfo != null)
                {
                    await _licenseRepository.DeleteAsync(existedlicenseinfo);
                }

                //LicenseInfo licenseInfo = new LicenseInfo();
                //licenseInfo.Column1 = _dataProtector.Protect(model.ModuleName);
                //licenseInfo.Column2 = _dataProtector.Protect(model.ModuleCount);
                //licenseInfo.Column3 = _dataProtector.Protect(model.LicenseStatus);
                //licenseInfo.Column4 = _dataProtector.Protect(model.FromDate);
                //licenseInfo.Column5 = _dataProtector.Protect(model.ToDate);
                return await _licenseRepository.CreateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("getlicenseinfo")]
        [System.Web.Http.HttpGet]
        public async Task<LicenseInfo> GetLicenseInfo()
        {
            try
            {
                LicenseInfo licenseInfo = await _licenseRepository.GetSingleAsync();
                //licenseInfo.Column1 = _dataProtector.Unprotect(licenseInfo.Column1);
                //licenseInfo.Column2 = _dataProtector.Unprotect(licenseInfo.Column2);
                //licenseInfo.Column3 = _dataProtector.Unprotect(licenseInfo.Column3);
                //licenseInfo.Column4 = _dataProtector.Unprotect(licenseInfo.Column4);
                //licenseInfo.Column5 = _dataProtector.Unprotect(licenseInfo.Column5);
                return licenseInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
