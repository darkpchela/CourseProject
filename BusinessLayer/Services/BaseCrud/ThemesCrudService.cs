﻿using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.Services.BaseCrud
{
    public class ThemesCrudService : BaseCrudService<Theme, ThemeModel>, IThemesCrudService
    {
        protected override IRepository<Theme> BaseRepository
        {
            get
            {
                return cPUnitOfWork.ThemesRepository;
            }
        }

        public ThemesCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }
    }
}