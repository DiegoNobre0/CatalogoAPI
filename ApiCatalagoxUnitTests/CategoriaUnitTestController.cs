using APICatalogo_.Context;
using APICatalogo_.DTOs.Mappings;
using APICatalogo_.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalagoxUnitTests
{
    internal class CategoriaUnitTestController
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public static DbContextOptions<AppContext> DbContextOptions { get; }

        public static string connectionString =
            "Server=localhost;DataBase=APICatalogoDB;Uid=root;Pwd=Dr561203";

        static CategoriaUnitTestController()
        {
            DbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public CategoriaUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            _mapper = config.CreateMapper();

            var context = new AppDbContext(DbContextOptions);
        }

    }
}
