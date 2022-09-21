﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ULABOBE.DataAccess.Data;
using ULABOBE.DataAccess.Repository.IRepository;

namespace ULABOBE.DataAccess.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _db;
        private static string ConnectionString = "";

        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        
        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon=new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        
        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                var result=SqlMapper.QueryMultiple(sqlCon,procedureName,param, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<T1>().ToList();
                var item2 = result.Read<T2>().ToList();

                if (item1!=null && item2!=null)
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
                }
                
            }
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }

        //public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> List<T1, T2, T3>(string procedureName, DynamicParameters param = null)
        //{
        //    using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
        //    {
        //        sqlCon.Open();
        //        var result = SqlMapper.QueryMultiple(sqlCon, procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
        //        var item1 = result.Read<T1>().ToList();
        //        var item2 = result.Read<T2>().ToList();
        //        var item3 = result.Read<T3>().ToList();

        //        if (item1 != null && item2 != null && item3 != null)
        //        {
        //            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(item1, item2, item3);
        //        }

        //    }
        //    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(new List<T1>(), new List<T2>(), new List<T3>());
        //}

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName,param,commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                var value = sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                return (T) Convert.ChangeType(value.FirstOrDefault(), typeof(T));
            }
        }
    }
}