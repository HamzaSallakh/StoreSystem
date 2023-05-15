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
    public class PaymentsController : ControllerBase
    {
        public IPayments<Payments> Payments { get; }
        public PaymentsController(IPayments<Payments> _Payments)
        {
            Payments = _Payments;
        }

        [HttpPost("GetAll")]
        public StanderdJson GetAll()
        {
            try
            {
                var data = Payments.View();
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<List<PaymentsModel>>(data);
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


        [HttpPost("AddPayments")]
        public StanderdJson AddPayments([FromBody] PaymentsModel model)
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Payments>(model);
                Payments.Add(NewData);
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


        [HttpPost("UpdatePayments")]
        public StanderdJson UpdatePayments([FromBody] PaymentsModel model)
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Payments>(model);
                Payments.Update(NewData);
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

        [HttpPost("DeletePayments")]
        public StanderdJson DeletePayments([FromBody] PaymentsModel model)//بنستخدم الفروم فورم بكثير احيان عشان نخلي في خيار اضافي لاضافة فايل 
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Payments>(model);
                Payments.Delete(NewData);
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


        [HttpPost("FindPayments")]
        public StanderdJson FindPayments([FromBody] int Id)//بنستخدم الفروم فورم بكثير احيان عشان نخلي في خيار اضافي لاضافة فايل 
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();

                var data = Payments.Find(Id);
                var NewData = MappConfig.Map<PaymentsModel>(data);
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
