using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;
using WebDisk.Web.Extensions;
using WebDisk.Web.Models;
using WebDisk.Web.Models.Field;
using WebDisk.Web.Models.Home;

namespace WebDisk.Web.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(n =>
            {
                n.CreateMap<Field, FieldViewModel>();
                n.CreateMap<HttpPostedFileBase, FileViewModel>();

                n.CreateMap<FieldShareInformation, FieldShareViewModel>()
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(from => from.Field.Name))
                .ForMember(dest => dest.SharedUserName, opts => opts.MapFrom(from => from.ApplicationUser.UserName))
                .ForMember(dest => dest.AccessType, opts => opts.MapFrom(from => from.AccessType.GetAttribute<DisplayAttribute>().Name))
                .ForMember(dest => dest.ShareType, opts => opts.MapFrom(from => from.ShareType.GetAttribute<DisplayAttribute>().Name));

                n.CreateMap<Field, FieldDescriptionViewModel>()
                .ForMember(dest => dest.Attribute, opts => opts.MapFrom(from => from.Attributes.GetAttribute<DisplayAttribute>().Name));

                n.CreateMap<Field, Field>()
                .ForMember(dest => dest.FieldId, opts => opts.MapFrom(from => Guid.NewGuid()))
                .ForMember(dest => dest.LastModifiedDate, opts => opts.MapFrom(from => DateTime.Now))
                .ForMember(dest => dest.CreationDate, opts => opts.MapFrom(from => DateTime.Now));

                
                
            });
        }
    }
}