﻿using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface ISiteSettingsRepository
    {
        Task<SiteSetting> Get();
        Task<SiteSetting> Set(SiteSetting siteSetting);
        Task DeleteAll();
        Task Create(SiteSetting siteSetting);
    }
}
