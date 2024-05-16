using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace honghaier.Utility
{
    class InfluxDBHelper : IDbProvider
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("InfluxDBHelper");
        private readonly string token = Const.InfluxDBToken;
        private readonly string bucket = Const.InfluxDBBucket;
        private readonly string org = Const.InfluxDBOrg;
        private readonly string serverIP = Const.InfluxServerIP;
        private readonly int port = Const.InfluxServerPort;

        private InfluxDBClient client;
        private readonly WriteOptions writeOptions;

        public InfluxDBHelper()
        {
            //client = InfluxDBClientFactory.Create($"http://{serverIP}:{port}", token.ToCharArray());
            client = new InfluxDBClient($"http://{serverIP}:{port}", token);
            client.SetLogLevel(InfluxDB.Client.Core.LogLevel.Body);

            writeOptions = WriteOptions.CreateNew()
                .BatchSize(5000)
                .FlushInterval(1000)
                .MaxRetries(3)
                .JitterInterval(1000)
                .RetryInterval(5000)
                .Build();

            // Task.Run(() => { WriteTest(); }).Start();
            Task.Run(() => { QueryTest(); });
        }

        public void WriteDict(object obj)
        {
            try
            {
                using (var writeApi = client.GetWriteApi(writeOptions))
                {
                    var keyValues = obj as Dictionary<string, object>;

                    foreach (var key in keyValues.Keys)
                    {
                        if (keyValues[key] == null)
                        {
                            continue;
                        }

                        var keyParts = key.Split('.');
                        if (keyParts.Length != 2)
                        {
                            throw new Exception("Tag 标签不正确，需要的形式为 \"Tag.Key\"，比如 \"wukong.actual_rpm_speed\"" + "\r\n" + key);
                        }

                        //
                        // Write by Point
                        //
                        var point = PointData.Measurement(keyParts[0])
                            .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                        switch (keyValues[key].GetType().Name)
                        {
                            case "Double":
                                //var pointSettings = new PointSettings();
                                point = point.Field(keyParts[1], (double)keyValues[key]);
                                break;
                            case "Int32":
                                point = point.Field(keyParts[1], (int)keyValues[key]);
                                break;
                            case "Byte":
                                point = point.Field(keyParts[1], (byte)keyValues[key]);
                                break;
                            case "Char":
                                point = point.Field(keyParts[1], (char)keyValues[key]);
                                break;
                            case "String":
                                point = point.Field(keyParts[1], (string)keyValues[key]);
                                break;
                            default:
                                break;
                        }

                        writeApi.WritePoint(point, bucket, org);
                    }
                    writeApi.Flush();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Exception(log, ex);
                //MessageBox.Show($"void WriteDict(object obj) in InfluxDBHelper.cs line ??: {ex.Message}");
            }
        }

        public void Reset()
        {
            client?.Dispose();

            //client = InfluxDBClientFactory.Create($"http://{serverIP}:{port}", token.ToCharArray());
            client = new InfluxDBClient($"http://{serverIP}:{port}", token);
            client.SetLogLevel(InfluxDB.Client.Core.LogLevel.Body);
        }

        public void WriteTest()
        {
            try
            {
                while (true)
                {
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    string str = "";
                    for (int i = 0; i < 10; i++)
                    {
                        char ch = (char)('a' + ((int)(rand.NextDouble() * 26)));
                        str += ch;
                    }

                    var point = PointData
                        .Measurement("test")
                        .Field("double", rand.NextDouble() * 100)
                        .Field("int", +rand.Next() % 100)
                        .Field("string", str)
                        .Tag("tag", "test")
                        .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                    using (var writeApi = client.GetWriteApi(writeOptions))
                    {
                        writeApi.WritePoint(point, bucket, org);
                        writeApi.Flush();
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Exception(log, ex);
                //MessageBox.Show($"void WriteTest() in InfluxDBHelper.cs line ??: {ex.Message}");
            }
        }

        public async void QueryTest()
        {
            try
            {
                //while (true)
                {
                    // Check bucket existance
                    var api = client.GetBucketsApi();
                    var buck = await api.FindBucketByNameAsync(bucket);
                    if (buck == null)
                    {
                        // Find ID of Organization with specified name (PermissionAPI requires ID of Organization).
                        var orgId = (await client.GetOrganizationsApi().FindOrganizationsAsync(org: org)).First().Id;
                        //var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, 3600);
                        buck = await client.GetBucketsApi().CreateBucketAsync(bucket, orgId);
                    }


                    string[] filedNames = { "double", "int", "string" };
                    var query = $"from(bucket: \"{bucket}\") |> range(start: -1h)" +
                        $"|> filter(fn: (r) => (r._field == \"{filedNames[0]}\" or r._field == \"{filedNames[1]}\" or r._field == \"{filedNames[2]}\"))";

                    var tables = await client.GetQueryApi().QueryAsync(query, org);

                    //foreach (var table in tables)
                    //{
                    //    if (table.Records.Count > 0)
                    //    {
                    //        foreach (var value in table.Records[0].Values)
                    //        {
                    //            if (value.Key == "_field" && value.Value.ToString() == "log")
                    //            {

                    //            }
                    //        }
                    //    }
                    //}

                    //Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Exception(log, ex);
                //MessageBox.Show($"void QueryTest() in InfluxDBHelper.cs line ??: {ex.Message}");
            }
        }
    }
}
