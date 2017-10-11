using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Persistence.DAL;
using Web.AutoMapper.Profiles;

namespace Web.Windsor.Installers
{
    public class AutomapperInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(x =>
                {
                    return new MapperConfiguration(c =>
                    {
                        c.AddProfile<ModelToDtoProfile>();
                    }).CreateMapper();
                }));
        }
    }
}