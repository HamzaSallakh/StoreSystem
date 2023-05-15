using Domain.AutoMapper;
using Domain.BaseEntity;
using Domain.Model;
using Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        public IBuyers<Buyers> Buyers { get; }
        public BuyersController(IBuyers<Buyers> _buyers)
        {
            Buyers = _buyers;
        }

        [HttpPost("GetAll")]
        public StanderdJson GetAll()
        {
            try
            {
                var data = Buyers.View();
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<List<BuyersModel>>(data);
                var Standerd = new StanderdJson
                {
                    Code = Ok().StatusCode,
                    Data = NewData,
                    Message = "Success",
                    Success = true
                };
                return Standerd;
            }
            catch (Exception)
            {
                return new StanderdJson
                {
                    Code = BadRequest().StatusCode,
                    Data = new NullColumns(),
                    Message = "Error",
                    Success = false
                };
            }
        }


        [HttpPost("AddBuyers")]
        public StanderdJson AddBuyers([FromBody]BuyersModel model) {
            try { 
            var MappConfig = AutoConfigMapper.CreateMapper();
            var NewData = MappConfig.Map<Buyers>(model);
            Buyers.Add(NewData);
                var Standerd = new StanderdJson
                {
                    Code = Ok().StatusCode,
                    Data = model,
                    Message = "Success",
                    Success = true
                };
                return Standerd;
            }
            catch (Exception)
            {
                var Standerd = new StanderdJson
                {
                    Code = BadRequest().StatusCode,
                    Data = new NullColumns(),
                    Message = "Error",
                    Success = false
                };
                return Standerd;
            }
        }


        [HttpPost("UpdateBuyers")]
        public StanderdJson UpdateBuyers([FromBody] BuyersModel model)
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Buyers>(model);
                Buyers.Update(NewData);
                var Standerd = new StanderdJson
                {
                    Code = Ok().StatusCode,
                    Data = model,
                    Message = "Success",
                    Success = true
                };
                return Standerd;
            }
            catch (Exception)
            {
                var Standerd = new StanderdJson
                {
                    Code = BadRequest().StatusCode,
                    Data = new NullColumns(),
                    Message = "Error",
                    Success = false
                };
                return Standerd;
            }

        }

        [HttpPost("DeleteBuyers")]//سوال هون ليش ما رضيت تشتغل بس كانت ات تي تي ديليت وبس سويتها اتش تي تي بوست زبطت ؟
        public StanderdJson DeleteBuyers([FromBody] BuyersModel model)//بنستخدم الفروم فورم بكثير احيان عشان نخلي في خيار اضافي لاضافة فايل 
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Buyers>(model);
                Buyers.Delete(NewData);
                var Standerd = new StanderdJson
                {
                    Code = Ok().StatusCode,
                    Data = model,
                    Message = "Success",
                    Success = true
                };
                return Standerd;
            }
            catch (Exception)
            {
                var Standerd = new StanderdJson
                {
                    Code = BadRequest().StatusCode,
                    Data = new NullColumns(),
                    Message = "Error",
                    Success = false
                };
                return Standerd;
            }

        }


        [HttpPost("FindBuyers")]
        public StanderdJson FindBuyers([FromBody] int Id)//بنستخدم الفروم فورم بكثير احيان عشان نخلي في خيار اضافي لاضافة فايل 
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                
                var data =Buyers.Find(Id);
                var NewData = MappConfig.Map<BuyersModel>(data);
                var Standerd = new StanderdJson
                {
                    Code = Ok().StatusCode,
                    Data = NewData,
                    Message = "Success",
                    Success = true
                };
                return Standerd;
            }
            catch (Exception)
            {
                var Standerd = new StanderdJson
                {
                    Code = BadRequest().StatusCode,
                    Data = new NullColumns(),
                    Message = "Error",
                    Success = false
                };
                return Standerd;
            }

        }
    }
}
