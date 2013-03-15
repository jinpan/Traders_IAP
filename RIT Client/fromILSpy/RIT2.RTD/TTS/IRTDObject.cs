using System;
using System.Collections.Generic;
using System.ServiceModel;
namespace TTS
{
	[ServiceContract]
	public interface IRTDObject
	{
		[OperationContract]
		Dictionary<string, object> GetData(string[] topics);
		[OperationContract]
		int GetRefreshInterval();
	}
}
