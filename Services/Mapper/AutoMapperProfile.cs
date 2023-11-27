using AutoMapper;
using DataAccess.Entities;
using Services.Models;

namespace Services.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppointmentPostModel, Appointment>()
                .ForMember(dst => dst.Id, opt => opt.Ignore());
            CreateMap<PrescriptionPostModel, Prescription>()
                .ForMember(dst => dst.Id, opt => opt.Ignore());
        }
    }
}
