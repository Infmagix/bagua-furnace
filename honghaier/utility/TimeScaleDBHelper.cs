using System;
using System.Collections.Generic;
using System.Diagnostics;
using Npgsql;
using System.Text;
using System.Linq;

namespace honghaier.Utility
{
    public interface IDbProvider
    {
        void WriteDict(object obj);
        void Reset();
    }

    public class TimeScaleDBHelper : IDbProvider
    {
        private log4net.ILog log = null;
        private readonly string Database = Const.TimeScaleDBName;
        private readonly int Port = Const.TimeScaleDBPort;
        public readonly string User = Const.TimeScaleServerUser;
        public readonly string Password = Const.TimeScaleDBPassword;
        public readonly string ServerIP = Const.TimeScaleServerIP;
        public readonly string MerlinTableName = Const.TimeScaleTableName;

        private Stopwatch sw = new Stopwatch();
        private string _connectionStr;
        private NpgsqlConnection _connection;

        public TimeScaleDBHelper()
        {
            if (!Const.TimeScaleDBEnable) { return; }

            if (string.IsNullOrEmpty(MerlinTableName))
                MerlinTableName = "merlin_hhr";

            if (string.IsNullOrEmpty(ServerIP))
                ServerIP = "localhost";

            if (string.IsNullOrEmpty(Database))
                Database = "postgres";

            if (Port <= 0)
                Port = 5432;

            if (string.IsNullOrEmpty(User))
                User = "postgres";
            if (string.IsNullOrEmpty(Password))
                Password = "postgres";

            log = log4net.LogManager.GetLogger("TimeScaleDBHelper");

            _BuildConnection();
        }

        public TimeScaleDBHelper(string tsServerIP, string tsUser, string tsPassword, string tsDatabase, int tsPort)
        {
            ServerIP = tsServerIP;
            User = tsUser;
            Password = tsPassword;
            Database = tsDatabase;
            Port = tsPort;
            _BuildConnection();
        }

        private Dictionary<string, string> _requredFiled2Types = null;

        //Configuration from .Net data type to postgres data type
        private Dictionary<string, string> _dbTypeConfig = new Dictionary<string, string>()
        {
            { "Double", "double precision" },
            { "Single", "real" },
            { "Int32",  "integer" },
            { "Int64",  "bigint" },
            { "Byte",  "smallint" },
            { "Boolean", "smallint" },
            { "Char",  "char" },
            { "String", "varchar" },
        };

        private bool _tableSchemaChecked = false;

        public void WriteDict(object obj)
        {
            var keyValues = obj as Dictionary<string, object>;
            if (_connection == null)
            {
                _BuildConnection();
            }
            
            try
            {
                _CreateHyperTableIfNotExist(MerlinTableName, keyValues);
                // Get all equipment variables
                using (var commnd = _connection.CreateCommand())
                {
                    commnd.CommandText = _BuildSQL_InsertRow(keyValues);

                    int i = 0;
                    foreach (var variable in keyValues)
                    {
                        var fieldName = variable.Key;
                        var type = _QueryDBType(fieldName);
                        if (string.IsNullOrEmpty(type))
                        {
                            continue;
                        }
                        commnd.Parameters.Add(new NpgsqlParameter($"@VAL{i++}", variable.Value));
                    }
                    commnd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _connection.Close();
                _connection = null;
                LogHelper.Exception(log, ex);
            }
        }

        private void _BuildConnection()
        {
            if(_connection != null && _connection.State.HasFlag(System.Data.ConnectionState.Open))
            {
                _connection.Close();
            }
            _connectionStr = $"Host={ServerIP};Username={User};Password={Password};Database={Database};Port={Port}";
            _connection = new NpgsqlConnection(_connectionStr);
            _connection.Open();
        }

        private string _QueryDBType(string fieldName, object objData = null)
        {
            //If objData is null, then only query cached field type
            if(_requredFiled2Types.TryGetValue(fieldName, out var pgType) || objData == null)
            {
                return pgType;
            }

            var objType = objData.GetType().Name;
            if (_dbTypeConfig.TryGetValue(objType, out pgType))
            {
                _requredFiled2Types.Add(fieldName, pgType);
            }
            else
            {

            }
            return pgType;
        }

        private bool _IsTableExist(string tableName)
        {
            using (var dbcmd = _connection.CreateCommand())
            {
                dbcmd.CommandText = _BuildSQL_IsTableExist(tableName);
                using (var dr = dbcmd.ExecuteReader())
                {
                    return dr.HasRows;
                }
            }
        }

        private void _CreateHyperTableIfNotExist(string tableName, Dictionary<string, object> fieldDatas)
        {
            //Cached Filed Types
            if (_requredFiled2Types == null)
            {
                _requredFiled2Types = new Dictionary<string, string>();
                foreach (var variable in fieldDatas)
                {
                   _QueryDBType(variable.Key, variable.Value);
                }
            }

            if (!_IsTableExist(tableName))
            {
                using (var commnd = _connection.CreateCommand())
                {
                    commnd.CommandText = _BuildSQL_CreateTable(tableName, _requredFiled2Types.Keys);
                    commnd.ExecuteNonQuery();
                    commnd.CommandText = _BuildSQL_create_hypertable(tableName);
                    commnd.ExecuteNonQuery();
                }
            }
            else
            {
                _CheckTableSchema(fieldDatas);
            }
        }

        public void Reset()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
            _BuildConnection();
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }

