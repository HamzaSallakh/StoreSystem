using AutoMapper;
using Domain.Model;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AutoMapper
{
    public class AutoConfigMapper
    {
        public static IMapper CreateMapper()
        {
            var MappConfig = new MapperConfiguration(x =>
            {

                x.CreateMap<Buyers, BuyersModel>().ForMember(x => x.BuyersId, z => z.MapFrom(q => q.Id)).ReverseMap();
                x.CreateMap<Orders, OrdersModel>().ForMember(x => x.OrdersId, z => z.MapFrom(q => q.Id)).ReverseMap();
                x.CreateMap<Payments, PaymentsModel>().ForMember(x => x.PaymentsId, z => z.MapFrom(q => q.Id)).ReverseMap();
                //x.CreateMap<UsersModel, Users>();

            });


            return MappConfig.CreateMapper();

        }
    }
}
