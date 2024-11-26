using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace CarAuctionApi.Service.Helpers;

public static class CodeHelper
{
    public static string EncodeToken(this string token)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(token);
        return WebEncoders.Base64UrlEncode(bytes);
    }

    public static string DecodeToken(this string token)
    {
        var decodedToken = WebEncoders.Base64UrlDecode(token);
        return Encoding.UTF8.GetString(decodedToken);
    }
}