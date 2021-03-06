using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Rye.DataAccess;
using Rye.MySql;

namespace Demo.DataAccess
{
	public partial class DaoRole : IRole
    {
        public MySqlConnectionProvider ConnectionProvider { get; }

        public DaoRole(MySqlConnectionProvider provider)
        {
            ConnectionProvider = provider;
        }

		public int GetLastIdentity()
		{
			using Connector conn = ConnectionProvider.GetConnection();
			return conn.Connection.ExecuteScalar<int>("SELECT SCOPE_IDENTITY()");
		}

        public int Insert(Role model, IDbTransaction trans, IDbConnection conn)
        {
        	string sql = "INSERT INTO role (appId,name,status,remarks,createTime) VALUES (@AppId,@Name,@Status,@Remarks,@CreateTime);";

            if (trans == null)
                return conn.Execute(sql, param: model, commandType: CommandType.Text);
            else
                return conn.Execute(sql, param: model, commandType: CommandType.Text, transaction: trans);
        }

        public async Task<int> InsertAsync(Role model, IDbTransaction trans, IDbConnection conn)
        {
        	string sql = "INSERT INTO role (appId,name,status,remarks,createTime) VALUES (@AppId,@Name,@Status,@Remarks,@CreateTime);";

            if (trans == null)
                return await conn.ExecuteAsync(sql, param: model, commandType: CommandType.Text);
            else
                return await conn.ExecuteAsync(sql, param: model, commandType: CommandType.Text, transaction: trans);
        }

        public int Insert(Role model)
        {
            using Connector conn = ConnectionProvider.GetConnection();
            return Insert(model, null, conn.Connection);
        }

        public async Task<int> InsertAsync(Role model)
        {
            using Connector conn = ConnectionProvider.GetConnection();
            return await InsertAsync(model, null, conn.Connection);
        }

        public int BatchInsert(IEnumerable<Role> items, IDbTransaction trans, IDbConnection conn)
        {
        	string sql = "INSERT INTO role (appId,name,status,remarks,createTime) VALUES (@AppId,@Name,@Status,@Remarks,@CreateTime);";

            if (trans == null)
                return conn.Execute(sql, param: items, commandType: CommandType.Text);
            else
                return conn.Execute(sql, param: items, commandType: CommandType.Text, transaction: trans);
        }
        
        public async Task<int> BatchInsertAsync(IEnumerable<Role> items, IDbTransaction trans, IDbConnection conn)
        {
        	string sql = "INSERT INTO role (appId,name,status,remarks,createTime) VALUES (@AppId,@Name,@Status,@Remarks,@CreateTime);";

             if (trans == null)
                return await conn.ExecuteAsync(sql, param: items, commandType: CommandType.Text);
            else
                return await conn.ExecuteAsync(sql, param: items, commandType: CommandType.Text, transaction: trans);
        }

        public int BatchInsert(IEnumerable<Role> items)
        {
        	string sql = "INSERT INTO role (appId,name,status,remarks,createTime) VALUES (@AppId,@Name,@Status,@Remarks,@CreateTime);";

            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.Execute(sql, param: items, commandType: CommandType.Text);
        }
        
        public async Task<int> BatchInsertAsync(IEnumerable<Role> items)
        {
        	string sql = "INSERT INTO role (appId,name,status,remarks,createTime) VALUES (@AppId,@Name,@Status,@Remarks,@CreateTime);";

            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.ExecuteAsync(sql, param: items, commandType: CommandType.Text);
        }

        public int InsertUpdate(Role model, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "UPDATE role SET  appId=@AppId, name=@Name, status=@Status, remarks=@Remarks, createTime=@CreateTime WHERE 1=1  AND id=@Id;INSERT INTO role (appId,name,status,remarks,createTime) SELECT @AppId,@Name,@Status,@Remarks,@CreateTime WHERE NOT EXISTS (SELECT 1 FROM role where 1=1  AND id=@Id)";
            if (trans == null)
                return conn.Execute(sql, param: model, commandType: CommandType.Text);
            else
                return conn.Execute(sql, param: model, commandType: CommandType.Text, transaction: trans);
        }
        
