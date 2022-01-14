﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPAM.DreamTour.DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task<IDictionary<T, U>> LoadData<T, U, V>(string storedProcedure, V parameters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}