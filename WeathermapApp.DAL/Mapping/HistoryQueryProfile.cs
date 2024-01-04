using AutoMapper;
using WeathermapApp.DAL.Dto;
using WeathermapApp.DAL.Models;

namespace WeathermapApp.DAL.Mapping;

public class HistoryQueryProfile : Profile
{
    public HistoryQueryProfile()
    {
        CreateMap<HistoryQueryModel, HistoryQueryDto>();
    }
}