        public async Task<int> InsertUpdateAsync(Role model, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "UPDATE role SET  appId=@AppId, name=@Name, status=@Status, remarks=@Remarks, createTime=@CreateTime WHERE 1=1  AND id=@Id;INSERT INTO role (appId,name,status,remarks,createTime) SELECT @AppId,@Name,@Status,@Remarks,@CreateTime WHERE NOT EXISTS (SELECT 1 FROM role where 1=1  AND id=@Id)";
            if (trans == null)
                return await conn.ExecuteAsync(sql, param: model, commandType: CommandType.Text);
            else
                return await conn.ExecuteAsync(sql, param: model, commandType: CommandType.Text, transaction: trans);
        }

        public int InsertUpdate(Role model)
        {
            using Connector conn = ConnectionProvider.GetConnection();
            return InsertUpdate(model, null, conn.Connection);
        }
        
        public async Task<int> InsertUpdateAsync(Role model)
        {
            using Connector conn = ConnectionProvider.GetConnection();
            return await InsertUpdateAsync(model, null, conn.Connection);
        }
        
        public int Update(Role model, IDbTransaction trans, IDbConnection conn)
		{
            string sql = "UPDATE role SET  appId=@AppId, name=@Name, status=@Status, remarks=@Remarks, createTime=@CreateTime WHERE 1=1  AND id=@Id";
            if (trans == null)
                return conn.Execute(sql, param: model, commandType: CommandType.Text);
            else
                return conn.Execute(sql, param: model, commandType: CommandType.Text, transaction: trans);
		}
        
        public int Update(Role model)
		{
			using Connector conn = ConnectionProvider.GetConnection();
            return Update(model, null, conn.Connection);
		}
        
        public async Task<int> UpdateAsync(Role model, IDbTransaction trans, IDbConnection conn)
		{
            string sql = "UPDATE role SET  appId=@AppId, name=@Name, status=@Status, remarks=@Remarks, createTime=@CreateTime WHERE 1=1  AND id=@Id";
            if (trans == null)
                return await conn.ExecuteAsync(sql, param: model, commandType: CommandType.Text);
            else
                return await conn.ExecuteAsync(sql, param: model, commandType: CommandType.Text, transaction: trans);
		}
        
        public async Task<int> UpdateAsync(Role model)
		{
            using Connector conn = ConnectionProvider.GetConnection();
            return await UpdateAsync(model, null, conn.Connection);
        }

        public bool Delete(int id, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "DELETE FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
            if (trans == null)
                return conn.Execute(sql, param: _params, commandType: CommandType.Text) > 0;
            else
                return conn.Execute(sql, param: _params, commandType: CommandType.Text,transaction: trans) > 0;
        }

        public bool Delete(int id)
        {
            using Connector conn = ConnectionProvider.GetConnection();
            return Delete(id, null, conn.Connection);
        }

