https://www.cnblogs.com/linezero/p/grpc.html

当前目录为 D:\code\ipc-integration\honghaier\merlincomm  调用如下命令行
.\..\packages\Grpc.Tools.2.36.4\tools\windows_x64\protoc.exe -IProtos --csharp_out ProtosBuild  honghaier.proto --grpc_out ProtosBuild --plugin=protoc-gen-grpc=.\..\packages\Grpc.Tools.2.36.4\tools\windows_x64\grpc_csharp_plugin.exe

$(ProjectDir)..\packages\Grpc.Tools.2.36.4\tools\windows_x64\protoc.exe -I$(ProjectDir)Protos --csharp_out $(ProjectDir)ProtosBuild  honghaier.proto --grpc_out $(ProjectDir)ProtosBuild --plugin=protoc-gen-grpc=$(ProjectDir)..\packages\Grpc.Tools.2.36.4\tools\windows_x64\grpc_csharp_plugin.exe

$(ProjectDir)..\packages\Grpc.Tools.2.36.4\tools\windows_x64\protoc.exe -I$(ProjectDir)Protos --csharp_out $(ProjectDir)ProtosBuild  wukong.proto --grpc_out $(ProjectDir)ProtosBuild --plugin=protoc-gen-grpc=$(ProjectDir)..\packages\Grpc.Tools.2.36.4\tools\windows_x64\grpc_csharp_plugin.exe

$(ProjectDir)..\packages\Grpc.Tools.2.36.4\tools\windows_x64\protoc.exe -I$(ProjectDir)Protos --csharp_out $(ProjectDir)ProtosBuild  *.proto --grpc_out $(ProjectDir)ProtosBuild --plugin=protoc-gen-grpc=$(ProjectDir)..\packages\Grpc.Tools.2.36.4\tools\windows_x64\grpc_csharp_plugin.exe

