using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MTS_CRM.DB;
using MTS_CRM.DTO;
namespace MTS_CRM.WCF
{
   
        public class AutoMapper
        {
            //public static void config()
            //{
            //    Mapper.Initialize( cfg => cfg.CreateMap<Author,AuthorDTO>());
            //}

            public MapperConfiguration Configure()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //way one 
                    cfg.CreateMap<Employee, EmployeeDTO>();
                    //way two
                    cfg.AddProfile<AuthorMappingProfile>();
                }
               );

                return config;
            }
        }
        public class AuthorMappingProfile : Profile
        {
            public AuthorMappingProfile()
            {
                CreateMap<Employee, EmployeeDTO>().ReverseMap();
            }
        }
    
}
