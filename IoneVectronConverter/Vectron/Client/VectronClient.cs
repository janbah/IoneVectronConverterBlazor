using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.Mapper;
using IoneVectronConverter.Vectron.MasterData.Models;
using IoneVectronConverter.Vectron.Models;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace IoneVectronConverter.Vectron.Client;

public class VectronClient : IVectronClient
{
    public const string SendDelimiter = "<EOF>";
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
        return await Task.Run(() => SendTest(receipt));

    }

    public IEnumerable<ItemCategory> GetCategories()
    {
        throw new NotImplementedException();
    }

    private async Task<VPosResponse> SendTest(Receipt receipt)
    {
        Socket socket = GetVPosSocket();
        SendBase64String(ref socket, receipt.ToJson());
        
        var bytes = GetResponse(socket);

        var text = Encoding.UTF8.GetString(bytes);
        
        Console.WriteLine(text);

        //string jsonText = Encoding.UTF8.GetString(Convert.FromBase64String(text));
        //string jsonText = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(GetResponse(socket))));

        //socket.Close();

        var response = JsonConvert.DeserializeObject<VPosResponse>(text);

        return response;

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
        //Todo: set buffersize
        var buffer = new byte[2048];
        List<byte> responseBytes = new List<byte>();
        
        
        var bytesRead  = socket.ReceiveAsync(buffer).Result; 
        responseBytes.AddRange(buffer.Take(bytesRead));

       // bytesRead = socket.Receive(buffer);
        
        //responseBytes.AddRange(buffer.Take(bytesRead));
        return responseBytes.ToArray();
    }

    public MasterDataResponse GetMasterData()
    {
        byte[] response = new byte[2048];
        byte[] responseBytes = new byte[2048];
        
        Socket socket = GetVPosSocket();
        SendBase64String(ref socket, "{\"GetMasterData\":1}");

        response = GetResponse(socket);
        var responseText = Encoding.UTF8.GetString(response);
        responseBytes = Convert.FromBase64String(responseText);
        var jsonResponseText = Encoding.UTF8.GetString(responseBytes);

        socket.Close();

        var error = "";

        
        
        return JsonConvert.DeserializeObject<MasterDataResponse>(jsonResponseText);

        
    }
    
    static Socket GetVPosSocket()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(new IPEndPoint(ipAddress, port));
        Console.WriteLine("Socket connected to {0}");
        sendSecret(socket);
        return socket;
    }

    private static void sendSecret(Socket socket)
    {
        // byte[] bytes = new byte[6];
        // socket.Receive(bytes);
        // string salt = Encoding.UTF8.GetString(bytes);
        // var md5 = MD5.Create();
        // socket.Send(md5.ComputeHash(Encoding.UTF8.GetBytes(secret + salt)));
    }
}