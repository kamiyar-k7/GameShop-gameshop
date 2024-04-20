
using System.Security.Cryptography;
using System.Text;


namespace Application.Helpers;

public class PassHelper
{
	public static string EncodePasswordMd5(string Pass)
	{
		byte[] originalBytes;
		byte[] encodedBytes;
		MD5 md5;


		md5 = new MD5CryptoServiceProvider();
		originalBytes = ASCIIEncoding.Default.GetBytes(Pass);
		encodedBytes = md5.ComputeHash(originalBytes);
		return BitConverter.ToString(encodedBytes);
	}
}
