using ProtoBuf;
using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace TTS
{
	[ProtoContract]
	public class ProtoTable
	{
		[ProtoMember(1)]
		private byte[] SerializedTable;
		public DataTable Table
		{
			get;
			set;
		}
		public ProtoTable()
		{
		}
		public ProtoTable(DataTable T)
		{
			this.Table = T;
		}
		public ProtoTable(byte[] bytes)
		{
			this.SerializedTable = bytes;
			this.ProtoAfterDeserialization();
		}
		[ProtoBeforeSerialization]
		private void ProtoBeforeSerialization()
		{
			this.Table.RemotingFormat = SerializationFormat.Binary;
			using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
			{
				new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(memoryStream, this.Table);
				this.SerializedTable = memoryStream.ToArray();
			}
		}
		[ProtoAfterDeserialization]
		private void ProtoAfterDeserialization()
		{
			if (this.SerializedTable == null || this.SerializedTable.Length == 0)
			{
				this.Table = null;
				return;
			}
			this.Table = (DataTable)new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize(new System.IO.MemoryStream(this.SerializedTable));
		}
		public byte[] GetProtoBytes()
		{
			this.ProtoBeforeSerialization();
			return this.SerializedTable;
		}
	}
}
