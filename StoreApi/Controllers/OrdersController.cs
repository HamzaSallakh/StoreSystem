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
    public class OrdersController : ControllerBase
    {
        public IOrders<Orders> Orders { get; }
        public OrdersController(IOrders<Orders> _Orders)
        {
            Orders = _Orders;
        }

        [HttpPost("GetAll")]
        public StanderdJson GetAll()
        {
            try
            {
                var data = Orders.View();
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<List<OrdersModel>>(data);
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


        [HttpPost("AddOrders")]
        public StanderdJson AddOrders([FromBody] OrdersModel model)
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Orders>(model);
                Orders.Add(NewData);
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


        [HttpPost("UpdateOrders")]
        public StanderdJson UpdateOrders([FromBody] OrdersModel model)
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Orders>(model);
                Orders.Update(NewData);
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

        [HttpPost("DeleteOrders")]
        public StanderdJson DeleteOrders([FromBody] OrdersModel model)//بنستخدم الفروم فورم بكثير احيان عشان نخلي في خيار اضافي لاضافة فايل 
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();
                var NewData = MappConfig.Map<Orders>(model);
                Orders.Delete(NewData);
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


        [HttpPost("FindOrders")]
        public StanderdJson FindOrders([FromBody] int Id)//بنستخدم الفروم فورم بكثير احيان عشان نخلي في خيار اضافي لاضافة فايل 
        {
            try
            {
                var MappConfig = AutoConfigMapper.CreateMapper();

                var data = Orders.Find(Id);
                var NewData = MappConfig.Map<OrdersModel>(data);
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