        public async Task<bool> DeleteAsync(int id, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "DELETE FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
            if (trans == null)
                return await conn.ExecuteAsync(sql, param: _params, commandType: CommandType.Text) > 0;
            else
                return await conn.ExecuteAsync(sql, param: _params, commandType: CommandType.Text,transaction: trans) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {   
            using Connector conn = ConnectionProvider.GetConnection();
            return await DeleteAsync(id, null, conn.Connection);
        }

        public Role GetModel(int id)
		{
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.QueryFirstOrDefault<Role>(sql, param: _params, commandType: CommandType.Text);
		}				
        
        public Role GetModelByWriteDb(int id)
		{
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.QueryFirstOrDefault<Role>(sql, param: _params, commandType: CommandType.Text);
		}
        
        public async Task<Role> GetModelAsync(int id)
		{  
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.QueryFirstOrDefaultAsync<Role>(sql, param: _params, commandType: CommandType.Text);
		}	
        
        public async Task<Role> GetModelByWriteDbAsync(int id)
		{  
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
    		using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.QueryFirstOrDefaultAsync<Role>(sql, param: _params, commandType: CommandType.Text);
		}	

        public Role GetModel(int id, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
            
            if (trans == null)
                return conn.QueryFirstOrDefault<Role>(sql, param: _params, commandType: CommandType.Text);

            else
                return conn.QueryFirstOrDefault<Role>(sql, param: _params, commandType: CommandType.Text, transaction: trans);

        }

        public async Task<Role> GetModelAsync(int id, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role WHERE 1=1 AND id=@Id";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
            
            if (trans == null)
                return await conn.QueryFirstOrDefaultAsync<Role>(sql, param: _params, commandType: CommandType.Text);

            else
                return await conn.QueryFirstOrDefaultAsync<Role>(sql, param: _params, commandType: CommandType.Text, transaction: trans);

        }

        public Role GetModel(object param, string whereSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public async Task<Role> GetModelAsync(object param, string whereSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public Role GetModelByWriteDb(object param, string whereSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public async Task<Role> GetModelByWriteDbAsync(object param, string whereSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public Role GetModel(object param, string whereSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + " LIMIT 1";
            
            if (trans == null)
                return conn.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text);

            else
                return conn.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text, transaction: trans);

        }

        public async Task<Role> GetModelAsync(object param, string whereSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + " LIMIT 1";
            
            if (trans == null)
                return await conn.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text);

            else
                return await conn.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text, transaction: trans);

        }

        public Role FirstOrDefault(object param, string whereSql, string orderSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + "ORDER BY " + orderSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public async Task<Role> FirstOrDefaultAsync(object param, string whereSql, string orderSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + "ORDER BY " + orderSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public Role FirstOrDefaultByWriteDb(object param, string whereSql, string orderSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + "ORDER BY " + orderSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public async Task<Role> FirstOrDefaultByWriteDbAsync(object param, string whereSql, string orderSql)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + "ORDER BY " + orderSql + " LIMIT 1";
            
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public Role FirstOrDefault(object param, string whereSql, string orderSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + "ORDER BY " + orderSql + " LIMIT 1";
            
            if (trans == null)
                return conn.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text);

            else
                return conn.QueryFirstOrDefault<Role>(sql, param: param, commandType: CommandType.Text, transaction: trans);

        }

        public async Task<Role> FirstOrDefaultAsync(object param, string whereSql, string orderSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role LIMIT 1 WHERE 1=1 AND " + whereSql + "ORDER BY " + orderSql + " LIMIT 1";
            
            if (trans == null)
                return await conn.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text);

            else
                return await conn.QueryFirstOrDefaultAsync<Role>(sql, param: param, commandType: CommandType.Text, transaction: trans);

        }
		
        public IEnumerable<Role> GetList()
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
                
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.Query<Role>(sql, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Role>> GetListAsync()
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
            
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.QueryAsync<Role>(sql, commandType: CommandType.Text);
        }

        public IEnumerable<Role> GetList(IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
                
            if (trans == null)
                return conn.Query<Role>(sql, commandType: CommandType.Text);

            else
                return conn.Query<Role>(sql, commandType: CommandType.Text, transaction: trans);

        }

        public async Task<IEnumerable<Role>> GetListAsync(IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
                
            return await conn.QueryAsync<Role>(sql, commandType: CommandType.Text);
        }

        public IEnumerable<Role> GetListByWriteDb()
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
                
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.Query<Role>(sql, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Role>> GetListByWriteDbAsync()
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
                
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.QueryAsync<Role>(sql, commandType: CommandType.Text);
        }

        public IEnumerable<Role> GetListByWriteDb(IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
                
            if (trans == null)
                return conn.Query<Role>(sql, commandType: CommandType.Text);

            else
                return conn.Query<Role>(sql, commandType: CommandType.Text, transaction: trans);

        }

        public async Task<IEnumerable<Role>> GetListByWriteDbAsync(IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  ORDER BY id DESC";
                
            return await conn.QueryAsync<Role>(sql, commandType: CommandType.Text);
        }

        public IEnumerable<Role> GetPage(object param, string whereSql, string orderSql, int pageIndex, int pageSize)
        {
            string sql = string.Format("SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  WHERE {0} ORDER BY {1} LIMIT {3},{2}", whereSql, orderSql, (pageIndex - 1) * pageSize, pageSize);
                
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.Query<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Role>> GetPageAsync(object param, string whereSql, string orderSql, int pageIndex, int pageSize)
        {
            string sql = string.Format("SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  WHERE {0} ORDER BY {1} LIMIT {3},{2}", whereSql, orderSql, (pageIndex - 1) * pageSize, pageSize);
                
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.QueryAsync<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public IEnumerable<Role> GetPageByWriteDb(object param, string whereSql, string orderSql, int pageIndex, int pageSize)
        {
            string sql = string.Format("SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  WHERE {0} ORDER BY {1} LIMIT {3},{2}", whereSql, orderSql, (pageIndex - 1) * pageSize, pageSize);
                
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.Query<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Role>> GetPageByWriteDbAsync(object param, string whereSql, string orderSql, int pageIndex, int pageSize)
        {
            string sql = string.Format("SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  WHERE {0} ORDER BY {1} LIMIT {3},{2}", whereSql, orderSql, (pageIndex - 1) * pageSize, pageSize);
                
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.QueryAsync<Role>(sql, param: param, commandType: CommandType.Text);
        }

        public IEnumerable<Role> GetPage(object param, string whereSql, string orderSql, int pageIndex, int pageSize, IDbTransaction trans, IDbConnection conn)
        {
            string sql = string.Format("SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  WHERE {0} ORDER BY {1} LIMIT {3},{2}", whereSql, orderSql, (pageIndex - 1) * pageSize, pageSize);
                
            if (trans == null)
                return conn.Query<Role>(sql, param: param, commandType: CommandType.Text);

            else
                return conn.Query<Role>(sql, param: param, commandType: CommandType.Text, transaction: trans);

        }

        public async Task<IEnumerable<Role>> GetPageAsync(object param, string whereSql, string orderSql, int pageIndex, int pageSize, IDbTransaction trans, IDbConnection conn)
        {
            string sql = string.Format("SELECT id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime FROM role  WHERE {0} ORDER BY {1} LIMIT {3},{2}", whereSql, orderSql, (pageIndex - 1) * pageSize, pageSize);
                
            if (trans == null)
                return await conn.QueryAsync<Role>(sql, param: param, commandType: CommandType.Text);

            else
                return await conn.QueryAsync<Role>(sql, param: param, commandType: CommandType.Text, transaction: trans);

        }

        public bool Exists(int id)
        {
            string sql = "SELECT 1 FROM role  WHERE 1=1  AND id=@Id LIMIT 1";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.ExecuteScalar<int>(sql, param: _params, commandType: CommandType.Text) > 0;
        }

        public bool ExistsByWriteDb(int id)
        {
            string sql = "SELECT 1 FROM role  WHERE 1=1  AND id=@Id LIMIT 1";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.ExecuteScalar<int>(sql, param: _params, commandType: CommandType.Text) > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            string sql = "SELECT 1 FROM role  WHERE 1=1  AND id=@Id LIMIT 1";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, param: _params, commandType: CommandType.Text) > 0;
        }

        public async Task<bool> ExistsByWriteDbAsync(int id)
        {
            string sql = "SELECT 1 FROM role  WHERE 1=1  AND id=@Id LIMIT 1";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, param: _params, commandType: CommandType.Text) > 0;
        }

        public bool Exists(int id, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT 1 FROM role  WHERE 1=1  AND id=@Id LIMIT 1";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            if (trans == null)
                return conn.ExecuteScalar<int>(sql, param: _params, commandType: CommandType.Text) > 0;
            else
                return conn.ExecuteScalar<int>(sql, param: _params, commandType: CommandType.Text, transaction: trans) > 0;
        }

        public async Task<bool> ExistsAsync(int id, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT 1 FROM role  WHERE 1=1  AND id=@Id LIMIT 1";
            var _params = new DynamicParameters();
			_params.Add("@Id", value: id, direction: ParameterDirection.Input);
                
            if (trans == null)
                return await conn.ExecuteScalarAsync<int>(sql, param: _params, commandType: CommandType.Text) > 0;
            else
                return await conn.ExecuteScalarAsync<int>(sql, param: _params, commandType: CommandType.Text, transaction: trans) > 0;
        }

        public bool Exists(object param, string whereSql)
        {
            string sql = "SELECT 1 FROM [role] WITH(NOLOCK) WHERE 1=1 AND " + whereSql + " LIMIT 1";
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text) > 0;
        }

        public async Task<bool> ExistsAsync(object param, string whereSql)
        {
            string sql = "SELECT 1 FROM [role] WITH(NOLOCK) WHERE 1=1 AND " + whereSql + " LIMIT 1";
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text) > 0;
        }

        public bool ExistsByWriteDb(object param, string whereSql)
        {
            string sql = "SELECT 1 FROM [role] WITH(NOLOCK) WHERE 1=1 AND " + whereSql + " LIMIT 1";
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text) > 0;
        }

