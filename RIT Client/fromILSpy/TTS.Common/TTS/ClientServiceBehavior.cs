using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
namespace TTS
{
	public class ClientServiceBehavior : IServiceBehavior
	{
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			using (System.Collections.Generic.IEnumerator<ChannelDispatcherBase> enumerator = serviceHostBase.ChannelDispatchers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ChannelDispatcher channelDispatcher = (ChannelDispatcher)enumerator.Current;
					channelDispatcher.ErrorHandlers.Add(new ClientServiceErrorHandler());
				}
			}
		}
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}
