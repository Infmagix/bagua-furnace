using Grpc.Core;
using Merlincomm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace merlincomm
{
    class gRPCImpl : gRPCHonghaier.gRPCHonghaierBase
    {
        // 实现SayHello方法
        public override Task<ReadReply> Read(ReadRequest request, ServerCallContext context)
        {
            ReadReply readReply = new ReadReply { SrcName = "Hello " };
            return Task.FromResult(readReply);
        }
    }

    class Test
    {
        const int Port = 9007;
        public void InitGRPCServer()
        {
            Server server = new Server
            {
                Services = { gRPCHonghaier.BindService(new gRPCImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("gRPC server listening on port " + Port);
            Console.WriteLine("任意键退出...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
