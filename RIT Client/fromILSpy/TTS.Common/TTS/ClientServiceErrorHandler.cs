using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
namespace TTS
{
	public class ClientServiceErrorHandler : IErrorHandler
	{
		public bool HandleError(System.Exception error)
		{
			return true;
		}
		public void ProvideFault(System.Exception error, MessageVersion version, ref Message fault)
		{
			fault = Message.CreateMessage(version, new FaultException<string>(error.Message, new FaultReason(error.Message)).CreateMessageFault(), "");
		}
	}
}
