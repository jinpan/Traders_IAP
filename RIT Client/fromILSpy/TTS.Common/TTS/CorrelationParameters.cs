using ProtoBuf;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class CorrelationParameters
	{
		[ProtoMember(1)]
		private byte[] SerializedCovariance;
		public decimal[,] Covariance
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public decimal[] StandardDeviation
		{
			get;
			set;
		}
		[ProtoBeforeSerialization]
		private void ProtoBeforeSerialization()
		{
			using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
			{
				new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(memoryStream, this.Covariance);
				this.SerializedCovariance = memoryStream.ToArray();
			}
		}
		[ProtoAfterDeserialization]
		private void ProtoAfterDeserialization()
		{
			if (this.SerializedCovariance == null || this.SerializedCovariance.Length == 0)
			{
				this.Covariance = null;
				return;
			}
			this.Covariance = (decimal[,])new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize(new System.IO.MemoryStream(this.SerializedCovariance));
		}
	}
}
