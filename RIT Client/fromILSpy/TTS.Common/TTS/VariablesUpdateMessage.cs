using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class VariablesUpdateMessage
	{
		[ProtoMember(1)]
		public GeneralParametersUpdate General
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public SecurityParametersUpdate Security
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public AssetParametersUpdate Asset
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public RiskTypeParametersUpdate TradingLimit
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public bool IsRTDEnabled
		{
			get;
			set;
		}
		public VariablesUpdateMessage()
		{
		}
		public VariablesUpdateMessage(GeneralParametersUpdate general, SecurityParametersUpdate security, AssetParametersUpdate asset, RiskTypeParametersUpdate tradinglimit)
		{
			this.General = general;
			this.Security = security;
			this.Asset = asset;
			this.TradingLimit = tradinglimit;
		}
	}
}
