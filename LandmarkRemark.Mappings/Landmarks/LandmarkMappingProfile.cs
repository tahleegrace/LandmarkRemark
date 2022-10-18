using AutoMapper;
using NetTopologySuite.Geometries;

using LandmarkRemark.Entities;
using LandmarkRemark.Models.Landmarks;

namespace LandmarkRemark.Mappings.Landmarks
{
    /// <summary>
    /// Defines mappings between landmark domain and entity model classes.
    /// </summary>
    public class LandmarkMappingProfile : Profile
    {
        public LandmarkMappingProfile()
        {
            CreateMap<CreateLandmarkRequest, Landmark>()
                .ForMember(dest => dest.Location, source => source.MapFrom(item => new Point(item.Longitude, item.Latitude)));

            CreateMap<Landmark, LandmarkDTO>()
                .ForMember(dest => dest.Longitude, source => source.MapFrom(item => item.Location.X))
                .ForMember(dest => dest.Latitude, source => source.MapFrom(item => item.Location.Y));
        }
    }
}