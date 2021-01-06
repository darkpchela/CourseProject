﻿using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface IThemesCrudService : IBaseCRUDService<Theme, ThemeModel>
    {
    }
}