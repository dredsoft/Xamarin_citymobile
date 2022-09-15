using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CityApp.Infrastructure.ApiManager;
using CityApp.Infrastructure.ConnectivityManager;
using CityApp.Models.Models.Base;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Utilities.Logging;
using Microsoft.AppCenter.Crashes;

namespace CityApp.Services.AmazonService
{
    public class AWSS3Service : IAWSS3Service
    {
		#region Constructors

	    public AWSS3Service()
	    {

	    }

		#endregion

		#region Properties

		public static ILogger Logger => LogManager.GetLog();

		#endregion

		#region Public Methods

		public string ReadFileUrl(string fileName)
	    {
		    Logger.Trace($"{nameof(fileName)}: {fileName}");
		    string url = string.Empty;

		    using (var client = new AmazonS3Client(CommonConstants.AWSAccessKeyID, CommonConstants.AWSSecretKey, Amazon.RegionEndpoint.USWest2))
		    {
			    GetPreSignedUrlRequest request = new GetPreSignedUrlRequest()
			    {
				    BucketName = CommonConstants.AmazonS3Bucket,
				    Key = fileName,
				    Expires = DateTime.Now.Add(TimeSpan.FromDays(7))
			    };

			    url = client.GetPreSignedURL(request);
		    }

		    Logger.Debug($"Result: {url}");
		    return url;
	    }

	    public async Task<bool> UploadFile(Stream file, string fileName,bool isPublic )
	    {
			Logger.Trace();

			try
			{
				using (var client = new AmazonS3Client(CommonConstants.AWSAccessKeyID, CommonConstants.AWSSecretKey, Amazon.RegionEndpoint.USWest2))
				{
						PutObjectRequest request = new PutObjectRequest()
						{
							InputStream = file,
							BucketName = CommonConstants.AmazonS3Bucket,
							Key = fileName
						};

						if (isPublic)
						{
							request.CannedACL = S3CannedACL.PublicRead;
						}

						var response = await client.PutObjectAsync(request);
				}
			}
			catch (AmazonS3Exception amazonS3Exception)
			{
				if (amazonS3Exception.ErrorCode != null &&
				    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
				     amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
				{
					return false;
				}

				Crashes.TrackError(amazonS3Exception);
			}

		    return true;
		}

		#endregion
	}
}
