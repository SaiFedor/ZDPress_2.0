using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using ZDPress.Dal.Entities;
using ZDPress.Opc;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using log4net.Repository.Hierarchy;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;

namespace ZDPress.Dal
{
    public class ZdPressDal
    {

        static bool isConnected {get;set;}
        public ZdPressDal()
        {
            //if (!isConnected)
            //{
            //    DbProviderFactory factory = SqlClientFactory.Instance;
            //    using (DbConnection conn = OpenConnection(factory, ConnectionString))
            //    {
            //        string connState = conn.State.ToString();
            //        if (conn.State == ConnectionState.Open)
            //        {
            //            isConnected = true;
            //        }
            //        else
            //        {
            //            CreateDB(factory);
            //        }
            //    }
            //}                  
        }

        private void CreateDB(DbProviderFactory factory)
        {
            using (DbConnection conn = OpenConnection(factory, ConnectionStringDBMaster))
            {
                using (DbCommand cmd = CreateTextCommand(conn, CreateDBCommandAsText()))
                {                    
                    try
                    {
                        Logger.Log.Debug("Создана новая БД - ZDPress1");
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Не удалось создать новую БД", ex);
                        //return false;
                        throw;
                    }
                }
            }

            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, CreateOperationTableCommandAsText()))
                {
                    try
                    {
                        Logger.Log.Debug("Создана таблица Operations");
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Не удалось создать таблицу Operations", ex);
                        //return false;
                        throw;
                    }
                }
                using (DbCommand cmd1 = CreateTextCommand(conn, CreateOperationDataTableCommandAsText()))
                {
                    try
                    {
                        Logger.Log.Debug("Создана таблица OperationsData");
                        cmd1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Не удалось создать таблицу OperationsData", ex);
                        //return false;
                        throw;
                    }
                }
            }
        }

        private string CreateOperationDataTableCommandAsText()
        {
            return @"SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[PressOperationData](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DispPress] [decimal](10, 2) NULL,
	[DlinaSopr] [int] NULL,
	[PressOperationId] [bigint] NULL,
	[DateInsert] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PressOperationData] ADD  CONSTRAINT [PressOperationData_DateInsert_DV]  DEFAULT (getdate()) FOR [DateInsert]

ALTER TABLE [dbo].[PressOperationData]  WITH CHECK ADD  CONSTRAINT [PressOperationData2PressOperations_FK] FOREIGN KEY([PressOperationId])
REFERENCES [dbo].[PressOperations] ([ID])