        public async Task<bool> ExistsByWriteDbAsync(object param, string whereSql)
        {
            string sql = "SELECT 1 FROM [role] WITH(NOLOCK) WHERE 1=1 AND " + whereSql + " LIMIT 1";
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text) > 0;
        }

        public bool Exists(object param, string whereSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT 1 FROM [role] WITH(NOLOCK) WHERE 1=1 AND " + whereSql + " LIMIT 1";
            if (trans == null)
                return conn.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text) > 0;
            else
                return conn.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text, transaction: trans) > 0;
        }

        public async Task<bool> ExistsAsync(object param, string whereSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT 1 FROM [role] WITH(NOLOCK) WHERE 1=1 AND " + whereSql + " LIMIT 1";
            if (trans == null)
                return await conn.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text) > 0;
            else
                return await conn.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text, transaction: trans) > 0;
        }

        public int Count()
        {
            string sql = "SELECT COUNT(1) FROM role ";
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.ExecuteScalar<int>(sql, commandType: CommandType.Text);
        }

        public async Task<int> CountAsync()
        {
            string sql = "SELECT COUNT(1) FROM role ";
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, commandType: CommandType.Text);
        }

        public int CountByWriteDb()
        {
            string sql = "SELECT COUNT(1) FROM role ";
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.ExecuteScalar<int>(sql, commandType: CommandType.Text);
        }

        public async Task<int> CountByWriteDbAsync()
        {
            string sql = "SELECT COUNT(1) FROM role ";
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, commandType: CommandType.Text);
        }

        public int Count(object param, string whereSql)
        {
            string sql = "SELECT COUNT(1) FROM role  WHERE 1=1 AND " + whereSql;
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return conn.Connection.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text);
        }

        public async Task<int> CountAsync(object param, string whereSql)
        {
            string sql = "SELECT COUNT(1) FROM role  WHERE 1=1 AND " + whereSql;
            using Connector conn = ConnectionProvider.GetReadOnlyConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text);
        }

        public int CountByWriteDb(object param, string whereSql)
        {
            string sql = "SELECT COUNT(1) FROM role  WHERE 1=1 AND " + whereSql;
            using Connector conn = ConnectionProvider.GetConnection();
            return conn.Connection.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text);
        }
        
        public async Task<int> CountByWriteDbAsync(object param, string whereSql)
        {
            string sql = "SELECT COUNT(1) FROM role  WHERE 1=1 AND " + whereSql;
            using Connector conn = ConnectionProvider.GetConnection();
            return await conn.Connection.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text);
        }

        public int Count(IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT COUNT(1) FROM role ";
            if (trans == null)
                return conn.ExecuteScalar<int>(sql, commandType: CommandType.Text);
            else
                return conn.ExecuteScalar<int>(sql, commandType: CommandType.Text, transaction: trans);
        }

        public async Task<int> CountAsync(IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT COUNT(1) FROM role ";
            if (trans == null)
                return await conn.ExecuteScalarAsync<int>(sql, commandType: CommandType.Text);
            else
                return await conn.ExecuteScalarAsync<int>(sql, commandType: CommandType.Text, transaction: trans);
        }

        public int Count(object param, string whereSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT COUNT(1) FROM role WHERE 1=1 AND " + whereSql;
            if (trans == null)
                return conn.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text);
            else
                return conn.ExecuteScalar<int>(sql, param: param, commandType: CommandType.Text, transaction: trans);
        }

        public async Task<int> CountAsync(object param, string whereSql, IDbTransaction trans, IDbConnection conn)
        {
            string sql = "SELECT COUNT(1) FROM role WHERE 1=1 AND " + whereSql;
            if (trans == null)
                return await conn.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text);
            else
                return await conn.ExecuteScalarAsync<int>(sql, param: param, commandType: CommandType.Text, transaction: trans);
        }
        
        
        private string GetColumns()
        {
            return "id Id,appId AppId,name Name,status Status,remarks Remarks,createTime CreateTime";
        }
	}
}
