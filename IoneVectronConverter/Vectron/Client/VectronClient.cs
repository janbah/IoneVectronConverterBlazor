using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.Mapper;
using IoneVectronConverter.Vectron.MasterData;
using IoneVectronConverter.Vectron.Models;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace IoneVectronConverter.Vectron.Client;

public class VectronClient : IVectronClient
{
    public const string SendDelimiter = "\0";
    const int port = 1050;
    const string secret = "*6H@6TF7bDrCbU-V1.0";

    private readonly ReceiptMapper _mapper;
    
    public VectronClient(ReceiptMapper mapper)
    {
        _mapper = mapper;
    }

    //Todo: implement client
    public async Task<VPosResponse> Send(OrderListData order)
    {
        var receipt = _mapper.Map(order);
        var result = await SendReceipt(receipt);
        return result;
    }
    
    public async Task<VPosResponse> SendReceipt(Receipt receipt)
    {
        return await Task.Run(() => waitForAnswer(receipt));
    }

    private static VPosResponse? waitForAnswer(Receipt receipt)
    {
        Socket socket = GetVPosSocket();
        string jsonText = "";
        
        SendBase64String(ref socket, receipt.ToJson());
        try
        {
            jsonText = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(GetResponse(socket))));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        socket.Close();

        return JsonConvert.DeserializeObject<VPosResponse>(jsonText);
    }

    static void SendBase64String(ref Socket socket, string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var base64String = Convert.ToBase64String(bytes); 
        var message = base64String  + SendDelimiter;
        var buffer = Encoding.UTF8.GetBytes(message);
        
        socket.Send(buffer);
    }
    
    static byte[] GetResponse(Socket socket)
    {
        var buffer = new byte[256];
        int bytesRead;
        List<byte> responseBytes = new List<byte>();
        
        // while ((bytesRead = socket.Receive(buffer)) > 0)
        // {
        //     responseBytes.AddRange(buffer.Take(bytesRead));
        // }

        bytesRead = socket.Receive(buffer);
        
        responseBytes.AddRange(buffer.Take(bytesRead));
        return responseBytes.ToArray();
    }

    public MasterDataResponse GetMasterData()
    {
        throw new NotImplementedException();
    }
    
    static Socket GetVPosSocket()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 1050);
        
        //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(new IPEndPoint(ipAddress, port));
        Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint);
        //byte[] bytes = new byte[6];
        //socket.Receive(bytes);
        //string salt = Encoding.UTF8.GetString(bytes);
        var md5 = MD5.Create();
        //socket.Send(md5.ComputeHash(Encoding.UTF8.GetBytes(secret + salt)));
        //socket.Send(md5.ComputeHash(Encoding.UTF8.GetBytes(secret + salt)));
        return socket;
    }
}