ALTER TABLE [dbo].[PressOperationData] CHECK CONSTRAINT [PressOperationData2PressOperations_FK]";
        }

        private string CreateOperationTableCommandAsText()
        {
            return @"SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[PressOperations](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OperationStart] [datetime] NULL,
	[OperationStop] [datetime] NULL,
	[FactoryNumber] [varchar](max) NULL,
	[WheelNumber] [varchar](max) NULL,
	[DiagramNumber] [varchar](max) NULL,
	[AxisNumber] [varchar](max) NULL,
	[WheelType] [varchar](max) NULL,
	[Side] [varchar](max) NULL,
	[DWheel] [decimal](10, 2) NULL,
	[DAxis] [decimal](10, 2) NULL,
	[LengthStup] [int] NULL,
	[Natiag] [decimal](10, 2) NULL,
	[LengthSopriazh] [decimal](10, 2) NULL,
	[Power100mm] [decimal](10, 2) NULL,
	[MaxPower] [decimal](10, 2) NULL,
	[LengthLines] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
        }

        private string CreateDBCommandAsText()
        {
            return @"CREATE DATABASE [ZDPress1]
 CONTAINMENT = NONE
 ON PRIMARY
(NAME = N'ZDPress', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ZDPress1.mdf' , SIZE = 94208KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON
(NAME = N'ZDPress_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ZDPress1_log.ldf' , SIZE = 1108800KB , MAXSIZE = 2048GB , FILEGROWTH = 10 %)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT


IF(1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC[ZDPress].[dbo].[sp_fulltext_database] @action = 'enable'
end";
        }

        public string ConnectionStringDBMaster
        {
            get { return ConfigurationManager.ConnectionStrings["DBMaster"].ConnectionString; }
        }

        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ZDPress1"].ConnectionString; }
        }

            public bool ClearOperationsTables (DateTime startDate, DateTime endDate)
        {
            DbProviderFactory factory = SqlClientFactory.Instance;
           
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, DeletePressOperationDataCommandAsText()))
                {
                    AddParameter(cmd, "@startDate", startDate, DbType.DateTime);
                    AddParameter(cmd, "@endDate", endDate, DbType.DateTime);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Не удалось удалить операции", ex);
                        return false;
                        throw;
                    }
                }

                using (DbCommand cmd = CreateTextCommand(conn, DeletePressOperationCommandAsText()))
                {
                    AddParameter(cmd, "@startDate", startDate, DbType.DateTime);
                    AddParameter(cmd, "@endDate", endDate, DbType.DateTime);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Не удалось удалить операции", ex);
                        return false;
                        throw;
                    }
                }
                return true;
            }
        }

        public RestoreResult RestoreBackupJsonOperation(string filePath)
        {
            RestoreResult restoreResult = new RestoreResult();
            try
            {
                var jsonFile = File.ReadAllText(filePath);
                var operationsDes = JsonConvert.DeserializeObject<List<PressOperation>>(jsonFile);
                DbProviderFactory factory = SqlClientFactory.Instance;


                using (DbConnection conn = OpenConnectionAsinc(factory, ConnectionString))
                {
                    using (DbCommand cmd = CreateTextCommand(conn, InsertPressOperationCommandAsTextBackup()))
                    {
                        cmd.CommandTimeout = 100000;
                        try
                        {
                            foreach (var pressOperation in operationsDes)
                            {
                                AddParameter(cmd, "@Id", pressOperation.Id, DbType.Int32);
                                AddParameter(cmd, "@OperationStart", pressOperation.OperationStart, DbType.DateTime);
                                AddParameter(cmd, "@OperationStop", pressOperation.OperationStop, DbType.DateTime);
                                AddParameter(cmd, "@FactoryNumber", pressOperation.FactoryNumber, DbType.String);
                                AddParameter(cmd, "@WheelNumber", pressOperation.WheelNumber, DbType.String);
                                AddParameter(cmd, "@DiagramNumber", pressOperation.DiagramNumber, DbType.String);
                                AddParameter(cmd, "@AxisNumber", pressOperation.AxisNumber, DbType.String);
                                AddParameter(cmd, "@WheelType", pressOperation.WheelType, DbType.String);
                                AddParameter(cmd, "@Side", pressOperation.Side, DbType.String);
                                AddParameter(cmd, "@DWheel", pressOperation.DWheel, DbType.Decimal);
                                AddParameter(cmd, "@DAxis", pressOperation.DAxis, DbType.Decimal);
                                AddParameter(cmd, "@LengthStup", pressOperation.LengthStup, DbType.Int32);
                                AddParameter(cmd, "@Natiag", pressOperation.Natiag, DbType.Decimal);
                                AddParameter(cmd, "@LengthSopriazh", pressOperation.LengthSopriazh, DbType.Decimal);
                                AddParameter(cmd, "@Power100mm", pressOperation.Power100Mm, DbType.Decimal);
                                AddParameter(cmd, "@MaxPower", pressOperation.MaxPower, DbType.Decimal);
                                AddParameter(cmd, "@LengthLines", pressOperation.LengthLines, DbType.Int32);
                                object res = cmd.ExecuteScalar();

                                int id = (int?)res ?? pressOperation.Id;

                                cmd.Parameters.Clear();
                                if (res != null)
                                {
                                    restoreResult.restoreCount++;
                                    foreach (var operData in pressOperation.PressOperationData)
                                    {
                                        InsertPressOperationData(operData, conn);
                                    }
                                }
                            }
                            restoreResult.result = true;
                            return restoreResult;
                        }
                        catch (Exception ex)
                        {
                            Logger.Log.Error("Error Ошибка восстановления базы данных", ex);
                            restoreResult.result = false;
                            return restoreResult;
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                restoreResult.result = false;
                return restoreResult;
                throw;
            }
        }

        public void SaveOperations(List<PressOperation> operations, string saveFilePath)
        {
            string serialized = JsonConvert.SerializeObject(operations, Formatting.Indented);
            File.WriteAllText(saveFilePath, serialized);

        }

        public bool BackupJsonOperationAsync(DateTime startDate, DateTime endDate, string saveFilePath)

        {
            List<PressOperation> pressOperationList = new List<PressOperation>();
            BindingList<PressOperationData> operationDataList = new BindingList<PressOperationData>();
            DbProviderFactory factory = SqlClientFactory.Instance;
            string fileName = @saveFilePath + @"\архив c " + @startDate.Date.ToString("yyyy-MM-dd") + @" по " + endDate.Date.ToString("yyyy-MM-dd") + @".json";
            string query = string.Format("SET QUERY_GOVERNOR_COST_LIMIT 0 SELECT * FROM [dbo].[PressOperations] WHERE  OperationStart BETWEEN @startDate AND @endDate;") ;

            string query2 = string.Format("SET QUERY_GOVERNOR_COST_LIMIT 0 SELECT * FROM [dbo].[PressOperationData] WHERE  DateInsert BETWEEN @startDate AND @endDate;");

            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    cmd.CommandTimeout = 100000;
                    AddParameter(cmd, "@startDate", startDate, DbType.DateTime);
                    AddParameter(cmd, "@endDate", endDate, DbType.DateTime);
                    try
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["ID"]);
                                DateTime operationStart = Convert.ToDateTime(reader["OperationStart"]);
                                DateTime? operationStop = !string.IsNullOrEmpty(reader["OperationStop"].ToString()) ? (DateTime?)Convert.ToDateTime(reader["OperationStop"]) : null;
                                string factoryNumber = !string.IsNullOrEmpty(reader["FactoryNumber"].ToString()) ? Convert.ToString(reader["FactoryNumber"]) : null;
                                string wheelNumber = !string.IsNullOrEmpty(reader["WheelNumber"].ToString()) ? Convert.ToString(reader["WheelNumber"]) : null;
                                string diagramNumber = !string.IsNullOrEmpty(reader["DiagramNumber"].ToString()) ? Convert.ToString(reader["DiagramNumber"]) : null;
                                string axisNumber = !string.IsNullOrEmpty(reader["AxisNumber"].ToString()) ? Convert.ToString(reader["AxisNumber"]) : null;
                                string wheelType = !string.IsNullOrEmpty(reader["WheelType"].ToString()) ? Convert.ToString(reader["WheelType"]) : null;
                                string side = !string.IsNullOrEmpty(reader["Side"].ToString()) ? Convert.ToString(reader["Side"]) : null;
                                decimal dWheel = !string.IsNullOrEmpty(reader["DWheel"].ToString()) ? Convert.ToDecimal(reader["DWheel"]) : 0;
                                decimal dAxis = !string.IsNullOrEmpty(reader["DAxis"].ToString()) ? Convert.ToDecimal(reader["DAxis"]) : 0;
                                int lengthStup = !string.IsNullOrEmpty(reader["LengthStup"].ToString()) ? Convert.ToInt32(reader["LengthStup"]) : 0;
                                decimal natiag = !string.IsNullOrEmpty(reader["Natiag"].ToString()) ? Convert.ToDecimal(reader["Natiag"]) : 0;
                                int lengthSopriazh = !string.IsNullOrEmpty(reader["LengthSopriazh"].ToString()) ? Convert.ToInt32(reader["LengthSopriazh"]) : 0;
                                decimal power100Mm = !string.IsNullOrEmpty(reader["Power100mm"].ToString()) ? Convert.ToDecimal(reader["Power100mm"]) : 0;
                                decimal maxPower = !string.IsNullOrEmpty(reader["MaxPower"].ToString()) ? Convert.ToDecimal(reader["MaxPower"]) : 0;
                                int lengthLines = !string.IsNullOrEmpty(reader["LengthLines"].ToString()) ? Convert.ToInt32(reader["LengthLines"]) : 0;

                                PressOperation operation = new PressOperation();

                                operation.Id = id;
                                operation.OperationStart = operationStart;
                                operation.OperationStop = operationStop;
                                operation.FactoryNumber = factoryNumber;
                                operation.WheelNumber = wheelNumber;
                                operation.DiagramNumber = diagramNumber;
                                operation.AxisNumber = axisNumber;
                                operation.WheelType = wheelType;
                                operation.Side = side;
                                operation.DWheel = dWheel;
                                operation.DAxis = dAxis;
                                operation.LengthStup = lengthStup;
                                operation.LengthSopriazh = lengthSopriazh;
                                operation.MaxPower = maxPower;
                                operation.LengthLines = lengthLines;
                                pressOperationList.Add(operation);
                            }
                            reader.Close();                            
                        }
                    }
                           
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Ошибка получения данных из таблицы OPerations", ex);
                        //return false;
                        throw;
                    }
                }

                using (DbCommand cmd1 = CreateTextCommand(conn, query2))
                {
                    cmd1.CommandTimeout = 100000;
                    AddParameter(cmd1, "@startDate", startDate, DbType.DateTime);
                    AddParameter(cmd1, "@endDate", endDate, DbType.DateTime);
                    try
                    {
                        using (IDataReader reader1 = cmd1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                int id = Convert.ToInt32(reader1["ID"]);
                                decimal DispPress = !string.IsNullOrEmpty(reader1["DispPress"].ToString()) ? Convert.ToDecimal(reader1["DispPress"]) : 0;
                                int DlinaSopr = !string.IsNullOrEmpty(reader1["DlinaSopr"].ToString()) ? Convert.ToInt32(reader1["DlinaSopr"]) : 0;
                                int PressOperationId = !string.IsNullOrEmpty(reader1["PressOperationId"].ToString()) ? Convert.ToInt32(reader1["PressOperationId"]) : 0;

                                DateTime dateInsert = Convert.ToDateTime(reader1["DateInsert"]);

                                PressOperationData operData = new PressOperationData();
                                operData.Id = id;
                                operData.DispPress = DispPress;
                                operData.DlinaSopr = DlinaSopr;
                                operData.PressOperationId = PressOperationId;
                                operData.DateInsert = dateInsert;

                                operationDataList.Add(operData);
                            }
                            reader1.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Ошибка получения данных из таблицы OperationsData", ex);
                        return false;
                        throw;
                    }
                }
            }

            try
            {
                foreach (var operation in pressOperationList)
                {
                    BindingList<PressOperationData> operDataList = new BindingList<PressOperationData>(operationDataList.Where(od => od.PressOperationId == operation.Id).ToList());
                    operation.PressOperationData = operDataList;
                }

                SaveOperations(pressOperationList, fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void InsertPressOperationData(PressOperationData pressOperationData, DbConnection conn)
        {
            using (DbCommand cmd = CreateTextCommand(conn, GetInsertPressOperationDataCommandAsTextForRestore()))
            {
                AddParameter(cmd, "@Id", pressOperationData.Id, DbType.Int32);
                AddParameter(cmd, "@PressOperationID", pressOperationData.PressOperationId, DbType.Int32);
                AddParameter(cmd, "@DlinaSopr", pressOperationData.DlinaSopr, DbType.Int32);
                AddParameter(cmd, "@DispPress", pressOperationData.DispPress, DbType.Double);
                AddParameter(cmd, "@DateInsert", pressOperationData.DateInsert, DbType.DateTime);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }

    /// <summary>
    /// Разобраться с постоянным открытием соединения.
    /// </summary>
    /// <param name="pressOperationData"></param>
    public void InsertPressOperationData(PressOperationData pressOperationData)
    {
        DbProviderFactory factory = SqlClientFactory.Instance;
        using (DbConnection conn = OpenConnection(factory, ConnectionString))
        {
            using (DbCommand cmd = CreateTextCommand(conn, GetInsertPressOperationDataCommandAsText()))
            {
                AddParameter(cmd, "@PressOperationID", pressOperationData.PressOperationId, DbType.Int32);
                AddParameter(cmd, "@DlinaSopr", pressOperationData.DlinaSopr, DbType.Int32);
                AddParameter(cmd, "@DispPress", pressOperationData.DispPress, DbType.Double);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }
    }

        private string DeletePressOperationDataCommandAsText()
        {
            return "DELETE FROM [dbo].[PressOperationData] WHERE DateInsert BETWEEN @startDate AND @endDate;";
        }

        private string DeletePressOperationCommandAsText()
        {
            return "DELETE FROM [dbo].[PressOperations] WHERE OperationStart BETWEEN @startDate AND @endDate;";
        }

        private string GetInsertPressOperationDataCommandAsTextForRestore()
        {
            return "SET IDENTITY_INSERT [dbo].[PressOperationData] ON; INSERT INTO [dbo].[PressOperationData]([Id], [DispPress], [DlinaSopr], [PressOperationID], [DateInsert]) VALUES(@Id, @DispPress,  @DlinaSopr, @PressOperationID, @DateInsert); SET IDENTITY_INSERT[dbo].[PressOperationData] OFF";
        }

        private string GetInsertPressOperationDataCommandAsText()
        {
            return "INSERT INTO [dbo].[PressOperationData]([DispPress], [DlinaSopr], [PressOperationID]) VALUES(@DispPress,  @DlinaSopr, @PressOperationID);";
        }

        public void UpdatePressOperationField(int id, string fieldName, object val, DbType valType)
        {
            string query = string.Format("UPDATE [dbo].[PressOperations] SET {0} = @val WHERE ID = @id", fieldName);

            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    AddParameter(cmd, "@fieldName", fieldName, DbType.String);
                    AddParameter(cmd, "@id", id, DbType.Int32);
                    AddParameter(cmd, "@val", val, valType);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Не удалось обновить операцию", ex);
                        throw;
                    }

                }
            }
        }


        public void UpdatePressOperationFieldTotal(string fieldName, PressOperation pressOperation, object val, DbType valType)
        {
            pressOperation.LengthSopriazh = GetMaxSopr(pressOperation.Id);
            pressOperation.MaxPower = GetMaxZapress(pressOperation.Id);
            pressOperation.LengthLines = GetDlinaPramUch(pressOperation.Id);


            string query = string.Format("UPDATE [dbo].[PressOperations] SET {0} = @val, LengthSopriazh = @LengthSopriazh, Power100mm = @Power100mm, MaxPower = @MaxPower, LengthLines = @LengthLines, FactoryNumber = @FactoryNumber, WheelNumber = @WheelNumber, DiagramNumber = @DiagramNumber, AxisNumber = @AxisNumber, WheelType = @WheelType, Side = @Side, DWheel = @DWheel, DAxis = @DAxis, LengthStup = @LengthStup WHERE ID = @id", fieldName);
            
            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    AddParameter(cmd, "@fieldName", fieldName, DbType.String);
                    AddParameter(cmd, "@id", pressOperation.Id, DbType.Int32);
                    AddParameter(cmd, "@val", val, valType);
                    AddParameter(cmd, "@LengthSopriazh", pressOperation.LengthSopriazh, DbType.Int32);
                    AddParameter(cmd, "@Power100mm", pressOperation.Power100Mm, DbType.Decimal);
                    AddParameter(cmd, "@MaxPower", pressOperation.MaxPower, DbType.Decimal);
                    AddParameter(cmd, "@LengthLines", pressOperation.LengthLines, DbType.Int32);
                    AddParameter(cmd, "@FactoryNumber", pressOperation.FactoryNumber, DbType.String);
                    AddParameter(cmd, "@WheelNumber", pressOperation.WheelNumber, DbType.String);
                    AddParameter(cmd, "@DiagramNumber", pressOperation.DiagramNumber, DbType.String);
                    AddParameter(cmd, "@AxisNumber", pressOperation.AxisNumber, DbType.String);
                    AddParameter(cmd, "@Side", pressOperation.Side, DbType.String);
                    AddParameter(cmd, "@WheelType", pressOperation.WheelType, DbType.String);
                    AddParameter(cmd, "@DWheel", pressOperation.DWheel, DbType.Decimal);
                    AddParameter(cmd, "@DAxis", pressOperation.DAxis, DbType.Decimal);
                    AddParameter(cmd, "@LengthStup", pressOperation.LengthStup, DbType.Int32);

                    System.Diagnostics.Trace.WriteLine("pressOperation.Id " + pressOperation.Id);
                    //System.Diagnostics.Trace.WriteLine(" pressOperation.FactoryNumber " + pressOperation.FactoryNumber);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Не удалось обновить операцию", ex);
                        throw;
                    }

                }
            }
        }


        public int SaveOrUpdatePressOperation(PressOperation pressOperation)
        {
            Logger.Log.Info("Создаём новую операцию в БД ConnectionString = " + ConnectionString);
            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, pressOperation.IsNew ? InsertPressOperationCommandAsText() : UpdatePressOperationCommandAsText()))
                {
                    AddParameter(cmd, "@OperationStart", pressOperation.OperationStart, DbType.DateTime);
                    AddParameter(cmd, "@OperationStop", pressOperation.OperationStop, DbType.DateTime);
                    AddParameter(cmd, "@FactoryNumber", pressOperation.FactoryNumber, DbType.String);
                    AddParameter(cmd, "@WheelNumber", pressOperation.WheelNumber, DbType.String);
                    AddParameter(cmd, "@DiagramNumber", pressOperation.DiagramNumber, DbType.String);
                    AddParameter(cmd, "@AxisNumber", pressOperation.AxisNumber, DbType.String);
                    AddParameter(cmd, "@WheelType", pressOperation.WheelType, DbType.String);
                    AddParameter(cmd, "@Side", pressOperation.Side, DbType.String);
                    AddParameter(cmd, "@DWheel", pressOperation.DWheel, DbType.Decimal);
                    AddParameter(cmd, "@DAxis", pressOperation.DAxis, DbType.Decimal);
                    AddParameter(cmd, "@LengthStup", pressOperation.LengthStup, DbType.Int32);
                    AddParameter(cmd, "@Natiag", pressOperation.Natiag, DbType.Decimal);
                    AddParameter(cmd, "@LengthSopriazh", pressOperation.LengthSopriazh, DbType.Decimal);
                    AddParameter(cmd, "@Power100mm", pressOperation.Power100Mm, DbType.Decimal);
                    AddParameter(cmd, "@MaxPower", pressOperation.MaxPower, DbType.Decimal);
                    AddParameter(cmd, "@LengthLines", pressOperation.LengthLines, DbType.Int32);
                    if (!pressOperation.IsNew)
                    {
                        AddParameter(cmd, "@Id", pressOperation.Id, DbType.Int32);    
                    }
                    object res = cmd.ExecuteScalar();
                    
                    int id = (int?) res ?? pressOperation.Id;

                    cmd.Parameters.Clear();
                    
                    return id;
                }
            }
        }


        protected void AddParameter(DbCommand cmd, string paramName, object paramValue, DbType dbType)
        {
            AddParameter(cmd, paramName, 0, paramValue, dbType);
        }


        protected void AddParameter(DbCommand cmd, string paramName, int size, object paramValue, DbType dbType)
        {
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.DbType = dbType;
            if (size > 0)
            {
                param.Size = size;
            }
            param.Value = paramValue ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }

        protected string InsertPressOperationCommandAsTextBackup()
        {
            return @" SET IDENTITY_INSERT [dbo].[PressOperations] ON;

BEGIN
   IF NOT EXISTS (SELECT * FROM [ZDPress].[dbo].[PressOperations] 
                   WHERE [Id] = @Id)
   BEGIN
                    INSERT INTO [ZDPress].[dbo].[PressOperations](
                   [Id]
                  ,[OperationStart]
                  ,[OperationStop]
                  ,[FactoryNumber]
                  ,[WheelNumber]
                  ,[DiagramNumber]
                  ,[AxisNumber]
                  ,[WheelType]
                  ,[Side]
                  ,[DWheel]
                  ,[DAxis]
                  ,[LengthStup]
                  ,[Natiag]
                  ,[LengthSopriazh]
                  ,[Power100mm]
                  ,[MaxPower]
                  ,[LengthLines])
                  OUTPUT INSERTED.Id
	              VALUES(@Id,@OperationStart,@OperationStop, @FactoryNumber, @WheelNumber, @DiagramNumber, @AxisNumber, @WheelType, @Side, @DWheel, @DAxis, @LengthStup, @Natiag, @LengthSopriazh,  @Power100mm, @MaxPower, @LengthLines)
    END
END
SET IDENTITY_INSERT [dbo].[PressOperations] OFF";
        
        }

        protected string InsertPressOperationCommandAsText()
        {
            return @"INSERT INTO [ZDPress].[dbo].[PressOperations](
                   [OperationStart]
                  ,[OperationStop]
                  ,[FactoryNumber]
                  ,[WheelNumber]
                  ,[DiagramNumber]
                  ,[AxisNumber]
                  ,[WheelType]
                  ,[Side]
                  ,[DWheel]
                  ,[DAxis]
                  ,[LengthStup]
                  ,[Natiag]
                  ,[LengthSopriazh]
                  ,[Power100mm]
                  ,[MaxPower]
                  ,[LengthLines])
                  OUTPUT INSERTED.Id
	              VALUES(@OperationStart,@OperationStop, @FactoryNumber, @WheelNumber, @DiagramNumber, @AxisNumber, @WheelType, @Side, @DWheel, @DAxis, @LengthStup, @Natiag, @LengthSopriazh,  @Power100mm, @MaxPower, @LengthLines)";
        }

        protected string UpdatePressOperationCommandAsText()
        {
            return @"UPDATE [ZDPress].[dbo].[PressOperations]
                    SET [OperationStart] = @OperationStart,
                    [OperationStop] = @OperationStop,
                    [FactoryNumber] = @FactoryNumber,
                    [WheelNumber] = @WheelNumber,
                    [DiagramNumber] = @DiagramNumber,
                    [AxisNumber] = @AxisNumber,
                    [WheelType] = @WheelType,
                    [Side] = @Side,
                    [DWheel] = @DWheel,
                    [DAxis] = @DAxis,
                    [LengthStup] = @LengthStup,
                    [Natiag] = @Natiag,
                    [LengthSopriazh] = @LengthSopriazh,
                    [Power100mm] = @Power100mm,
                    [MaxPower] = @MaxPower,
                    [LengthLines] = @LengthLines
                    WHERE [ID] = @Id";
        }


        protected DbCommand CreateTextCommand(DbConnection conn, string cmdText)
        {
            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            return cmd;
        }


        protected DbConnection OpenConnection(DbProviderFactory factory, string connectionString)
        {
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception)
            {
                return conn;
                throw;
            }
            
        }

        protected DbConnection OpenConnectionAsinc(DbProviderFactory factory, string connectionString)
        {
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.OpenAsync();
            return conn;
        }

        /// <summary>
        /// Длина сопряжения. Точка на оси Х, в которой было зафиксированно максимальное усилие прессования.
        /// </summary>
        /// <param name="operationId">operationId</param>
        /// <returns>int</returns>
        public int GetMaxSopr(int operationId)
        {
            const string query = "SELECT TOP 1 [DlinaSopr] FROM [dbo].[PressOperationData] pd WHERE pd.PressOperationId = @operationId ORDER BY [DispPress] DESC";
                                  
            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    AddParameter(cmd, "@operationId", operationId, DbType.Int32);
                    try
                    {
                        object res = cmd.ExecuteScalar();
                        return res != DBNull.Value ? Convert.ToInt32(res) : 0;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Error GetMaxSopr", ex);
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// Максимальное усилие запрессовки. Максимальное усилие зафиксированное во время прессования.
        /// </summary>
        /// <param name="operationId">operationId</param>
        /// <returns>int</returns>
        public decimal GetMaxZapress(int operationId)
        {
            const string query = "SELECT MAX([DispPress]) FROM [dbo].[PressOperationData] pd where pd.[PressOperationId] = @operationId";

            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    AddParameter(cmd, "@operationId", operationId, DbType.Int32);
                    try
                    {
                        object res = cmd.ExecuteScalar();
                        return res != DBNull.Value ? Convert.ToDecimal(res) : 0.0M;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Error GetMaxZapress", ex);
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// Длина прямых участков. Сумма длин всех участков, на которых давление оставалось неизменным. Значение вычисляется автоматически.
        /// </summary>
        /// <param name="operationId">operationId</param>
        /// <returns>int</returns>
        public int GetDlinaPramUch(int operationId)
        {
            string query = @"

DECLARE  @operationId INT = @operation_id

DECLARE @data_source TABLE
(
[ID] INT, 
[DispPress] DECIMAL(10,2),
[DlinaSopr] INT
)

INSERT INTO @data_source([ID], [DispPress], [DlinaSopr])
SELECT [ID], [DispPress], [DlinaSopr] FROM [dbo].[PressOperationData] where [PressOperationId] = @operationId

DECLARE @t TABLE
(
id INT,
disp_press DECIMAL(10,2),
dlina_sopr INT 
)

INSERT INTO @t(id, disp_press, dlina_sopr)
SELECT [ID], [DispPress], [DlinaSopr] FROM @data_source
WHERE
id < (SELECT MIN([ID]) FROM @data_source WHERE [DispPress] = (SELECT MAX([DispPress]) FROM @data_source))

SELECT 
 SUM(CASE WHEN currentRow.dlina_sopr <> nextRow.dlina_sopr  AND currentRow.disp_press = nextRow.disp_press THEN 1 ELSE 0 END) 
  FROM @t currentRow
   INNER JOIN @t nextRow ON(currentRow.Id + 1 = nextRow.Id)
"; 

            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    AddParameter(cmd, "@operation_id", operationId, DbType.Int32);
                    try
                    {
                        object res = cmd.ExecuteScalar();
                        return res != DBNull.Value ? Convert.ToInt32(res) : 0;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Error DlinaPramUch", ex);
                        throw;
                    }
                }
            }
        }


        public List<PressOperation> PageOperation(int pageSize, int pageNumber)
        {
            string query =
             " SELECT " +
                  " [ID] " +
                  ",[OperationStart] " +
                  ",[OperationStop] " +
                  ",[WheelNumber] " +
                  ",[FactoryNumber] " +
                  ",[DiagramNumber] " +
                  ",[AxisNumber] " +
                  ",[WheelType] " +
                  ",[Side] " +
                  ",[DWheel] " +
                  ",[DAxis] " +
                  ",[LengthStup] " +
                  ",[Natiag] " +
                  ",[LengthSopriazh] " +
                  ",[Power100mm] " +
                  ",[MaxPower] " +
                  ",[LengthLines] " +
                  ", [TotalRows] " +
            "FROM " +
            "(SELECT ROW_NUMBER() OVER (ORDER BY [ID] DESC) AS RowNum, " +
                  " [ID] " +
                  ",[OperationStart] " +
                  ",[OperationStop] " +
                  ",[WheelNumber] " +
                  ",[FactoryNumber] " +
                  ",[DiagramNumber] " +
                  ",[AxisNumber] " +
                  ",[WheelType] " +
                  ",[Side] " +
                  ",[DWheel] " +
                  ",[DAxis] " +
                  ",[LengthStup] " +
                  ",[Natiag] " +
                  ",[LengthSopriazh] " +
                  ",[Power100mm] " +
                  ",[MaxPower] " +
                  ",[LengthLines] " +
                  " ,COUNT(*) OVER() AS TotalRows " +
            " from [dbo].[PressOperations] " +
            ") AS PressOperations " +
            "WHERE RowNum BETWEEN (((@pageNumber - 1) * @pageSize )+ 1) AND (@pageNumber * @pageSize) ";

            List<PressOperation> operations = new List<PressOperation>();
            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    cmd.CommandTimeout = 0;
                    AddParameter(cmd, "@pageSize", pageSize, DbType.Int32);
                    AddParameter(cmd, "@pageNumber", pageNumber, DbType.Int32);
                    try
                    {
                        DbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            DateTime operationStart = Convert.ToDateTime(reader["OperationStart"]);
                            DateTime? operationStop = !string.IsNullOrEmpty(reader["OperationStop"].ToString()) ? (DateTime?)Convert.ToDateTime(reader["OperationStop"]) : null;
                            string factoryNumber = !string.IsNullOrEmpty(reader["FactoryNumber"].ToString()) ? Convert.ToString(reader["FactoryNumber"]) : null;
                            string wheelNumber = !string.IsNullOrEmpty(reader["WheelNumber"].ToString()) ? Convert.ToString(reader["WheelNumber"]) : null;
                            string diagramNumber = !string.IsNullOrEmpty(reader["DiagramNumber"].ToString()) ? Convert.ToString(reader["DiagramNumber"]) : null;
                            string axisNumber = !string.IsNullOrEmpty(reader["AxisNumber"].ToString()) ? Convert.ToString(reader["AxisNumber"]) : null;
                            string wheelType = !string.IsNullOrEmpty(reader["WheelType"].ToString()) ? Convert.ToString(reader["WheelType"]) : null;
                            string side = !string.IsNullOrEmpty(reader["Side"].ToString()) ? Convert.ToString(reader["Side"]) : null;
                            decimal dWheel = !string.IsNullOrEmpty(reader["DWheel"].ToString()) ? Convert.ToDecimal(reader["DWheel"]) : 0;
                            decimal dAxis = !string.IsNullOrEmpty(reader["DAxis"].ToString()) ? Convert.ToDecimal(reader["DAxis"]) : 0;
                            int lengthStup = !string.IsNullOrEmpty(reader["LengthStup"].ToString()) ? Convert.ToInt32(reader["LengthStup"]) : 0;
                            decimal natiag = !string.IsNullOrEmpty(reader["Natiag"].ToString()) ? Convert.ToDecimal(reader["Natiag"]) : 0;
                            int lengthSopriazh = !string.IsNullOrEmpty(reader["LengthSopriazh"].ToString()) ? Convert.ToInt32(reader["LengthSopriazh"]) : 0;
                            decimal power100Mm = !string.IsNullOrEmpty(reader["Power100mm"].ToString()) ? Convert.ToDecimal(reader["Power100mm"]) : 0;
                            decimal maxPower = !string.IsNullOrEmpty(reader["MaxPower"].ToString()) ? Convert.ToDecimal(reader["MaxPower"]) : 0;
                            int lengthLines = !string.IsNullOrEmpty(reader["LengthLines"].ToString()) ? Convert.ToInt32(reader["LengthLines"]) : 0;//GetDlinaPramUch(id);
                            int totalRows = !string.IsNullOrEmpty(reader["TotalRows"].ToString()) ? Convert.ToInt32(reader["TotalRows"]) : 0;

                            PressOperation operation = new PressOperation();

                            operation.Id = id;
                            operation.OperationStart = operationStart;
                            operation.OperationStop = operationStop;
                            operation.FactoryNumber = factoryNumber;
                            operation.WheelNumber = wheelNumber;
                            operation.DiagramNumber = diagramNumber;
                            operation.AxisNumber = axisNumber;
                            operation.WheelType = wheelType;
                            operation.Side = side;
                            operation.DWheel = dWheel;
                            operation.DAxis = dAxis;
                            operation.LengthStup = lengthStup;
                            operation.LengthSopriazh = lengthSopriazh;
                            operation.MaxPower = maxPower;
                            operation.LengthLines = lengthLines;
                            operation.TotalRows = totalRows;

                            operations.Add(operation);
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Error PageOperation", ex);
                        throw;
                    }
                }
            }
            return operations;
        }


        public Tuple<List<PressOperationData>, int> GetOperationData(int pressOperationId, int lastPressOperationDataId)
        {
            string query =
                "SET QUERY_GOVERNOR_COST_LIMIT 0 " +
"SELECT " +
"(select TOP 1 id from dbo.pressOperations where id > CASE  WHEN @pressOperationId = 0 THEN (SELECT MAX(ID) FROM [dbo].[PressOperations]) ELSE  @pressOperationId END) as NextId, " +
"(SELECT COUNT(1) FROM [dbo].[PressOperationData]  WHERE [PressOperationId] = CASE  WHEN @pressOperationId = 0 THEN (SELECT MAX(ID) FROM [dbo].[PressOperations]) ELSE  @pressOperationId END AND [id] > @lastId) AS total," +
 "[ID], [DispPress], [DlinaSopr], [PressOperationId], [DateInsert] FROM [dbo].[PressOperationData]  WHERE [PressOperationId] = CASE  WHEN @pressOperationId = 0 THEN (SELECT MAX(ID) FROM [dbo].[PressOperations]) ELSE  @pressOperationId END AND [id] > @lastId";


            List<PressOperationData> data = new List<PressOperationData>();

            DbProviderFactory factory = SqlClientFactory.Instance;

            int total = 0;

            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    AddParameter(cmd, "@pressOperationId", pressOperationId, DbType.Int32);
                    AddParameter(cmd, "@lastId", lastPressOperationDataId, DbType.Int32);
                    try
                    {
                        DbDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);

                            decimal dispPress = Convert.ToDecimal(reader["DispPress"]);

                            int dlinaSopr = Convert.ToInt32(reader["DlinaSopr"]);

                            int pressOperation_Id = Convert.ToInt32(reader["PressOperationId"]);
                            
                            total = Convert.ToInt32(reader["total"]);
                            
                            DateTime dateInsert = Convert.ToDateTime(reader["DateInsert"]);

                            PressOperationData item = new PressOperationData
                            {
                                Id = id,
                                PressOperationId = pressOperation_Id,
                                DispPress = dispPress,
                                DlinaSopr = dlinaSopr,
                                DateInsert = dateInsert
                            };


                            data.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Error PageOperation", ex);
                        throw;
                    }
                }
            }

            return Tuple.Create<List<PressOperationData>, int>(data, total);
        }


        public void LoadPressOperationData(PressOperation pressOperation)
        {
            int operationId = pressOperation.Id;
            
            string query = "SELECT  [ID], [DispPress], [DlinaSopr], [PressOperationId], [DateInsert] FROM [ZDPress].[dbo].[PressOperationData]  WHERE [PressOperationId] = @pressOperationId";
            List<PressOperationData> data = new List<PressOperationData>();
            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    cmd.CommandTimeout = 0;
                    AddParameter(cmd, "@pressOperationId", operationId, DbType.Int32);
                    try
                    {
                        DbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            decimal dispPress = Convert.ToDecimal(reader["DispPress"]);
                            int dlinaSopr = Convert.ToInt32(reader["DlinaSopr"]);
                            int pressOperationId = Convert.ToInt32(reader["PressOperationId"]);
                            DateTime dateInsert = Convert.ToDateTime(reader["DateInsert"]);
                            PressOperationData item = new PressOperationData
                            {
                                Id = id,
                                PressOperationId = pressOperationId,
                                DispPress = dispPress,
                                DlinaSopr = dlinaSopr,
                                DateInsert = dateInsert
                            };


                            data.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Error PageOperation", ex);
                        throw;
                    }
                }
            }
            pressOperation.PressOperationData = new BindingList<PressOperationData>(data);
        }

        public PressOperation GetOperationForPassport(string axisNum, string sideParam)
        {
            const string query = "SELECT TOP 1 " +
                                 " [ID] " +
                                 ",[OperationStart] " +
                                 ",[OperationStop] " +
                                 ",[WheelNumber] " +
                                 ",[FactoryNumber] " +
                                 ",[DiagramNumber] " +
                                 ",[AxisNumber] " +
                                 ",[WheelType] " +
                                 ",[Side] " +
                                 ",[DWheel] " +
                                 ",[DAxis] " +
                                 ",[LengthStup] " +
                                 ",[Natiag] " +
                                 ",[LengthSopriazh] " +
                                 ",[Power100mm] " +
                                 ",[MaxPower] " +
                                 ",[LengthLines] " +
                                 " FROM [dbo].[PressOperations] " +
                                 " WHERE UPPER([AxisNumber]) = UPPER(@axisNumber) AND UPPER([Side]) <> UPPER(@side)  " +
                                 " ORDER BY [OperationStart] DESC ";

            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection conn = OpenConnection(factory, ConnectionString))
            {
                using (DbCommand cmd = CreateTextCommand(conn, query))
                {
                    AddParameter(cmd, "@axisNumber", axisNum, DbType.String);
                    AddParameter(cmd, "@side", sideParam, DbType.String);
                    try
                    {
                        PressOperation operation = new PressOperation();
                        DbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            DateTime operationStart = Convert.ToDateTime(reader["OperationStart"]);
                            DateTime? operationStop = !string.IsNullOrEmpty(reader["OperationStop"].ToString()) ? (DateTime?)Convert.ToDateTime(reader["OperationStop"]) : null;
                            string factoryNumber = !string.IsNullOrEmpty(reader["FactoryNumber"].ToString()) ? Convert.ToString(reader["FactoryNumber"]) : null;
                            string wheelNumber = !string.IsNullOrEmpty(reader["WheelNumber"].ToString()) ? Convert.ToString(reader["WheelNumber"]) : null;
                            string diagramNumber = !string.IsNullOrEmpty(reader["DiagramNumber"].ToString()) ? Convert.ToString(reader["DiagramNumber"]) : null;
                            string axisNumber = !string.IsNullOrEmpty(reader["AxisNumber"].ToString()) ? Convert.ToString(reader["AxisNumber"]) : null;
                            string wheelType = !string.IsNullOrEmpty(reader["WheelType"].ToString()) ? Convert.ToString(reader["WheelType"]) : null;
                            string side = !string.IsNullOrEmpty(reader["Side"].ToString()) ? Convert.ToString(reader["Side"]) : null;
                            decimal dWheel = !string.IsNullOrEmpty(reader["DWheel"].ToString()) ? Convert.ToDecimal(reader["DWheel"]) : 0;
                            decimal dAxis = !string.IsNullOrEmpty(reader["DAxis"].ToString()) ? Convert.ToDecimal(reader["DAxis"]) : 0;
                            int lengthStup = !string.IsNullOrEmpty(reader["LengthStup"].ToString()) ? Convert.ToInt32(reader["LengthStup"]) : 0;
                            decimal natiag = !string.IsNullOrEmpty(reader["Natiag"].ToString()) ? Convert.ToDecimal(reader["Natiag"]) : 0;
                            int lengthSopriazh = !string.IsNullOrEmpty(reader["LengthSopriazh"].ToString()) ? Convert.ToInt32(reader["LengthSopriazh"]) : 0;
                            decimal power100Mm = !string.IsNullOrEmpty(reader["Power100mm"].ToString()) ? Convert.ToDecimal(reader["Power100mm"]) : 0;
                            decimal maxPower = !string.IsNullOrEmpty(reader["MaxPower"].ToString()) ? Convert.ToDecimal(reader["MaxPower"]) : 0;
                            int lengthLines = !string.IsNullOrEmpty(reader["LengthLines"].ToString()) ? Convert.ToInt32(reader["LengthLines"]) : 0;

                            
                            operation.Id = id;
                            operation.OperationStart = operationStart;
                            operation.OperationStop = operationStop;
                            operation.FactoryNumber = factoryNumber;
                            operation.WheelNumber = wheelNumber;
                            operation.DiagramNumber = diagramNumber;
                            operation.AxisNumber = axisNumber;
                            operation.WheelType = wheelType;
                            operation.LengthSopriazh = lengthSopriazh;
                            operation.Side = side;
                            operation.DWheel = dWheel;
                            operation.DAxis = dAxis;
                            operation.LengthStup = lengthStup;
                            operation.MaxPower = maxPower;
                            operation.LengthLines = lengthLines;
                        }

                        return operation;

                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("Error GetOperationForPassport", ex);
                        throw;
                    }
                }
            }
        }
    }
}