        private void _CheckTableSchema(Dictionary<string, object> fieldValues)
        {
            if(_tableSchemaChecked)
            {
                return;
            }

            var existField2Types = _QueryTableSchemas(MerlinTableName);
            var existColField = new HashSet<string>(existField2Types.Keys);

            var requiredColumnFields = _requredFiled2Types.Keys;
            // Check removing columns if any
            existColField.Remove("time");
            var columns2Remove = existColField.Except(requiredColumnFields);
            var sqlStatement = _BuildSQL_RemoveColumns(MerlinTableName, columns2Remove);
            if (!string.IsNullOrEmpty(sqlStatement))
            {
                // If varialble type is valid, do append varialble as a column
                using (var commnd = _connection.CreateCommand())
                {
                    commnd.CommandText = sqlStatement;
                    commnd.ExecuteNonQuery();
                }
            }
             
            var var2Add = requiredColumnFields.Except(existColField);
            sqlStatement = _BuildSQL_AddColumns(var2Add);
            if (!string.IsNullOrEmpty(sqlStatement))
            {
                // If varialble type is valid, do append varialble as a column
                using (var commnd = _connection.CreateCommand())
                {
                    commnd.CommandText = sqlStatement;
                    commnd.ExecuteNonQuery();
                }
            }
            _tableSchemaChecked = true;
        }

        private Dictionary<string, string> _QueryTableSchemas(string tableName)
        {
            var field2TypeDict = new Dictionary<string, string>();
            using (var dbcmd = _connection.CreateCommand())
            {
                dbcmd.CommandText = _BuildSQL_QuerySchema(tableName);
                using (var dr = dbcmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string strColumn = dr[0].ToString();
                        string strType = dr[1].ToString();
                        field2TypeDict[strColumn] = strType;
                    }
                }
            }
            return field2TypeDict;
        }



        private string _BuildSQL_InsertRow(Dictionary<string, object> variables)
        {
            var sqlBuilder = new StringBuilder();

            sqlBuilder.Append($"INSERT INTO \"{MerlinTableName}\" ");
            sqlBuilder.Append(" (time");
            foreach (var variable in variables)
            {
                var fieldName = variable.Key;
                var type = _QueryDBType(fieldName);
                if (string.IsNullOrEmpty(type))
                {
                    continue;
                }
                sqlBuilder.Append($", \"{fieldName}\"");
            }
            sqlBuilder.Append(" )");

            sqlBuilder.Append(" VALUES (");
            sqlBuilder.Append("NOW()");
            int i = 0;
            foreach (var variable in variables)
            {
                var fieldName = variable.Key;
                var type = _QueryDBType(fieldName);
                if (string.IsNullOrEmpty(type))
                {
                    continue;
                }
                sqlBuilder.Append($", @VAL{i++}");
            }

            sqlBuilder.Append(" )");
            return sqlBuilder.ToString();
        }

        private string _BuildSQL_CreateTable(string tableName, IEnumerable<string> fieldNames)
        {
            var fieldBuilder = new StringBuilder();
            fieldBuilder.Append( $"CREATE TABLE \"{tableName}\" ");
            fieldBuilder.Append(" (");
            fieldBuilder.Append("time TIMESTAMPTZ NOT NULL");
            foreach (var fieldName in fieldNames)
            {
                fieldBuilder.Append($", \"{fieldName}\" {_QueryDBType(fieldName)}");
            }
            fieldBuilder.Append(")");
            return fieldBuilder.ToString();
        }

        private string _BuildSQL_IsTableExist(string tableName)
        {
            return $@" SELECT table_name FROM information_schema.tables
                WHERE table_name = '{tableName}'order by table_name;";
        }

        private string _BuildSQL_QuerySchema(string tableName)
        {
            return $@" SELECT column_name, data_type FROM information_schema.columns
                     WHERE table_name = '{tableName}';";
        }

        private string _BuildSQL_AddColumns(IEnumerable<string> fieldNames)
        {
            if (!fieldNames.Any()) return null;

            var fieldsStr = new StringBuilder();
            fieldsStr.Append($"ALTER TABLE \"{MerlinTableName}\" ");
            foreach (var fieldName in fieldNames)
            {
                var varType = _QueryDBType(fieldName);
                if (string.IsNullOrEmpty(varType)) continue;
                fieldsStr.Append($"ADD COLUMN \"{fieldName}\" {varType},");
            }
            fieldsStr.Replace(',', ';', fieldsStr.Length - 1, 1);
            return fieldsStr.ToString();
        }

        private static string _BuildSQL_RemoveColumns(string tableName, IEnumerable<string> fieldNames)
        {
            if (!fieldNames.Any()) return null;

            var fieldsStr = new StringBuilder();
            fieldsStr.Append($"ALTER TABLE \"{tableName}\" ");
            foreach (var col in fieldNames)
            {
                fieldsStr.Append($"DROP COLUMN \"{col}\",");
            }
            fieldsStr.Replace(',', ';', fieldsStr.Length - 1, 1);
            return fieldsStr.ToString();
            // If varialble type is valid, do append varialble as a column
        }

        private static string _BuildSQL_create_hypertable(string tableName)
        {
            return $"SELECT create_hypertable('\"{tableName}\"', 'time');";
        }
    }
}
