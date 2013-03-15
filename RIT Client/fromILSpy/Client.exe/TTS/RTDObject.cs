using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
namespace TTS
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = true, MaxItemsInObjectGraph = 2147483647)]
	public class RTDObject : IRTDObject
	{
		public System.Collections.Generic.Dictionary<string, object> GetData(string[] t)
		{
			if (((Client)ThreadHelper.MainThread).CurrentUIState != Client.UIState.ACTIVE)
			{
				return null;
			}
			System.Collections.Generic.Dictionary<string, object> dictionary = new System.Collections.Generic.Dictionary<string, object>(t.Length);
			for (int i = 0; i < t.Length; i++)
			{
				string text = t[i];
				dictionary.Add(text, text);
			}
			System.Collections.Generic.IEnumerable<string[]> enumerable = 
				from x in t
				select x.Split(new char[]
				{
					'|'
				});
			System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<PriceVolume>> dictionary2 = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<PriceVolume>>();
			System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<PriceVolume>> dictionary3 = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<PriceVolume>>();
			foreach (string[] current in enumerable)
			{
				string key = string.Join("|", current);
				try
				{
					if (current.Length == 1)
					{
						if (current[0] == "TRADERID")
						{
							dictionary[key] = Game.State.Trader.TraderID;
						}
						else
						{
							if (current[0] == "PL")
							{
								dictionary[key] = Game.State.NLV;
							}
							else
							{
								if (current[0] == "TRADERNAME")
								{
									dictionary[key] = Game.State.Trader.FirstName + " " + Game.State.Trader.LastName;
								}
								else
								{
									if (current[0] == "TIMEREMAINING")
									{
										dictionary[key] = Game.State.General.TicksPerPeriod - Game.State.Current.Tick;
									}
									else
									{
										if (current[0] == "PERIOD")
										{
											dictionary[key] = Game.State.Current.Period;
										}
										else
										{
											if (current[0] == "YEARTIME")
											{
												dictionary[key] = Game.State.General.TicksPerYear;
											}
											else
											{
												if (current[0] == "TIMESPEED")
												{
													dictionary[key] = 1000m / Game.State.Current.MillisecondsPerTick;
												}
												else
												{
													if (current[0] == "ALLASSETTICKERS")
													{
														dictionary[key] = string.Join(",", (
															from x in Game.State.AssetInfoTable.Select()
															select (string)x["Ticker"]).ToArray<string>());
													}
													else
													{
														if (current[0] == "ALLASSETTICKERINFO")
														{
															dictionary[key] = string.Join(";", Game.State.AssetInfoTable.Select().Select(delegate(DataRow x)
															{
																string arg_159_0 = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}";
																object[] array = new object[11];
																array[0] = x["Ticker"];
																array[1] = x["Description"];
																array[2] = x["Type"];
																array[3] = x["StartPeriod"];
																array[4] = x["StopPeriod"];
																array[5] = x["TotalQuantity"];
																array[6] = (int)x["TotalQuantity"] - (int)x["LeaseCount"];
																array[7] = x["DisplayCost"];
																object[] arg_EF_0 = array;
																int arg_EF_1 = 8;
																string arg_EF_2;
																if (x["ConvertFrom"] != System.DBNull.Value)
																{
																	arg_EF_2 = string.Join("|", 
																		from y in x["ConvertFrom"] as TickerWeight[]
																		select y.ToString());
																}
																else
																{
																	arg_EF_2 = "";
																}
																arg_EF_0[arg_EF_1] = arg_EF_2;
																object[] arg_148_0 = array;
																int arg_148_1 = 9;
																string arg_148_2;
																if (x["ConvertTo"] != System.DBNull.Value)
																{
																	arg_148_2 = string.Join("|", 
																		from y in x["ConvertTo"] as TickerWeight[]
																		select y.ToString());
																}
																else
																{
																	arg_148_2 = "";
																}
																arg_148_0[arg_148_1] = arg_148_2;
																array[10] = x["TicksPerConversion"];
																return string.Format(arg_159_0, array);
															}));
														}
														else
														{
															if (current[0] == "ALLTICKERS")
															{
																dictionary[key] = string.Join(",", Game.State.Securities.Keys.ToArray<string>());
															}
															else
															{
																if (current[0] == "ALLTICKERINFO")
																{
																	dictionary[key] = string.Join(";", 
																		from x in Game.State.Securities.Values
																		select string.Format("{0},{1},{2},{3},{4},{5}", new object[]
																		{
																			x.Parameters.Ticker,
																			x.Parameters.Description,
																			x.Parameters.Type,
																			x.Parameters.StartPeriod,
																			x.Parameters.StopPeriod,
																			x.Parameters.UnitMultiplier
																		}));
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						int num;
						if (current[0] == "LATESTNEWS" && int.TryParse(current[1], out num) && num <= Game.State.NewsView.Count && num >= 1)
						{
							dictionary[key] = string.Format("{0},{1},{2},{3},{4}", new object[]
							{
								Game.State.NewsView[num - 1]["Period"],
								Game.State.NewsView[num - 1]["Tick"],
								Game.State.NewsView[num - 1]["Ticker"],
								Game.State.NewsView[num - 1]["Headline"],
								Game.State.NewsView[num - 1]["Body"]
							});
						}
						else
						{
							if (current[0] == "NEWS" && int.TryParse(current[1], out num) && num <= Game.State.NewsTable.Rows.Count && num >= 1)
							{
								dictionary[key] = string.Format("{0},{1},{2},{3},{4}", new object[]
								{
									Game.State.NewsTable.Rows[num - 1]["Period"],
									Game.State.NewsTable.Rows[num - 1]["Tick"],
									Game.State.NewsTable.Rows[num - 1]["Ticker"],
									Game.State.NewsTable.Rows[num - 1]["Headline"],
									Game.State.NewsTable.Rows[num - 1]["Body"]
								});
							}
							else
							{
								if (Game.State.Securities.ContainsKey(current[0]))
								{
									DataRow dataRow = Game.State.PortfolioTable.Rows.Find(current[0]);
									if (current[1] == "LAST")
									{
										dictionary[key] = Game.State.Securities[current[0]].Last;
									}
									else
									{
										if (current[1] == "BID" && current.Length == 2)
										{
											dictionary[key] = ((dataRow["Bid"] == System.DBNull.Value) ? null : dataRow["Bid"]);
										}
										else
										{
											if (current[1] == "ASK" && current.Length == 2)
											{
												dictionary[key] = ((dataRow["Ask"] == System.DBNull.Value) ? null : dataRow["Ask"]);
											}
											else
											{
												if (current[1] == "BSZ" && current.Length == 2)
												{
													dictionary[key] = Game.State.Securities[current[0]].BidSize;
												}
												else
												{
													if (current[1] == "ASZ" && current.Length == 2)
													{
														dictionary[key] = Game.State.Securities[current[0]].AskSize;
													}
													else
													{
														if (current[1] == "POSITION")
														{
															dictionary[key] = (dataRow.IsNull("Position") ? null : dataRow["Position"]);
														}
														else
														{
															if (current[1] == "VOLUME")
															{
																dictionary[key] = (dataRow.IsNull("Volume") ? null : dataRow["Volume"]);
															}
															else
															{
																if (current[1] == "TYPE")
																{
																	dictionary[key] = Game.State.Securities[current[0]].Parameters.Type.ToString();
																}
																else
																{
																	if (current[1] == "SIZE")
																	{
																		dictionary[key] = Game.State.Securities[current[0]].Parameters.UnitMultiplier;
																	}
																	else
																	{
																		if (current[1] == "COST")
																		{
																			dictionary[key] = (dataRow.IsNull("VWAP") ? null : dataRow["VWAP"]);
																		}
																		else
																		{
																			if (current[1] == "VWAP")
																			{
																				dictionary[key] = (dataRow.IsNull("SecurityVWAP") ? null : dataRow["SecurityVWAP"]);
																			}
																			else
																			{
																				if (current[1] == "NLV")
																				{
																					dictionary[key] = (dataRow.IsNull("NLV") ? null : dataRow["NLV"]);
																				}
																				else
																				{
																					if (current[1] == "PLUNR")
																					{
																						dictionary[key] = (dataRow.IsNull("Unrealized") ? null : dataRow["Unrealized"]);
																					}
																					else
																					{
																						if (current[1] == "PLREL")
																						{
																							dictionary[key] = (dataRow.IsNull("Realized") ? null : dataRow["Realized"]);
																						}
																						else
																						{
																							if (current[1] == "BID")
																							{
																								dictionary[key] = ((System.Convert.ToInt32(current[2]) <= Game.State.Securities[current[0]].BidView.Count) ? Game.State.Securities[current[0]].BidView[System.Convert.ToInt32(current[2]) - 1]["Price"] : null);
																							}
																							else
																							{
																								if (current[1] == "ASK")
																								{
																									dictionary[key] = ((System.Convert.ToInt32(current[2]) <= Game.State.Securities[current[0]].AskView.Count) ? Game.State.Securities[current[0]].AskView[System.Convert.ToInt32(current[2]) - 1]["Price"] : null);
																								}
																								else
																								{
																									if (current[1] == "BSZ")
																									{
																										dictionary[key] = ((System.Convert.ToInt32(current[2]) <= Game.State.Securities[current[0]].BidView.Count) ? Game.State.Securities[current[0]].BidView[System.Convert.ToInt32(current[2]) - 1]["VolumeRemaining"] : null);
																									}
																									else
																									{
																										if (current[1] == "ASZ")
																										{
																											dictionary[key] = ((System.Convert.ToInt32(current[2]) <= Game.State.Securities[current[0]].AskView.Count) ? Game.State.Securities[current[0]].AskView[System.Convert.ToInt32(current[2]) - 1]["VolumeRemaining"] : null);
																										}
																										else
																										{
																											if (current[1] == "MKTSELL")
																											{
																												dictionary[key] = this.MarketOrderVWAP(Game.State.Securities[current[0]].BidView, System.Convert.ToInt32(current[2]));
																											}
																											else
																											{
																												if (current[1] == "MKTBUY")
																												{
																													dictionary[key] = this.MarketOrderVWAP(Game.State.Securities[current[0]].AskView, System.Convert.ToInt32(current[2]));
																												}
																												else
																												{
																													if (current[1] == "BIDBOOK")
																													{
																														dictionary[key] = this.BookToString(Game.State.Securities[current[0]].BidView, 100);
																													}
																													else
																													{
																														if (current[1] == "ASKBOOK")
																														{
																															dictionary[key] = this.BookToString(Game.State.Securities[current[0]].AskView, 100);
																														}
																														else
																														{
																															if (current[1] == "OPENORDERS")
																															{
																																dictionary[key] = this.OrdersToString(Game.State.Securities[current[0]].OpenOrderView, 100);
																															}
																															else
																															{
																																if (current[1] == "ALLORDERS")
																																{
																																	dictionary[key] = this.OrdersToString(Game.State.Securities[current[0]].ArchivedOrderView, 100);
																																}
																																else
																																{
																																	if (current[1] == "AGBID")
																																	{
																																		dictionary[key] = ((this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].BidView, System.Convert.ToInt32(current[2]), ref dictionary2) != null) ? this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].BidView, System.Convert.ToInt32(current[2]), ref dictionary2).Price : null);
																																	}
																																	else
																																	{
																																		if (current[1] == "AGASK")
																																		{
																																			dictionary[key] = ((this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].AskView, System.Convert.ToInt32(current[2]), ref dictionary3) != null) ? this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].AskView, System.Convert.ToInt32(current[2]), ref dictionary3).Price : null);
																																		}
																																		else
																																		{
																																			if (current[1] == "AGBSZ")
																																			{
																																				dictionary[key] = ((this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].BidView, System.Convert.ToInt32(current[2]), ref dictionary2) != null) ? this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].BidView, System.Convert.ToInt32(current[2]), ref dictionary2).Volume : null);
																																			}
																																			else
																																			{
																																				if (current[1] == "AGASZ")
																																				{
																																					dictionary[key] = ((this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].AskView, System.Convert.ToInt32(current[2]), ref dictionary3) != null) ? this.AggregatePriceVolume(current[0], Game.State.Securities[current[0]].AskView, System.Convert.ToInt32(current[2]), ref dictionary3).Volume : null);
																																				}
																																				else
																																				{
																																					if (current[1] == "INTERESTRATE")
																																					{
																																						dictionary[key] = Game.State.Securities[current[0]].Parameters.InterestRate;
																																					}
																																					else
																																					{
																																						if (current[1] == "LIMITORDERS")
																																						{
																																							dictionary[key] = Game.State.Securities[current[0]].OpenLimitOrders;
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch (System.Exception)
				{
				}
			}
			return dictionary;
		}
		public int GetRefreshInterval()
		{
			return 500;
		}
		private PriceVolume AggregatePriceVolume(string topic, UpdateableDataView view, int i, ref System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<PriceVolume>> cache)
		{
			if (!cache.ContainsKey(topic))
			{
				cache.Add(topic, this.AggregateView(view, 20));
				return this.AggregatePriceVolume(topic, view, i, ref cache);
			}
			if (i > cache[topic].Count)
			{
				return null;
			}
			return cache[topic][i - 1];
		}
		private System.Collections.Generic.List<PriceVolume> AggregateView(UpdateableDataView view, int limit)
		{
			System.Collections.Generic.List<PriceVolume> list = new System.Collections.Generic.List<PriceVolume>();
			decimal d = -79228162514264337593543950335m;
			int num = 0;
			while (num < view.Count && list.Count < limit)
			{
				decimal num2 = (decimal)view[num]["Price"];
				decimal num3 = (decimal)view[num]["VolumeRemaining"];
				if (d != num2)
				{
					list.Add(new PriceVolume(num2, num3));
				}
				else
				{
					list[list.Count - 1].Volume += num3;
				}
				d = num2;
				num++;
			}
			return list;
		}
		private string OrdersToString(UpdateableDataView view, int limit)
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(limit);
			int num = 0;
			while (num < view.Count && num < limit)
			{
				DataRowView dataRowView = view[num];
				list.Add(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", new object[]
				{
					dataRowView["ID"],
					dataRowView["Price"],
					dataRowView["Volume"],
					dataRowView["VolumeRemaining"],
					dataRowView["VWAP"],
					dataRowView["Period"],
					dataRowView["Tick"],
					dataRowView["Timestamp"]
				}));
				num++;
			}
			return string.Join(";", list.ToArray());
		}
		private string BookToString(UpdateableDataView view, int limit)
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(limit);
			int num = 0;
			while (num < view.Count && num < limit)
			{
				DataRowView dataRowView = view[num];
				list.Add(string.Format("{0},{1},{2}", dataRowView["TraderID"], dataRowView["Price"], dataRowView["VolumeRemaining"]));
				num++;
			}
			return string.Join(";", list.ToArray());
		}
		private decimal MarketOrderVWAP(UpdateableDataView view, decimal volume)
		{
			decimal num = 0m;
			decimal num2 = volume;
			int num3 = 0;
			while (num3 < view.Count && num2 > 0m)
			{
				decimal num4 = System.Math.Min((decimal)view[num3]["VolumeRemaining"], num2);
				num = MathHelper.CalculateVWAP(num, volume - num2, num4, (decimal)view[num3]["Price"]);
				num2 -= num4;
				num3++;
			}
			return num;
		}
	}
}
